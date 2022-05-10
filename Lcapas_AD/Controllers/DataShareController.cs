using Lcapas.Core.Library;
using Lcapas.Core.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace Lcapas.AD.Controllers
{
    [WebService(Namespace = "http://lcapps.lethbridgecollege.ca/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class DataShareController : Controller
    {
        private LcapasLogic lcapasLogic = new LcapasLogic();

        [AuthorizationRequired]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizationRequired]
        public ActionResult Datashare(AdmissionsApplicationObj admissionsApplication)
        {
            AdmissionsApplicationObj _AdmissionsApplication = admissionsApplication ?? new AdmissionsApplicationObj();

            try
            {
                _AdmissionsApplication.Datashare = true;
                _AdmissionsApplication.Load();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.DatashareController, "Datashare", "Error", ex.ToString());
            }

            return View(_AdmissionsApplication);

            //admissionsApplication.Datashare = true;
            //admissionsApplication.Load();
            //return View(admissionsApplication);
        }

        //public ActionResult Index(string strBatch = null, string strBirth = null, string strFrom = null, string strTo = null, string lastName = null, string firstName = null, string middleName = null, string asn = null)
        //{
        //    //verify authentication (could switch to user.authentication)
        //    if (Session["log"] != null)
        //    {
        //        List<ApplicationSearchFilter> apps = new List<ApplicationSearchFilter>();

        //        // load batches by date
        //        List<string> batches = lcapasLogic.GetBatchDates();


        //        if (batches.Count > 0)
        //        {
        //            // Handle Dates
        //            DateTime? batchDate = null; DateTime? birthDate = null; DateTime? fromDate = null; DateTime? toDate = null;

        //            if (strBirth != null && strBirth != "____/__/__") { birthDate = Functions.convertDate2(strBirth); }
        //            if (strBatch != null) { batchDate = Functions.convertDate2(strBatch); }
        //            if (strFrom != null) { fromDate = Functions.convertDate2(strFrom); }
        //            if (strTo != null) { toDate = Functions.convertDate2(strTo); }

        //            ViewBag.Batches = batches;

        //            // include most recent batch if empty e.g. onload
        //            if (batchDate == null) { batchDate = DateTime.ParseExact(batches[0], "yyyy/MM/dd", CultureInfo.InvariantCulture); }

        //            apps = lcapasLogic.GetApplications(batchDate, birthDate, fromDate, toDate, lastName, firstName, middleName, asn);
        //        }
        //        return View(apps);
        //    }
        //    else
        //    {
        //        return this.RedirectToAction("Index", "Login");
        //    }

        //}

        public JsonResult ImportNursingApplications(string data)
        {
            string returnMessage, streamContents;
            XDocument webapps = new XDocument();
            UserResultObj userResultModel = new UserResultObj();

            try
            {
                // This override should not be necessary as I added the .cer to the computer trusted roots - need to investigate
                //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                //WebRequest req = WebRequest.Create(@"https://142.66.5.26/LCNursingWS/webresources/admissionApplication/201601");
                //req.Method = "GET";
                //req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("LethbridgeCollege:april10"));

                //HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

                //using (var streamReader = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
                //{
                //    streamContents = streamReader.ReadToEnd();
                //    webapps = XDocument.Parse(streamContents.Replace("&", ""));
                //}
                ///////////////////////////////////////////////

                // Pull sample from file
                ///////////////////////////////////////////////
                string path = Server.MapPath("~/SampleUofLXml.xml");
                using (var streamReader = new StreamReader(path, Encoding.UTF8))
                {
                    streamContents = streamReader.ReadToEnd();
                    webapps = XDocument.Parse(streamContents.Replace("&", ""));
                }

                // Save webapps to db
                returnMessage = lcapasLogic.SaveApplications(webapps);
            }
            catch (Exception ex)
            {
                returnMessage = "An error has occured: " + ex.ToString().Substring(0, 250) + "...";
            }

            userResultModel.Message = returnMessage;

            return Json(userResultModel);
        }

        public JsonResult PrepareDataShareWordApps(List<string> data)
        {
            XmlDocument doc = lcapasLogic.GetNursingApplications(data);

            Session["SessionDatashareApps"] = doc;

            UserResultObj userResultModel = new UserResultObj();
            userResultModel.Message = "Success";

            return Json(userResultModel);
        }

        public void DownloadDataShareWordApps()
        {
            XslCompiledTransform _transform;
            string _xslPath = Server.MapPath("~/Stylesheets/DataShare/uoflwordstyle.xslt");
            string _xmlPath = "NursingApplications_" + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss") + ".doc";

            XmlDocument _doc = (XmlDocument)Session["SessionDatashareApps"];
            HttpResponseBase response = this.ControllerContext.HttpContext.Response;

            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Cookies.Clear();
            response.ContentType = "application/x-msword";
            response.AddHeader("content-disposition", "attachment;filename=" + _xmlPath);

            _transform = new XslCompiledTransform();

            try
            {
                XsltSettings xslt_settings = new XsltSettings();
                xslt_settings.EnableScript = true;


                _transform.Load(_xslPath, xslt_settings, new XmlUrlResolver());
                _transform.Transform(_doc, null, response.OutputStream);

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }

            response.Flush();
            response.End();
        }

        public JsonResult PrepareDataShareExcelList(List<string> data)
        {
            //XmlDocument doc = Submissionsdb.GetSubmissionApplications(data.values);
            XmlDocument doc = lcapasLogic.GetNursingApplications(data);

            Session["SessionDatashareList"] = doc;

            UserResultObj userResultModel = new UserResultObj();
            userResultModel.Message = "Success";

            return Json(userResultModel);
        }

        public void DownloadDataShareExcelList()
        {
            XslCompiledTransform _transform;
            // need to move _xslPath to database settings
            string _xslPath = Server.MapPath("~/Stylesheets/DataShare/uoflexcelstyle.xslt");
            string _xmlPath = "AdmissionApplicantList_" + DateTime.Now.ToString("MM-dd-yyyy") + ".xls";

            XmlDocument _doc = (XmlDocument)Session["SessionDatashareList"];
            HttpResponseBase response = this.ControllerContext.HttpContext.Response;

            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Cookies.Clear();
            response.ContentType = "application/ms-excel";
            response.AddHeader("content-disposition", "attachment;filename=" + _xmlPath);

            _transform = new XslCompiledTransform();

            try
            {
                _transform.Load(_xslPath);
                _transform.Transform(_doc, null, response.OutputStream);

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }

            response.Flush();
            response.End();
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