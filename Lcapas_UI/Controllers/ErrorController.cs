using Lcapas.Core.Library;
using Lcapas.Core.Logic;
using System;
using System.Web.Mvc;

namespace Lcapas.UI.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            string url = "https://applyalberta.ca/";

            ViewBag.ApasReturnPath = url;
            ViewBag.ApasLogoutPath = url;
            ViewBag.ApasWriteGifPath = string.Empty;
            return View();
        }
        public ViewResult NotFound()
        {
            string url = "http://www.lethbridgecollege.ca/admissions/how-apply";

            Response.Redirect(url);
            return View();
        }

        public ViewResult Error()
        {
            string url = "https://applyalberta.ca/";

            ViewBag.ApasReturnPath = url;
            ViewBag.ApasLogoutPath = url;
 
             return View();
        }

        //public ViewResult Error(string id, string errorMessage = null, bool sendEmail = true) {

        //    ApplicationsManager _SessionManager = new ApplicationsManager(id);

        //    if (!_SessionManager.Ready)
        //    {
        //        if (sendEmail)
        //        {
        //            new SendEmail(Structs.EmailType.TechErrorEmail, Structs.EmailReceiverGroup.Technical, id, !_SessionManager.Debugging, errorMessage);
        //        }

        //        return View("Index");
        //    }
        //    else
        //    {
        //        ViewBag.UUID = id;
        //        ViewBag.ApasReturnPath = _SessionManager.ApasReturnPath;
        //        ViewBag.ApasLogoutPath = _SessionManager.ApasLogoutPath;
        //        ViewBag.ApasWriteGifPath = _SessionManager.ApasWriteGifPath;
        //    }

        //    return View();
        //}
    }
}