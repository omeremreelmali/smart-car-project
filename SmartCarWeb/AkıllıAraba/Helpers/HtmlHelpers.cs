using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace AkıllıAraba.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString IconLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, String iconName, object htmlAttributes = null)
        {
            var linkMarkup = htmlHelper.ActionLink(linkText, actionName, routeValues, htmlAttributes).ToHtmlString();
            var iconMarkup = String.Format("<span class=\"{0}\" aria-hidden=\"true\"></span>", iconName);
            return new MvcHtmlString(linkMarkup.Insert(linkMarkup.IndexOf(@"</a>"), iconMarkup));
        }
        public static MvcHtmlString IconLink(this HtmlHelper helper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, string beforeText, string afterText)
        {
            var link = helper.ActionLink(string.Format("before{0}after", linkText), actionName, controllerName, routeValues, htmlAttributes).ToString();
            link = link.Replace("before", beforeText).Replace("after", afterText);
            return MvcHtmlString.Create(link);
        }
        public static MvcHtmlString IconLink(this AjaxHelper helper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes, string beforeText, string afterText)
        {
            var link = helper.ActionLink(string.Format("before{0}after", linkText), actionName, controllerName, routeValues, ajaxOptions, htmlAttributes).ToString();
            link = link.Replace("before", beforeText).Replace("after", afterText);
            return MvcHtmlString.Create(link);
        }

        public static MvcHtmlString IconLink(this HtmlHelper helper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, string beforeText)
        {
            var link = helper.ActionLink(string.Format("before{0}", linkText), actionName, controllerName, routeValues, htmlAttributes).ToString();
            link = link.Replace("before", beforeText);
            return MvcHtmlString.Create(link);
        }
        public static MvcHtmlString IconLink(this AjaxHelper helper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, Dictionary<string, object> htmlAttributes, string beforeText)
        {
            var link = helper.ActionLink(string.Format("before{0}", linkText), actionName, controllerName, routeValues, ajaxOptions, htmlAttributes).ToString();
            link = link.Replace("before", beforeText);
            return MvcHtmlString.Create(link);
        }
        public static MvcHtmlString IconLink(this HtmlHelper helper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, Dictionary<string, object> htmlAttributes, string beforeText)
        {
            var link = helper.ActionLink(string.Format("before{0}", linkText), actionName, controllerName, routeValues, htmlAttributes).ToString();
            link = link.Replace("before", beforeText);
            return MvcHtmlString.Create(link);
        }
        public static MvcHtmlString IconLink(this AjaxHelper helper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes, string beforeText)
        {
            var link = helper.ActionLink(string.Format("before{0}", linkText), actionName, controllerName, routeValues, ajaxOptions, htmlAttributes).ToString();
            link = link.Replace("before", beforeText);
            return MvcHtmlString.Create(link);
        }
        public static IHtmlString Image(this HtmlHelper helper, string src, string alt)
        {
            var tb = new TagBuilder("img");
            tb.Attributes["src"] = src;
            tb.Attributes["alt"] = alt;
            return new MvcHtmlString(tb.ToString());
        }
    }
}