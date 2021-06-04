using LocalizationPOC.Helpers;
using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace LocalizationPOC.Attributes
{
    public class LocalizationAttribute : ActionFilterAttribute
    {
        private readonly string _defaultLanguage = "en-US";

        public LocalizationAttribute(string defaultLanguage)
        {
            _defaultLanguage = defaultLanguage;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           base.OnActionExecuting(filterContext);

            string culture, uiCulture;
            culture = uiCulture = _defaultLanguage;

            if (CookieHelper.CookieExist("Exos.Culture"))
            {
                culture = uiCulture = CookieHelper.GetFromCookie("Exos.Culture") ?? _defaultLanguage;
            }

            try
            {
                // set Resource culture
                Consumer.Culture = new CultureInfo(culture);

                // set thread culture
                Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(uiCulture);
            }
            catch (Exception)
            {
                throw new NotSupportedException(string.Format("ERROR: Invalid language culture '{0}', uiCulture '{1}'.", culture, uiCulture));
            }
        }
    }
}