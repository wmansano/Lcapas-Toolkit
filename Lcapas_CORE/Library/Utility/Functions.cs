using Lcapas.Core.Library;
using Lcapas.Core.Library.Apas.CoreMain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;
using System.Text;
using System.Net.NetworkInformation;
using System.Management;
using System.Net;

namespace Lcapas.Core.Logic
{
    public static class Functions
    {
        public static string systemUserName = "s9999999";

        public static DocumentProcessCodeType GetProcessCode() {
            DocumentProcessCodeType processCode = DocumentProcessCodeType.TEST;
            try
            {
                if (Structs.Environment.Prod.ToString().Contains(GetEnvironment()))
                {
                    processCode = DocumentProcessCodeType.PRODUCTION;
                }
                else
                {
                    processCode = DocumentProcessCodeType.TEST;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return processCode;
        }

        public static string GetEnvironment()
        {
            string url = string.Empty;
            string env = string.Empty;
            bool proddb = false;

            try
            {
                // get urlstring
                url = HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;

                // make sure the database is set to production
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    proddb = Convert.ToBoolean(lcapasLogic.GetSetting(Structs.SettingTypes.Boolean, Structs.Settings.Production));
                }
                
                // Get the environment selected in the web config
                var defaultConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["lcapasdb_entities"].ConnectionString;
                var ecsBuilder = new EntityConnectionStringBuilder(defaultConnStr);
                var sqlCsBuilder = new SqlConnectionStringBuilder(ecsBuilder.ProviderConnectionString);

                switch (sqlCsBuilder.InitialCatalog.ToString())
                {
                    case "lcapasdb_prod":
                        if (proddb) {
                            if (url.Contains(Structs.Environment.Prod) || url.Contains(Structs.Environment.Localhost)) {
                                env = Structs.Environment.Prod;
                            }
                        }
                        break;
                    case "lcapasdb_test":
                        if (!proddb) {
                            if (url.Contains(Structs.Environment.Test) || url.Contains(Structs.Environment.Localhost))
                            {
                                defaultConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["colldb_entities"].ConnectionString;
                                ecsBuilder = new EntityConnectionStringBuilder(defaultConnStr);
                                sqlCsBuilder = new SqlConnectionStringBuilder(ecsBuilder.ProviderConnectionString);
                                switch (sqlCsBuilder.InitialCatalog.ToString())
                                {
                                    case "coll18_patchsql":
                                        env = Structs.Environment.Patch;
                                        break;
                                    case "coll18_ROTestSql":
                                        env = Structs.Environment.ROTest;
                                        break;
                                    default:
                                        env = Structs.Environment.Test;
                                        break;
                                }
                            }
                        }
                        break;
                    case "lcapasdb_dev":
                        if (!proddb)
                        {
                            if (url.Contains(Structs.Environment.Dev) || url.Contains(Structs.Environment.Localhost))
                            {
                                env = Structs.Environment.Dev;
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "GetPathEnv", "Error: ", ex.ToString());
                }
            }

            return env;
        }

        public static bool IsDatabaseConnected(string connectionName)
        {
            try
            {
                // Get the connection string in the web config
                var defaultConnStr = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                var ecsBuilder = new EntityConnectionStringBuilder(defaultConnStr);
                var sqlCsBuilder = new SqlConnectionStringBuilder(ecsBuilder.ProviderConnectionString);

                using (var conn = new SqlConnection(sqlCsBuilder.ConnectionString))
                {
                    using (var cmd = new SqlCommand("SELECT 1", conn)) {
                        try
                        {
                            conn.Open();
                            cmd.ExecuteScalar();
                            return true;
                        }
                        catch (SqlException)
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "IsDatabaseConnected", "Error: ", ex.ToString());
                }
                return false;
            }
        }

        public static ServerStatusObj IsServerAvailable(string serverUrl, string serviceList)
        {
            ServerStatusObj _ServerStatus = new ServerStatusObj();

            try
            {
                // Get server status
                if (!string.IsNullOrWhiteSpace(serverUrl))
                {
                    _ServerStatus.URL = serverUrl;
                    Ping pingSender = new Ping();
                    PingReply reply = pingSender.Send(serverUrl);
                    _ServerStatus.Status = reply.Status.ToString();
                    if (reply.Status == IPStatus.Success)
                    {
                        _ServerStatus.IPAddress = reply.Address.ToString();
                        _ServerStatus.RoundTrip = reply.RoundtripTime;
                        _ServerStatus.TimeToLive = reply.Options.Ttl;
                        _ServerStatus.DontFragment = reply.Options.DontFragment;
                        _ServerStatus.BufferSize = reply.Buffer.Length;

                        // Check is services are running
                        _ServerStatus.ServiceList = IsServiceRunning(serverUrl, serviceList);
                    }
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "IsServerAvailable", "Error: ", ex.ToString());
                }
            }
            return _ServerStatus;
        }

        public static Dictionary<string, bool> IsServiceRunning(string serverName, string serviceList)
        {
            string status = string.Empty;
            //string name = string.Empty;
            Dictionary<string, bool> _ServiceStatus = new Dictionary<string, bool>();
            List<string> _ServiceList;

            try
            {
                // Get service status
                if (!string.IsNullOrWhiteSpace(serverName) && !string.IsNullOrWhiteSpace(serviceList))
                {
                    // Separate service list
                    _ServiceList = serviceList.Split(',').ToList();

                    // Connect to remote server
                    ConnectionOptions op = new ConnectionOptions();
                    using (LcapasLogic lcapasLogic = new LcapasLogic())
                    {
                        op.Username = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LcDomain) + "\\" +
                                      lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCUserName);
                        op.Password = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCPassword);
                    }
                    ManagementScope scope = new ManagementScope(String.Format(@"\\{0}\root\cimv2", serverName), op);
                    scope.Connect();

                    // Check status of each service on the remote server
                    foreach (string serviceName in _ServiceList)
                    {
                        try
                        {
                            ManagementPath path = new ManagementPath("Win32_Service.Name='" + serviceName + "'");
                            ManagementObject wmiService = new ManagementObject(scope, path, null);
                            wmiService.Get();
                            //name = wmiService.GetPropertyValue("Name").ToString();
                            status = wmiService.GetPropertyValue("State").ToString().ToLower();
                            _ServiceStatus.Add(serviceName, status == "running");
                        }
                        catch (Exception)
                        {
                            _ServiceStatus.Add(serviceName, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "IsServiceRunning", "Error: ", ex.ToString());
                }
            }
            return _ServiceStatus;
        }

        public static Dictionary<string, bool> IsWebServiceRunning()
        {
            Dictionary<string, bool> _WebServiceStatus = new Dictionary<string, bool>();
            List<string> webServiceList = new List<string>();

            try
            {
                using (ApasLogic apasLogic = new ApasLogic())
                {
                    webServiceList.Add(apasLogic.qs.Url);
                    webServiceList.Add(apasLogic.qs2.Url);
                    webServiceList.Add(apasLogic.distsvc.Url);
                    webServiceList.Add(apasLogic.toksvc.Url);
                    webServiceList.Add(apasLogic.instsvc.Url);

                    foreach (string webService in webServiceList)
                    {
                        try
                        {
                            var myRequest = (HttpWebRequest)WebRequest.Create(webService);
                            var response = (HttpWebResponse)myRequest.GetResponse();
                            _WebServiceStatus.Add(webService, response.StatusCode == HttpStatusCode.OK);
                        }
                        catch (Exception)
                        {
                            _WebServiceStatus.Add(webService, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "IsWebServiceRunning", "Error: ", ex.ToString());
                }
            }

            return _WebServiceStatus;
        }

        public static UserAccessObj GetUserAccess()
        {
            UserAccessObj _UserAccess = new UserAccessObj();
            try
            {
                _UserAccess = (UserAccessObj)HttpContext.Current.Session["UserAccess"];
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "GetUserAccess", "Error: ", ex.ToString());
                }
            }
            return _UserAccess;
        }

        public static bool SaveUserAccess(UserAccessObj userAccess) {
            bool success = false;
            try
            {
                HttpContext.Current.Session.Add("UserAccess", userAccess);
                success = true;
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "SaveUserAccess", "Error: ", ex.ToString());
                }
            }
            return success;
        }

        public static bool UserHasAccess(string actionName, string controllerName) {
            bool success = false;
            try
            {
                UserAccessObj _UserAccess = GetUserAccess();

                if (_UserAccess != null)
                {
                    if (_UserAccess.AccessGroups != null && _UserAccess.AccessGroups.Contains(Enums.AccessGroupType.Admin))
                    {
                        success = true;
                    }
                    else
                    {
                        Enums.ActionTypeType actionType = (Enums.ActionTypeType)Enum.Parse(typeof(Enums.ActionTypeType), actionName);
                        Enums.ControllerTypeType controllerType = (Enums.ControllerTypeType)Enum.Parse(typeof(Enums.ControllerTypeType), controllerName);

                        bool controller = _UserAccess.Controllers.Exists(x => x.ControllerType == controllerType);
                        bool action = _UserAccess.Controllers.Exists(x => x.Actions.Exists(y => y.ActionType == actionType));

                        if (controller && action)
                        {
                            success = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "UserHasAccess", "Error: ", ex.ToString());
                }
            }
            return success;
        }

        public static HtmlString GetHtmlDocument(string key, bool multiple, DocumentTypeCodeType? docType = null) {
            HtmlString TransformedDocument = new HtmlString(Structs.Literals.DocumentNotRendered);
            string xmlString = string.Empty;
            string defaultXslPath = string.Empty;
            string documentType = string.Empty;
            string rootAttribute = string.Empty;
            List<string> keys = new List<string>();
            try
            {
                if (docType != null && !string.IsNullOrWhiteSpace(key)) {
                    using (LcapasLogic lcapasLogic = new LcapasLogic())
                    {
                        if (multiple)
                        {
                            keys = lcapasLogic.GetKeyValueTempCache(key).Select(a => a.Value).ToList();
                        };

                        if (!keys.Any()) { keys = new List<string>() { key }; }

                        switch (docType)
                        {
                            case DocumentTypeCodeType.StudentRequest:
                            case DocumentTypeCodeType.RequestedRecord:
                            case DocumentTypeCodeType.InstitutionRequest:
                            case DocumentTypeCodeType.ThirdPartyRequest:
                            case DocumentTypeCodeType.Request:
                                xmlString = lcapasLogic.GetTransmissionDataXmlString(keys, out rootAttribute);
                                documentType = lcapasLogic.GetDocumentVersion(xmlString);
                                switch (documentType)
                                {
                                    case Structs.DocumentType.CollegeTranscript:
                                        defaultXslPath = Structs.StyleSheetPaths.ApasTranscriptPS;
                                        break;
                                    case Structs.DocumentType.HighSchoolTranscript:
                                        defaultXslPath = Structs.StyleSheetPaths.ApasTranscriptHS;
                                        break;
                                    case Structs.DocumentType.Request:
                                        defaultXslPath = Structs.StyleSheetPaths.ApasTranscriptRequest;
                                        break;
                                }
                                break;
                            case DocumentTypeCodeType.Response:
                                xmlString = lcapasLogic.GetTransmissionDataXmlString(keys, out rootAttribute);
                                documentType = lcapasLogic.GetDocumentVersion(xmlString);
                                switch (documentType)
                                {
                                    case Structs.DocumentType.CollegeTranscript:
                                        defaultXslPath = Structs.StyleSheetPaths.ApasTranscriptPS;
                                        break;
                                    case Structs.DocumentType.HighSchoolTranscript:
                                        defaultXslPath = Structs.StyleSheetPaths.ApasTranscriptHS;
                                        break;
                                    case Structs.DocumentType.Response:
                                        defaultXslPath = Structs.StyleSheetPaths.ApasTranscriptResponse;
                                        break;
                                }
                                break;
                            case DocumentTypeCodeType.DisbursementRoster:
                                break;
                            case DocumentTypeCodeType.DisbursementForecast:
                                break;
                            case DocumentTypeCodeType.DisbursementAcknowledgement:
                                break;
                            case DocumentTypeCodeType.Application:
                                xmlString = lcapasLogic.GetApplicationXml(keys);
                                defaultXslPath = Structs.StyleSheetPaths.ApasApplication;
                                break;
                            case DocumentTypeCodeType.Change:
                                break;
                            case DocumentTypeCodeType.CertificationRequest:
                                break;
                            case DocumentTypeCodeType.Receipt:
                                break;
                            case DocumentTypeCodeType.TermEnroll:
                                break;
                            case DocumentTypeCodeType.TermGrade:
                                break;
                            default:
                                break;
                        }
                    }

                    if (xmlString != null)
                    {
                        defaultXslPath = GetApplicationPath() + defaultXslPath;
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(xmlString);
                        TransformXml xmlToTransform = new TransformXml(xmlDoc, Enums.XsltAllow.DocumentFunction | Enums.XsltAllow.DtdProcessing | Enums.XsltAllow.ExternalResources, new Uri(defaultXslPath, UriKind.Absolute));

                        TransformedDocument = new HtmlString(xmlToTransform.ApplyTransformation());
                    }
                }           
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "GetHtmlDocument", "Error: ", ex.ToString());
                }
            }
            return TransformedDocument;
        }

        public static HtmlString GetErrorMessageHtmlDocument(string requestTrackingID)
        {
            HtmlString TransformedDocument = new HtmlString(Structs.Literals.NoDocumentFound);
            string xmlString = string.Empty;
            string defaultXslPath = string.Empty;
            try
            {
                if (!string.IsNullOrWhiteSpace(requestTrackingID))
                {
                    using (LcapasLogic lcapasLogic = new LcapasLogic())
                    {
                        defaultXslPath = Structs.StyleSheetPaths.ErrorMessage;
                        xmlString = lcapasLogic.GetErrorMessageXmlString(requestTrackingID);
                    }

                    if (!string.IsNullOrWhiteSpace(xmlString))
                    {
                        defaultXslPath = GetApplicationPath() + defaultXslPath;
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(xmlString);
                        TransformXml xmlToTransform = new TransformXml(xmlDoc, Enums.XsltAllow.DocumentFunction | Enums.XsltAllow.DtdProcessing | Enums.XsltAllow.ExternalResources, new Uri(defaultXslPath, UriKind.Absolute));

                        TransformedDocument = new HtmlString(xmlToTransform.ApplyTransformation());
                    }
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "GetErrorMessageHtmlDocument", "Error: ", ex.ToString());
                }
            }
            return TransformedDocument;
        }

        public static string GetApplicationPath()
        {
            string path = string.Empty;
            try
            {
                path = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath;
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "GetApplicationPath", "Error: ", ex.ToString());
                }
            }

            return path;
        }

        public static HtmlString GetReportHtmlDocument(string _reportId, string docType, bool allSelected = false, string filterFields = null, string destAction = null)
        {
            HtmlString TransformedDocument = new HtmlString(Structs.Literals.NoDocumentFound);
            string xmlString = string.Empty;
            string defaultXslPath = string.Empty;
            List<string> _reportIdList;

            try
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    _reportIdList = lcapasLogic.GetKeyValueTempCache(_reportId).Select(a => a.Value).ToList();
                }

                if (_reportIdList.Any())
                {
                    xmlString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
                    switch (docType)
                    {
                        case Structs.DocumentType.StudentDailyRequest:
                            defaultXslPath = Structs.StyleSheetPaths.DailyRequestReport;

                            xmlString += "<DailyStudentRequests>";

                            if (_reportIdList != null && _reportIdList.Count > 0)
                            {
                                using (ColleagueLogic collLogic = new ColleagueLogic())
                                {
                                    List<DailyRequestReportSearchResultsFilter> _DailyRequestList = collLogic.GetDailyRequestReportByIds(_reportIdList.ToArray(), allSelected, filterFields);
                                    foreach (DailyRequestReportSearchResultsFilter _DailyRequest in _DailyRequestList)
                                    {
                                        xmlString += _DailyRequest.Serialize();
                                    }
                                }
                            }

                            xmlString += "</DailyStudentRequests>";
                            break;
                        case Structs.DocumentType.LoginHistory:
                            defaultXslPath = Structs.StyleSheetPaths.LoginHistoryReport;

                            xmlString += "<LoginHistoryReports>";

                            if (_reportIdList != null && _reportIdList.Count > 0)
                            {
                                using (LcapasLogic lcapasLogic = new LcapasLogic())
                                {
                                    List<LoginHistoryReportSearchResultsFilter>  _LoginHistoryList = lcapasLogic.GetLoginHistoryReportByIds(_reportIdList.ToArray(), allSelected, filterFields);
                                    foreach (LoginHistoryReportSearchResultsFilter _LoginHistory in _LoginHistoryList)
                                    {
                                        xmlString += _LoginHistory.Serialize();
                                    }
                                }
                            }

                            xmlString += "</LoginHistoryReports>";
                            break;
                        case Structs.DocumentType.ExceptionError:
                            defaultXslPath = Structs.StyleSheetPaths.ExceptionErrorReport;

                            xmlString += "<ExceptionErrorReports>";

                            if (_reportIdList != null && _reportIdList.Count > 0)
                            {
                                using (LcapasLogic lcapasLogic = new LcapasLogic())
                                {
                                    List<ExceptionErrorsReportSearchResultsFilter> _ExceptionErrorsList = lcapasLogic.GetExceptionErrorReportByIds(_reportIdList.ToArray(), allSelected, filterFields);
                                    foreach (ExceptionErrorsReportSearchResultsFilter _ExceptionErrors in _ExceptionErrorsList)
                                    {
                                        xmlString += _ExceptionErrors.Serialize();
                                    }
                                }
                            }

                            xmlString += "</ExceptionErrorReports>";
                            break;
                        case Structs.DocumentType.ApplicationReport:
                            defaultXslPath = Structs.StyleSheetPaths.ApplicationReport;

                            xmlString += "<ApplicationReports>";

                            if (_reportIdList != null && _reportIdList.Count > 0)
                            {
                                using (LcapasLogic lcapasLogic = new LcapasLogic())
                                {
                                    List<ApplicationReportSearchResultsFilter> _ApplicationList = lcapasLogic.GetApplicationReportByIds(_reportIdList.ToArray(), allSelected, filterFields);
                                    foreach (ApplicationReportSearchResultsFilter _Application in _ApplicationList)
                                    {
                                        xmlString += _Application.Serialize();
                                    }
                                }
                            }

                            xmlString += "</ApplicationReports>";
                            break;
                        case Structs.DocumentType.PaymentReport:
                            defaultXslPath = Structs.StyleSheetPaths.PaymentReport;

                            xmlString += "<PaymentReports>";

                            if (_reportIdList != null && _reportIdList.Count > 0)
                            {
                                using (LcapasLogic lcapasLogic = new LcapasLogic())
                                {
                                    List<PaymentReportSearchResultsFilter> _PaymentList = lcapasLogic.GetPaymentReportByIds(_reportIdList.ToArray(), allSelected, filterFields);
                                    foreach (PaymentReportSearchResultsFilter _Payment in _PaymentList)
                                    {
                                        xmlString += _Payment.Serialize();
                                    }
                                }
                            }

                            xmlString += "</PaymentReports>";
                            break;
                        case Structs.DocumentType.HoldTypeReport:
                            defaultXslPath = Structs.StyleSheetPaths.HoldTypeReport;

                            xmlString += "<HoldTypeReports>";

                            if (_reportIdList != null && _reportIdList.Count > 0)
                            {
                                using (LcapasLogic lcapasLogic = new LcapasLogic())
                                {
                                    List<HoldTypeReportSearchResultsFilter> _HoldTypeList = lcapasLogic.GetHoldTypeReportByIds(_reportIdList.ToArray(), destAction, filterFields);
                                    foreach (HoldTypeReportSearchResultsFilter _HoldType in _HoldTypeList)
                                    {
                                        xmlString += _HoldType.Serialize();
                                    }
                                }
                            }

                            xmlString += "</HoldTypeReports>";
                            break;
                        case Structs.DocumentType.CollProgramApplicationReport:
                            defaultXslPath = Structs.StyleSheetPaths.CollProgramApplicationReport;

                            xmlString += "<ProgramApplicationReports>";

                            if (_reportIdList != null && _reportIdList.Count > 0)
                            {
                                using (ColleagueLogic collLogic = new ColleagueLogic())
                                {
                                    List<CollProgramApplicationReportSearchResultsFilter> _ProgramApplicationList = collLogic.GetProgramApplicationReportByIds(_reportIdList.ToArray(), allSelected, filterFields);
                                    foreach (CollProgramApplicationReportSearchResultsFilter _ProgramApplication in _ProgramApplicationList)
                                    {
                                        xmlString += _ProgramApplication.Serialize();
                                    }
                                }
                            }

                            xmlString += "</ProgramApplicationReports>";
                            break;
                        case Structs.DocumentType.SentEmailReport:
                            defaultXslPath = Structs.StyleSheetPaths.SentEmailReport;

                            xmlString += "<SentEmailReports>";

                            if (_reportIdList != null && _reportIdList.Count > 0)
                            {
                                using (LcapasLogic lcapasLogic = new LcapasLogic())
                                {
                                    List<SentEmailReportSearchResultsFilter> _SentEmailList = lcapasLogic.GetSentEmailReportByIds(_reportIdList.ToArray(), allSelected, filterFields);
                                    foreach (SentEmailReportSearchResultsFilter _SentEmail in _SentEmailList)
                                    {
                                        xmlString += _SentEmail.Serialize();
                                    }
                                }
                            }

                            xmlString += "</SentEmailReports>";
                            break;
                        case Structs.DocumentType.CollTestingResultsReport:
                            defaultXslPath = Structs.StyleSheetPaths.CollTestingResultsReport;

                            xmlString += "<TestingResultsReports>";

                            if (_reportIdList != null && _reportIdList.Count > 0)
                            {
                                using (ColleagueLogic collLogic = new ColleagueLogic())
                                {
                                    List<CollTestingResultsReportSearchResultsFilter> _TestingResultsList = collLogic.GetCollTestingResultsReportByIds(_reportIdList.ToArray(), allSelected, filterFields);
                                    foreach (CollTestingResultsReportSearchResultsFilter _TestingResults in _TestingResultsList)
                                    {
                                        xmlString += _TestingResults.Serialize();
                                    }
                                }
                            }

                            xmlString += "</TestingResultsReports>";
                            break;
                           
                        case Structs.DocumentType.CollWaitListReport:
                            defaultXslPath = Structs.StyleSheetPaths.CollWaitListReport;

                            xmlString += "<WaitListReports>";

                            if (_reportIdList != null && _reportIdList.Count > 0)
                            {
                                using (ColleagueLogic collLogic = new ColleagueLogic())
                                {
                                    List<CollWaitListReportSearchResultsFilter> _WaitListList = collLogic.GetWaitListReportByIds(_reportIdList.ToArray(), allSelected, filterFields);
                                    foreach (CollWaitListReportSearchResultsFilter _WaitList in _WaitListList)
                                    {
                                        xmlString += _WaitList.Serialize();
                                    }
                                }
                            }

                            xmlString += "</WaitListReports>";
                            break;
                        case Structs.DocumentType.CollAdmissionConditionsReport:
                            defaultXslPath = Structs.StyleSheetPaths.CollAdmissionConditionsReport;

                            xmlString += "<AdmissionConditionsReports>";

                            if (_reportIdList != null && _reportIdList.Count > 0)
                            {
                                using (ColleagueLogic collLogic = new ColleagueLogic())
                                {
                                    List<CollAdmissionConditionsReportSearchResultsFilter> _AdmissionConditionList = collLogic.GetCollAdmissionConditionsReportByIds(_reportIdList.ToArray(), allSelected, filterFields);
                                    foreach (CollAdmissionConditionsReportSearchResultsFilter _AdmissionCondition in _AdmissionConditionList)
                                    {
                                        xmlString += _AdmissionCondition.Serialize();
                                    }
                                }
                            }

                            xmlString += "</AdmissionConditionsReports>";
                            break;
                        case Structs.DocumentType.CollOverduesReport:
                            defaultXslPath = Structs.StyleSheetPaths.CollOverduesReport;

                            xmlString += "<OverduesReports>";

                            if (_reportIdList != null && _reportIdList.Count > 0)
                            {

                                using (ColleagueLogic collLogic = new ColleagueLogic())
                                {
                                    List<CollOverduesReportSearchResultsFilter> _OverduesList = collLogic.GetCollOverduesReportByIds(_reportIdList.ToArray(), allSelected, filterFields);
                                    foreach (CollOverduesReportSearchResultsFilter _Overdues in _OverduesList)
                                    {
                                        xmlString += _Overdues.Serialize();
                                    }
                                }
                            }
                            xmlString += "</OverduesReports>";
                            break;

                        case Structs.DocumentType.CollMissingGradeReport:
                            defaultXslPath = Structs.StyleSheetPaths.CollMissingGradeReport;

                            xmlString += "<MissingGradeReports>";

                            if (_reportIdList != null && _reportIdList.Count > 0)
                            {
                                using (ColleagueLogic collLogic = new ColleagueLogic())
                                {
                                    //List<CollMissingGradeReportSearchResultsFilter> _MissingGradeList = collLogic.GetCollMissingGradeReportByIds(_reportIdList.ToArray());
                                    //foreach (CollWaitListReportSearchResultsFilter _MissingList in _MissingGradeList)
                                    //{
                                    //    xmlString += _MissingList.Serialize();
                                    //}
                                }
                            }

                            xmlString += "</WaitListReports>";
                            break;

                        case Structs.DocumentType.TranserCreditReport:
                            defaultXslPath = Structs.StyleSheetPaths.TransferCreditReport;

                            xmlString += "<TransferCredits>";

                            if (_reportIdList != null && _reportIdList.Count > 0)
                            {
                                using (ColleagueLogic collLogic = new ColleagueLogic())
                                {
                                    List<TransferCreditSearchResultsFilter> _TransferCreditList = collLogic.GetTransferCreditsReportByIds(_reportIdList.ToArray(), allSelected, filterFields);
                                    foreach (TransferCreditSearchResultsFilter _TransferCredit in _TransferCreditList)
                                    {
                                        xmlString += _TransferCredit.Serialize();
                                    }
                                }
                            }

                            xmlString += "</TransferCredits>";
                            break;

                        case Structs.DocumentType.UnsolicitedBatchTranscriptReport:
                            defaultXslPath = Structs.StyleSheetPaths.UnsolicitedBatchTranscriptReport;

                            xmlString += "<UnsolicitedBatchTranscriptReports>";

                            if (_reportIdList != null && _reportIdList.Count > 0)
                            {
                                using (ColleagueLogic collLogic = new ColleagueLogic())
                                {
                                    List<UnsolicitedBatchObj> _UnsolicitedBatchTranscriptList = collLogic.GetUnsolicitedBatchTranscriptReportByIds(_reportIdList.ToArray(), allSelected, filterFields);
                                    foreach (UnsolicitedBatchObj _UnsolicitedBatchTranscript in _UnsolicitedBatchTranscriptList)
                                    {
                                        xmlString += _UnsolicitedBatchTranscript.Serialize();
                                    }
                                }
                            }

                            xmlString += "</UnsolicitedBatchTranscriptReports>";
                            break;

                        case Structs.DocumentType.MyCredsTransReport:
                            defaultXslPath = Structs.StyleSheetPaths.MyCredsBatchTranscriptReport;

                            xmlString += "<MyCredsBatchTranscriptReports>";

                            if (_reportIdList != null && _reportIdList.Count > 0)
                            {
                                using (ColleagueLogic collLogic = new ColleagueLogic())
                                {
                                    List<TRRQRequestObj> _TRRQRequestObjList = collLogic.GetMyCredsRequestReportByIds(_reportIdList.ToArray(), allSelected, filterFields);
                                    foreach (TRRQRequestObj _TRRQRequestObj in _TRRQRequestObjList)
                                    {
                                        xmlString += _TRRQRequestObj.Serialize();
                                    }
                                }
                            }

                            xmlString += "</MyCredsBatchTranscriptReports>";
                            break;

                        case Structs.DocumentType.MyCredsBulkSendReport:
                            defaultXslPath = Structs.StyleSheetPaths.MyCredsBulkSendReport;

                            xmlString += "<MyCredsBulkSendReports>";

                            if (_reportIdList != null && _reportIdList.Count > 0)
                            {
                                using (ColleagueLogic collLogic = new ColleagueLogic())
                                {
                                    List<StudentObj> _StudentObjList = collLogic.GetMyCredsBulkSendReportByIds(_reportIdList.ToArray(), allSelected, filterFields);
                                    foreach (StudentObj _StudentObj in _StudentObjList)
                                    {
                                        xmlString += _StudentObj.Serialize();
                                    }
                                }
                            }

                            xmlString += "</MyCredsBulkSendReports>";
                            break;
                    }

                    xmlString = xmlString.RemoveDuplicateXmlDeclarations();

                    if (!string.IsNullOrWhiteSpace(xmlString))
                    {
                        defaultXslPath = GetReportPath() + defaultXslPath;
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(xmlString);
                        TransformXml xmlToTransform = new TransformXml(xmlDoc, Enums.XsltAllow.DocumentFunction | Enums.XsltAllow.DtdProcessing | Enums.XsltAllow.ExternalResources, new Uri(defaultXslPath, UriKind.Absolute));

                        TransformedDocument = new HtmlString(xmlToTransform.ApplyTransformation());
                    }
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "GetReportHtmlDocument", "Error: ", ex.ToString());
                }
            }
            return TransformedDocument;
        }


        public static HtmlString GetQueryHtmlDocument( string docType)
        {
            HtmlString TransformedDocument = new HtmlString(Structs.Literals.NoDocumentFound);
            string xmlString = string.Empty;
            string defaultXslPath = string.Empty;

            try
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())

                xmlString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
                defaultXslPath = "/Stylesheets/LC/Query.xsl";
                xmlString += "<QueryReports>";
                switch (docType)
                    {

                    case Structs.DocumentType.CollOverduesQuery:

                        using (LcapasLogic lcapasLogic = new LcapasLogic())
                        {
                            xmlString += lcapasLogic.GetCollQueryXmlString(Structs.DocumentType.CollOverduesQuery);
                        }
                          
                        break;
                    case Structs.DocumentType.CollWaitListQuery:

                        using (LcapasLogic lcapasLogic = new LcapasLogic())
                        {
                            xmlString += lcapasLogic.GetCollQueryXmlString(Structs.DocumentType.CollWaitListQuery);
                        }

                        break;
                    case Structs.DocumentType.CollAdmissionQuery:

                        using (LcapasLogic lcapasLogic = new LcapasLogic())
                        {
                            xmlString += lcapasLogic.GetCollQueryXmlString(Structs.DocumentType.CollAdmissionQuery);
                        }

                         break;
                    case Structs.DocumentType.CollApplicationQuery:

                        using (LcapasLogic lcapasLogic = new LcapasLogic())
                        {
                            xmlString += lcapasLogic.GetCollQueryXmlString(Structs.DocumentType.CollApplicationQuery);
                        }

                        break;
                    case Structs.DocumentType.CollTestingQuery:

                        using (LcapasLogic lcapasLogic = new LcapasLogic())
                        {
                            xmlString += lcapasLogic.GetCollQueryXmlString(Structs.DocumentType.CollTestingQuery);
                        }

                        break;
                }

                    xmlString += "</QueryReports>";
                    xmlString = xmlString.RemoveDuplicateXmlDeclarations();

                    if (!string.IsNullOrWhiteSpace(xmlString))
                    {
                        defaultXslPath = GetReportPath() + defaultXslPath;
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(xmlString);
                        TransformXml xmlToTransform = new TransformXml(xmlDoc, Enums.XsltAllow.DocumentFunction | Enums.XsltAllow.DtdProcessing | Enums.XsltAllow.ExternalResources, new Uri(defaultXslPath, UriKind.Absolute));

                        TransformedDocument = new HtmlString(xmlToTransform.ApplyTransformation());
                    }
                
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "GetQueryHtmlDocument", "Error: ", ex.ToString());
                }
            }
            return TransformedDocument;
        }

        public static string GetReportPath()
        {
            string path = string.Empty;
            try
            {
                path = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath;
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "GetReportPath", "Error: ", ex.ToString());
                }
            }

            return path;
        }

        public static byte[] GetReportExcelDocument(string reportId, string reportType, bool allSelected = false, string filterFields = null, string destAction = null)
        {
            byte[] file = null;

            try
            {
                string[] idList = reportId.Split(',');

                switch (reportType)
                {
                    case Structs.ReportType.AdmissionConditionsReport:
                        CollAdmissionConditionsReportViewObj _CollAdmissionConditionsReport = new CollAdmissionConditionsReportViewObj();
                        file = _CollAdmissionConditionsReport.Export(idList, allSelected, filterFields);
                        break;
                    case Structs.ReportType.WaitListReport:
                        CollWaitListReportViewObj _CollWaitListReport = new CollWaitListReportViewObj();
                        file = _CollWaitListReport.Export(idList, allSelected, filterFields);
                        break;
                    case Structs.ReportType.DailyRequestReport:
                        DailyRequestReportViewObj _DailyRequestReport = new DailyRequestReportViewObj();
                        file = _DailyRequestReport.Export(idList, allSelected, filterFields);
                        break;
                    case Structs.ReportType.OverduesReport:
                        CollOverduesReportViewObj _CollOverduesReport = new CollOverduesReportViewObj();
                        file = _CollOverduesReport.Export(idList, allSelected, filterFields);
                        break;
                    case Structs.ReportType.TestingResultsReport:
                        CollTestingResultsReportViewObj _CollTestingResultsReport = new CollTestingResultsReportViewObj();
                        file = _CollTestingResultsReport.Export(idList, allSelected, filterFields);
                        break;
                    case Structs.ReportType.WebApplicationReport:
                        CollProgramApplicationReportViewObj _CollProgramApplicationReport = new CollProgramApplicationReportViewObj();
                        file = _CollProgramApplicationReport.Export(idList, allSelected, filterFields);
                        break;
                    case Structs.ReportType.ToolkitApplicationReport:
                        ApplicationReportViewObj _ApplicationReportViewObj = new ApplicationReportViewObj();
                        file = _ApplicationReportViewObj.Export(idList, allSelected, filterFields);
                        break;
                    case Structs.ReportType.HoldTypeReport:
                        HoldTypeReportViewObj _HoldTypeReport = new HoldTypeReportViewObj();
                        file = _HoldTypeReport.Export(idList, destAction, filterFields);
                        break;
                    case Structs.ReportType.TransferCreditReport:
                        TransferCreditViewObj _TransferCreditReport = new TransferCreditViewObj();
                        file = _TransferCreditReport.Export(idList, allSelected, filterFields);
                        break;
                    case Structs.ReportType.TransferCreditReportXML:
                        TCA _TransferCreditReportXML = new TCA();
                        file = _TransferCreditReportXML.XMLExport(idList, allSelected, filterFields);
                        break;
                    case Structs.ReportType.UnsolicitedBatchTranscriptReport:
                        UnsolicitedBatchListViewObj _UnsolicitedBatchTranscriptReport = new UnsolicitedBatchListViewObj();
                        file = _UnsolicitedBatchTranscriptReport.Export(idList, allSelected, filterFields);
                        break;
                    case Structs.ReportType.MyCredsBatchTranscriptReport:
                        TRRQRequestListViewObj _MyCredsBatchTranscriptReport = new TRRQRequestListViewObj();
                        file = _MyCredsBatchTranscriptReport.Export(idList, allSelected, filterFields);
                        break;
                    case Structs.ReportType.MyCredsBulkSendReport:
                        BulkSendListViewObj _MyCredsBulkSendReport = new BulkSendListViewObj();
                        file = _MyCredsBulkSendReport.Export(idList, allSelected, filterFields);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "GetReportHtmlDocument", "Error: ", ex.ToString());
                }
            }
            return file;
        }

        public static void LogOut( string controllerName, string actionName) {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Session.Abandon();
            }
            if (!(controllerName == "Login" && actionName == "Index")) {
                FormsAuthentication.RedirectToLoginPage();
            }             
        }

        public static string EdiASCISafe(this object value)
        {
            string retval = string.Empty;
            try
            {
                if (value == null)
                {
                    retval = string.Empty;
                }
                else {
                    retval = Regex.Replace(value.ToString(), @"[^\u0000-\u007F]+", string.Empty);
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "EdiASCISafe", "Error: ", ex.ToString());
                }
            }
            return retval;
        }

        public static string EdiNonASCISafe(this object value)
        {
            string retval = string.Empty;
            try
            {
                if (value == null)
                {
                    retval = string.Empty;
                }
                else
                {
                    retval = Regex.Replace(value.ToString(), @"[^\u0000-\u007F]+", string.Empty);
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "EdiNonASCISafe", "Error: ", ex.ToString());
                }
            }
            return retval;
        }

        public static string CheckGuids(string keys) {
            string retStr = string.Empty;
            List<string> cleanGuids = new List<string>();
            Guid result;
            try
            {

                if (keys.Contains(','))
                {
                    List<string> keyList = keys.Split(',').ToList();

                    foreach (string key in keyList)
                    {
                        if (Guid.TryParse(key, out result))
                        {
                            cleanGuids.Add(result.ToString());
                        }
                    }
                    retStr = string.Join(",", cleanGuids.ToArray());
                }
                else {
                    retStr = CheckGuid(keys);
                }              
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "TryParseGuid", "Error: ", ex.ToString());
                }
            }
            return retStr;
        }

        public static string CheckGuid(string key) {
            string retStr = string.Empty;
            Guid result;
            try
            {
                if (Guid.TryParse(key, out result)) {
                    retStr = result.ToString();
                }

            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "CheckGuid", "Error: ", ex.ToString());
                }
            }
            return retStr;
        }

        public static string CleanString(string input, bool encodeQuotation = false)
        {
            string output = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    output = input.Replace("  "," ");
                    if (encodeQuotation)
                    {
                        output = output.Replace("'", "''");
                    }
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "CleanString", "Error: ", ex.ToString());
                }
            }

            return output;
        }

        // Remove character (Ex: dash "-") from phone if it's bigger than maxLength
        public static string CleanPhone(string input, string removeString, int maxLength = 0)
        {
            string output = input;

            try
            {
                if (!string.IsNullOrWhiteSpace(input) && !string.IsNullOrWhiteSpace(removeString) && (maxLength > 0) && (input.Count() > maxLength))
                {
                    output = input.Replace(removeString, "").Substring(0, maxLength);
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "CleanPhone", "Error: ", ex.ToString());
                }
            }

            return output;
        }

        public static string FixDomesticPhone(string phoneNumber, string areaCode = null) {
            Regex rgx = new Regex("[^\\d]");
            string retval = string.Empty;
            try
            {
                string strNum = rgx.Replace((areaCode ?? string.Empty) + (phoneNumber ?? string.Empty), "");
                if (!string.IsNullOrWhiteSpace(strNum)) {
                    if (strNum.Length == 10)
                    {
                        retval = String.Format("{0:###-###-####}", Convert.ToInt64(strNum));
                    } else
                    {
                        // International Phone
                        if (!string.IsNullOrWhiteSpace(areaCode))
                        {
                            retval = String.Format("{0:#######}", Convert.ToInt64(areaCode));
                        }
                        if (!string.IsNullOrWhiteSpace(phoneNumber))
                        {
                            retval = retval + (!string.IsNullOrWhiteSpace(retval) ? "-":"") + String.Format("{0:##########}", Convert.ToInt64(phoneNumber));
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "FixDomesticPhone", "Error: ", ex.ToString());
                }
            }

            return retval;
        }

        public static DateTime? CheckForNullDate(DateTime? date = null) {
            DateTime? _FixedDate = null;
            List<string> _NullDateFormats = new List<string> { "1/1/0001 12:00:00 AM", "1900-01-01 00:00:00.000", "1/1/1900 12:00:00 AM" };

            try
            {
                if (date != null && !_NullDateFormats.Contains(date.ToString())) {
                    _FixedDate = date;
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "CheckForNullDate", "Error: ", ex.ToString());
                }
            }
            return _FixedDate;
        }

        public static string JoinStrings(List<string> strList = null)
        {
            string _retString = null;

            try
            {
                if (strList != null && strList.Count() > 0) {
                    _retString = string.Join(" ", strList);
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "JoinString", "Error: ", ex.ToString());
                }
            }
            return _retString;
        }

        public static string GetLcTerm(string sessionDesignator)
        {
            string result = "";

            try
            {
                if (!string.IsNullOrWhiteSpace(sessionDesignator))
                {
                    var arry = sessionDesignator.Split('-');
                    string year = arry[0].Substring(2, 2);
                    string term = string.Empty;

                    switch (arry[1])
                    {
                        case "01":
                            term = "WN";
                            break;
                        case "09":
                            term = "FL";
                            break;
                        case "06":
                            term = "S2";
                            break;
                        case "07":
                            term = "S1";
                            break;
                        default:
                            term = "SM";
                            break;
                    }
                    result = year + term;
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "GetLcTerm", "Error: ", ex.ToString());
                }
            }

            return result;
        }

        public static string GetLcCurrentTerm()
        {
            string result = null;

            try
            {
                string currentMonth = DateTime.Now.Month.ToString();
                string currentYear = DateTime.Now.Year.ToString();

                if (!string.IsNullOrWhiteSpace(currentMonth) && !string.IsNullOrWhiteSpace(currentYear))
                {
                    string year = currentYear.Substring(2, 2);
                    string term = string.Empty;

                    if (Int32.TryParse(currentMonth, out int month))
                    {
                        switch (month)
                        {
                            case int n when (n <= 5):
                                term = "WN";
                                break;
                            case int n when (n >= 9):
                                term = "FL";
                                break;
                            default:
                                term = "SM";
                                break;
                        }
                    }

                    result = year + term;
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "GetLcCurrentTerm", "Error: ", ex.ToString());
                }
            }

            return result;
        }

        public static string GetLcTermFullDescription(string termCode)
        {
            string termDesc = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(termCode) && termCode.Length == 4)
                {
                    string year = termCode.Substring(0, 2);
                    if (DateTime.TryParseExact(year, "yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime yearParsed))
                    {
                        string term = termCode.Substring(2, 2);

                        termDesc = yearParsed.Year.ToString() + " ";

                        using (LcapasLogic lcapasLogic = new LcapasLogic())
                        {
                            termDesc = termDesc + lcapasLogic.GetTranscriptTermDesc(term);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "GetLcTermFullDescription", "Error: ", ex.ToString());
                }
            }

            return termDesc;
        }

        public static string GetLcSessionDesignator(string termCode)
        {
            string termDesc = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(termCode) && termCode.Length == 4)
                {
                    string year = termCode.Substring(0, 2);
                    if (DateTime.TryParseExact(year, "yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime yearParsed))
                    {
                        string term = termCode.Substring(2, 2);

                        termDesc = yearParsed.Year.ToString() + "-";

                        switch (term)
                        {
                            case "WN":
                                termDesc = termDesc + "01";
                                break;
                            case "FL":
                                termDesc = termDesc + "09";
                                break;
                            case "S2":
                                termDesc = termDesc + "06";  // S2 actually start May - 05, using June - 06 just to separate from SM, that also start May - 05
                                break;
                            case "S1":
                                termDesc = termDesc + "07";
                                break;
                            default:  // SM
                                termDesc = termDesc + "05";
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "GetLcSessionDesignator", "Error: ", ex.ToString());
                }
            }

            return termDesc;
        }

        public static string GetCurrentUser()
        {
            string username = HttpContext.Current.User.Identity.Name;

            if (!string.IsNullOrWhiteSpace(username))
            {
                return username;
            }
            else {
                return systemUserName;
            }           
        }

        public static bool IsUserNameEmpty(string username)
        {
            return (string.IsNullOrWhiteSpace(username) || username == systemUserName);
        }

        public static string[] SeparateNames(string fullName)
        {
            string[] result = new string[3];
            result[0] = "";  // First Name
            result[1] = null;  // Middle Name
            result[2] = "";  // Last Name

            try
            {
                if (!string.IsNullOrWhiteSpace(fullName))
                {
                    if (fullName.IndexOf(',') > 0)  // If last name is informed first by a comma
                    {
                        string[] tempFullName = fullName.Split(',');
                        result[2] = tempFullName.First();  // Last Name

                        tempFullName = tempFullName[1].Trim().Split(' ');
                        if (tempFullName.Length > 0)
                        {
                            result[0] = tempFullName[0];  // First Name
                        }
                        if (tempFullName.Length > 1)
                        {
                            result[1] = string.Join(" ", tempFullName, 1, tempFullName.Length - 1);  // Middle Name
                        }
                    }
                    else
                    {
                        string[] tempFullName = fullName.Split(' ');
                        if (tempFullName.Length > 0)
                        {
                            result[0] = tempFullName[0];  // First Name
                        }
                        if (tempFullName.Length > 1)
                        {
                            result[2] = tempFullName[tempFullName.Length - 1];  // Last Name
                        }
                        if (tempFullName.Length > 2)
                        {
                            result[1] = string.Join(" ", tempFullName, 1, tempFullName.Length - 2);  // Middle Name
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "EdiASCISafe", "Error: ", ex.ToString());
                }
            }

            return result;
        }

        public const string phoneRegex = @"^\+?([0-9]{1,2})?[-. ]?\(?([0-9]{3})\)?[-. ]?([0-9]{3,4})[-. ]?([0-9]{4})[-. ]?([xX][-. ]?[0-9]{2,}|[eE][xX][tT][-. ]?[0-9]{2,})?$";

        public static string[] SeparatePhoneNumbers(string phoneNumber)
        {
            string[] result = new string[3];
            result[0] = "";  // Country Code
            result[1] = "";  // Area Code
            result[2] = "";  // Phone Number + Extenion

            try
            {
                if (!string.IsNullOrWhiteSpace(phoneNumber))
                {
                    string[] phone = Regex.Split(phoneNumber, phoneRegex);
                    phone = phone.Where(p => p != "").ToArray();

                    switch (phone.Length)
                    {
                        case 1:  // Phone Number
                            result[2] = phone[0];
                            break;
                        case 2:  // Area Code + Phone Number
                            result[1] = phone[0];
                            result[2] = phone[1];
                            break;
                        case 3:
                            if (phone[0].Length < 3)
                            {  // Country Code + Area Code + Phone Number
                                result[0] = phone[0];
                                result[1] = phone[1];
                                result[2] = phone[2];
                            }
                            else
                            {  // Area Code + Phone Number
                                result[1] = phone[0];
                                result[2] = phone[1] + ' ' + phone[2];
                            }
                            break;
                        case 4:
                            if (phone[0].Length < 3)
                            {  // Country Code + Area Code + Phone Number
                                result[0] = phone[0];
                                result[1] = phone[1];
                                result[2] = phone[2] + ' ' + phone[3];
                            }
                            else
                            {  // Area Code + Phone Number + Extension
                                result[1] = phone[0];
                                result[2] = phone[1] + ' ' + phone[2] + ' ' + phone[3];
                            }
                            break;
                        case 5:  // Country Code + Area Code + Phone Number + Extension
                            result[0] = phone[0];
                            result[1] = phone[1];
                            result[2] = phone[2] + ' ' + phone[3] + ' ' + phone[4];
                            break;
                    };
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "SeparatePhoneNumbers", "Error: ", ex.ToString());
                }
            }

            return result;
        }

        public static DateTime? ConvertDate2(string strDate)
        {
            TimeSpan ts = new TimeSpan(00, 00, 00, 000);
            DateTime newDate = new DateTime();

            try
            {
                newDate = DateTime.ParseExact(strDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                return newDate.Date + ts;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime ConvertLocalTime(DateTime? dt)
        {
            DateTime result = dt ?? DateTime.Now;
            try
            {
                if (result.TimeOfDay != new TimeSpan(0, 0, 0))
                {
                    result = result.ToLocalTime();
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "ConvertLocalTime", "Error: ", ex.ToString());
                }
            }

            return result;
        }

        public static DateTime ConvertUTCTime(DateTime? dt)
        {
            DateTime result = DateTime.Now;
            try
            {
                result = (dt ?? DateTime.Now).ToUniversalTime();
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "ConvertUTCTime", "Error: ", ex.ToString());
                }
            }

            return result;
        }

        public static string FormatColleagueDate(this DateTime date)
        {
            string totalDays = null;
            DateTime uniDataBaseDate = new DateTime(1967, 12, 31);

            try
            {
                TimeSpan difference = date - uniDataBaseDate;
                totalDays = difference.TotalDays.ToString();
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "FormatColleagueDate", "Error :", ex.ToString());
                }
            }
            return totalDays;
        }

        public static DateTime ConvertColleagueDaysToDate(this string totalDaysStr)
        {
            DateTime? resultDate = null;
            DateTime uniDataBaseDate = new DateTime(1967, 12, 31);

            try
            {
                if (double.TryParse(totalDaysStr, out double totalDays))
                {
                    resultDate = uniDataBaseDate.AddDays(totalDays);
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "ConvertColleagueDaysToDate", "Error :", ex.ToString());
                }
            }
            return resultDate.Value;
        }


        public static int? ToInt32(string value)
        {
            int? retVal = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    retVal = Int32.Parse(value);
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "ToInt32", "Error :", ex.ToString());
                }
            }

            return retVal;
        }

        public class Utf8StringWriter : StringWriter
        {
            // Use UTF8 encoding but write no BOM to the wire
            public override Encoding Encoding
            {
                get { return new UTF8Encoding(false); } // in real code I'll cache this encoding.
            }
        }

        /// <summary>
        /// Generic Serialize function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Serialize<T>(this T value, string nameSpacePrefix = null, string uri = null)
        {
            string retval = string.Empty; 

            try
            {
                if (value != null) {
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    if (!string.IsNullOrWhiteSpace(nameSpacePrefix) && !string.IsNullOrWhiteSpace(uri))
                    {
                        ns.Add(nameSpacePrefix, uri);
                    }
                    var xmlserializer = new XmlSerializer(typeof(T));
                    var stringWriter = new Utf8StringWriter();
                    using (var writer = XmlWriter.Create(stringWriter))
                    {
                        xmlserializer.Serialize(writer, value, ns);
                        retval = stringWriter.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "Serialize", "Error :" ,ex.ToString());
                }
            }

            return retval;
        }

        /// <summary>
        /// Generic Deserialize: Don't know if this will work
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Deserialize<T>(dynamic obj, string value)
        {
            dynamic objType = obj.GetType();
            try
            {
                XmlSerializer ser = new XmlSerializer(objType);
                TextReader tr = new StringReader(value);
                obj = (T)ser.Deserialize(tr);
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "Deserialize", "Error: ", ex.ToString());
                }
            }
            return obj;
        }

        public static string CheckPrefix(this string xmlString)
        {
            return xmlString.Replace("<TransmissionData>", "<AdmApp:TransmissionData>")
                            .Replace("</TransmissionData>", "</AdmApp:TransmissionData>")
                            .Replace("<Applicant>", "<AdmApp:Applicant>")
                            .Replace("</Applicant>", "</AdmApp:Applicant>");
        }

        public static string GetRootPath()
        {
            var debugPath = string.Empty;

            try
            {
                debugPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, debugPath);
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "GetRootPath", "Error: ", ex.ToString());
                }
            }
            return debugPath;
        }

        // Get XmlEnumAttribute from enum
        public static string GetXmlEnumAttribute(this Enum enumValue, Type enumType)
        {
            return enumValue != null ? 
                    (enumType.GetMember(enumValue.ToString())
                    .FirstOrDefault()
                    .GetCustomAttributes(false).OfType<XmlEnumAttribute>().FirstOrDefault()?.Name.ToString() ?? enumValue.ToString())
                    : string.Empty;
        }

        public static string GetNewLcDocumentID()
        {
            string _NewDocumentID = Guid.NewGuid().ToString();
            string localOrgId = string.Empty;
            try
            {
                _NewDocumentID = "LC" + "DOC" + DateTime.Now.ToString("yyyyMMddHHmm") + "-" + Guid.NewGuid().ToString();
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "GetNewLcDocumentID", "Error: ", ex.ToString());
                }
            }
            return _NewDocumentID.Substring(0,35);
        }

        public static string GetNewRequestTrackingId()
        {
            string _NewDocumentID = Guid.NewGuid().ToString();
            string localOrgId = string.Empty;
            try
            {
                _NewDocumentID = "LC" + "RTD" + DateTime.Now.ToString("yyyyMMddHHmm") + "-" +Guid.NewGuid().ToString();
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "GetNewRequestTrackingId", "Error: ", ex.ToString());
                }
            }
            return _NewDocumentID.Substring(0, 35);
        }

        public static DateTime GetFirstDayOfWeek(this DateTime date)
        {
            var firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

            while (date.DayOfWeek != firstDayOfWeek)
            {
                date = date.AddDays(+1);
            }

            return date;
        }

        public static DateTime GetLastBusinessDayOfMonth(int year, int month)
        {
            DateTime LastOfMonth;
            DateTime LastBusinessDay;
            int daysInMonth = DateTime.DaysInMonth(year, month);

            if (month == 12)
            {
                daysInMonth = 24;
            }

            LastOfMonth = new DateTime(year, month, daysInMonth);
            if ((LastOfMonth.DayOfWeek == DayOfWeek.Sunday))
            {
                LastBusinessDay = LastOfMonth.AddDays(-2);
            }
            else if ((LastOfMonth.DayOfWeek == DayOfWeek.Saturday))
            {
                LastBusinessDay = LastOfMonth.AddDays(-1);
            }
            else
            {
                LastBusinessDay = LastOfMonth;
            }      

            return LastBusinessDay;
        }

        public static string CleanSnumber(object value)
        {
            string retval = string.Empty;

            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                retval = value.ToString().Replace("s", "");
            }

            return retval;
        }

        public static string CleanAddressLine(this string addressLine)
        {
            string retval = null;

            if (!string.IsNullOrWhiteSpace(addressLine))
            {
                retval = addressLine.Trim();
                if (retval.Length > 40)
                {
                    retval = retval.Substring(0, 40);
                }
            }

            return retval;
        }

        public static string ToTitleCase(this string text)
        {
            string result = null;
            if (!string.IsNullOrWhiteSpace(text))
            {
                result = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(text.ToLower());
            }
            return result;
        }

        public static string TruncateString(this string value, int maxLength)
        {
            string result = null;

            if (!string.IsNullOrWhiteSpace(value))
            {
                result = value.Trim();
                if (result.Length > maxLength)
                {
                    result = result.Substring(0, maxLength);
                }
            }

            return result;
        }

        public static bool In<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }

        public static string GetStringBetween(this string strSource, string strStart, string strEnd)
        {
            string result = string.Empty;

            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                result = strSource.Substring(Start, End - Start);
            }

            return result;
        }

        public static string RemoveDuplicateXmlDeclarations(this string xmlString)
        {
            string result = string.Empty;
            string strStart = "<?xml ", strEnd = "?>";

            int StartPos, EndPos;
            if (xmlString.Contains(strStart) && xmlString.Contains(strEnd))
            {
                StartPos = xmlString.IndexOf(strEnd, 0) + 2;  // Skip first occurrence
                result = xmlString;
                while (result.IndexOf(strStart, StartPos) > 0)
                {
                    StartPos = result.IndexOf(strStart, StartPos);
                    EndPos = result.IndexOf(strEnd, StartPos) + 2;
                    result = result.Remove(StartPos, EndPos - StartPos);
                }
            }

            return result;
        }

        public static string InsertRootElement(this string xmlString, string root, string attributes = null)
        {
            string result = string.Empty;
            string strEnd = "?>";

            int InsertPos = 0;
            if (xmlString.Contains(strEnd))
            {
                InsertPos = xmlString.IndexOf(strEnd, 0) + 2;
                result = xmlString.Insert(InsertPos, "<"+root+" "+attributes+">") + "</"+root+">";
            }

            return result;
        }

        public static string NormalizePostalCode(this string postalCode, string country = null)
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(postalCode))
            {
                // Include space for Canadians Postal Codes
                if (!string.IsNullOrWhiteSpace(country) && (country == "XCA" || country == "CA"))
                {
                    // Removes any spaces 
                    result = postalCode.Trim().Replace(" ", "");

                    switch (result.Length)
                    {
                        //case 5:
                        //    // Add space after 2 characters if length is 5
                        //    result = result.Insert(2, " ");
                        //    break;
                        case 6:
                            // Add space after 3 characters if length is 6
                            result = result.Insert(3, " ");
                            break;
                            //case 7:
                            //    // Add space after 4 characters if length is 7
                            //    result = result.Insert(4, " ");
                            //    break;
                    }

                } else {
                    // If it's not a canadian address
                    result = postalCode;
                }
            }
            return result;
        }

        public static string IsValidEmail(this string email, bool includeLCEmail = false)
        {
            string result = string.Empty;

            if (!string.IsNullOrWhiteSpace(email))
            {
                Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);

                if (regex.IsMatch(email))
                {
                    // Check if it's a Lethbridge College email address
                    if (includeLCEmail || (!includeLCEmail && !email.Trim().ToLower().Contains(Structs.Email.LethbridgeCollegeEmailType)))
                    {
                        result = email;
                    }
                }
            }

            return result;
        }

    }
}
