using LocalizationPOC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace LocalizationPOC.Controllers.WAPI
{
    public class CultureController : ApiController
    {
        [Route("api/culture")]
        [HttpGet]
        public HttpResponseMessage GetActiveCulture()
        {
            if (CookieHelper.CookieExist("Exos.Culture"))
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent($"Exos.Culture: {CookieHelper.GetFromCookie("Exos.Culture")}")
                };
                return response;
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [Route("api/culture/{newCulture}")]
        [HttpGet]
        public HttpResponseMessage SaveCulture(string newCulture)
        {

            if (!string.IsNullOrWhiteSpace(newCulture))
            {
                // TODO: Validate culture
                var resp = new HttpResponseMessage();
                var cookie = new CookieHeaderValue("Exos.Culture", newCulture)
                {
                    Expires = DateTimeOffset.Now.AddDays(30),
                    Domain = Request.RequestUri.Host,
                    Path = "/"
                };
                resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                return resp;
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }


        [Route("api/culture/{newCulture}")]
        [HttpPost]
        public HttpResponseMessage Post(string newCulture)
        {
            if (!string.IsNullOrWhiteSpace(newCulture))
            {
                var resp = new HttpResponseMessage(HttpStatusCode.OK);
                var cookie = new CookieHeaderValue("Exos.Culture", newCulture)
                {
                    Expires = DateTimeOffset.Now.AddDays(30),
                    Domain = Request.RequestUri.Host,
                    Path = "/"
                };
                resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                resp.Content = new StringContent("success");
                return resp;
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }


    }
}
