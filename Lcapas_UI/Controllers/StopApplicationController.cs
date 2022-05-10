using System;
using System.Web.Mvc;
using Lcapas.Core.Library;
using Lcapas.Core.Logic;
using System.Linq;

namespace Lcapas.UI.Controllers
{
    public class StopApplicationController : Controller
    {
        //private lcapasLogic_entities ctx = new lcapasLogic_entities();
        private LcapasLogic lcapasLogic = new LcapasLogic();

        // GET: Landing
        public ActionResult Index()
        {
            string uuid = string.Empty;
            string sessionId = string.Empty;
            string securityToken = string.Empty;
            string applicationID = string.Empty;

            try
            {
                uuid = Request[Structs.Literals.UUID];
                applicationID = Request[Structs.Literals.ApplicationID];
                uuid = lcapasLogic.GetPaidUuidByApplicationID(applicationID);

                ApplicationsManager _ApplicationsManager = new ApplicationsManager(uuid)
                {
                    Debugging = Request.Url.Host.Contains(Structs.Environment.Localhost) ? true : false,
                    SessionId = Request[Structs.Literals.SessionId],
                    SecurityToken = Request[Structs.Literals.SecurityToken],
                    Cancelled = true
                };

                //if (_ApplicationsManager.Ready)
                //{
                    new SendEmail(Structs.EmailType.ApplicationCancelPayment, Structs.EmailReceiverGroup.Student, uuid, !_ApplicationsManager.Debugging, null, applicationID);
                    new SendEmail(Structs.EmailType.ApplicationCancelPayment, Structs.EmailReceiverGroup.Admissions, uuid, !_ApplicationsManager.Debugging, null, applicationID);
                    new SendEmail(Structs.EmailType.ApplicationCancelPayment, Structs.EmailReceiverGroup.Technical, uuid, !_ApplicationsManager.Debugging, null, applicationID);
                //}

                    ViewBag.UUID = _ApplicationsManager.UUID;
                    ViewBag.ApasWriteGifPath = _ApplicationsManager.ApasWriteGifPath;
                    ViewBag.ApasReturnPath = _ApplicationsManager.ApasReturnPath;
                    ViewBag.ApasLogoutPath = _ApplicationsManager.ApasLogoutPath;
                    ViewBag.ApasWriteGifPath = _ApplicationsManager.ApasWriteGifPath;

                    ViewBag.Environment = Functions.GetEnvironment();

                    //return View();
                //}
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.StopApplicationController, "StopApplication", "Error", ex.ToString());
                return RedirectToAction("Error", "Error");
            }

            //return RedirectToAction("Error", "Error");
            return View();
        }

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