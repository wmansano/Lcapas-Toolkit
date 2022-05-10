using Lcapas.Core.Library;
using Lcapas.Core.Logic;

using Lcapas.Core.Models.Lcappsdb;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lcapas.AD.Controllers
{
    public class ReportsController : Controller
    {
        private LcapasLogic lcapasLogic = new LcapasLogic();


        #region Users

        [AuthorizationRequired]
        public ActionResult Index()
        {
            ViewBag.Environment = Functions.GetEnvironment();
            return View();
        }


        // GET: Reports
        [AuthorizationRequired]
        public ActionResult DailyRequest(DailyRequestReportViewObj _dailyRequestReportviewobject)
        {
            DailyRequestReportViewObj _DailyRequestReports = _dailyRequestReportviewobject ?? new DailyRequestReportViewObj();
            try
            {
                _DailyRequestReports.Load();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "DailyRequest", "Error: ", ex.ToString());
            }

            return View("DailyRequestReport", _DailyRequestReports);
        }

        // GET: Login History Reports
        [AuthorizationRequired]
        public ActionResult LoginHistory(LoginHistoryReportViewObj _loginHistoryReportViewObject)
        {
            LoginHistoryReportViewObj _LoginHistoryReports = _loginHistoryReportViewObject ?? new LoginHistoryReportViewObj();
            try
            {
                _LoginHistoryReports.Load();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "LoginHistory", "Error: ", ex.ToString());
            }

            return View("LoginHistory", _LoginHistoryReports);
        }


        // GET: Exception Error Reports
        [AuthorizationRequired]
        public ActionResult ExceptionError(ExceptionErrorsReportViewObj _ExceptionErrorsReportViewObj)
        {
            ExceptionErrorsReportViewObj _ExceptionErrors = _ExceptionErrorsReportViewObj ?? new ExceptionErrorsReportViewObj();
            try
            {
                _ExceptionErrors.Load();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "ExceptionError", "Error: ", ex.ToString());
            }

            return View("ExceptionError", _ExceptionErrors);
        }

        // GET: Application Reports
        [AuthorizationRequired]
        public ActionResult ApplicationReport(ApplicationReportViewObj _ApplicationReportViewObj)
        {
            ApplicationReportViewObj _ApplicationReport = _ApplicationReportViewObj ?? new ApplicationReportViewObj();
            try
            {
                _ApplicationReport.Load();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "ApplicationReport", "Error: ", ex.ToString());
            }

            return View("ApplicationReport", _ApplicationReport);
        }

        // GET: Payment Reports
        [AuthorizationRequired]
        public ActionResult PaymentReport(PaymentReportViewObj _PaymentReportViewObj)
        {
            PaymentReportViewObj _PaymentReport = _PaymentReportViewObj ?? new PaymentReportViewObj();
            try
            {
                _PaymentReport.Load();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "PaymentReport", "Error: ", ex.ToString());
            }

            return View("PaymentReport", _PaymentReport);
        }

        // GET: Sent Email Reports
        [AuthorizationRequired]
        public ActionResult SentEmailReport(SentEmailReportViewObj _SentEmailReportViewObject)
        {
            SentEmailReportViewObj _SentEmailReports = _SentEmailReportViewObject ?? new SentEmailReportViewObj();
            try
            {
                _SentEmailReports.Load();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "SentEmailReport", "Error: ", ex.ToString());
            }

            return View("SentEmailReport", _SentEmailReports);
        }

        // GET: HoldType Reports
        [AuthorizationRequired]
        public ActionResult HoldTypeReport(HoldTypeReportViewObj _HoldTypeReportViewObj, string destAction = null)
        {
            HoldTypeReportViewObj _HoldTypeReport = _HoldTypeReportViewObj ?? new HoldTypeReportViewObj();
            try
            {
                if (!string.IsNullOrWhiteSpace(destAction) && destAction.Trim().ToUpper() == Structs.DestActions.AdmissionsOutbox.Trim().ToUpper())
                {
                    _HoldTypeReport.DestAction = Structs.DestActions.AdmissionsOutbox;
                }
                else
                {
                    _HoldTypeReport.DestAction = Structs.DestActions.RecordsInbox;
                }
                _HoldTypeReport.Load();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "HoldTypeReport", "Error: ", ex.ToString());
            }

            return View("HoldTypeReport", _HoldTypeReport);
        }

        // GET: CollProgramApplication Reports
        [AuthorizationRequired]
        public ActionResult CollProgramApplicationReport(CollProgramApplicationReportViewObj _CollProgramApplicationReportViewObj)
        {
            CollProgramApplicationReportViewObj _CollProgramApplicationReport = _CollProgramApplicationReportViewObj ?? new CollProgramApplicationReportViewObj();
            try
            {
                _CollProgramApplicationReport.Load();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "CollProgramApplicationReport", "Error: ", ex.ToString());
            }

            return View("CollProgramApplicationReport", _CollProgramApplicationReport);
        }

        // GET: WaitListReports
        [AuthorizationRequired]
        public ActionResult CollWaitListReport(CollWaitListReportViewObj _CollWaitListReportViewObj)
        {
            CollWaitListReportViewObj _CollWaitListReport = _CollWaitListReportViewObj ?? new CollWaitListReportViewObj();
            try
            {
                _CollWaitListReport.Load();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "CollWaitListReport", "Error: ", ex.ToString());
            }

            return View("CollWaitListReport", _CollWaitListReport);
        }

        // GET: CollAdmissionConditionsReport
        [AuthorizationRequired]
        public ActionResult CollAdmissionConditionsReport(CollAdmissionConditionsReportViewObj _CollAdmissionConditionsReportViewObj)
        {
            CollAdmissionConditionsReportViewObj _CollAdmissionConditionsReport = _CollAdmissionConditionsReportViewObj ?? new CollAdmissionConditionsReportViewObj();
            try
            {
                _CollAdmissionConditionsReport.Load();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "CollAdmissionConditionsReport", "Error: ", ex.ToString());
            }

            return View("CollAdmissionConditionsReport", _CollAdmissionConditionsReport);
        }

        // GET: CollOverduesReport
        [AuthorizationRequired]
        public ActionResult CollOverduesReport(CollOverduesReportViewObj _CollOverduesReportViewObj)
        {
            CollOverduesReportViewObj _CollOverduesReport = _CollOverduesReportViewObj ?? new CollOverduesReportViewObj();
            try
            {
                _CollOverduesReport.Load();

            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "CollOverduesReport", "Error: ", ex.ToString());
            }

            return View("CollOverduesReport", _CollOverduesReport);
        }

        [AuthorizationRequired]
        public ActionResult CollTestingResultsReport(CollTestingResultsReportViewObj _CollTestingResultsReportViewObj)
        {
            CollTestingResultsReportViewObj _CollTestingResultsReport = _CollTestingResultsReportViewObj ?? new CollTestingResultsReportViewObj();
            try
            {
                _CollTestingResultsReport.Load();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "CollTestingResultsReport", "Error: ", ex.ToString());
            }

            return View("CollTestingResultsReport", _CollTestingResultsReport);
        }

        // GET: CollMissingGradeReport
        [AuthorizationRequired]
        public ActionResult CollMissingGradeReport(CollMissingGradeReportViewObj _CollMissingGradeReportViewObj)
        {
            CollMissingGradeReportViewObj _CollMissingGradeReport = _CollMissingGradeReportViewObj ?? new CollMissingGradeReportViewObj();
            try
            {
                _CollMissingGradeReport.Load();

            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "CollMissingGradeReport", "Error: ", ex.ToString());
            }

            return View("CollMissingGradeReport", _CollMissingGradeReport);
        }

        [AuthorizationRequired]
        public ActionResult ExportReports(string reportId, string reportType, bool allSelected = false, string filterFields = null, string destAction = null)
        {
            byte[] file = null;
            string fileName = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(reportId) && !string.IsNullOrWhiteSpace(reportType))
                {
                    fileName = reportType + " - " + DateTime.Now.ToString("dd MMM yyyy HH:mm:ss") + ".csv";

                    file = Functions.GetReportExcelDocument(reportId, reportType, allSelected, filterFields, destAction);
                }

                var stream = new MemoryStream(file);
                return File(stream, "text/excel", fileName);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "ExportReports", "Error: ", ex.ToString());

                var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes("Report failed to generate: " + ex));
                return File(stream, "text/plain", "Failed.txt");
            }
        }

      
        [AuthorizationRequired]
        public JsonResult PrintReports(string[] reportId, string docType, bool allSelected = false, string filterFields = null, string destAction = null)
        {
            UserResultObj ReportModel = new UserResultObj()
            {
                Success = false,
                Message = Structs.Literals.ContactHelpDesk
            };

            try
            {
                ReportModel.ItemId = lcapasLogic.SaveKeyValueTempCache(reportId);
                if (ReportModel.ItemId != null)
                {
                    ReportModel.Message = Functions.GetReportHtmlDocument(ReportModel.ItemId, docType, allSelected, filterFields, destAction).ToHtmlString();
                    ReportModel.Success = true;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "PrintReports", "Error", ex.ToString());
            }

            return Json(ReportModel);
        }

        [AuthorizationRequired]
        public JsonResult PrintQuery(string docType)
        {
            UserResultObj ReportModel = new UserResultObj()
            {
                Success = false,
                Message = Structs.Literals.ContactHelpDesk
            };

            ReportModel.Message = Functions.GetQueryHtmlDocument(docType).ToHtmlString();
            ReportModel.Success = true;
        

            return Json(ReportModel);
        }

        #endregion
    }
}

