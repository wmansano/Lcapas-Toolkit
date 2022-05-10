using Lcapas.Core.Logic;
using Lcapas.Core.Models.Lcappsdb;
using System.Web.Mvc;

namespace Lcapas.AD.Controllers
{
    public class TranscriptSettingsController : Controller
    {
        private LcapasLogic lcapasLogic = new LcapasLogic();

        // GET: Settings
        [AuthorizationRequired]
        public ActionResult Index()
        {
            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        [AuthorizationRequired]
        public ActionResult SynchronizeMessages()
        {
            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        [AuthorizationRequired]
        public ActionResult MessageStatus()
        {
            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        [AuthorizationRequired]
        public ActionResult RefreshInstitutions()
        {
            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        [AuthorizationRequired]
        public ActionResult SystemPreferences()
        {
            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        [AuthorizationRequired]
        public ActionResult ContactInformation()
        {
            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        [AuthorizationRequired]
        public ActionResult ConfigureEmail()
        {
            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        [AuthorizationRequired]
        public ActionResult DefaultStylesheets()
        {
            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        [AuthorizationRequired]
        public ActionResult EnabledFunctionality()
        {
            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        public ActionResult NotificationSettings()
        {
            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        [AuthorizationRequired]
        public ActionResult SecurityLog()
        {
            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        [AuthorizationRequired]
        public ActionResult OperationsLog()
        {
            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        [AuthorizationRequired]
        public ActionResult ToolkitUsers()
        {
            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        [AllowAnonymous]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                lcapasLogic.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}