using Lcapas.Core.Logic;
using Lcapas.Core.Library;
using System;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services;
using System.Threading.Tasks;
//using Ellucian.Dmi.Client;

namespace Lcapas.AD.Controllers
{
    [WebService(Namespace = "http://lcapps.lethbridgecollege.ca/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class LoginController : Controller
    {

        [AllowAnonymous]
        public ActionResult Index()
        {
            //using (LcapasLogic lcapasLogic = new LcapasLogic())
            //{
                //lcapasLogic.ParseApasMessage("ba0d7f17-2d12-49eb-af3a-a5ff5d435a74", Enums.MessageTypes.ReceivedRequest);  // McLean, Dean
                //lcapasLogic.ParseApasMessage("3b90e2cd-a6b9-4e27-a71c-c8eb6adc3395", Enums.MessageTypes.ReceivedRequest);  // Vandesteeg, Lana

                //lcapasLogic.ParseApasMessage("f1bc2da0-1857-4917-8b5f-e5edafdf426d", Enums.MessageTypes.ReceivedRequest);  // Aitkens, Danielle
                //lcapasLogic.ParseApasMessage("6cf6d171-6b2c-4b50-a096-63b7fb0a89cc", Enums.MessageTypes.ReceivedRequest);  // Tomato, Brandywine
                //lcapasLogic.ParseApasMessage("2d102acd-6e5a-4bea-a8c3-3ba0eee7be30", Enums.MessageTypes.ReceivedRequest);  // Simpson, Marge
                //lcapasLogic.ParseApasMessage("d0a6f83a-3ea7-4f08-9e13-1d7955af6814", Enums.MessageTypes.ReceivedRequest);  // Simpson, Bart
                //lcapasLogic.ParseApasMessage("8294fde2-de9c-412b-8aeb-0815b740f07a", Enums.MessageTypes.ReceivedRequest);  // Flanders, Ned

                //lcapasLogic.ParseApasMessage("8769aca6-07c2-271f-e053-280a000ad9b1", Enums.MessageTypes.ReceivedResponse);
                //lcapasLogic.ReParseAcademicRecordResponses();

                //Core.Models.Lcappsdb.Request _Request = lcapasLogic.GetRequestByRequestTrackingId("LCRTD201807301609-d06dcd20-40f0-4de");
                //lcapasLogic.PrepareAutoResponse(_Request);

                //lcapasLogic.QueueSendBulkTranscript();

                //lcapasLogic.ParseArchivedApplications();

                //lcapasLogic.QueueLookupSNumberByANS();

                //lcapasLogic.ResendPaidUnsentApplications();

                //lcapasLogic.LoadApplications(new System.Collections.Generic.List<string>() { "68cb3968-a1cb-46e9-89ae-7ac83ecf381e" }).Export();  // ApplicationMessage UUID
            //}

            //CollWebApi.MainDriver mainDriver = new CollWebApi.MainDriver();
            //mainDriver.GetXmlTranscripts("PS", "168ba42d-d231-4997-8f0d-ad3cd7ea43af");
            //mainDriver.SaveRequestLog("PS", "dd10cdee-6ffb-4e32-ba28-06ec9323a45c");

            //using (ColleagueLogic collLogic = new ColleagueLogic())
            //{
            //    collLogic.GetColleagueTranscript("04218631-36e4-4b8e-9351-4f0fec717a03");

            //}

            // Run a Job
            //using (TranscriptsManager transManager = new TranscriptsManager())
            //{
            //    using (LcapasLogic lcapasLogic = new LcapasLogic())
            //    {
            //        transManager.RunJob(lcapasLogic.GetJob("bb5d8e70-6141-11e9-9f3a-00505687cc62"));
            //    }
            //}

            ViewBag.Environment = Functions.GetEnvironment();

            using (LcapasLogic lcapasLogic = new LcapasLogic())
            {
                if (lcapasLogic.GetSetting(Structs.SettingTypes.Boolean, Structs.Settings.AutoLogin))
                {
                    LoginModel model = new LoginModel()
                    {
                        sNumber = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LcIntegrationUserName),
                        Password = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LcIntegrationPassword),
                        RememberMe = true
                    };

                    return Index(model);
                }
            }

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            bool success = false;

            // run transcript manager
            //TranscriptsManager manager = new TranscriptsManager();

            //using (LcapasLogic lcapasLogic = new LcapasLogic())
            //{
            //    // lcapasLogic.ImportPastApplications("75667732-5edf-4df1-9f2b-d84c588552b1");

            //   //lcapasLogic.ParseApplicationMessage("");
            //    lcapasLogic.ParseApasMessage("9d5a78fa-01d7-4e07-9f83-448b002aee81", Enums.MessageTypes.ReceivedResponse);

            //lcapasLogic.ParseApasMessage("1219879a-aa59-420b-8e1c-74f32122185b", Enums.MessageTypes.Application);
            // lcapasLogic.ParseApasMessage("504dc8e7-4ca2-02dd-e053-460a000a83a6", Enums.MessageTypes.ReceivedResponse);  // College Transcript XML Message
            // lcapasLogic.ParseApasMessage("d2ce506b-4d30-4bbd-b463-7880bee96dd6", Enums.MessageTypes.ReceivedResponse);  // High School Transcript XML Message
            // lcapasLogic.ParseApasMessage("71ead944-02ef-42df-9d51-c950a139d261", Enums.MessageTypes.ReceivedRequest);  // Transcript Request XML Message
            // lcapasLogic.ParseApasMessage("504dc8e7-4b87-02dd-e053-460a000a83a6", Enums.MessageTypes.ReceivedResponse);  // Transcript Response XML Message

            //lcapasLogic.ResendPaidUnsentApplications();
            //}

            //using (ApasLogic apasLogic = new ApasLogic())
            //{
                //apasLogic.SendTransferResponse("2cc2a772-1c09-4a2c-b64f-4cd5aa039e0c");
                //apasLogic.SendApasMessage("79d4eb6b-2675-4500-a92b-7f1ed9d6c826");  // Tinker Bell
                //apasLogic.GetApasEducationalInstitutions();
                //apasLogic.GetApasHostInstitution();
            //}

            // Lana Vandesteeg: bc43c63e-88d0-4ca0-bcf8-7919c083e6e1
            // Tinker Bell: 79d4eb6b-2675-4500-a92b-7f1ed9d6c826

            //CollWebApi.MainDriver driver = new CollWebApi.MainDriver();
            //driver.GetXmlTranscripts("PS", "901b3bcc-e519-4881-a726-34997d37cbf5");

            //using (LcapasLogic lcapasLogic = new LcapasLogic()) {
            //string transtr = lcapasLogic.GetTransactionTranscriptResult("d2a82102-3dff-4d0f-9273-5a56fbace5a7");

            //lcapasLogic.ParseColleagueXmlString(transtr);

            //Core.Library.Apas.CollegeTranscript.CollegeTranscript _CollegeTranscript = LcapasLogic.DeserializeCollegeTranscript(transtr);

            //} 


            try
            {
                if (this.ModelState.IsValid)
                {
                    if (Membership.ValidateUser(model.sNumber, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.sNumber, model.RememberMe);

                        UserAccessObj _UserAccess = new UserAccessObj();
                        _UserAccess.Load(model.sNumber);

                        if (_UserAccess.LoginDateTime != null) {
                            success = true;
                        }
                        else
                        {
                            FormsAuthentication.RedirectToLoginPage();
                        }
                    }
                    else
                    {
                        ViewBag.Message = "sNumber and password are not valid!";
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            if (success)
            {
                //RunStartupProcesses();
                //return this.RedirectToAction("Index", "Home");
                // for troubleshooting transcripts
                return this.RedirectToAction("Index", "Home");

            }
            else {

                ViewBag.Environment = Functions.GetEnvironment();

                return this.View(model);
            }   
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            HttpContext.Session.Abandon();
            return this.RedirectToAction("Index", "Login");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        this.Dispose();
        //        //lcapasLogic.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}