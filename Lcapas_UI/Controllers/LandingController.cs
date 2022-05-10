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
    public class LandingController : Controller
    {
        //private LcapasLogic lcapasLogic = new LcapasLogic();

        // GET: Landing
        public ActionResult Landing(string Uuid, string UserName, string SessionId, string SecurityToken)
        {
            string _exMsg = string.Empty;
            LandingObj _Landing = new LandingObj();
            DateTime startTime = DateTime.Now;

            using (ApplicationsManager _ApplicationsManager = new ApplicationsManager(Uuid, UserName, SessionId, SecurityToken)) {
                try
                {
                    //lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.LandingController, "Index", "Info", "Landing: User: " + _ApplicationsManager.Username + " - UUID: " + _ApplicationsManager.UUID);

                    // This is Test Account!!!
                    // bool LC_Testuser = _ApplicationsManager.Username == "Testuser Lethbridge1";

                    if (_ApplicationsManager.Ready)
                    {
                        using (LcapasLogic lcapasLogic = new LcapasLogic()) {

                            _Landing = lcapasLogic.GetLanding(_ApplicationsManager.UUID);

                            ViewBag.UUID = _ApplicationsManager.UUID;
                            ViewBag.ApasReturnPath = _ApplicationsManager.ApasReturnPath;
                            ViewBag.ApasLogoutPath = _ApplicationsManager.ApasLogoutPath;
                            ViewBag.ApasWriteGifPath = _ApplicationsManager.ApasWriteGifPath;

                            ViewBag.Programs = lcapasLogic.GetApplicationPrograms().Select(p => new { ApplicationProgramId = p.ApplicationProgramId, Description = string.Format("{0} ({1})", p.ProgramDesc, p.ProgramCode) });
                            ViewBag.Campuses = lcapasLogic.GetCampuses();
                            ViewBag.Terms = lcapasLogic.GetTerms();
                            ViewBag.StartingYears = lcapasLogic.GetStartingYears();
                            ViewBag.ReferenceTypes = lcapasLogic.GetReferenceTypes();

                            ViewBag.ApplicationFee = lcapasLogic.GetApplicationFee().ApplicationFeeAmt.ToString();
                        }                     

                        ViewBag.Environment = Functions.GetEnvironment();

                        return View(_Landing);
                    }
                    else
                    {
                        _exMsg = ", Error: " + Structs.ErrorMessages.SessionManagerNotReady;
                    }
                }
                catch (Exception ex)
                {
                    _exMsg = ", Error: " + ex.ToString();
                }

                // Capture Exception
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.LandingController, "Index Action", "Error", "UUID: " + _ApplicationsManager.UUID + _exMsg);
                }
                
            }

            // Redirect back to APAS
            return RedirectToAction("Error", "Error");
        }

        public ActionResult Agreement(LandingObj studentapplication)
        {
            string _exMsg = string.Empty;

            try
            {
                using (ApplicationsManager _ApplicationsManager = new ApplicationsManager(studentapplication.UUID)) {
                    if (_ApplicationsManager.Debugging || _ApplicationsManager.Ready)
                    {
                        using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                            if (lcapasLogic.UpdateStudentApplication(studentapplication))
                            {
                                _ApplicationsManager.Submitted = true;
                                ViewBag.UUID = _ApplicationsManager.UUID;
                                ViewBag.ApasReturnPath = _ApplicationsManager.ApasReturnPath;
                                ViewBag.ApasLogoutPath = _ApplicationsManager.ApasLogoutPath;
                                ViewBag.ApasWriteGifPath = _ApplicationsManager.ApasWriteGifPath;

                                ViewBag.ApplicationFee = lcapasLogic.GetApplicationFee().ApplicationFeeAmt.ToString();

                                ViewBag.Environment = Functions.GetEnvironment();

                                return View();
                            }
                            else
                            {
                                throw new Exception("Error updating student application.");
                            }
                        }
                    }
                    else
                    {
                        _exMsg = ", Error: " + Structs.ErrorMessages.SessionManagerNotReady;
                    }
                }               
            }
            catch (Exception ex)
            {
                _exMsg = ", Error: " + ex.ToString();
            }

            using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.LandingController, "Agreement Action", "Error", "UUID: " + studentapplication.UUID + _exMsg);
            }

            return RedirectToAction("Error", "Error");
        }

        [HttpPost]
        public ActionResult Payment(string uuid)
        {
            string _exMsg = string.Empty;

            try
            {
                using (ApplicationsManager _ApplicationsManager = new ApplicationsManager(uuid)) {
                    if (_ApplicationsManager.Debugging || _ApplicationsManager.Ready)
                    {
                        ViewBag.UUID = _ApplicationsManager.UUID;
                        ViewBag.ApasReturnPath = _ApplicationsManager.ApasReturnPath;
                        ViewBag.ApasLogoutPath = _ApplicationsManager.ApasLogoutPath;
                        ViewBag.ApasWriteGifPath = _ApplicationsManager.ApasWriteGifPath;

                        using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                            ApplicationFee _ApplicationFee = lcapasLogic.GetApplicationFee();
                            ViewBag.ApplicationFee = _ApplicationFee.ApplicationFeeAmt.ToString();
                            ViewBag.ApplicationFeeMessage = _ApplicationFee.Message;
                            // Skip PayPal payment if application fee amount is $0.00
                            if (!string.IsNullOrWhiteSpace(ViewBag.ApplicationFee) && ViewBag.ApplicationFee == "0")
                            {
                                lcapasLogic.MarkApplicationMessageAsPaid(uuid);
                            }
                        }

                        ViewBag.Environment = Functions.GetEnvironment();

                        return View();
                    }
                    else
                    {
                        _exMsg = ", Error: " + Structs.ErrorMessages.SessionManagerNotReady;
                    }
                }     
            }
            catch (Exception ex)
            {
                _exMsg = ", Error: " + ex.ToString();
            }

            using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.LandingController, "Payment Action", "Error", "UUID: " + uuid + _exMsg);
            }

            return RedirectToAction("Error", "Error");
        }

        #region async calls

        public ActionResult FilterProgramDetails(string programId, string uuid)
        {
            string _exMsg = string.Empty;
            List<ProgTermCampObj> _ProgramTermCampuses = new List<ProgTermCampObj>();
            try
            {
                //ApplicationsManager _ApplicationsManager = new ApplicationsManager(uuid);

                //if (_ApplicationsManager.Debugging || _ApplicationsManager.Ready)
                //{
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    _ProgramTermCampuses = lcapasLogic.GetProgramDetailsByProgramId(programId);
                }
                
                //}
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.LandingController, "FilterProgramDetails Action", "Error", "UUID: " + uuid + ", Error: " + ex.ToString());
                }
                
                return Json(false);
            }

            return Json(_ProgramTermCampuses, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProgramTerms(string uuid)
        {
            string _exMsg = string.Empty;
            List<ProgramTerm> _ProgramTerms = new List<ProgramTerm>();
            try
            {
                //ApplicationsManager _ApplicationsManager = new ApplicationsManager(uuid);

                //if (_ApplicationsManager.Debugging || _ApplicationsManager.Ready)
                //{
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    _ProgramTerms = lcapasLogic.GetTerms();
                }
                
                //}
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.LandingController, "GetProgramTerms Action", "Error", "UUID: " + uuid + ", Error: " + ex.ToString());
                }
                

                return Json(false);
            }
            
            return Json(_ProgramTerms, JsonRequestBehavior.AllowGet);
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    //lcapasLogic.Dispose();
            //}
            base.Dispose(disposing);
        }
    }
}