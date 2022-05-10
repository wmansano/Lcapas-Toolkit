using Lcapas.Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Lcapas.AD.FilterConfig;

namespace Lcapas.AD.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizationRequired]
        public ActionResult Index()
        {
 
            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }
    }
}