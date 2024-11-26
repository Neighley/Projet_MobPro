using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Projet_MobPro.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString YesNoFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool?>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            bool? nullableValue = metadata.Model as bool?;
            bool value = nullableValue ?? false;
            return new MvcHtmlString(value ? "Oui" : "Non");
        }
    }
}