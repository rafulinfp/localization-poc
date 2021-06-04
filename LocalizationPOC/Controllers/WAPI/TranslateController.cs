using LocalizationPOC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Resources;
using System.Web.Http;

namespace LocalizationPOC.Controllers.WAPI
{
    public class TranslateController : ApiController
    {
        [Route("api/translate/{key}")]
        [HttpGet]
        public HttpResponseMessage Translate(string key)
        {
            HttpResponseMessage response;
            var culture = "en-US";
            if (CookieHelper.CookieExist("Exos.Culture"))
            {
                culture = CookieHelper.GetFromCookie("Exos.Culture");
            }
            var val = LocalizationPOC.Consumer.ResourceManager.GetString(key, new System.Globalization.CultureInfo(culture));
            if (val != null)
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(val)
                };
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return response;
        }


        [Route("api/translate")]
        [HttpPost]
        public HttpResponseMessage TranslateBatch([FromBody] string[] keys)
        {
            HttpResponseMessage response;
            var culture = "en-US";
            if (CookieHelper.CookieExist("Exos.Culture"))
            {
                culture = CookieHelper.GetFromCookie("Exos.Culture");
            }
            var translations = new List<dynamic>();
            foreach (var key in keys)
            {
                translations.Add(
                    new
                    {
                        Key = key,
                        Value = LocalizationPOC.Consumer.ResourceManager.GetString(key, new System.Globalization.CultureInfo(culture))
                    });
            }
            if (translations != null)
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(translations))
                };
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return response;
        }
    }
}
