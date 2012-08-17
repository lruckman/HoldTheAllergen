using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using HoldTheAllergen.Data.DataAccess.Impl;
using HoldTheAllergen.Data.Models;
using HoldTheAllergen.Models;
using HoldTheAllergen.Models.FormHandlers;

namespace HoldTheAllergen.Crawler.BaskinRobbins
{
    internal class Program
    {
        public const string RestaurantDomain = "http://www.baskinrobbins.com";
        public const string StartUrl = RestaurantDomain + "/content/baskinrobbins/en/nutritioncatalog.html";
        public const string PagePattern = @"popFire\('(?<page>[^']*)'\)";

        public const string IngredientPattern =
            "class=\"title\">(?<name>(.|\n)+)</h1>((.|\n)+) class=\"ingredients\"((.|\n)+)<p>(?<description>(.|\n)+)</p>";

        public const string AllergenPattern =
            "<a href=\"javascript:void\\(0\\);\" class=\"([^>]*)>(?<allergen>[^<]*)</a>";

        public const int RestaurantId = 4883;

        private static readonly HoldTheAllergenEntities Db =
            new HoldTheAllergenEntities(
                ConfigurationManager.ConnectionStrings["HoldTheAllergenEntities"].ConnectionString);

        private static readonly RestaurantIngredientCreateModelHandler CreateModelHandler =
            new RestaurantIngredientCreateModelHandler(new RestaurantRepository(Db), new AllergenRepository(Db));

        private static void Main(string[] args)
        {
            foreach (var nutritionUrl in GetNutritionPages(PagePattern, GetHtml(StartUrl)))
            {
                Thread.Sleep(2500);
                Console.WriteLine("Found: {0}", nutritionUrl);
                var nutritionPageUrl = !nutritionUrl.ToLowerInvariant().StartsWith(RestaurantDomain)
                                        ? RestaurantDomain + nutritionUrl
                                        : nutritionUrl;
                var nutritionPage = GetHtml(nutritionPageUrl);

                var ingredient = ExtractIngredientInformation(IngredientPattern, AllergenPattern, nutritionPage);

                if (ingredient==null)
                {
                    continue;
                }

                if (!Db.RestaurantIngredients.Any(x => x.Name == ingredient.Name && x.RestaurantId == RestaurantId))
                {
                    CreateModelHandler.Handle(ingredient);
                }
                else
                {
                    Console.WriteLine(" Skipping.");
                }
            }
            Console.WriteLine("DONE.");
            Console.ReadLine();
        }

        private static string GetHtml(string requestUriString)
        {
            var req = WebRequest.Create(requestUriString);
            req.Method = "GET";

            using (var reader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }

        private static IEnumerable<string> GetNutritionPages(string pattern, string input)
        {
            return from Match match in Regex.Matches(input, pattern) select match.Groups["page"].Value;
        }

        private static RestaurantIngredientCreateModel ExtractIngredientInformation(string ingredientPattern,
                                                                                    string allergenPattern, string input)
        {
            if (!Regex.IsMatch(input, ingredientPattern))
            {
                return null;
            }

            var ingredientMatch = Regex.Match(input, ingredientPattern);

            var ingredientName = ingredientMatch.Groups["name"].Value.Replace(Environment.NewLine, "").Trim();
            var ingredientDescription = ingredientMatch.Groups["description"].Value.Replace(Environment.NewLine,"").Trim();

            Console.WriteLine("Found Ingredient: {0} \r\n{1}", ingredientName, ingredientDescription);

            return new RestaurantIngredientCreateModel
                {
                    AllergenIds = (from Match m in Regex.Matches(input, allergenPattern)
                                   select m.Groups["allergen"].Value.Trim()
                                   into allergenName where Db.Allergens.Any(x => x.Name == allergenName)
                                   select Db.Allergens.First(x => x.Name == allergenName).Id).ToArray(),
                    CreateMenuItem = true,
                    Description = ingredientDescription,
                    GroupName = "",
                    Name = ingredientName,
                    RestaurantId = RestaurantId
                };
        }
    }
}