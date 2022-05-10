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

    public class TRRQRequestListViewObj : MessageListViewObj
    {
        #region private variables

        private LcapasLogic lcapasLogic = new LcapasLogic();
        private ColleagueLogic collLogic = new ColleagueLogic();

        private TRRQRequestSearchObj _SearchFilter;
        private List<TRRQRequestObj> _TranscriptRequests;
        private List<SelectListItem> _HoldTypes;

        #endregion private variables

        #region constructors
        public TRRQRequestListViewObj() {
            this.IsInitialized = true;
            LoadDefaultCollections();
        }

        #endregion constructors

        #region methods
        public bool LoadObject()
        {
            bool success = false;
            try
            {
                if (this.IsInitialized)
                {
                    if (!_HoldTypes.Any()) { _HoldTypes = collLogic.GetColleagueHoldTypes(); }
                    
                    if (SearchFilter.FromRequestDate == null) { SearchFilter.FromRequestDate = DateTime.Now.AddDays(-1); }
                    if (SearchFilter.ToRequestDate == null) { SearchFilter.ToRequestDate = DateTime.Now; }

                    success = collLogic.LoadTRRQRequests(this);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.MyCredsObject, "TRRQRequests.Load", "Error", ex.ToString());
            }

            return success;
        }

        public void LoadDefaultCollections() {
            if (this.IsInitialized) {
                if (_HoldTypes == null) { _HoldTypes = new List<SelectListItem>(); }
                if (_TranscriptRequests == null) { _TranscriptRequests = new List<TRRQRequestObj>(); }
                if (_SearchFilter == null) { _SearchFilter = new TRRQRequestSearchObj(); }
            }
        }

        public byte[] Export(string[] reportID, bool allSelected = false, string filterFields = null)
        {
            var csvFile = new CsvExport();

            try
            {
                List<TRRQRequestObj> _TRRQRequestBatchList = new List<TRRQRequestObj>();

                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    _TRRQRequestBatchList = collLogic.GetMyCredsRequestReportByIds(reportID, allSelected, filterFields);
                }

                foreach (TRRQRequestObj _TRRQRequestBatch in _TRRQRequestBatchList)
                {
                    csvFile.AddRow();

                    csvFile["Request ID"] = _TRRQRequestBatch.RequestID;
                    csvFile["Full Name"] = _TRRQRequestBatch.FullName;
                    csvFile["sNumber"] = _TRRQRequestBatch.sNumber;
                    csvFile["Email"] = _TRRQRequestBatch.Email;
                    csvFile["Request Date"] = _TRRQRequestBatch.RequestDate;
                    csvFile["Hold Type"] = _TRRQRequestBatch.HoldType;
                    csvFile["Date Produced"] = _TRRQRequestBatch.DateProduced;
                    csvFile["Operator"] = _TRRQRequestBatch.Operator;
                    csvFile["Comments"] = (_TRRQRequestBatch.Comments ?? "").ToString().Replace('ý', ' ').EdiASCISafe();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.TranscriptObject, "TRRQRequestListViewObj.Export", "Error", ex.ToString());
            }

            return csvFile.ExportToBytes();
        }

        #endregion methods

        #region public variables

        public TRRQRequestSearchObj SearchFilter { get { return _SearchFilter; } set { _SearchFilter = value; } }

        public List<TRRQRequestObj> TranscriptRequests { get { return _TranscriptRequests; } set { _TranscriptRequests = value; } }

        public IEnumerable<SelectListItem> Holdtypes { get { return _HoldTypes; } }

        #endregion public variables
        public new void Dispose()
        {
            IsInitialized = false;
            lcapasLogic.Dispose();
            collLogic.Dispose();
            this.Dispose();
        }
    }

    public class TRRQRequestObj
    {
        #region private variables

        private string _RequestID;
        private string _sNumber;
        private string _FirstName;
        private string _LastName;
        private string _FullName;
        private string _Email;
        //private string _InstitutionName;
        private string _HoldType;
        private DateTime? _RequestDate;
        private DateTime? _DateProduced;
        private string _Operator;
        private string _Comments;
        private string _Restriction;
        private int? _AcadCoursesCount;

        #endregion private variables

        #region constructors
        public TRRQRequestObj()
        {

        }

        #endregion constructors

        #region methods

        #endregion methods

        #region public variables

        public string RequestID { get { return _RequestID; } set { _RequestID = value; } }

        public string sNumber { get { return _sNumber; } set { _sNumber = value; } }

        public string FirstName { get { return _FirstName; } set { _FirstName = value; } }

        public string LastName { get { return _LastName; } set { _LastName = value; } }

        public string FullName { get { return _FullName; } set { _FullName = value; } }
        
        public string Email { get { return _Email; } set { _Email = value; } }

        //public string InstitutionName { get { return _InstitutionName; } set { _InstitutionName = value; } }

        public string HoldType { get { return _HoldType; } set { _HoldType = value; } }

        public DateTime? RequestDate { get { return _RequestDate; } set { _RequestDate = value; } }

        public DateTime? DateProduced { get { return _DateProduced; } set { _DateProduced = value; } }
        
        public string Operator { get { return _Operator; } set { _Operator = value; } }
        
        public string Comments { get { return _Comments; } set { _Comments = value; } }

        public string Restriction { get { return _Restriction; } set { _Restriction = value; } }
        
        public int? AcadCoursesCount { get { return _AcadCoursesCount; } set { _AcadCoursesCount = value; } }

        public bool InvalidEmail { get { return string.IsNullOrWhiteSpace(_Email) || 
                                              (!string.IsNullOrWhiteSpace(_Email) && (_Email.Trim().ToUpper().Contains(Structs.Email.LethbridgeCollegeEmailType.ToUpper())) || !_Email.Contains("@") || !_Email.Contains(".")); } }

        #endregion public variables
    }

    public class BulkSendListViewObj : MessageListViewObj
    {
        #region private variables

        private LcapasLogic lcapasLogic = new LcapasLogic();
        private ColleagueLogic collLogic = new ColleagueLogic();

        private SendBulkSearchObj _SearchFilter;
        private List<StudentObj> _Students;
        private List<SelectListItem> _Ethnic;
        private List<SelectListItem> _AlienStatus;
        private List<SelectListItem> _ProgramCode;
        private List<SelectListItem> _Campus;
        private List<SelectListItem> _CDDType;
        private List<SelectListItem> _Honors;

        #endregion private variables

        #region constructors
        public BulkSendListViewObj()
        {
            this.IsInitialized = true;
            LoadDefaultCollections();
        }

        #endregion constructors

        #region methods
        public bool LoadObject()
        {
            bool success = false;
            try
            {
                if (this.IsInitialized)
                {
                    if (!_Ethnic.Any()) { _Ethnic = collLogic.GetColleagueEthnic(); }
                    if (!_AlienStatus.Any()) { _AlienStatus = collLogic.GetColleagueAlienStatus(); }
                    if (!_ProgramCode.Any()) { _ProgramCode = collLogic.GetColleagueProgramsWithTitle(); }
                    if (!_Campus.Any()) { _Campus = collLogic.GetColleagueAcadCredLocations(true); }
                    if (!_CDDType.Any()) { _CDDType = collLogic.GetColleagueAcadCredCDDType(true); }
                    if (!_Honors.Any()) { _Honors = collLogic.GetColleagueAcadCredHonors(true); }

                    if (SearchFilter.FromAcadCCDDate == null) { SearchFilter.FromAcadCCDDate = DateTime.Now.AddDays(-30); }
                    if (SearchFilter.ToAcadCCDDate == null) { SearchFilter.ToAcadCCDDate = DateTime.Now; }

                    success = collLogic.LoadBulkSendStudents(this);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.MyCredsObject, "BulkSend.Load", "Error", ex.ToString());
            }

            return success;
        }

        public void LoadDefaultCollections()
        {
            if (this.IsInitialized)
            {
                if (_Ethnic == null) { _Ethnic = new List<SelectListItem>(); }
                if (_AlienStatus == null) { _AlienStatus = new List<SelectListItem>(); }
                if (_ProgramCode == null) { _ProgramCode = new List<SelectListItem>(); }
                if (_Campus == null) { _Campus = new List<SelectListItem>(); }
                if (_CDDType == null) { _CDDType = new List<SelectListItem>(); }
                if (_Honors == null) { _Honors = new List<SelectListItem>(); }
                if (_Students == null) { _Students = new List<StudentObj>(); }
                if (_SearchFilter == null) { _SearchFilter = new SendBulkSearchObj(); }
            }
        }

        public byte[] Export(string[] reportID, bool allSelected = false, string filterFields = null)
        {
            var csvFile = new CsvExport();

            try
            {
                List<StudentObj> _StudentObjList = new List<StudentObj>();

                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    _StudentObjList = collLogic.GetMyCredsBulkSendReportByIds(reportID, allSelected, filterFields);
                }

                foreach (StudentObj _StudentObj in _StudentObjList)
                {
                    csvFile.AddRow();
                    csvFile["Acad Cred ID"] = _StudentObj.AcadCredentialsID;
                    csvFile["Full Name"] = _StudentObj.FullName;
                    csvFile["sNumber"] = _StudentObj.sNumber;
                    csvFile["Email"] = _StudentObj.Email;
                    csvFile["Ethnic"] = _StudentObj.Ethnic;
                    csvFile["Alien Status"] = _StudentObj.AlienStatus;
                    csvFile["Program"] = _StudentObj.ProgramCode + " - " + _StudentObj.ProgramDesc;
                    csvFile["Campus"] = _StudentObj.Campus;
                    csvFile["CCDType"] = _StudentObj.CCDType;
                    csvFile["Honors"] = _StudentObj.AcadHonors;
                    csvFile["GPA"] = _StudentObj.AcadGPA;
                    csvFile["Acad CCD Date"] = _StudentObj.AcadCCDDate;
                    csvFile["Acad Add Date"] = _StudentObj.AcadCredAddDate;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.TranscriptObject, "TRRQRequestListViewObj.Export", "Error", ex.ToString());
            }

            return csvFile.ExportToBytes();
        }

        #endregion methods

        #region public variables

        public SendBulkSearchObj SearchFilter { get { return _SearchFilter; } set { _SearchFilter = value; } }

        public List<StudentObj> Students { get { return _Students; } set { _Students = value; } }

        public IEnumerable<SelectListItem> Ethnic { get { return _Ethnic; } }
        public IEnumerable<SelectListItem> AlienStatus { get { return _AlienStatus; } }
        public IEnumerable<SelectListItem> ProgramCode { get { return _ProgramCode; } }
        public IEnumerable<SelectListItem> Campus { get { return _Campus; } }
        public IEnumerable<SelectListItem> CDDType { get { return _CDDType; } }
        public IEnumerable<SelectListItem> Honors { get { return _Honors; } }

        #endregion public variables
        public new void Dispose()
        {
            IsInitialized = false;
            lcapasLogic.Dispose();
            collLogic.Dispose();
            this.Dispose();
        }
    }

    public class StudentObj
    {
        #region private variables

        private string _AcadCredentialsID;
        private string _sNumber;
        private string _FirstName;
        private string _LastName;
        private string _FullName;
        private string _Email;
        private string _Ethnic;
        private string _AlienStatus;
        private DateTime? _AcadCredAddDate;
        private DateTime? _AcadCCDDate;
        private string _ProgramCode;
        private string _ProgramDesc;
        private string _Campus;
        private string _CCDType;
        private string _AcadHonors;
        private decimal? _AcadGPA;
        private string _Restriction;
        private int? _AcadCoursesCount;

        #endregion private variables

        #region constructors
        public StudentObj()
        {

        }

        #endregion constructors

        #region methods

        #endregion methods

        #region public variables

        public string AcadCredentialsID { get { return _AcadCredentialsID; } set { _AcadCredentialsID = value; } }

        public string sNumber { get { return _sNumber; } set { _sNumber = value; } }

        public string FirstName { get { return _FirstName; } set { _FirstName = value; } }

        public string LastName { get { return _LastName; } set { _LastName = value; } }

        public string FullName { get { return _FullName; } set { _FullName = value; } }
        
        public string Email { get { return _Email; } set { _Email = value; } }

        public string Ethnic { get { return _Ethnic; } set { _Ethnic = value; } }

        public string AlienStatus { get { return _AlienStatus; } set { _AlienStatus = value; } }

        public DateTime? AcadCredAddDate { get { return _AcadCredAddDate; } set { _AcadCredAddDate = value; } }
        
        public DateTime? AcadCCDDate { get { return _AcadCCDDate; } set { _AcadCCDDate = value; } }

        public string ProgramCode { get { return _ProgramCode; } set { _ProgramCode = value; } }
        
        public string ProgramDesc { get { return _ProgramDesc; } set { _ProgramDesc = value; } }

        public string Campus { get { return _Campus; } set { _Campus = value; } }

        public string CCDType { get { return _CCDType; } set { _CCDType = value; } }

        public string AcadHonors { get { return _AcadHonors; } set { _AcadHonors = value; } }
        
        public decimal? AcadGPA { get { return _AcadGPA; } set { _AcadGPA = value; } }

        public string Restriction { get { return _Restriction; } set { _Restriction = value; } }

        public int? AcadCoursesCount { get { return _AcadCoursesCount; } set { _AcadCoursesCount = value; } }
        
        public bool InvalidEmail
        {
            get
            {
                return string.IsNullOrWhiteSpace(_Email) ||
                     (!string.IsNullOrWhiteSpace(_Email) && (_Email.Trim().ToUpper().Contains(Structs.Email.LethbridgeCollegeEmailType.ToUpper())) || !_Email.Contains("@") || !_Email.Contains("."));
            }
        }

        #endregion public variables
    }

    #endregion Complex Objects

    #region Simple Objects

    public class TRRQRequestSearchObj : SearchObj
    {
        #region private variables

        private string _RequestID;
        private string _HoldType;
        //private string _TRRQRequestingInstitution;
        private DateTime? _FromRequestDate;
        private DateTime? _ToRequestDate;
        private DateTime? _FromDateProduced;
        private DateTime? _ToDateProduced;
        private string _Comments;
        private bool _SearchHasValues = false;

        #endregion private variables

        #region constructor
        public TRRQRequestSearchObj()
        {

        }
        #endregion constructor

        #region methods

        public void CheckSearchHasValues()
        {
            try
            {
                string values = RequestID + HoldType + /*TRRQRequestingInstitution +*/ Comments + FromRequestDate.ToString() + FromDateProduced.ToString();

                _SearchHasValues = values != null && values.Trim().Length > 0;
            }
            catch (Exception)
            {

            }
        }
        #endregion methods

        #region public variables

        [Display(Name = "Request ID")]
        public string RequestID { get { return _RequestID; } set { _RequestID = value; } }

        [Display(Name = "Hold Type")]
        public string HoldType { get { return _HoldType; } set { _HoldType = value; } }

        //public string TRRQRequestingInstitution { get { return _TRRQRequestingInstitution; } set { _TRRQRequestingInstitution = value; } }

        [Display(Name = "Request From")]
        public DateTime? FromRequestDate { get { return _FromRequestDate; } set { _FromRequestDate = value; } }

        [Display(Name = "Request To")]
        public DateTime? ToRequestDate { get { return _ToRequestDate; } set { _ToRequestDate = value; } }

        [Display(Name = "Produced From")]
        public DateTime? FromDateProduced { get { return _FromDateProduced; } set { _FromDateProduced = value; } }

        [Display(Name = "Produced To")]
        public DateTime? ToDateProduced { get { return _ToDateProduced; } set { _ToDateProduced = value; } }

        [Display(Name = "Comments")]
        public string Comments { get { return _Comments; } set { _Comments = value; } }

        public bool SearchHasValues { get { CheckSearchHasValues(); return _SearchHasValues; } set { _SearchHasValues = value; } }


        #endregion public variables
    }

    public class SendBulkSearchObj : SearchObj
    {
        #region private variables

        private string _AcadCredentialsID;
        private List<string> _Ethnic;
        private List<string> _AlienStatus;
        private List<string> _ProgramCode;
        private string _ProgramDesc;
        private List<string> _Campus;
        private List<string> _CCDType;
        private List<string> _AcadHonors;
        private string _AcadGPA;
        private DateTime? _FromAcadCCDDate;
        private DateTime? _ToAcadCCDDate;
        private DateTime? _FromAcadCredAddDate;
        private DateTime? _ToAcadCredAddDate;
        private bool _SearchHasValues = false;

        #endregion private variables

        #region constructor
        public SendBulkSearchObj()
        {

        }
        #endregion constructor

        #region methods

        public void CheckSearchHasValues()
        {
            try
            {
                string values = AcadCredentialsID + Ethnic + AlienStatus + ProgramCode + ProgramDesc + Campus + FromAcadCredAddDate.ToString() + ToAcadCredAddDate.ToString() +
                                FromAcadCCDDate.ToString() + ToAcadCCDDate.ToString() + CCDType + AcadHonors + AcadGPA;

                _SearchHasValues = values != null && values.Trim().Length > 0;
            }
            catch (Exception)
            {

            }
        }
        #endregion methods

        #region public variables

        [Display(Name = "Acad Cred ID")]
        public string AcadCredentialsID { get { return _AcadCredentialsID; } set { _AcadCredentialsID = value; } }

        [Display(Name = "Ethnic")]
        public List<string>Ethnic { get { return _Ethnic; } set { _Ethnic = value; } }

        [Display(Name = "Alien Status")]
        public List<string> AlienStatus { get { return _AlienStatus; } set { _AlienStatus = value; } }

        [Display(Name = "Program")]
        public List<string> ProgramCode { get { return _ProgramCode; } set { _ProgramCode = value; } }

        [Display(Name = "Program Name")]
        public string ProgramDesc { get { return _ProgramDesc; } set { _ProgramDesc = value; } }

        [Display(Name = "Campus")]
        public List<string> Campus { get { return _Campus; } set { _Campus = value; } }

        [Display(Name = "CCD Type")]
        public List<string> CCDType { get { return _CCDType; } set { _CCDType = value; } }

        [Display(Name = "Honors")]
        public List<string> AcadHonors { get { return _AcadHonors; } set { _AcadHonors = value; } }

        [Display(Name = "GPA")]
        public string AcadGPA { get { return _AcadGPA; } set { _AcadGPA = value; } }

        [Display(Name = "From CCD Date")]
        public DateTime? FromAcadCCDDate { get { return _FromAcadCCDDate; } set { _FromAcadCCDDate = value; } }

        [Display(Name = "To CCD Date")]
        public DateTime? ToAcadCCDDate { get { return _ToAcadCCDDate; } set { _ToAcadCCDDate = value; } }

        [Display(Name = "From Add Date")]
        public DateTime? FromAcadCredAddDate { get { return _FromAcadCredAddDate; } set { _FromAcadCredAddDate = value; } }

        [Display(Name = "To Add Date")]
        public DateTime? ToAcadCredAddDate { get { return _ToAcadCredAddDate; } set { _ToAcadCredAddDate = value; } }

        public bool SearchHasValues { get { CheckSearchHasValues(); return _SearchHasValues; } set { _SearchHasValues = value; } }


        #endregion public variables
    }


    public class CsvBatchObj
    {
        #region private variables

        private string _sNumber;
        private string _FullName;
        private string _Email;
        private string _DocumentType;
        private string _BatchCode;
        private string _FileName;

        #endregion private variables

        #region constructors
        public CsvBatchObj()
        {

        }

        #endregion constructors

        #region methods

        #endregion methods

        #region public variables


        public string sNumber { get { return _sNumber; } set { _sNumber = value; } }

        public string FullName { get { return _FullName; } set { _FullName = value; } }
        
        public string Email { get { return _Email; } set { _Email = value; } }

        public string DocumentType { get { return _DocumentType; } set { _DocumentType = value; } }

        public string BatchCode { get { return _BatchCode; } set { _BatchCode = value; } }

        public string FileName { get { return _FileName; } set { _FileName = value; } }

        #endregion public variables
    }

    public class MyCredsTransactionObj
    {
        #region private variables

        private string _UUID;
        private string _sNumber;
        private string _FullName;
        private List<string> _RequestIdList;
        private string _BatchCode;
        private string _TransGroup = "PS";
        private string _AddressLine;
        private string _City;
        private string _State;
        private string _PostalCode;
        private string _Country;
        private string _FileName;
        private string _Email;
        private string _XmlContent;
        private DateTime? _RequestedDate;
        private DateTime? _ProducedDate;

        #endregion private variables

        #region constructors
        public MyCredsTransactionObj()
        {

        }

        #endregion constructors

        #region methods

        #endregion methods

        #region public variables

        public string UUID { get { return _UUID; } set { _UUID = value; } }

        public string sNumber { get { return _sNumber; } set { _sNumber = value; } }
        
        public string FullName { get { return _FullName; } set { _FullName = value; } }

        public List<string> RequestIdList { get { return _RequestIdList; } set { _RequestIdList = value; } }
        
        public string BatchCode { get { return _BatchCode; } set { _BatchCode = value; } }
        
        public string TransGroup { get { return _TransGroup; } set { _TransGroup = value; } }
        
        public string AddressLine { get { return _AddressLine; } set { _AddressLine = value; } }
                
        public string City { get { return _City; } set { _City = value; } }
        
        public string State { get { return _State; } set { _State = value; } }
        
        public string PostalCode { get { return _PostalCode; } set { _PostalCode = value; } }
        
        public string Country { get { return _Country; } set { _Country = value; } }
        
        public string FileName { get { return _FileName; } set { _FileName = value; } }
        
        public string Email { get { return _Email; } set { _Email = value; } }
        
        public string XmlContent { get { return _XmlContent; } set { _XmlContent = value; } }

        public DateTime? RequestedDate { get { return _RequestedDate; } set { _RequestedDate = value; } }
        
        public DateTime? ProducedDate { get { return _ProducedDate; } set { _ProducedDate = value; } }

        #endregion public variables
    }

    public class RequestBodyInitialLoginObj
    {
        #region public variables

        public string idp { get; set; }

        public string type { get; set; }

        public string value { get; set; }

        #endregion public variables
    }

    public class RequestBodyUserObj
    {
        #region public variables

        public string id { get; set; }

        public string full_name { get; set; }

        public string email { get; set; }

        public RequestBodyInitialLoginObj initial_login { get; set; }

        #endregion public variables
    }

    public class RequestBodyCustomFieldsObj
    {
        #region public variables

        public string name { get; set; }

        public string value { get; set; }

        #endregion public variables
    }

    public class RequestBodyDocumentObj
    {
        #region public variables

        public string type { get; set; }

        public string format { get; set; }

        public List<RequestBodyCustomFieldsObj> custom_fields { get; set; }

        #endregion public variables
    }

    public class RequestBodySettingsObj
    {
        #region public variables

        public bool overwrite_duplicates { get; set; }
        
        public bool generate_pdf { get; set; }
        
        public string display_name { get; set; }
        
        public bool metadata_update { get; set; }

        #endregion public variables
    }

    public class RequestBodyCertificationObj
    {
        #region public variables

        public string certification_key_id { get; set; }

        public bool suppress_email { get; set; }

        public bool live_document { get; set; }

        #endregion public variables
    }

    public class RequestBodyAccessChargeObj
    {
        #region public variables

        public string amount { get; set; }

        public string currency { get; set; }

        public string charge_method { get; set; }

        #endregion public variables
    }

    public class RequestBodyObj
    {
        #region public variables

        public RequestBodyUserObj user { get; set; }

        public RequestBodyDocumentObj document { get; set; }

        public RequestBodySettingsObj settings { get; set; }
        
        public RequestBodyCertificationObj auto_certification { get; set; }
        
        public RequestBodyAccessChargeObj access_charge { get; set; }

        #endregion public variables
    }

    #endregion Simple Objects


}