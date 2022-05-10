using Lcapas.Core.Logic;
using System.Web.Mvc;

namespace Lcapas.AD.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Response.StatusCode = 401;
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Error()
        {

            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        [AllowAnonymous]
        public ActionResult Unauthorised()
        {

            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }
    }
}