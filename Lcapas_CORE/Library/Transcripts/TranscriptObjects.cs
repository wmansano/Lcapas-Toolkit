using Lcapas.Core.Library.Apas.AcademicRecord;
using Lcapas.Core.Library.Apas.CoreMain;
using Lcapas.Core.Logic;
using Lcapas.Core.Models.Lcappsdb;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Lcapas.CollWebApi.DataContracts;
using Jitbit.Utils;

namespace Lcapas.Core.Library
{
    #region Complex Objects

    public class MessageListViewObj : IDisposable
    {
        #region private variables

        private bool _IsInitialized = false;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        private bool _StudentSelected;

        //private string _DestAction;
        private string _DestAction;

        private List<SelectListItem> _Institutions;
        private List<SelectListItem> _Statuses;
        private List<SelectListItem> _TransListTypes;
        private List<SelectListItem> _RespTransTypes;

        private PaginationFilter _PaginationFilter = new PaginationFilter();

        #endregion private variables

        #region constructors
        public MessageListViewObj()
        {
            _IsInitialized = true;
            LoadObject();
        }

        #endregion constructors

        #region methods
        private bool LoadObject()
        {
            bool success = false;
            try
            {
                if (_IsInitialized)
                {
                    if (_Institutions == null) { _Institutions = lcapasLogic.GetDestinations(); }
                    if (_Statuses == null) { _Statuses = lcapasLogic.GetStatuses(); }
                    if (_TransListTypes == null) { _TransListTypes = lcapasLogic.GetTransListTypes(); }
                    if (_RespTransTypes == null) { _RespTransTypes = lcapasLogic.GetRespTransTypes(); }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "Responses.Load", "Error", ex.ToString());
            }

            return success;
        }
        public void Dispose()
        {
            _IsInitialized = false;
            lcapasLogic.Dispose();
            this.Dispose();
        }

        #endregion methods

        #region public variables
        public bool IsInitialized;
        public bool StudentSelected { get { return _StudentSelected; } set { _StudentSelected = value; } }

        public string DestAction { get { return _DestAction; } set { _DestAction = value; } }

        public IEnumerable<SelectListItem> Institutions { get { return _Institutions; } }

        public IEnumerable<SelectListItem> Statuses { get { return _Statuses; } }

        public IEnumerable<SelectListItem> TransListTypes { get { return _TransListTypes; } }

        public IEnumerable<SelectListItem> RespTransTypes { get { return _RespTransTypes; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }

        #endregion public variables
    }

    public class RequestListViewObj : MessageListViewObj
    {
        #region private variables

        private LcapasLogic lcapasLogic = new LcapasLogic();

        private RequestSearchObj _SearchFilter;
        private List<RequestObj> _TranscriptRequests;
        private List<SelectListItem> _HoldTypes;

        #endregion private variables

        #region constructors
        public RequestListViewObj() {
            this.IsInitialized = true;
            LoadDefaultCollections();
        }

        public RequestListViewObj(ResponseSearchObj searchObj) {
            this.IsInitialized = true;
            _SearchFilter = new RequestSearchObj(searchObj.SelectedTransID);
            LoadDefaultCollections();
        }

        #endregion constructors

        #region methods
        public bool LoadObject(string destAction)
        {
            bool success = false;
            try
            {
                if (this.IsInitialized)
                {
                    this.DestAction = destAction;

                    if (_SearchFilter.SelectedStuID != null)
                    {
                        this.StudentSelected = true;
                    }
                    else {
                        _SearchFilter.LocalOrganizationIDs = new List<string>() {
                            lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                            lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PescApasCode),
                        };
                        _SearchFilter.Received = true;

                        if (!_HoldTypes.Any()) { _HoldTypes = lcapasLogic.GetHoldTypes(); }

                        success = lcapasLogic.LoadTransferRequests(this);
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "Requests.Load", "Error", ex.ToString());
            }

            return success;
        }

        public void LoadDefaultCollections() {
            if (this.IsInitialized) {
                if (_HoldTypes == null) { _HoldTypes = new List<SelectListItem>(); }
                if (_TranscriptRequests == null) { _TranscriptRequests = new List<RequestObj>(); }
                if (_SearchFilter == null) { _SearchFilter = new RequestSearchObj(); }
            }
        }

        #endregion methods

        #region public variables

        public RequestSearchObj SearchFilter { get { return _SearchFilter; } set { _SearchFilter = value; } }

        public List<RequestObj> TranscriptRequests { get { return _TranscriptRequests; } set { _TranscriptRequests = value; } }

        public IEnumerable<SelectListItem> Holdtypes { get { return _HoldTypes; } }

        #endregion public variables
    }

    public class ResponseListViewObj : MessageListViewObj
    {
        private LcapasLogic lcapasLogic = new LcapasLogic();

        private ResponseSearchObj _SearchFilter;
        private List<ResponseObj> _TranscriptResponses;

        private List<SelectListItem> _HoldReasonTypes;

        #region constructors
        public ResponseListViewObj() {
            this.IsInitialized = true;
            LoadDefaultCollections();
        }

        public ResponseListViewObj(RequestSearchObj searchObj)
        {
            this.IsInitialized = true;
            _SearchFilter = new ResponseSearchObj(searchObj.SelectedTransID);
            LoadDefaultCollections();
        }

        #endregion

        public void LoadObject(string destAction)
        {
            try
            {
                this.DestAction = destAction;

                if (this.IsInitialized)
                {
                    if (_SearchFilter.SelectedStuID != null)
                    {
                        this.StudentSelected = true;
                    }
                    else {
                        _SearchFilter.LocalOrganizationIDs = new List<string>(){
                            lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                            lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PescApasCode),
                        };

                        if (_HoldReasonTypes == null || !_HoldReasonTypes.Any()) { _HoldReasonTypes = lcapasLogic.GetHoldReasonTypes(); }

                        lcapasLogic.LoadTransferResponses(this);
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "Responses.Load", "Error", ex.ToString());
            }
        }

        public void LoadDefaultCollections()
        {
            if (this.IsInitialized)
            {
                if (_HoldReasonTypes == null) { _HoldReasonTypes = new List<SelectListItem>(); }
                if (_TranscriptResponses == null) { _TranscriptResponses = new List<ResponseObj>(); }
                if (_SearchFilter == null) { _SearchFilter = new ResponseSearchObj(); }
            }
        }

        public ResponseSearchObj SearchFilter { get { return _SearchFilter; } set { _SearchFilter = value; } }

        public List<ResponseObj> TranscriptResponses { get { return _TranscriptResponses; } set { _TranscriptResponses = value; } }

        public IEnumerable<SelectListItem> HoldReasonTypes { get { return _HoldReasonTypes; } }
    }

    public class UnsolicitedBatchListViewObj : MessageListViewObj
    {
        private LcapasLogic lcapasLogic = new LcapasLogic();
        private ColleagueLogic collLogic = new ColleagueLogic();
        
        private UnsolicitedBatchSearchObj _SearchFilter;
        private List<UnsolicitedBatchObj> _Students;

        private List<SelectListItem> _Programs;
        private List<SelectListItem> _Departments;
        private List<SelectListItem> _Terms;
        private List<SelectListItem> _DestinationInstitutions;

        private Institution _PreferredDestination;

        #region constructors
        public UnsolicitedBatchListViewObj()
        {
            this.IsInitialized = true;
            LoadDefaultCollections();
        }

        #endregion

        public void LoadObject()
        {
            try
            {
                if (this.IsInitialized)
                {
                    if (_Programs != null && _Programs.Count() <= 0) { _Programs = collLogic.GetColleagueProgramsWithTitle(); }
                    if (_Departments != null && _Departments.Count() <= 0) { _Departments = collLogic.GetColleagueDepartments(); }
                    if (_Terms != null && _Terms.Count() <= 0) { _Terms = collLogic.GetColleagueTerms(false, true); }

                    if (string.IsNullOrWhiteSpace(this.SearchFilter.DeptCode)) { this.SearchFilter.DeptCode = "NSG"; }  // Nursing Department
                    if (string.IsNullOrWhiteSpace(this.SearchFilter.Term)) { this.SearchFilter.Term = Functions.GetLcCurrentTerm(); }

                    // Get preferred destination institution
                    if (_PreferredDestination == null) { _PreferredDestination = lcapasLogic.GetInstByLOrgID("48009000"); }  // University of Lethbridge
                    if (_DestinationInstitutions != null && _DestinationInstitutions.Count() <= 0) { _DestinationInstitutions = lcapasLogic.GetDestinations(_PreferredDestination.InstitutionId.ToString()); }

                    collLogic.UnsolicitedBatchTranscriptReports(this);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "UnsolicitedBatchTranscript.Load", "Error", ex.ToString());
            }
        }

        public void LoadDefaultCollections()
        {
            if (this.IsInitialized)
            {
                if (_Students == null) { _Students = new List<UnsolicitedBatchObj>(); }
                if (_SearchFilter == null) { _SearchFilter = new UnsolicitedBatchSearchObj(); }
                if (_Programs == null) { _Programs = new List<SelectListItem>(); }
                if (_Departments == null) { _Departments = new List<SelectListItem>(); }
                if (_Terms == null) { _Terms = new List<SelectListItem>(); }
                if (_DestinationInstitutions == null) { _DestinationInstitutions = new List<SelectListItem>(); }
            }
        }

        public byte[] Export(string[] reportID, bool allSelected = false, string filterFields = null)
        {
            var csvFile = new CsvExport();

            try
            {
                List<UnsolicitedBatchObj> _UnsolicitedBatchList = new List<UnsolicitedBatchObj>();

                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    _UnsolicitedBatchList = collLogic.GetUnsolicitedBatchTranscriptReportByIds(reportID, allSelected, filterFields);
                }

                foreach (UnsolicitedBatchObj _UnsolicitedBatch in _UnsolicitedBatchList)
                {
                    csvFile.AddRow();

                    csvFile["Full Name"] = _UnsolicitedBatch.FullName;
                    csvFile["sNumber"] = _UnsolicitedBatch.sNumber;
                    csvFile["ASN"] = _UnsolicitedBatch.Asn;
                    csvFile["Program"] = _UnsolicitedBatch.ProgramCode + " - " + _UnsolicitedBatch.ProgramTitle;
                    csvFile["Department"] = _UnsolicitedBatch.DeptCode + " - " + _UnsolicitedBatch.DeptTitle;
                    csvFile["Terms"] = _UnsolicitedBatch.Term.ToString();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.TranscriptObject, "UnsolicitedBatchListViewObj.Export", "Error", ex.ToString());
            }

            return csvFile.ExportToBytes();
        }

        public UnsolicitedBatchSearchObj SearchFilter { get { return _SearchFilter; } set { _SearchFilter = value; } }

        public List<UnsolicitedBatchObj> Students { get { return _Students; } set { _Students = value; } }

        public List<SelectListItem> Programs { get { return _Programs; } set { _Programs = value; } }

        public List<SelectListItem> Departments { get { return _Departments; } set { _Departments = value; } }

        public List<SelectListItem> Terms { get { return _Terms; } set { _Terms = value; } }

        public List<SelectListItem> DestinationInstitutions { get { return _DestinationInstitutions; } set { _DestinationInstitutions = value; } }

        public new void Dispose()
        {
            IsInitialized = false;
            lcapasLogic.Dispose();
            collLogic.Dispose();
            this.Dispose();
        }
    }

    public class CreateMsgViewObj : IDisposable
    {
        #region Private variables
        private LcapasLogic lcapasLogic = new LcapasLogic();
        private ApasLogic apasLogic = new ApasLogic();
        private ColleagueLogic collLogic = new ColleagueLogic();

        private bool _IsInitialized;
        private bool _Ready;
        private bool _AutoRespond;
        private bool _StudentRestriction = false;
        private bool _StudentMissingASN = false;

        private List<string> _Uuids;
        private string _DocumentID;
        private int? _TransDataID;
        private string _TransmissionDataUUID;
        private string _TransactionTranscriptUuid;
        private string _RequestTrackingId;
        private DateTime _CreatedDateTime;

        private string _DestAction;

        private StudentRecordObj _StudentRecord;

        private List<SelectListItem> _Destinations;
        private Institution _PreferredDestination;

        private List<DestInstObj> _DestinationDetails = new List<DestInstObj>();

        private bool _DeliveryMethodSpecified;
        private DeliveryMethodType _DeliveryMethod = DeliveryMethodType.Electronic;

        private HoldTypeType _HoldTypeType = HoldTypeType.Now;
        private HoldReasonType? _HoldReasonType;
        private ResponseStatusType? _ResponseStatusType;

        private List<SelectListItem> _HoldTypes;
        private List<SelectListItem> _HoldReasonTypes;
        private List<SelectListItem> _ResponseStatusTypes;

        private string _HoldTypeDate;

        private bool _DocumentOfficialCodeSpecified;
        private DocumentOfficialCodeType _DocumentOfficialCode;

        private bool _RushProcessingRequested;
        private bool _TranscriptTypeSpecified;
        private bool _UpdateContactsInformationSpecified;
        private bool _ReleaseAuthorizedIndicator;
        private bool _PreviewMessageIndicator = false;
        private string _RecipientTrackingID;

        private bool _MyCredsMessage = false;
        private bool _UseMyCredsXsl = false;
        private List<string> _StudentRequestLogIDList;

        private TranscriptPurposeType _TranscriptPurpose;
        private TranscriptTypeType _TranscriptType;
        private DocumentTypeCodeType _DocumentTypeCode;
        private TransmissionTypeType _TransmissionType;
        private DocumentProcessCodeType _DocumentProcessCode;
        private DocumentCompleteCodeType _DocumentCompleteCode;
        private UpdateContactsInformationType _UpdateContactsInformation;
        private ReleaseAuthorizedMethodType _ReleaseAuthorizedMethod;
        private LocalOrganizationIDCodeQualifierType _LocalOrganizationIDCodeQualifier;

        #endregion Private variables

        #region Constructors

        public CreateMsgViewObj()
        {
            _IsInitialized = true;
            SetDefaultCollections();
        }

        public CreateMsgViewObj(SearchObj filter)
        {
            _IsInitialized = true;
            if (filter.StudentRecord != null) { _StudentRecord = filter.StudentRecord; }
            if (filter.DestAction != null) { _DestAction = filter.DestAction; }
            if (filter.SelectedTransID != null) { _TransDataID = filter.SelectedTransID; }
            if (!string.IsNullOrWhiteSpace(filter.TransactionTranscriptUuid)) { _TransactionTranscriptUuid = filter.TransactionTranscriptUuid; }
            SetDefaultCollections();
        }
        #endregion constructors

        #region Methods

        public bool LoadObject()
        {
            bool success = false;
            string _LocalOrganizationID = string.Empty;

            try
            {
                if (_IsInitialized)
                {
                    if (!string.IsNullOrWhiteSpace(_DestAction) && _StudentRecord != null)
                    {
                        _Ready = true;
                        _AutoRespond = lcapasLogic.GetSetting(Structs.SettingTypes.Boolean, Structs.Settings.AutoRespond);

                        // Check for Student Restriction
                        if (_DestAction == Structs.DestActions.CreateTranscript)
                        {
                            _StudentRestriction = collLogic.GetStudentRestriction(_StudentRecord.Snumber);
                        }

                        // Get preferred destination
                        if (_PreferredDestination == null)
                        {
                            _PreferredDestination = lcapasLogic.GetPreferredDestination();
                        }

                        if (_Destinations == null)
                        {
                            _Destinations = lcapasLogic.GetDestinations(_PreferredDestination.InstitutionId.ToString());
                        }

                        //if request exists, then we will respond to a single request source as destination
                        Request _Request = lcapasLogic.GetRequestByTransDataId(_TransDataID);

                        if (_DestinationDetails == null || _DestinationDetails.Count() == 0)
                        {
                            if (_Request != null)
                            {
                                _RequestTrackingId = _Request.TransmissionData.RequestTrackingID ?? "";
                                InstitutionObj _ReqDestination = new InstitutionObj()
                                {
                                    InstitutionID = _Request.TransmissionData.SourceInstitution.InstitutionId,
                                    InstitutionName = _Request.TransmissionData.SourceInstitution.InstitutionNames.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().Name,
                                };

                                _DestinationDetails.Add(new DestInstObj() { HoldTypeType = HoldTypeType.Now, ResponseStatusType = Apas.AcademicRecord.ResponseStatusType.NoRecord, Destination = _ReqDestination });
                            }
                            else
                            {
                                _DestinationDetails.Add(new DestInstObj() { HoldTypeType = HoldTypeType.Now, ResponseStatusType = Apas.AcademicRecord.ResponseStatusType.NoRecord, Destination = new InstitutionObj() { InstitutionID = this.PreferredDestination.InstitutionId, InstitutionName = this.PreferredDestination.InstitutionNames.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().Name } });
                            }
                        }

                        switch (_DestAction)
                        {
                            case Structs.DestActions.CreateRequest:
                                if (_HoldTypes == null || !_HoldTypes.Any())
                                {
                                    _HoldTypes = lcapasLogic.GetHoldTypes();
                                }
                                break;
                            case Structs.DestActions.CreateResponse:
                                if (_ResponseStatusTypes == null || !_ResponseStatusTypes.Any())
                                {
                                    _ResponseStatusTypes = lcapasLogic.GetStatuses();
                                }
                                break;
                            default:
                                if (_HoldReasonTypes == null || !_HoldReasonTypes.Any())
                                {
                                    _HoldReasonTypes = lcapasLogic.GetHoldReasonTypes();
                                }
                                break;
                        }

                        // CANNED TESTING RESPONSES
                        this.ResponseStatusType = lcapasLogic.GetAliasResponseType(this.StudentRecord);

                        // Check if Student is missing ASN number
                        _StudentMissingASN = string.IsNullOrWhiteSpace(_StudentRecord.ASN);
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "TranscriptRequest", "Error", ex.ToString());
            }

            return success;
        }

        private void SetDefaultCollections() {
            try
            {
                if (_IsInitialized) {

                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "SetDefaultCollections", "Error", ex.ToString());
            }
        }

        /// <summary>
        /// Preview
        /// </summary>
        /// <returns></returns>
        public bool GeneratePreview()
        {
            bool success = false;
            try
            {
                // Create XML
                switch (_DestAction)
                {
                    case Structs.DestActions.CreateRequest:
                        // Create Request XML, save Apas Message, parse message and return list of UUIDs
                        _TransmissionDataUUID = lcapasLogic.CreateApasRequest(this);
                        break;
                    case Structs.DestActions.CreateTranscript:
                        if (!string.IsNullOrWhiteSpace(_TransactionTranscriptUuid))
                        {
                            _TransmissionDataUUID = lcapasLogic.CreateColleagueTranscript(this);
                        }
                        break;
                    case Structs.DestActions.CreateResponse:
                        _TransmissionDataUUID = lcapasLogic.CreateApasResponse(this);
                        break;
                }
                success = !string.IsNullOrWhiteSpace(_TransmissionDataUUID);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "Preview", "Error", ex.ToString());
            }

            return success;
        }

        public bool Submit()
        {
            bool success = false;
            try
            {
                // Retrieve and send request generated at the preview stage
                if (string.IsNullOrWhiteSpace(_TransmissionDataUUID))
                {
                    // Call preview to generate Transmission Data (Request, Transcript or Response)
                    GeneratePreview();
                }

                // Get list of stored UUIDs
                _Uuids = lcapasLogic.GetKeyValueTempCache(_TransmissionDataUUID).Select(x => x.Value).ToList();

                switch (_DestAction)
                {
                    case Structs.DestActions.CreateRequest:
                        foreach (string apasMessageUUID in _Uuids)
                        {
                            if (!string.IsNullOrWhiteSpace(apasMessageUUID))
                            {
                                // queue new request to send
                                success = apasLogic.SendTransferRequest(apasMessageUUID);  // To show right way at the Admission Outbox (if queued the page has to be refreshed to show the new request)
                                                                                           //success = lcapasLogic.QueueJob(apasMessageUUID, Enums.JobTypeType.SendApasTransferRequest);

                                // We will be sending just Received Request to Colleague TRRQ (Records Request, it doesn't include Admission Sent Request)

                                //// Don't save Request Log in Colleague if it is an Everywhre Student
                                //if (!string.IsNullOrWhiteSpace(StudentRecord.Snumber))
                                //{
                                //if (success)  // Prepare TransactionRequestLog table for the next step to SaveColleagueRequest
                                //{
                                //    success = lcapasLogic.CreateRequestLog("PS", apasMessageUUID, this);
                                //}

                                //    if (success)  // Queue new request to save in Colleague (Request Log)
                                //    {
                                //        success = lcapasLogic.QueueJob(apasMessageUUID, Enums.JobTypeType.SaveColleagueRequest);
                                //    }
                                //}
                            }
                        }
                        break;
                    case Structs.DestActions.CreateResponse:
                    case Structs.DestActions.CreateTranscript:
                        foreach (string apasMessageUUID in _Uuids)
                        {
                            if (!string.IsNullOrWhiteSpace(apasMessageUUID))
                            {
                                // queue transcript to send
                                success = apasLogic.SendTransferResponse(apasMessageUUID);    // To show right way at the Admission Outbox (if queued the page has to be refreshed to show the new request)
                                //success = lcapasLogic.QueueJob(apasMessageUUID, Enums.JobTypeType.SendApasTransferResponse);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "Submit", "Error", ex.ToString());
            }

            return success;
        }

        public void Dispose()
        {
            IsInitialized = false;
            lcapasLogic.Dispose();
            collLogic.Dispose();
            apasLogic.Dispose();
            this.Dispose();
        }

        #endregion Methods

        #region Public Properties

        public bool IsInitialized;
        [Required(ErrorMessage = "Please Select")]
        [Display(Name = "Delivery Method")]
        [DataType(DataType.Text)]
        public DeliveryMethodType DeliveryMethod { get { return _DeliveryMethod; } set { _DeliveryMethod = value; } }
        public DocumentTypeCodeType DocumentTypeCode { get { return _DocumentTypeCode; } set { _DocumentTypeCode = value; } }
        public TransmissionTypeType TransmissionType { get { return _TransmissionType; } set { _TransmissionType = value; } }
        public DocumentProcessCodeType DocumentProcessCode { get { return _DocumentProcessCode; } set { _DocumentProcessCode = value; } }
        public DocumentOfficialCodeType DocumentOfficialCode { get { return _DocumentOfficialCode; } set { _DocumentOfficialCode = value; } }
        public DocumentCompleteCodeType DocumentCompleteCode { get { return _DocumentCompleteCode; } set { _DocumentCompleteCode = value; } }
        public bool UpdateContactsInformationSpecified { get { return _UpdateContactsInformationSpecified; } set { _UpdateContactsInformationSpecified = value; } }
        public UpdateContactsInformationType UpdateContactsInformation { get { return _UpdateContactsInformation; } set { _UpdateContactsInformation = value; } }
        public ReleaseAuthorizedMethodType ReleaseAuthorizedMethod { get { return _ReleaseAuthorizedMethod; } set { _ReleaseAuthorizedMethod = value; } }

        [Required(ErrorMessage = "Must be Authorized")]
        [Display(Name = "Authorized")]
        public bool ReleaseAuthorizedIndicator { get { return _ReleaseAuthorizedIndicator; } set { _ReleaseAuthorizedIndicator = value; } }

        public bool StudentRestriction { get { return _StudentRestriction; }  set { _StudentRestriction = value; } }

        public bool StudentMissingASN { get { return _StudentMissingASN; }  set { _StudentMissingASN = value; } }

        [Display(Name = "Preview")]
        public bool PreviewMessageIndicator { get { return _PreviewMessageIndicator; } set { _PreviewMessageIndicator = value; } }
        public bool DeliveryMethodSpecified { get { return _DeliveryMethodSpecified; } set { _DeliveryMethodSpecified = value; } }
        public bool DocumentOfficialCodeSpecified { get { return _DocumentOfficialCodeSpecified; } set { _DocumentOfficialCodeSpecified = value; } }
        public bool RushProcessingRequested { get { return _RushProcessingRequested; } set { _RushProcessingRequested = value; } }
        public string RecipientTrackingID { get { return _RecipientTrackingID; } set { _RecipientTrackingID = value; } }
        public bool TranscriptTypeSpecified { get { return _TranscriptTypeSpecified; } set { _TranscriptTypeSpecified = value; } }
        public TranscriptPurposeType TranscriptPurpose { get { return _TranscriptPurpose; } set { _TranscriptPurpose = value; } }
        public TranscriptTypeType TranscriptType { get { return _TranscriptType; } set { _TranscriptType = value; } }
        public LocalOrganizationIDCodeQualifierType LocalOrganizationIDCodeQualifier { get { return _LocalOrganizationIDCodeQualifier; } set { _LocalOrganizationIDCodeQualifier = value; } }

        public StudentRecordObj StudentRecord { get { return _StudentRecord; } set { _StudentRecord = value; } }

        public IEnumerable<SelectListItem> Destinations { get { return _Destinations; } }

        public IEnumerable<SelectListItem> HoldTypes { get { return _HoldTypes; } }

        public IEnumerable<SelectListItem> HoldReasonTypes { get { return _HoldReasonTypes; } }

        public IEnumerable<SelectListItem> ResponseStatusTypes { get { return _ResponseStatusTypes; } }

        public Institution PreferredDestination { get { return _PreferredDestination; } set { _PreferredDestination = value; } }

        public string DocumentID { get { return _DocumentID; } set { _DocumentID = value; } }

        public int? TransDataID { get { return _TransDataID; } set { _TransDataID = value; } }

        public string TransactionTranscriptUuid { get { return _TransactionTranscriptUuid; } set { _TransactionTranscriptUuid = value; } }

        public DateTime CreatedDateTime { get { return _CreatedDateTime; } set { _CreatedDateTime = value; } }

        public List<string> UUIDs { get { return _Uuids; } set { _Uuids = value; } }

        public string RequestTrackingId { get { return _RequestTrackingId; } set { _RequestTrackingId = value; } }

        public string TransmissionDataUUID { get { return _TransmissionDataUUID; } set { _TransmissionDataUUID = value; } }

        public string DestAction { get { return _DestAction; } set { _DestAction = value; } }

        public bool Ready { get { return _Ready; } set { _Ready = value; } }

        public bool AutoRespond { get { return _AutoRespond; } set { _AutoRespond = value; } }

        public bool MyCredsMessage { get { return _MyCredsMessage; } set { _MyCredsMessage = value; } }

        public bool useMyCredsXsl { get { return _UseMyCredsXsl; } set { _UseMyCredsXsl = value; } }
        
        public List<string> StudentRequestLogIDList { get { return _StudentRequestLogIDList; } set { _StudentRequestLogIDList = value; } }

        [Display(Name = "Destinations")]
        public List<DestInstObj> DestinationDetails { get { return _DestinationDetails; } set { _DestinationDetails = value; } }

        public HoldTypeType HoldTypeType { get { return _HoldTypeType; } set { _HoldTypeType = value; } }

        public HoldReasonType? HoldReasonType { get { return _HoldReasonType; } set { _HoldReasonType = value; } }

        public ResponseStatusType? ResponseStatusType { get { return _ResponseStatusType; } set { _ResponseStatusType = value; } }

        public string HoldTypeDate { get { return _HoldTypeDate; } set { _HoldTypeDate = value; } }

        #endregion Public Properties        
    }

    public class StuLookupListViewObj : IDisposable
    {
        #region private properties

        private bool _IsInitialized;

        //private string _Snumber;
        private bool _Redirect;
        private bool _NoDestAction;
        private SearchObj _SearchFilter = new SearchObj();
        private PaginationFilter _PaginationFilter = new PaginationFilter();
        private List<StudentRecordObj> _StudentRecords = new List<StudentRecordObj>();

        private LcapasLogic lcapasLogic = new LcapasLogic();
        private ColleagueLogic collLogic = new ColleagueLogic();

        #endregion private properties

        #region constructors
        public StuLookupListViewObj()
        {
            _IsInitialized = true;
        }

        public StuLookupListViewObj(SearchObj searchObj)
        {
            _IsInitialized = true;
            _SearchFilter = searchObj;
        }

        #endregion

        #region methods

        public void LoadObject()
        {
            try
            {
                // if coming from unsolicited 
                if (_IsInitialized)
                {
                    if (_SearchFilter != null)
                    {
                        Student _ParsedStudent = null;

                        // load local student if we have studentId (to populate the filter)
                        if (_SearchFilter.SelectedStuID != null)
                        {
                            _SearchFilter.StudentRecord.LoadLcapasDbStudent(_SearchFilter.SelectedStuID);
                        }

                        bool collegeData = false;

                        // if we have values (filter is populated)
                        if (_SearchFilter.StudentRecord != null && _SearchFilter.HasValues)
                        {
                            if (Functions.GetEnvironment() == Structs.Environment.Prod)
                            {
                                collegeData = collLogic.GetColleagueStudents(this);
                            } else
                            {
                                if (!lcapasLogic.GetAliasStudent(this))
                                {
                                    collegeData = collLogic.GetColleagueStudents(this);
                                }
                            }

                            // if we have results, ...exactly one result...
                            if (_StudentRecords != null && _StudentRecords.Count() == 1)
                            {
                                // set it to the filter
                                _SearchFilter.StudentRecord = _StudentRecords.FirstOrDefault();

                                if (_SearchFilter.SelectedStuID != null && _SearchFilter.StudentRecord.StudentId == null) {
                                    _SearchFilter.StudentRecord.StudentId = _SearchFilter.SelectedStuID;
                                }

                                // Redirect to create message
                                _Redirect = true;

                                // Parse Student Info
                                _ParsedStudent = lcapasLogic.ParseStudent(_SearchFilter.StudentRecord, collegeData);

                                // Save match sNumber to sNumber table
                                lcapasLogic.SaveStudentSNumber(_SearchFilter.SelectedStuID ?? _ParsedStudent.StudentId, _SearchFilter.StudentRecord.Snumber);
                            }

                            // Handle Response Types (Response & Transcript)
                            if (_SearchFilter.DestAction != Structs.DestActions.CreateRequest)
                            {
                                if (!string.IsNullOrWhiteSpace(_SearchFilter.StudentRecord.Snumber))
                                {
                                    switch (_SearchFilter.DestAction)
                                    {
                                        case Structs.DestActions.CreateTranscript:
                                            // Queue a job to kickoff an async call to colleague to generate a transcript
                                            _SearchFilter.TransactionTranscriptUuid = lcapasLogic.CreateQueueTransactionTranscript(_SearchFilter.TransactionTranscriptUuid, _SearchFilter.StudentRecord.Snumber);
                                            break;
                                        case Structs.DestActions.SaveColleagueRequestTRRQ:
                                            // Queue Job to Import Request (TRRQ) into Colleague
                                            //lcapasLogic.QueueJob(_Request.TransmissionData.Uuid, Enums.JobTypeType.SaveColleagueRequest);

                                            bool matchedRequestStudent = false;
                                            // Check if Matched Student is different from Requested Student
                                            if (!string.IsNullOrWhiteSpace(_SearchFilter.TransactionTranscriptUuid))
                                            {
                                                Request _Request = lcapasLogic.GetRequest(_SearchFilter.TransactionTranscriptUuid);
                                                if (_ParsedStudent != null && _ParsedStudent.StudentId != _Request.RequestedStudent.StudentId)
                                                {
                                                    // Save matched Student
                                                    matchedRequestStudent = lcapasLogic.SaveRequestMatchedStudent(_SearchFilter.TransactionTranscriptUuid, _ParsedStudent.StudentId);
                                                }

                                                // Check if Student has no sNumber in Colleague
                                                if (lcapasLogic.CheckRequestStudentSNumber(_SearchFilter.TransactionTranscriptUuid, matchedRequestStudent))
                                                {
                                                    // Send request to Colleague TRRQ
                                                    collLogic.SaveColleagueRequest(_SearchFilter.TransactionTranscriptUuid);
                                                }
                                            }
                                            break;
                                        case Structs.DestActions.ExportTranscriptToColleague:
                                            // Queue Job to Import Transcript Courses into Colleague
                                            //lcapasLogic.QueueJob(_Request.TransmissionData.Uuid, Enums.JobTypeType.SaveColleagueExtCourse);

                                            bool matchedResponseStudent = false;
                                            // Check if Matched Student is different from Requested Student
                                            if (!string.IsNullOrWhiteSpace(_SearchFilter.TransactionTranscriptUuid))
                                            {
                                                Response _Response = lcapasLogic.GetResponseByTransDataId(_SearchFilter.TransactionTranscriptUuid);
                                                if (_ParsedStudent != null && _Response != null && _Response.RequestedStudent != null && _ParsedStudent.StudentId != _Response.RequestedStudent.StudentId)
                                                {
                                                    // Save matched Student
                                                    matchedResponseStudent = lcapasLogic.SaveResponseMatchedStudent(_SearchFilter.TransactionTranscriptUuid, _ParsedStudent.StudentId);
                                                }

                                                // Check if Student has no sNumber in Colleague
                                                if (lcapasLogic.CheckResponseStudentSNumber(_SearchFilter.TransactionTranscriptUuid, matchedResponseStudent))
                                                {
                                                    // Send Transcript Courses to Colleague
                                                    //collLogic.ExportCourseToColleague(_SearchFilter.TransactionTranscriptUuid);
                                                    UserResultObj userResultObj = new UserResultObj();
                                                    List<string> transDataIdList = new List<string>() { _SearchFilter.TransactionTranscriptUuid };
                                                    lcapasLogic.ExportTranscriptsToColleague(transDataIdList, ref userResultObj, matchedResponseStudent);
                                                }
                                            }
                                            break;
                                    }

                                    //_SearchFilter.DestAction = Structs.DestActions.CreateTranscript;

                                    //if (_SearchFilter.TransactionTranscriptUuid != null && created)
                                    //{
                                    //    //lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "StudentLookup.Load", "Queue GetColleagueTranscript", "TransactionTranscriptUuid: " + _SearchFilter.TransactionTranscriptUuid + ", Snumber: " + _SearchFilter.StudentRecord.Snumber, false);
                                    //    lcapasLogic.QueueJob(_SearchFilter.TransactionTranscriptUuid, Enums.JobTypeType.GetColleagueTranscript);
                                    //}
                                }
                                else
                                {
                                    if (string.IsNullOrWhiteSpace(_SearchFilter.DestAction) || _SearchFilter.DestAction == Structs.DestActions.CreateTranscript)
                                    {
                                        _SearchFilter.DestAction = Structs.DestActions.CreateResponse;
                                    }
                                }
                            }
                        }
                    }
                    else {
                        _NoDestAction = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "StudentLookup.Load", "Error", ex.ToString());
            }
        }

        public void Dispose()
        {
            _IsInitialized = false;
            this.Dispose();
            collLogic.Dispose();
            lcapasLogic.Dispose();
        }
        #endregion methods

        #region public properties
        public bool isInitialized;

        public SearchObj SearchFilter { get { return _SearchFilter; } set { _SearchFilter = value; } }

        public List<StudentRecordObj> StudentRecords { get { return _StudentRecords; } set { _StudentRecords = value; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }

        public bool Redirect { get { return _Redirect; } set { _Redirect = value; } }

        public bool NoDestAction { get { return _NoDestAction; } }

        #endregion public properties

    }

    public class StudentRecordObj
    {
        #region private variables
        //private bool _IsInitialized;

        private string _sNumber;
        private int? _StudentId;

        private string _ASN = string.Empty;
        private DateTime? _BirthDate;

        private string _LastName = string.Empty;
        private string _FirstName = string.Empty;
        private string _MiddleName = string.Empty;
        private string _FullName = string.Empty;

        private string _City = string.Empty;
        private string _State = string.Empty;
        private string _Zip = string.Empty;
        private string _Country = string.Empty;
        private string _Addr1 = string.Empty;
        private string _Addr2 = string.Empty;
        private string _Phone = string.Empty;
        private string _Email = string.Empty;

        private string _Gender = string.Empty;
        private GenderCodeType? _GenderCode;

        private StuGradesObj _StudentGrades;
        private List<StuNameObj> _FormerNames = new List<StuNameObj>();

        private bool _HasValues = false;

        private LcapasLogic lcapasLogic = new LcapasLogic();
        private ColleagueLogic collLogic = new ColleagueLogic();

        #endregion private variables

        #region Constructors
        public StudentRecordObj()
        {
            //_IsInitialized = true;
        }

        //public StudentRecordObj(int? selectedStuId)
        //{
        //    _IsInitialized = true;
        //    if (selectedStuId != null) {
        //        _StudentId = selectedStuId;
        //    }
        //}


        #endregion Constructors

        #region methods
        private string GetGender(string gender)
        {
            string retval = string.Empty;
            switch (gender)
            {
                case "M":
                case "Male":
                    retval = "Male";
                    break;
                case "F":
                case "Female":
                    retval = "Female";
                    break;
                case "":
                default:
                    retval = "Unreported";
                    break;
            }

            return retval;
        }

        public bool LoadLcapasDbStudent(int? selectedStuId) {
            bool success = false;
            try
            {
                if (selectedStuId != null) {
                    // lookup student by id in Lcapasdb
                    var _dbStudent = lcapasLogic.GetStudent((Int32)selectedStuId);

                    // populate search filter
                    if (_dbStudent != null && _dbStudent.Person != null)
                    {
                        _StudentId = _dbStudent.StudentId;
                        if (_dbStudent.Person.BirthDate != null)
                        {
                            _BirthDate = _dbStudent.Person.BirthDate;
                        }
                        if (_dbStudent.Person.Names.Any())
                        {
                            PersonName personName = _dbStudent.Person.Names.Where(y => y.NameType == Structs.Name.PersonalNameType).OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                            _FirstName = personName.FirstName;
                            _LastName = personName.LastName;
                            _MiddleName = personName.MiddleNames;
                        }
                        if (_dbStudent.Person.Genders.Any())
                        {
                            Gender gender = _dbStudent.Person.Genders.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                            _Gender = gender.GenderCodeType.ToString();
                            _GenderCode = gender.GenderCodeType;
                        }
                        if (_dbStudent.ASNs.Any())
                        {
                            _ASN = _dbStudent.ASNs.Where(x => x.StateProvinceCode == StateProvinceCodeType.AB).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault().AgencyAssignedID;
                        }
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "LoadLcapasDbStudent", "Error", ex.ToString());
            }
            return success;
        }

        public bool LoadColleagueStudent() {
            bool success = false;
            try
            {
                this.collLogic.GetStudent(_sNumber);
                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "LoadColleagueStudent", "Error", ex.ToString());
            }
            return success;
        }

        private bool ParseFullName() {
            bool success = false;
            try
            {
                if (!string.IsNullOrWhiteSpace(_FullName))
                {
                    string[] _StudentNames = Functions.SeparateNames(_FullName);
                    string firstName = string.Empty; string middleName = string.Empty; string lastName = string.Empty;

                    int cnt = 0;
                    if (!string.IsNullOrWhiteSpace(_StudentNames[0])) { _FirstName = _StudentNames[0]; cnt++; }
                    if (!string.IsNullOrWhiteSpace(_StudentNames[1])) { _MiddleName = _StudentNames[1]; cnt++; }
                    if (!string.IsNullOrWhiteSpace(_StudentNames[2])) { _LastName = _StudentNames[2]; cnt++; }

                    success = true;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "ParseFullName", "Error", ex.ToString());
            }
            return success;
        }

        private void CheckValue(object value) {
            try
            {
                if (value != null)
                {
                    _HasValues = true;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "CheckValue", "Error", ex.ToString());
            }
        }
        #endregion

        #region public variables
        public bool isInitialized;

        [Display(Name = "sNumber")]
        //[StringLength(8, MinimumLength = 8)]
        //[RegularExpression("^[a-z]{1}[0-9]{7}$", ErrorMessage = "e.g. s1234567")]
        public string Snumber {
            get { return _sNumber; }
            set {
                _sNumber = Functions.CleanSnumber(value);
                _HasValues = true;
            }
        }

        [Display(Name = "StudentId")]
        public int? StudentId { get { return _StudentId; } set { _StudentId = value; CheckValue(value); } }

        //[Required(ErrorMessage = "Required (10 digits)")]
        [Display(Name = "ASN")]
        [MaxLength(10)]
        //[RegularExpression("[0-9]{10}$", ErrorMessage = "ASN is 10 digits")]
        public string ASN { get { return _ASN; } set { _ASN = value; _HasValues = true; } }

        //[Required(ErrorMessage = "yyyy/mm/dd")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get { return _BirthDate; } set { _BirthDate = value; _HasValues = true; } }

        //[Required(ErrorMessage = "Required")]
        [Display(Name = "Gender")]
        [DataType(DataType.Text)]
        public string Gender
        {
            get
            {
                return _Gender;
            }
            set
            {
                _Gender = GetGender(value);
                _GenderCode = (GenderCodeType)Enum.Parse(typeof(GenderCodeType), _Gender, true);
                CheckValue(value);
            }
        }

        [Display(Name = "Gender")]
        public GenderCodeType? GenderCode
        {
            get
            {
                return _GenderCode;
            }
            set
            {
                _GenderCode = value;
                _Gender = GetGender(value.ToString());
                CheckValue(value);
            }
        }

        //[Required(ErrorMessage = "Required")]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string LastName
        {
            get
            {
                return _LastName;
                //string retval = string.Empty;
                //if (_LastName != null) {
                //    retval = _LastName;
                //}
                //else if(_Name != null && _Name.LastName != null) {
                //    retval = _Name.LastName;
                //}
                //return retval;
            }
            set
            {
                //_Name.LastName = value;
                _LastName = value;
                CheckValue(value);
            }
        }

        //[Required(ErrorMessage = "Required")]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) {
                    _FirstName = value;
                    CheckValue(value);
                }
            }
        }

        //[Required(ErrorMessage = "*")]
        [Display(Name = "Middle Name")]
        [DataType(DataType.Text)]
        public string MiddleName
        {
            get
            {
                return _MiddleName;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _MiddleName = value;
                    CheckValue(value);
                }
            }
        }

        //[Required(ErrorMessage = "*")]
        [Display(Name = "Full Name")]
        [DataType(DataType.Text)]
        public string FullName
        {
            get
            {
                return _FullName;
            }
            set {
                if (!string.IsNullOrWhiteSpace(value)) {
                    _FullName = value;
                    ParseFullName();
                    CheckValue(value);
                }

            }
        }

        //[Required(ErrorMessage = "Required")]
        [Display(Name = "City")]
        [DataType(DataType.Text)]
        public string City { get { return _City; } set { _City = value; CheckValue(value); } }

        //[Required(ErrorMessage = "Required")]
        [Display(Name = "Province")]
        [DataType(DataType.Text)]
        public string State { get { return _State; } set { _State = value; CheckValue(value); } }

        //^([a-zA-Z]\d[a-zA-Z]\s?\d[a-zA-Z]\d)$
        [Required(ErrorMessage = "E.g. A9A9B9")]
        [RegularExpression("^([a-zA-Z]\\d[a-zA-Z]\\s?\\d[a-zA-Z]\\d)$", ErrorMessage = "Format: A9A9B9")]
        [Display(Name = "Postal Code")]
        [DataType(DataType.Text)]
        public string Zip { get { return _Zip; } set { _Zip = value; CheckValue(value); } }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Country")]
        [DataType(DataType.Text)]
        public string Country { get { return _Country; } set { _Country = value; CheckValue(value); } }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Address Line 1")]
        [DataType(DataType.Text)]
        public string Addr1 { get { return _Addr1; } set { _Addr1 = value; CheckValue(value); } }

        //[Required(ErrorMessage = "Address Line 2")]
        [Display(Name = "Address Line 2")]
        [DataType(DataType.Text)]
        public string Addr2 { get { return _Addr2; } set { _Addr2 = value; CheckValue(value); } }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Phone")]
        [RegularExpression("^(?:(?:\\+?1\\s*(?:[.-]\\s*)?)?(?:\\(\\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\\s*\\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\\s*(?:[.-]\\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\\s*(?:[.-]\\s*)?([0-9]{4})(?:\\s*(?:#|x\\.?|ext\\.?|extension)\\s*(\\d+))?$", ErrorMessage = "e.g. (999) 999-9999")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get { return _Phone; } set { _Phone = value; CheckValue(value); } }

        public string Email { get { return _Email; } set { _Email = value; } }

        public List<StuNameObj> FormerNames { get { return _FormerNames; } set { _FormerNames = value; } }

        public GenderCodeType Genders { get; set; }

        public StuGradesObj StudentGrades { get { return _StudentGrades; } set { _StudentGrades = value; } }

        public bool HasValues { get { return _HasValues; } set { CheckValue(value); } }

        #endregion public variables
    }

    public class RequestObj
    {
        #region private variables

        private LcapasLogic lcapasLogic = new LcapasLogic();

        //private bool _IsInitialized;
        private Request _Request;
        private ResponseStatusType? _ResponseStatus;
        private int? _ResponseCount;
        private int? _TranscriptCount;
        private int? _ErrorMessageCount;
        private int? _RequestOrder;
        private string _Operator;

        #endregion private variables

        #region constructors
        public RequestObj()
        {
            //_IsInitialized = true;
        }

        #endregion constructors

        #region methods
        //public bool LoadObject()
        //{
        //    bool success = false;
        //    try
        //    {
        //        if (_IsInitialized)
        //        {
        //            success = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "RequestObj.Load", "Error", ex.ToString());
        //    }
        //    return success;
        //}

        #endregion methods

        #region public variables

        public Request Request { get { return _Request; } set { _Request = value; } }

        public ResponseStatusType? ResponseStatus { get { return _ResponseStatus; } set { _ResponseStatus = value; } }

        public int? ResponseCount { get { return _ResponseCount; } set { _ResponseCount = value; } }

        public int? TranscriptCount { get { return _TranscriptCount; } set { _TranscriptCount = value; } }

        public int? ErrorMessageCount { get { return _ErrorMessageCount; } set { _ErrorMessageCount = value; } }

        public int? RequestOrder { get { return _RequestOrder; } set { _RequestOrder = value; } }

        public string Operator { get { return _Operator; } set { _Operator = value; } }

        #endregion public variables
    }

    public class ResponseObj
    {
        #region private variables

        private LcapasLogic lcapasLogic = new LcapasLogic();

        //private bool _IsInitialized;
        private Response _Response;
        private int? _RequestCount;
        private DateTime? _RequestSentToTRRQ;
        private string _RequestTransDataUUID;
        private int? _ErrorMessageCount;
        private int? _ResponseOrder;
        private string _Operator;

        #endregion private variables

        #region constructors
        public ResponseObj()
        {
            //_IsInitialized = true;
        }

        #endregion constructors

        #region methods
        //public bool LoadObject()
        //{
        //    bool success = false;
        //    try
        //    {
        //        if (_IsInitialized)
        //        {
        //            success = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "ResponseObj.Load", "Error", ex.ToString());
        //    }
        //    return success;
        //}

        #endregion methods

        #region public variables

        public Response Response { get { return _Response; } set { _Response = value; } }

        public int? RequestCount { get { return _RequestCount; } set { _RequestCount = value; } }

        public DateTime? RequestSentToTRRQ { get { return _RequestSentToTRRQ; } set { _RequestSentToTRRQ = value; } }

        public string RequestTransDataUUID { get { return _RequestTransDataUUID; } set { _RequestTransDataUUID = value; } }

        public int? ErrorMessageCount { get { return _ErrorMessageCount; } set { _ErrorMessageCount = value; } }

        public int? ResponseOrder { get { return _ResponseOrder; } set { _ResponseOrder = value; } }

        public string Operator { get { return _Operator; } set { _Operator = value; } }

        #endregion public variables
    }


    public class UnsolicitedBatchObj
    {
        #region private variables

        private string _sNumber;
        private string _FirstName;
        private string _LastName;
        private string _FullName;
        private string _Asn;
        private string _ProgramCode;
        private string _ProgramTitle;
        private string _DeptCode;
        private string _DeptTitle;
        private string _Term;

        #endregion private variables

        #region constructors
        public UnsolicitedBatchObj()
        {
            //_IsInitialized = true;
        }

        #endregion constructors

        #region public variables

        public string sNumber { get { return _sNumber; } set { _sNumber = value; } }

        public string FirstName { get { return _FirstName; } set { _FirstName = value; } }

        public string LastName { get { return _LastName; } set { _LastName = value; } }
        
        public string FullName { get { return _FullName; } set { _FullName = value; } }

        public string Asn { get { return _Asn; } set { _Asn = value; } }

        public string ProgramCode { get { return _ProgramCode; } set { _ProgramCode = value; } }

        public string ProgramTitle { get { return _ProgramTitle; } set { _ProgramTitle = value; } }

        public string DeptCode { get { return _DeptCode; } set { _DeptCode = value; } }

        public string DeptTitle { get { return _DeptTitle; } set { _DeptTitle = value; } }

        public string Term { get { return _Term; } set { _Term = value; } }

        #endregion public variables
    }

    #endregion Complex Objects

    #region Search Filters

    public class SearchObj : IDisposable
    {
        #region private properties

        private bool _IsInitialized;

        LcapasLogic _LcapasLogic = new LcapasLogic();

        private string _SelectedSnum;
        private int? _SelectedStuID;
        private int? _SelectedTransID;
        private string _TransactionTranscriptUuid;

        private bool _clearFilter = false;

        private bool _hasvalues = false;

        private string _DestAction;
        private string _FromUrlAction;

        private string _LocalOrganizationID = string.Empty;

        private int? _SourceInstitution;
        private int? _DestinationInstitution;

        private int? _Status;
        private string _Operator;
        private DateTime? _FromStatusDate;
        private DateTime? _ToStatusDate;

        private string _HoldTypeData = string.Empty;

        private Enums.TransListTypes _ListType = Enums.TransListTypes.All;
        private Enums.RespTransTypes _RespTransType = Enums.RespTransTypes.All;

        private StudentRecordObj _StudentRecord = new StudentRecordObj();

        #endregion

        #region constructors
        public SearchObj()
        {
            _IsInitialized = true;
        }
        #endregion End constructors

        #region methods

        public void ClearLookupFields() {
            _StudentRecord.LastName = string.Empty;
            _StudentRecord.FirstName = string.Empty;
            _StudentRecord.BirthDate = null;
            _StudentRecord.Gender = string.Empty;
            _StudentRecord.Snumber = string.Empty;


            _clearFilter = false;
        }

        public void CheckHasValues() {
            try
            {
                string values = _StudentRecord.Snumber + _StudentRecord.ASN + _StudentRecord.LastName + _StudentRecord.FirstName + _StudentRecord.MiddleName + _StudentRecord.FullName + _StudentRecord.BirthDate.ToString();

                _hasvalues = values != null && values.Trim().Length > 0;
            }
            catch (Exception)
            {
                
            }
        }

        public void Dispose()
        {
            _IsInitialized = false;
            _LcapasLogic.Dispose();
            this.Dispose();
        }

        #endregion end methods

        #region public properties 

        bool IsInitialized { get { return _IsInitialized; } set { _IsInitialized = value; } }

        //[Required(ErrorMessage = "*")]
        [Display(Name = "Received")]
        public bool Received { get; set; }

        //[Required(ErrorMessage = "*")]
        [Display(Name = "LocalOrganizationID")]
        [DataType(DataType.Text)]
        public List<string> LocalOrganizationIDs { get; set; }

        //[Required(ErrorMessage = "*")]
        [Display(Name = "Requesting Institution")]
        public int RequestingInstitution { get; set; }

        //[Required(ErrorMessage = "*")]
        [Display(Name = "Destination Organization")]
        public int? DestinationOrganization { get; set; }

        [Display(Name = "Hold Type Data")]
        [DataType(DataType.Text)]
        public string HoldTypeData { get; set; }

        [Display(Name = "Response Status")]
        public int? Status { get { return _Status; } set { _Status = value; } }

        [Display(Name = "Operator")]
        public string Operator { get { return _Operator; } set { _Operator = value; } }

        //[Required(ErrorMessage = "*")]
        [Display(Name = "From Status")]
        [DataType(DataType.Date)]
        public DateTime? FromStatusDate { get { return _FromStatusDate; } set { _FromStatusDate = value; } }

        //[Required(ErrorMessage = "*")]
        [Display(Name = "To Status")]
        [DataType(DataType.Date)]
        public DateTime? ToStatusDate { get { return _ToStatusDate; } set { _ToStatusDate = value; } }

        public StudentRecordObj StudentRecord { get { return _StudentRecord; } set { _StudentRecord = value; } }

        [Display(Name = "Colleague Status")]
        public Enums.TransListTypes ListType { get { return _ListType; } set { _ListType = value; } }

        public Enums.RespTransTypes RespTransType { get { return _RespTransType; } set { _RespTransType = value; } }

        public int? SelectedStuID { get { return _SelectedStuID; } set { _SelectedStuID = value; } }

        public int? SelectedTransID { get { return _SelectedTransID; } set { _SelectedTransID = value; } }

        public string TransactionTranscriptUuid { get { return _TransactionTranscriptUuid; } set { _TransactionTranscriptUuid = value; } }

        public bool ClearFilter { get { return _clearFilter; } set {            
                _clearFilter = value;
                if (_clearFilter) {
                    ClearLookupFields();
                }
            }
        }

        public bool HasValues { get { CheckHasValues(); return _hasvalues; } set { _hasvalues = value; } }

        public int? SourceInstitution { get { return _SourceInstitution; } set { _SourceInstitution = value; } }

        [Display(Name = "Destination Institution")]
        public int? DestinationInstitution { get { return _DestinationInstitution; } set { _DestinationInstitution = value; } }

        public string DestAction { get { return _DestAction; } set { _DestAction = value; } }

        public string FromUrlAction { get { return _FromUrlAction; } set { _FromUrlAction = value; } }

        public string SelectedSnum { get { return _SelectedSnum; } set { _SelectedSnum = value; } }
        #endregion public properties

    }

    public class RequestSearchObj : SearchObj
    {
        #region private variables

        private string _HoldType = string.Empty;
        private bool _SearchHasValues = false;

        #endregion private variables

        #region constructor
        public RequestSearchObj()
        {

        }
        public RequestSearchObj(int? transId)
        {
            if (transId != null)
            {
                this.SelectedTransID = transId;
            };
        }
        #endregion constructor

        #region methods

        public void CheckSearchHasValues()
        {
            try
            {
                string values = DestinationInstitution.ToString() + HoldType + HoldTypeData + Status + Operator + FromStatusDate.ToString() + ToStatusDate.ToString();

                _SearchHasValues = values != null && values.Trim().Length > 0 && ListType != Enums.TransListTypes.All;
            }
            catch (Exception)
            {

            }
        }
        #endregion methods

        #region public variables

        [Display(Name = "Hold Type")]
        public string HoldType { get; set; }

        public bool SearchHasValues { get { CheckSearchHasValues(); return _SearchHasValues; } set { _SearchHasValues = value; } }


        #endregion public variables
    }

    public class ResponseSearchObj : SearchObj
    {
        #region private variables
        private HoldReasonType? _HoldReasonType;

        #endregion private variables

        #region methods
        public ResponseSearchObj()
        {

        }

        public ResponseSearchObj(int? transId)
        {
            if (transId != null) {
                this.SelectedTransID = transId;
            };
        }
        #endregion methods

        #region public  variables

        //[Required(ErrorMessage = "*")]
        //[Display(Name = "Requesting Institution")]
        //public int RequestingInstitution { get { return _RequestingInstitution; } set { _RequestingInstitution = value; } }

        //[Required(ErrorMessage = "*")]
        //[Display(Name = "Destination Organization")]
        //public int DestinationOrganization { get { return _DestinationOrganization; } set { _DestinationOrganization = value; } }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Hold Type")]
        public HoldReasonType? HoldReasonType { get { return _HoldReasonType; } set { _HoldReasonType = value; } }

        #endregion public variables
    }

    public class UnsolicitedBatchSearchObj : SearchObj
    {
        #region private variables
        private string _ProgramCode;
        private string _ProgramTitle;
        private string _DeptCode;
        private string _DeptTitle;
        private string _Term;

        #endregion private variables

        #region methods
        public UnsolicitedBatchSearchObj()
        {

        }

        #endregion methods

        #region public  variables

        [Display(Name = "Program")]
        public string ProgramCode { get { return _ProgramCode; } set { _ProgramCode = value; } }
        public string ProgramTitle { get { return _ProgramTitle; } set { _ProgramTitle = value; } }
        
        [Display(Name = "Department")]
        public string DeptCode { get { return _DeptCode; } set { _DeptCode = value; } }
        public string DeptTitle { get { return _DeptTitle; } set { _DeptTitle = value; } }

        public string Term { get { return _Term; } set { _Term = value; } }

        #endregion public variables
    }

    #endregion Search Filters

    #region Simple Objects
    public class StuNameObj
    {
        private string _FirstName = string.Empty;
        private string _LastName = string.Empty;
        private string _MiddleName = string.Empty;
        //[Required(ErrorMessage = "*")]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get { return _FirstName; } set { _FirstName = value; } }

        //[Required(ErrorMessage = "*")]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get { return _LastName; } set { _LastName = value; } }

        //[Required(ErrorMessage = "*")]
        [Display(Name = "Middle Name")]
        [DataType(DataType.Text)]
        public string MiddleName { get { return _MiddleName; } set { _MiddleName = value; } }
    }

    public class StuGradesObj : IDisposable
    {
        public bool isInitialized;
        public StuGradesObj()
        {
            isInitialized = true;
        }

        #region public properties
        public string STU_ID { get; set; }
        public string CRS_CODE { get; set; }
        public string CRS_TERM { get; set; }
        public string CRS_TITLE { get; set; }
        public decimal? CRS_ATT_CRD { get; set; }
        public decimal? CRS_CMPL_CRD { get; set; }
        public decimal? CRS_GPA_CRD { get; set; }
        public string CRS_GRADE { get; set; }
        public decimal? CRS_GRDPT { get; set; }
        public decimal? TRM_CRD { get; set; }
        public string TRM_STAND { get; set; }
        public decimal? TRM_GPA { get; set; }
        public DateTime CRS_END_DATE { get; set; }
        public string CRS_ACAD_LEVEL { get; set; }

        #endregion public properties

        public void Dispose()
        {
            isInitialized = false;
            this.Dispose();
        }

    }

    public class InstitutionObj
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Institution ID")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int InstitutionID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Institution Name")]
        [DataType(DataType.Text)]
        public string InstitutionName { get; set; }
    }

    public class DestInstObj
    {
        public InstitutionObj Destination { get; set; }

        [Display(Name = "Hold Type")]
        public HoldTypeType HoldTypeType { get; set; }

        [Display(Name = "Hold Reason Type")]
        public HoldReasonType? HoldReasonType { get; set; }

        [Display(Name = "Response Status Type")]
        public ResponseStatusType? ResponseStatusType { get; set; }

        [Display(Name = "Hold Type Data")]
        public string HoldTypeData { get; set; }
    }

    public class CourseExportObj {
        public int CourseId { get; set; }

        public string sNumber { get; set; }

        public string RequestTrackingID { get; set; }

        public ContractRequest_InsertExtCrs Course { get; set; }
    }

    #endregion Simple Objects
}