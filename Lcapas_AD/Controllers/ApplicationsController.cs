using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Xsl;
using System.Web.Services;
using Lcapas.Core.Library;
using Lcapas.Core.Logic;
using Lcapas.Core.Models.Lcappsdb;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;
using static Lcapas.AD.FilterConfig;

namespace Lcapas.AD.Controllers
{
    [WebService(Namespace = "http://lcapps.lethbridgecollege.ca/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class ApplicationsController : Controller
    {
        private LcapasLogic lcapasLogic = new LcapasLogic();

        [AuthorizationRequired]
        public ActionResult Index() {
            ViewBag.Environment = Functions.GetEnvironment();
            return View();
        }

        [AuthorizationRequired]
        public ActionResult Applications(AdmissionsApplicationObj admissionsApplication)
        {
            AdmissionsApplicationObj _AdmissionsApplication = admissionsApplication ?? new AdmissionsApplicationObj();

            try
            {
                _AdmissionsApplication.Load();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdmissionsApplication, "Applications", "Error", ex.ToString());
            }

            ViewBag.Environment = Functions.GetEnvironment();

            return View(_AdmissionsApplication);
        }

        [AuthorizationRequired]
        public JsonResult PrintApplicationReport(string[] values)
        {
            UserResultObj userResultModel = new UserResultObj()
            {
                Success = false,
                Message = Structs.Literals.ContactHelpDesk
            };

            try
            {
                userResultModel.ItemId = lcapasLogic.SaveKeyValueTempCache(values);
                if (userResultModel.ItemId != null)
                {
                    userResultModel.Message = Functions.GetHtmlDocument(userResultModel.ItemId, true, Core.Library.Apas.CoreMain.DocumentTypeCodeType.Application).ToHtmlString();
                    userResultModel.Success = true;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdmissionsApplication, "PrintApplicationReport", "Error", ex.ToString());
            }

            return Json(userResultModel);
        }

        [AuthorizationRequired]
        public JsonResult ExportApplications(string[] values)
        {
            UserResultObj userResultModel = new UserResultObj()
            {
                Success = false,
                Message = Structs.Literals.ContactHelpDesk
            };

            try
            {
                if (values == null)
                {
                    userResultModel.Message = Structs.Export.EmptyDataMessage;
                }
                else
                {
                    userResultModel.Success = lcapasLogic.LoadApplications(values.ToList()).Export();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdmissionsApplication, "ExportApplications", "Error", ex.ToString());
            }
            return Json(userResultModel);
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



        //[AuthorizationRequired]
        //public JsonResult PrepareApplicationReport(string[] values)
        //{
        //    UserResultObj userResultModel = new UserResultObj()
        //    {
        //        Success = false,
        //        Message = Structs.Literals.ContactHelpDesk
        //    };

        //    try
        //    {
        //        userResultModel.ItemId = lcapasLogic.SaveKeyValueTempCache(values);
        //        if (userResultModel.ItemId != null) {
        //            userResultModel.Success = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdmissionsApplication, "PrepareApplicationReport", "Error", ex.ToString());
        //    }

        //    return Json(userResultModel);
        //}

        //[AuthorizationRequired]
        //public ActionResult HtmlApplicationReport(string uuid, bool multiple = false)
        //{
        //    HtmlString HtmlDocument = null;
        //    string ids = uuid;

        //    try
        //    {
        //        HtmlDocument = Functions.GetHtmlDocument(uuid, multiple, Core.Library.Apas.CoreMain.DocumentTypeCodeType.Application);
        //    }
        //    catch (Exception ex)
        //    {
        //        lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdmissionsApplication, "HtmlApplicationReport", "Error", ex.ToString());
        //    }

        //    return View("../Shared/DisplayMessage", HtmlDocument);
        //}


        
        
        
        //[AuthorizationRequired]
        //public JsonResult PrepareWordReport(Ids data)
        //{
        //    XmlDocument doc = lcapasLogic.GetApplicationXml(data.values);

        //    Session[Structs.WordReport.SessionWord] = doc;

        //    UserResultModel userResultModel = new UserResultModel();
        //    userResultModel.Message = Structs.Literals.Success;

        //    return Json(userResultModel);
        //}

        //[AuthorizationRequired]
        //public void DownloadWordReport()
        //{
        //    XslCompiledTransform _transform;
        //    string _xslPath = Server.MapPath(Structs.WordReport.MapPath);
        //    string _xmlPath = "AdmissionApplications_" + DateTime.Now.ToString("MM-dd-yyyy_HH:mm:ss") + ".doc";

        //    XmlDocument _doc = (XmlDocument)Session[Structs.WordReport.SessionWord];
        //    HttpResponseBase response = this.ControllerContext.HttpContext.Response;

        //    response.Clear();
        //    response.ClearContent();
        //    response.ClearHeaders();
        //    response.Cookies.Clear();
        //    response.ContentType = Structs.WordReport.ContentType;
        //    response.AddHeader(Structs.WordReport.ContentHeader, "attachment;filename=" + _xmlPath);

        //    _transform = new XslCompiledTransform();

        //    try
        //    {
        //        _transform.Load(_xslPath);
        //        _transform.Transform(_doc, null, response.OutputStream);

        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.ToString();
        //    }

        //    response.Flush();
        //    response.End();
        //}

        //public JsonResult PrepareExcelReport(Ids data)
        //{
        //    XmlDocument doc = lcapasLogic.GetApplicationXml(data.values);

        //    Session[Structs.ExcelReport.SessionExcel] = doc;

        //    UserResultModel userResultModel = new UserResultModel();
        //    userResultModel.Message = Structs.Literals.Success;

        //    return Json(userResultModel);
        //}

        //[AuthorizationRequired]
        //public void DownloadExcelReport()
        //{
        //    XslCompiledTransform _transform;
        //    // need to move _xslPath to database settings
        //    string _xslPath = Server.MapPath(Structs.ExcelReport.MapPath);
        //    string _xmlPath = "AdmissionApplicantList_" + DateTime.Now.ToString("MM-dd-yyyy_HH:mm:ss") + ".xls";

        //    XmlDocument _doc = (XmlDocument)Session[Structs.ExcelReport.SessionExcel];
        //    HttpResponseBase response = this.ControllerContext.HttpContext.Response;

        //    response.Clear();
        //    response.ClearContent();
        //    response.ClearHeaders();
        //    response.Cookies.Clear();
        //    response.ContentType = Structs.ExcelReport.ContentType;
        //    response.AddHeader(Structs.ExcelReport.ContentHeader, "attachment;filename=" + _xmlPath);

        //    _transform = new XslCompiledTransform();

        //    try
        //    {
        //        _transform.Load(_xslPath);
        //        _transform.Transform(_doc, null, response.OutputStream);

        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.ToString();
        //    }

        //    response.Flush();
        //    response.End();
        //}
    }
}
