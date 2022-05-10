using Lcapas.Core.Library.Apas.CoreMain;
using Lcapas.Core.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lcapas.Core.Library
{
    #region Simple Objects
    public class UserResultObj
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ItemId { get; set; }
        public string DocType { get; set; }
    }

    public class LandingObj
    {
        public bool isInitialized;
        public LandingObj()
        {
            isInitialized = true;
        }

        [Required]
        public string UUID { get; set; }
        public string ApplicationID { get; set; }

        [Required]
        [Display(Name = "Alberta Education Number")]
        [DataType(DataType.Text)]
        public string ASN { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "1. Program")]
        [DataType(DataType.Text)]
        public int ProgramSelected { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "2. Session Start")]
        [DataType(DataType.Text)]
        public int AppliedSession { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "3. Campus")]
        [DataType(DataType.Text)]
        public int Campus { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "4. Part/Full Time?")]
        public string FullTime { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "5. Starting year of study")]
        [DataType(DataType.Text)]
        public int StartingYear { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "6. Have you previously applied to Lethbridge College?")]
        [DataType(DataType.Text)]
        public bool PreviouslyApplied { get; set; }

        [Display(Name = "7. What is your previous sNumber?")]
        [DataType(DataType.Text)]
        public string PreviousStuId { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "8. Will you require additional support?")]
        public bool AdditionalSupport { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "9. Please provide the name of an emergency contact")]
        [DataType(DataType.Text)]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "10. Emergency contact phone number e.g. 4033203200")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(Functions.phoneRegex, ErrorMessage = " ")]
        public string ContactPhone { get; set; }

        [Display(Name = "11. How did you first hear about us?")]
        [DataType(DataType.Text)]
        public string ReferenceSource { get; set; }

        [Display(Name = "Application Fee")]
        [DataType(DataType.Text)]
        public bool ApplicationFee { get; set; }

        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        [Display(Name = "Address Line1")]
        public string AddressLine { get; set; }

        [Display(Name = "Address Line2")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        public bool City { get; set; }

        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        public bool State { get; set; }

        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        [Display(Name = "Zip")]
        [DataType(DataType.Text)]
        public bool Zip { get; set; }

        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        [Display(Name = "Country")]
        [DataType(DataType.Text)]
        public bool Country { get; set; }

        [Required]
        public int TermId { get; set; }

        [Required]
        public int CampusId { get; set; }

        [Required]
        public int ProgramId { get; set; }

    }

    public class ProgTermCampObj
    {
        public int Program { get; set; }
        public int Term { get; set; }
        public string TermCode { get; set; }
        public string TermDesc { get; set; }
        public int Campus { get; set; }
        public int? Order { get; set; }
        public bool? Active { get; set; }
    }

    public class ApplInstObj
    {
        public string InstitutionName { get; set; }
        public string LocalOrganizationID { get; set; }
        public string InstitutionID { get; set; }
        public string CityProv { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
        public string Duration { get; set; }
        public string HighestGrade { get; set; }
        public string HighestGradeDesc { get; set; }
        public string StudentLevel { get; set; }
    }

    public class ApplCrsObj
    {
        public string CourseName { get; set; }
        public string CourseId { get; set; }
        public string Grade { get; set; }
        public string Credit { get; set; }
        public string Status { get; set; }
        public string EndDate { get; set; }
    }

    #endregion Simple Objects

    #region Complex Objects

    public class AdmissionsApplicationObj
    {
        private List<ApplicationSearchResultsFilter> _ApplicationSearchResultsFilter;
        private ApplicationSearchFilter _ApplicationSearchFilter;
        private PaginationFilter _PaginationFilter;
        private string _PageTitle;
        private bool _Datashare;

        private List<SelectListItem> _CitizenshipStatusSelectedList;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        public AdmissionsApplicationObj()
        {
            isInitialized = true;

            _ApplicationSearchFilter = this._ApplicationSearchFilter ?? new ApplicationSearchFilter();
            _ApplicationSearchResultsFilter = this.ApplicationSearchResultsFilter ?? new List<ApplicationSearchResultsFilter>();
            _PaginationFilter = this.Pagination ?? new PaginationFilter();
        }

        public bool Load()
        {
            bool success = false;
            try
            {
                // TODO: Take a look at this - breaking
                //lcapasLogic.ReturnPaidApplications();

                _PageTitle = this.Datashare ? Structs.ApplicationPageTitle.Datashare : Structs.ApplicationPageTitle.Applications;

                _CitizenshipStatusSelectedList = lcapasLogic.GetCitizenshipStatus();

                success = lcapasLogic.LoadAdmissionsApplications(this);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.AdmissionsApplication, "AdmissionsApplicationObj.Load", "Error", ex.ToString());
            }
            return success;
        }


        public bool isInitialized;

        public List<ApplicationSearchResultsFilter> ApplicationSearchResultsFilter { get { return _ApplicationSearchResultsFilter; } set { _ApplicationSearchResultsFilter = value; } }

        public ApplicationSearchFilter ApplicationSearchFilter { get { return _ApplicationSearchFilter; } set { _ApplicationSearchFilter = value; } }

        public string PageTitle { get { return _PageTitle; } set { _PageTitle = value; } }

        public bool Datashare { get { return _Datashare; } set { _Datashare = value; } }

        public PaginationFilter Pagination { get { return _PaginationFilter; } set { _PaginationFilter = value; } }
    
        public List<SelectListItem> CitizenshipStatusSelectedList { get { return _CitizenshipStatusSelectedList; } }
    }

    public class ApplicationsObj
    {
        private List<string> _UuidStrList;
        private DateTime _ReceivedDateTime = DateTime.Now;
        private List<ApplicationObj> _ApplicationFilters;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        public ApplicationsObj()
        {

        }

        public string ToXml()
        {
            string xmlString = string.Empty;

            try
            {
                xmlString = _ApplicationFilters.Serialize();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return xmlString;
        }

        // ELF import file definitions can be found in Colleague: form ELFF and File AAI.APPLICATIONS
        public bool Export()
        {
            bool success = false;
            int instCnt = 0;

            try
            {
                EdiObj MainEdiFile = new EdiObj(String.Format("LBAP{0}_main", _ReceivedDateTime.ToString("MMddyyHHmmss")));
                EdiObj InstEdiFile = new EdiObj(String.Format("LBAP{0}_inst", _ReceivedDateTime.ToString("MMddyyHHmmss")));
                EdiObj SummEdiFile = new EdiObj(String.Format("LBAP{0}", _ReceivedDateTime.ToString("MMddyyHHmmss")));

                // Get settings
                bool useOutsideNorthAmerica = lcapasLogic.GetSetting(Structs.SettingTypes.Boolean, Structs.Settings.UseOutsideNorthAmerica) ?? false;
                string HSOutsideNorthAmerica = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.HSOutsideNorthAmerica);
                string PSOutsideNorthAmerica = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PSOutsideNorthAmerica);

                foreach (var filter in _ApplicationFilters)
                {
                    MainEdiFile.AddField(10, string.Empty);
                    MainEdiFile.AddField(45, filter.ASN);
                    MainEdiFile.AddField(30, filter.LastName);
                    MainEdiFile.AddField(30, filter.FirstName);
                    MainEdiFile.AddField(15, filter.MiddleName);
                    MainEdiFile.AddField(25, string.Empty);
                    MainEdiFile.AddField(30, filter.AddressLine1);
                    MainEdiFile.AddField(60, filter.AddressLine2);
                    MainEdiFile.AddField(25, filter.AddressCity);

                    if (filter.InCanada)
                    {
                        MainEdiFile.AddField(2, filter.AddressState);
                    }
                    else
                    {
                        MainEdiFile.AddField(2, string.Empty);
                    }

                    MainEdiFile.AddField(10, filter.AddressPostalCode);
                    MainEdiFile.AddField(7, string.Empty);  // County

                    if (!filter.InCanada)
                    {
                        MainEdiFile.AddField(8, filter.AddressCountry);
                    }
                    else
                    {
                        MainEdiFile.AddField(8, string.Empty);
                    }

                    // Permanent/Home Phone Number
                    if (string.IsNullOrWhiteSpace(filter.PermanentPhoneNumber))
                    {
                        MainEdiFile.AddField(22, string.Empty);
                    }
                    else
                    {
                        MainEdiFile.AddField(12, Functions.CleanPhone(filter.PermanentPhoneNumber, "-", 12));
                        MainEdiFile.AddField(5, string.Empty);  // Phone number extension
                        MainEdiFile.AddField(5, "HO");  // Home Phone
                    }

                    // Mobile Phone Number
                    if (string.IsNullOrWhiteSpace(filter.MobilePhoneNumber))
                    {
                        MainEdiFile.AddField(22, string.Empty);
                    }
                    else
                    {
                        MainEdiFile.AddField(12, Functions.CleanPhone(filter.MobilePhoneNumber, "-", 12));
                        MainEdiFile.AddField(5, string.Empty);  // Phone number extension
                        MainEdiFile.AddField(5, "CP");  // Cell Phone
                    }

                    //// Preferred Phone Number
                    //if (string.IsNullOrWhiteSpace(filter.PreferredPhoneNumber))
                    //{
                    //    MainEdiFile.AddField(22, string.Empty);
                    //}
                    //else
                    //{
                    //    MainEdiFile.AddField(12, Functions.CleanPhone(filter.PreferredPhoneNumber, "-", 12));
                    //    MainEdiFile.AddField(5, string.Empty);  // Phone number extension
                    //    MainEdiFile.AddField(5, "CP");  // Cell Phone
                    //}

                    // Day Phone Number
                    if (string.IsNullOrWhiteSpace(filter.DayPhoneNumber))
                    {
                        MainEdiFile.AddField(53, string.Empty);
                    }
                    else
                    {
                        MainEdiFile.AddField(12, Functions.CleanPhone(filter.DayPhoneNumber, "-", 12));
                        MainEdiFile.AddField(5, Functions.CleanPhone(filter.DayPhoneExtensionNumber, "-", 5));  // Phone number extension
                        MainEdiFile.AddField(36, "BU");  // Business Phone
                    }

                    MainEdiFile.AddField(8, filter.BirthDateEdi);
                    MainEdiFile.AddField(5, filter.RaceEthnicity);
                    MainEdiFile.AddField(1, (filter.Gender == "F" || filter.Gender == "M" ) ? filter.Gender : string.Empty);
                    MainEdiFile.AddField(10, filter.MaritalStatus);
                    MainEdiFile.AddField(50, filter.Email);
                    MainEdiFile.AddField(483, "OFF"); // Off campus email type for colleague import
                    MainEdiFile.AddField(15, filter.FirstLearned);
                    MainEdiFile.AddField(78, filter.Program);
                    MainEdiFile.AddField(10, filter.ProgramLocation);
                    MainEdiFile.AddField(19, filter.AdmitStatus);
                    MainEdiFile.AddField(33, filter.StudyLoad);
                    MainEdiFile.AddField(7, filter.Term);
                    MainEdiFile.AddField(58, filter.CitizenshipCountryCode);
                    MainEdiFile.AddField(19, filter.FirstLanguage);
                    MainEdiFile.AddField(44, filter.CitizenshipCountryCode);
                    MainEdiFile.AddField(8, filter.ImmigrationEntryDateEdi);
                    MainEdiFile.AddField(122, filter.CitizenshipStatusCode);

                    //MainEdiFile.AddField(175, filter.FormerNames);
                    MainEdiFile.AddField(25, filter.FormerLastName);
                    MainEdiFile.AddField(10, filter.FormerFirstName);
                    MainEdiFile.AddField(10, filter.FormerMiddleName);
                    MainEdiFile.AddField(130, string.Empty);

                    MainEdiFile.AddField(30, filter.EmergencyContactName);
                    MainEdiFile.AddField(12, string.Empty);
                    MainEdiFile.AddField(500, string.Empty);  // User Fields
                    MainEdiFile.AddField(5, string.Empty);  // Catalog
                    MainEdiFile.AddField(50, string.Empty);  // Ethincs
                    MainEdiFile.AddField(50, string.Empty);  // Races
                    MainEdiFile.AddField(150, string.Empty);  // Emergency Contact 1
                    MainEdiFile.AddField(150, string.Empty);  // Emergency Contact 2
                    MainEdiFile.AddField(38, string.Empty);  // Consent, Persona, Source

                    MainEdiFile.AddField(20, string.Empty);  // FA.MISC1, FA.MISC2
                    MainEdiFile.AddField(40, string.Empty);  // FA.MERIT.AWARD.1, 2, 3, 4, 5
                    MainEdiFile.AddField(40, string.Empty);  // FA.NON.MERIT.AWARD.1, 2, 3, 4, 5
                    MainEdiFile.AddField(5, string.Empty);  // APPL.PRIORITY
                    MainEdiFile.AddField(40, string.Empty);  // FA.MERIT.AWARD.6, 7, 8, 9, 10
                    MainEdiFile.AddField(40, string.Empty);  // FA.NON.MERIT.AWARD.6, 7, 8, 9, 10

                    MainEdiFile.AddField(30, string.Empty);  // AAIAP.CHOSEN.FIRST.NAME
                    MainEdiFile.AddField(30, string.Empty);  // AAIAP.CHOSEN.MIDDLE.NAME
                    MainEdiFile.AddField(57, string.Empty);  // AAIAP.CHOSEN.LAST.NAME
                    MainEdiFile.AddField(10, filter.Gender);  // AAIAP.GENDER.IDENTITY: Same as GENDER
                    MainEdiFile.AddField(10, string.Empty);  // AAIAP.SEXUAL.ORIENTATION
                    MainEdiFile.AddField(6, string.Empty);  // AAIAP.PERSONAL.PRONOUN
                    MainEdiFile.AddField(5, string.Empty);  // AAIAP.APPL.STATUS
                    MainEdiFile.AddField(1, "Y");  // AAIAP.ADDR.PREF.RESIDENCE
                    MainEdiFile.AddField(1, "Y");  // AAIAP.ADDR.PREF.MAILING

                    MainEdiFile.AddField(5, "20"); // account code for colleague import (ECOM.AR.TYPE)
                    //If Application Fee is $0.00, send $1.00 as MasterCard to pass Elf Import process
                    MainEdiFile.AddField(10, string.IsNullOrWhiteSpace(filter.PayPalCardType) && filter.ApplicationFee == 0 ? "PAYMTMCRD" : filter.PayPalCardType);  // Distr. Code + Payment Method (Visa or MasterCard) - (ECOM.GL.DISTR.CODE, ECOM.PAYMENT.METHOD)
                    MainEdiFile.AddField(10, filter.ApplicationFee == 0 ? "100" : filter.ApplicationFee.ToString());  // ECOM.AMOUNT
                    MainEdiFile.AddField(10, filter.ApplicationFee == 0 ? "100" : filter.ApplicationFee.ToString());  // ECOM.TOTAL
                    MainEdiFile.AddField(8, filter.ReceivedDateTime);  // ECOM.DATE
                    MainEdiFile.AddField(20, filter.EmergencyContactPhone);  // EMER.PHONE
                    MainEdiFile.AddField(1, filter.FamilyAttendedCollege);  // PARENT.EDUC.LEVEL

                    MainEdiFile.AddLine();

                    foreach (var institution in filter.ApplicationInstitutions)
                    {
                        InstEdiFile.AddField(10, string.Empty);
                        InstEdiFile.AddField(30, filter.LastName);
                        InstEdiFile.AddField(9, string.Empty);
                        InstEdiFile.AddField(30, filter.ASN);
                        InstEdiFile.AddField(8, string.Empty);  // Start Dates
                        InstEdiFile.AddField(8, string.Empty);  // End Dates
                        InstEdiFile.AddField(4, institution.StartYear);
                        InstEdiFile.AddField(4, institution.EndYear);
                        InstEdiFile.AddField(5, institution.HighestGrade);  // Grade Type
                        InstEdiFile.AddField(6, string.Empty);  // Cred
                        InstEdiFile.AddField(7, string.Empty);  // GPA
                        InstEdiFile.AddField(6, string.Empty);  // Rank PCT
                        InstEdiFile.AddField(57, institution.InstitutionName);

                        // International Institutions
                        if (useOutsideNorthAmerica)
                        {
                            if (string.IsNullOrWhiteSpace(institution.StudentLevel) ||
                                (!string.IsNullOrWhiteSpace(institution.StudentLevel) && Int32.TryParse(institution.StudentLevel, out int studentLevel) &&
                                    studentLevel < (int)StudentLevelCodeType.Postsecondary))
                            {
                                InstEdiFile.AddField(15, HSOutsideNorthAmerica);  // Hidh School Institution
                            }
                            else
                            {
                                InstEdiFile.AddField(15, PSOutsideNorthAmerica);  // Post Secondary Institution
                            }
                        }
                        else
                        {
                            InstEdiFile.AddField(15, institution.LocalOrganizationID);  // Regular process
                        }

                        InstEdiFile.AddField(261, string.Empty);

                        InstEdiFile.AddLine();

                        instCnt++;
                    }
                }

                // create summary edi file
                SummEdiFile.AddField(36, String.Format("HD {0} 0000{1} applications", _ReceivedDateTime.ToString("MM/dd/yy HH:mm"), _ApplicationFilters.Count));
                SummEdiFile.AddLine();
                SummEdiFile.AddField(37, String.Format("MNLBAP{0}_main         {1:00000}", _ReceivedDateTime.ToString("MMddyyHHmmss"), _ApplicationFilters.Count));
                SummEdiFile.AddLine();
                SummEdiFile.AddField(37, String.Format("INLBAP{0}_inst         {1:00000}", _ReceivedDateTime.ToString("MMddyyHHmmss"), instCnt));
                SummEdiFile.AddLine();


                // write all 3 files to coltestap/colprodap
                if (SummEdiFile.SaveToUNC() && MainEdiFile.SaveToUNC() && InstEdiFile.SaveToUNC())
                {
                    success = lcapasLogic.SaveExportedDateTime(_UuidStrList);
                }

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "CreateApplicationEdi", "Error", ex.ToString());
            }
            return success;
        }

        public DateTime ReceivedDateTime
        {
            get { return _ReceivedDateTime; }
            set { _ReceivedDateTime = value; }
        }

        public List<string> UuidStrList
        {
            get { return _UuidStrList; }
            set { _UuidStrList = value; }
        }

        public List<ApplicationObj> ApplicationFilters
        {
            get { return _ApplicationFilters; }
            set { _ApplicationFilters = value; }
        }
    }

    public class ApplicationObj
    {
        #region private properties
        private string _ApplicationID = string.Empty;
        private string _ASN = string.Empty;
        private string _PrevSNumber = string.Empty;
        private string _PreviouslyApplied = string.Empty;
        private string _FirstName = string.Empty;
        private string _MiddleName = string.Empty;
        private string _LastName = string.Empty;
        private string _AddressLine1 = string.Empty;
        private string _AddressLine2 = string.Empty;
        private string _AddressCity = string.Empty;
        private string _FirstNameDesc = string.Empty;
        private string _MiddleNameDesc = string.Empty;
        private string _LastNameDesc = string.Empty;
        private string _AddressLine1Desc = string.Empty;
        private string _AddressLine2Desc = string.Empty;
        private string _AddressCityDesc = string.Empty;
        private string _AddressState = string.Empty;
        private string _AddressPostalCode = string.Empty;
        private string _AddressCountry = string.Empty;
        private string _AddressCountryDesc = string.Empty;
        private string _PermanentPhoneNumber = string.Empty;
        private string _DayPhoneNumber = string.Empty;
        private string _DayPhoneExtensionNumber = string.Empty;
        private string _MobilePhoneNumber = string.Empty;
        private string _PreferredPhoneNumber = string.Empty;
        private string _BirthDateEdi = string.Empty;
        private string _BirthDateView = string.Empty;
        private string _RaceEthnicity = string.Empty;
        private string _RaceEthnicityDesc = string.Empty;
        private string _Gender = string.Empty;
        private string _GenderDesc = string.Empty;
        private string _FamilyAttendedCollege = string.Empty;
        private string _MaritalStatus = string.Empty;
        private string _MaritalStatusDesc = string.Empty;
        private string _Disability = string.Empty;
        private string _Email = string.Empty;
        private string _FirstLearned = string.Empty;
        private string _FirstLearnedDesc = string.Empty;
        private string _Program = string.Empty;
        private string _ProgramLocation = string.Empty;
        private string _StudyLoad = string.Empty;
        private string _StudyLoadDesc = string.Empty;
        private string _AdmitStatus = string.Empty;
        private string _Term = string.Empty;
        private string _CitizenshipCountryCode = string.Empty;
        private string _CitizenshipCountryDesc = string.Empty;
        private string _CitizenshipStatusCode = string.Empty;
        private string _CitizenshipStatusDesc = string.Empty;
        private string _FirstLanguage = string.Empty;
        private string _ResidencyCountryCode = string.Empty;
        private string _ResidencyCountryDesc = string.Empty;
        private string _ResidencyStatusCode = string.Empty;
        private string _ResidencyStatusDesc = string.Empty;
        private string _ResidencyEntryDateEdi = string.Empty;
        private string _ResidencyEntryDateView = string.Empty;
        private string _ImmigrationEntryDateEdi = string.Empty;
        private string _ImmigrationEntryDateView = string.Empty;
        private string _FormerFirstName = string.Empty;
        private string _FormerLastName = string.Empty;
        private string _FormerMiddleName = string.Empty;
        private string _EmergencyContactName = string.Empty;
        private string _EmergencyContactPhone = string.Empty;
        private Double _ApplicationFee = 0.00;
        private string _PayPalCardType = string.Empty;
        private string _ReceivedDateTime;
        private bool _InCanada;
        private string _DateExported;
        private string _DateSubmitted;
        private string _DateCancelled;
        private String _CurrentDate;
        private List<ApplInstObj> _ApplicationInstitutions;
        private List<ApplCrsObj> _ApplicationCourses;
        #endregion private properties

        #region public properties
        public string ApplicationID
        {
            get { return _ApplicationID; }
            set { _ApplicationID = value; }
        }
        public string ASN
        {
            get { return _ASN; }
            set { _ASN = value; }
        }
        public string PreviouslyApplied
        {
            get { return _PreviouslyApplied; }
            set { _PreviouslyApplied = value; }
        }
        public string PrevSNumber
        {
            get { return _PrevSNumber; }
            set { _PrevSNumber = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
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
        public string AddressCity
        {
            get { return _AddressCity; }
            set { _AddressCity = value; }
        }
        public string FirstNameDesc
        {
            get { return _FirstNameDesc; }
            set { _FirstNameDesc = value; }
        }
        public string MiddleNameDesc
        {
            get { return _MiddleNameDesc; }
            set { _MiddleNameDesc = value; }
        }
        public string LastNameDesc
        {
            get { return _LastNameDesc; }
            set { _LastNameDesc = value; }
        }
        public string AddressLine1Desc
        {
            get { return _AddressLine1Desc; }
            set { _AddressLine1Desc = value; }
        }
        public string AddressLine2Desc
        {
            get { return _AddressLine2Desc; }
            set { _AddressLine2Desc = value; }
        }
        public string AddressCityDesc
        {
            get { return _AddressCityDesc; }
            set { _AddressCityDesc = value; }
        }
        public string AddressState
        {
            get { return _AddressState; }
            set { _AddressState = value; }
        }
        public string AddressPostalCode
        {
            get { return _AddressPostalCode; }
            set { _AddressPostalCode = value; }
        }
        public string AddressCountry
        {
            get { return _AddressCountry; }
            set { _AddressCountry = value; }
        }
        public string AddressCountryDesc
        {
            get { return _AddressCountryDesc; }
            set { _AddressCountryDesc = value; }
        }
        public string PermanentPhoneNumber
        {
            get { return _PermanentPhoneNumber; }
            set { _PermanentPhoneNumber = value; }
        }
        public string DayPhoneNumber
        {
            get { return _DayPhoneNumber; }
            set { _DayPhoneNumber = value; }
        }
        public string DayPhoneExtensionNumber
        {
            get { return _DayPhoneExtensionNumber; }
            set { _DayPhoneExtensionNumber = value; }
        }
        public string MobilePhoneNumber
        {
            get { return _MobilePhoneNumber; }
            set { _MobilePhoneNumber = value; }
        }
        public string PreferredPhoneNumber
        {
            get { return _PreferredPhoneNumber; }
            set { _PreferredPhoneNumber = value; }
        }
        public string BirthDateEdi
        {
            get { return _BirthDateEdi; }
            set { _BirthDateEdi = value; }
        }
        public string BirthDateView
        {
            get { return _BirthDateView; }
            set { _BirthDateView = value; }
        }
        public string RaceEthnicity
        {
            get { return _RaceEthnicity; }
            set { _RaceEthnicity = value; }
        }
        public string RaceEthnicityDesc
        {
            get { return _RaceEthnicityDesc; }
            set { _RaceEthnicityDesc = value; }
        }
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        public string GenderDesc
        {
            get { return _GenderDesc; }
            set { _GenderDesc = value; }
        }

        public string FamilyAttendedCollege
        {
            get { return _FamilyAttendedCollege; }
            set { _FamilyAttendedCollege = value; }
        }

        public string MaritalStatus
        {
            get { return _MaritalStatus; }
            set { _MaritalStatus = value; }
        }
        public string MaritalStatusDesc
        {
            get { return _MaritalStatusDesc; }
            set { _MaritalStatusDesc = value; }
        }
        public string Disability
        {
            get { return _Disability; }
            set { _Disability = value; }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public string FirstLearned
        {
            get { return _FirstLearned; }
            set { _FirstLearned = value; }
        }
        public string FirstLearnedDesc
        {
            get { return _FirstLearnedDesc; }
            set { _FirstLearnedDesc = value; }
        }
        public string FirstLanguage
        {
            get { return _FirstLanguage; }
            set { _FirstLanguage = value; }
        }
        public string Program
        {
            get { return _Program; }
            set { _Program = value; }
        }
        public string ProgramLocation
        {
            get { return _ProgramLocation; }
            set { _ProgramLocation = value; }
        }
        public string StudyLoad
        {
            get { return _StudyLoad; }
            set { _StudyLoad = value; }
        }
        public string StudyLoadDesc
        {
            get { return _StudyLoadDesc; }
            set { _StudyLoadDesc = value; }
        }
        public string AdmitStatus
        {
            get { return _AdmitStatus; }
            set { _AdmitStatus = value; }
        }
        public string Term
        {
            get { return _Term; }
            set { _Term = value; }
        }
        public string CitizenshipCountryCode
        {
            get { return _CitizenshipCountryCode; }
            set { _CitizenshipCountryCode = value; }
        }
        public string CitizenshipCountryDesc
        {
            get { return _CitizenshipCountryDesc; }
            set { _CitizenshipCountryDesc = value; }
        }
        public string ResidencyCountryCode
        {
            get { return _ResidencyCountryCode; }
            set { _ResidencyCountryCode = value; }
        }
        public string ResidencyCountryDesc
        {
            get { return _ResidencyCountryDesc; }
            set { _ResidencyCountryDesc = value; }
        }
        public string ResidencyStatusCode
        {
            get { return _ResidencyStatusCode; }
            set { _ResidencyStatusCode = value; }
        }
        public string ResidencyStatusDesc
        {
            get { return _ResidencyStatusDesc; }
            set { _ResidencyStatusDesc = value; }
        }
        public string ResidencyEntryDateEdi
        {
            get { return _ResidencyEntryDateEdi; }
            set { _ResidencyEntryDateEdi = value; }
        }
        public string ResidencyEntryDateView
        {
            get { return _ResidencyEntryDateView; }
            set { _ResidencyEntryDateView = value; }
        }
        public string ImmigrationEntryDateEdi
        {
            get { return _ImmigrationEntryDateEdi; }
            set { _ImmigrationEntryDateEdi = value; }
        }
        public string ImmigrationEntryDateView
        {
            get { return _ImmigrationEntryDateView; }
            set { _ImmigrationEntryDateView = value; }
        }
        public string CitizenshipStatusCode
        {
            get { return _CitizenshipStatusCode; }
            set { _CitizenshipStatusCode = value; }
        }
        public string CitizenshipStatusDesc
        {
            get { return _CitizenshipStatusDesc; }
            set { _CitizenshipStatusDesc = value; }
        }
        public string FormerFirstName
        {
            get { return _FormerFirstName; }
            set { _FormerFirstName = value; }
        }
        public string FormerLastName
        {
            get { return _FormerLastName; }
            set { _FormerLastName = value; }
        }
        public string FormerMiddleName
        {
            get { return _FormerMiddleName; }
            set { _FormerMiddleName = value; }
        }
        public string EmergencyContactName
        {
            get { return _EmergencyContactName; }
            set { _EmergencyContactName = value; }
        }
        public string EmergencyContactPhone
        {
            get { return _EmergencyContactPhone; }
            set { _EmergencyContactPhone = value; }
        }
        public Double ApplicationFee
        {
            get { return _ApplicationFee; }
            set { _ApplicationFee = value; }
        }
        public string PayPalCardType
        {
            get { return _PayPalCardType; }
            set { _PayPalCardType = value; }
        }
        public string ReceivedDateTime
        {
            get { return _ReceivedDateTime; }
            set { _ReceivedDateTime = value; }
        }

        public bool InCanada
        {
            get { return _InCanada; }
            set { _InCanada = value; }
        }

        public string DateExported
        {
            get { return _DateExported; }
            set { _DateExported = value; }
        }

        public string DateSubmitted
        {
            get { return _DateSubmitted; }
            set { _DateSubmitted = value; }
        }

        public string DateCancelled
        {
            get { return _DateCancelled; }
            set { _DateCancelled = value; }
        }

        public String CurrentDate
        {
            get { return _CurrentDate; }
            set { _CurrentDate = value; }
        }
        public List<ApplInstObj> ApplicationInstitutions
        {
            get { return _ApplicationInstitutions; }
            set { _ApplicationInstitutions = value; }
        }
        public List<ApplCrsObj> ApplicationCourses
        {
            get { return _ApplicationCourses; }
            set { _ApplicationCourses = value; }
        }

        #endregion public properties

    }
    #endregion Complex Objects

    #region Search Filters

    public class ApplicationSearchFilter
    {
        private LcapasLogic lcapasLogic = new LcapasLogic();
        private DateTime? _FromDate;
        private DateTime? _ToDate;
        private DateTime? _BirthDate;
        private DateTime? _BatchDate;
        private string _LastName;
        private string _FirstName;
        private string _MiddleName;
        private string _ASN;
        private Enums.Exported _Exported = Enums.Exported.New;
        private bool _Datashare;
        private string _Program;
        private List<string> _Term;
        private double? _Amount;
        private IEnumerable<SelectListItem> _Programs;
        private IEnumerable<SelectListItem> _Terms;
        private GenderCodeType? _Gender;
        private List<string> _CitizenshipStatus;

        public ApplicationSearchFilter()
        {
            if (_Programs == null) { _Programs = lcapasLogic.GetPrograms(); };
            if (_Terms == null) { _Terms = lcapasLogic.GetTermList(); };
        }

        [Display(Name = "From Date")]
        public DateTime? FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        [Display(Name = "To Date")]
        public DateTime? ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }

        [Display(Name = "Birth Date")]
        public DateTime? BirthDate
        {
            get { return _BirthDate; }
            set { _BirthDate = value; }
        }

        [Display(Name = "Batch Date")]
        public DateTime? BatchDate
        {
            get { return _BatchDate; }
            set { _BatchDate = value; }
        }

        [Display(Name = "Last Name")]
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        [Display(Name = "First Name")]
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        [Display(Name = "Middle Name")]
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }

        [Display(Name = "ASN")]
        public string ASN
        {
            get { return _ASN; }
            set { _ASN = value; }
        }

        [Display(Name = "Exported")]
        public Enums.Exported Exported
        {
            get { return _Exported; }
            set { _Exported = value; }
        }

        [Display(Name = "Datashare")]
        public bool Datashare
        {
            get { return _Datashare; }
            set { _Datashare = value; }
        }

        [Display(Name = "Program")]
        public string Program
        {
            get { return _Program; }
            set { _Program = value; }
        }

        [Display(Name = "Programs")]
        public IEnumerable<SelectListItem> Programs { get { return _Programs; } }

        [Display(Name = "Gender")]
        public GenderCodeType? Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        [Display(Name = "Citizenship Status")]
        public List<string> CitizenshipStatus
        {
            get { return _CitizenshipStatus; }
            set { _CitizenshipStatus = value; }
        }

        [Display(Name = "Term")]
        public List<string> Term
        {
            get { return _Term; }
            set { _Term = value; }
        }

        [Display(Name = "Terms")]
        public IEnumerable<SelectListItem> Terms { get { return _Terms; } }


        [Display(Name = "Amount")]
        [DisplayFormat(DataFormatString = "{0:#,##0.00#}")]
        public double? Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

    }

    public class ApplicationSearchResultsFilter
    {
        //private string _Id;
        private string _Uuid;
        private string _StuId;
        private string _ASN;
        private string _RefNumber;
        private string _AltID;
        private DateTime? _ExportedDateTime;
        private DateTime? _SubmittedDateTime;
        private DateTime? _CancelledDateTime;
        private string _LastName;
        private string _FirstName;
        private string _MiddleName;
        private string _Email;
        private string _CitizenshipStatus;
        private GenderCodeType _Gender;
        private string _Program;
        private string _ProgramCode;
        private string _Term;
        private string _ApplStatus;
        private DateTime? _CreatedDateTime;
        private int _Count;
        private double _Amount;


        public string Uuid
        {
            get { return _Uuid; }
            set { _Uuid = value; }
        }

        public string StuId
        {
            get { return _StuId; }
            set { _StuId = value; }
        }

        public string ASN
        {
            get { return _ASN; }
            set { _ASN = value; }
        }

        public string RefNumber
        {
            get { return _RefNumber; }
            set { _RefNumber = value; }
        }

        public string AltID
        {
            get { return _AltID; }
            set { _AltID = value; }
        }

        public DateTime? ExportedDateTime
        {
            get { return _ExportedDateTime; }
            set { _ExportedDateTime = value; }
        }

        public DateTime? SubmittedDateTime
        {
            get { return _SubmittedDateTime; }
            set { _SubmittedDateTime = value; }
        }

        public DateTime? CancelledDateTime
        {
            get { return _CancelledDateTime; }
            set { _CancelledDateTime = value; }
        }

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string CitizenshipStatus
        {
            get { return _CitizenshipStatus; }
            set { _CitizenshipStatus = value; }
        }

        public GenderCodeType Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        public string Program
        {
            get { return _Program; }
            set { _Program = value; }
        }


        public string ProgramCode
        {
            get { return _ProgramCode; }
            set { _ProgramCode = value; }
        }

        public string Term
        {
            get { return _Term; }
            set { _Term = value; }
        }

        public string ApplStatus
        {
            get { return _ApplStatus; }
            set { _ApplStatus = value; }
        }

        public DateTime? CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }

        public int Count
        {
            get { return _Count; }
            set { _Count = value; }
        }

        [DisplayFormat(DataFormatString = "{0:#,##0.00#}", ApplyFormatInEditMode = true)]
        public double Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

    }

    #endregion Search Filters

}