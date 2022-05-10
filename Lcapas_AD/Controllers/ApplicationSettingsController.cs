using Lcapas.Core.Library;
using Lcapas.Core.Logic;
using Lcapas.Core.Models.Lcappsdb;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lcapas.AD.Controllers
{
    public class ApplicationSettingsController : Controller
    {
        private LcapasLogic lcapasLogic = new LcapasLogic();

        [AuthorizationRequired]
        public ActionResult Index()
        {
            ViewBag.Environment = Functions.GetEnvironment();
            return View();
        }

        #region Programs

        // GET: ApplicationAdmin
        [AuthorizationRequired]
        public ActionResult Program()
        {
            ViewBag.Environment = Functions.GetEnvironment();
            return View("Program", lcapasLogic.GetApplicationPrograms(false, false));
        }

        // POST: ApplicationAdmin/Create
        [HttpPost]
		[AuthorizationRequired]
        public ActionResult ProgramCreate(string itemCode, string itemDesc, bool active)
        {
            UserResultObj _UserResultModel = new UserResultObj();

            try
            {
                _UserResultModel = lcapasLogic.CreateProgram(itemCode, itemDesc, active);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ProgramCreate", "Error: ", ex.ToString());
            }
            return Json(_UserResultModel);
        }

        // POST: Activate/Deactivate Application
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult ProgramActivate(int id, bool active)
        {
            bool success = false;
            try
            {          
                success = lcapasLogic.SetApplicationProgramActiveStatus(id, active);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ProgramActivate", "Error: ", ex.ToString());
            }
            return Json(success);
        }

        // POST: ApplicationAdmin/Delete/5
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult ProgramDelete(int id)
        {
            UserResultObj _UserResultModel = new UserResultObj();

            try
            {              
                _UserResultModel = lcapasLogic.DeleteApplicationProgram(id);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ProgramDelete", "Error: ", ex.ToString());
            }

            return Json(_UserResultModel);
        }

        // POST: ApplicationAdmin/Order
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult ProgramOrder(List<int> itemOrder)
        {
            bool success = false;
            try
            {          
                success = lcapasLogic.SaveProgramOrder(itemOrder);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ProgramOrder", "Error: ", ex.ToString());
            }
            return Json(success);
        }

        // POST: Activate/Deactivate Application
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult ProgramPending(int id, bool pending)
        {
            bool success = false;
            try
            {
                success = lcapasLogic.SetApplicationProgramPendingStatus(id, pending);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ProgramPending", "Error: ", ex.ToString());
            }
            return Json(success);
        }

        #endregion

        #region Campuses

        // GET: ProgramCampus
        [AuthorizationRequired]
        public ActionResult Campus()
        {
            List<ProgramCampus> _ProgramCampuses = new List<ProgramCampus>();

            try
            {
                _ProgramCampuses = lcapasLogic.GetCampuses(false);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "Campus", "Error: ", ex.ToString());
            }

            ViewBag.Environment = Functions.GetEnvironment();

            return View("Campus", _ProgramCampuses);
        }

        // POST: ProgramCampus/Create
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult CampusCreate(string itemCode, string itemDesc, bool active)
        {
            UserResultObj _UserResultModel = new UserResultObj();

            try
            {
                if (ModelState.IsValid)
                {
                    _UserResultModel = lcapasLogic.CreateCampus(itemCode, itemDesc, active);
                }
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                {
                    _UserResultModel.Message = "Cannot add this campus! \n It already exists!";
                    
                }
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "CampusCreate", "Error: ", ex.ToString());
            }
            return Json(_UserResultModel);
        }

        // POST: Activate/Deactivate Campus
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult CampusActivate(int id, bool active)
        {
            bool success = false;
            try
            {
                success = lcapasLogic.ActivateCampus(id, active);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "CampusActivate", "Error: ", ex.ToString());
            }
            return Json(success);
        }

        // POST: ProgramCampus/Delete/5
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult CampusDelete(int id)
        {
            UserResultObj _UserResultModel = new UserResultObj();
            _UserResultModel.Success = false;
            try
            {
                _UserResultModel = lcapasLogic.DeleteCampus(id);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "CampusDelete", "Error: ", ex.ToString());
            }
            return Json(_UserResultModel);
        }

        // POST: ProgramCampus/Order
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult CampusOrder(List<int> itemOrder)
        {
            bool success = false;
            try
            {
                success = lcapasLogic.SaveCampusOrder(itemOrder);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "CampusOrder", "Error: ", ex.ToString());
            }
            return Json(success);
        }

        #endregion

        #region Terms

        // GET: ProgramTerms
        [AuthorizationRequired]
        public ActionResult Term()
        {
            List<ProgramTerm> _ProgramTerms = new List<ProgramTerm>();
            try
            {
                _ProgramTerms = lcapasLogic.GetTerms(false);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "Term", "Error: ", ex.ToString());
            }

            ViewBag.Environment = Functions.GetEnvironment();

            return View("Term", _ProgramTerms);
        }

        // POST: ProgramTerms/Create
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult TermCreate(string itemCode, string itemDesc, bool active)
        {
            UserResultObj _UserResultModel = new UserResultObj();

            try
            {
                if (ModelState.IsValid)
                {
                    _UserResultModel = lcapasLogic.CreateTerm(itemCode, itemDesc, active);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "TermCreate", "Error: ", ex.ToString());
            }
            return Json(_UserResultModel);
        }

        // POST: Activate/Deactivate Term
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult TermActivate(int id, bool active)
        {
            bool success = false;
            try
            {
                lcapasLogic.ActivateTerm(id, active);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "TermActivate", "Error: ", ex.ToString());
            }
            return Json(success);
        }

        // POST: ProgramTerms/Delete/5
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult TermDelete(int id)
        {
            UserResultObj _UserResultModel = new UserResultObj();
            _UserResultModel.Success = false;
            try
            {
                _UserResultModel = lcapasLogic.DeleteTerm(id);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "TermDelete", "Error: ", ex.ToString());
            }
            return Json(_UserResultModel);
        }

        // POST: ProgramTerms/Order
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult TermOrder(List<int> itemOrder)
        {
            bool success = false;
            try
            {
                success = lcapasLogic.OrderTerm(itemOrder);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "TermOrder", "Error: ", ex.ToString());
            }
            return Json(success);
        }

        #endregion

        #region Program Details

        // GET: ProgramDetails
        [AuthorizationRequired]
        public ActionResult ProgramDetail()
        {
            try
            {
                ViewBag.Programs = lcapasLogic.GetApplicationPrograms(true, false).Select(p => new { ApplicationProgramId = p.ApplicationProgramId, Description = string.Format("{0} ({1})", p.ProgramDesc, p.ProgramCode) });
                ViewBag.Campuses = lcapasLogic.GetCampuses();
                ViewBag.Terms = lcapasLogic.GetTerms();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ProgramDetail", "Error: ", ex.ToString());
            }

            ViewBag.Environment = Functions.GetEnvironment();

            return View("ProgramDetail");
        }

        // GET: List of ProgramDetails
        [AllowAnonymous]
        public ActionResult GetProgramDetails(int programId)
        {
            List<ProgTermCampObj> _ProgTermCampObj = new List<ProgTermCampObj>();
            try
            {
                _ProgTermCampObj = lcapasLogic.GetProgramDetails(programId);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "GetProgramDetails", "Error: ", ex.ToString());
            }
            return Json(_ProgTermCampObj);
        }

        // POST: ProgramDetails/Create
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult SaveProgramDetails(int[] programListId, int campusId, int termId, bool active = true)
        {
            bool success = false;
            
            try
            {
                foreach (int programId in programListId)
                {
                    success = lcapasLogic.SaveProgramDetails(programId, campusId, termId, active);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "SaveProgramDetails", "Error: ", ex.ToString());
            }

            return Json(success);
        }

        // POST: ProgramDetails/Delete
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult DeleteProgramDetails(int programId, int campusId, int termId)
        {
            UserResultObj _UserResultModel = new UserResultObj();

            try
            {
                _UserResultModel = lcapasLogic.DeleteProgramDetails(programId, campusId, termId);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "DeleteProgramDetails", "Error: ", ex.ToString());
            }

            return Json(_UserResultModel);
        }

        //// POST: ProgramDetails/Delete
        //[HttpPost]
        //[AuthorizationRequired]
        //public ActionResult ActivateProgramDetails(int programId, int campusId, int termId)
        //{
        //    UserResultObj _UserResultModel = new UserResultObj();

        //    try
        //    {
        //        _UserResultModel = lcapasLogic.ActivateProgramDetails(programId, campusId, termId);
        //    }
        //    catch (Exception ex)
        //    {
        //        lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ActivateProgramDetails", "Error: ", ex.ToString());
        //    }

        //    return Json(_UserResultModel);
        //}

        // POST: Activate/Deactivate ProgramDetail
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult ProgramDetailActivate(int[] programListId, int campusId, int termId, bool active)
        {
            bool success = false;
            try
            {
                foreach (int programId in programListId)
                {
                    success = lcapasLogic.ActivateProgramDetails(programId, campusId, termId, active);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ProgramDetailActivate", "Error: ", ex.ToString());
            }
            return Json(success);
        }

        // POST: ProgramDetails search
        //[HttpPost]
        //[AuthorizationRequired]
        //public ActionResult ProgramDetailSearch( int CampusID, int TermID)
        //{
        //    bool success = false;

        //    try
        //    {
        //        ProgramDetail PD = lcapasLogic.GetProgramDetailsByCampusTerm(CampusID, TermID);
                

        //        if (PD != null) success = true;


        //    }
        //    catch (Exception ex)
        //    {
        //        lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ProgramDetailSearch", "Error: ", ex.ToString());
        //    }

        //    return Json(success);
        //}

        // POST: ProgramDetails/Order
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult ProgramDetailOrder(int programId, int campusId, List<int> termListOrder)
        {
            bool success = false;

            try
            {
                success = lcapasLogic.OrderProgramDetails(programId, campusId, termListOrder);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ProgramDetailOrder", "Error: ", ex.ToString());
            }

            return Json(success);
        }

        #endregion

        #region Reference Types

        // GET: ReferenceTypes
        [AuthorizationRequired]
        public ActionResult ReferenceType()
        {
            List<ReferenceType> _ReferenceTypes = new List<ReferenceType>();
            try
            {
                _ReferenceTypes = lcapasLogic.GetReferenceTypes();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ReferenceType", "Error: ", ex.ToString());
            }

            ViewBag.Environment = Functions.GetEnvironment();

            return View("ReferenceType", _ReferenceTypes);
        }

        // POST: ReferenceTypes/Create
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult ReferenceTypeCreate(string itemCode, string itemDesc, bool active)
        {
            UserResultObj _UserResultModel = new UserResultObj();

            try
            {
                _UserResultModel = lcapasLogic.CreateReferenceType(itemCode, itemDesc);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ReferenceTypeCreate", "Error: ", ex.ToString());
            }

            return Json(_UserResultModel);
        }

        // POST: ReferenceTypes/Delete/5
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult ReferenceTypeDelete(int id)
        {
            UserResultObj _UserResultModel = new UserResultObj();

            try
            {
                _UserResultModel = lcapasLogic.DeleteReferenceType(id);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ReferenceTypeDelete", "Error: ", ex.ToString());
            }

            return Json(_UserResultModel);
        }

        // POST: ReferenceTypes/Order
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult ReferenceTypeOrder(List<int> itemOrder)
        {
            bool success = false;
            try
            {
                success = lcapasLogic.OrderReferenceType(itemOrder);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ReferenceTypeOrder", "Error: ", ex.ToString());
            }
            return Json(success);
        }

        #endregion

        #region Application Fees

        // GET: ApplicationFee
        [AuthorizationRequired]
        public ActionResult ApplicationFee()
        {
            List<ApplicationFee> _ApplicationFees = new List<ApplicationFee>();
            try
            {
                _ApplicationFees = lcapasLogic.GetApplicationFees();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ApplicationFee", "Error: ", ex.ToString());
            }

            ViewBag.Environment = Functions.GetEnvironment();

            return View("ApplicationFee", _ApplicationFees);
        }

        // POST: ApplicationFee/Create
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult ApplicationFeeCreate(string itemCode, string itemDesc, string itemMessage)
        {
            UserResultObj _UserResultModel = new UserResultObj();

            try
            {
                if (!string.IsNullOrWhiteSpace(itemCode) && !string.IsNullOrWhiteSpace(itemDesc))
                {
                    _UserResultModel = lcapasLogic.CreateApplicationFee(itemDesc, itemCode, itemMessage);
                } else
                {
                    _UserResultModel.Success = false;
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorCreatingItem;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ApplicationFeeCreate", "Error: ", ex.ToString());
            }

            return Json(_UserResultModel);
        }

        // POST: ApplicationFee/Delete
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult ApplicationFeeDelete(int id)
        {
            UserResultObj _UserResultModel = new UserResultObj();

            try
            {
                _UserResultModel = lcapasLogic.DeleteApplicationFee(id);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "ApplicationFeeDelete", "Error: ", ex.ToString());
            }

            return Json(_UserResultModel);
        }

        #endregion

        #region Settings

        [AuthorizationRequired]
        public ActionResult Settings()
        {

            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        // GET: Settings
        [AuthorizationRequired]
        public ActionResult Parameters()
        {
            List<Setting> _Settings = new List<Setting>();
            try
            {
                _Settings = lcapasLogic.GetSettings();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "Parameters", "Error: ", ex.ToString());
            }

            ViewBag.Environment = Functions.GetEnvironment();

            return View("Parameters", _Settings);
        }

        // POST: Settings/Order
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult SettingOrder(List<int> itemOrder)
        {
            bool success = false;
            try
            {
                success = lcapasLogic.OrderSettings(itemOrder);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "SettingOrder", "Error: ", ex.ToString());
            }
            return Json(success);
        }

        // POST: SettingCategory/Order
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult SettingCategoryOrder(List<int> itemOrder)
        {
            bool success = false;
            try
            {
                success = lcapasLogic.OrderSettingCategories(itemOrder);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "SettingCategoryOrder", "Error: ", ex.ToString());
            }
            return Json(success);
        }

        #endregion

        [AllowAnonymous]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                lcapasLogic.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: CreateProgramTerm
        [AuthorizationRequired]
        public ActionResult CreateProgramTerm(ApplicationProgramViewObj _ApplicationProgramViewObj)
        {
            ApplicationProgramViewObj _ApplicationProgram = _ApplicationProgramViewObj ?? new ApplicationProgramViewObj();
            try
            {
                _ApplicationProgram.Load();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "CreateProgramTerm", "Error: ", ex.ToString());
            }

            return View("CreateProgramTerm", _ApplicationProgram);
        }

        // GET: ActivateProgram
        [AuthorizationRequired]
        public ActionResult DeactivateProgramTerm(ProgramDetailViewObj _ProgramDetailViewObj)
        {
            ProgramDetailViewObj _ProgramDetails = _ProgramDetailViewObj ?? new ProgramDetailViewObj();
            try
            {
                _ProgramDetails.Load();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "DeactivateProgramTerm", "Error: ", ex.ToString());
            }

            return View("DeactivateProgramTerm", _ProgramDetails);
        }


        // POST: AddProgramDetails
        [HttpPost]
        [AuthorizationRequired]
        public ActionResult AddProgramTermController(int[] ProgramListID, int CampusId, int TermId)
        {
            bool success = false;
            try
            {
                foreach (int programId in ProgramListID)
                {
                    ProgramDetail PD = lcapasLogic.AddProgramDetails(false, DateTime.Now, programId, CampusId, TermId);

                    if (PD != null) success = true;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdminController, "AddProgramTermController", "Error: ", ex.ToString());
            }

            return Json(success);
        }
    }
}