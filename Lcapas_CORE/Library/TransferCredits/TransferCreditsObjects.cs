using Jitbit.Utils;
using Lcapas.Core.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Lcapas.Core.Library
{

    // Transfer Credit Object
    #region Transfer Credit Object
    public class TransferCreditViewObj : IDisposable
    {
        private List<TransferCreditSearchResultsFilter> _TransferCreditSearchResultsFilter;
        private TransferCreditSearchFilter _TransferCreditSearchFilter;
        private PaginationFilter _PaginationFilter;
        private string _PageTitle;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        public TransferCreditViewObj()
        {
            isInitialized = true;

            _TransferCreditSearchFilter = this._TransferCreditSearchFilter ?? new TransferCreditSearchFilter();
            _TransferCreditSearchResultsFilter = this._TransferCreditSearchResultsFilter ?? new List<TransferCreditSearchResultsFilter>();
            _PaginationFilter = this.Pagination ?? new PaginationFilter();
            //ReportSearchFilter.Fromdate = DateTime.Now;
            //ReportSearchFilter.Todate = DateTime.Now;
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
                _PageTitle = Structs.TransferCreditPageTitle.TransferCredits;

                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    success = collLogic.LoadTransferCredits(this);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TransferCredit, "TransferCreditObj.Load", "Error", ex.ToString());
            }
            return success;
        }

        public byte[] Export(string[] reportID, bool allSelected = false, string filterFields = null)
        {
            var csvFile = new CsvExport();

            try
            {
                List<TransferCreditSearchResultsFilter> _TransferCreditList = new List<TransferCreditSearchResultsFilter>();

                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    _TransferCreditList = collLogic.GetTransferCreditsReportByIds(reportID, allSelected, filterFields);
                }

                foreach (TransferCreditSearchResultsFilter _TransferCredit in _TransferCreditList)
                {
                    csvFile.AddRow();
                    csvFile["ASN"] = _TransferCredit.ASN;
                    csvFile["Birth Date"] = _TransferCredit.BirthDate;
                    csvFile["Provider"] = _TransferCredit.Provider;
                    //csvFile["Academic Year"] = _TransferCredit.AcademicYear;
                    //csvFile["Program ID"] = _TransferCredit.ProgramID;
                    //csvFile["Specialization ID"] = _TransferCredit.SpecializationID;
                    csvFile["From Institution"] = _TransferCredit.FromInstitution;
                    csvFile["From Institution Loc"] = _TransferCredit.FromInstitutionLoc;
                    csvFile["From Institution Course Code"] = _TransferCredit.FromInstitutionCourseCode;
                    csvFile["From Institution Academic Year Course Taken"] = _TransferCredit.FromInstitutionAcademicYearCourseTaken;
                    //csvFile["TCA Course Code"] = _TransferCredit.TCACourseCode;
                    csvFile["TCA For Course Transfer"] = _TransferCredit.TCAForCourseTransfer;
                    csvFile["TCA Academic Year"] = _TransferCredit.TCAAcadmicYear;
                    csvFile["_TCT BY Course;"] = _TransferCredit.TCTBYCourse;
                    csvFile["_TCT For PLAR;"] = _TransferCredit.TCTForPLAR;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TransferCredit, "TransferCreditViewObj.Export", "Error", ex.ToString());
            }

            return csvFile.ExportToBytes();
        }


        public bool isInitialized;

        public List<TransferCreditSearchResultsFilter> ReportSearchResultsFilter { get { return _TransferCreditSearchResultsFilter; } set { _TransferCreditSearchResultsFilter = value; } }

        public TransferCreditSearchFilter ReportSearchFilter { get { return _TransferCreditSearchFilter; } set { _TransferCreditSearchFilter = value; } }

        public string PageTitle { get { return _PageTitle; } set { _PageTitle = value; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }
    }

    #endregion

    public class TransferCreditSearchFilter
    {
        #region private properties

        private string _ASN;
        private DateTime? _BirthDate;
        private string _Provider;
        private string _AcademicYear;
        private string _ProgramID;
        private string _SpecializationID;
        private string _FromInstitution;
        private string _FromInstitutionLoc;
        private string _FromInstitutionCourseCode;
        private string _FromInstitutionAcademicYearCourseTaken;
        private string _TCACourseCode;
        private string _TCAForCourseTransfer;
        private string _TCAAcadmicYear;
        private string _TCTBYCourse;
        private string _TCTForPLAR;

        #endregion

        #region public properties

        [Display(Name = "ASN")]
        public string ASN
        {
            get { return _ASN; }
            set { _ASN = value; }
        }

        [Display(Name = "Birth Date")]
        public DateTime? BirthDate
        {
            get { return _BirthDate; }
            set { _BirthDate = value; }
        }

        [Display(Name = "Provider")]
        public string Provider
        {
            get { return _Provider; }
            set { _Provider = value; }
        }

        [Display(Name = "AcademicYear")]
        public string AcademicYear
        {
            get { return _AcademicYear; }
            set { _AcademicYear = value; }
        }

        [Display(Name = "ProgramID")]
        public string ProgramID
        {
            get { return _ProgramID; }
            set { _ProgramID = value; }
        }

        [Display(Name = "SpecializationID")]
        public string SpecializationID
        {
            get { return _SpecializationID; }
            set { _SpecializationID = value; }
        }

        [Display(Name = "From Institution")]
        public string FromInstitution
        {
            get { return _FromInstitution; }
            set { _FromInstitution = value; }
        }

        [Display(Name = "From InstitutionLoc")]
        public string FromInstitutionLoc
        {
            get { return _FromInstitutionLoc; }
            set { _FromInstitutionLoc = value; }
        }


        [Display(Name = "From Institution Course Code")]
        public string FromInstitutionCourseCode
        {
            get { return _FromInstitutionCourseCode; }
            set { _FromInstitutionCourseCode = value; }
        }

        [Display(Name = "From Institution Academic Year Course Taken")]
        public string FromInstitutionAcademicYearCourseTaken
        {
            get { return _FromInstitutionAcademicYearCourseTaken; }
            set { _FromInstitutionAcademicYearCourseTaken = value; }
        }

        [Display(Name = "TCA Course Code")]
        public string TCACourseCode
        {
            get { return _TCACourseCode; }
            set { _TCACourseCode = value; }
        }

        [Display(Name = "TCA For Course Transfer")]
        public string TCAForCourseTransfer
        {
            get { return _TCAForCourseTransfer; }
            set { _TCAForCourseTransfer = value; }
        }

        [Display(Name = "TCA Acadmic Year")]
        public string TCAAcadmicYear
        {
            get { return _TCAAcadmicYear; }
            set { _TCAAcadmicYear = value; }
        }


        [Display(Name = "TCT BY Course")]
        public string TCTBYCourse
        {
            get { return _TCTBYCourse; }
            set { _TCTBYCourse = value; }
        }

        [Display(Name = "TCT For PLAR")]
        public string TCTForPLAR
        {
            get { return _TCTForPLAR; }
            set { _TCTForPLAR = value; }
        }
        #endregion
    }

    public class TransferCreditSearchResultsFilter
    {
        #region private properties

        private string _sNumber;
        private string _ASN;
        private DateTime? _BirthDate;
        private string _Provider;
        private string _AcademicYear;
        private string _ProgramID;
        private DateTime? _StartTermDate;
        private string _SpecializationID;
        private string _FromInstitution;
        private TransferCreditSearchFilter SubElement;
        private string _FromInstitutionLoc;
        private string _FromInstitutionCourseCode;
        private string _FromInstitutionAcademicYearCourseTaken;
        private string _TCACourseCode;
        private string _TCAForCourseTransfer;
        private string _TCAAcadmicYear;
        private string _TCTBYCourse;
        private string _TCTForPLAR;

        #endregion

        #region public properties

        [Display(Name = "sNumber")]
        public string sNumber
        {
            get { return _sNumber; }
            set { _sNumber = value; }
        }

        [Display(Name = "ASN")]
        public string ASN
        {
            get { return _ASN; }
            set { _ASN = value; }
        }

        [Display(Name = "Birth Date")]
        public DateTime? BirthDate
        {
            get { return _BirthDate; }
            set { _BirthDate = value; }
        }

        [Display(Name = "Provider")]
        public string Provider
        {
            get { return _Provider; }
            set { _Provider = value; }
        }

        [Display(Name = "AcademicYear")]
        public string AcademicYear
        {
            get { return _AcademicYear; }
            set { _AcademicYear = value; }
        }

        [Display(Name = "ProgramID")]
        public string ProgramID
        {
            get { return _ProgramID; }
            set { _ProgramID = value; }
        }

        [Display(Name = "Start Term")]
        public DateTime? StartTermDate
        {
            get { return _StartTermDate; }
            set { _StartTermDate = value; }
        }

        [Display(Name = "SpecializationID")]
        public string SpecializationID
        {
            get { return _SpecializationID; }
            set { _SpecializationID = value; }
        }

        [Display(Name = "From Institution")]
        public string FromInstitution
        {
            get { return _FromInstitution; }
            set { _FromInstitution = value; }
        }

        [Display(Name = "From InstitutionLoc")]
        public string FromInstitutionLoc
        {
            get { return _FromInstitutionLoc; }
            set { _FromInstitutionLoc = value; }
        }


        [Display(Name = "From Institution Course Code")]
        public string FromInstitutionCourseCode
        {
            get { return _FromInstitutionCourseCode; }
            set { _FromInstitutionCourseCode = value; }
        }

        [Display(Name = "From Institution Academic Year Course Taken")]
        public string FromInstitutionAcademicYearCourseTaken
        {
            get { return _FromInstitutionAcademicYearCourseTaken; }
            set { _FromInstitutionAcademicYearCourseTaken = value; }
        }

        [Display(Name = "TCA Course Code")]
        public string TCACourseCode
        {
            get { return _TCACourseCode; }
            set { _TCACourseCode = value; }
        }

        [Display(Name = "TCA For Course Transfer")]
        public string TCAForCourseTransfer
        {
            get { return _TCAForCourseTransfer; }
            set { _TCAForCourseTransfer = value; }
        }

        [Display(Name = "TCA Acadmic Year")]
        public string TCAAcadmicYear
        {
            get { return _TCAAcadmicYear; }
            set { _TCAAcadmicYear = value; }
        }


        [Display(Name = "TCT BY Course")]
        public string TCTBYCourse
        {
            get { return _TCTBYCourse; }
            set { _TCTBYCourse = value; }
        }

        [Display(Name = "TCT For PLAR")]
        public string TCTForPLAR
        {
            get { return _TCTForPLAR; }
            set { _TCTForPLAR = value; }
        }
        #endregion

    }
}

public class TCA
{
    private List<TCALearner> _TCALearner;
    private LcapasLogic lcapasLogic = new LcapasLogic();


    public byte[] XMLExport(string[] reportID, bool allSelected = false, string filterFields = null)
    {
        List<TCA> _TCAObjList = new List<TCA>();
        byte[] result = null;

        try
        {
            TCA TCA = new TCA();

            using (ColleagueLogic collLogic = new ColleagueLogic())
            {
                TCA = collLogic.GetXMLTransferCreditsReportByIds(reportID, allSelected, filterFields);
            }

            if (TCA != null)
            {
                result = System.Text.Encoding.ASCII.GetBytes(TCA.TCALearner.Serialize().Replace("ArrayOfTCALearner", "TCA"));
            }
        }
        catch (Exception ex)
        {
            lcapasLogic.SaveException(Lcapas.Core.Library.Structs.Project.LcapasAdmin, Lcapas.Core.Library.Structs.Class.TransferCredit, "TCAObj.XMLExport", "Error", ex.ToString());
        }
           
        return result;
    }


    [Display(Name = "TCALearner")]
    public List<TCALearner> TCALearner
    {
        get { return _TCALearner; }
        set { _TCALearner = value; }
    }

}

public class TCALearner
{
    private TransferCreditObj _TransferCredit;
    private string _ASN;
    private string _Birthdate;
    private string _Provider;
    private string _AcademicYear;
    private string _ProgramID;
    private string _SpecializationID;
    private string _SNumber;
    private DateTime? _StartTermDate;



    public TCALearner()
    {
            
    }

    [Display(Name = "TransferCredit")]
    public TransferCreditObj TransferCredit
    {
        get { return _TransferCredit; }
        set { _TransferCredit = value; }
    }

    [Display(Name = "ASN")]
    public string ASN
    {
        get { return _ASN; }
        set { _ASN = value; }
    }

    [Display(Name = "Birthdate")]
    public string Birthdate
    {
        get { return _Birthdate; }
        set { _Birthdate = value; }
    }

    [Display(Name = "Provider")]
    public string Provider
    {
        get { return _Provider; }
        set { _Provider = value; }
    }

    [Display(Name = "Academic Year")]
    public string AcademicYear
    {
        get { return _AcademicYear; }
        set { _AcademicYear = value; }
    }

    [Display(Name = "Program ID")]
    public string ProgramID
    {
        get { return _ProgramID; }
        set { _ProgramID = value; }
    }

    [Display(Name = "Specialization ID")]
    public string SpecializationID
    {
        get { return _SpecializationID; }
        set { _SpecializationID = value; }
    }

    [XmlIgnoreAttribute]
     public string SNumber
    {
        get { return _SNumber; }
        set { _SNumber = value; }
    }

    [XmlIgnoreAttribute]
    public DateTime? StartTermDate
    {
        get { return _StartTermDate; }
        set { _StartTermDate = value; }
    }

}

public class TransferCreditObj
{
    private string _FromInstitution;
    private string _FILocation;
    private string _FICourseCode;
    private string _FIAcademicYearCourseTaken;
    private string _TCACourseCode;
    private string _TCAForCourseTransfer;
    private string _TCAAcademicYear;
    private string _TCTByCourse;
    private string _TCTForPLAR;
    private string _Notes;

    public TransferCreditObj()
    {

    }

    [Display(Name = "From Institution")]
    public string FromInstitution
    {
        get { return _FromInstitution; }
        set { _FromInstitution = value; }
    }

    [Display(Name = "FILocation")]
    public string FILocation
    {
        get { return _FILocation; }
        set { _FILocation = value; }
    }

    [Display(Name = "FICourseCode")]
    public string FICourseCode
    {
        get { return _FICourseCode; }
        set { _FICourseCode = value; }
    }

    [Display(Name = "FIAcademicYearCourseTaken")]
    public string FIAcademicYearCourseTaken
    {
        get { return _FIAcademicYearCourseTaken; }
        set { _FIAcademicYearCourseTaken = value; }
    }

    [Display(Name = "TCACourseCode")]
    public string TCACourseCode
    {
        get { return _TCACourseCode; }
        set { _TCACourseCode = value; }
    }

    [Display(Name = "TCAForCourseTransfer")]
    public string TCAForCourseTransfer
    {
        get { return _TCAForCourseTransfer; }
        set { _TCAForCourseTransfer = value; }
    }

    [Display(Name = "TCAAcademicYear")]
    public string TCAAcademicYear
    {
        get { return _TCAAcademicYear; }
        set { _TCAAcademicYear = value; }
    }

    [Display(Name = "TCTByCourse")]
    public string TCTByCourse
    {
        get { return _TCTByCourse; }
        set { _TCTByCourse = value; }
    }

    [Display(Name = "_TCTForPLAR")]
    public string TCTForPLAR
    {
        get { return _TCTForPLAR; }
        set { _TCTForPLAR = value; }
    }

    [Display(Name = "Notes")]
    public string Notes
    {
        get { return _Notes; }
        set { _Notes = value; }
    }
}






    



