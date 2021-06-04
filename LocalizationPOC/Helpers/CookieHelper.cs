using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalizationPOC.Helpers
{
    public static class CookieHelper
    {
        /// <summary>
        /// Retrieves a single value from Request.Cookies
        /// </summary>
        public static string GetFromCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                string val = cookie.Value;
                if (!String.IsNullOrEmpty(val)) return Uri.UnescapeDataString(val);
            }
            return null;
        }

        /// <summary>
        /// Checks if a cookie / key exists in the current HttpContext.
        /// </summary>
        public static bool CookieExist(string cookieName)
        {
            HttpCookieCollection cookies = HttpContext.Current.Request.Cookies;
            return !string.IsNullOrEmpty(cookieName) && cookies[cookieName] != null;
        }
    }
}