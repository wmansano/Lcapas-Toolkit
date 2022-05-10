using Lcapas.Core.Library.Apas.AcademicRecord;
using Lcapas.Core.Library.Apas.CoreMain;
using Lcapas.Core.Logic;
using Lcapas.Core.Library;

using Lcapas.Core.Models.Lcappsdb;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Lcapas.CollWebApi.DataContracts;
using static Lcapas.Core.Library.Structs;
using System.ComponentModel.DataAnnotations;
using Lcapas.Core.Models.Colldb;
using Jitbit.Utils;

namespace Lcapas.Core.Library
{
    #region Complex Objects
    public class UserAccessViewObj : IDisposable
    {
        #region private variables

        private bool _IsInitialized = false;

        private List<User> _Users = null;
        private List<AccessGroup> _AccessGroups = null;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        #endregion private variables

        #region constructors
        public UserAccessViewObj()
        {
            _IsInitialized = true;
        }

        #endregion constructors

        #region methods
        public bool LoadObject()
        {
            bool success = false;
            try
            {
                if (_IsInitialized)
                {
                    if (_Users == null) { _Users = lcapasLogic.GetUsers(); }
                    if (_AccessGroups == null) { _AccessGroups = lcapasLogic.GetAccessGroup(); }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "UserAccessViewObj.Load", "Error", ex.ToString());
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

        public List<User> Users { get { return _Users; } set { _Users = value; } }

        public List<AccessGroup> AccessGroups { get { return _AccessGroups; } set { _AccessGroups = value; } }

        #endregion public variables
    }

    #endregion Complex Objects

    // Student Daily Request Report Object
    #region Daily Request Report Object
    public class DailyRequestReportViewObj : IDisposable
    {
        private List<DailyRequestReportSearchResultsFilter> _DailyRequestReportSearchResultsFilter;
        private DailyRequestReportSearchFilter _DailyRequestReportSearchFilter;
        private PaginationFilter _PaginationFilter;
        private string _PageTitle;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        public DailyRequestReportViewObj()
        {
            isInitialized = true;

            _DailyRequestReportSearchFilter = this._DailyRequestReportSearchFilter ?? new DailyRequestReportSearchFilter();
            _DailyRequestReportSearchResultsFilter = this._DailyRequestReportSearchResultsFilter ?? new List<DailyRequestReportSearchResultsFilter>();
            _PaginationFilter = this.Pagination ?? new PaginationFilter();
            ReportSearchFilter.Fromdate = DateTime.Now;
            ReportSearchFilter.Todate = DateTime.Now;
        }

        public void Dispose()
        {
            lcapasLogic.Dispose();
            this.Dispose();
        }

        public bool Load()
        {
            bool success = false;
            try
            {
                _PageTitle = Structs.ReportPageTitle.DailyRequestReports;
                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    success = collLogic.DailyRequestReports(this);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "DailyRequestReportViewObj.Load", "Error", ex.ToString());
            }
            return success;
        }

        public byte[] Export(string[] reportID, bool allSelected = false, string filterFields = null)
        {
            var csvFile = new CsvExport();

            try
            {
                List<DailyRequestReportSearchResultsFilter> _DailyRequestList = new List<DailyRequestReportSearchResultsFilter>();

                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    _DailyRequestList = collLogic.GetDailyRequestReportByIds(reportID, allSelected, filterFields);
                }

                foreach (DailyRequestReportSearchResultsFilter DailyRequest in _DailyRequestList)
                {
                    csvFile.AddRow();
                    csvFile["Qty"] = DailyRequest.Qty;
                    csvFile["Date"] = DailyRequest.Fromdate;
                    csvFile["Student ID"] = DailyRequest.StuID;
                    csvFile["Full Name"] = DailyRequest.Fullname;
                    csvFile["Type"] = DailyRequest.Type;
                    csvFile["Delivery"] = DailyRequest.Del;
                    csvFile["Comments"] = DailyRequest.Comments;
                    csvFile["Rec Full Name"] = DailyRequest.RecpFullName;
                    csvFile["Rec ID"] = DailyRequest.RecpID;
                    csvFile["Street"] = DailyRequest.Street;
                    csvFile["City"] = DailyRequest.City;
                    csvFile["Prv"] = DailyRequest.Prv;
                    csvFile["Postal Code"] = DailyRequest.PCode;
                    csvFile["Add Opr"] = DailyRequest.AddOpr;
                    csvFile["Add Date"] = DailyRequest.AddDt;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "DailyRequestReportViewObj.Export", "Error", ex.ToString());
            }

            return csvFile.ExportToBytes();
        }

        public bool isInitialized;

        public List<DailyRequestReportSearchResultsFilter> ReportSearchResultsFilter { get { return _DailyRequestReportSearchResultsFilter; } set { _DailyRequestReportSearchResultsFilter = value; } }

        public DailyRequestReportSearchFilter ReportSearchFilter { get { return _DailyRequestReportSearchFilter; } set { _DailyRequestReportSearchFilter = value; } }

        public string PageTitle { get { return _PageTitle; } set { _PageTitle = value; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }
    }

    #endregion Daily Request Report Object


    public class DailyRequestReportObj
    {
        #region private properties
        private string _LogID = string.Empty;
        private string _Qty = string.Empty;
        private string _StuID = string.Empty;
        private string _FullName = string.Empty;
        private string _Type = string.Empty;
        private DateTime? _FromDate = null;
        private DateTime? _ToDate = null;
        private string _Del = string.Empty;
        private string _Comments = string.Empty;
        private string _RecpID = string.Empty;
        private string _RecpFullName = string.Empty;
        private string _Street1 = string.Empty;
        private string _City = string.Empty;
        private string _Prv = string.Empty;
        private string _PCode = string.Empty;
        private string _Ctry = string.Empty;
        private string _AddMod = string.Empty;
        private string _AddOpr = string.Empty;
        private DateTime? _AddDt = null;

        #endregion private properties

        #region public properties
        public string LogID
        {
            get { return _LogID; }
            set { _LogID = value; }
        }
        public string Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
        }
        public string StuID
        {
            get { return _StuID; }
            set { _StuID = value; }
        }
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public DateTime? FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public DateTime? ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        public string Del
        {
            get { return _Del; }
            set { _Del = value; }
        }
        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }
        public string RecpID
        {
            get { return _RecpID; }
            set { _RecpID = value; }
        }
        public string RecpFullName
        {
            get { return _RecpFullName; }
            set { _RecpFullName = value; }
        }
        public string AddMod
        {
            get { return _AddMod; }
            set { _AddMod = value; }
        }
        public string Street1
        {
            get { return _Street1; }
            set { _Street1 = value; }
        }
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        public string Prv
        {
            get { return _Prv; }
            set { _Prv = value; }
        }
        public string PCode
        {
            get { return _PCode; }
            set { _PCode = value; }
        }
        public string Ctry
        {
            get { return _Ctry; }
            set { _Ctry = value; }
        }
        public string AddOpr
        {
            get { return _AddOpr; }
            set { _AddOpr = value; }
        }
        public DateTime? AddDt
        {
            get { return _AddDt; }
            set { _AddDt = value; }
        }

        #endregion public properties

    }

    public class DailyRequestReportSearchFilter
    {
        private string _qty;
        private DateTime? _fromdate;
        private DateTime? _todate;
        private string _stuID;
        private string _fullname;
        private string _type;
        private string _del;
        private string _comments;
        private string _recpID;
        private string _recpFullName;
        private string _addMod;
        private string _street;
        private string _city;
        private string _prv;
        private string _pCode;
        private string _ctry;
        private string _addOpr;
        private DateTime? _addDt;

        [Display(Name = "Qty")]
        public string Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }

        [Display(Name = "From Date")]
        public DateTime? Fromdate
        {
            get { return _fromdate; }
            set { _fromdate = value; }
        }

        [Display(Name = "To Date")]
        public DateTime? Todate
        {
            get { return _todate; }
            set { _todate = value; }
        }

        [Display(Name = "Student ID")]
        public string StuID
        {
            get { return _stuID; }
            set { _stuID = value; }
        }

        [Display(Name = "FullName")]
        public string Fullname
        {
            get { return _fullname; }
            set { _fullname = value; }
        }

        [Display(Name = "type")]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        [Display(Name = "Delivery")]
        public string Del
        {
            get { return _del; }
            set { _del = value; }
        }

        [Display(Name = "Comments")]
        public string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }


        [Display(Name = "Rec FullName")]
        public string RecpFullName
        {
            get { return _recpFullName; }
            set { _recpFullName = value; }
        }

        [Display(Name = "Rec ID")]
        public string RecpID
        {
            get { return _recpID; }
            set { _recpID = value; }
        }

        [Display(Name = "Street")]
        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }

        [Display(Name = "City")]
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        [Display(Name = "Prv")]
        public string Prv
        {
            get { return _prv; }
            set { _prv = value; }
        }

        [Display(Name = "Postal Code")]
        public string PCode
        {
            get { return _pCode; }
            set { _pCode = value; }
        }

        [Display(Name = "Country")]
        public string Ctry
        {
            get { return _ctry; }
            set { _ctry = value; }
        }


        [Display(Name = "Add Mod")]
        public string AddMod
        {
            get { return _addMod; }
            set { _addMod = value; }
        }

        [Display(Name = "Add Opr")]
        public string AddOpr
        {
            get { return _addOpr; }
            set { _addOpr = value; }
        }

        [Display(Name = "Add Date")]
        public DateTime? AddDt
        {
            get { return _addDt; }
            set { _addDt = value; }
        }
    }

    public class DailyRequestReportSearchResultsFilter
    {
        //private string _id;
        private string _logID;
        private string _qty;
        private string _stuID;
        private DateTime? _fromdate;
        private DateTime? _todate;
        private string _fullname;
        private string _type;
        private string _del;
        private string _comments;
        private string _recpID;
        private string _recpFullName;
        private string _addMod;
        private string _street;
        private string _city;
        private string _prv;
        private string _pCode;
        private string _ctry;
        private string _addOpr;
        private DateTime? _addDt;



        [Display(Name = "logID")]
        public string LogID
        {
            get { return _logID; }
            set { _logID = value; }
        }

        [Display(Name = "qty")]
        public string Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }

        [Display(Name = "stuID")]
        public string StuID
        {
            get { return _stuID; }
            set { _stuID = value; }
        }

        [Display(Name = "fromdate")]
        public DateTime? Fromdate
        {
            get { return _fromdate; }
            set { _fromdate = value; }
        }

        [Display(Name = "todate")]
        public DateTime? Todate
        {
            get { return _todate; }
            set { _todate = value; }
        }

        [Display(Name = "fullname")]
        public string Fullname
        {
            get { return _fullname; }
            set { _fullname = value; }
        }

        [Display(Name = "type")]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        [Display(Name = "del")]
        public string Del
        {
            get { return _del; }
            set { _del = value; }
        }

        [Display(Name = "comments")]
        public string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        [Display(Name = "recpID")]
        public string RecpID
        {
            get { return _recpID; }
            set { _recpID = value; }
        }

        [Display(Name = "recp FName")]
        public string RecpFullName
        {
            get { return _recpFullName; }
            set { _recpFullName = value; }
        }


        [Display(Name = "addMod")]
        public string AddMod
        {
            get { return _addMod; }
            set { _addMod = value; }
        }

        [Display(Name = "street")]
        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }

        [Display(Name = "city")]
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        [Display(Name = "prv")]
        public string Prv
        {
            get { return _prv; }
            set { _prv = value; }
        }

        [Display(Name = "pCode")]
        public string PCode
        {
            get { return _pCode; }
            set { _pCode = value; }
        }

        [Display(Name = "ctry")]
        public string Ctry
        {
            get { return _ctry; }
            set { _ctry = value; }
        }

        [Display(Name = "addOpr")]
        public string AddOpr
        {
            get { return _addOpr; }
            set { _addOpr = value; }
        }

        [Display(Name = "addDt")]
        public DateTime? AddDt
        {
            get { return _addDt; }
            set { _addDt = value; }
        }
    }

    //Login History Report Objects
    public class LoginHistoryReportViewObj : IDisposable
    {
        private List<LoginHistoryReportSearchResultsFilter> _LoginHistoryReportSearchResultsFilter;
        private LoginHistoryReportSearchFilter _LoginHistoryReportSearchFilter;
        private PaginationFilter _PaginationFilter;
        private string _PageTitle;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        private List<ActionType> _ActionDesc;
        private List<SelectListItem> _ActionDescList;


        public LoginHistoryReportViewObj()
        {
            isInitialized = true;

            _LoginHistoryReportSearchFilter = this._LoginHistoryReportSearchFilter ?? new LoginHistoryReportSearchFilter();
            _LoginHistoryReportSearchResultsFilter = this._LoginHistoryReportSearchResultsFilter ?? new List<LoginHistoryReportSearchResultsFilter>();
            _PaginationFilter = this.Pagination ?? new PaginationFilter();
        }

        public void Dispose()
        {
            lcapasLogic.Dispose();
            this.Dispose();
        }

        public bool Load()
        {
            bool success = false;
            try
            {
                _PageTitle = Structs.ReportPageTitle.LoginHistoryReports;

                success = lcapasLogic.LoginHistoryReports(this);
                _ActionDesc = lcapasLogic.GetActionTypes();

                if (_ActionDesc.Any()) _ActionDescList = _ActionDesc.Select(o => new SelectListItem { Text = o.ActionDesc.ToString(), Value = o.ActionDesc.ToString() }).ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "LoginHistoryReportViewObj.Load", "Error", ex.ToString());
            }
            return success;
        }


        public bool isInitialized;

        public List<LoginHistoryReportSearchResultsFilter> LoginHistoryReportSearchResultsFilter { get { return _LoginHistoryReportSearchResultsFilter; } set { _LoginHistoryReportSearchResultsFilter = value; } }

        public LoginHistoryReportSearchFilter LoginHistoryReportSearchFilter { get { return _LoginHistoryReportSearchFilter; } set { _LoginHistoryReportSearchFilter = value; } }

        public string PageTitle { get { return _PageTitle; } set { _PageTitle = value; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }

        public List<SelectListItem> ActionDescList { get { return _ActionDescList; } }


    }

    public class LoginHistoryReport
    {
        #region private properties
        private string _UserID = string.Empty;
        private string _UserFullName = string.Empty;
        private string _ActionDesc = string.Empty;
        private string _ActionID = string.Empty;
        private string _CreatedBy = string.Empty;
        private string _ModifiedBy = string.Empty;
        private DateTime? _CreatedDate = null;
        private DateTime? _ModifiedBydDate = null;

        #endregion private properties

        #region public properties
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string UserFullName
        {
            get { return _UserFullName; }
            set { _UserFullName = value; }
        }
        public string ActionDesc
        {
            get { return _ActionDesc; }
            set { _ActionDesc = value; }
        }
        public string ActionID
        {
            get { return _ActionID; }
            set { _ActionID = value; }
        }
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public DateTime? CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        public DateTime? ModifiedBydDate
        {
            get { return _ModifiedBydDate; }
            set { _ModifiedBydDate = value; }
        }


        #endregion public properties

    }


    public class LoginHistoryReportSearchFilter
    {
        private string _UserID;
        private string _UserFullName;
        private string _ActionDesc;
        private string _ActionID;
        private string _CreatedBy;
        private string _ModifiedBy;
        private DateTime? _CreatedDate;
        private DateTime? _ModifiedBydDate;

        [Display(Name = "User ID")]
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        [Display(Name = "User Full Name")]
        public string UserFullName
        {
            get { return _UserFullName; }
            set { _UserFullName = value; }
        }

        [Display(Name = "Action Description")]
        public string ActionDesc
        {
            get { return _ActionDesc; }
            set { _ActionDesc = value; }
        }

        [Display(Name = "Action ID")]
        public string ActionID
        {
            get { return _ActionID; }
            set { _ActionID = value; }
        }

        [Display(Name = "Created By")]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        [Display(Name = "Modified By")]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }

        [Display(Name = "Created Date")]
        public DateTime? CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        [Display(Name = "Modified By Date")]
        public DateTime? ModifiedBydDate
        {
            get { return _ModifiedBydDate; }
            set { _ModifiedBydDate = value; }
        }

    }

    public class LoginHistoryReportSearchResultsFilter
    {
        private string _HistoryID;
        private string _UserID;
        private string _UserFullName;
        private string _ActionDesc;
        private string _ActionID;
        private string _CreatedBy;
        private string _ModifiedBy;
        private DateTime? _CreatedDate;
        private DateTime? _ModifiedBydDate;

        [Display(Name = "HistoryID")]
        public string HistoryID
        {
            get { return _HistoryID; }
            set { _HistoryID = value; }
        }

        [Display(Name = "UserID")]
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        [Display(Name = "UserFullName")]
        public string UserFullName
        {
            get { return _UserFullName; }
            set { _UserFullName = value; }
        }

        [Display(Name = "ActionDesc")]
        public string ActionDesc
        {
            get { return _ActionDesc; }
            set { _ActionDesc = value; }
        }

        [Display(Name = "ActionID")]
        public string ActionID
        {
            get { return _ActionID; }
            set { _ActionID = value; }
        }

        [Display(Name = "CreatedBy")]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        [Display(Name = "ModifiedBy")]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }

        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        [Display(Name = "ModifiedBydDate")]
        public DateTime? ModifiedBydDate
        {
            get { return _ModifiedBydDate; }
            set { _ModifiedBydDate = value; }
        }
    }

    //Exception Error Report Objects
    public class ExceptionErrorsReportViewObj : IDisposable
    {
        private List<ExceptionErrorsReportSearchResultsFilter> _ExceptionErrorsReportSearchResultsFilter;
        private ExceptionErrorsReportSearchFilter _ExceptionErrorsReportSearchFilter;
        private PaginationFilter _PaginationFilter;
        private string _PageTitle;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        public ExceptionErrorsReportViewObj()
        {
            isInitialized = true;

            _ExceptionErrorsReportSearchFilter = this._ExceptionErrorsReportSearchFilter ?? new ExceptionErrorsReportSearchFilter();
            _ExceptionErrorsReportSearchResultsFilter = this._ExceptionErrorsReportSearchResultsFilter ?? new List<ExceptionErrorsReportSearchResultsFilter>();
            _PaginationFilter = this.Pagination ?? new PaginationFilter();
            ExceptionErrorsReportSearchFilter.CreatedDateTime = DateTime.Now;
        }

        public void Dispose()
        {
            lcapasLogic.Dispose();
            this.Dispose();
        }

        public bool Load()
        {
            bool success = false;
            try
            {
                _PageTitle = Structs.ReportPageTitle.ExceptionErrorReports;

                success = lcapasLogic.ExceptionErrorsReports(this);

            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "ExceptionErrorsReportViewObj.Load", "Error", ex.ToString());
            }
            return success;
        }


        public bool isInitialized;

        public List<ExceptionErrorsReportSearchResultsFilter> ExceptionErrorsReportSearchResultsFilter { get { return _ExceptionErrorsReportSearchResultsFilter; } set { _ExceptionErrorsReportSearchResultsFilter = value; } }

        public ExceptionErrorsReportSearchFilter ExceptionErrorsReportSearchFilter { get { return _ExceptionErrorsReportSearchFilter; } set { _ExceptionErrorsReportSearchFilter = value; } }

        public string PageTitle { get { return _PageTitle; } set { _PageTitle = value; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }
    }

    public class ExceptionErrorsObj
    {
        #region private properties
        private string _StatusTrackingID = string.Empty;
        private string _Project = string.Empty;
        private string _Page = string.Empty;
        private string _Function = string.Empty;
        private string _Value = string.Empty;
        private string _CreatedBy = string.Empty;
        private DateTime? _CreatedDateTime = null;

        #endregion private properties

        #region public properties
        public string StatusTrackingID
        {
            get { return _StatusTrackingID; }
            set { _StatusTrackingID = value; }
        }
        public string Project
        {
            get { return _Project; }
            set { _Project = value; }
        }
        public string Page
        {
            get { return _Page; }
            set { _Page = value; }
        }
        public string Function
        {
            get { return _Function; }
            set { _Function = value; }
        }
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public DateTime? CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }

        #endregion public properties
    }

    public class ExceptionErrorsReportSearchFilter
    {
        private string _StatusTrackingID;
        private string _Project;
        private string _Page;
        private string _Function;
        private string _Value;
        private string _CreatedBy;
        private DateTime? _CreatedDateTime;

        [Display(Name = "StatusTrackingID")]
        public string StatusTrackingID
        {
            get { return _StatusTrackingID; }
            set { _StatusTrackingID = value; }
        }

        [Display(Name = "Project")]
        public string Project
        {
            get { return _Project; }
            set { _Project = value; }
        }

        [Display(Name = "Page")]
        public string Page
        {
            get { return _Page; }
            set { _Page = value; }
        }

        [Display(Name = "Function")]
        public string Function
        {
            get { return _Function; }
            set { _Function = value; }
        }

        [Display(Name = "Value")]
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        [Display(Name = "Created By")]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        [Display(Name = "Created Date")]
        public DateTime? CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }
    }

    public class ExceptionErrorsReportSearchResultsFilter
    {
        private string _StatusTrackingID;
        private string _Project;
        private string _Page;
        private string _Function;
        private string _Value;
        private string _CreatedBy;
        private DateTime? _CreatedDateTime;

        [Display(Name = "StatusTrackingID")]
        public string StatusTrackingID
        {
            get { return _StatusTrackingID; }
            set { _StatusTrackingID = value; }
        }

        [Display(Name = "Project")]
        public string Project
        {
            get { return _Project; }
            set { _Project = value; }
        }

        [Display(Name = "Page")]
        public string Page
        {
            get { return _Page; }
            set { _Page = value; }
        }

        [Display(Name = "Function")]
        public string Function
        {
            get { return _Function; }
            set { _Function = value; }
        }

        [Display(Name = "Value")]
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        [Display(Name = "CreatedBy")]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        [Display(Name = "CreatedDateTime")]
        public DateTime? CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }

    }

    // Application Report Object
    public class ApplicationReportViewObj : IDisposable
    {
        // Aplication Report Object
        private List<ApplicationReportSearchResultsFilter> _ApplicationReportSearchResultsFilter;
        private ApplicationReportSearchFilter _ApplicationReportSearchFilter;
        private PaginationFilter _PaginationFilter;
        private string _PageTitle;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        private List<ProgramTerm> _TermCodeList;
        private List<SelectListItem> _TermCodeSelectedList;
        private List<ApplicationProgram> _ProgramCodeList;
        private List<SelectListItem> _ProgramCodeSelectedList;
        private List<SelectListItem> _LanguageCodeSelectedList;
        private List<ProgramCampus> _CampusCodeList;
        private List<SelectListItem> _CampusCodeSelectedList;
        private List<SelectListItem> _StudenLoadCodeSelectedList;
        private List<SelectListItem> _PreviousAppliedCodeSelectedList;
        private List<SelectListItem> _FamilyAttendedCollegeCodeSelectedList;
        private List<SelectListItem> _EthnicityRaceCodeSelectedList;
        private List<SelectListItem> _GenderCodeSelectedList;


        public ApplicationReportViewObj()
        {
            isInitialized = true;

            _ApplicationReportSearchFilter = this._ApplicationReportSearchFilter ?? new ApplicationReportSearchFilter();
            _ApplicationReportSearchResultsFilter = this._ApplicationReportSearchResultsFilter ?? new List<ApplicationReportSearchResultsFilter>();
            _PaginationFilter = this.Pagination ?? new PaginationFilter();
        }

        public void Dispose()
        {
            lcapasLogic.Dispose();
            this.Dispose();
        }

        public bool Load()
        {
            bool success = false;

            try
            {
                _PageTitle = Structs.ReportPageTitle.ApplicationReports;

                if (_TermCodeSelectedList == null) { _TermCodeSelectedList = new List<SelectListItem>(); }
                if (_ProgramCodeSelectedList == null) { _ProgramCodeSelectedList = new List<SelectListItem>(); }
                if (_CampusCodeSelectedList == null) { _CampusCodeSelectedList = new List<SelectListItem>(); }
                if (_LanguageCodeSelectedList == null) { _LanguageCodeSelectedList = new List<SelectListItem>(); }
                if (_StudenLoadCodeSelectedList == null) { _StudenLoadCodeSelectedList = new List<SelectListItem>(); }
                if (_PreviousAppliedCodeSelectedList == null) { _PreviousAppliedCodeSelectedList = new List<SelectListItem>(); }

                _TermCodeList = lcapasLogic.GetTerms();
                _ProgramCodeList = lcapasLogic.GetApplicationPrograms();
                _LanguageCodeSelectedList = lcapasLogic.GetLanguages();
                _CampusCodeList = lcapasLogic.GetCampuses();
                _StudenLoadCodeSelectedList = lcapasLogic.GetFullpart();
                _PreviousAppliedCodeSelectedList = lcapasLogic.GetTrueFalse();
                _FamilyAttendedCollegeCodeSelectedList = lcapasLogic.GetFamilyAttendedCollege();
                _EthnicityRaceCodeSelectedList = lcapasLogic.GetEthnicityRace();
                _GenderCodeSelectedList = lcapasLogic.GetGender();

                if (_TermCodeList.Any()) _TermCodeSelectedList = _TermCodeList.Select(s => new SelectListItem { Text = s.TermCode.ToString(), Value = s.TermCode.ToString() }).ToList();
                if (_ProgramCodeList.Any()) _ProgramCodeSelectedList = _ProgramCodeList.Select(e => new SelectListItem { Text = e.ProgramCode.ToString(), Value = e.ProgramCode.ToString() }).ToList();
                if (_CampusCodeList.Any()) _CampusCodeSelectedList = _CampusCodeList.Select(g => new SelectListItem { Text = g.CampusCode.ToString(), Value = g.CampusCode.ToString() }).ToList();

                success = lcapasLogic.ApplicationReports(this);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "ApplicationReportViewObj.Load", "Error", ex.ToString());
            }
            return success;
        }

        public byte[] Export(string[] reportID, bool allSelected = false, string filterFields = null)
        {
            var csvFile = new CsvExport();

            try
            {
                List<ApplicationReportSearchResultsFilter> _ApplicationPrograms = new List<ApplicationReportSearchResultsFilter>();

                _ApplicationPrograms = lcapasLogic.GetApplicationReportByIds(reportID, allSelected, filterFields);

                foreach (ApplicationReportSearchResultsFilter _ApplicationProgram in _ApplicationPrograms)
                {
                    csvFile.AddRow();

                    csvFile["LName, FName, MName"] = _ApplicationProgram.FullName;
                    csvFile["Program"] = _ApplicationProgram.ProgramCode;
                    csvFile["Term"] = _ApplicationProgram.TermCode;
                    csvFile["Campus"] = _ApplicationProgram.CampusCode;
                    csvFile["sNumber"] = _ApplicationProgram.sNumber;
                    csvFile["ASN"] = _ApplicationProgram.AgencyAssignedID;
                    csvFile["Lang"] = _ApplicationProgram.LanguageCode;
                    csvFile["Admit Status"] = _ApplicationProgram.AdmitStatus;
                    csvFile["Study Load"] = _ApplicationProgram.StudyLoad;
                    csvFile["Reference Desc"] = _ApplicationProgram.ReferenceTypeDesc;
                    csvFile["Prev Applied"] = _ApplicationProgram.PreviouslyApplied;
                    csvFile["Disability"] = _ApplicationProgram.Disability;
                    csvFile["App ID"] = _ApplicationProgram.ApplicationID;
                    csvFile["From Date"] = _ApplicationProgram.FromDate;
                    csvFile["Received Date"] = _ApplicationProgram.ReceivedDateTime;
                    csvFile["Paid Date"] = _ApplicationProgram.PaidDateTime;
                    csvFile["Prev SNumber"] = _ApplicationProgram.PrevSNumber;
                    csvFile["Address Line1"] = _ApplicationProgram.AddressLine1;
                    csvFile["Address Line2"] = _ApplicationProgram.AddressLine2;
                    csvFile["City"] = _ApplicationProgram.City;
                    csvFile["Province"] = _ApplicationProgram.Province;
                    csvFile["Postal Code"] = _ApplicationProgram.PostalCode;
                    csvFile["Country"] = _ApplicationProgram.Country;
                    csvFile["Phone Number"] = _ApplicationProgram.AreaCode + " " + _ApplicationProgram.PhoneNumber;
                    csvFile["Gender"] = _ApplicationProgram.Gender;
                    csvFile["Birth Date"] = _ApplicationProgram.BirthDate;
                    csvFile["Ethnicity Race"] = _ApplicationProgram.EthnicityRace;
                    csvFile["First Entry Into Country"] = _ApplicationProgram.FirstEntryIntoCountry;
                    csvFile["Family Attended College"] = _ApplicationProgram.FamilyAttendedCollege;
                    csvFile["Email Address"] = _ApplicationProgram.EmailAddress;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "ApplicationReportViewObj.Export", "Error", ex.ToString());
            }

            return csvFile.ExportToBytes();
        }

        public bool isInitialized;

        public List<ApplicationReportSearchResultsFilter> ApplicationReportSearchResultsFilter { get { return _ApplicationReportSearchResultsFilter; } set { _ApplicationReportSearchResultsFilter = value; } }

        public ApplicationReportSearchFilter ApplicationReportSearchFilter { get { return _ApplicationReportSearchFilter; } set { _ApplicationReportSearchFilter = value; } }

        public string PageTitle { get { return _PageTitle; } set { _PageTitle = value; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }

        public List<SelectListItem> TermCodeSelectedList { get { return _TermCodeSelectedList; } }

        public List<SelectListItem> ProgramCodeSelectedList { get { return _ProgramCodeSelectedList; } }

        public List<SelectListItem> LanguageCodeSelectedList { get { return _LanguageCodeSelectedList; } }

        public List<SelectListItem> CampusCodeSelectedList { get { return _CampusCodeSelectedList; } }

        public List<SelectListItem> StudenLoadCodeSelectedList { get { return _StudenLoadCodeSelectedList; } }

        public List<SelectListItem> PreviousAppliedCodeSelectedList { get { return _PreviousAppliedCodeSelectedList; } }

        public List<SelectListItem> FamilyAttendedCollegeCodeSelectedList { get { return _FamilyAttendedCollegeCodeSelectedList; } }

        public List<SelectListItem> EthnicityRaceCodeSelectedList { get { return _EthnicityRaceCodeSelectedList; } }
        
        public List<SelectListItem> GenderCodeSelectedList { get { return _GenderCodeSelectedList; } }
    }

    public class ApplicationReportObj
    {
        #region private properties
        private string _FullName = string.Empty;
        private string _PrevSNumber = string.Empty;
        private string _AgencyAssignedID = string.Empty;
        private string _AddressLine1 = string.Empty;
        private string _AddressLine2 = string.Empty;
        private string _City = string.Empty;
        private string _Province = string.Empty;
        private string _PostalCode = string.Empty;
        private string _Country = string.Empty;
        private string _AreaCode = string.Empty;
        private string _PhoneNumber = string.Empty;
        private string _Gender = string.Empty;
        private DateTime? _BirthDate = null;
        private string _EthnicityRace = string.Empty;
        private string _EmailAddress = string.Empty;
        private string _TermCode = string.Empty;
        private string _StudyLoad = string.Empty;
        private string _PreviouslyApplied = string.Empty;
        private string _ProgramCode = string.Empty;
        private string _CampusCode = string.Empty;
        private string _AdmitStatus = string.Empty;
        private string _LanguageCode = string.Empty;
        private string _ReferenceTypeDesc = string.Empty;
        private string _Disability = string.Empty;
        private string _ApplicationID = string.Empty;
        private DateTime? _FromDate = null;
        private DateTime? _ReceivedDateTime = null;
        private DateTime? _PaidDateTime = null;

        #endregion private properties

        #region public properties
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        public string PrevSNumber
        {
            get { return _PrevSNumber; }
            set { _PrevSNumber = value; }
        }
        public string AgencyAssignedID
        {
            get { return _AgencyAssignedID; }
            set { _AgencyAssignedID = value; }
        }
        public string AddressLine1
        {
            get { return _AddressLine1; }
            set { _AddressLine1 = value; }
        }
        public string AddressLine2
        {
            get { return _AddressLine2; }
            set { _AddressLine2 = value; }
        }
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        public string Province
        {
            get { return _Province; }
            set { _Province = value; }
        }
        public string PostalCode
        {
            get { return _PostalCode; }
            set { _PostalCode = value; }
        }
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        public string AreaCode
        {
            get { return _AreaCode; }
            set { _AreaCode = value; }
        }
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        public DateTime? BirthDate
        {
            get { return _BirthDate; }
            set { _BirthDate = value; }
        }
        public string EthnicityRace
        {
            get { return _EthnicityRace; }
            set { _EthnicityRace = value; }
        }
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        public string TermCode
        {
            get { return _TermCode; }
            set { _TermCode = value; }
        }
        public string StudyLoad
        {
            get { return _StudyLoad; }
            set { _StudyLoad = value; }
        }
        public string PreviouslyApplied
        {
            get { return _PreviouslyApplied; }
            set { _PreviouslyApplied = value; }
        }
        public string ProgramCode
        {
            get { return _ProgramCode; }
            set { _ProgramCode = value; }
        }
        public string CampusCode
        {
            get { return _CampusCode; }
            set { _CampusCode = value; }
        }
        public string AdmitStatus
        {
            get { return _AdmitStatus; }
            set { _AdmitStatus = value; }
        }
        public string LanguageCode
        {
            get { return _LanguageCode; }
            set { _LanguageCode = value; }
        }

        public string ReferenceTypeDesc
        {
            get { return _ReferenceTypeDesc; }
            set { _ReferenceTypeDesc = value; }
        }
        public string Disability
        {
            get { return _Disability; }
            set { _Disability = value; }
        }
        public string ApplicationID
        {
            get { return _ApplicationID; }
            set { _ApplicationID = value; }
        }

        public DateTime? FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public DateTime? ReceivedDateTime
        {
            get { return _ReceivedDateTime; }
            set { _ReceivedDateTime = value; }
        }

        public DateTime? PaidDateTime
        {
            get { return _PaidDateTime; }
            set { _PaidDateTime = value; }
        }
        #endregion public properties
    }

    public class ApplicationReportSearchFilter
    {
        private string _FullName;
        private string _PrevSNumber;
        private string _AgencyAssignedID;
        private string _AddressLine1;
        private string _AddressLine2;
        private string _City;
        private string _Province;
        private string _PostalCode;
        private string _Country;
        private string _AreaCode;
        private string _PhoneNumber;
        private List<string> _Gender;
        private DateTime? _BirthDate;
        private List<string> _EthnicityRace;
        private string _EmailAddress;
        private string _TermCode;
        private string _StudyLoad;
        private string _PreviouslyApplied;
        private string _ProgramCode;
        private string _CampusCode;
        private string _AdmitStatus;
        private string _LanguageCode;
        private List<string> _LanguageListCode;
        private string _ReferenceTypeDesc;
        private string _Disability;
        private string _ApplicationID;
        private DateTime? _FromDate;
        private DateTime? _ReceivedDateTime;
        private DateTime? _PaidDateTime;
        private DateTime? _FirstEntryIntoCountry;
        private List<AttendedCollegeCodeType?> _FamilyAttendedCollegeListCode;
        private string _sNumber;

        #region public properties
        [Display(Name = "LName, FName, MName")]
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        [Display(Name = "Prev SNumber")]
        public string PrevSNumber
        {
            get { return _PrevSNumber; }
            set { _PrevSNumber = value; }
        }

        [Display(Name = "ASN")]
        public string AgencyAssignedID
        {
            get { return _AgencyAssignedID; }
            set { _AgencyAssignedID = value; }
        }

        [Display(Name = "Address Line1")]
        public string AddressLine1
        {
            get { return _AddressLine1; }
            set { _AddressLine1 = value; }
        }

        [Display(Name = "Address Line2")]
        public string AddressLine2
        {
            get { return _AddressLine2; }
            set { _AddressLine2 = value; }
        }

        [Display(Name = "City")]
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        [Display(Name = "Province")]
        public string Province
        {
            get { return _Province; }
            set { _Province = value; }
        }

        [Display(Name = "Postal Code")]
        public string PostalCode
        {
            get { return _PostalCode; }
            set { _PostalCode = value; }
        }

        [Display(Name = "Country")]
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        [Display(Name = "Area Code")]
        public string AreaCode
        {
            get { return _AreaCode; }
            set { _AreaCode = value; }
        }

        [Display(Name = "Phone Number")]
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        [Display(Name = "Gender")]
        public List<string> Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        [Display(Name = "Birth Date")]
        public DateTime? BirthDate
        {
            get { return _BirthDate; }
            set { _BirthDate = value; }
        }

        [Display(Name = "Ethnicity Race")]
        public List<string> EthnicityRace
        {
            get { return _EthnicityRace; }
            set { _EthnicityRace = value; }
        }

        [Display(Name = "Email Address")]
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

        [Display(Name = "Term")]
        public string TermCode
        {
            get { return _TermCode; }
            set { _TermCode = value; }
        }

        [Display(Name = "Study Load")]
        public string StudyLoad
        {
            get { return _StudyLoad; }
            set { _StudyLoad = value; }
        }

        [Display(Name = "Prev Applied")]
        public string PreviouslyApplied
        {
            get { return _PreviouslyApplied; }
            set { _PreviouslyApplied = value; }
        }

        [Display(Name = "Program")]
        public string ProgramCode
        {
            get { return _ProgramCode; }
            set { _ProgramCode = value; }
        }

        [Display(Name = "Campus")]
        public string CampusCode
        {
            get { return _CampusCode; }
            set { _CampusCode = value; }
        }

        [Display(Name = "Admit Status")]
        public string AdmitStatus
        {
            get { return _AdmitStatus; }
            set { _AdmitStatus = value; }
        }

        [Display(Name = "Lang")]
        public string LanguageCode
        {
            get { return _LanguageCode; }
            set { _LanguageCode = value; }
        }

        [Display(Name = "Lang")]
        public List<string> LanguageListCode
        {
            get { return _LanguageListCode; }
            set { _LanguageListCode = value; }
        }

        [Display(Name = "Reference Desc")]
        public string ReferenceTypeDesc
        {
            get { return _ReferenceTypeDesc; }
            set { _ReferenceTypeDesc = value; }
        }

        [Display(Name = "Disability")]
        public string Disability
        {
            get { return _Disability; }
            set { _Disability = value; }
        }

        [Display(Name = "App ID")]
        public string ApplicationID
        {
            get { return _ApplicationID; }
            set { _ApplicationID = value; }
        }

        [Display(Name = "From Date")]
        public DateTime? FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        [Display(Name = "Received DateTime")]
        public DateTime? ReceivedDateTime
        {
            get { return _ReceivedDateTime; }
            set { _ReceivedDateTime = value; }
        }

        [Display(Name = "Paid DateTime")]
        public DateTime? PaidDateTime
        {
            get { return _PaidDateTime; }
            set { _PaidDateTime = value; }
        }

        [Display(Name = "First Entry Into Country")]
        public DateTime? FirstEntryIntoCountry
        {
            get { return _FirstEntryIntoCountry; }
            set { _FirstEntryIntoCountry = value; }
        }

        [Display(Name = "Family Attended College")]
        public List<AttendedCollegeCodeType?> FamilyAttendedCollegeListCode
        {
            get { return _FamilyAttendedCollegeListCode; }
            set { _FamilyAttendedCollegeListCode = value; }
        }

        [Display(Name = "sNumber")]
        public string sNumber
        {
            get { return _sNumber; }
            set { _sNumber = value; }
        }

        #endregion public properties

    }
    public class ApplicationReportSearchResultsFilter
    {
        private string _FullName;
        private string _PrevSNumber;
        private string _AgencyAssignedID;
        private string _AddressLine1;
        private string _AddressLine2;
        private string _City;
        private string _Province;
        private string _PostalCode;
        private string _Country;
        private string _AreaCode;
        private string _PhoneNumber;
        private string _Gender;
        private DateTime? _BirthDate;
        private string _EthnicityRace;
        private string _EmailAddress;
        private string _TermCode;
        private string _StudyLoad;
        private string _PreviouslyApplied;
        private string _ProgramCode;
        private string _CampusCode;
        private string _AdmitStatus;
        private string _LanguageCode;
        private string _ReferenceTypeDesc;
        private string _Disability;
        private string _ApplicationID;
        private DateTime? _FromDate;
        private DateTime? _ReceivedDateTime;
        private DateTime? _PaidDateTime;
        private DateTime? _FirstEntryIntoCountry;
        private AttendedCollegeCodeType? _FamilyAttendedCollege;
        private string _sNumber;

        #region public properties
        [Display(Name = "LName, FName, MName")]
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        [Display(Name = "Pre SNumber")]
        public string PrevSNumber
        {
            get { return _PrevSNumber; }
            set { _PrevSNumber = value; }
        }

        [Display(Name = "ASN")]
        public string AgencyAssignedID
        {
            get { return _AgencyAssignedID; }
            set { _AgencyAssignedID = value; }
        }

        [Display(Name = "Address Line1")]
        public string AddressLine1
        {
            get { return _AddressLine1; }
            set { _AddressLine1 = value; }
        }

        [Display(Name = "Address Line2")]
        public string AddressLine2
        {
            get { return _AddressLine2; }
            set { _AddressLine2 = value; }
        }

        [Display(Name = "City")]
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        [Display(Name = "Province")]
        public string Province
        {
            get { return _Province; }
            set { _Province = value; }
        }

        [Display(Name = "_Postal Code")]
        public string PostalCode
        {
            get { return _PostalCode; }
            set { _PostalCode = value; }
        }

        [Display(Name = "Country")]
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        [Display(Name = "Area Code")]
        public string AreaCode
        {
            get { return _AreaCode; }
            set { _AreaCode = value; }
        }

        [Display(Name = "Phone Number")]
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        [Display(Name = "Gender")]
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        [Display(Name = "Birth Date")]
        public DateTime? BirthDate
        {
            get { return _BirthDate; }
            set { _BirthDate = value; }
        }

        [Display(Name = "Ethnicity Race")]
        public string EthnicityRace
        {
            get { return _EthnicityRace; }
            set { _EthnicityRace = value; }
        }

        [Display(Name = "Email Address")]
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

        [Display(Name = "Term Code")]
        public string TermCode
        {
            get { return _TermCode; }
            set { _TermCode = value; }
        }

        [Display(Name = "Study Load")]
        public string StudyLoad
        {
            get { return _StudyLoad; }
            set { _StudyLoad = value; }
        }

        [Display(Name = "Prev Applied")]
        public string PreviouslyApplied
        {
            get { return _PreviouslyApplied; }
            set { _PreviouslyApplied = value; }
        }

        [Display(Name = "Program")]
        public string ProgramCode
        {
            get { return _ProgramCode; }
            set { _ProgramCode = value; }
        }

        [Display(Name = "Campus")]
        public string CampusCode
        {
            get { return _CampusCode; }
            set { _CampusCode = value; }
        }

        [Display(Name = "Admit Status")]
        public string AdmitStatus
        {
            get { return _AdmitStatus; }
            set { _AdmitStatus = value; }
        }

        [Display(Name = "Lang")]
        public string LanguageCode
        {
            get { return _LanguageCode; }
            set { _LanguageCode = value; }
        }

        [Display(Name = "Reference Desc")]
        public string ReferenceTypeDesc
        {
            get { return _ReferenceTypeDesc; }
            set { _ReferenceTypeDesc = value; }
        }

        [Display(Name = "Disability")]
        public string Disability
        {
            get { return _Disability; }
            set { _Disability = value; }
        }

        [Display(Name = "App ID")]
        public string ApplicationID
        {
            get { return _ApplicationID; }
            set { _ApplicationID = value; }
        }

        [Display(Name = "From Date")]
        public DateTime? FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        [Display(Name = "Received Date")]
        public DateTime? ReceivedDateTime
        {
            get { return _ReceivedDateTime; }
            set { _ReceivedDateTime = value; }
        }

        [Display(Name = "Paid Date")]
        public DateTime? PaidDateTime
        {
            get { return _PaidDateTime; }
            set { _PaidDateTime = value; }
        }

        [Display(Name = "First Entry Into Country")]
        public DateTime? FirstEntryIntoCountry
        {
            get { return _FirstEntryIntoCountry; }
            set { _FirstEntryIntoCountry = value; }
        }

        [Display(Name = "Family Attended College")]
        public AttendedCollegeCodeType? FamilyAttendedCollege
        {
            get { return _FamilyAttendedCollege; }
            set { _FamilyAttendedCollege = value; }
        }

        [Display(Name = "sNumber")]
        public string sNumber
        {
            get { return _sNumber; }
            set { _sNumber = value; }
        }

        #endregion public properties
    }

    // Payment Report Object
    public class PaymentReportViewObj : IDisposable
    {
        private List<PaymentReportSearchResultsFilter> _PaymentReportSearchResultsFilter;
        private PaymentReportSearchFilter _PaymentReportSearchFilter;
        private PaginationFilter _PaginationFilter;
        private string _PageTitle;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        private List<ProgramTerm> _TermCodeList;
        private List<SelectListItem> _TermCodeSelectedList;

        private List<ApplicationProgram> _ProgramCodeList;
        private List<SelectListItem> _ProgramCodeSelectedList;

        private List<SelectListItem> _LanguageCodeSelectedList;

        private List<ProgramCampus> _CampusCodeList;
        private List<SelectListItem> _CampusCodeSelectedList;


        public PaymentReportViewObj()
        {
            isInitialized = true;

            _PaymentReportSearchFilter = this._PaymentReportSearchFilter ?? new PaymentReportSearchFilter();
            _PaymentReportSearchResultsFilter = this._PaymentReportSearchResultsFilter ?? new List<PaymentReportSearchResultsFilter>();
            _PaginationFilter = this.Pagination ?? new PaginationFilter();
        }

        public void Dispose()
        {
            lcapasLogic.Dispose();
            this.Dispose();
        }

        public bool Load()
        {
            bool success = false;
            try
            {
                _PageTitle = Structs.ReportPageTitle.PaymentReports;
                if (_TermCodeSelectedList == null) { _TermCodeSelectedList = new List<SelectListItem>(); }
                if (_ProgramCodeSelectedList == null) { _ProgramCodeSelectedList = new List<SelectListItem>(); }
                if (_CampusCodeSelectedList == null) { _CampusCodeSelectedList = new List<SelectListItem>(); }
                if (_LanguageCodeSelectedList == null) { _LanguageCodeSelectedList = new List<SelectListItem>(); }

                _TermCodeList = lcapasLogic.GetTerms();
                _ProgramCodeList = lcapasLogic.GetApplicationPrograms();
                _LanguageCodeSelectedList = lcapasLogic.GetLanguages();
                _CampusCodeList = lcapasLogic.GetCampuses();

                if (_TermCodeList.Any()) _TermCodeSelectedList = _TermCodeList.Select(s => new SelectListItem { Text = s.TermCode.ToString(), Value = s.TermCode.ToString() }).ToList();
                if (_ProgramCodeList.Any()) _ProgramCodeSelectedList = _ProgramCodeList.Select(e => new SelectListItem { Text = e.ProgramCode.ToString(), Value = e.ProgramCode.ToString() }).ToList();
                if (_CampusCodeList.Any()) _CampusCodeSelectedList = _CampusCodeList.Select(g => new SelectListItem { Text = g.CampusCode.ToString(), Value = g.CampusCode.ToString() }).ToList();

                success = lcapasLogic.PaymentReports(this);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "PaymentReportViewObj.Load", "Error", ex.ToString());
            }
            return success;
        }


        public bool isInitialized;

        public List<PaymentReportSearchResultsFilter> PaymentReportSearchResultsFilter { get { return _PaymentReportSearchResultsFilter; } set { _PaymentReportSearchResultsFilter = value; } }

        public PaymentReportSearchFilter PaymentReportSearchFilter { get { return _PaymentReportSearchFilter; } set { _PaymentReportSearchFilter = value; } }

        public string PageTitle { get { return _PageTitle; } set { _PageTitle = value; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }


        public List<SelectListItem> TermCodeSelectedList { get { return _TermCodeSelectedList; } }
        public List<SelectListItem> ProgramCodeSelectedList { get { return _ProgramCodeSelectedList; } }
        public List<SelectListItem> LanguageCodeSelectedList { get { return _LanguageCodeSelectedList; } }
        public List<SelectListItem> CampusCodeSelectedList { get { return _CampusCodeSelectedList; } }

    }

    public class PaymentReportObj
    {
        #region private properties
        private string _FullName = string.Empty;
        private string _PrevSNumber = string.Empty;
        private string _AddressLine1 = string.Empty;
        private string _AddressLine2 = string.Empty;
        private string _City = string.Empty;
        private string _Province = string.Empty;
        private string _PostalCode = string.Empty;
        private string _Country = string.Empty;
        private string _AreaCode = string.Empty;
        private string _PhoneNumber = string.Empty;
        private DateTime? _BirthDate = null;
        private string _EmailAddress = string.Empty;
        private string _TermCode = string.Empty;
        private string _ProgramCode = string.Empty;
        private string _CampusCode = string.Empty;
        private string _LanguageCode = string.Empty;
        private string _PaypalResponseId = string.Empty;
        private string _Uuid = string.Empty;
        private string _BilltoName = string.Empty;
        private string _Billtoemail = string.Empty;
        private string _Billtostreet = string.Empty;
        private string _Billtocountry = string.Empty;
        private string _Billtozip = string.Empty;
        private string _Billtophone = string.Empty;
        private string _Cardtype = string.Empty;
        private string _Method = string.Empty;
        private string _Respmsg = string.Empty;
        private string _Result = string.Empty;
        private DateTime? _PaidDateTime = null;

        #endregion private properties

        #region public properties
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        public string PrevSNumber
        {
            get { return _PrevSNumber; }
            set { _PrevSNumber = value; }
        }
        public string AddressLine1
        {
            get { return _AddressLine1; }
            set { _AddressLine1 = value; }
        }
        public string AddressLine2
        {
            get { return _AddressLine2; }
            set { _AddressLine2 = value; }
        }
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        public string Province
        {
            get { return _Province; }
            set { _Province = value; }
        }
        public string PostalCode
        {
            get { return _PostalCode; }
            set { _PostalCode = value; }
        }
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        public string AreaCode
        {
            get { return _AreaCode; }
            set { _AreaCode = value; }
        }
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        public DateTime? BirthDate
        {
            get { return _BirthDate; }
            set { _BirthDate = value; }
        }
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        public string TermCode
        {
            get { return _TermCode; }
            set { _TermCode = value; }
        }
        public string ProgramCode
        {
            get { return _ProgramCode; }
            set { _ProgramCode = value; }
        }
        public string CampusCode
        {
            get { return _CampusCode; }
            set { _CampusCode = value; }
        }
        public string LanguageCode
        {
            get { return _LanguageCode; }
            set { _LanguageCode = value; }
        }
        public string PaypalResponseId
        {
            get { return _PaypalResponseId; }
            set { _PaypalResponseId = value; }
        }
        public string Uuid
        {
            get { return _Uuid; }
            set { _Uuid = value; }
        }
        public string BilltoName
        {
            get { return BilltoName; }
            set { _BilltoName = value; }
        }
        public string Billtoemail
        {
            get { return _Billtoemail; }
            set { _Billtoemail = value; }
        }
        public string Billtostreet
        {
            get { return _Billtostreet; }
            set { _Billtostreet = value; }
        }
        public string Billtocountry
        {
            get { return _Billtocountry; }
            set { _Billtocountry = value; }
        }
        public string Billtozip
        {
            get { return _Billtozip; }
            set { _Billtozip = value; }
        }
        public string Billtophone
        {
            get { return _Billtophone; }
            set { _Billtophone = value; }
        }
        public string Cardtype
        {
            get { return _Cardtype; }
            set { _Cardtype = value; }
        }
        public string Method
        {
            get { return _Method; }
            set { _Method = value; }
        }
        public string Respmsg
        {
            get { return _Respmsg; }
            set { _Respmsg = value; }
        }
        public string Result
        {
            get { return _Result; }
            set { _Result = value; }
        }
        public DateTime? PaidDateTime
        {
            get { return _PaidDateTime; }
            set { _PaidDateTime = value; }
        }
        #endregion public properties
    }

    public class PaymentReportSearchFilter
    {
        #region private properties
        private string _FullName;
        private string _PrevSNumber;
        private string _AddressLine1;
        private string _AddressLine2;
        private string _City;
        private string _Province;
        private string _PostalCode;
        private string _Country;
        private string _AreaCode;
        private string _PhoneNumber;
        private DateTime? _BirthDate;
        private string _EmailAddress;
        private string _TermCode;
        private string _ProgramCode;
        private string _CampusCode;
        private string _LanguageCode;
        private string _PaypalResponseId;
        private string _Uuid;
        private string _BilltoName;
        private string _BilltoEmail;
        private string _BilltoStreet;
        private string _BilltoCountry;
        private string _BilltoZip;
        private string _BilltoPhone;
        private string _CardType;
        private string _Method;
        private string _RespMsg;
        private string _Result;
        private DateTime? _PaidDateTime;
        #endregion private properties

        #region public properties
        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        [Display(Name = "Prev SNumber")]
        public string PrevSNumber
        {
            get { return _PrevSNumber; }
            set { _PrevSNumber = value; }
        }

        [Display(Name = "Address Line1")]
        public string AddressLine1
        {
            get { return _AddressLine1; }
            set { _AddressLine1 = value; }
        }

        [Display(Name = "Address Line2")]
        public string AddressLine2
        {
            get { return _AddressLine2; }
            set { _AddressLine2 = value; }
        }

        [Display(Name = "City")]
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        [Display(Name = "Province")]
        public string Province
        {
            get { return _Province; }
            set { _Province = value; }
        }

        [Display(Name = "Postal Code")]
        public string PostalCode
        {
            get { return _PostalCode; }
            set { _PostalCode = value; }
        }

        [Display(Name = "Country")]
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        [Display(Name = "Area Code")]
        public string AreaCode
        {
            get { return _AreaCode; }
            set { _AreaCode = value; }
        }

        [Display(Name = "Phone Number")]
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        [Display(Name = "Birth Date")]
        public DateTime? BirthDate
        {
            get { return _BirthDate; }
            set { _BirthDate = value; }
        }

        [Display(Name = "Email Address")]
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

        [Display(Name = "Term")]
        public string TermCode
        {
            get { return _TermCode; }
            set { _TermCode = value; }
        }

        [Display(Name = "Program")]
        public string ProgramCode
        {
            get { return _ProgramCode; }
            set { _ProgramCode = value; }
        }

        [Display(Name = "Campus")]
        public string CampusCode
        {
            get { return _CampusCode; }
            set { _CampusCode = value; }
        }

        [Display(Name = "Lang")]
        public string LanguageCode
        {
            get { return _LanguageCode; }
            set { _LanguageCode = value; }
        }

        [Display(Name = "Paypal ID")]
        public string PaypalResponseId
        {
            get { return _PaypalResponseId; }
            set { _PaypalResponseId = value; }
        }

        [Display(Name = "Uuid")]
        public string Uuid
        {
            get { return _Uuid; }
            set { _Uuid = value; }
        }

        [Display(Name = "B. Name")]
        public string BilltoName
        {
            get { return _BilltoName; }
            set { _BilltoName = value; }
        }

        [Display(Name = "B. Email")]
        public string BilltoEmail
        {
            get { return _BilltoEmail; }
            set { _BilltoEmail = value; }
        }

        [Display(Name = "B. Street")]
        public string BilltoStreet
        {
            get { return _BilltoStreet; }
            set { _BilltoStreet = value; }
        }

        [Display(Name = "B. Country")]
        public string BilltoCountry
        {
            get { return _BilltoCountry; }
            set { _BilltoCountry = value; }
        }

        [Display(Name = "B. Zipcode")]
        public string BilltoZip
        {
            get { return _BilltoZip; }
            set { _BilltoZip = value; }
        }

        [Display(Name = "B. Phone")]
        public string BilltoPhone
        {
            get { return _BilltoPhone; }
            set { _BilltoPhone = value; }
        }

        [Display(Name = "Card Type")]
        public string CardType
        {
            get { return _CardType; }
            set { _CardType = value; }
        }

        [Display(Name = "Method")]
        public string Method
        {
            get { return _Method; }
            set { _Method = value; }
        }

        [Display(Name = "Respond Msg")]
        public string RespMsg
        {
            get { return _RespMsg; }
            set { _RespMsg = value; }
        }

        [Display(Name = "Result")]
        public string Result
        {
            get { return _Result; }
            set { _Result = value; }
        }

        [Display(Name = "Paid DateTime")]
        public DateTime? PaidDateTime
        {
            get { return _PaidDateTime; }
            set { _PaidDateTime = value; }
        }
        #endregion public properties

    }
    public class PaymentReportSearchResultsFilter
    {
        #region private properties
        private string _FullName;
        private string _PrevSNumber;
        private string _AddressLine1;
        private string _AddressLine2;
        private string _City;
        private string _Province;
        private string _PostalCode;
        private string _Country;
        private string _AreaCode;
        private string _PhoneNumber;
        private DateTime? _BirthDate;
        private string _EmailAddress;
        private string _TermCode;
        private string _ProgramCode;
        private string _CampusCode;
        private string _LanguageCode;
        private string _PaypalResponseId;
        private string _Uuid;
        private string _BilltoName;
        private string _Billtoemail;
        private string _Billtostreet;
        private string _Billtocountry;
        private string _Billtozip;
        private string _Billtophone;
        private string _Cardtype;
        private string _Method;
        private string _Respmsg;
        private string _Result;
        private DateTime? _PaidDateTime;
        #endregion private properties

        #region public properties
        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        [Display(Name = "Pre SNumber")]
        public string PrevSNumber
        {
            get { return _PrevSNumber; }
            set { _PrevSNumber = value; }
        }

        [Display(Name = "Address Line1")]
        public string AddressLine1
        {
            get { return _AddressLine1; }
            set { _AddressLine1 = value; }
        }

        [Display(Name = "Address Line2")]
        public string AddressLine2
        {
            get { return _AddressLine2; }
            set { _AddressLine2 = value; }
        }

        [Display(Name = "City")]
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        [Display(Name = "Province")]
        public string Province
        {
            get { return _Province; }
            set { _Province = value; }
        }

        [Display(Name = "Postal Code")]
        public string PostalCode
        {
            get { return _PostalCode; }
            set { _PostalCode = value; }
        }

        [Display(Name = "Country")]
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        [Display(Name = "Area Code")]
        public string AreaCode
        {
            get { return _AreaCode; }
            set { _AreaCode = value; }
        }

        [Display(Name = "Phone Number")]
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        [Display(Name = "Birth Date")]
        public DateTime? BirthDate
        {
            get { return _BirthDate; }
            set { _BirthDate = value; }
        }

        [Display(Name = "Email Address")]
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

        [Display(Name = "Term")]
        public string TermCode
        {
            get { return _TermCode; }
            set { _TermCode = value; }
        }

        [Display(Name = "Program")]
        public string ProgramCode
        {
            get { return _ProgramCode; }
            set { _ProgramCode = value; }
        }

        [Display(Name = "Campus")]
        public string CampusCode
        {
            get { return _CampusCode; }
            set { _CampusCode = value; }
        }

        [Display(Name = "Lang")]
        public string LanguageCode
        {
            get { return _LanguageCode; }
            set { _LanguageCode = value; }
        }

        [Display(Name = "Paypal ID")]
        public string PaypalResponseId
        {
            get { return _PaypalResponseId; }
            set { _PaypalResponseId = value; }
        }

        [Display(Name = "Uuid")]
        public string Uuid
        {
            get { return _Uuid; }
            set { _Uuid = value; }
        }

        [Display(Name = "Billing Name")]
        public string BilltoName
        {
            get { return _BilltoName; }
            set { _BilltoName = value; }
        }

        [Display(Name = "B. email")]
        public string Billtoemail
        {
            get { return _Billtoemail; }
            set { _Billtoemail = value; }
        }

        [Display(Name = "B. street")]
        public string Billtostreet
        {
            get { return _Billtostreet; }
            set { _Billtostreet = value; }
        }

        [Display(Name = "B. country")]
        public string Billtocountry
        {
            get { return _Billtocountry; }
            set { _Billtocountry = value; }
        }

        [Display(Name = "B. zip")]
        public string Billtozip
        {
            get { return _Billtozip; }
            set { _Billtozip = value; }
        }

        [Display(Name = "B. phone")]
        public string Billtophone
        {
            get { return _Billtophone; }
            set { _Billtophone = value; }
        }

        [Display(Name = "Card type")]
        public string Cardtype
        {
            get { return _Cardtype; }
            set { _Cardtype = value; }
        }

        [Display(Name = "Method")]
        public string Method
        {
            get { return _Method; }
            set { _Method = value; }
        }

        [Display(Name = "Respond msg")]
        public string Respmsg
        {
            get { return _Respmsg; }
            set { _Respmsg = value; }
        }

        [Display(Name = "Result")]
        public string Result
        {
            get { return _Result; }
            set { _Result = value; }
        }

        [Display(Name = "Paid DateTime")]
        public DateTime? PaidDateTime
        {
            get { return _PaidDateTime; }
            set { _PaidDateTime = value; }
        }
        #endregion public properties

    }


    // HoldType Report Object
    public class HoldTypeReportViewObj : RequestListViewObj
    {
        #region private variables
        private List<HoldTypeReportSearchResultsFilter> _HoldTypeReportSearchResultsFilter;
        private HoldTypeReportSearchFilter _HoldTypeReportSearchFilter;
        //private PaginationFilter _PaginationFilter;
        //private string _PageTitle;

        private List<HoldTypeType?> _ReportHoldTypes;
        private List<SelectListItem> _ReportViewHoldTypes;

        private LcapasLogic lcapasLogic = new LcapasLogic();
        #endregion private variables

        public HoldTypeReportViewObj()
        {
            isInitialized = true;
            //_HoldTypeReportSearchFilter = this._HoldTypeReportSearchFilter ?? new HoldTypeReportSearchFilter();
            //_HoldTypeReportSearchResultsFilter = this._HoldTypeReportSearchResultsFilter ?? new List<HoldTypeReportSearchResultsFilter>();
            //_PaginationFilter = this.Pagination ?? new PaginationFilter();
            if (_ReportViewHoldTypes == null) { _ReportViewHoldTypes = new List<SelectListItem>(); }
        }

        public bool Load()
        {
            bool success = false;
            try
            {
                Pagination.PageTitle = Structs.ReportPageTitle.HoldTypeReports;

                if (!_ReportViewHoldTypes.Any())
                {
                    _ReportHoldTypes = Enum.GetValues(typeof(HoldTypeType)).Cast<HoldTypeType?>().Where(x => x.Value != HoldTypeType.Now).ToList();
                    _ReportViewHoldTypes = _ReportHoldTypes.Select(s => new SelectListItem { Text = s.ToString(), Value = ((int)s.Value).ToString() }).ToList();
                }

                success = lcapasLogic.LoadTransferRequests(this, true);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "HoldTypeReportViewObj.Load", "Error", ex.ToString());
            }
            return success;
        }

        public byte[] Export(string[] reportID, string destAction = null, string filterFields = null)
        {
            var csvFile = new CsvExport();

            try
            {
                List<HoldTypeReportSearchResultsFilter> _HoldTypeList = lcapasLogic.GetHoldTypeReportByIds(reportID, destAction, filterFields);

                foreach (HoldTypeReportSearchResultsFilter _HoldType in _HoldTypeList)
                {
                    csvFile.AddRow();
                    csvFile["sNumber"] = _HoldType.SNumber;
                    csvFile["Full Name"] = _HoldType.FullName;
                    csvFile["ASN"] = _HoldType.ASN;
                    if (_HoldType.DestAction == Structs.DestActions.AdmissionsOutbox)
                    {
                        csvFile["To Fulfilling Institution"] = _HoldType.FromRequestingInstitution;
                    } else
                    {
                        csvFile["From Requesting Institution"] = _HoldType.FromRequestingInstitution;
                    }
                    csvFile["Colleague Status"] = _HoldType.ColleagueStatus;
                    csvFile["Hold Type"] = _HoldType.HoldType;
                    csvFile["Hold Type Data"] = _HoldType.HoldTypeData;
                    csvFile["Response Status"] = _HoldType.ResponseStatus;
                    csvFile["Operator"] = _HoldType.Operator;
                    csvFile["Status Date"] = _HoldType.FromStatus.Value.ToString("yyyy/MM/dd");
                    if (_HoldType.TranscriptCount > 0 ||
                        (_HoldType.ResponseCount > 0 &&
                            (_HoldType.ResponseStatus == ResponseStatusType.Canceled.ToString() ||
                                _HoldType.ResponseStatus == ResponseStatusType.OfflineRecordSent.ToString())))
                    {
                        csvFile["Status"] = "Transcript Sent";
                    }
                    else if (_HoldType.ResponseCount > 0)
                    {
                        csvFile["Status"] = "Response Sent";
                    }
                    else if (_HoldType.ErrorMessageCount > 0)
                    {
                        csvFile["Status"] = "Error Message";
                    } else
                    {
                        csvFile["Status"] = "";
                    }
                    if (_HoldType.DestAction == Structs.DestActions.RecordsInbox)
                    {
                        csvFile["Sent TRRQ"] = _HoldType.SentTRRQ;
                    }
                    csvFile["Pending"] = _HoldType.ViewedDateTime == null ? "Not Viewed/Pending" : "Viewed";
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "HoldTypeReportViewObj.Export", "Error", ex.ToString());
            }

            return csvFile.ExportToBytes();
        }

        public bool isInitialized;

        //public List<HoldTypeReportSearchResultsFilter> HoldTypeReportSearchResultsFilter { get { return _HoldTypeReportSearchResultsFilter; } set { _HoldTypeReportSearchResultsFilter = value; } }

        //public HoldTypeReportSearchFilter HoldTypeReportSearchFilter { get { return _HoldTypeReportSearchFilter; } set { _HoldTypeReportSearchFilter = value; } }

        //public string _PageTitle { get { return _PageTitle; } set { _PageTitle = value; } }

        //public PaginationFilter Pagination { get { return PaginationFilter; } set { PaginationFilter = value; } }

        public IEnumerable<SelectListItem> ReportViewHoldTypes { get { return _ReportViewHoldTypes; } }
    }

    //public class HoldTypeReportObj
    //{
    //    #region private properties
    //    private string _FullName = string.Empty;
    //    private string _SNumber = string.Empty;
    //    private string _ASN = string.Empty;
    //    private string _FromRequestingInstitution = string.Empty;
    //    private string _ColleagueStatus = string.Empty;
    //    private string _HoldType = string.Empty;
    //    private string _HoldTypeData = string.Empty;
    //    private string _ResponseStatus = string.Empty;
    //    private string _Operator = string.Empty;
    //    private DateTime _CreateDateTime;
    //    private int _ResponseCount = 0;
    //    private int _TranscriptCount = 0;
    //    private int _ErrorMessageCount = 0;
    //    #endregion private properties

    //    #region public properties
    //    public string FullName
    //    {
    //        get { return _FullName; }
    //        set { _FullName = value; }
    //    }
    //    public string SNumber
    //    {
    //        get { return _SNumber; }
    //        set { _SNumber = value; }
    //    }
    //    public string ASN
    //    {
    //        get { return _ASN; }
    //        set { _ASN = value; }
    //    }
    //    public string FromRequestingInstitution
    //    {
    //        get { return _FromRequestingInstitution; }
    //        set { _FromRequestingInstitution = value; }
    //    }
    //    public string ColleagueStatus
    //    {
    //        get { return _ColleagueStatus; }
    //        set { _ColleagueStatus = value; }
    //    }
    //    public string HoldType
    //    {
    //        get { return _HoldType; }
    //        set { _HoldType = value; }
    //    }
    //    public string HoldTypeData
    //    {
    //        get { return _HoldTypeData; }
    //        set { _HoldTypeData = value; }
    //    }
    //    public string ResponseStatus
    //    {
    //        get { return _ResponseStatus; }
    //        set { _ResponseStatus = value; }
    //    }
    //    public string Operator
    //    {
    //        get { return _Operator; }
    //        set { _Operator = value; }
    //    }
    //    public DateTime CreateDateTime
    //    {
    //        get { return _CreateDateTime; }
    //        set { _CreateDateTime = value; }
    //    }

    //    public int ResponseCount
    //    {
    //        get { return _ResponseCount; }
    //        set { _ResponseCount = value; }
    //    }

    //    public int TranscriptCount
    //    {
    //        get { return _TranscriptCount; }
    //        set { _TranscriptCount = value; }
    //    }

    //    public int ErrorMessageCount
    //    {
    //        get { return _ErrorMessageCount; }
    //        set { _ErrorMessageCount = value; }
    //    }

    //    #endregion public properties
    //}

    public class HoldTypeReportSearchFilter
    {
        private string _FullName;
        private string _SNumber;
        private string _ASN;
        private string _FromRequestingInstitution;
        private string _ColleagueStatus;
        private string _HoldType;
        private string _HoldTypeData;
        private string _ResponseStatus;
        private string _Operator;
        private DateTime? _FromStatus;
        private DateTime? _ToStatus;

        #region public properties
        [Display(Name = "LName, FName, MName")]
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        [Display(Name = "Person ID")]
        public string SNumber
        {
            get { return _SNumber; }
            set { _SNumber = value; }
        }

        [Display(Name = "ASN")]
        public string ASN
        {
            get { return _ASN; }
            set { _ASN = value; }
        }

        [Display(Name = "From Requesting Institution")]
        public string FromRequestingInstitution
        {
            get { return _FromRequestingInstitution; }
            set { _FromRequestingInstitution = value; }
        }

        [Display(Name = "Colleague Status")]
        public string ColleagueStatus
        {
            get { return _ColleagueStatus; }
            set { _ColleagueStatus = value; }
        }

        [Display(Name = "Hold Type")]
        public string HoldType
        {
            get { return _HoldType; }
            set { _HoldType = value; }
        }

        [Display(Name = "Hold Type Data")]
        public string HoldTypeData
        {
            get { return _HoldTypeData; }
            set { _HoldTypeData = value; }
        }

        [Display(Name = "Response Status")]
        public string ResponseStatus
        {
            get { return _ResponseStatus; }
            set { _ResponseStatus = value; }
        }

        [Display(Name = "Operator")]
        public string Operator
        {
            get { return _Operator; }
            set { _Operator = value; }
        }

        [Display(Name = "From Status")]
        public DateTime? FromStatus
        {
            get { return _FromStatus; }
            set { _FromStatus = value; }
        }

        [Display(Name = "To Status")]
        public DateTime? ToStatus
        {
            get { return _ToStatus; }
            set { _ToStatus = value; }
        }

        #endregion public properties

    }
    public class HoldTypeReportSearchResultsFilter
    {
        #region private properties
        private string _FullName;
        private string _SNumber;
        private string _ASN;
        private string _FromRequestingInstitution;
        private string _DestAction;
        private string _ColleagueStatus;
        private string _HoldType;
        private string _HoldTypeData;
        private string _ResponseStatus;
        private string _Operator;
        private DateTime? _FromStatus;
        //private DateTime? _ToStatus;
        private int? _ResponseCount;
        private int? _TranscriptCount;
        private int? _ErrorMessageCount;
        private DateTime? _SentTRRQ;
        private DateTime? _ViewedDateTime;
        private DateTime? _SentDateTime;
        private DateTime? _ReceivedDateTime;
        private DateTime? _CreatedDateTime;
        #endregion private properties

        #region private variables
        [Display(Name = "LName, FName, MName")]
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        [Display(Name = "Person ID")]
        public string SNumber
        {
            get { return _SNumber; }
            set { _SNumber = value; }
        }

        [Display(Name = "ASN")]
        public string ASN
        {
            get { return _ASN; }
            set { _ASN = value; }
        }

        [Display(Name = "From Requesting Institution")]
        public string FromRequestingInstitution
        {
            get { return _FromRequestingInstitution; }
            set { _FromRequestingInstitution = value; }
        }

        [Display(Name = "Colleague Status")]
        public string ColleagueStatus
        {
            get { return _ColleagueStatus; }
            set { _ColleagueStatus = value; }
        }

        [Display(Name = "Hold Type")]
        public string HoldType
        {
            get { return _HoldType; }
            set { _HoldType = value; }
        }

        [Display(Name = "Hold Type Data")]
        public string HoldTypeData
        {
            get { return _HoldTypeData; }
            set { _HoldTypeData = value; }
        }

        [Display(Name = "Response Status")]
        public string ResponseStatus
        {
            get { return _ResponseStatus; }
            set { _ResponseStatus = value; }
        }

        [Display(Name = "Operator")]
        public string Operator
        {
            get { return _Operator; }
            set { _Operator = value; }
        }

        [Display(Name = "From Status")]
        public DateTime? FromStatus
        {
            get { return _FromStatus; }
            set { _FromStatus = value; }
        }

        //[Display(Name = "To Status")]
        //public DateTime? ToStatus
        //{
        //    get { return _ToStatus; }
        //    set { _ToStatus = value; }
        //}

        [Display(Name = "Dest Action")]
        public string DestAction
        {
            get { return _DestAction; }
            set { _DestAction = value; }
        }

        [Display(Name = "Response Count")]
        public int? ResponseCount
        {
            get { return _ResponseCount; }
            set { _ResponseCount = value; }
        }

        [Display(Name = "Transcript Count")]
        public int? TranscriptCount
        {
            get { return _TranscriptCount; }
            set { _TranscriptCount = value; }
        }

        [Display(Name = "Error Message Count")]
        public int? ErrorMessageCount
        {
            get { return _ErrorMessageCount; }
            set { _ErrorMessageCount = value; }
        }

        [Display(Name = "Sent TRRQ")]
        public DateTime? SentTRRQ
        {
            get { return _SentTRRQ; }
            set { _SentTRRQ = value; }
        }

        [Display(Name = "Viewed DateTime")]
        public DateTime? ViewedDateTime
        {
            get { return _ViewedDateTime; }
            set { _ViewedDateTime = value; }
        }

        [Display(Name = "Sent DateTime")]
        public DateTime? SentDateTime
        {
            get { return _SentDateTime; }
            set { _SentDateTime = value; }
        }

        [Display(Name = "Received DateTime")]
        public DateTime? ReceivedDateTime
        {
            get { return _ReceivedDateTime; }
            set { _ReceivedDateTime = value; }
        }

        [Display(Name = "Created DateTime")]
        public DateTime? CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }
        
        #endregion public properties

    }

    // Application Report Object
    public class CollProgramApplicationReportViewObj : IDisposable
    {
        // Aplication Report Object
        private List<CollProgramApplicationReportSearchResultsFilter> _CollProgramApplicationReportSearchResultsFilter;
        private CollProgramApplicationReportSearchFilter _CollProgramApplicationReportSearchFilter;
        private PaginationFilter _PaginationFilter;
        private string _PageTitle;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        private List<SelectListItem> _ColleagueTermSelectedList;
        private List<SelectListItem> _ColleagueProgramSelectedList;
        private List<SelectListItem> _ColleagueLocationSelectedList;
        private List<SelectListItem> _ColleagueAdmissionStatusSelectedList;
        private List<SelectListItem> _ColleagueAlienStatusSelectedList;
        private List<SelectListItem> _ColleagueApplicationStatusSelectedList;
        private List<SelectListItem> _ColleagueResidenceCountrySelectedList;
        private List<SelectListItem> _ColleagueCitizenshipCountrySelectedList;
        private List<SelectListItem> _ColleagueEthnicSelectedList;

        public CollProgramApplicationReportViewObj()
        {
            isInitialized = true;

            _CollProgramApplicationReportSearchFilter = this._CollProgramApplicationReportSearchFilter ?? new CollProgramApplicationReportSearchFilter();
            _CollProgramApplicationReportSearchResultsFilter = this._CollProgramApplicationReportSearchResultsFilter ?? new List<CollProgramApplicationReportSearchResultsFilter>();
            _PaginationFilter = this.Pagination ?? new PaginationFilter();
        }

        public void Dispose()
        {
            lcapasLogic.Dispose();
            this.Dispose();
        }

        public bool Load()
        {
            bool success = false;

            try
            {
                _PageTitle = Structs.ReportPageTitle.CollProgramApplicationReports;

                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    //_ColleagueTermSelectedList = collLogic.GetColleagueTerms();
                    if (_ColleagueTermSelectedList == null)
                    {
                        _ColleagueTermSelectedList = new List<SelectListItem>();

                        _ColleagueTermSelectedList = collLogic.GetColleagueTerms(true);
                        if (this.CollProgramApplicationReportSearchFilter.Term == null || !this.CollProgramApplicationReportSearchFilter.Term.Any())
                        {
                            // Adding current Term as default search
                            if (this.CollProgramApplicationReportSearchFilter.Term == null) { this.CollProgramApplicationReportSearchFilter.Term = new List<string>(); }
                            this.CollProgramApplicationReportSearchFilter.Term.Add(Functions.GetLcSessionDesignator(Functions.GetLcCurrentTerm()));
                        }
                    }
                    if (_ColleagueProgramSelectedList == null) { _ColleagueProgramSelectedList = new List<SelectListItem>(); }

                    _ColleagueProgramSelectedList = collLogic.GetColleaguePrograms(true);
                    _ColleagueLocationSelectedList = collLogic.GetColleagueApplicationLocations();
                    _ColleagueAdmissionStatusSelectedList = collLogic.GetColleagueAdmissionStatus();
                    _ColleagueAlienStatusSelectedList = collLogic.GetColleagueAlienStatus();
                    _ColleagueApplicationStatusSelectedList = collLogic.GetColleagueApplicationStatus();
                    _ColleagueResidenceCountrySelectedList = collLogic.GetColleagueResidenceCountry();
                    _ColleagueCitizenshipCountrySelectedList = collLogic.GetColleagueCitizenshipCountry();
                    _ColleagueEthnicSelectedList = collLogic.GetColleagueEthnic();

                    success = collLogic.CollProgramApplicationReports(this);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "ApplicationReportViewObj.Load", "Error", ex.ToString());
            }
            return success;
        }


        public byte[] Export(string[] reportID, bool allSelected = false, string filterFields = null)
        {
            var csvFile = new CsvExport();

            try
            {
                List<CollProgramApplicationReportSearchResultsFilter> _ApplicationPrograms = new List<CollProgramApplicationReportSearchResultsFilter>();

                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    _ApplicationPrograms = collLogic.GetProgramApplicationReportByIds(reportID, allSelected, filterFields);
                }

                foreach (CollProgramApplicationReportSearchResultsFilter _ApplicationProgram in _ApplicationPrograms)
                {
                    csvFile.AddRow();

                    csvFile["Appl. ID"] = _ApplicationProgram.ID;
                    csvFile["Person ID"] = _ApplicationProgram.PID;
                    csvFile["LName, FName, MName"] = _ApplicationProgram.FullName;
                    csvFile["Program"] = _ApplicationProgram.Program;
                    csvFile["Email Address"] = _ApplicationProgram.EmailAddress;
                    csvFile["ASN"] = _ApplicationProgram.ASN;
                    csvFile["Terms"] = _ApplicationProgram.Term;
                    csvFile["Study Load"] = _ApplicationProgram.StudyLoad;
                    csvFile["Location"] = _ApplicationProgram.Location;
                    //csvFile["Visa Status"] = _ApplicationProgram.VisaStatus;
                    csvFile["Citizenship"] = _ApplicationProgram.Citizenship;
                    csvFile["Ethnic"] = _ApplicationProgram.Ethnic;
                    csvFile["Admit Status"] = _ApplicationProgram.AdmitStatus;
                    csvFile["Alien Status"] = _ApplicationProgram.AlienStatus;
                    csvFile["Origin Country"] = _ApplicationProgram.OrgCountry;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "CollProgramApplicationReportViewObj.Export", "Error", ex.ToString());
            }

            return csvFile.ExportToBytes();
        }

        public bool isInitialized;

        public List<CollProgramApplicationReportSearchResultsFilter> CollProgramApplicationReportSearchResultsFilter { get { return _CollProgramApplicationReportSearchResultsFilter; } set { _CollProgramApplicationReportSearchResultsFilter = value; } }

        public CollProgramApplicationReportSearchFilter CollProgramApplicationReportSearchFilter { get { return _CollProgramApplicationReportSearchFilter; } set { _CollProgramApplicationReportSearchFilter = value; } }

        public string PageTitle { get { return _PageTitle; } set { _PageTitle = value; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }

        public List<SelectListItem> ColleagueTermSelectedList { get { return _ColleagueTermSelectedList; } }
        public List<SelectListItem> ColleagueProgramSelectedList { get { return _ColleagueProgramSelectedList; } }
        public List<SelectListItem> ColleagueLocationSelectedList { get { return _ColleagueLocationSelectedList; } }
        public List<SelectListItem> ColleagueAdmissionStatusSelectedList { get { return _ColleagueAdmissionStatusSelectedList; } }
        public List<SelectListItem> ColleagueApplicationStatusSelectedList { get { return _ColleagueApplicationStatusSelectedList; } }
        public List<SelectListItem> ColleagueAlienStatusSelectedList { get { return _ColleagueAlienStatusSelectedList; } }
        public List<SelectListItem> ColleagueResidenceCountrySelectedList { get { return _ColleagueResidenceCountrySelectedList; } }
        public List<SelectListItem> ColleagueCitizenshipCountrySelectedList { get { return _ColleagueCitizenshipCountrySelectedList; } }
        public List<SelectListItem> ColleagueEthnicSelectedList { get { return _ColleagueEthnicSelectedList; } }
    }

    public class CollProgramApplicationReportObj
    {
        #region private properties
        private string _ID = string.Empty;
        private string _PID = string.Empty;
        private string _FullName = string.Empty;
        private string _EmailAddress = string.Empty;
        private string _ASN = string.Empty;
        private string _StudyLoad = string.Empty;
        private string _Program = string.Empty;
        private string _Term = string.Empty;
        private string _Location = string.Empty;
        private string _AdmitStatus = string.Empty;
        private string _VisaStatus = string.Empty;
        private string _Citizenship = string.Empty;
        private string _ApplicationStatus = string.Empty;
        private string _AlienStatus = string.Empty;
        private string _OrgCountry = string.Empty;


        #endregion private properties

        #region public properties
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        
        public string PID
        {
            get { return _PID; }
            set { _PID = value; }
        }
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        public string ASN
        {
            get { return _ASN; }
            set { _ASN = value; }
        }
        public string StudyLoad
        {
            get { return _StudyLoad; }
            set { _StudyLoad = value; }
        }
        public string Program
        {
            get { return _Program; }
            set { _Program = value; }
        }
        public string Term
        {
            get { return _Term; }
            set { _Term = value; }
        }
        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }
        public string AdmitStatus
        {
            get { return _AdmitStatus; }
            set { _AdmitStatus = value; }
        }
        public string Citizenship
        {
            get { return _Citizenship; }
            set { _Citizenship = value; }
        }
        public string VisaStatus
        {
            get { return _VisaStatus; }
            set { _VisaStatus = value; }
        }
        public string ApplicationStatus
        {
            get { return _ApplicationStatus; }
            set { _ApplicationStatus = value; }
        }
        public string AlienStatus
        {
            get { return _AlienStatus; }
            set { _AlienStatus = value; }
        }

        public string OrgCountry
        {
            get { return _OrgCountry; }
            set { _OrgCountry = value; }
        }
        #endregion public properties
    }

    public class CollProgramApplicationReportSearchFilter
    {
        private string _ID;
        private string _PID;
        private string _FullName;
        private string _EmailAddress;
        private string _ASN;
        private string _StudyLoad;
        private string _Program;
        private List<string> _Term;
        private string _Location;
        private List<string> _AdmitStatus;
        private string _VisaStatus;
        private List<string> _Citizenship;
        private List<string> _ApplicationStatus;
        private List<string> _AlienStatus;
        private List<string> _OrgCountry;
        private List<string> _Ethnic;

        #region public properties
        [Display(Name = "Appl. ID")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [Display(Name = "Person ID")]
        public string PID
        {
            get { return _PID; }
            set { _PID = value; }
        }
        [Display(Name = "LName, FName, MName")]
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        [Display(Name = "Email Address")]
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

        [Display(Name = "ASN")]
        public string ASN
        {
            get { return _ASN; }
            set { _ASN = value; }
        }

        [Display(Name = "Study Load")]
        public string StudyLoad
        {
            get { return _StudyLoad; }
            set { _StudyLoad = value; }
        }

        [Display(Name = "Program")]
        public string Program
        {
            get { return _Program; }
            set { _Program = value; }
        }

        [Display(Name = "Term")]
        public List<string> Term
        {
            get { return _Term; }
            set { _Term = value; }
        }

        [Display(Name = "Campus")]
        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        [Display(Name = "Admission Status")]
        public List<string> AdmitStatus
        {
            get { return _AdmitStatus; }
            set { _AdmitStatus = value; }
        }

        [Display(Name = "Visa Status")]
        public string VisaStatus
        {
            get { return _VisaStatus; }
            set { _VisaStatus = value; }
        }

        [Display(Name = "Citizenship Country")]
        public List<string> Citizenship
        {
            get { return _Citizenship; }
            set { _Citizenship = value; }
        }

        [Display(Name = "Application Status")]
        public List<string> ApplicationStatus
        {
            get { return _ApplicationStatus; }
            set { _ApplicationStatus = value; }
        }

        [Display(Name = "Alien Status")]
        public List<string> AlienStatus
        {
            get { return _AlienStatus; }
            set { _AlienStatus = value; }
        }

        [Display(Name = "Residence Country")]
        public List<string> OrgCountry
        {
            get { return _OrgCountry; }
            set { _OrgCountry = value; }
        }

        [Display(Name = "Ethnic")]
        public List<string> Ethnic
        {
            get { return _Ethnic; }
            set { _Ethnic = value; }
        }

        #endregion public properties

    }

    public class CollProgramApplicationReportSearchResultsFilter
    {
        private string _ID;
        private string _PID;
        private string _FullName;
        private string _EmailAddress;
        private string _ASN;
        private string _StudyLoad;
        private string _Program;
        private string _Term;
        private string _Location;
        private string _AdmitStatus;
        private string _ApplicationStatus;
        private string _VisaStatus;
        private string _Citizenship;
        private string _AlienStatus;
        private string _OrgCountry;
        private string _Ethnic;

        #region public properties
        [Display(Name = "Appl. ID")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Display(Name = "Person ID")]
        public string PID
        {
            get { return _PID; }
            set { _PID = value; }
        }
        [Display(Name = "LName, FName, MName")]
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        [Display(Name = "Email Address")]
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

        [Display(Name = "ASN")]
        public string ASN
        {
            get { return _ASN; }
            set { _ASN = value; }
        }

        [Display(Name = "Study Load")]
        public string StudyLoad
        {
            get { return _StudyLoad; }
            set { _StudyLoad = value; }
        }

        [Display(Name = "Program")]
        public string Program
        {
            get { return _Program; }
            set { _Program = value; }
        }

        [Display(Name = "Term")]
        public string Term
        {
            get { return _Term; }
            set { _Term = value; }
        }

        [Display(Name = "Campus")]
        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        [Display(Name = "Admission Status")]
        public string AdmitStatus
        {
            get { return _AdmitStatus; }
            set { _AdmitStatus = value; }
        }
        [Display(Name = "Visa Status")]
        public string VisaStatus
        {
            get { return _VisaStatus; }
            set { _VisaStatus = value; }
        }

        [Display(Name = "Citizenship Contry")]
        public string Citizenship
        {
            get { return _Citizenship; }
            set { _Citizenship = value; }
        }

        [Display(Name = "Application Status")]
        public string ApplicationStatus
        {
            get { return _ApplicationStatus; }
            set { _ApplicationStatus = value; }
        }

        [Display(Name = "Alien Status")]
        public string AlienStatus
        {
            get { return _AlienStatus; }
            set { _AlienStatus = value; }
        }

        [Display(Name = "Residence Country")]
        public string OrgCountry
        {
            get { return _OrgCountry; }
            set { _OrgCountry = value; }
        }

        [Display(Name = "Ethnic")]
        public string Ethnic
        {
            get { return _Ethnic; }
            set { _Ethnic = value; }
        }

        #endregion public properties
    }


    // WaitingList Report Object
    public class CollWaitListReportViewObj : IDisposable
    {
        // Aplication Report Object
        private List<CollWaitListReportSearchResultsFilter> _CollWaitListReportSearchResultsFilter;
        private CollWaitListReportSearchFilter _CollWaitListReportSearchFilter;
        private PaginationFilter _PaginationFilter;
        private string _PageTitle;

        private LcapasLogic lcapasLogic = new LcapasLogic();
        private List<SelectListItem> _ColleaugeProgramSelectedList;

        public CollWaitListReportViewObj()
        {
            isInitialized = true;

            _CollWaitListReportSearchFilter = this._CollWaitListReportSearchFilter ?? new CollWaitListReportSearchFilter();
            _CollWaitListReportSearchResultsFilter = this._CollWaitListReportSearchResultsFilter ?? new List<CollWaitListReportSearchResultsFilter>();
            _PaginationFilter = this.Pagination ?? new PaginationFilter();
        }
        public void Dispose()
        {
            lcapasLogic.Dispose();
            this.Dispose();
        }

        public bool Load()
        {
            bool success = false;

            try
            {
                _PageTitle = Structs.ReportPageTitle.CollWaitListReports;

                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    _ColleaugeProgramSelectedList = collLogic.GetColleaguePrograms(true);

                    success = collLogic.CollWaitListReports(this);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "CollWaitListReportViewObj.Load", "Error", ex.ToString());
            }
            return success;
        }

        public byte[] Export(string[] reportID, bool allSelected = false, string filterFields = null)
        {
            var csvFile = new CsvExport();

            try
            {
                List<CollWaitListReportSearchResultsFilter> _WaitLists = new List<CollWaitListReportSearchResultsFilter>();

                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    _WaitLists = collLogic.GetWaitListReportByIds(reportID, allSelected, filterFields);
                }

                foreach (CollWaitListReportSearchResultsFilter WaitList in _WaitLists)
                {
                    csvFile.AddRow();

                    csvFile["Person ID"] = WaitList.APPL_APPLICANT;
                    csvFile["LName, FName, MName"] = WaitList.FullName;
                    csvFile["Term"] = WaitList.APPL_START_TERM;
                    csvFile["Application Status Date"] = WaitList.APPL_STATUS_DATE;
                    csvFile["Application Status Time"] = WaitList.APPL_STATUS_TIME.Value.ToString("hh:mm:ss tt");
                    csvFile["Citizenship Country"] = WaitList.APPL_CITIZENSHIP;
                    csvFile["Visa Status"] = WaitList.APPL_VISA_STATUS;
                    csvFile["program"] = WaitList.APPL_ACAD_PROGRAM;
                    csvFile["Residence Country"] = WaitList.APPL_RESIDENCE;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "CollWaitListReportViewObj.Export", "Error", ex.ToString());
            }

            return csvFile.ExportToBytes();
        }

        public bool isInitialized;

        public List<CollWaitListReportSearchResultsFilter> CollWaitListReportSearchResultsFilter { get { return _CollWaitListReportSearchResultsFilter; } set { _CollWaitListReportSearchResultsFilter = value; } }

        public CollWaitListReportSearchFilter CollWaitListReportSearchFilter { get { return _CollWaitListReportSearchFilter; } set { _CollWaitListReportSearchFilter = value; } }

        public string PageTitle { get { return _PageTitle; } set { _PageTitle = value; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }

        public List<SelectListItem> ColleagueProgramSelectedList { get { return _ColleaugeProgramSelectedList; } }
    }

    public class CollWaitListReportObj
    {
        #region private properties
        private string _APPL_APPLICATION_ID = string.Empty;
        private string _FullName = string.Empty;
        private string _APPL_START_TERM = string.Empty;
        private string _APPL_APPLICANT = string.Empty;
        private string _APPL_STATUS = string.Empty;
        private DateTime? _APPL_STATUS_DATE;
        private string _APPL_ACAD_PROGRAM = string.Empty;
        private string _APPL_CITIZENSHIP;
        private string _APPL_VISA_STATUS;
        private string _APPL_RESIDENCE = string.Empty;

        #endregion private properties

        #region public properties
        public string APPL_APPLICATION_ID
        {
            get { return _APPL_APPLICATION_ID; }
            set { _APPL_APPLICATION_ID = value; }
        }
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        public string APPL_START_TERM
        {
            get { return _APPL_START_TERM; }
            set { _APPL_START_TERM = value; }
        }
        public string APPL_APPLICANT
        {
            get { return _APPL_APPLICANT; }
            set { _APPL_APPLICANT = value; }
        }
        public string APPL_STATUS
        {
            get { return _APPL_STATUS; }
            set { _APPL_STATUS = value; }
        }
        public DateTime? APPL_STATUS_DATE
        {
            get { return _APPL_STATUS_DATE; }
            set { _APPL_STATUS_DATE = value; }
        }

        public string APPL_VISA_STATUS
        {
            get { return _APPL_VISA_STATUS; }
            set { _APPL_VISA_STATUS = value; }
        }

        public string APPL_CITIZENSHIP
        {
            get { return _APPL_CITIZENSHIP; }
            set { _APPL_CITIZENSHIP = value; }
        }
        public string APPL_ACAD_PROGRAM
        {
            get { return _APPL_ACAD_PROGRAM; }
            set { _APPL_ACAD_PROGRAM = value; }
        }

        public string APPL_RESIDENCE
        {
            get { return _APPL_RESIDENCE; }
            set { _APPL_RESIDENCE = value; }
        }
        
        #endregion public properties
    }

    public class CollWaitListReportSearchFilter
    {
        private string _APPL_APPLICATION_ID;
        private string _FullName;
        private string _APPL_START_TERM;
        private string _APPL_APPLICANT;
        private string _APPL_STATUS;
        private DateTime? _APPL_STATUS_DATE;
        private DateTime? _APPL_STATUS_TIME;
        private string _APPL_ACAD_PROGRAM;
        private string _APPL_CITIZENSHIP;
        private string _APPL_VISA_STATUS;
        private string _APPL_RESIDENCE;

        #region public properties
        [Display(Name = "Application ID")]
        public string APPL_APPLICATION_ID
        {
            get { return _APPL_APPLICATION_ID; }
            set { _APPL_APPLICATION_ID = value; }
        }

        [Display(Name = "LName, FName, MName")]
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        [Display(Name = "Term")]
        public string APPL_START_TERM
        {
            get { return _APPL_START_TERM; }
            set { _APPL_START_TERM = value; }
        }

        [Display(Name = "Person ID")]
        public string APPL_APPLICANT
        {
            get { return _APPL_APPLICANT; }
            set { _APPL_APPLICANT = value; }
        }

        [Display(Name = "Application Stat")]
        public string APPL_STATUS
        {
            get { return _APPL_STATUS; }
            set { _APPL_STATUS = value; }
        }

        [Display(Name = "Application Status Date")]
        public DateTime? APPL_STATUS_DATE
        {
            get { return _APPL_STATUS_DATE; }
            set { _APPL_STATUS_DATE = value; }
        }

        [Display(Name = "Application Status Time")]
        public DateTime? APPL_STATUS_TIME
        {
            get { return _APPL_STATUS_TIME; }
            set { _APPL_STATUS_TIME = value; }
        }

        [Display(Name = "Visa Status")]
        public string APPL_VISA_STATUS
        {
            get { return _APPL_VISA_STATUS; }
            set { _APPL_VISA_STATUS = value; }
        }
        [Display(Name = "Citizenship Country")]
        public string APPL_CITIZENSHIP
        {
            get { return _APPL_CITIZENSHIP; }
            set { _APPL_CITIZENSHIP = value; }
        }

        [Display(Name = "Program")]
        public string APPL_ACAD_PROGRAM
        {
            get { return _APPL_ACAD_PROGRAM; }
            set { _APPL_ACAD_PROGRAM = value; }
        }
        
        [Display(Name = "Residence Country")]
        public string APPL_RESIDENCE
        {
            get { return _APPL_RESIDENCE; }
            set { _APPL_RESIDENCE = value; }
        }
        #endregion public properties

    }

    public class CollWaitListReportSearchResultsFilter
    {
        private string _APPL_APPLICATION_ID;
            private string _FullName;
        private string _APPL_START_TERM;
        private string _APPL_APPLICANT;
        private string _APPL_STATUS;
        private DateTime? _APPL_STATUS_DATE;
        private DateTime? _APPL_STATUS_TIME;
        private string _APPL_ACAD_PROGRAM;
        private string _APPL_CITIZENSHIP;
        private string _APPL_VISA_STATUS;
        private string _APPL_RESIDENCE;

        #region public properties
        [Display(Name = "Application ID")]
        public string APPL_APPLICATION_ID
        {
            get { return _APPL_APPLICATION_ID; }
            set { _APPL_APPLICATION_ID = value; }
        }

        [Display(Name = "LName, FName, MName")]
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        [Display(Name = "Term")]
        public string APPL_START_TERM
        {
            get { return _APPL_START_TERM; }
            set { _APPL_START_TERM = value; }
        }

        [Display(Name = "Person ID")]
        public string APPL_APPLICANT
        {
            get { return _APPL_APPLICANT; }
            set { _APPL_APPLICANT = value; }
        }

        [Display(Name = "Application Stat")]
        public string APPL_STATUS
        {
            get { return _APPL_STATUS; }
            set { _APPL_STATUS = value; }
        }

        [Display(Name = "Application Status Date")]
        public DateTime? APPL_STATUS_DATE
        {
            get { return _APPL_STATUS_DATE; }
            set { _APPL_STATUS_DATE = value; }
        }

        [Display(Name = "Application Stat Time")]
        public DateTime? APPL_STATUS_TIME
        {
            get { return _APPL_STATUS_TIME; }
            set { _APPL_STATUS_TIME = value; }
        }
        [Display(Name = "Visa Status")]
        public string APPL_VISA_STATUS
        {
            get { return _APPL_VISA_STATUS; }
            set { _APPL_VISA_STATUS = value; }
        }
        [Display(Name = "Citizenship Country")]
        public string APPL_CITIZENSHIP
        {
            get { return _APPL_CITIZENSHIP; }
            set { _APPL_CITIZENSHIP = value; }
        }

        [Display(Name = "Program")]
        public string APPL_ACAD_PROGRAM
        {
            get { return _APPL_ACAD_PROGRAM; }
            set { _APPL_ACAD_PROGRAM = value; }
        }

        [Display(Name = "Residence Country")]
        public string APPL_RESIDENCE
        {
            get { return _APPL_RESIDENCE; }
            set { _APPL_RESIDENCE = value; }
        }
        #endregion public properties

    }

    //Testing Results Report Object
    public class CollTestingResultsReportViewObj : IDisposable
    {
        // Aplication Report Object
        private List<CollTestingResultsReportSearchResultsFilter> _CollTestingResultsReportSearchResultsFilter;
        private CollTestingResultsReportSearchFilter _CollTestingResultsReportSearchFilter;
        private PaginationFilter _PaginationFilter;
        private string _PageTitle;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        public CollTestingResultsReportViewObj()
        {
            isInitialized = true;

            _CollTestingResultsReportSearchFilter = this._CollTestingResultsReportSearchFilter ?? new CollTestingResultsReportSearchFilter();
            _CollTestingResultsReportSearchResultsFilter = this._CollTestingResultsReportSearchResultsFilter ?? new List<CollTestingResultsReportSearchResultsFilter>();
            _PaginationFilter = this.Pagination ?? new PaginationFilter();
        }

        public void Dispose()
        {
            lcapasLogic.Dispose();
            this.Dispose();
        }
        
        public bool Load()
        {
            bool success = false;

            try
            {
                _PageTitle = Structs.ReportPageTitle.CollTestingResultsReports;

                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    success = collLogic.CollTestingResultsReports(this);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "CollTestingResultsReportViewObj.Load", "Error", ex.ToString());
            }
            return success;
        }

        public byte[] Export(string[] reportID, bool allSelected = false, string filterFields = null)
        {
            var csvFile = new CsvExport();

            try
            {
                List<CollTestingResultsReportSearchResultsFilter> _TestingResultList = new List<CollTestingResultsReportSearchResultsFilter>();

                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    _TestingResultList = collLogic.GetCollTestingResultsReportByIds(reportID, allSelected, filterFields);
                }

                foreach (CollTestingResultsReportSearchResultsFilter _TestingResult in _TestingResultList)
                {
                    csvFile.AddRow();

                    csvFile["Person ID"] = _TestingResult.PERSON_ID;
                    csvFile["Last Name"] = _TestingResult.LAST_NAME;
                    csvFile["First Name"] = _TestingResult.FIRST_NAME;
                    csvFile["Middle Name"] = _TestingResult.MIDDLE_NAME;
                    csvFile["Status Date"] = _TestingResult.STATUS_DATE;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "CollTestingResultsReportViewObj.Export", "Error", ex.ToString());
            }

            return csvFile.ExportToBytes();
        }

        public bool isInitialized;

        public List<CollTestingResultsReportSearchResultsFilter> CollTestingResultsReportSearchResultsFilter { get { return _CollTestingResultsReportSearchResultsFilter; } set { _CollTestingResultsReportSearchResultsFilter = value; } }

        public CollTestingResultsReportSearchFilter CollTestingResultsReportSearchFilter { get { return _CollTestingResultsReportSearchFilter; } set { _CollTestingResultsReportSearchFilter = value; } }

        public string PageTitle { get { return _PageTitle; } set { _PageTitle = value; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }
    }

    public class CollTestingResultsReportObj
    {
        #region private properties
        private string _PERSON_ID = string.Empty;
        private string _LAST_NAME = string.Empty;
        private string _FIRST_NAME = string.Empty;
        private string _MIDDLE_NAME = string.Empty;
        private DateTime? _STATUS_DATE;
       
        #endregion private properties

        #region public properties
        public string PERSON_ID
        {
            get { return _PERSON_ID; }
            set { _PERSON_ID = value; }
        }
        public string LAST_NAME
        {
            get { return _LAST_NAME; }
            set { _LAST_NAME = value; }
        }
        public string FIRST_NAME
        {
            get { return _FIRST_NAME; }
            set { _FIRST_NAME = value; }
        }
        public string MIDDLE_NAME
        {
            get { return _MIDDLE_NAME; }
            set { _MIDDLE_NAME = value; }
        }

        public DateTime? STATUS_DATE
        {
            get { return _STATUS_DATE; }
            set { _STATUS_DATE = value; }
        }
        #endregion public properties
    }

    public class CollTestingResultsReportSearchFilter
    {
        private string _PERSON_ID;
        private string _LAST_NAME;
        private string _FIRST_NAME;
        private string _MIDDLE_NAME;
        private DateTime? _STATUS_DATE;

        #region public properties
        [Display(Name = "Person ID")]
        public string PERSON_ID
        {
            get { return _PERSON_ID; }
            set { _PERSON_ID = value; }
        }

        [Display(Name = "Last Name")]
        public string LAST_NAME
        {
            get { return _LAST_NAME; }
            set { _LAST_NAME = value; }
        }

        [Display(Name = "First Name")]
        public string FIRST_NAME
        {
            get { return _FIRST_NAME; }
            set { _FIRST_NAME = value; }
        }

        [Display(Name = "Middle Name")]
        public string MIDDLE_NAME
        {
            get { return _MIDDLE_NAME; }
            set { _MIDDLE_NAME = value; }
        }

        [Display(Name = "Status Date")]
        public DateTime? STATUS_DATE
        {
            get { return _STATUS_DATE; }
            set { _STATUS_DATE = value; }
        }
        #endregion public properties
    }

    public class CollTestingResultsReportSearchResultsFilter
    {
        private string _PERSON_ID;
        private string _LAST_NAME;
        private string _FIRST_NAME;
        private string _MIDDLE_NAME;
        private DateTime? _STATUS_DATE;

        #region public properties
        [Display(Name = "Person ID")]
        public string PERSON_ID
        {
            get { return _PERSON_ID; }
            set { _PERSON_ID = value; }
        }

        [Display(Name = "Last Name")]
        public string LAST_NAME
        {
            get { return _LAST_NAME; }
            set { _LAST_NAME = value; }
        }

        [Display(Name = "First Name")]
        public string FIRST_NAME
        {
            get { return _FIRST_NAME; }
            set { _FIRST_NAME = value; }
        }

        [Display(Name = "Middle Name")]
        public string MIDDLE_NAME
        {
            get { return _MIDDLE_NAME; }
            set { _MIDDLE_NAME = value; }
        }

        [Display(Name = "Status Date")]
        public DateTime? STATUS_DATE
        {
            get { return _STATUS_DATE; }
            set { _STATUS_DATE = value; }
        }
        #endregion public properties

    }

    // Overdues Report Object
    public class CollOverduesReportViewObj : IDisposable
    {
        // Aplication Report Object
        private List<CollOverduesReportSearchResultsFilter> _CollOverduesReportSearchResultsFilter;
        private CollOverduesReportSearchFilter _CollOverduesReportSearchFilter;
        private PaginationFilter _PaginationFilter;
        private string _PageTitle;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        private List<SelectListItem> _ColleagueAlienStatusSelectedList;
        private List<SelectListItem> _ColleagueTermSelectedList;
        private List<SelectListItem> _ColleagueLocationSelectedList;

        public CollOverduesReportViewObj()
        {
            isInitialized = true;

            _CollOverduesReportSearchFilter = this._CollOverduesReportSearchFilter ?? new CollOverduesReportSearchFilter();
            _CollOverduesReportSearchResultsFilter = this._CollOverduesReportSearchResultsFilter ?? new List<CollOverduesReportSearchResultsFilter>();
            _PaginationFilter = this.Pagination ?? new PaginationFilter();
        }

        public void Dispose()
        {
            lcapasLogic.Dispose();
            this.Dispose();
        }

        public bool Load()
        {
            bool success = false;

            try
            {
                _PageTitle = Structs.ReportPageTitle.CollOverduesReports;

                if (this.CollOverduesReportSearchFilter.StartTerm == null || !this.CollOverduesReportSearchFilter.StartTerm.Any())
                {
                    if (this.CollOverduesReportSearchFilter.StartTerm == null) { this.CollOverduesReportSearchFilter.StartTerm = new List<string>(); }
                    
                    // Adding current term as default search
                    this.CollOverduesReportSearchFilter.StartTerm.Add(Functions.GetLcSessionDesignator(Functions.GetLcCurrentTerm()));
                }

                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    _ColleagueAlienStatusSelectedList = collLogic.GetColleagueAlienStatus();
                    _ColleagueTermSelectedList = collLogic.GetColleagueTerms(true);
                    _ColleagueLocationSelectedList = collLogic.GetColleagueApplicationLocations(true);
                    success = collLogic.CollOverduesReports(this);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "CollOverduesReportViewObj.Load", "Error", ex.ToString());
            }
            return success;
        }

      
        public byte[] Export(string[] reportID, bool allSelected = false, string filterFields = null)
        {
            var csvFile = new CsvExport();

            try
            {
                List<CollOverduesReportSearchResultsFilter> _OverduesList = new List<CollOverduesReportSearchResultsFilter>();

                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    _OverduesList = collLogic.GetCollOverduesReportByIds(reportID, allSelected, filterFields);
                }

                foreach (CollOverduesReportSearchResultsFilter overdues in _OverduesList)
                {
                    csvFile.AddRow();

                    csvFile["Person ID"] = overdues.ID;
                    csvFile["LName, FName, MName"] = overdues.FullName;
                    csvFile["Alien Status"] = overdues.AlienStatus;
                    csvFile["App Status"] = overdues.Status;
                    csvFile["Program"] = overdues.CondProgram;
                    csvFile["Cond. Status"] = overdues.CondStatus;
                    csvFile["Phone1"] = overdues.Phone1;
                    csvFile["Type1"] = overdues.Type1;
                    csvFile["Phone2"] = overdues.Phone2;
                    csvFile["Type2"] = overdues.Type2;
                    csvFile["Deadline"] = overdues.Deadline;
                    csvFile["Start Term"] = overdues.StartTerm;
                    csvFile["Location"] = overdues.Location;
                    csvFile["Comments"] = overdues.Comments;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "CollOverduesReportViewObj.Export", "Error", ex.ToString());
            }

            return csvFile.ExportToBytes();
        }

        public bool isInitialized;

        public List<CollOverduesReportSearchResultsFilter> CollOverduesReportSearchResultsFilter { get { return _CollOverduesReportSearchResultsFilter; } set { _CollOverduesReportSearchResultsFilter = value; } }

        public CollOverduesReportSearchFilter CollOverduesReportSearchFilter { get { return _CollOverduesReportSearchFilter; } set { _CollOverduesReportSearchFilter = value; } }

        public string PageTitle { get { return _PageTitle; } set { _PageTitle = value; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }

        public List<SelectListItem> ColleagueTermSelectedList { get { return _ColleagueTermSelectedList; } }

        public List<SelectListItem> ColleagueAlienStatusSelectedList { get { return _ColleagueAlienStatusSelectedList ; } }

        public List<SelectListItem> ColleagueLocationSelectedList { get { return _ColleagueLocationSelectedList; } }
    }

    public class CollOverduesReportObj
    {
        #region private properties
        private string _ApplicationID;
        private string _ID = string.Empty;
        private string _ApplProgram = string.Empty;
        private string _Status = string.Empty;
        private string _FullName = string.Empty;
        private string _AlienStatus = string.Empty;
        private string _CondProgram = string.Empty;
        private string _CondStatus;
        private DateTime? _Deadline;
        private string _Phone1 = string.Empty;
        private string _Type1 = string.Empty;
        private string _Phone2 = string.Empty;
        private string _Type2 = string.Empty;
        private string _StartTerm = string.Empty;
        private string _Comments = string.Empty;
        private string _Location = string.Empty;

        #endregion private properties

        #region public properties

        public string ApplicationID
        {
            get { return _ApplicationID; }
            set { _ApplicationID = value; }
        }
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string ApplProgram
        {
            get { return _ApplProgram; }
            set { _ApplProgram = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string AlienStatus
        {
            get { return _AlienStatus; }
            set { _AlienStatus = value; }
        }

        public string CondProgram
        {
            get { return _CondProgram; }
            set { _CondProgram = value; }
        }

        public string CondStatus
        {
            get { return _CondStatus; }
            set { _CondStatus = value; }
        }

        public DateTime? Deadline
        {
            get { return _Deadline; }
            set { _Deadline = value; }
        }

        public string Phone1
        {
            get { return _Phone1; }
            set { _Phone1 = value; }
        }

        public string Type1
        {
            get { return _Type1; }
            set { _Type1 = value; }
        }

        public string Phone2
        {
            get { return _Phone2; }
            set { _Phone2 = value; }
        }

        public string Type2
        {
            get { return _Type2; }
            set { _Type2 = value; }
        }
        public string StartTerm
        {
            get { return _StartTerm; }
            set { _StartTerm = value; }
        }

        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }

        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        #endregion public properties
    }

    public class CollOverduesReportSearchFilter
    {
        private string _ApplicationID;
        private string _ID;
        private string _ApplProgram;
        private string _Status;
        private string _FullName;
        private List<string> _AlienStatus;
        private string _CondProgram;
        private string _CondStatus;
        private DateTime? _Deadline;
        private string _Phone1;
        private string _Type1;
        private string _Phone2;
        private string _Type2;
        private List<string> _StartTerm;
        private string _Comments;
        private List<string> _Location;

        #region public properties
        [Display(Name = "Application ID")]
        public string ApplicationID
        {
            get { return _ApplicationID; }
            set { _ApplicationID = value; }
        }

        [Display(Name = "Person ID")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [Display(Name = "Program")]
        public string ApplProgram
        {
            get { return _ApplProgram; }
            set { _ApplProgram = value; }
        }

        [Display(Name = "App Status")]
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        [Display(Name = "LName, FName, MName")]
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        [Display(Name = "Alien Status")]
        public List<string> AlienStatus
        {
            get { return _AlienStatus; }
            set { _AlienStatus = value; }
        }

        [Display(Name = "Program")]
        public string CondProgram
        {
            get { return _CondProgram; }
            set { _CondProgram = value; }
        }

        [Display(Name = "Cond. Status")]
        public string CondStatus
        {
            get { return _CondStatus; }
            set { _CondStatus = value; }
        }

        [Display(Name = "Deadline")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? Deadline
        {
            get { return _Deadline; }
            set { _Deadline = value; }
        }

        [Display(Name = "Phone1")]
        public string Phone1
        {
            get { return _Phone1; }
            set { _Phone1 = value; }
        }

        [Display(Name = "Type1")]
        public string Type1
        {
            get { return _Type1; }
            set { _Type1 = value; }
        }

        [Display(Name = "Phone2")]
        public string Phone2
        {
            get { return _Phone2; }
            set { _Phone2 = value; }
        }
        

        [Display(Name = "Type2")]
        public string Type2
        {
            get { return _Type2; }
            set { _Type2 = value; }
        }
        [Display(Name = "Start Term")]

        public List<string> StartTerm
        {
            get { return _StartTerm; }
            set { _StartTerm = value; }
        }

        [Display(Name = "Comments")]
        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }

        [Display(Name = "Location")]
        public List<string> Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        #endregion public properties
    }

    public class CollOverduesReportSearchResultsFilter
    {
        private string _ApplicationID;
        private string _ID;
        private string _ApplProgram;
        private string _Status;
        private string _FullName;
        private string _AlienStatus;
        private string _CondProgram;
        private string _CondStatus;
        private DateTime? _Deadline;
        private string _Phone1;
        private string _Type1;
        private string _Phone2;
        private string _Type2;
        private string _StartTerm;
        private string _Comments;
        private string _Location;

        #region public properties
        [Display(Name = "Application ID")]
        public string ApplicationID
        {
            get { return _ApplicationID; }
            set { _ApplicationID = value; }
        }

        [Display(Name = "Person ID")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [Display(Name = "Program")]
        public string ApplProgram
        {
            get { return _ApplProgram; }
            set { _ApplProgram = value; }
        }


        [Display(Name = "App Status")]
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        [Display(Name = "LName, FName, MName")]
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        [Display(Name = "Alien Status")]
        public string AlienStatus
        {
            get { return _AlienStatus; }
            set { _AlienStatus = value; }
        }

        [Display(Name = "Program")]
        public string CondProgram
        {
            get { return _CondProgram; }
            set { _CondProgram = value; }
        }

        [Display(Name = "Cond. Status")]
        public string CondStatus
        {
            get { return _CondStatus; }
            set { _CondStatus = value; }
        }

        [Display(Name = "Deadline")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? Deadline
        {
            get { return _Deadline; }
            set { _Deadline = value; }
        }

        [Display(Name = "Phone1")]
        public string Phone1
        {
            get { return _Phone1; }
            set { _Phone1 = value; }
        }

        [Display(Name = "Type1")]
        public string Type1
        {
            get { return _Type1; }
            set { _Type1 = value; }
        }

        [Display(Name = "Phone2")]
        public string Phone2
        {
            get { return _Phone2; }
            set { _Phone2 = value; }
        }

        [Display(Name = "Type2")]
        public string Type2
        {
            get { return _Type2; }
            set { _Type2 = value; }
        }

        [Display(Name = "Start Term")]
        public string StartTerm
        {
            get { return _StartTerm; }
            set { _StartTerm = value; }
        }

        [Display(Name = "Comments")]
        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }

        [Display(Name = "Location")]
        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        #endregion public properties

    }

    // Missing grade Report Object
    public class CollMissingGradeReportViewObj : IDisposable
    {
        //Missing grade Report Object
        private List<CollMissingGradeReportSearchResultsFilter> _CollMissingGradeReportSearchResultsFilter;
        private CollMissingGradeReportSearchFilter _CollMissingGradeReportSearchFilter;
        private PaginationFilter _PaginationFilter;
        private string _PageTitle;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        private List<SelectListItem> _ColleagueAlienStatusSelectedList;
        private List<SelectListItem> _ColleagueTermSelectedList;
        private List<SelectListItem> _ColleagueLocationSelectedList;

        public CollMissingGradeReportViewObj()
        {
            isInitialized = true;

            _CollMissingGradeReportSearchFilter = this._CollMissingGradeReportSearchFilter ?? new CollMissingGradeReportSearchFilter();
            _CollMissingGradeReportSearchResultsFilter = this._CollMissingGradeReportSearchResultsFilter ?? new List<CollMissingGradeReportSearchResultsFilter>();
            _PaginationFilter = this.Pagination ?? new PaginationFilter();
        }

        public void Dispose()
        {
            lcapasLogic.Dispose();
            this.Dispose();
        }

        public bool Load()
        {
            bool success = false;

            try
            {
                _PageTitle = Structs.ReportPageTitle.CollMissingGradeReports;

                //if (this.CollMissingGradeReportSearchFilter.StartTerm == null || !this.CollMissingGradeReportSearchFilter.StartTerm.Any())
                //{
                //    if (this.CollMissingGradeReportSearchFilter.StartTerm == null) { this.CollMissingGradeReportSearchFilter.StartTerm = new List<string>(); }

                //    // Adding current term as default search
                //    this.CollMissingGradeReportSearchFilter.StartTerm.Add(Functions.GetLcSessionDesignator(Functions.GetLcCurrentTerm()));
                //}

                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    _ColleagueAlienStatusSelectedList = collLogic.GetColleagueAlienStatus();
                    _ColleagueTermSelectedList = collLogic.GetColleagueTerms(true);
                    _ColleagueLocationSelectedList = collLogic.GetColleagueApplicationLocations(true);
                    success = collLogic.CollMissingGradeReports(this);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "CollMissingGradeReportViewObj.Load", "Error", ex.ToString());
            }
            return success;
        }

        public byte[] Export(string[] reportID)
        {
            var csvFile = new CsvExport();

            try
            {
                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    List<CollMissingGradeReportSearchResultsFilter> _MissingGradesList = new List<CollMissingGradeReportSearchResultsFilter>();
                    for (int i = 0; i < reportID.Length; i++)
                    {
                        string ID = reportID.ToArray()[i].Split(';')[0];
                        DateTime Date = Convert.ToDateTime(reportID.ToArray()[i].Split(';')[1]);
                        string Term = reportID.ToArray()[i].Split(';')[2];
                        string Stat = reportID.ToArray()[i].Split(';')[3];

                        //_MissingGradesList.AddRange(collLogic.CollMissingGradeReportByIds(ID, Date, Term, Stat));
                    }
                    foreach (CollMissingGradeReportSearchResultsFilter Missing in _MissingGradesList)
                    {
                        csvFile.AddRow();
                        csvFile["Course"] = Missing.Course;
                        csvFile["Sec"] = Missing.Sec;
                        csvFile["StuID"] = Missing.StuID;
                        csvFile["StuLastName"] = Missing.StuLastName;
                        csvFile["StuFirstName"] = Missing.StuFirstName;
                        csvFile["InstrID"] = Missing.InstrID;
                        csvFile["InstrLastName"] = Missing.InstrLastName;
                        csvFile["InstrFirstName"] = Missing.InstrFirstName;
                        csvFile["FinGrade"] = Missing.FinGrade;
                        csvFile["VerGrade"] = Missing.VerGrade;
                        csvFile["MidTerm"] = Missing.MidTerm;
                        csvFile["LastDateAttend"] = Missing.LastDateAttend;
                        csvFile["NvrAttend"] = Missing.NvrAttend;
                        csvFile["Dep"] = Missing.Dept;
                        csvFile["SecStarts"] = Missing.SecStarts;
                        csvFile["SecEnds"] = Missing.SecEnds;
                        csvFile["AcdLv"] = Missing.AcdLv;
                        csvFile["Term"] = Missing.Term;

                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "CollMissingGradeReportViewObj.Export", "Error", ex.ToString());
            }

            return csvFile.ExportToBytes();
        }

        public bool isInitialized;

        public List<CollMissingGradeReportSearchResultsFilter> CollMissingGradeReportSearchResultsFilter { get { return _CollMissingGradeReportSearchResultsFilter; } set { _CollMissingGradeReportSearchResultsFilter = value; } }

        public CollMissingGradeReportSearchFilter CollMissingGradeReportSearchFilter { get { return _CollMissingGradeReportSearchFilter; } set { _CollMissingGradeReportSearchFilter = value; } }

        public string PageTitle { get { return _PageTitle; } set { _PageTitle = value; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }

        public List<SelectListItem> ColleagueTermSelectedList { get { return _ColleagueTermSelectedList; } }

        public List<SelectListItem> ColleagueAlienStatusSelectedList { get { return _ColleagueAlienStatusSelectedList; } }

        public List<SelectListItem> ColleagueLocationSelectedList { get { return _ColleagueLocationSelectedList; } }
    }

    public class CollMissingGradeReportObj
    {
        #region private properties
        private string _Course = string.Empty;
        private string _Sec = string.Empty;
        private string _StuID = string.Empty;
        private string _StuLastName = string.Empty;
        private string _StuFirstName = string.Empty;
        private string _ID = string.Empty;
        private string _InstrLastName = string.Empty;
        private string _InstFirstName = string.Empty;
        private string _FinGrade = string.Empty;
        private string _VerGrade = string.Empty;
        private string _MidTerm = string.Empty;
        private DateTime? _LastDateAttend;
        private string _NvrAttend = string.Empty;
        private string _Dept = string.Empty;
        private DateTime? _SecStarts;
        private DateTime? _SecEnds;
        private string _AcdLv = string.Empty;
        private string _Term = string.Empty;

        #endregion private properties

        #region public properties

        public string Course
        {
            get { return _Course; }
            set { _Course = value; }
        }
        public string Sec
        {
            get { return _Sec; }
            set { _Sec = value; }
        }
        public string StuID
        {
            get { return _StuID; }
            set { _StuID = value; }
        }

        public string StuLastName
        {
            get { return _StuLastName; }
            set { _StuLastName = value; }
        }

        public string StuFirstName
        {
            get { return _StuFirstName; }
            set { _StuFirstName = value; }
        }


        public string InstrLastName
        {
            get { return _InstrLastName; }
            set { _InstrLastName = value; }
        }

        public string InstFirstName
        {
            get { return _InstFirstName; }
            set { _InstFirstName = value; }
        }

        public string FinGrade
        {
            get { return _FinGrade; }
            set { _FinGrade = value; }
        }

        public string VerGrade
        {
            get { return _VerGrade; }
            set { _VerGrade = value; }
        }

        public string MidTerm
        {
            get { return _MidTerm; }
            set { _MidTerm = value; }
        }

        public DateTime? LastDateAttend
        {
            get { return _LastDateAttend; }
            set { _LastDateAttend = value; }
        }
        public string NvrAttend
        {
            get { return _NvrAttend; }
            set { _NvrAttend = value; }
        }

        public string Dept
        {
            get { return _Dept; }
            set { _Dept = value; }
        }

        public DateTime? SecStarts
        {
            get { return _SecStarts; }
            set { _SecStarts = value; }
        }

        public DateTime? SecEnds
        {
            get { return _SecEnds; }
            set { _SecEnds = value; }
        }

        public string AcdLv
        {
            get { return _AcdLv; }
            set { _AcdLv = value; }
        }

        public string Term
        {
            get { return _Term; }
            set { _Term = value; }
        }
        #endregion public properties
    }

    public class CollMissingGradeReportSearchFilter
    {
        private string _Course = string.Empty;
        private string _Sec = string.Empty;
        private string _StuID = string.Empty;
        private string _StuLastName = string.Empty;
        private string _StuFirstName = string.Empty;
        private string _InstrID = string.Empty;
        private string _InstrLastName = string.Empty;
        private string _InstrFirstName = string.Empty;
        private string _FinGrade = string.Empty;
        private string _VerGrade = string.Empty;
        private string _MidTerm = string.Empty;
        private DateTime? _LastDateAttend;
        private string _NvrAttend = string.Empty;
        private string _Dept = string.Empty;
        private DateTime? _SecStarts;
        private DateTime? _SecEnds;
        private string _AcdLv = string.Empty;
        private string _Term = string.Empty;

        #region public properties
        [Display(Name = "Course")]
        public string Course
        {
            get { return _Course; }
            set { _Course = value; }
        }

        [Display(Name = "Sec")]
        public string Sec
        {
            get { return _Sec; }
            set { _Sec = value; }
        }

        [Display(Name = "StuID")]
        public string StuID
        {
            get { return _StuID; }
            set { _StuID = value; }
        }

        [Display(Name = "StuLastName")]
        public string StuLastName
        {
            get { return _StuLastName; }
            set { _StuLastName = value; }
        }
        [Display(Name = "StuFirstName")]
        public string StuFirstName
        {
            get { return _StuFirstName; }
            set { _StuFirstName = value; }
        }

        [Display(Name = "InstrID")]
        public string InstrID
        {
            get { return _InstrID; }
            set { _InstrID = value; }
        }

        [Display(Name = "InstrLastName")]
        public string InstrLastName
        {
            get { return _InstrLastName; }
            set { _InstrLastName = value; }
        }

        [Display(Name = "InstrFirstName")]
        public string InstrFirstName
        {
            get { return _InstrFirstName; }
            set { _InstrFirstName = value; }
        }

        [Display(Name = "FinGrade")]
        public string FinGrade
        {
            get { return _FinGrade; }
            set { _FinGrade = value; }
        }

        [Display(Name = "VerGrade")]
        public string VerGrade
        {
            get { return _VerGrade; }
            set { _VerGrade = value; }
        }

        [Display(Name = "MidTerm")]
        public string MidTerm
        {
            get { return _MidTerm; }
            set { _MidTerm = value; }
        }

        [Display(Name = "LastDateAttend")]
        public DateTime? LastDateAttend
        {
            get { return _LastDateAttend; }
            set { _LastDateAttend = value; }
        }

        [Display(Name = "NvrAttend")]
        public string NvrAttend
        {
            get { return _NvrAttend; }
            set { _NvrAttend = value; }
        }

        [Display(Name = "Dept")]
        public string Dept
        {
            get { return _Dept; }
            set { _Dept = value; }
        }

        [Display(Name = "SecStarts")]
        public DateTime? SecStarts
        {
            get { return _SecStarts; }
            set { _SecStarts = value; }
        }

        [Display(Name = "SecEnds")]
        public DateTime? SecEnds
        {
            get { return _SecEnds; }
            set { _SecEnds = value; }
        }

        [Display(Name = "AcdLv")]
        public string AcdLv
        {
            get { return _AcdLv; }
            set { _AcdLv = value; }
        }

        [Display(Name = "Term")]
        public string Term
        {
            get { return _Term; }
            set { _Term = value; }
        }
        #endregion public properties
    }

    public class CollMissingGradeReportSearchResultsFilter
    {
        private string _Course;
        private string _Sec;
        private string _StuID;
        private string _StuLastName;
        private string _StuFirstName;
        private string _InstrID;
        private string _InstrLastName;
        private string _InstrFirstName;
        private string _FinGrade;
        private string _VerGrade;
        private string _MidTerm;
        private DateTime? _LastDateAttend;
        private string _NvrAttend;
        private string _Dept;
        private DateTime? _SecStarts;
        private DateTime? _SecEnds;
        private string _AcdLv;
        private string _Term;

        #region public properties
        [Display(Name = "Course")]
        public string Course
        {
            get { return _Course; }
            set { _Course = value; }
        }

        [Display(Name = "Sec")]
        public string Sec
        {
            get { return _Sec; }
            set { _Sec = value; }
        }

        [Display(Name = "StuID")]
        public string StuID
        {
            get { return _StuID; }
            set { _StuID = value; }
        }

        [Display(Name = "StuLastName")]
        public string StuLastName
        {
            get { return _StuLastName; }
            set { _StuLastName = value; }
        }
        [Display(Name = "StuFirstName")]
        public string StuFirstName
        {
            get { return _StuFirstName; }
            set { _StuFirstName = value; }
        }

        [Display(Name = "InstrID")]
        public string InstrID
        {
            get { return _InstrID; }
            set { _InstrID = value; }
        }

        [Display(Name = "InstrLastName")]
        public string InstrLastName
        {
            get { return _InstrLastName; }
            set { _InstrLastName = value; }
        }

        [Display(Name = "InstrFirstName")]
        public string InstrFirstName
        {
            get { return _InstrFirstName; }
            set { _InstrFirstName = value; }
        }

        [Display(Name = "FinGarde")]
        public string FinGrade
        {
            get { return _FinGrade; }
            set { _FinGrade = value; }
        }

        [Display(Name = "VerGrade")]
        public string VerGrade
        {
            get { return _VerGrade; }
            set { _VerGrade = value; }
        }

        [Display(Name = "MidTerm")]
        public string MidTerm
        {
            get { return _MidTerm; }
            set { _MidTerm = value; }
        }

        [Display(Name = "LastDateAttend")]
        public DateTime? LastDateAttend
        {
            get { return _LastDateAttend; }
            set { _LastDateAttend = value; }
        }

        [Display(Name = "NvrAttend")]
        public string NvrAttend
        {
            get { return _NvrAttend; }
            set { _NvrAttend = value; }
        }

        [Display(Name = "Dept")]
        public string Dept
        {
            get { return _Dept; }
            set { _Dept = value; }
        }

        [Display(Name = "SecStarts")]
        public DateTime? SecStarts
        {
            get { return _SecStarts; }
            set { _SecStarts = value; }
        }

        [Display(Name = "SecEnds")]
        public DateTime? SecEnds
        {
            get { return _SecEnds; }
            set { _SecEnds = value; }
        }

        [Display(Name = "AcdLv")]
        public string AcdLv
        {
            get { return _AcdLv; }
            set { _AcdLv = value; }
        }

        [Display(Name = "Term")]
        public string Term
        {
            get { return _Term; }
            set { _Term = value; }
        }
        #endregion public properties

    }

    //  Admission Condition Report Object
    public class CollAdmissionConditionsReportViewObj : IDisposable
    {

        // Admission Conditions Report Object
        private List<CollAdmissionConditionsReportSearchResultsFilter> _CollAdmissionConditionsReportSearchResultsFilter;
        private CollAdmissionConditionsReportSearchFilter _CollAdmissionConditionsReportSearchFilter;
        private PaginationFilter _PaginationFilter;
        private string _PageTitle;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        private List<SelectListItem> _ColleagueTermSelectedList;
        private List<SelectListItem> _ColleagueProgramSelectedList;
        private List<SelectListItem> _ColleagueApplicationStatusSelectedList;
        private List<SelectListItem> _ColleagueAlienStatusSelectedList;

        public CollAdmissionConditionsReportViewObj()
        {
            isInitialized = true;

            _CollAdmissionConditionsReportSearchFilter = this._CollAdmissionConditionsReportSearchFilter ?? new CollAdmissionConditionsReportSearchFilter();
            _CollAdmissionConditionsReportSearchResultsFilter = this._CollAdmissionConditionsReportSearchResultsFilter ?? new List<CollAdmissionConditionsReportSearchResultsFilter>();
            _PaginationFilter = this.Pagination ?? new PaginationFilter();
        }

        public void Dispose()
        {
            lcapasLogic.Dispose();
            this.Dispose();
        }

        public bool Load()
        {
            bool success = false;

            try
            {
                _PageTitle = Structs.ReportPageTitle.CollAdmissionConditionsReports;

                if (this.CollAdmissionConditionsReportSearchFilter.Term == null || !this.CollAdmissionConditionsReportSearchFilter.Term.Any())
                {
                    if (this.CollAdmissionConditionsReportSearchFilter.Term == null) { this.CollAdmissionConditionsReportSearchFilter.Term = new List<string>(); }
                    
                    // Adding current term as default search
                    this.CollAdmissionConditionsReportSearchFilter.Term.Add(Functions.GetLcSessionDesignator(Functions.GetLcCurrentTerm()));
                }
                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    _ColleagueTermSelectedList = collLogic.GetColleagueTerms(true);
                    _ColleagueProgramSelectedList = collLogic.GetColleaguePrograms(true);
                    _ColleagueApplicationStatusSelectedList = collLogic.GetColleagueApplicationStatus();
                    _ColleagueAlienStatusSelectedList = collLogic.GetColleagueAlienStatus();
                    success = collLogic.CollAdmissionConditionsReports(this);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "CollAdmissionConditionReportViewObj.Load", "Error", ex.ToString());
            }
            return success;
        }

        public byte[] Export(string[] reportID, bool allSelected = false, string filterFields = null)
        {
            var csvFile = new CsvExport();

            try
            {
                List<CollAdmissionConditionsReportSearchResultsFilter> _AdmissionConditionsList = new List<CollAdmissionConditionsReportSearchResultsFilter>();

                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    _AdmissionConditionsList = collLogic.GetCollAdmissionConditionsReportByIds(reportID, allSelected, filterFields);
                }

                foreach (CollAdmissionConditionsReportSearchResultsFilter admission in _AdmissionConditionsList)
                {
                    csvFile.AddRow();
                    csvFile["Person ID"] = admission.PersonID;
                    csvFile["App Status"] = admission.ApplicationStatus;
                    csvFile["LName, FName, MName"] = admission.FullName;
                    csvFile["Term"] = admission.Term;
                    csvFile["Alien Status"] = admission.AlienStatus;
                    csvFile["Program"] = admission.ConditionProgram;
                    csvFile["Condition"] = admission.Condition;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "CollAdmissionConditionReportViewObj.Export", "Error", ex.ToString());
            }

            return csvFile.ExportToBytes();
        }

        public bool isInitialized;

        public List<CollAdmissionConditionsReportSearchResultsFilter> CollAdmissionConditionReportSearchResultsFilter { get { return _CollAdmissionConditionsReportSearchResultsFilter; } set { _CollAdmissionConditionsReportSearchResultsFilter = value; } }

        public CollAdmissionConditionsReportSearchFilter CollAdmissionConditionsReportSearchFilter { get { return _CollAdmissionConditionsReportSearchFilter; } set { _CollAdmissionConditionsReportSearchFilter = value; } }

        public string PageTitle { get { return _PageTitle; } set { _PageTitle = value; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }

        public List<SelectListItem> ColleagueTermSelectedList { get { return _ColleagueTermSelectedList; } }
        public List<SelectListItem> ColleagueProgramSelectedList { get { return _ColleagueProgramSelectedList; } }
        public List<SelectListItem> ColleagueApplicationStatusSelectedList { get { return _ColleagueApplicationStatusSelectedList; } }
        public List<SelectListItem> ColleagueAlienStatusSelectedList { get { return _ColleagueAlienStatusSelectedList; } }
    }

    public class CollAdmissionConditionsReportObj
    {
        #region private properties
        private string _ApplicationID = string.Empty;
        private string _PersonID = string.Empty;
        private string _ApplicationProgram = string.Empty;
        private List<string> _ApplicationStatus;
        private string _FullName = string.Empty;
        private string _Term = string.Empty;
        private List<string> _AlienStatus;
        private List<string> _ConditionProgram;
        private string _Condition = string.Empty;

        #endregion private properties

        #region public properties

        public string ApplicationID
        {
            get { return _ApplicationID; }
            set { _ApplicationID = value; }
        }

        public string PersonID
        {
            get { return _PersonID; }
            set { _PersonID = value; }
        }

        public string ApplicationProgram
        {
            get { return _ApplicationProgram; }
            set { _ApplicationProgram = value; }
        }

        public List<string> ApplicationStatus
        {
            get { return _ApplicationStatus; }
            set { _ApplicationStatus = value; }
        }

        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        public string Term
        {
            get { return _Term; }
            set { _Term = value; }
        }

        public List<string> AlienStatus
        {
            get { return _AlienStatus; }
            set { _AlienStatus = value; }
        }

        public List<string> ConditionProgram
        {
            get { return _ConditionProgram; }
            set { _ConditionProgram = value; }
        }

        public string Condition
        {
            get { return _Condition; }
            set { _Condition = value; }
        }
        #endregion public properties
    }

    public class CollAdmissionConditionsReportSearchFilter
    {
        #region private properties
        private string _ApplicationID = string.Empty;
        private string _PersonID = string.Empty;
        private List<string> _ApplicationProgram;
        private List<string> _ApplicationStatus;
        private string _FullName = string.Empty;
        private List<string> _Term;
        private List<string> _AlienStatus;
        private List<string> _ConditionProgram;
        private string _Condition = string.Empty;

        #endregion private properties

        #region public properties

        [Display(Name = "Application ID")]
        public string ApplicationID
        {
            get { return _ApplicationID; }
            set { _ApplicationID = value; }
        }

        [Display(Name = "Person ID")]
        public string PersonID
        {
            get { return _PersonID; }
            set { _PersonID = value; }
        }

        [Display(Name = "Application Program")]
        public List<string> ApplicationProgram
        {
            get { return _ApplicationProgram; }
            set { _ApplicationProgram = value; }
        }

        [Display(Name = "App Status")]
        public List<string> ApplicationStatus
        {
            get { return _ApplicationStatus; }
            set { _ApplicationStatus = value; }
        }

        [Display(Name = "LName, FName, MName")]
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        [Display(Name = "Term")]
        public List<string> Term
        {
            get { return _Term; }
            set { _Term = value; }
        }

        [Display(Name = "Alien Status")]
        public List<string> AlienStatus
        {
            get { return _AlienStatus; }
            set { _AlienStatus = value; }
        }

        [Display(Name = "Program")]
        public List<string> ConditionProgram
        {
            get { return _ConditionProgram; }
            set { _ConditionProgram = value; }
        }

        [Display(Name = "Condition")]
        public string Condition
        {
            get { return _Condition; }
            set { _Condition = value; }
        }
        #endregion public properties

    }

    public class CollAdmissionConditionsReportSearchResultsFilter
    {
        #region private properties
        private string _PersonID;
        private string _ApplicationID;
        private string _ApplicationProgram;
        private string _ApplicationStatus;
        private string _FullName;
        private string _Term;
        private string _AlienStatus;
        private string _ConditionProgram;
        private string _Condition;

        #endregion private properties

        #region public properties

        [Display(Name = "Application ID")]
        public string ApplicationID
        {
            get { return _ApplicationID; }
            set { _ApplicationID = value; }
        }

        [Display(Name = "Person ID")]
        public string PersonID
        {
            get { return _PersonID; }
            set { _PersonID = value; }
        }

        [Display(Name = "Application Program")]
        public string ApplicationProgram
        {
            get { return _ApplicationProgram; }
            set { _ApplicationProgram = value; }
        }

        [Display(Name = "App Status")]
        public string ApplicationStatus
        {
            get { return _ApplicationStatus; }
            set { _ApplicationStatus = value; }
        }

        [Display(Name = "LName, FName, MName")]
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        [Display(Name = "Term")]
        public string Term
        {
            get { return _Term; }
            set { _Term = value; }
        }

        [Display(Name = "Alien Status")]
        public string AlienStatus
        {
            get { return _AlienStatus; }
            set { _AlienStatus = value; }
        }

        [Display(Name = "Program")]
        public string ConditionProgram
        {
            get { return _ConditionProgram; }
            set { _ConditionProgram = value; }
        }

        [Display(Name = "Condition")]
        public string Condition
        {
            get { return _Condition; }
            set { _Condition = value; }
        }
        #endregion public properties

    }

    // ApplicationProgram Report Object 
    public class ApplicationProgramViewObj : IDisposable
    {
        // ProgramReport Object
        private List<ApplicationProgramSearchResultsFilter> _ApplicationProgramSearchResultsFilter;
        private ApplicationProgramSearchFilter _ApplicationProgramSearchFilter;
        private PaginationFilter _PaginationFilter;
        private string _PageTitle;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        private List<ProgramCampus> _CampusSelectedList;
        private List<ProgramTerm> _TermSelectedList;

        public ApplicationProgramViewObj()
        {
            isInitialized = true;

            _ApplicationProgramSearchFilter = this._ApplicationProgramSearchFilter ?? new ApplicationProgramSearchFilter();
            _ApplicationProgramSearchResultsFilter = this._ApplicationProgramSearchResultsFilter ?? new List<ApplicationProgramSearchResultsFilter>();
            _PaginationFilter = this.Pagination ?? new PaginationFilter();
        }

        public void Dispose()
        {
            lcapasLogic.Dispose();
            this.Dispose();
        }

        public bool Load()
        {
            bool success = false;

            try
            {
                //_PageTitle = Library.Structs.ReportPageTitle.SentEmailReports;

                _CampusSelectedList = lcapasLogic.GetCampuses();
                _TermSelectedList = lcapasLogic.GetTerms();
                success = lcapasLogic.GetApplicationProgramsForProgramTerm(this);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Library.Structs.Project.LcapasAdmin, Library.Structs.Class.SettingReport, "ApplicationProgramViewObj.Load", "Error", ex.ToString());
            }
            return success;
        }


        public bool isInitialized;

        public List<ApplicationProgramSearchResultsFilter> ApplicationProgramSearchResultsFilter { get { return _ApplicationProgramSearchResultsFilter; } set { _ApplicationProgramSearchResultsFilter = value; } }

        public ApplicationProgramSearchFilter ApplicationProgramSearchFilter { get { return _ApplicationProgramSearchFilter; } set { _ApplicationProgramSearchFilter = value; } }

        public string PageTitle { get { return _PageTitle; } set { _PageTitle = value; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }

        public List<ProgramCampus> CampusSelectedList { get { return _CampusSelectedList; } }
        public List<ProgramTerm> TermSelectedList { get { return _TermSelectedList; } }

    }

    public class ApplicationProgramObj
    {
        #region private properties

        public int _ApplicationProgramId;
        public string _ProgramCode = string.Empty;
        public string _ProgramDesc = string.Empty;
        public bool _Active = true;
        public string _CreatedBy = string.Empty;
        public DateTime? _CreatedDateTime;
        public string _ModifiedBy = string.Empty;
        public DateTime? _ModifiedDateTime;


        #endregion private properties

        #region public properties
        public int ApplicationProgramId
        {
            get { return _ApplicationProgramId; }
            set { _ApplicationProgramId = value; }
        }

        public string ProgramCode
        {
            get { return _ProgramCode; }
            set { _ProgramCode = value; }
        }
        public string ProgramDesc
        {
            get { return _ProgramDesc; }
            set { _ProgramDesc = value; }
        }
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public DateTime? CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public DateTime? ModifiedDateTime
        {
            get { return _ModifiedDateTime; }
            set { _ModifiedDateTime = value; }
        }

        #endregion public properties
    }

    public class ApplicationProgramSearchFilter
    {
        public int _ApplicationProgramId;
        public int _TermId;
        public int _CampusId;
        public string _ProgramCode = string.Empty;
        public string _ProgramDesc = string.Empty;
        public bool _Active = true;
        public string _CreatedBy = string.Empty;
        public DateTime? _CreatedDateTime;
        public string _ModifiedBy = string.Empty;
        public DateTime? _ModifiedDateTime;


        #region public properties
        public int ApplicationProgramId
        {
            get { return _ApplicationProgramId; }
            set { _ApplicationProgramId = value; }
        }
        public int TermId
        {
            get { return _TermId; }
            set { _TermId = value; }
        }
        public int CampusId
        {
            get { return _CampusId; }
            set { _CampusId = value; }
        }

        public string ProgramCode
        {
            get { return _ProgramCode; }
            set { _ProgramCode = value; }
        }
        public string ProgramDesc
        {
            get { return _ProgramDesc; }
            set { _ProgramDesc = value; }
        }
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public DateTime? CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public DateTime? ModifiedDateTime
        {
            get { return _ModifiedDateTime; }
            set { _ModifiedDateTime = value; }
        }


        #endregion public properties

    }

    public class ApplicationProgramSearchResultsFilter
    {

        public int _ApplicationProgramId;
        public int _TermId;
        public int _CampusId;
        public string _ProgramCode;
        public string _ProgramDesc;
        public bool _Active;
        public string _CreatedBy;
        public DateTime? _CreatedDateTime;
        public string _ModifiedBy;
        public DateTime? _ModifiedDateTime;


        #region public properties
        [Display(Name = "Application ProgramId")]
        public int ApplicationProgramId
        {
            get { return _ApplicationProgramId; }
            set { _ApplicationProgramId = value; }
        }
        [Display(Name = "TermId")]
        public int TermId
        {
            get { return _TermId; }
            set { _TermId = value; }
        }
        [Display(Name = "CampusId")]
        public int CampusId
        {
            get { return _CampusId; }
            set { _CampusId = value; }
        }
        [Display(Name = "Program Code")]
        public string ProgramCode
        {
            get { return _ProgramCode; }
            set { _ProgramCode = value; }
        }
        [Display(Name = "Program Desc")]
        public string ProgramDesc
        {
            get { return _ProgramDesc; }
            set { _ProgramDesc = value; }
        }
        [Display(Name = "Active")]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [Display(Name = "CreatedBy")]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Display(Name = "Created DateTime")]
        public DateTime? CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }
        [Display(Name = "ModifiedBy")]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [Display(Name = "Modified DateTime")]
        public DateTime? ModifiedDateTime
        {
            get { return _ModifiedDateTime; }
            set { _ModifiedDateTime = value; }
        }

        #endregion public properties


    }

    // WaitingList Report Object 
    public class ProgramDetailViewObj : IDisposable
    {
        // ProgramReport Object
        private List<ProgramDetailSearchResultsFilter> _ProgramDetailSearchResultsFilter;
        private ProgramDetailSearchFilter _ProgramDetailSearchFilter;
        private PaginationFilter _PaginationFilter;
        private string _PageTitle;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        private List<ProgramCampus> _CampusSelectedList;
        private List<ProgramTerm> _TermSelectedList;
        private List<SelectListItem> _ActiveSelectedList;

        public ProgramDetailViewObj()
        {
            isInitialized = true;

            _ProgramDetailSearchFilter = this._ProgramDetailSearchFilter ?? new ProgramDetailSearchFilter();
            _ProgramDetailSearchResultsFilter = this._ProgramDetailSearchResultsFilter ?? new List<ProgramDetailSearchResultsFilter>();
            _PaginationFilter = this.Pagination ?? new PaginationFilter();
        }

        public void Dispose()
        {
            lcapasLogic.Dispose();
            this.Dispose();
        }

        public bool Load()
        {
            bool success = false;
            if (_ActiveSelectedList == null) { _ActiveSelectedList = new List<SelectListItem>(); }

            try
            {
                //_PageTitle = Library.Structs.ReportPageTitle.SentEmailReports;

                _CampusSelectedList = lcapasLogic.GetCampuses();
                _TermSelectedList = lcapasLogic.GetTerms();
                _ActiveSelectedList = lcapasLogic.GetTrueFalse();
                success = lcapasLogic.DeactivateProgramTerm(this);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Library.Structs.Project.LcapasAdmin, Library.Structs.Class.SettingReport, "ProgramDetailViewObj.Load", "Error", ex.ToString());
            }
            return success;
        }


        public bool isInitialized;

        public List<ProgramDetailSearchResultsFilter> ProgramDetailSearchResultsFilter { get { return _ProgramDetailSearchResultsFilter; } set { _ProgramDetailSearchResultsFilter = value; } }

        public ProgramDetailSearchFilter ProgramDetailSearchFilter { get { return _ProgramDetailSearchFilter; } set { _ProgramDetailSearchFilter = value; } }

        public string PageTitle { get { return _PageTitle; } set { _PageTitle = value; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }

        public List<ProgramCampus> CampusSelectedList { get { return _CampusSelectedList; } }
        public List<ProgramTerm> TermSelectedList { get { return _TermSelectedList; } }
        public List<SelectListItem> ActiveSelectedList { get { return _ActiveSelectedList; } }


    }

    public class ProgramDetailObj
    {
        #region private properties
        private string _ProgramDetailsId = string.Empty;
        private DateTime? _StartDate;
        private bool _MonthlyBased = false;
        private string _ProgramDetailOrder = string.Empty;
        private bool _Active = false;
        private string _CreatedBy = string.Empty;
        private DateTime? _CreatedDateTime;
        private string _ModifiedBy = string.Empty;
        private DateTime? _ModifiedDateTime;
        private string _ProgramTerm_ProgramTermId = string.Empty;
        private string _ProgramCampus_ProgramCampusId = string.Empty;
        private string _ApplicationProgram_ApplicationProgramId = string.Empty;
        private string _ApplicationProgram = string.Empty;


        #endregion private properties

        #region public properties
        public string ProgramDetailsId
        {
            get { return _ProgramDetailsId; }
            set { _ProgramDetailsId = value; }
        }
        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public bool MonthlyBased
        {
            get { return _MonthlyBased; }
            set { _MonthlyBased = value; }
        }
        public string ProgramDetailOrder
        {
            get { return _ProgramDetailOrder; }
            set { _ProgramDetailOrder = value; }
        }
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public DateTime? CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public DateTime? ModifiedDateTime
        {
            get { return _ModifiedDateTime; }
            set { _ModifiedDateTime = value; }
        }
        public string ProgramTerm_ProgramTermId
        {
            get { return _ProgramTerm_ProgramTermId; }
            set { _ProgramTerm_ProgramTermId = value; }
        }
        public string ProgramCampus_ProgramCampusId
        {
            get { return _ProgramCampus_ProgramCampusId; }
            set { _ProgramCampus_ProgramCampusId = value; }
        }
        public string ApplicationProgram_ApplicationProgramId
        {
            get { return _ApplicationProgram_ApplicationProgramId; }
            set { _ApplicationProgram_ApplicationProgramId = value; }
        }
        public string ApplicationPrograms
        {
            get { return _ApplicationProgram; }
            set { _ApplicationProgram = value; }
        }
        #endregion public properties
    }

    public class ProgramDetailSearchFilter
    {
        private string _ProgramDetailsId;
        private DateTime? _StartDate;
        private bool _MonthlyBased;
        private string _ProgramDetailOrder;
        private string _Active;
        private string _CreatedBy;
        private DateTime? _CreatedDateTime;
        private string _ModifiedBy;
        private DateTime? _ModifiedDateTime;
        private string _ProgramTerm_ProgramTermId;
        private string _ProgramCampus_ProgramCampusId;
        private string _ApplicationProgram_ApplicationProgramId;
        private string _ApplicationProgramDescription;


        #region public properties
        [Display(Name = "Program DetailsId")]
        public string ProgramDetailsId
        {
            get { return _ProgramDetailsId; }
            set { _ProgramDetailsId = value; }
        }
        [Display(Name = "Start Date")]
        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Display(Name = "Monthly Based")]
        public bool MonthlyBased
        {
            get { return _MonthlyBased; }
            set { _MonthlyBased = value; }
        }
        [Display(Name = "Program Detail Order")]
        public string ProgramDetailOrder
        {
            get { return _ProgramDetailOrder; }
            set { _ProgramDetailOrder = value; }
        }
        [Display(Name = "Active")]
        public string Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [Display(Name = "CreatedBy")]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Display(Name = "Created DateTime")]
        public DateTime? CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }
        [Display(Name = "ModifiedBy")]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [Display(Name = "ModifiedDateTime")]
        public DateTime? ModifiedDateTime
        {
            get { return _ModifiedDateTime; }
            set { _ModifiedDateTime = value; }
        }
        [Display(Name = "Term")]
        public string ProgramTerm_ProgramTermId
        {
            get { return _ProgramTerm_ProgramTermId; }
            set { _ProgramTerm_ProgramTermId = value; }
        }
        [Display(Name = "Campus")]
        public string ProgramCampus_ProgramCampusId
        {
            get { return _ProgramCampus_ProgramCampusId; }
            set { _ProgramCampus_ProgramCampusId = value; }
        }
        [Display(Name = "Application ProgramId")]
        public string ApplicationProgram_ApplicationProgramId
        {
            get { return _ApplicationProgram_ApplicationProgramId; }
            set { _ApplicationProgram_ApplicationProgramId = value; }
        }
        [Display(Name = "Application Program")]
        public string ApplicationProgramDescription
        {
            get { return _ApplicationProgramDescription; }
            set { _ApplicationProgramDescription = value; }
        }

        #endregion public properties

    }

    public class ProgramDetailSearchResultsFilter
    {
        private string _ProgramDetailsId;
        private DateTime? _StartDate;
        private bool _MonthlyBased;
        private string _ProgramDetailOrder;
        private string _Active;
        private string _CreatedBy;
        private DateTime? _CreatedDateTime;
        private string _ModifiedBy;
        private DateTime? _ModifiedDateTime;
        private string _ProgramTerm_ProgramTermId;
        private string _ProgramCampus_ProgramCampusId;
        private string _ApplicationProgram_ApplicationProgramId;
        private string _ApplicationProgramDescription;



        #region public properties
        [Display(Name = "Program DetailsId")]
        public string ProgramDetailsId
        {
            get { return _ProgramDetailsId; }
            set { _ProgramDetailsId = value; }
        }
        [Display(Name = "Start Date")]
        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Display(Name = "Monthly Based")]
        public bool MonthlyBased
        {
            get { return _MonthlyBased; }
            set { _MonthlyBased = value; }
        }
        [Display(Name = "Program Detail Order")]
        public string ProgramDetailOrder
        {
            get { return _ProgramDetailOrder; }
            set { _ProgramDetailOrder = value; }
        }
        [Display(Name = "Active")]
        public string Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [Display(Name = "CreatedBy")]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Display(Name = "Created DateTime")]
        public DateTime? CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }
        [Display(Name = "ModifiedBy")]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [Display(Name = "ModifiedDateTime")]
        public DateTime? ModifiedDateTime
        {
            get { return _ModifiedDateTime; }
            set { _ModifiedDateTime = value; }
        }
        [Display(Name = "Program TermId")]
        public string ProgramTerm_ProgramTermId
        {
            get { return _ProgramTerm_ProgramTermId; }
            set { _ProgramTerm_ProgramTermId = value; }
        }
        [Display(Name = "Program CampusId")]
        public string ProgramCampus_ProgramCampusId
        {
            get { return _ProgramCampus_ProgramCampusId; }
            set { _ProgramCampus_ProgramCampusId = value; }
        }
        [Display(Name = "Application ProgramId")]
        public string ApplicationProgram_ApplicationProgramId
        {
            get { return _ApplicationProgram_ApplicationProgramId; }
            set { _ApplicationProgram_ApplicationProgramId = value; }
        }
        [Display(Name = "Application Program")]
        public string ApplicationProgramDescription
        {
            get { return _ApplicationProgramDescription; }
            set { _ApplicationProgramDescription = value; }
        }

        #endregion public properties


    }


    // SentEmail Report Object
    public class SentEmailReportViewObj : IDisposable
    {
        // SentEmail Report Object
        private List<SentEmailReportSearchResultsFilter> _SentEmailReportSearchResultsFilter;
        private SentEmailReportSearchFilter _SentEmailReportSearchFilter;
        private PaginationFilter _PaginationFilter;
        private string _PageTitle;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        public SentEmailReportViewObj()
        {
            isInitialized = true;

            _SentEmailReportSearchFilter = this._SentEmailReportSearchFilter ?? new SentEmailReportSearchFilter();
            _SentEmailReportSearchResultsFilter = this._SentEmailReportSearchResultsFilter ?? new List<SentEmailReportSearchResultsFilter>();
            _PaginationFilter = this.Pagination ?? new PaginationFilter();
        }

        public void Dispose()
        {
            lcapasLogic.Dispose();
            this.Dispose();
        }

        public bool Load()
        {
            bool success = false;


            try
            {
                _PageTitle = Structs.ReportPageTitle.SentEmailReports;

                success = lcapasLogic.SentEmailReports(this);
                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.SettingReport, "SentEmailReportViewObj.Load", "Error", ex.ToString());
            }
            return success;
        }


        public bool isInitialized;

        public List<SentEmailReportSearchResultsFilter> SentEmailReportSearchResultsFilter { get { return _SentEmailReportSearchResultsFilter; } set { _SentEmailReportSearchResultsFilter = value; } }

        public SentEmailReportSearchFilter SentEmailReportSearchFilter { get { return _SentEmailReportSearchFilter; } set { _SentEmailReportSearchFilter = value; } }

        public string PageTitle { get { return _PageTitle; } set { _PageTitle = value; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }
    }

    public class SentEmailReportObj
    {
        #region private properties
        private string _SentEmailId = string.Empty;
        private string _EmailType = string.Empty;
        private string _Subject = string.Empty;
        private string _Body = string.Empty;
        private string _From = string.Empty;
        private string _To = string.Empty;
        private string _CreatedBy = string.Empty;
        private DateTime? _CreatedDateTime;
        private string _ModifiedBy = string.Empty;
        private DateTime? _ModifiedDateTime;

        #endregion private properties

        #region public properties
        public string SentEmailId
        {
            get { return _SentEmailId; }
            set { _SentEmailId = value; }
        }
        public string EmailType
        {
            get { return _EmailType; }
            set { _EmailType = value; }
        }
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        public string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }
        public string From
        {
            get { return _From; }
            set { _From = value; }
        }
        public string To
        {
            get { return _To; }
            set { _To = value; }
        }
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public DateTime? CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public DateTime? ModifiedDateTime
        {
            get { return _ModifiedDateTime; }
            set { _ModifiedDateTime = value; }
        }

        #endregion public properties
    }

    public class SentEmailReportSearchFilter
    {
        private string _SentEmailId;
        private string _EmailType;
        private string _Subject;
        private string _Body;
        private string _From;
        private string _To;
        private string _CreatedBy;
        private DateTime? _CreatedDateTime;
        private string _ModifiedBy;
        private DateTime? _ModifiedDateTime;

        #region public properties
        [Display(Name = "Sent Email ID")]
        public string SentEmailId
        {
            get { return _SentEmailId; }
            set { _SentEmailId = value; }
        }

        [Display(Name = "Email Type")]
        public string EmailType
        {
            get { return _EmailType; }
            set { _EmailType = value; }
        }

        [Display(Name = "Subject")]
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }

        [Display(Name = "Body")]
        public string Body
        {
            get { return _Body; }
            set { _Body= value; }
        }

        [Display(Name = "From")]
        public string From
        {
            get { return _From; }
            set { _From = value; }
        }

        [Display(Name = "To")]
        public string To
        {
            get { return _To; }
            set { _To= value; }
        }

        [Display(Name = "Created By")]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        [Display(Name = "Created DateTime")]
        public DateTime? CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }

        [Display(Name = "Modifieded By")]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }

        [Display(Name = "ModifiedBy DateTime")]
        public DateTime? ModifiedDateTime
        {
            get { return _ModifiedDateTime; }
            set { _ModifiedDateTime = value; }
        }
        #endregion public properties

    }

    public class SentEmailReportSearchResultsFilter
    {
        private string _SentEmailId = string.Empty;
        private string _EmailType = string.Empty;
        private string _Subject = string.Empty;
        private string _Body = string.Empty;
        private string _From = string.Empty;
        private string _To = string.Empty;
        private string _CreatedBy = string.Empty;
        private DateTime? _CreatedDateTime;
        private string _ModifiedBy = string.Empty;
        private DateTime? _ModifiedDateTime;

        #region public properties
        [Display(Name = "Sent Email ID")]
        public string SentEmailId
        {
            get { return _SentEmailId; }
            set { _SentEmailId = value; }
        }

        [Display(Name = "Email Type")]
        public string EmailType
        {
            get { return _EmailType; }
            set { _EmailType = value; }
        }

        [Display(Name = "Subject")]
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }

        [Display(Name = "Body")]
        public string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }

        [Display(Name = "From")]
        public string From
        {
            get { return _From; }
            set { _From = value; }
        }

        [Display(Name = "To")]
        public string To
        {
            get { return _To; }
            set { _To = value; }
        }

        [Display(Name = "Created By")]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        [Display(Name = "Created DateTime")]
        public DateTime? CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }

        [Display(Name = "Modifieded By")]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }

        [Display(Name = "Modified DateTime")]
        public DateTime? ModifiedDateTime
        {
            get { return _ModifiedDateTime; }
            set { _ModifiedDateTime = value; }
        }
        #endregion public properties

    }


    // System Settings Object
    public class SystemSettingsViewObj : IDisposable
    {
        // System Settings Object
        private List<BooleanValue> _BooleanSettings;
        private List<ServerStatusObj> _ServerList;
        private Dictionary<string, bool> _WebServiceList;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        public SystemSettingsViewObj()
        {
            _BooleanSettings = this._BooleanSettings ?? new List<BooleanValue>();
        }

        public bool Load()
        {
            bool success = false;

            try
            {
                _BooleanSettings = lcapasLogic.GetBooleanSettings();
                success = _BooleanSettings.Any();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.Settings, "SystemSettingsViewObj.Load", "Error", ex.ToString());
            }
            return success;
        }

        public bool Save()
        {
            bool success = false;

            try
            {
                success = lcapasLogic.SaveBooleanSettings(this._BooleanSettings);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.Settings, "SystemSettingsViewObj.Save", "Error", ex.ToString());
            }
            return success;
        }

        public bool GetServerStatus()
        {
            bool success = false;
            _ServerList = new List<ServerStatusObj>();
            Dictionary<string, string> serverList = new Dictionary<string, string>();

            try
            {
                // Check Server Status
                serverList = lcapasLogic.GetServerSettings();

                foreach (var server in serverList)
                {
                    _ServerList.Add(Functions.IsServerAvailable(server.Key, server.Value));
                }

                // Check WebService Status
                _WebServiceList = Functions.IsWebServiceRunning();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.Settings, "SystemSettingsViewObj.GetServerStatus", "Error", ex.ToString());
            }
            return success;
        }


        public void Dispose()
        {
            lcapasLogic.Dispose();
            this.Dispose();
        }

        public List<BooleanValue> BooleanSettings { get { return _BooleanSettings; } set { _BooleanSettings = value; } }
        [Display(Name = "APAS Toolkit Database")]
        public bool IsApasDatabaseConnected { get { return Functions.IsDatabaseConnected("lcapasdb_entities"); }  }
        [Display(Name = "Colleague Database")]
        public bool IsColleagueDatabaseConnected { get { return Functions.IsDatabaseConnected("colldb_entities"); } }
        public List<ServerStatusObj> ServerList { get { return _ServerList; } set { _ServerList = value; } }
        [Display(Name = "Web Service List")]
        public Dictionary<string, bool> WebServiceList { get { return _WebServiceList; } set { _WebServiceList = value; } }

    }

    // Server Status Object
    public class ServerStatusObj : IDisposable
    {
        private string _URL = string.Empty;
        private string _IPAddress = string.Empty;
        private long _RoundTrip = 0;
        private int _TimeToLive = 0;
        private bool _DontFragment = false;
        private int _BufferSize = 0;
        private string _Status = string.Empty;
        private Dictionary<string, bool> _ServiceList;

        public ServerStatusObj()
        {

        }

        public void Dispose()
        {
            this.Dispose();
        }

        [Display(Name = "URL")]
        public string URL { get { return _URL; } set { _URL = value; } }
        [Display(Name = "IP Address")]
        public string IPAddress { get { return _IPAddress; } set { _IPAddress = value; } }
        [Display(Name = "Round Trip Time")]
        public long RoundTrip { get { return _RoundTrip; } set { _RoundTrip = value; } }
        [Display(Name = "Time to Live")]
        public int TimeToLive { get { return _TimeToLive; } set { _TimeToLive = value; } }
        [Display(Name = "Don't Fragment")]
        public bool DontFragment { get { return _DontFragment; } set { _DontFragment = value; } }
        [Display(Name = "Buffer Size")]
        public int BufferSize { get { return _BufferSize; } set { _BufferSize = value; } }
        [Display(Name = "Status")]
        public string Status { get { return _Status; } set { _Status = value; } }
        [Display(Name = "Service List")]
        public Dictionary<string, bool> ServiceList { get { return _ServiceList; } set { _ServiceList = value; } }
    }

    // System Settings Object
    public class PermissionsViewObj : IDisposable
    {
        // System Settings Object
        private List<ActionType> _ActionTypes;
        private List<ControllerType> _ControllerTypes;
        private List<PermissionRecord> _PermissionRecords;
        private List<SelectListItem> _EnumControllerType;
        private List<SelectListItem> _EnumActionType;
        private List<SelectListItem> _AccessGroupIds;
        private int? _SaveType;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        public PermissionsViewObj()
        {
            _ActionTypes = this._ActionTypes ?? new List<ActionType>();
            _ControllerTypes = this._ControllerTypes ?? new List<ControllerType>();
            _PermissionRecords = this._PermissionRecords ?? new List<PermissionRecord>();
        }

        public bool Load()
        {
            bool success = false;

            try
            {
                _ActionTypes = lcapasLogic.GetActionTypes();
                _ControllerTypes = lcapasLogic.GetControllerTypes();
                _PermissionRecords = lcapasLogic.GetPermissionRecords();
                _AccessGroupIds = lcapasLogic.GetAccessGroup().Select(x => new SelectListItem {
                                                                    Text = x.AccessGroupDesc,
                                                                    Value = x.AccessGroupId.ToString(),
                                                                }).ToList();

                // Get Controller Type Enumeration
                _EnumControllerType = Enum.GetValues(typeof(Enums.ControllerTypeType)).Cast<Enums.ControllerTypeType>()
                                     .Select(v => new SelectListItem {
                                            Text = v.ToString(),
                                            Value = ((int)v).ToString()
                                        }).ToList();

                // Get Action Type Enumeration
                _EnumActionType = Enum.GetValues(typeof(Enums.ActionTypeType)).Cast<Enums.ActionTypeType>()
                                     .Select(v => new SelectListItem
                                     {
                                         Text = v.ToString(),
                                         Value = ((int)v).ToString()
                                     }).ToList();

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.Settings, "PermissionsViewObj.Load", "Error", ex.ToString());
            }
            return success;
        }

        public void Dispose()
        {
            lcapasLogic.Dispose();
            this.Dispose();
        }

        public List<ActionType> ActionTypes { get { return _ActionTypes; } set { _ActionTypes = value; } }
        public List<ControllerType> ControllerTypes { get { return _ControllerTypes; } set { _ControllerTypes = value; } }
        public List<PermissionRecord> PermissionRecords { get { return _PermissionRecords; } set { _PermissionRecords = value; } }
        public List<SelectListItem> EnumControllerType { get { return _EnumControllerType; } set { _EnumControllerType = value; } }
        public List<SelectListItem> EnumActionType { get { return _EnumActionType; } set { _EnumActionType = value; } }
        public List<SelectListItem> AccessGroupIds { get { return _AccessGroupIds; } set { _AccessGroupIds = value; } }
        public int? SaveType { get { return _SaveType; } set { _SaveType = value; } }

    }

}





