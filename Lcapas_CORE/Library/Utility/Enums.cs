using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lcapas.Core.Library
{
    public static class EnumExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetAttribute<DisplayAttribute>().GetName();
        }

        public static string GetDataType(this Enum enumValue)
        {
            return enumValue.GetAttribute<DataTypeAttribute>().GetDataTypeName();
        }
    }

    public class Enums
    {
        public enum ResponseType
        {
            Transcript = 0,
        }

        public enum AcadLevels
        {
            PS = 0,
            NCR = 1,
        }

        public enum EdiConvType
        {
            String = 0,
            DateTime = 1,
            Double = 2,
            Enum = 3,
        }

        public enum PayPalCardType {
            PAYMTVS = 0,    // Visa
            PAYMTMCRD = 1,  // MasterCard
            PAYMTDIS = 2,   // Discover
            PAYMTAMEX = 3,  // American Express
            PAYMTDICL = 4,  // Diner's Club
            PAYMTJCB = 5,    // JCB
            PAYMTPPL = 6,    // PayPal
        }

        public enum PaymentMethodType {
            P = 0, // PayPal
            CC = 1, // CreditCard
        }

        public enum AccessGroupType
        {
            Admin = 1,
            SuperUser = 2,
            AdmissionsAdmin = 3,
            AdmissionsStaff = 4,
            RecordsAdmin = 5,
            RecordsStaff = 6,
            Awards = 7,
            Finance = 8,
            AdmissionInbox = 9,
            AdmissionsReports = 10,
            RecordsReports = 11,
            AdministratorReports = 12
        }

        public enum PerspectiveType
        {
            Admissions = 0,
            Records = 1
        }

        public enum ControllerTypeType
        {
            Login = 1,
            Home = 2,
            Applications = 3,
            Transcripts = 4,
            DataShare = 5,
            Settings = 6,
            ApplicationSettings = 7,
            TranscriptSettings  = 8,
            Error = 9,
            Landing = 10,
            Users = 11,
            Reports = 12,
            TransferCredits = 13,
            MyCreds = 14,
        }

        public enum ActionTypeType {

            //////////////////////////////// Annonymous / Generic
            Index = 1,
            Dispose = 2,
            LogOff  = 3,
            //////////////////////////////// ApplicationsController
            Applications = 4,
            // application reports
            PrintApplicationReport = 5,
            //PrepareExcelReport = 7,
            //ExportWordReport = 8,
            //DownloadExcelReport = 9,
            ExportApplications = 10,
            /////////////////////////////// ApplicationSettingsController
            // application programs
            Program = 12,
            ProgramCreate = 13,
            ProgramEdit = 14,
            ProgramDelete = 15,
            ProgramOrder = 16,
            ProgramActivate = 17,
            ProgramPending = 18,
            CreateProgramTerm = 292,
            PrintCreateProgramTerm = 293,
            DeactivateProgramTerm = 294,
            PrintDeactivateProgramTerm = 295,
            // application campuses
            Campus = 20,
            CampusCreate = 21,
            CampusEdit = 22,
            CampusDelete = 23,
            CampusOrder = 24,
            CampusActivate = 25,
            // application terms
            Term = 30,
            TermCreate = 31,
            TermEdit = 32,
            TermDelete = 33,
            TermOrder = 34,
            TermActivate = 35,
            // application programdetails
            ProgramDetail = 40,
            GetProgramDetails = 41,
            SaveProgramDetails = 42,
            DeleteProgramDetails = 43,
            ProgramDetailOrder = 44,
            FilterProgramDetails = 45,
            ProgramDetailActivate = 46,
            SaveProgramDetailsForProgramTerm = 166,

            // application referencetypes
            ReferenceType = 50,
            ReferenceTypeCreate = 51,
            ReferenceTypeEdit = 52,
            ReferenceTypeDelete = 53,
            ReferenceTypeOrder = 54,

            // Application Fee
            ApplicationFee = 56,
            ApplicationFeeCreate = 57,
            ApplicationFeeDelete = 58,

            // application settings
            Settings = 60,
            Parameters = 61,
            SettingEdit = 62,
            SettingOrder = 63,
            SettingCategoryOrder = 64,        
            //////////////////////////////// TRANSCRIPTS
            AdmissionsInbox  = 70,
            AdmissionsOutbox = 71,
            RecordsInbox = 72,
            RecordsOutbox = 73,
            PrintRequestResponseReport = 74,
            LookupRedirect = 75,
            StudentLookup = 76,
            SendRequest = 77,
            SubmitRequest = 78,
            GetStudent = 79,
            RequestTranscript = 80,
            SendUnsolicitedTranscript = 81,
            CheckTransactionTranscriptComplete = 82,
            DisplayMessage = 83,
            CreateMessage = 84,
            DisplayIframeDocument = 85,
            DisplayErrorMessages = 86,
            MarkAsNotViewed = 87,
            SendtoColleagueTRRQ = 88,
            ExportTranscriptColleague = 89,
            ParseResponse = 104,
            UnsolicitedBatchTranscript = 108,
            SendUnsolicitedBatchTranscripts = 109,

            //////////////////////////////// TRANSCRIPTSETTINGS
            SynchronizeMessages = 90,
            MessageStatus = 91,
            RefreshInstitutions = 92,
            SystemPreferences = 93,
            ContactInformation = 94,
            ConfigureEmail = 95,
            DefaultStylesheets = 96,
            EnabledFunctionality = 97,
            NotificationSettings = 98,
            SecurityLog = 99,
            OperationsLog = 100,
            ToolkitUsers = 101,
            /////////////////////////////// UsersController
            // users
            User = 212,
            UserCreate = 213,
            UserEdit = 214,
            UserDelete = 215,
            UserOrder = 216,
            UserActivate = 217,
            UserAccessGroup = 221,
            SetUsersGroups = 226,

            /////////////////////////////// ReportsController
            // reports
            DailyRequest = 218,
            ReportCreate = 219,
            LoginHistory = 220,
            ExceptionError = 222,
            PrintException = 223,
            PrintHistory = 224,
            PrintReports = 225,
            ApplicationReport = 260,
            PaymentReport = 261,
            PrintApplicationReportReport = 262,
            PrintPaymentReport = 263,
            HoldTypeReport = 264,
            SentEmailReport = 290,
            PrintSentEmailReport = 291,
            CollProgramApplicationReport = 270,
            CollWaitListReport = 271,
            PrintCollProgramApplicationReport = 272,
            PrintCollWaitListReport = 273,
            CollOverduesReport = 300,
            CollTestingResultsReport = 301,
            CollAdmissionConditionsReport = 302,
            CollMissingGradeReport = 305,
            ExportReports = 303,
            PrintQuery = 304,

            //////////////////////////////// SETTINGS
            SystemSettings = 102,
            ServerStatus = 103,
            Permissions = 105,
            CreatePermissionType = 106,
            DeletePermissionType = 107,

            //////////////////////////////// Transfer Credit
            GenerateTransferCreditXML = 400,
            XMLReport = 401,

            //////////////////////////////// MyCreds
            GenerateTranscriptBatch = 500,
            ExportTranscriptBatch = 501,
            BulkSend = 502,
            ExportBulkSendBatch = 503,

            //////////////////////////////// DataShare
            DataShare = 110,

            //////////////////////////////// LOGIN


            //////////////////////////////// ERROR
            Unauthorised = 200,

        }

        public enum PermissionSaveType
        {
            ControllerType = 0,
            ActionType = 1,
            PermissionRecord = 2,
        }

        public enum Exported {
            New = 0,
            Exported = 1,
            Stopped = 2,
            All = 3
        }

        public enum TransListTypes
        {
            New = 0,
            Exported = 1,
            All = 2,
        }

        public enum RespTransTypes
        {
            All = 0,
            Resp = 1,
            Trans = 2,
        }

        public enum JobTypeType
        {
            SendUnsentApps = 0,
            ParseSentTransferRequest = 1,
            ParseReceivedTransferRequest = 2,
            ParseSentApasResponse = 3,
            ParseReceivedApasResponse = 4,
            SendApasTransferRequest = 5,
            SendApasTransferResponse = 6,
            GetColleagueTranscript = 7,
            SaveColleagueRequest = 8,
            SaveColleagueExtCourse = 9,
            SendAutoResponse = 10,
            SendBulkTranscript = 11,
            LookupSNumberByASN = 12,
            CompletePaidUnsetApplication = 13,
            QueueMyCredsTRRQRequestResponses = 14,
        }

        public enum MessageTypes
        {
            Application = 0,
            ReceivedRequest = 2,
            SentRequest = 3,
            ReceivedResponse = 4,
            SentResponse = 5,
            StatisticalData = 6,
        }

        public enum MyCredsDocumentTypes
        {
            [Display(Name = "Transcript")]
            Transcript = 0,
            [Display(Name = "Confirmation of Graduation")]
            ConfirmationGraduation = 1,
            [Display(Name = "Confirmation of Enrolment")]
            ConfirmationEnrolment = 2,
            [Display(Name = "Parchment")]
            Parchment = 3,
            [Display(Name = "International Confirmation of Graduation")]
            ConfirmationGraduationInternational = 4,
            [Display(Name = "Transcript Pre 1995")]
            TranscriptPre1995 = 5,
        }

        /// <summary>
        /// <para>Passing these options into the TransformXml class constructor will allow each of the operations that are passed.</para>
        /// <para>(usage: XsltAllowed.DtdProcessing | XsltAllowed.ExternalResources  will allow dtd processing as well as the downloading of external documents if embedded in the stylesheet)</para>
        /// </summary>
        [Flags]
        public enum XsltAllow
        {
            /// <summary>
            /// Use default settings 
            /// (Strict with no dtdprocessing, external resources, scripts. External resources disabled and characters are checked)
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Allow DtdProcessing if one is present
            /// </summary>
            DtdProcessing = 0x1,
            /// <summary>
            /// Automatically resolve and download - apply external resources 
            /// (i.e. Xslt with embedded Xslt includes)
            /// </summary>
            ExternalResources = 0x2,
            /// <summary>
            /// Allow the Document() function
            /// </summary>
            DocumentFunction = 0x4,
            /// <summary>
            /// Enable embedded script tags
            /// </summary>
            EmbeddedScripts = 0x8
        }

        public enum DeptAction {
            AdmissionsInbox = 0,
            RecordsOutbox = 1,
            AdmissionsOutbox = 2,
            RecordsInbox = 3,
            CreateResponse = 4,
            CreateRequest = 5,
            CreateTranscript = 6,
        }

        public enum PhoneTypes {
            InstitutionPhoneType = 0,
            PermanentPhoneType = 1,
            DayPhoneType = 2,
            MobilePhoneType = 3,
            PreferredPhoneType = 4,
            FaxPhoneType = 5,
        }

        public enum FilterStudentLookupTypes
        {
            sNumber = 0,
            ASN = 1,
            FirstName = 2,
            MiddleName = 3,
            LastName = 4,
            BirthDate = 5
        }

        public enum CitizenshipStatus
        {
            [Display(Name = "Citizen")]
            Citizen = 1,
            [Display(Name = "Other")]
            NonCitizen = 6,
            [Display(Name = "Permanent Resident")]
            PermanentVisa = 11,
            [Display(Name = "Refugee")]
            Refugee = 12,
            [Display(Name = "Student Visa")]
            StudentAuthorization = 14,
            [Display(Name = "Work Visa")]
            WorkVisa = 19,
        }
    }
}
