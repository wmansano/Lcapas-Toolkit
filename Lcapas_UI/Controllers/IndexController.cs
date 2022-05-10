using Lcapas.Core.Library;
using Lcapas.Core.Logic;
using Lcapas.Core.Models.Lcappsdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Lcapas.UI.Controllers
{
    public class IndexController : Controller
    {
        // GET: Landing
        public ActionResult Index()
        {
            
            ViewBag.UserName = HttpContext.User.Identity.Name;
            ViewBag.SessionId = HttpContext.Request[Structs.Literals.SessionId];
            ViewBag.SecurityToken = HttpContext.Request[Structs.Literals.SecurityToken];
            ViewBag.Uuid = HttpContext.Request[Structs.Literals.UUID];

            // Testing UUID
            //ViewBag.Uuid = "d0b0facb-3b3d-4358-b7af-157684d8698e";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}