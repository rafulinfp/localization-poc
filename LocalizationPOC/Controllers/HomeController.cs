using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace LocalizationPOC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.CulturesCollection = new List<CultureOption>
            {
                new CultureOption { Value = "en-US", Name = "English (US)" },
                new CultureOption { Value = "en-CA", Name = "English (Canada)" },
                new CultureOption { Value = "en-GB", Name = "English (UK)" },
                new CultureOption { Value = "es-US", Name = "Spanish (US)" },
                new CultureOption { Value = "es-ES", Name = "Spanish (ES)" },
                new CultureOption { Value = "fr-CA", Name = "France (Canada)" },
                new CultureOption { Value = "fr-FR", Name = "France (France)" },
            };

            var resourceSet = LocalizationPOC.Consumer.ResourceManager.GetResourceSet(System.Threading.Thread.CurrentThread.CurrentUICulture, true, true);

            var enumerator = resourceSet.GetEnumerator();
            List<string> resourceStrings = new List<string>();
            while (enumerator.MoveNext())
            {
                resourceStrings.Add(enumerator.Key.ToString());
            }
            ViewBag.ResourceKeys = resourceStrings;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    public class CultureOption
    {
        public string Value { get; set; }
        public string Name { get; set; }
    }
}