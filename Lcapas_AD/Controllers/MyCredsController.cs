using Lcapas.Core.Library;
using Lcapas.Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lcapas.AD.Controllers
{
    public class MyCredsController : Controller
    {
        private LcapasLogic lcapasLogic = new LcapasLogic();

        // GET: MeCreds Home Page
        [AuthorizationRequired]
        public ActionResult Index()
        {
            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        [AuthorizationRequired]
        public ActionResult GenerateTranscriptBatch(TRRQRequestListViewObj requests)
        {
            TRRQRequestListViewObj _Requests = requests ?? new TRRQRequestListViewObj();

            try
            {
                _Requests.LoadObject();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.MyCredsController, "GenerateTranscriptBatch", "Error", ex.ToString());
            }

            ViewBag.Title = "Generate Transcript Batch (MyCreds)";
            ViewBag.Environment = Functions.GetEnvironment();

            return View("GenerateTranscriptBatch", _Requests);
        }

        [AuthorizationRequired]
        public JsonResult ExportTranscriptBatch(string[] requestIdList, bool useMyCredsXsl = false, bool allSelected = false, string filterFields = null, bool uploadMyCredsAPI = false)
        {
            UserResultObj userResultObj = new UserResultObj()
            {
                Success = false,
                Message = Structs.Literals.ContactHelpDesk,
            };

            Dictionary<string, List<string>> studentIdList = new Dictionary<string, List<string>>();

            try
            {
                if (requestIdList != null && requestIdList.Length > 0)
                {
                    // Retrieve Student IDs (sNumbers) from Acad Cred ID list
                    using (ColleagueLogic collLogic = new ColleagueLogic())
                    {
                        studentIdList = collLogic.GetColleagueSNumberByRequestIDs(requestIdList.ToList(), allSelected, filterFields);
                    };

                    if (studentIdList != null && studentIdList.Any())
                    {
                        // Export Files by Student IDs
                        lcapasLogic.ExportTranscriptsToFilesByStudentIDs(studentIdList, ref userResultObj, useMyCredsXsl, uploadMyCredsAPI);
                    }
                    else
                    {
                        userResultObj.Message = Structs.Export.EmptyRequestSelection;
                    }
                }
                else
                {
                    userResultObj.Message = Structs.Export.EmptyRequestSelection;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.MyCredsController, "ExportTranscriptBatch", "Error", "requestIdList: " + requestIdList.ToString() + ", " + ex.ToString());
            }

            return Json(userResultObj);
        }

        [AuthorizationRequired]
        public ActionResult BulkSend(BulkSendListViewObj students)
        {
            BulkSendListViewObj _Students = students ?? new BulkSendListViewObj();

            try
            {
                _Students.LoadObject();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.MyCredsController, "BulkSend", "Error", ex.ToString());
            }

            ViewBag.Title = "Bulk Send (MyCreds)";
            ViewBag.Environment = Functions.GetEnvironment();

            return View("BulkSend", _Students);
        }

        [AuthorizationRequired]
        public JsonResult ExportBulkSendBatch(string[] requestIdList, bool useMyCredsXsl = false, bool allSelected = false, string filterFields = null, bool uploadMyCredsAPI = false)
        {
            UserResultObj userResultObj = new UserResultObj()
            {
                Success = false,
                Message = Structs.Literals.ContactHelpDesk,
            };

            Dictionary<string, List<string>> studentIdList = new Dictionary<string, List<string>>();

            try
            {
                if (requestIdList != null && requestIdList.Length > 0)
                {
                    // Retrieve Student IDs (sNumbers) from Acad Cred ID list
                    using (ColleagueLogic collLogic = new ColleagueLogic())
                    {
                        collLogic.GetColleagueSNumberByAcadCredIDs(requestIdList.ToList(), allSelected, filterFields).ForEach(x => studentIdList.Add(key: x, value: null));
                    };

                    if (studentIdList != null && studentIdList.Any())
                    {
                        // Export Files by Student IDs
                        lcapasLogic.ExportTranscriptsToFilesByStudentIDs(studentIdList, ref userResultObj, useMyCredsXsl, uploadMyCredsAPI);
                    }
                    else
                    {
                        userResultObj.Message = Structs.Export.EmptyStudentSelection;
                    }
                }
                else
                {
                    userResultObj.Message = Structs.Export.EmptyStudentSelection;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.MyCredsController, "ExportBulkSendBatch", "Error", "requestIdList: " + requestIdList.ToString() + ", " + ex.ToString());
            }

            return Json(userResultObj);
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