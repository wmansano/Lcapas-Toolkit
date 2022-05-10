using Lcapas.Core.Library;
using Lcapas.Core.Logic;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Lcapas.AD.Controllers
{
    public class TranscriptsController : Controller
    {
        private ApasLogic apasLogic = new ApasLogic();
        private LcapasLogic lcapasLogic = new LcapasLogic();
        private ColleagueLogic colleagueLogic = new ColleagueLogic();

        // GET: Transcripts
        [AuthorizationRequired]
        public ActionResult Index()
        {
            ViewBag.Environment = Functions.GetEnvironment();

            return View();
        }

        [AuthorizationRequired]
        public ActionResult RequestTranscript(CreateMsgViewObj createMessageViewObj)
        {
            return CreateMessage(createMessageViewObj);
        }

        [AuthorizationRequired]
        public ActionResult SendUnsolicitedTranscript(CreateMsgViewObj createMessageViewObj)
        {
            return CreateMessage(createMessageViewObj);
        }

        [AuthorizationRequired]
        public ActionResult StudentLookup(StuLookupListViewObj lookup)
        {
            StuLookupListViewObj _Lookup = lookup ?? new StuLookupListViewObj();

            _Lookup.LoadObject();

            try
            {
                if (_Lookup.NoDestAction)
                {
                    return RedirectToAction("Index", "Transcripts");
                }
                else if (_Lookup.Redirect)
                {
                    switch (_Lookup.SearchFilter.DestAction)
                    {
                        case Structs.DestActions.SaveColleagueRequestTRRQ:
                            return RecordsInbox(null);
                        case Structs.DestActions.ExportTranscriptToColleague:
                            return AdmissionsInbox(null);
                        default:
                            return CreateMessage(new CreateMsgViewObj(_Lookup.SearchFilter));
                    }
                }               
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsController, "StudentLookup", "Error", ex.ToString());
            }

            ViewBag.Environment = Functions.GetEnvironment();

            return View("StudentLookup", _Lookup);
        }

        [AuthorizationRequired]
        public ActionResult CreateMessage(CreateMsgViewObj createMessageViewObj)
        {
            CreateMsgViewObj _CreateMessageViewObj = createMessageViewObj ?? new CreateMsgViewObj();

            _CreateMessageViewObj.LoadObject();
            
            try
            {
                if (!_CreateMessageViewObj.Ready || (string.IsNullOrWhiteSpace(_CreateMessageViewObj.StudentRecord.Snumber) && string.IsNullOrWhiteSpace(_CreateMessageViewObj.StudentRecord.ASN) && _CreateMessageViewObj.DestAction != Structs.DestActions.CreateResponse)) {

                    return StudentLookup(new StuLookupListViewObj(new SearchObj() { DestAction = _CreateMessageViewObj.DestAction, StudentRecord = _CreateMessageViewObj.StudentRecord }));
                }
                else {

                    if (_CreateMessageViewObj.PreviewMessageIndicator) {

                        _CreateMessageViewObj.GeneratePreview();
                    }
                    else if (_CreateMessageViewObj.ReleaseAuthorizedIndicator) {

                        _CreateMessageViewObj.Submit();

                        if (_CreateMessageViewObj.DestAction == Structs.DestActions.CreateRequest)
                        {
                            return RedirectToAction("AdmissionsOutbox", "Transcripts");
                        }
                        else
                        {
                            return RedirectToAction("RecordsOutbox", "Transcripts");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsController, "CreateMessage", "Error", ex.ToString());
            }

            switch (_CreateMessageViewObj.DestAction)
            {
                case Structs.DestActions.CreateRequest:
                    ViewBag.Title = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.CreateRequestTitle);
                    break;
                case Structs.DestActions.CreateTranscript:
                    ViewBag.Title = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.CreateTranscriptTitle);
                    break;
                case Structs.DestActions.CreateResponse:
                    ViewBag.Title = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.CreateResponseTitle);
                    break;
                default:
                    ViewBag.Title = _CreateMessageViewObj.DestAction;
                    break;
            }

            ViewBag.Environment = Functions.GetEnvironment();
            
            return View("CreateMessage", _CreateMessageViewObj);
        }

        [AuthorizationRequired]
        public ActionResult RecordsInbox(RequestListViewObj requests)
        {
            RequestListViewObj _Requests = requests ?? new RequestListViewObj();

            _Requests.LoadObject(Structs.DestActions.RecordsInbox);
            _Requests.SearchFilter.FromUrlAction = Url.Action();

            try
            {
                if (_Requests.StudentSelected)
                {
                    if (!string.IsNullOrWhiteSpace(_Requests.SearchFilter.DestAction)) {
                        switch (_Requests.SearchFilter.DestAction) {
                            case Structs.DestActions.RecordsOutbox:
                                return RecordsOutbox(new ResponseListViewObj(_Requests.SearchFilter));
                            default:
                                return StudentLookup(new StuLookupListViewObj(_Requests.SearchFilter));
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsController, "RecordsInbox", "Error", ex.ToString());
            }

            ViewBag.Title = "Records Inbox (Inbound Requests)";
            ViewBag.Environment = Functions.GetEnvironment();
            ViewBag.Department = _Requests.DestAction;

            return View("TransferRequests", _Requests);
        }

        [AuthorizationRequired]
        public ActionResult AdmissionsOutbox(RequestListViewObj requests)
        {
            RequestListViewObj _Requests = requests ?? new RequestListViewObj();

            _Requests.LoadObject(Structs.DestActions.AdmissionsOutbox);
            _Requests.SearchFilter.FromUrlAction = Url.Action();

            try
            {
                if (_Requests.StudentSelected)
                {
                    if (!string.IsNullOrWhiteSpace(_Requests.SearchFilter.DestAction))
                    {
                        switch (_Requests.SearchFilter.DestAction)
                        {
                            case Structs.DestActions.AdmissionsInbox:
                                return AdmissionsInbox(new ResponseListViewObj(_Requests.SearchFilter));
                            default:
                                return StudentLookup(new StuLookupListViewObj(_Requests.SearchFilter));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsController, "AdmissionsOutbox", "Error", ex.ToString());
            }


            ViewBag.Title = "Admissions Outbox (Outbound Requests)";
            ViewBag.Environment = Functions.GetEnvironment();
            ViewBag.Department = _Requests.DestAction;

            return View("TransferRequests", _Requests);
        }

        [AuthorizationRequired]
        public ActionResult AdmissionsInbox(ResponseListViewObj responses)
        {
            ResponseListViewObj _Responses = responses ?? new ResponseListViewObj();

            _Responses.LoadObject(Structs.DestActions.AdmissionsInbox);
            _Responses.SearchFilter.FromUrlAction = Url.Action();

            try
            {
                if (_Responses.StudentSelected)
                {
                    if (!string.IsNullOrWhiteSpace(_Responses.SearchFilter.DestAction))
                    {
                        switch (_Responses.SearchFilter.DestAction)
                        {
                            case Structs.DestActions.AdmissionsOutbox:
                                return AdmissionsOutbox(new RequestListViewObj(_Responses.SearchFilter));
                            default:
                                return StudentLookup(new StuLookupListViewObj(_Responses.SearchFilter));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsController, "AdmissionsInbox", "Error", ex.ToString());
            }


            ViewBag.Title = "Admissions Inbox (Inbound Responses)";
            ViewBag.Environment = Functions.GetEnvironment();
            ViewBag.Department = _Responses.DestAction;

            return View("TransferResponses", _Responses);
        }

        [AuthorizationRequired]
        public ActionResult RecordsOutbox(ResponseListViewObj responses)
        {
            ResponseListViewObj _Responses = responses ?? new ResponseListViewObj();

            _Responses.LoadObject(Structs.DestActions.RecordsOutbox);
            _Responses.SearchFilter.FromUrlAction = Url.Action();

            try
            {
                if (_Responses.StudentSelected)
                {
                    if (!string.IsNullOrWhiteSpace(_Responses.SearchFilter.DestAction))
                    {
                        switch (_Responses.SearchFilter.DestAction)
                        {
                            case Structs.DestActions.RecordsInbox:
                                return RecordsInbox(new RequestListViewObj(_Responses.SearchFilter));
                            default:
                                return StudentLookup(new StuLookupListViewObj(_Responses.SearchFilter));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsController, "RecordsOutbox", "Error", ex.ToString());
            }

            ViewBag.Title = "Records Outbox (Outbound Responses)";
            ViewBag.Environment = Functions.GetEnvironment();
            ViewBag.Department = _Responses.DestAction;

            return View("TransferResponses", _Responses);
        }

        [AuthorizationRequired]
        public ActionResult UnsolicitedBatchTranscript(UnsolicitedBatchListViewObj unsolicitedBatch)
        {
            UnsolicitedBatchListViewObj _UnsolicitedBatch = unsolicitedBatch ?? new UnsolicitedBatchListViewObj();

            try
            {
                _UnsolicitedBatch.LoadObject();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsController, "UnsolicitedBatchTranscript", "Error", ex.ToString());
            }

            ViewBag.Title = "Unsolicited Batch Transcript";
            ViewBag.Environment = Functions.GetEnvironment();

            return View("UnsolicitedBatchTranscripts", _UnsolicitedBatch);
        }

        [AuthorizationRequired]
        public JsonResult CheckTransactionTranscriptComplete(string transactionTranscriptUuid) {

            UserResultObj userResultObj = new UserResultObj()
            {
                Success = false,
                Message = string.Empty,
            };

            try
            {
                if (!string.IsNullOrWhiteSpace(transactionTranscriptUuid)) {
                    userResultObj.Success = lcapasLogic.CheckTransactionTranscriptComplete(transactionTranscriptUuid);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsController, "CheckTransactionTranscriptComplete", "Error", "transactionTranscriptId: " + transactionTranscriptUuid + ", " + ex.ToString());
            }

            return Json(userResultObj);
        }

        [AuthorizationRequired]
        public JsonResult ExportTranscriptColleague(string[] transmissionDataIdList)
        {
            UserResultObj userResultObj = new UserResultObj()
            {
                Success = false,
                Message = Structs.Literals.ContactHelpDesk,
            };

            try
            {
                if (transmissionDataIdList != null && transmissionDataIdList.Length > 0 )
                {
                    lcapasLogic.ExportTranscriptsToColleague(transmissionDataIdList.ToList(), ref userResultObj);
                }
                else
                {
                    userResultObj.Message = Structs.Export.EmptyTranscriptSelection;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsController, "ExportTranscriptColleague", "Error", "transmissionDataId: " + transmissionDataIdList.ToString() + ", " + ex.ToString());
            }

            return Json(userResultObj);
        }

        [AuthorizationRequired]
        public JsonResult ParseResponse(string transmissionDataUuid)
        {
            UserResultObj userResultObj = new UserResultObj()
            {
                Success = false,
                Message = Structs.Literals.ContactHelpDesk,
            };

            try
            {
                if (!string.IsNullOrWhiteSpace(transmissionDataUuid))
                {
                    userResultObj.Success = lcapasLogic.ParseApasMessage(transmissionDataUuid, Enums.MessageTypes.ReceivedResponse);
                }
                else
                {
                    userResultObj.Message = Structs.Export.EmptyParseTranscriptSelection;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsController, "ParseResponse", "Error", "transmissionDataUuid: " + transmissionDataUuid + ", " + ex.ToString());
            }

            return Json(userResultObj);
        }

        [AuthorizationRequired]
        public ActionResult DisplayMessage(string uuid, bool multiple = false)
        {
            HtmlString TransformedDocument = new HtmlString(Structs.Literals.DocumentNotRendered);

            var result = lcapasLogic.GetDocumentType(uuid);

            if (result != null) {
                TransformedDocument = Functions.GetHtmlDocument(uuid, multiple, result);
            }

            ViewBag.Environment = Functions.GetEnvironment();

            return View("DisplayMessage", TransformedDocument);
        }

        [AuthorizationRequired]
        public JsonResult DisplayIframeDocument(string transmissionDataUUID, bool multiple = false)
        {
            UserResultObj userResultObj = new UserResultObj()
            {
                Success = false,
                Message = Structs.Literals.DocumentNotRendered,
            };

            try
            {
                var docType = lcapasLogic.GetDocumentType(transmissionDataUUID);

                if (docType != null)
                {
                    userResultObj.Message = Functions.GetHtmlDocument(transmissionDataUUID, multiple, docType).ToHtmlString();
                    userResultObj.DocType = docType.ToString();
                    if (!string.IsNullOrWhiteSpace(userResultObj.Message) && userResultObj.Message != Structs.Literals.DocumentNotRendered) {
                        // Mark Request or Response as Viewed and return Success = true back to view/script
                        switch (docType)
                        {
                            case Core.Library.Apas.CoreMain.DocumentTypeCodeType.StudentRequest:
                            case Core.Library.Apas.CoreMain.DocumentTypeCodeType.RequestedRecord:
                            case Core.Library.Apas.CoreMain.DocumentTypeCodeType.InstitutionRequest:
                            case Core.Library.Apas.CoreMain.DocumentTypeCodeType.ThirdPartyRequest:
                            case Core.Library.Apas.CoreMain.DocumentTypeCodeType.Request:
                                userResultObj.Success = lcapasLogic.MarkRequestViewed(transmissionDataUUID);
                                break;
                            case Core.Library.Apas.CoreMain.DocumentTypeCodeType.Response:
                                userResultObj.Success = lcapasLogic.MarkResponseViewed(transmissionDataUUID);
                                break;
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsController, "DisplayIframeDocument", "Error", "transmissionDataUUID: " + transmissionDataUUID + ", " + ex.ToString());
            }

            return Json(userResultObj);
        }

        [AuthorizationRequired]
        public JsonResult DisplayErrorMessages(string requestTrackingID)
        {
            UserResultObj userResultObj = new UserResultObj()
            {
                Success = false,
                Message = Structs.Literals.DocumentNotRendered,
            };

            try
            {
                if (!string.IsNullOrWhiteSpace(requestTrackingID))
                {
                    userResultObj.Message = Functions.GetErrorMessageHtmlDocument(requestTrackingID).ToHtmlString();
                    userResultObj.Success = true;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsController, "DisplayErrorMessages", "Error", "requestTrackingID: " + requestTrackingID + ", " + ex.ToString());
            }

            return Json(userResultObj);
        }

        [AuthorizationRequired]
        public JsonResult MarkAsNotViewed(string transDataUUID, string docType)
        {
            UserResultObj userResultObj = new UserResultObj()
            {
                Success = false,
                DocType = docType,
            };

            try
            {
                if (!string.IsNullOrWhiteSpace(transDataUUID) && !string.IsNullOrWhiteSpace(docType))
                {
                    userResultObj.Success = lcapasLogic.MarkAsNotViewed(transDataUUID, docType);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsController, "MarkAsNotViewed", "Error", "transDataUUID: " + transDataUUID + ", docType: " + docType + ", " + ex.ToString());
            }

            return Json(userResultObj);
        }

        [AuthorizationRequired]
        public JsonResult SendtoColleagueTRRQ(string transDataUUID)
        {
            UserResultObj userResultObj = new UserResultObj()
            {
                Success = false,
            };

            try
            {
                if (!string.IsNullOrWhiteSpace(transDataUUID))
                {
                    // Check if Student has no sNumber in Colleague
                    if (lcapasLogic.CheckRequestStudentSNumber(transDataUUID))
                    {
                        // Send request to Colleague TRRQ
                        userResultObj.Success = colleagueLogic.SaveColleagueRequest(transDataUUID);
                    } else
                    {
                        // Student has no sNumber in Colleague: Redirect to the Student Lookup page using JQuery
                        userResultObj.Message = Structs.ErrorMessages.NoSNumberFound;
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsController, "SendtoColleagueTRRQ", "Error", "transDataUUID: " + transDataUUID + ", Error: " + ex.ToString());
            }

            return Json(userResultObj);
        }

        [AuthorizationRequired]
        public JsonResult PrintRequestResponseReport(string[] values, string type = null)
        {
            UserResultObj userResultModel = new UserResultObj()
            {
                Success = false,
                Message = Structs.Literals.ContactHelpDesk,
            };

            string htmlResult = string.Empty;

            Core.Library.Apas.CoreMain.DocumentTypeCodeType? docType = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(type))
                {
                    docType = type == Structs.DocumentType.Request ? Core.Library.Apas.CoreMain.DocumentTypeCodeType.Request : Core.Library.Apas.CoreMain.DocumentTypeCodeType.Response;

                    foreach (string value in values)
                    {
                        string htmlResultTemp = Functions.GetHtmlDocument(value, false, docType).ToHtmlString();

                        if (!string.IsNullOrWhiteSpace(htmlResultTemp))
                        {
                            htmlResult += "<div style='page-break-after: always;'>" + htmlResultTemp + "</div>";
                            if (docType == Core.Library.Apas.CoreMain.DocumentTypeCodeType.Request)
                            {
                                lcapasLogic.MarkRequestViewed(value);
                            } else
                            {
                                lcapasLogic.MarkResponseViewed(value);
                            }
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(htmlResult))
                    {
                        userResultModel.Success = true;
                        userResultModel.Message = htmlResult;
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsController, "PrintRequestResponseReport", "Error", ex.ToString());
            }

            return Json(userResultModel);
        }

        [AuthorizationRequired]
        public JsonResult SendUnsolicitedBatchTranscripts(string[] values, int? institutionId = null, bool allSelected = false, string filterFields = null)
        {
            UserResultObj userResultObj = new UserResultObj()
            {
                Success = false,
                Message = Structs.Literals.ContactHelpDesk,
            };

            try
            {
                if (values != null && values.Count() > 0)
                {
                    userResultObj.Success = lcapasLogic.SaveStudentsToBulkSendTranscript(values, institutionId, allSelected, filterFields);
                    if (userResultObj.Success)
                    {
                        userResultObj.Success = lcapasLogic.QueueSendBulkTranscript();
                    } else
                    {
                        userResultObj.Message = Structs.ErrorMessages.AlreadyQueuedUnsolicitedTranscriptBatch;
                    }
                } else
                {
                    userResultObj.Message = Structs.ErrorMessages.EmptyUnsolicitedTranscriptBatchSelection;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsController, "SendUnsolicitedBatchTranscripts", "Error", ex.ToString());
            }

            return Json(userResultObj);
        }

        [AllowAnonymous]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                lcapasLogic.Dispose();
                apasLogic.Dispose();
                colleagueLogic.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}