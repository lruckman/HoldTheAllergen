using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using HoldTheAllergen.Data.DataAccess.Impl;
using HoldTheAllergen.Data.Models;
using HoldTheAllergen.Models;
using HoldTheAllergen.Models.FormHandlers;

namespace HoldTheAllergen.Crawler.MarieCallenders
{
    internal class Program
    {
        private const string IngredientPattern =
            "class=\"clsPopupRegularTextBold\">(?<name>[^:]*):</span>([^<]*)<span ([^>]*) class=\"clsPopupTextSmall\">(?<value>[^<]*)</span>";

        private const string IngredientNameGroupName = "name";
        private const string IngredientValueGroupName = "value";
        private static readonly string DataSource = ConfigurationManager.AppSettings["DataSource"];
        private static readonly string DataSourceTargetExtensions = ConfigurationManager.AppSettings["DataSourceTargetExtensions"];

        public const int RestaurantId = 4885;

        private static readonly HoldTheAllergenEntities Db =
            new HoldTheAllergenEntities(ConfigurationManager.ConnectionStrings["HoldTheAllergenEntities"].ConnectionString);

        private static readonly RestaurantIngredientCreateModelHandler CreateModelHandler =
            new RestaurantIngredientCreateModelHandler(new RestaurantRepository(Db), new AllergenRepository(Db));

        private static void Main()
        {
            foreach (var ingredients in System.IO.Directory.GetFiles(DataSource, DataSourceTargetExtensions)
                .Select(System.IO.File.ReadAllText)
                .Select(ParseFileContents))
            {
                SaveIngredients(ingredients);
            }
        }

        private static IEnumerable<KeyValuePair<string, string>> ParseFileContents(string fileContents)
        {
            var ingredients = new Dictionary<string, string>();
            var ingredientRegEx = new Regex(IngredientPattern, RegexOptions.IgnoreCase);
            foreach (Match match in ingredientRegEx.Matches(fileContents))
            {
                var key = match.Groups[IngredientNameGroupName].Value;

                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }
                else
                {
                    key = key.Replace(Environment.NewLine, "").Trim();
                }

                var value = match.Groups[IngredientValueGroupName].Value;

                if (!string.IsNullOrWhiteSpace(value))
                {
                    value = value.Replace(Environment.NewLine, "").Trim();
                }

                if (!ingredients.ContainsKey(key))
                {
                    ingredients.Add(key, value);
                }
            }

            return ingredients;
        }

        private static void SaveIngredients(IEnumerable<KeyValuePair<string, string>> ingredients)
        {
            foreach (var form in from ingredient in ingredients
                                 where !Db.RestaurantIngredients.Any(
                                         x => x.Name == ingredient.Key && x.RestaurantId == RestaurantId)
                                 select new RestaurantIngredientCreateModel
                                     {
                                         AllergenIds = new int[] {},
                                         CreateMenuItem = false,
                                         Description = ingredient.Value,
                                         GroupName = null,
                                         Name = ingredient.Key,
                                         RestaurantId = RestaurantId
                                     })
            {
                Console.WriteLine("Found Ingredient: {0} \r\n{1}", form.Name, form.Description);
                CreateModelHandler.Handle(form);
            }
        }
    }
}