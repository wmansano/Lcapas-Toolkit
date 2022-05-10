namespace Lcapas.Core.Library
{
    public class Structs
    {
        public struct DocumentType {
            public const string NotSet = "Not Set";
            public const string CollegeTranscript = "CollegeTranscript";
            public const string HighSchoolTranscript = "HighSchoolTranscript";
            public const string Response = "Response";
            public const string Request = "Request";
            public const string LoginHistory = "LoginHistory";
            public const string ExceptionError = "ExceptionError";
            public const string StudentDailyRequest = "StudentDailyRequest";
            public const string ApplicationReport = "ApplicationReport";
            public const string PaymentReport = "PaymentReport";
            public const string HoldTypeReport = "HoldTypeReport";
            public const string CollProgramApplicationReport = "CollProgramApplicationReport";
            public const string CollWaitListReport = "CollWaitListReport";
            public const string SentEmailReport = "SentEmailReport";
            public const string CollTestingResultsReport = "CollTestingResultsReport";
            public const string CollAdmissionConditionsReport = "CollAdmissionConditionsReport";
            public const string CollOverduesReport = "CollOverduesReport";
            public const string CollWaitListQuery = "WaitList";
            public const string CollAdmissionQuery = "Admission";
            public const string CollMissingGradeReport = "CollMissongGradeReport";
            public const string CollTestingQuery = "Testing";
            public const string CollOverduesQuery = "Overdues";
            public const string CollApplicationQuery = "Application";
            public const string CollMissingGradeQuery = "MissingGrades";
            public const string TranserCreditReport = "TranserCreditReport";
            public const string UnsolicitedBatchTranscriptReport = "UnsolicitedBatchTranscriptReport";
            public const string MyCredsTransReport = "MyCredsTransReport";
            public const string MyCredsBulkSendReport = "MyCredsBulkSendReport";
        }
        public struct XmlDocType
        {
            public const string NotSet = "Not Set";
            public const string CollegeTranscript = "CollegeTranscript";
            public const string HighSchoolTranscript = "HighSchoolTranscript";
            public const string Response = "Response";
            public const string Request = "Request";
        }

        public struct Boolean {
            public const string True = "true";
            public const string False = "false";
        }

        public struct Terms {
            public const string WN = "WN";
            public const string SM = "SM";
            public const string S1 = "S1";
            public const string S2 = "S2";
            public const string FL = "FL";
        }

        public struct Literals {
            public const string UUID = "uuid";
            public const string ApplicationID = "ApplicationID";
            public const string APASDataSetXML = "APASDataSetXML";
            public const string SessionId = "SessionId";
            public const string SecurityToken = "SecurityToken";
            public const string Success = "Success";
            public const string SecureToken = "SECURETOKEN";
            public const string ContactHelpDesk = "Please contact the helpdesk.";
            public const string GeneratedByApasToolkit = "APAS - Toolkit";
            public const string GeneratedByMyCredsToolkit = "MyCreds - Toolkit";
            public const string Duplicate = "DUPLICATE";
            public const string DocumentNotRendered = "This document could not be rendered. Please contact the helpdesk.";
            public const string NoDocumentFound = "No document found!";
        }

        public struct Environment {

            public const string Prod = "prod";
            public const string Test = "test";
            public const string ROTest = "rotest";
            public const string Patch = "patch";
            public const string Dev = "dev";
            public const string Localhost = "localhost";
        }

        public struct ApasWebService {
            public const string distsesssvc = "LcapasCore_distsesssvc_DistributedSessionService";
            public const string instsvc = "LcapasCore_instsvc_InstitutionInformationAndCodesServices";
            public const string quesvc = "LcapasCore_quesvc_APASQueueService";
            public const string quesvcext2 = "LcapasCore_quesvcext2_APASQueueServiceExt2";
            public const string reftokensvc = "LcapasCore_reftokensvc_RefreshTokenServices";
        }

        public struct EmailType {
            public const string ApplicationPaymentConfirm = "ApplicationPaymentConfirm";
            public const string ApplicationPaymentError = "ApplicationPaymentError";
            public const string ApplicationDoublePaymentError = "ApplicationDoublePaymentError";
            public const string ApplicationCancelPayment = "ApplicationCancelPayment";
            public const string TechErrorEmail = "TechErrorEmail";
        }

        public struct EmailSettings
        {
            public const string SmtpServer = "SmtpServer";
            public const string SmtpPort = "SmtpPort";
            public const string SmtpUserName = "SmtpUserName";
            public const string SmtpPassword = "SmtpPassword";
            public const string SmtpFromEmail = "SmtpFromEmail";
            public const string SmtpCredentials = "SmtpCredentials";
            public const string SmtpSSL = "SmtpSSL";
            public const string StuPayErrMsgTtl = "StuPayErrMsgTtl";
            public const string StuPayErrMsgSubTtl = "StuPayErrMsgSubTtl";
            public const string StuPayErrMsgBody = "StuPayErrMsgBody";
            public const string StuDblPayErrMsgTtl = "StuDblPayErrMsgTtl";
            public const string StuDblPayErrMsgSubTtl = "StuDblPayErrMsgSubTtl";
            public const string StuDblPayErrMsgBody = "StuDblPayErrMsgBody";
            public const string TechEmailAddr = "TechEmailAddr";
            public const string TechPayErrMsgTtl = "TechPayErrMsgTtl";
            public const string TechPayErrMsgSubTtl = "TechPayErrMsgSubTtl";
            public const string TechPayErrMsgBody = "TechPayErrMsgBody";
            public const string TechDblPayErrMsgTtl = "TechDblPayErrMsgTtl";
            public const string TechDblPayErrMsgSubTtl = "TechDblPayErrMsgSubTtl";
            public const string TechDblPayErrMsgBody = "TechDblPayErrMsgBody";
            public const string TechErrMsgTtl = "TechErrMsgTtl";
            public const string TechErrMsgSubTtl = "TechErrMsgSubTtl";
            public const string TechErrMsgBody = "TechErrMsgBody";
            public const string AdmEmailAddr = "AdmEmailAddr";
            public const string AdmManagerEmailAddr = "AdmManagerEmailAddr";
            public const string AdmManagerMissingGradeEmailAddr = "AdmManagerMissingGradeEmailAddr";
            public const string AdmPayErrMsgTtl = "AdmPayErrMsgTtl";
            public const string AdmPayErrMsgSubTtl = "AdmPayErrMsgSubTtl";
            public const string AdmPayErrMsgBody = "AdmPayErrMsgBody";
            public const string AdmDblPayErrMsgTtl = "AdmDblPayErrMsgTtl";
            public const string AdmDblPayErrMsgSubTtl = "AdmDblPayErrMsgSubTtl";
            public const string AdmDblPayErrMsgBody = "AdmDblPayErrMsgBody";
            public const string PayCompMsgTtl = "PayCompMsgTtl";
            public const string PayCompMsgSubTtl = "PayCompMsgSubTtl";
            public const string StuPayCompMsgBody = "StuPayCompMsgBody";
            public const string StuPayCompMsgBodyNoTranscript = "StuPayCompMsgBodyNoTranscript";
            public const string StuSendEmailEnabled = "StuSendEmailEnabled";
            public const string AdmSendEmailEnabled = "AdmSendEmailEnabled";
            public const string TechSendEmailEnabled = "TechSendEmailEnabled";
            public const string StuCancelEmailEnabled = "StuCancelEmailEnabled";
            public const string StuCancelMsgTitle = "StuCancelMsgTitle";
            public const string StuCancelMsgSubtitle = "StuCancelMsgSubtitle";
            public const string StuCancelMsgBody = "StuCancelMsgBody";
            public const string MissingGradeSendEmailEnabled = "MissingGradeSendEmailEnabled";
            public const string AdmMissingGradeSendEmailEnabled = "AdmMissingGradeSendEmailEnabled";
        }

        public struct EmailReceiverGroup
        {
            public const string Student = "Student";
            public const string Admissions = "Admissions";
            public const string Technical = "Technical";
        }

        public struct Settings {
            public const string LocalAPASCode = "LocalApasCode";
            public const string PescApasCode = "PescApasCode";
            public const string Production = "Production";
            public const string ApasSuppPath = "ApasSuppPath";
            public const string ApasProdPath = "ApasProdPath";
            public const string AdminWriteGifPath = "AdminWriteGifPath";
            public const string ApplicantWriteGifPath = "ApplicantWriteGifPath";
            public const string ApasRestoreLoginPath = "ApasRestoreLoginPath";
            public const string ApasRestoreAdminLoginPath = "ApasRestoreAdminLoginPath";
            public const string ApasLogoutPath = "ApasLogoutPath";
            public const string ApasRedirectPath = "ApasRedirectPath";
            public const string PayPalProdAPIUser = "PayPalProdAPIUser";
            public const string PayPalProdAPIPassword = "PayPalProdAPIPassword";
            public const string PayPalProdAPIVendor = "PayPalProdAPIVendor";
            public const string PayPalProdAPIPartner = "PayPalProdAPIPartner";
            public const string PayPalDevAPIUser = "PayPalDevAPIUser";
            public const string PayPalDevAPIPassword = "PayPalDevAPIPassword";
            public const string PayPalDevAPIVendor = "PayPalDevAPIVendor";
            public const string PayPalDevAPIPartner = "PayPalDevAPIPartner";
            public const string PayPalProdHostPath = "PayPalProdHostPath";
            public const string PayPalProdEndPointPath = "PayPalProdEndPointPath";
            public const string PayPalDevHostPath = "PayPalDevHostPath";
            public const string PayPalDevEndPointPath = "PayPalDevEndPointPath";
            public const string PayPalEnvironment = "PayPalEnvironment";
            public const string PayPalBNCode = "PayPalBNCode";
            public const string PayPalTimeOut = "PayPalTimeOut";
            public const string PayPalTender = "PayPalTender";
            public const string PayPalAction = "PayPalAction";
            public const string PayPalTRXType = "PayPalTRXType";
            public const string PayPalErrorPath = "PayPalErrorPath";
            public const string PayPalReturnPath = "PayPalReturnPath";
            public const string PayPalCancelPath = "PayPalCancelPath";
            public const string PayPalCreateSecureToken = "PayPalCreateSecureToken";
            public const string collUNCProdPath = "collUNCProdPath";
            public const string collUNCTestPath = "collUNCTestPath";
            public const string collUNCROTestPath = "collUNCROTestPath";
            public const string collUNCPatchPath = "collUNCPatchPath";
            public const string collUNCDevPath = "collUNCTestPath";
            public const string collUNCProdDirectory = "collUNCProdDirectory";
            public const string collUNCTestDirectory = "collUNCTestDirectory";
            public const string collUNCROTestDirectory = "collUNCROTestDirectory";
            public const string collUNCPatchDirectory = "collUNCPatchDirectory";
            public const string collUNCDevDirectory = "collUNCDevDirectory";
            public const string collUNCUserName = "collUNCUserName";
            public const string collUNCPassword = "collUNCPassword";
            public const string LcDomain = "LcDomain";
            public const string LcApplicationUrl = "LcApplicationUrl";
            public const string TestUserActive = "TestUserActive";
            public const string TestUserName = "TestUserName";
            public const string TestPaypalAmount = "TestPaypalAmount";
            public const string PayFlowProdLink = "PayFlowProdLink";
            public const string PayFlowTestLink = "PayFlowTestLink";
            public const string QueueSvcPath = "QueueSvcPath";
            public const string QueueSvc2Path = "QueueSvc2Path";
            public const string DistSessSvcPath = "DistSessSvcPath";
            public const string RefTokSvcPath = "RefTokSvcPath";
            public const string InstInfoPath = "InstInfoPath";
            public const string ApasProxyProdPath = "ApasProxyProdPath";
            public const string ApasProxyProdSuppPath = "ApasProxyProdSuppPath";

            public const string CheckTranscriptsFrequency = "CheckTranscriptsFrequency";
            public const string CheckRequestsFrequency = "CheckRequestsFrequency";
            public const string AutoRespond = "AutoRespond";

            public const string CreateRequestTitle = "CreateRequestTitle";
            public const string CreateTranscriptTitle = "CreateTranscriptTitle";
            public const string CreateResponseTitle = "CreateResponseTitle";

            public const string AutoLogin = "AutoLogin";
            public const string LcIntegrationUserName = "LcIntegrationUserName";
            public const string LcIntegrationPassword = "LcIntegrationPassword";

            // Prod Dmi Settings
            public const string DmiProdAccountName = "DmiProdAccountName";
            public const string DmiProdIpAddress = "DmiProdIpAddress";
            public const string DmiProdPort = "DmiProdPort";
            public const string DmiProdSecure = "DmiProdSecure";
            public const string DmiProdSharedSecret = "DmiProdSharedSecret";

            // Test Dmi Settings
            public const string DmiTestAccountName = "DmiTestAccountName";
            public const string DmiTestIpAddress = "DmiTestIpAddress";
            public const string DmiTestPort = "DmiTestPort";
            public const string DmiTestSecure = "DmiTestSecure";
            public const string DmiTestSharedSecret = "DmiTestSharedSecret";

            // ROTest Dmi Settings
            public const string DmiROTestAccountName = "DmiROTestAccountName";
            public const string DmiROTestIpAddress = "DmiROTestIpAddress";
            public const string DmiROTestPort = "DmiROTestPort";
            public const string DmiROTestSecure = "DmiROTestSecure";
            public const string DmiROTestSharedSecret = "DmiROTestSharedSecret";

            // Patch Dmi Settings
            public const string DmiPatchAccountName = "DmiPatchAccountName";
            public const string DmiPatchIpAddress = "DmiPatchIpAddress";
            public const string DmiPatchPort = "DmiPatchPort";
            public const string DmiPatchSecure = "DmiPatchSecure";
            public const string DmiPatchSharedSecret = "DmiPatchSharedSecret";

            // Dev Dmi Settings
            public const string DmiDevAccountName = "DmiDevAccountName";
            public const string DmiDevIpAddress = "DmiDevIpAddress";
            public const string DmiDevPort = "DmiDevPort";
            public const string DmiDevSecure = "DmiDevSecure";
            public const string DmiDevSharedSecret = "DmiDevSharedSecret";

            // Job Scheduler
            public const string JobsMaxPerExecution = "JobsMaxPerExecution";
            public const string JobsMaxSizeListSNumberLookup = "JobsMaxSizeListSNumberLookup";

            // Stylesheet
            public const string StylesheetRequestTimeout = "StylesheetRequestTimeout";

            // EDI Institution Code
            public const string UseOutsideNorthAmerica = "UseOutsideNorthAmerica";
            public const string HSOutsideNorthAmerica = "HSOutsideNorthAmerica";
            public const string PSOutsideNorthAmerica = "PSOutsideNorthAmerica";

            // Pending Geovernment Approval
            public const string PendingGovernmentApproval = "(Pending Government Approval)";

            // Response and Request Max Load
            public const string ResponseMaxLoad = "ResponseMaxLoad";
            public const string RequestMaxLoad = "RequestMaxLoad";

            // MyCreds Settings
            public const string collUNCMyCredsProdDirectory = "collUNCMyCredsProdDirectory";
            public const string collUNCMyCredsTestDirectory = "collUNCMyCredsTestDirectory";
            public const string collUNCMyCredsDevDirectory = "collUNCMyCredsDevDirectory";
            public const string collUNCMyCredsPatchDirectory = "collUNCMyCredsPatchDirectory";
            public const string collUNCMyCredsROTestDirectory = "collUNCMyCredsROTestDirectory";

            public const string MyCredsApiUserName = "MyCredsApiUserName";
            public const string MyCredsApiPassword = "MyCredsApiPassword";
            public const string MyCredsApiHost = "MyCredsApiHost";
            public const string MyCredsProdApiDomainUrl = "MyCredsProdApiDomainUrl";
            public const string MyCredsTestApiDomainUrl = "MyCredsTestApiDomainUrl";
            public const string MyCredsApiEndPointUrl = "MyCredsApiEndPointUrl";

            public const string MyCredsProcessLastMonthRequests = "MyCredsProcessLastMonthRequests";

            public const string MyCredsAutoRespond = "MyCredsAutoRespond";

        }

        public struct PaypalErrors
        {
            public const string UserAuthenticationFailed = "1";  // User authentication failed
            public const string TimeOut = "11";  // Client time-out waiting for response
            public const string Declined = "12";
            public const string Referral = "13";
            public const string InvalidAccount = "23";  // Invalid account number
            public const string InvalidExpirationDate = "24";  // Invalid expiration date
            public const string InvalidHostMapping = "25";  // Invalid Host Mapping. You are trying to process a tender type such as Discover Card, but you are not set up with your merchant bank to accept this card type.
            public const string DuplicateTransaction = "30";  // Duplicate Transaction
            public const string InvalidTransaction = "100";  // Invalid transaction returned from Host
            public const string SecurityCodeMismatch = "114";  // Card Security Code mismatch
            public const string SecureTokenAlreadyBeenUsed = "160";  // Secure Token already been used
            public const string SecureTokenExpired = "162";  // Secure Token Expired
        }

        public struct PaypalErrorMessages
        {
            public const string UserAuthenticationFailedMessage = "User authentication failed. Please return to paypal to try login again or try using a credit card to make your payment.";  // User authentication failed
            public const string TimeOutMessage = "Time-out waiting for response. Please return to paypal to make your payment.";  // Client time-out waiting for response
            public const string DeclinedMessage = "Please check the credit card number, expiration date, and transaction information to make sure they were entered correctly. " +
                                                  "If this does not resolve the problem, please contact your bank.";
            public const string ReferralMessage = "Transaction cannot be approved electronically. "+
                                                  "Please contact your bank.";
            public const string InvalidAccountExpirationDateMessage = "Please return to paypal and check credit card number, expiration date, and transaction information to make sure they were entered correctly and re-submit.";  // Invalid account number or expiration date
            public const string DuplicateTransactionMessage = "Payment for this application has already been received. You were not charged again.";  // Duplicate Transaction
            public const string InvalidTransactionMessage = "Transaction type not supported by our online payment provider. " +
                                                            "Visa and Mastercard debit cards are not accepted by our payment provider. " +
                                                            "Please return to paypal to make your payment with an accepted credit card (Visa, Mastercard).";  // Invalid transaction returned from Host
            public const string SecureTokenExpiredMessage = "Your session has expired! <br /> Please return to Apply Alberta to resume your Lethbridge College application: <p><a class='button' href='https://applyalberta.ca/'>Click here to return to Apply Alberta</a></p>";
            public const string SecurityCodeMismatchMessage = "Please return to paypal and check credit card number, expiration date, security code number (CVV) and transaction information to make sure they were entered correctly and re-submit.";  // Card Security Code mismatch
            public const string UnspecifiedMessage = "Unspecified error returned from Paypal.";
        }

        public struct ErrorMessages
        {
            public const string SessionManagerNotReady = "Session Manager is NOT ready - Session Expired!";
            public const string SetExpressCheckoutFail = "Setting Express Checkout Fails!";
            public const string CancelPaymentError = "Error occurred when canceling payment (Return to merchant website - paypal return link)!";
            public const string LandingException = "Landing Controller Exception!";
            public const string AgreementException = "Agreement Controller Exception!";
            public const string PaymentException = "Payment Controller Exception!";
            public const string MakePaymentException = "Make Payment Controller Exception!";
            public const string FilterProgramDetailsException = "Filter Program Details Controller Exception!";
            public const string StopApplicationFail = "Stop Application Fails!";
            public const string NoSNumberFound = "NoSNumberFound";
            public const string EmptyUnsolicitedTranscriptBatchSelection = "Please select at least one Student to queued the Unsolicited Transcript Batch!";
            public const string AlreadyQueuedUnsolicitedTranscriptBatch = "The Unsolicited Transcript Batch for the selected Students and Destination Institution was already queued!";
        }

        public struct ProcStatusFields {
            public const string SessionId = "SessionId";
            public const string UUID = "UUID";
            public const string SecurityToken = "SecurityToken";
            public const string Valid = "Valid";
            public const string Production = "Production";
            public const string Debugging = "Debugging";
            public const string Administrator = "Administrator";
            public const string Username = "Username";
            public const string Received = "Received";
            public const string Imported = "Imported";
            public const string Submitted = "Submitted";
            public const string Paid = "Paid";
            public const string Exported = "Exported";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedBy = "CreatedBy";
            public const string ModifiedDateTime = "ModifiedDateTime";
            public const string ModifiedBy = "ModifiedBy";
        }

        public struct Name
        {
            public const string PersonalNameType = "Personal";
            public const string EmergencyContactNameType = "Emergency";
            public const string FormerType = "Former";
        }

        public struct Phone
        {
            public const string PhoneType = "Phone";
            public const string MobilePhoneType = "Mobile";
            public const string FaxPhoneType = "Fax";
        }

        public struct Email
        {
            public const string PrimaryEmailType = "PrimaryEmail";
            public const string InstitutionEmailType = "InstitutionEmail";
            public const string LethbridgeCollegeEmailType = "lethbridgecollege";
        }

        public struct Address
        {
            public const string PermanentAddressType = "PermanentAddress";
            public const string CurrentAddressType = "CurrentAddress";
            public const string InstitutionType = "InstitutionAddress";
        }

        public struct Institution
        {
            public const string InstitutionSourceDestinationType = "SourceDestination";
            public const string DidNotCompleteLocalOrganizationIDCode = "DIDNOTCOMP";
            public const string DidNotCompleteOrganizationName = "DID NOT COMPLETE HIGH SCHOOL";
            public const string AlbertaEducation = "Alberta Education";
            public const string LethbridgeCollegeID = "0000011";
        }

        public struct AcademicRecord
        {
            public const string SourceType = "ApasApplication";
        }

        public struct WordReport
        {
            public const string SessionWord = "SessionWordReport";
            public const string ContentType = "application/x-msword";
            public const string ContentHeader = "content-disposition";
            public const string MapPath = "~/Stylesheets/Reports/admappdoc.xslt";
        }

        public struct ExcelReport
        {
            public const string SessionExcel = "SessionExcelReport";
            public const string ContentType = "application/ms-excel";
            public const string ContentHeader = "content-disposition";
            public const string MapPath = "~/Stylesheets/Reports/admapplist.xslt";
        }

        public struct StyleSheetPaths
        {
            // APAS
            public const string ApasApplication = "/Stylesheets/APAS/application.xsl";
            public const string ApasTranscriptPS = "/Stylesheets/APAS/20120514TranscriptPS.xsl";
            public const string ApasTranscriptHS = "/Stylesheets/APAS/20120514TranscriptHS.xsl";
            public const string ApasTranscriptRequest = "/Stylesheets/APAS/20120514TranscriptRequest.xsl";
            public const string ApasTranscriptResponse = "/Stylesheets/APAS/20120514TranscriptResponse.xsl";
            
            // MyCreds
            public const string MyCredsTranscript = "/Stylesheets/MyCreds/20211117Transcript.xsl";
            
            // Erro Message
            public const string ErrorMessage = "/Stylesheets/LC/20180815ErrorMessage.xsl";

            // Reports
            public const string DailyRequestReport = "/Stylesheets/LC/20181126DailyRequestReport.xsl";
            public const string LoginHistoryReport = "/Stylesheets/LC/20181220LoginHistoryReport.xsl";
            public const string ExceptionErrorReport = "/Stylesheets/LC/20181220ExceptionErrorReport.xsl";
            public const string ApplicationReport = "/Stylesheets/LC/20190114ApplicationReport.xsl";
            public const string PaymentReport = "/Stylesheets/LC/20190121PaymentReport.xsl";
            public const string HoldTypeReport = "/Stylesheets/LC/20190123HoldTypeReport.xsl";
            public const string CollProgramApplicationReport = "/Stylesheets/LC/20190222CollProgramApplicationReport.xsl";
            public const string SentEmailReport = "/Stylesheets/LC/20190318SentEmailReport.xsl";
            public const string CollTestingResultsReport = "/Stylesheets/LC/20190620CollTestingResultsReport.xsl";
            public const string CollWaitListReport = "/Stylesheets/LC/20190222CollWaitListReport.xsl";
            public const string CollAdmissionConditionsReport = "/Stylesheets/LC/20190620CollAdmissionConditionsReport.xsl";
            public const string CollOverduesReport = "/Stylesheets/LC/20190620CollOverduesReport.xsl";
            public const string CollMissingGradeReport = "/Stylesheets/LC/20200223CollMissingGradeReport.xsl";
            public const string TransferCreditReport = "/Stylesheets/LC/20200115TransferCreditReport.xsl";
            public const string UnsolicitedBatchTranscriptReport = "/Stylesheets/LC/20211206UnsolicitedBatchTranscriptReport.xsl";
            public const string MyCredsBatchTranscriptReport = "/Stylesheets/MyCreds/20220124MyCredsBatchTranscriptReport.xsl";
            public const string MyCredsBulkSendReport = "/Stylesheets/MyCreds/20220203MyCredsBulkSendReport.xsl";


        }

        public struct Export
        {
            public const string DontExportMessage = "Selected/Filtered applications were already exported!\nThey cannot be exported again!";
            public const string EmptyDataMessage = "Please select applications to be exported!";
            public const string CannotExportMessage = "There are still applications to be imported to colleage!\nThe export cannot be done until previus applications are imported!";
            public const string EmptyTranscriptSelection = "Please select transcripts to be exported!";
            public const string EmptyParseTranscriptSelection = "Please select a transcript to be parsed!";
            public const string EmptyCourse = "Transcript has no course to be exported!";
            public const string EmptyRequestSelection = "Please select requests to be exported!";
            public const string EmptyStudentSelection = "Please select students to be exported!";
            public const string BatchExportedWithSucess = "Batch exported with success!";
            public const string BatchFilesNotSaved = "Batch files could not be saved or converted!";
            public const string BatchUploadedWithSucess = "Batch uploaded with success!";
            public const string BatchFilesNotUploaded = "Batch files could not be uploaded!";
            public const string CourseInstitutionNotFound = "Course Institution Organization not found in Colleague!<br/>The Institution APAS ID has to macth the 'INST - Other ID' field in Colleague!";
            public const string CourseidFailedToBeParsed = "External Transcript Course Id failed to be parsed!";
            public const string CourseAlreadyInColleague = "External Transcript Course is already in Colleague!";
            public const string CourseFailedToExportToColleague = "External Transcript Course failed to be exported to Colleague!";
        }
        public struct Project
        {
            public const string LcapasAdmin = "Admin";
            public const string LcapasCore = "Core";
            public const string LcapasUI = "UI";
        }

        public struct Class
        {
            public const string Lcapas = "Lcapas";
            public const string LcapasLogic = "LcapasLogic";
            public const string Apas = "Apas";
            public const string ApasLogic = "ApasLogic";
            public const string ColleagueLogic = "ColleagueLogic";
            public const string Utility = "Utility";
            public const string SendEmail = "SendEmail";
            public const string LandingController = "LandingController";
            public const string AdminController = "AdminController";
            public const string PaymentController = "PaymentController";
            public const string ApplicationsManager = "ApplicationsManager";
            public const string StopApplicationController = "StopApplicationController";
            public const string TranscriptsController = "TranscriptsController";
            public const string TranscriptObject = "TranscriptObject";
            public const string AdmissionsApplication = "AdmissionsApplication";
            public const string DatashareController = "DatashareController";
            public const string CollStuGrades = "CollStuGrades";
            public const string Service = "Service";
            public const string TranscriptsManager = "TranscriptsManager";
            public const string SettingReport = "ReportSetting";
            public const string Settings = "Settings";
            public const string TransferCredit = "TransferCredit";
            public const string MyCredsController = "MyCredsController";
            public const string MyCredsObject = "MyCredsObject";
        }

        public struct ApasCodeTypes {
            public const string AboriginalStatus = "AboriginalStatus";
            public const string CitizenshipStatus = "CitizenshipStatus";
            public const string Countries = "Countries";
            public const string CredRcvdExpect = "CredRcvdExpect";
            public const string DiplomaRcvd = "DiplomaRcvd";
            public const string FirstLanguage = "FirstLanguage";
            public const string HighestGrade = "HighestGrade";
            public const string MaritalStatus = "MaritalStatus";
        }

        public struct SettingTypes {
            public const string Boolean = "TrueFalse";
            public const string String = "String";
            public const string DteTime = "DteTime";
            public const string Integer = "Integer";
            public const string Double = "Double";
        }

        public struct MessageTypes
        {
            public const int StatisticalData = 6;
        }

        public struct ApplicationPageTitle
        {
            public const string Applications = "Admissions Applications";
            public const string Datashare = "U of L Nursing Applications";
        }

        public struct ApplicationSettingMessages
        {
            public const string CannotCreateItem = "Cannot create this item! \n It already exists!";
            public const string CannotDeleteItem = "Cannot delete this item! \n It has an association!";
            public const string ErrorCreatingItem = "An error occured while attempting to create this item! Please contact the Help Desk!";
            public const string ErrorDeletingItem = "An error occured while attempting to delete this item! Please contact the Help Desk!";
        }

        public struct AccessTypes {
            public const string Admissions = "Admissions";
            public const string Records = "Records";
            public const string Awards = "Awards";
            public const string Finance = "Finance";
        }

        public struct USP {
            public const string GetStudentGrades = "usp_get_stu_grades";
        }

        public struct DestActions
        {
            public const string RecordsInbox = "RecordsInbox";
            public const string AdmissionsOutbox = "AdmissionsOutbox";
            public const string RecordsOutbox = "RecordsOutbox";
            public const string AdmissionsInbox = "AdmissionsInbox";
            public const string CreateRequest = "CreateRequest";
            public const string CreateResponse = "CreateResponse";
            public const string CreateTranscript = "CreateTranscript";
            public const string HoldTypeReport = "HoldTypeReport";
            public const string SaveColleagueRequestTRRQ = "SaveColleagueRequestTRRQ";
            public const string ExportTranscriptToColleague = "ExportTranscriptToColleague";
        }

        public struct AuthenticationMessages
        {
            public const string UUIDMissing = "Authentication error: UUID missing!";
        }

        public struct SchemaVersion
        {
            public const string AdmissionsApplication_v0_0_1 = "urn:ca:applyalberta:message:AdmissionsApplication:v0.0.1";
            public const string AdmissionsApplication_v1_3_0 = "urn:org:pesc:message:AdmissionsApplication:v1.3.0";
            public const string CollegeTranscript_v1_0_0a = "urn:ca:applyalberta:message:CollegeTranscript:v1.0.0a";
            public const string CollegeTranscript_v1_6_0 = "urn:org:pesc:message:CollegeTranscript:v1.6.0";
            public const string HighSchoolTranscript_v1_0_0a = "urn:ca:applyalberta:message:HighSchoolTranscript:v1.0.0a";
            public const string HighSchoolTranscript_v1_5_0 = "urn:org:pesc:message:HighSchoolTranscript:v1.5.0";
            public const string TranscriptResponse_v1_0_0 = "urn:org:pesc:message:TranscriptResponse:v1.0.0";
            public const string TranscriptResponse_v1_4_0 = "urn:org:pesc:message:TranscriptResponse:v1.4.0";
            public const string TranscriptRequest_v1_0_0 = "urn:org:pesc:message:TranscriptRequest:v1.0.0";
            public const string TranscriptRequest_v1_4_0 = "urn:org:pesc:message:TranscriptRequest:v1.4.0";
            public const string AcademicRecord_v1_9_0 = "urn:org:pesc:sector:AcademicRecord:v1.9.0";
            public const string CoreMain_v1_14_0 = "urn:org:pesc:core:CoreMain:v1.14.0";
            public const string CoreMain_v1_16_0 = "urn:org:pesc:core:CoreMain:v1.16.0";
        }

        public struct XmlAttributes
        {
            public const string XmlSchemaInstance = "http://www.w3.org/2001/XMLSchema-instance";
            public const string XmlNsInstance = "http://www.w3.org/2000/xmlns/";
        }

        public struct Settingreport
        {
            public const string XmlSchemaInstance = "http://www.w3.org/2001/XMLSchema-instance";
            public const string XmlNsInstance = "http://www.w3.org/2000/xmlns/";
        }

        public struct ReportPageTitle
        {
            public const string DailyRequestReports = "Student Daily Request Report";
            public const string LoginHistoryReports = "Login History Report";
            public const string ExceptionErrorReports = "Exception Errors Report";
            public const string ApplicationReports = "Toolkit Application Report";
            public const string PaymentReports = "Payment Report";
            public const string CollProgramApplicationReports = "Colleague Application Report";
            public const string CollWaitListReports = "Wait List Report";
            public const string CollTestingResultsReports = "Testing Results Report";
            public const string CollOverduesReports = "Overdues Report";
            public const string CollAdmissionConditionsReports = "Admission Conditions Report";
            public const string CollMissingGradeReports = "Missing Grade Report";
            public const string HoldTypeReports = "Hold Type Report";
            public const string SentEmailReports = "Sent Email Report";
            public const string Datashare = "U of L Student Request Report";
        }

        public struct TranscripHoldTypes
        {
            public const string AfterGradesPosted = "AGP"; //"FG";  // Hold For Final Grades (After Grades Posted)
            public const string AfterDegreeAwarded = "ADA"; //"GN";  // Hold For Graduation (After Degree Awarded)
            public const string AfterSpecifiedTerm = "AST";  // Hold Specified Term (After Specified Term)
        }

        public struct MyCredsHoldTypes
        {
            public const string NoHold = "NO HOLD";
            public const string NoHoldDesc = "No Hold Type";
        }

        public struct ColleagueRequestHolds
        {
            public const string STU_REQUEST_LOG_HOLDS = "STU.REQUEST.LOG.HOLDS"; // Request Hold group name to lookup at the VALS code table
        }

        public struct Courses
        {
            public const string GradeScheme = "EXT";  // External Transfer Grade Scheme
        }

        public struct ReportType
        {
            public const string AdmissionConditionsReport = "AdmissionConditionsReport";
            public const string WaitListReport = "WaitListReport";
            public const string DailyRequestReport = "DailyRequestReport";
            public const string OverduesReport = "OverduesReport";
            public const string WebApplicationReport = "WebApplicationReport";
            public const string ToolkitApplicationReport = "ToolkitApplicationReport";
            public const string TestingResultsReport = "TestingResultsReport";
            public const string HoldTypeReport = "HoldTypeReport";
            public const string TransferCreditReport = "TransferCreditReport";
            public const string TransferCreditReportXML = "TransferCreditReportXML";
            public const string UnsolicitedBatchTranscriptReport = "UnsolicitedBatchTranscriptReport";
            public const string MyCredsBatchTranscriptReport = "MyCredsBatchTranscriptReport";
            public const string MyCredsBulkSendReport = "MyCredsBulkSendReport";
        }

        public struct Gender
        {
            public const string Female = "Female";
            public const string Male = "Male";
            public const string Unreported = "Unreported";
            public const string Unspecified = "Unspecified";
        }

        public struct EthnicityRace
        {
            public const string FirstNationStatus = "FirstNationStatus";
            public const string FirstNationsNonStatus = "FirstNationsNonStatus";
            public const string Inuit = "Inuit";
            public const string Metis = "Metis";
        }

        public struct TransferCreditPageTitle
        {
            public const string TransferCredits = "Transfer Credits";
        }

        public struct MyCredsDocumentTypes
        {
            public const string Transcript = "Transcript";
            public const string ConfirmationGraduation = "ConfirmationGraduation";
            public const string ConfirmationEnrolment = "ConfirmationEnrolment";
            public const string Parchment = "Parchment";
            public const string ConfirmationGraduationInternational = "ConfirmationGraduationInternational";
            public const string TranscriptPre1995 = "TranscriptPre1995";
        }

        public struct MyCredsFolders
        {
            public const string Transcripts = "Transcripts";
            public const string Credentials = "Credentials";
            public const string Conf_Enrollment = "Conf_Enrollment";
            public const string Badges = "Badges";
            public const string Export = "Export";
            public const string Import = "Import";
        }

        public struct MyCredsCharges
        {
            public const string Method = "share";
            public const string Amount = "1500";
            public const string Currency = "CAD";
        }

        public struct MyCredsRequestSettings
        {
            public const string Digitary = "digitary";
            public const string Type = "email";
            public const string Transcript = "transcript";
            public const string DocumentFormatPdf = "pdf";
            public const string DocumentFormatXml = "xml";
            public const string BatchCode = "Batch Code";
            public const string CertificationKeyId = "lethbridgecollege.ca.01.10.2021";
        }

        public struct WebApiCalls
        {
            public const string POST = "POST";
            public const string GET = "GET";
            public const string PUT = "PUT";
        }

        public struct WebApiContentTypes
        {
            public const string JSON = "application/json";
            public const string XML = "application/xml";
            public const string TextXML = "text/xml";
            public const string PDF = "application/pdf";
        }

        public struct WebApiQueryParameters
        {
            public const string FormatJSON = "?format=json";
            public const string FormatUploadPDF = "/data/pdf?filename=";
            public const string FormatUploadXML = "/data/xml?filename=";
        }
    }
}
