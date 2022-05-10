using Lcapas.Core.Library;
using Lcapas.Core.Logic;

namespace System.Web.Mvc.Html
{
    public static class LcLinkExtensions
    {
        public static MvcHtmlString LcActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues = null, object htmlAttributes = null)
        {
            if (Functions.UserHasAccess(actionName, controllerName))
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
            }
            else
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, new { @class = "menu-disabled", disabled = "disabled", style = "color:grey;text-decoration:none;font-style:italic;cursor:auto;", title = "Access Denied" });
            }
        }

        public static bool LcAccessLink(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            return Functions.UserHasAccess(actionName, controllerName);
        }

        public static string ActivePage(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            string classValue = "";

            string currentController = htmlHelper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
            string currentAction = htmlHelper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();

            if (currentController == controllerName && currentAction == actionName)
            {
                classValue = "selected";
            }

            return classValue;
        }

        public static string TruncateString(this HtmlHelper htmlHelper, string value, int maxLength)
        {
            return Functions.TruncateString(value, maxLength) + (value.Trim().Length > maxLength ? "..." : "") ;
        }
    }
}