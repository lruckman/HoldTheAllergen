using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HoldTheAllergen.Backend.Core.Helpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty[]>> expression,
            MultiSelectList listOfValues)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var sb = new StringBuilder();
            var labelText = metaData.Model;

            if (listOfValues != null)
            {
                foreach (var item in listOfValues)
                {
                    var id = string.Format("{0}_{1}",
                                           htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(
                                               ExpressionHelper.GetExpressionText(expression)), item.Value);
                    var name =
                        htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(
                            ExpressionHelper.GetExpressionText(expression));
                    //var label = htmlHelper.Label(id, HttpUtility.HtmlEncode(item.Text));

                    //Get currently select values from the ViewData model
                    var list = expression.Compile().Invoke(htmlHelper.ViewData.Model);

                    //Convert selected value list to a List<string> for easy manipulation
                    var selectedValues = new List<string>();

                    if (list != null)
                    {
                        selectedValues = new List<TProperty>(list).ConvertAll(i => i.ToString());
                    }

                    var cb = new TagBuilder("input");
                    cb.MergeAttribute("type", "checkbox");
                    cb.MergeAttribute("name", name);
                    cb.MergeAttribute("value", item.Value);
                    cb.MergeAttribute("id", id);

                    if (selectedValues.Contains(item.Value))
                    {
                        cb.MergeAttribute("checked", "checked");
                    }

                    var label = new TagBuilder("label") {InnerHtml = HttpUtility.HtmlEncode(item.Text) + " " + cb};
                    label.MergeAttribute("class", "checkbox");
                    sb.Append(label);
                }
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}