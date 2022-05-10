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
    public class TransferCreditsController : Controller
    {
        private LcapasLogic lcapasLogic = new LcapasLogic();


        #region TransferCredit

        [AuthorizationRequired]
        public ActionResult Index()
        {
            ViewBag.Environment = Functions.GetEnvironment();
            return View();
        }


        // GET: Generate Transfer Credit XML File
        [AuthorizationRequired]
        public ActionResult GenerateTransferCreditXML(TransferCreditViewObj _TransferCreditViewObj)
        {
            TransferCreditViewObj _TransferCredit = _TransferCreditViewObj ?? new TransferCreditViewObj();
            try
            {
                _TransferCredit.Load();
            }
            catch (Exception ex)
            {
                //TO Do take a look at SettingReport
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TransferCredit, "GenerateTransferCreditXML", "Error: ", ex.ToString());
            }

            return View("GenerateTransferCreditXML", _TransferCredit);
        }

        [AuthorizationRequired]
        public ActionResult XMLReport(string reportId, string reportType, bool allSelected = false, string filterFields = null)
        {
            byte[] file = null;
            string fileName = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(reportId) && !string.IsNullOrWhiteSpace(reportType))
                {
                    fileName = reportType + " - " + DateTime.Now.ToString("dd MMM yyyy HH:mm:ss") + ".xml";

                    file = Functions.GetReportExcelDocument(reportId, reportType, allSelected, filterFields);
                }

                var stream = new MemoryStream(file);
                return File(stream, "text/xml", fileName);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "XMLReport", "Error: ", ex.ToString());

                var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes("Report failed to generate: " + ex));
                return File(stream, "text/plain", "Failed.txt");
            }
        }

        #endregion
    }
}

