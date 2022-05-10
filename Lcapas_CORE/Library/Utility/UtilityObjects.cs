using Lcapas.Core.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Xml;
using System.Xml.Xsl;
using static Lcapas.Core.Library.Enums;
using System.Collections.Specialized;
using System.Text;
using System.Web.Mvc;

namespace Lcapas.Core.Library
{
    public class PaginationFilter
    {
        #region private variables
        private LcapasLogic lcapasLogic = new LcapasLogic();
        private int _pageSize = 50;
        private int _pageIndex = 1;
        private int _pageCount = 1;
        private int _recCount = 0;
        private String _pageTitle = string.Empty;

        private IEnumerable<SelectListItem> _PageSizes;
        #endregion private variables

        #region methods
        public PaginationFilter()
        {
            _PageSizes = lcapasLogic.PageSizes;
        }
        #endregion methods

        #region public variables
        [Display(Name = "Page Size: ")]
        [DataType(DataType.Text)]
        public int PageSize { get { return _pageSize; } set { _pageSize = value; } }

        [DataType(DataType.Text)]
        [Display(Name = "Page: ")]
        public int PageIndex { get { return _pageIndex; } set { _pageIndex = value; } }

        [DataType(DataType.Text)]
        [Display(Name = "Page Count: ")]
        public int PageCount { get { return _pageCount; } set { _pageCount = value; } }

        [DataType(DataType.Text)]
        [Display(Name = "Total Results: ")]
        public int RecCount { get { return _recCount; } set { _recCount = value; } }

        [Display(Name = "Results/Page: ")]
        public IEnumerable<SelectListItem> PageSizes { get { return _PageSizes; } }

        [DataType(DataType.Text)]
        [Display(Name = "Title: ")]
        public string PageTitle { get { return _pageTitle; } set { _pageTitle = value; } }


        #endregion public variables

    }

    public class ControllerTypeType
    {
        [Display(Name = "Controller Type")]
        public Enums.ControllerTypeType ControllerType { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Controller Description")]
        public string ControllerDesc { get; set; }

        public List<ActionTypeType> Actions;
    }

    public class ActionTypeType
    {
        [Display(Name = "Action Type")]
        public Enums.ActionTypeType ActionType { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Action Description")]
        public string ActionDesc { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "sNumber")]
        public string sNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        //[Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        //public string Username { get; set; }
        //[Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
        //[DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        //public string Password { get; set; }
    }

    public class UserAccessObj
    {
        #region private properties

        private string _UserId = string.Empty;
        private DateTime _LoginDateTime;
        private List<Enums.AccessGroupType> _AccessGroups;
        private string _Environment = string.Empty;
        private string _ConnectionString = string.Empty;

        private List<ControllerTypeType> _Controllers;
        #endregion private properties

        #region methods
        public UserAccessObj()
        {
            IsInitialized = true;
        }

        public bool Load(string sNumber)
        {
            bool success = false;
            try
            {
                if (!string.IsNullOrWhiteSpace(sNumber))
                {
                    _UserId = sNumber;
                    _Environment = Functions.GetEnvironment();
                    _ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["lcapasdb_entities"].ConnectionString;
                    _LoginDateTime = DateTime.Now;

                    // save connection string to session for first time load
                    success = Functions.SaveUserAccess(this);

                    // load permissions
                    using (LcapasLogic lcapasLogic = new LcapasLogic())
                    {
                        lcapasLogic.GetPermissions(this);
                    }

                    // save permissions to session for subsequent loads
                    success = Functions.SaveUserAccess(this);
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "LoadUserAccess", "Error: ", ex.ToString());
                }
            }
            return success;
        }

        #endregion methods

        #region public properties
        public bool IsInitialized { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "UserId")]
        public string UserId { get { return _UserId; } set { _UserId = value; } }

        [DataType(DataType.DateTime)]
        [Display(Name = "Login DateTime")]
        public DateTime LoginDateTime { get { return _LoginDateTime; } set { _LoginDateTime = value; } }

        [Display(Name = "Controllers")]
        public List<ControllerTypeType> Controllers { get { return _Controllers; } set { _Controllers = value; } }

        [Display(Name = "Access Groups")]
        public List<Enums.AccessGroupType> AccessGroups { get { return _AccessGroups; } set { _AccessGroups = value; } }

        public string Environment { get { return _Environment; } }

        public string ConnectionString { get { return _ConnectionString; } }

        #endregion public properties
    }

    public class SendEmail
    {
        // must be set
        private string _Uuid = string.Empty;
        private string _To = string.Empty;
        private string _Cc = null;
        private string _From = string.Empty;
        private string _Title = string.Empty;
        private string _Subtitle = string.Empty;
        private string _Body = string.Empty;
        private string _Format = string.Empty;
        private string _Env = string.Empty;

        private string _ReceiverGroup = string.Empty;
        private string _InProvInst = string.Empty;
        private string _OutProvInst = string.Empty;
        private string _StudentEmail = string.Empty;
        private string _StudentFirstName = string.Empty;
        private string _StudentLastName = string.Empty;
        private string _StudentASN = string.Empty;
        private string _ApplicationDate = string.Empty;
        private string _ProgramCodeSelected = string.Empty;
        private string _ProgramDescSelected = string.Empty;
        private string _TermCodeSelected = string.Empty;
        private string _TermDescSelected = string.Empty;
        private string _ApplicationID = string.Empty;
        private bool _RequiresTranscript = true;

        private LcapasLogic lcapasLogic = new LcapasLogic();

        private void CheckEnvironment()
        {
            if (Functions.GetEnvironment().ToLower() != Structs.Environment.Prod.ToLower())
            {
                _Env = " - TESTING APPLICATION SYSTEM";
            }
        }

        public SendEmail()
        {
            CheckEnvironment();
        }

        public SendEmail(string emailtype, string receivergroup, string uuid, bool send, string errorMessage = null, string applicationID = null)
        {
            try
            {
                CheckEnvironment();

                _Uuid = uuid ?? string.Empty;
                _ReceiverGroup = receivergroup ?? string.Empty;
                _ApplicationID = applicationID ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(_Uuid) && !string.IsNullOrWhiteSpace(_ReceiverGroup)) {
                    if (SetupStuInfo())
                    {
                        switch (emailtype)
                        {
                            case Structs.EmailType.ApplicationPaymentConfirm:
                                AdmAppStuPayEmail();
                                break;
                            case Structs.EmailType.ApplicationPaymentError:
                                switch (receivergroup)
                                {
                                    case Structs.EmailReceiverGroup.Student:
                                        AdmAppStuPayErrEmail();
                                        break;
                                    case Structs.EmailReceiverGroup.Admissions:
                                        AdmAppPayErrEmail();
                                        break;
                                    case Structs.EmailReceiverGroup.Technical:
                                        AdmAppTechPayErrEmail();
                                        break;
                                }
                                break;
                            case Structs.EmailType.ApplicationDoublePaymentError:
                                switch (receivergroup)
                                {
                                    case Structs.EmailReceiverGroup.Student:
                                        AdmAppStuDblPayErrEmail();
                                        break;
                                    case Structs.EmailReceiverGroup.Admissions:
                                        AdmAppDblPayErrEmail();
                                        break;
                                    case Structs.EmailReceiverGroup.Technical:
                                        AdmAppTechDblPayErrEmail();
                                        break;
                                }
                                break;
                            case Structs.EmailType.ApplicationCancelPayment:
                                switch (receivergroup)
                                {
                                    case Structs.EmailReceiverGroup.Student:
                                        AdmAppStuCancelEmail();
                                        break;
                                    case Structs.EmailReceiverGroup.Admissions:
                                        AdmAppAdmissionCancelEmail();
                                        break;
                                    case Structs.EmailReceiverGroup.Technical:
                                        AdmAppTechCancelEmail();
                                        break;
                                }
                                break;
                            case Structs.EmailType.TechErrorEmail:
                                switch (receivergroup)
                                {
                                    case Structs.EmailReceiverGroup.Technical:
                                        AdmAppTechErrorEmail();
                                        break;
                                }
                                break;
                        }

                        // construct message body
                        _Body = string.Format(_Format, _Title, _Subtitle, _StudentFirstName, _StudentLastName, _StudentASN, _InProvInst, _OutProvInst, _ApplicationDate, uuid,
                                              _ProgramCodeSelected, _ProgramDescSelected, _TermCodeSelected, _TermDescSelected, errorMessage, _ApplicationID);

                        if (GroupCanSend() && send)
                        {
                            Send();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "SendApplicationEmail", "Error: ", ex.ToString());
            }
        }

        public SendEmail(string project, string page, string function, string error, string message)
        {
            LcapasLogic lcapasLogic = new LcapasLogic();

            try
            {
                _To = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.TechEmailAddr);
                _From = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmEmailAddr);
                _Title = "LCAPAS application error";
                _Body = "An application error has occured in LCAPAS<br />";
                _Body += "The error occured at: " + DateTime.Now + ".<br />";
                _Body += "Application: " + project + ".<br />";
                _Body += "Class: " + page + ".<br />";
                _Body += "Function: " + function + ".<br />";
                _Body += "Type: " + error + "<br />";
                _Body += "Message: " + message + "<br /><br />";
                _Body += "This is an automated email from the Lethbridge College LCAPAS application.<br />";
                _Body += "Please do not respond.";

                if (GroupCanSend())
                {
                    Send();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "SendTechErrorEmail", "Error: ", ex.ToString());
            }
        }

        public SendEmail(string gradeCode, string courseTitle, string studentName, string ASN, string sNumber, string institutionName)
        {
            LcapasLogic lcapasLogic = new LcapasLogic();

            try
            {
                // Check settings if it's set to send to admission email address
                if (lcapasLogic.GetSetting(Structs.SettingTypes.Boolean, Structs.EmailSettings.AdmMissingGradeSendEmailEnabled))
                {
                    _ReceiverGroup = Structs.EmailReceiverGroup.Admissions;
                    _To = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmManagerMissingGradeEmailAddr);
                } else
                {
                    _ReceiverGroup = Structs.EmailReceiverGroup.Technical;
                    _To = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.TechEmailAddr);
                }
                _Cc = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.TechEmailAddr);
                _From = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmEmailAddr);
                _Title = "LCAPAS Transcript - Grade code missing in Colleague! - Environment: " + _Env;
                _Body = "An external transcript was received in the LCAPAS Toolkit, with a grade code that doesn't yet exists in Colleague!<br />";
                _Body += "It was received at: " + DateTime.Now + "<br /><br />";
                _Body += "Grade Code: <b>" + gradeCode + "</b><br /><br />";
                _Body += "Source Institution: " + institutionName + "<br />";
                _Body += "Course: " + courseTitle + "<br />";
                _Body += "Student Name: " + studentName + "<br />";
                _Body += "ASN: " + ASN + "<br />";
                _Body += "sNumber: " + sNumber + "<br /><br />";
                _Body += "This is an automated email from the Lethbridge College LCAPAS Toolkit.<br />";
                _Body += "Please do not respond.";

                if (GroupCanSend())
                {
                    Send();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "SendEmail - Missing Grade in Colleague", "Error: ", ex.ToString());
            }
        }

        private void Send()
        {
            LcapasLogic lcapasLogic = new LcapasLogic();

            try
            {
                bool credentials = lcapasLogic.GetSetting(Structs.SettingTypes.Boolean, Structs.EmailSettings.SmtpCredentials);

                WebMail.From = _From;
                WebMail.SmtpPort = lcapasLogic.GetSetting(Structs.SettingTypes.Integer, Structs.EmailSettings.SmtpPort);
                WebMail.SmtpServer = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.SmtpServer);
                WebMail.EnableSsl = lcapasLogic.GetSetting(Structs.SettingTypes.Boolean, Structs.EmailSettings.SmtpSSL);

                if (credentials)
                {
                    WebMail.UserName = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.SmtpUserName);
                    WebMail.Password = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.SmtpPassword);
                }

                if (!string.IsNullOrWhiteSpace(_To))
                {
                    WebMail.Send(to: _To, subject: _Title + _Env, body: _Body, from: _From, cc: _Cc, filesToAttach: null, isBodyHtml: true, additionalHeaders: null, bcc: null, contentEncoding: null, headerEncoding: null, priority: null, replyTo: null);

                    // Save a email copy/history
                    lcapasLogic.SaveSentEmail(this);
                }
                else
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "Send", "Recipient email address missing!", "Body Message: " + _Body);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "Send", "Error sending confirmation: ", ex.ToString());
            }
        }

        private bool SetupStuInfo()
        {
            bool success = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(_Uuid))
                {
                    Models.Lcappsdb.Person _Person = lcapasLogic.GetPersonByApplicationId(_Uuid);

                    if (_Person != null)
                    {
                        // Student Email
                        if (_Person.Emails.Count() > 0) {
                            _StudentEmail = _Person.Emails.OrderByDescending(x => x.CreatedDateTime).Select(a => a.EmailAddress).FirstOrDefault();
                        }
                        // Student Name
                        if (_Person.Names.Count() > 0)
                        {
                            _StudentFirstName = _Person.Names.Where(t => t.NameType == Structs.Name.PersonalNameType).OrderByDescending(x => x.CreatedDateTime).Select(a => a.FirstName).FirstOrDefault();
                            _StudentLastName = _Person.Names.Where(t => t.NameType == Structs.Name.PersonalNameType).OrderByDescending(x => x.CreatedDateTime).Select(a => a.LastName).FirstOrDefault();
                        }
                        // Student ASN
                        if (_Person.Students.Count() > 0)
                        {
                            _StudentASN = _Person.Students.FirstOrDefault().ASNs.OrderByDescending(x => x.CreatedDateTime).Select(a => a.AgencyAssignedID).FirstOrDefault();

                            // Student Application
                            Models.Lcappsdb.StudentApplication _StudentApplication = _Person.Students.FirstOrDefault().StudentApplications.Where(y => y.ApplicationMessages.Any(z => z.UUID == _Uuid)).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();
                            if (_StudentApplication != null)
                            {
                                _ApplicationDate = _StudentApplication.CreatedDateTime.ToString("yyyy/MM/dd HH:mm");

                                if (_StudentApplication.ProgramDetail != null)
                                {
                                    _ProgramCodeSelected = _StudentApplication.ProgramDetail.ApplicationProgram.ProgramCode;
                                    _ProgramDescSelected = _StudentApplication.ProgramDetail.ApplicationProgram.ProgramDesc;
                                    _TermCodeSelected = _StudentApplication.ProgramDetail.ProgramTerm.TermCode;
                                    _TermDescSelected = _StudentApplication.ProgramDetail.ProgramTerm.TermDesc;
                                    _RequiresTranscript = _StudentApplication.ProgramDetail.ApplicationProgram.RequiresTranscript;
                                }
                            }
                        }
                    }

                    success = true;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "SetupStuInfo", "Error: UUID = " + _Uuid, ex.ToString());
            }

            return success;
        }

        private bool AdmAppStuPayEmail()
        {
            bool success = false;

            try
            {
                // Send student confirmation email
                List<string> _AlbertaInstitutions = lcapasLogic.GetInstitutionsByUUID(_Uuid, true);
                List<string> _OtherInstitutions = lcapasLogic.GetInstitutionsByUUID(_Uuid, false);

                if (_OtherInstitutions.Any())
                {
                    foreach (string _institution in _OtherInstitutions)
                    {
                        _OutProvInst += _institution + "<br />";
                    }
                }
                else
                {
                    _OutProvInst = "<b>None</b>.  You did not report any post secondary academic history outside of Alberta.<br />";
                }

                if (_AlbertaInstitutions.Any())
                {
                    foreach (string _province in _AlbertaInstitutions)
                    {
                        _InProvInst += _province + "<br />";
                    }
                }
                else
                {
                    _InProvInst = "<b>None</b>. You did not report any post secondary academic history in Alberta.<br />";
                }

                _To = _StudentEmail;
                _From = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmEmailAddr);
                _Title = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.PayCompMsgTtl);
                _Subtitle = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.PayCompMsgSubTtl);
                if (_RequiresTranscript)
                {
                    _Format = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.StuPayCompMsgBody);
                }
                else
                {
                    // Don't show Transcript request text
                    _Format = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.StuPayCompMsgBodyNoTranscript);
                }

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "AdmAppStuPayEmail", "Error: ", ex.ToString());
            }

            return success;
        }

        private bool AdmAppStuPayErrEmail()
        {
            bool success = false;

            try
            {
                _To = _StudentEmail;
                _From = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmEmailAddr);
                _Title = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.StuPayErrMsgTtl);
                _Subtitle = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.StuPayErrMsgSubTtl);
                _Format = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.StuPayErrMsgBody);

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "AdmAppStuPayErrEmail", "Error: ", ex.ToString());
            }

            return success;
        }

        private bool AdmAppStuDblPayErrEmail()
        {
            bool success = false;

            try
            {
                _To = _StudentEmail;
                _From = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmEmailAddr);
                _Title = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.StuDblPayErrMsgTtl);
                _Subtitle = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.StuDblPayErrMsgSubTtl);
                _Format = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.StuDblPayErrMsgBody);

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "AdmAppStuDblPayErrEmail", "Error: ", ex.ToString());
            }

            return success;
        }

        private bool AdmAppPayErrEmail()
        {
            bool success = false;

            try
            {
                //_To = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmEmailAddr);
                _To = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmManagerEmailAddr);
                _From = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmEmailAddr);
                _Title = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmPayErrMsgTtl);
                _Subtitle = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmPayErrMsgSubTtl);
                _Format = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmPayErrMsgBody);

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "AdmAppPayErrEmail", "Error: ", ex.ToString());
            }

            return success;
        }

        private bool AdmAppDblPayErrEmail()
        {
            bool success = false;

            try
            {
                //_To = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmEmailAddr);
                _To = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmManagerEmailAddr);
                _From = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmEmailAddr);
                _Title = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmDblPayErrMsgTtl);
                _Subtitle = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmDblPayErrMsgSubTtl);
                _Format = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmDblPayErrMsgBody);

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "AdmAppDblPayErrEmail", "Error: ", ex.ToString());
            }

            return success;
        }

        private bool AdmAppTechPayErrEmail()
        {
            bool success = false;

            try
            {
                _To = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.TechEmailAddr);
                _From = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmEmailAddr);
                _Title = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.TechPayErrMsgTtl);
                _Subtitle = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.TechPayErrMsgSubTtl);
                _Format = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.TechPayErrMsgBody);

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "AdmAppTechPayErrEmail", "Error: ", ex.ToString());
            }

            return success;
        }

        private bool AdmAppTechDblPayErrEmail()
        {
            bool success = false;

            try
            {
                _To = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.TechEmailAddr);
                _From = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmEmailAddr);
                _Title = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.TechDblPayErrMsgTtl);
                _Subtitle = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.TechDblPayErrMsgSubTtl);
                _Format = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.TechDblPayErrMsgBody);

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "AdmAppTechDblPayErrEmail", "Error: ", ex.ToString());
            }

            return success;
        }

        private bool AdmAppStuCancelEmail()
        {
            bool success = false;

            try
            {
                _To = _StudentEmail;
                _From = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmEmailAddr);
                _Title = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.StuCancelMsgTitle);
                _Subtitle = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.StuCancelMsgSubtitle);
                _Format = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.StuCancelMsgBody);

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "AdmAppStuCancelEmail", "Error: ", ex.ToString());
            }

            return success;
        }

        private bool AdmAppAdmissionCancelEmail()
        {
            bool success = false;

            try
            {
                _To = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmEmailAddr);
                _From = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmEmailAddr);
                _Title = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.StuCancelMsgTitle);
                _Subtitle = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.StuCancelMsgSubtitle);
                _Format = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.StuCancelMsgBody);

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "AdmAppAdmissionCancelEmail", "Error: ", ex.ToString());
            }

            return success;
        }

        private bool AdmAppTechCancelEmail()
        {
            bool success = false;

            try
            {
                _To = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.TechEmailAddr);
                _From = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmEmailAddr);
                _Title = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.StuCancelMsgTitle);
                _Subtitle = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.StuCancelMsgSubtitle);
                _Format = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.StuCancelMsgBody);

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "AdmAppTechCancelEmail", "Error: ", ex.ToString());
            }

            return success;
        }

        private bool AdmAppTechErrorEmail()
        {
            bool success = false;

            try
            {
                _To = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.TechEmailAddr);
                _From = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.AdmEmailAddr);
                _Title = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.TechErrMsgTtl);
                _Subtitle = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.TechErrMsgSubTtl);
                _Format = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.EmailSettings.TechErrMsgBody);

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "AdmAppTechErrorEmail", "Error: ", ex.ToString());
            }

            return success;
        }

        private bool GroupCanSend()
        {
            bool _Allowed = false;
            LcapasLogic lcapasLogic = new LcapasLogic();

            try
            {
                switch (_ReceiverGroup)
                {
                    case Structs.EmailReceiverGroup.Technical:
                        _Allowed = lcapasLogic.GetSetting(Structs.SettingTypes.Boolean, Structs.EmailSettings.TechSendEmailEnabled);
                        break;
                    case Structs.EmailReceiverGroup.Admissions:
                        _Allowed = lcapasLogic.GetSetting(Structs.SettingTypes.Boolean, Structs.EmailSettings.AdmSendEmailEnabled);
                        break;
                    case Structs.EmailReceiverGroup.Student:
                        _Allowed = lcapasLogic.GetSetting(Structs.SettingTypes.Boolean, Structs.EmailSettings.StuSendEmailEnabled);
                        break;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.SendEmail, "PermissionsOk", "Error: ", ex.ToString());
            }
            return _Allowed;
        }

        public string Title
        {
            get
            {
                return _Title;
            }
            set {
                _Title = value;
            }
        }

        public string Subtitle
        {
            get
            {
                return _Subtitle;
            }
            set {
                _Subtitle = value;
            }

        }

        public string Body
        {
            get
            {
                return _Body;
            }
            set {
                _Body = value;
            }
        }

        public string ReceiverGroup
        {
            get
            {
                return _ReceiverGroup;
            }
            set
            {
                _ReceiverGroup = value;
            }
        }

        public string From
        {
            get
            {
                return _From;
            }
            set
            {
                _From = value;
            }
        }

        public string To
        {
            get
            {
                return _To;
            }
            set
            {
                _To = value;
            }
        }
    }

    public class TransformXml
    {
        #region private properties
        LcapasLogic lcapasLogic = new LcapasLogic();
        private XmlDocument _doc;
        private XmlReaderSettings _readerSettings;
        private XsltSettings _styleSettings;
        private Uri _includeUrlToXsl;
        private Uri _defaultUrlToXsl;
        private XmlUrlResolver _externalLinkResolver;
        #endregion private properties

        #region constructors

        /// <summary>
        /// Creates an TransformXml class to handle the downloading and subsequent application of Xslt and Xsl style sheets 
        /// to Xml documents.
        /// </summary>
        /// <param name="documentToTransform">The Xml document to manipulate</param>
        /// <param name="settings">Options on how to process the xml and xslt documents. If XsltAllowedSettings.None is used,
        /// document is processed with the default settings.</param>
        /// <exception cref="ArgumentNullException">Must have a valid XmlDocument to perform any transformation</exception>
        public TransformXml(XmlDocument documentToTransform, XsltAllow settings, Uri defaultXslUri)
        {
            if (documentToTransform != null)
            {
                _doc = documentToTransform;
            }
            else
            {
                throw new ArgumentNullException("Must have a valid XmlDocument to perform any transformation");
            }

            if (defaultXslUri != null) { _defaultUrlToXsl = defaultXslUri; } 

            CreateSettings(settings);
            GetStyleSheetUri(defaultXslUri);
        }

        #endregion constructors

        #region methods

        private void CreateSettings(XsltAllow settingsParams)
        {
            if ((settingsParams & XsltAllow.None) == XsltAllow.None)
            {
                _readerSettings = new XmlReaderSettings();
                _readerSettings.DtdProcessing = (settingsParams & XsltAllow.DtdProcessing) == XsltAllow.DtdProcessing ? DtdProcessing.Parse : DtdProcessing.Prohibit;
                _styleSettings = new XsltSettings();
                _styleSettings.EnableDocumentFunction = (settingsParams & XsltAllow.DocumentFunction) == XsltAllow.DocumentFunction;
                _styleSettings.EnableScript = (settingsParams & XsltAllow.EmbeddedScripts) == XsltAllow.EmbeddedScripts;
                _externalLinkResolver = (settingsParams & XsltAllow.ExternalResources) == XsltAllow.ExternalResources ? new XmlUrlResolver() : null;
            }
            else
            {
                _readerSettings = new XmlReaderSettings();
                _styleSettings = XsltSettings.Default;
                _externalLinkResolver = null;
            }
        }

        private void GetStyleSheetUri(Uri defaultXslUri = null)
        {
            try
            {
                XmlProcessingInstruction instruction = _doc.SelectSingleNode("processing-instruction('xml-stylesheet')") as XmlProcessingInstruction;
                if (instruction != null && instruction.Value.Contains("href="))
                {
                    string url = System.Text.RegularExpressions.Regex.Match(instruction.Data, "href=(\"|')(?<url>.*?)(\"|')").Groups["url"].Value;
                    if (string.IsNullOrWhiteSpace(url))
                    {
                        url = instruction.Value.Substring(instruction.Value.IndexOf("href="));
                        url = url.Replace("\"", string.Empty).Replace("'", string.Empty).Replace("href=", string.Empty).Trim();
                    }

                    if (CheckUrl(url))
                    {
                        _includeUrlToXsl = new Uri(url);
                    }
                    else
                    {
                        _includeUrlToXsl = defaultXslUri;
                    }
                }
                else
                {
                    _includeUrlToXsl = defaultXslUri;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.Utility, "GetStyleSheetUri", "Error: ", ex.ToString());
                throw new XsltException("Cannot get stylesheet URL! - defaultXslUri: "+ defaultXslUri.ToString() + " - Error: " + ex.ToString());
            }
        }

        public string ApplyTransformation()
        {
            XslCompiledTransform myXslTrans = new XslCompiledTransform();

            try
            {
                myXslTrans = CompileTransformFromUri();
                return GetTransformedDocument(myXslTrans);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.Utility, "ApplyTransformation", "Error: ", ex.ToString());
                throw new XsltException("Cannot find stylesheet URL within the XmlDocument and no document or Uri was specified! - Error: " + ex.ToString());
            }
        }

        private string GetTransformedDocument(XslCompiledTransform myXslTrans)
        {
            using (StringReader xmlStringReader = new StringReader(_doc.InnerXml))
            {
                using (XmlReader reader = XmlReader.Create(xmlStringReader, _readerSettings))
                {
                    using (StringWriter htmlConverted = new StringWriter())
                    {
                        XmlTextWriter myWriter = new XmlTextWriter(htmlConverted);
                        myXslTrans.Transform(reader, null, myWriter);
                        return htmlConverted.GetStringBuilder().ToString();
                    }
                }
            }
        }

        private XslCompiledTransform CompileTransformFromUri()
        {
            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            try
            {
                myXslTrans = SetUri(_includeUrlToXsl.ToString());

                if (myXslTrans == null && _includeUrlToXsl != _defaultUrlToXsl) { myXslTrans = SetUri(_defaultUrlToXsl.ToString()); }
            }
            catch (Exception ex)
            {
                myXslTrans = null;
                lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.Utility, "CompileTransformFromUri", "Error: ", "myXslTrans: " + myXslTrans + " - Error: " + ex.ToString());
                throw new XsltException("Cannot set URI stylesheet for the XmlDocument and no document or Uri was specified! - Error: " + ex.ToString());
            }

            return myXslTrans;
        }

        private XslCompiledTransform SetUri(string urlToXsl)
        {
            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            try
            {
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (XmlReader stylesheetReader = XmlReader.Create(urlToXsl, _readerSettings))
                {
                    myXslTrans.Load(stylesheetReader, _styleSettings, _externalLinkResolver);
                }
            }
            catch (Exception ex)
            {
                myXslTrans = null;
                lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.Utility, "SetUri", "Error: ", "urlToXsl: " + urlToXsl + " - Error: " + ex.ToString());
            }

            return myXslTrans;
        }

        private bool CheckUrl(string url)
        {
            HttpWebResponse response;
            Uri urlCheck = new Uri(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlCheck);
            request.Timeout = lcapasLogic.GetSetting(Structs.SettingTypes.Integer, Structs.Settings.StylesheetRequestTimeout) ?? 2000;
            bool success = false;

            try
            {
                response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                success = false;
                lcapasLogic.SaveException(Structs.Project.LcapasUI, Structs.Class.Utility, "CheckUrl", "Error: ", "URL: " + url + " - Error: " + ex.ToString());
            }

            return success;
        }
        #endregion methods

        #region public properties
        /// <summary>
        /// The embedded Xslt reference. The Uri pointing to the Xslt document 
        /// </summary>
        public Uri XsltLocation
        {
            get
            {
                return _includeUrlToXsl;
            }
        }

        /// <summary>
        /// The xmlDocument that is being manipulated
        /// </summary>
        public XmlDocument XmlDocument
        {
            get
            {
                return _doc;
            }
        }

        /// <summary>
        /// For explicitly setting the settings used in the xsltTransform.
        /// <para>Note: Generally should use the bit flags in the constructors instead.</para>
        /// </summary>
        public XsltSettings XsltSettings
        {
            get
            {
                return _styleSettings;
            }
            set
            {
                _styleSettings = value;
            }
        }

        /// <summary>
        /// For explicitly setting the settings used in the reading of the xml. 
        /// <para>Note: Generally should use the bit flags in the constructors instead.</para>
        /// </summary>
        public XmlReaderSettings XmlReaderSettings
        {
            get
            {
                return _readerSettings;
            }
            set
            {
                _readerSettings = value;
            }
        }
        #endregion public properties
    }

    /// <summary>
    /// Summary description for NVPAPICaller
    /// </summary>
    public class NVPAPICaller
    {
        private const string VENDOR = "VENDOR";
        private const string PARTNER = "PARTNER";
        private const string PWD = "PWD";

        private int TIMEOUT;
        private double AMT;
        private string APIUser, APIPassword, APIVendor, APIPartner, BNCode, EndPointUrl, Host;
        private string TENDER, ACTION, TRXTYPE, RETURNURL, CANCELURL, ERRORURL, CREATESECURETOKEN, ORDERID;
        private string retMsg = string.Empty;
        private string SECURETOKENID = string.Empty;
        private static readonly string[] SECURED_NVPS = new string[] { VENDOR, PARTNER, PWD };

        public void LoadPayPalSettings(string applicationID = null)
        {

            LcapasLogic lcapasLogic = new LcapasLogic();

            // DEVELOPMENT & PRODUCTION
            // TODO: Read from 

            if (Functions.GetEnvironment() == Structs.Environment.Prod) // change to bool?
            {
                Host = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalProdHostPath);
                EndPointUrl = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalProdEndPointPath);

                APIUser = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalProdAPIUser);
                APIPassword = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalProdAPIPassword);
                APIVendor = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalProdAPIVendor);
                APIPartner = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalProdAPIPartner);
            }
            else
            {
                Host = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalDevHostPath);
                EndPointUrl = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalDevEndPointPath);

                APIUser = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalDevAPIUser);
                APIPassword = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalDevAPIPassword);
                APIVendor = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalDevAPIVendor);
                APIPartner = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalDevAPIPartner);
            }

            BNCode = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalBNCode);

            TENDER = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalTender);
            ACTION = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalAction);
            TRXTYPE = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalTRXType);
            ERRORURL = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalErrorPath);
            RETURNURL = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalReturnPath);
            CANCELURL = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalCancelPath);
            // Pulling from Application fee table
            AMT = lcapasLogic.GetApplicationFee().ApplicationFeeAmt;
            CREATESECURETOKEN = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayPalCreateSecureToken);
            ORDERID = applicationID;  // Pass Apllication Id to avoid duplicate payment

            TIMEOUT = lcapasLogic.GetSetting(Structs.SettingTypes.Integer, Structs.Settings.PayPalTimeOut);
        }

        /// <summary>
        /// ShortcutExpressCheckout: The shortcut implementation of SetExpressCheckout
        /// </summary>
        /// <param name="amt"></param>
        /// <param ref name="token"></param>
        /// <param ref name="retMsg"></param>
        /// <returns></returns>
        public bool SetExpressCheckout(string serverUrl, ref string token, ref string tokenId, ref string retMsg, ref NameValueCollection paypalParameters, string userName = null, string uuid = null, string applicationID = null)
        {
            NVPCodec encoder = new NVPCodec();
            LcapasLogic lcapasLogic = new LcapasLogic();

            encoder["TRXTYPE"] = TRXTYPE;
            encoder["RETURNURL"] = serverUrl + RETURNURL;
            if (!string.IsNullOrWhiteSpace(uuid)) { encoder["RETURNURL"] = encoder["RETURNURL"] + "?uuid=" + uuid; }
            encoder["CANCELURL"] = serverUrl + CANCELURL;
            encoder["ERRORURL"] = serverUrl + ERRORURL;
            encoder["AMT"] = AMT.ToString();
            encoder["NOSHIPPING"] = "1";
            encoder["URLMETHOD"] = Structs.WebApiCalls.POST;
            encoder["CREATESECURETOKEN"] = CREATESECURETOKEN;
            if (!string.IsNullOrWhiteSpace(ORDERID)) { encoder["ORDERID"] = ORDERID; }
            encoder["SECURETOKENID"] = Guid.NewGuid().ToString();
            SECURETOKENID = encoder["SECURETOKENID"];

            // TO DO: FOR TESTING PURPOSES!!!!!!!!!!!!!!
            // When user is 'LC_Testuser' AMT will change to $1.00
            if (!string.IsNullOrWhiteSpace(userName))
            {
                bool testUserActive = lcapasLogic.GetSetting(Structs.SettingTypes.Boolean, Structs.Settings.TestUserActive);
                string testUserName = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.TestUserName);  // bool LC_Testuser = userName == "Testuser Lethbridge1";
                string testPaypalAmount = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.TestPaypalAmount);  // encoder["AMT"] = "1";

                if (testUserActive && userName == testUserName)
                {
                    encoder["AMT"] = testPaypalAmount;
                }
            }

            string uniqueID = uuid ?? Guid.NewGuid().ToString();
            if (!string.IsNullOrWhiteSpace(applicationID))
            {
                // Use Apllication Id to avoid duplicate payment
                uniqueID = applicationID;
            }

            string pStrrequestforNvp = encoder.Encode();

            string pStresponsenvp = HttpCall(pStrrequestforNvp, uniqueID);

            NVPCodec decoder = new NVPCodec();
            decoder.Decode(pStresponsenvp);

            string strAck = decoder["RESULT"].ToLower();
            if (strAck != null && strAck == "0")
            {
                token = decoder["SECURETOKEN"];
                tokenId = decoder["SECURETOKENID"];

                if (Functions.GetEnvironment() == Structs.Environment.Prod)
                {
                    retMsg = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayFlowProdLink);
                    paypalParameters.Add("MODE", "LIVE");
                }
                else
                {
                    retMsg = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.PayFlowTestLink);
                    paypalParameters.Add("MODE", "TEST");
                }

                paypalParameters.Add("SECURETOKEN", token);
                paypalParameters.Add("SECURETOKENID", tokenId);

                return true;
            }
            else
            {
                retMsg = "ErrorCode=" + strAck + "&Desc=" + decoder["RESPMSG"];

                return false;
            }
        }

        /// <summary>
        /// HttpCall: The main method that is used for all API calls
        /// </summary>
        /// <param name="NvpRequest"></param>
        /// <returns></returns>
        public string HttpCall(string NvpRequest, string unique_id)
        {
            string url = EndPointUrl;

            string strPost = NvpRequest + "&" + BuildCredentialsNVPString();
            strPost = strPost + "&BUTTONSOURCE=" + BNCode;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Timeout = TIMEOUT;
            objRequest.Method = Structs.WebApiCalls.POST;
            objRequest.ContentLength = strPost.Length;
            objRequest.ContentType = "text/namevalue";
            objRequest.Headers.Add("X-VPS-CLIENT-TIMEOUT", "45");
            objRequest.Headers.Add("X-VPS-REQUEST-ID", unique_id);

            try
            {
                using (StreamWriter myWriter = new StreamWriter(objRequest.GetRequestStream()))
                {
                    myWriter.Write(strPost);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            //Retrieve the Response returned from the NVP API call to PayPal
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            string result;
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }

        /// <summary>
        /// HttpCall: The main method that is used for all API calls
        /// </summary>
        /// <param name="NvpRequest"></param>
        /// <returns></returns>
        public string SubmitForm(string url, NameValueCollection paypalParameters, string method = "GET")
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string formName = "submitPayment";
            var context = HttpContext.Current;

            try
            {
                context.Response.Clear();
                context.Response.Write("<html><head>");
                context.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", formName));
                context.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", formName, method, url));
                for (int i = 0; i < paypalParameters.Keys.Count; i++)
                    context.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode(paypalParameters.Keys[i]), HttpUtility.HtmlEncode(paypalParameters[paypalParameters.Keys[i]])));
                context.Response.Write("</form>");
                context.Response.Write("</body></html>");
                context.Response.End();
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "SubmitForm", "Error", ex.ToString());
                }
            }

            return context.ToString();
        }

        /// <summary>
        /// Credentials added to the NVP string
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        private string BuildCredentialsNVPString()
        {
            NVPCodec codec = new NVPCodec();

            if (!string.IsNullOrWhiteSpace(APIUser))
                codec["USER"] = APIUser;

            if (!string.IsNullOrWhiteSpace(APIPassword))
                codec[PWD] = APIPassword;

            if (!string.IsNullOrWhiteSpace(APIVendor))
                codec[VENDOR] = APIVendor;

            if (!string.IsNullOrWhiteSpace(APIPartner))
                codec[PARTNER] = APIPartner;

            return codec.Encode();
        }
    }

    [Serializable]
    public sealed class NVPCodec : NameValueCollection
    {
        private const string AMPERSAND = "&";
        private const string EQUALS = "=";
        private static readonly char[] AMPERSAND_CHAR_ARRAY = AMPERSAND.ToCharArray();
        private static readonly char[] EQUALS_CHAR_ARRAY = EQUALS.ToCharArray();

        /// <summary>
        /// Returns the built NVP string of all name/value pairs in the Hashtable
        /// </summary>
        /// <returns></returns>
        public string Encode()
        {
            StringBuilder sb = new StringBuilder();
            bool firstPair = true;
            foreach (string kv in AllKeys)
            {
                string name = kv;
                string val = this[kv];
                if (!firstPair)
                {
                    sb.Append(AMPERSAND);
                }
                sb.Append(name).Append(EQUALS).Append(val);
                firstPair = false;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Decoding the string
        /// </summary>
        /// <param name="nvpstring"></param>
        public void Decode(string nvpstring)
        {
            Clear();
            foreach (string nvp in nvpstring.Split(AMPERSAND_CHAR_ARRAY))
            {
                string[] tokens = nvp.Split(EQUALS_CHAR_ARRAY);
                if (tokens.Length >= 2)
                {
                    string name = tokens[0];
                    string val = tokens[1];
                    Add(name, val);
                }
            }
        }

        #region Array methods
        public void Add(string name, string val, int index)
        {
            this.Add(GetArrayName(index, name), val);
        }

        public void Remove(string arrayName, int index)
        {
            this.Remove(GetArrayName(index, arrayName));
        }

        /// <summary>
        /// 
        /// </summary>
        public string this[string name, int index]
        {
            get
            {
                return this[GetArrayName(index, name)];
            }
            set
            {
                this[GetArrayName(index, name)] = value;
            }
        }

        private static string GetArrayName(int index, string name)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", "index can not be negative : " + index);
            }
            return name + index;
        }
        #endregion
    }

    public class EdiObj
    {
        private string _fileName = string.Empty;
        private string _fileContent = string.Empty;
        private List<EdiField> _fields = new List<EdiField>();
        private LcapasLogic lcapasLogic = new LcapasLogic();

        public EdiObj(string fileName)
        {
            _fileName = fileName;
        }

        public void AddField(int fieldlength, string fieldvalue)
        {
            _fields.Add(new EdiField()
            {
                FieldValue = fieldvalue,
                FieldLength = fieldlength,
            });
        }

        public void AddLine()
        {
            _fields.Add(new EdiField()
            {
                FieldValue = Environment.NewLine,
                FieldLength = 0,
            });
        }

        public bool CreateEdiFile()
        {
            bool success = false;

            try
            {
                foreach (EdiField field in _fields)
                {
                    if (field.FieldLength > 0)
                    {
                        _fileContent += (field.FieldValue ?? string.Empty).ToString().PadRight(field.FieldLength).Substring(0, field.FieldLength);
                    }
                    else
                    {
                        _fileContent += field.FieldValue;
                    }

                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "CreateEdiFile", "Error", ex.ToString());
            }
            return success;
        }

        public bool SaveToUNC()
        {
            bool success = false;
            string UNCPath = string.Empty;
            string impdir = string.Empty;
            string env = Functions.GetEnvironment();
            switch (env)
            {
                case Structs.Environment.Test:
                    UNCPath = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCTestPath);
                    impdir = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCTestDirectory);
                    break;
                case Structs.Environment.ROTest:
                    UNCPath = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCROTestPath);
                    impdir = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCROTestDirectory);
                    break;
                case Structs.Environment.Patch:
                    UNCPath = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCPatchPath);
                    impdir = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCPatchDirectory);
                    break;
                case Structs.Environment.Dev:
                    UNCPath = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCDevPath);
                    impdir = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCDevDirectory);
                    break;
                default:  // Prod
                    UNCPath = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCProdPath);
                    impdir = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCProdDirectory);
                    break;
            }
            string domain = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LcDomain);
            string user = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LcIntegrationUserName);
            string pwd = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LcIntegrationPassword);

            string dirpath = Path.Combine(UNCPath, impdir);
            string filepath = Path.Combine(dirpath, _fileName);

            try
            {
                new UNCAccessWithCredentials.UNCAccessWithCredentials().NetUseDelete();

                using (UNCAccessWithCredentials.UNCAccessWithCredentials unc = new UNCAccessWithCredentials.UNCAccessWithCredentials())
                {
                    if (unc.NetUseWithCredentials(dirpath, user, domain, pwd))
                    {
                        CreateEdiFile();

                        Directory.CreateDirectory(dirpath);
                        File.WriteAllText(filepath, _fileContent);
                        success = true;
                    }
                    else
                    {
                        lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "saveToUNCPath", "Error", "Failed to connect to " + UNCPath + "\r\nLastError = " + unc.LastError.ToString());
                    }
                    unc.NetUseDelete();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "SaveToUNC", "Error", "Unable to write to UNCPath: " + _fileName + ". " + ex.ToString());
            }
            return success;
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public List<EdiField> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }
    }

    public class EdiField
    {

        private int _fieldLength;
        private string _fieldValue;

        public EdiField()
        {

        }

        public EdiField(int fieldlength, string fieldValue)
        {

        }

        public int FieldLength
        {
            get { return _fieldLength; }
            set { _fieldLength = value; }
        }
        public string FieldValue
        {
            get { return _fieldValue; }
            set { _fieldValue = value; }
        }
    }


    #region MyCreds Api Calls

    public class MyCredsApiCalls
    {
        #region private attributes
        
        private string _ApiUsername, _ApiPassword, _ApiHost, _DomainUrl, _EndPointUrl, _DocumentID;

        #endregion

        #region methods

        public MyCredsApiCalls()
        {
            // Loading Web Api settings
            using (LcapasLogic lcapasLogic = new LcapasLogic())
            {
                string env = Functions.GetEnvironment();
                switch (env)
                {
                    case Structs.Environment.Prod:
                        _DomainUrl = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.MyCredsProdApiDomainUrl);
                        break;
                    default:  // Test
                        _DomainUrl = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.MyCredsTestApiDomainUrl);
                        break;
                }
                _ApiUsername = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.MyCredsApiUserName);
                _ApiPassword = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.MyCredsApiPassword);
                _ApiHost = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.MyCredsApiHost);
                _EndPointUrl = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.MyCredsApiEndPointUrl);
            }
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public string CreateDocumentRequest(string jsonData)
        {
            string location = string.Empty;

            try
            {
                // Post Api call with JSON
                string url = _DomainUrl + _EndPointUrl + Structs.WebApiQueryParameters.FormatJSON;
                string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(_ApiUsername + ":" + _ApiPassword));

                using (WebClient client = new WebClient())
                {
                    client.Credentials = CredentialCache.DefaultCredentials;
                    client.Headers.Add(HttpRequestHeader.Authorization, "Basic " + encoded);
                    client.Headers.Add(HttpRequestHeader.ContentType, Structs.WebApiContentTypes.JSON);
                    client.UploadString(url, Structs.WebApiCalls.POST, jsonData);

                    location = client.ResponseHeaders["Location"];
                }

                // Retrieve DocumentID from header location
                if (!string.IsNullOrWhiteSpace(location))
                {
                    int lastIndex = location.LastIndexOf("/");
                    if (lastIndex >= 0)
                    {
                        _DocumentID = location.Substring(lastIndex + 1, location.Length - (lastIndex + 1));
                    }
                }
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "MyCredsApiCalls.CreateDocumentRequest", "Error: ", ex.ToString());
                }
            }

            return _DocumentID;
        }

        public bool UploadFileByteContent(string documentID, string fileName, byte[] fileData = null, bool uploadXML = false)
        {
            bool success = false;

            try
            {
                // Put Api call
                string url = _DomainUrl + _EndPointUrl + "/" + documentID + (uploadXML ? Structs.WebApiQueryParameters.FormatUploadXML : Structs.WebApiQueryParameters.FormatUploadPDF) + fileName;

                string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(_ApiUsername + ":" + _ApiPassword));

                // Put Api call
                using (WebClient client = new WebClient())
                {
                    client.Credentials = CredentialCache.DefaultCredentials;
                    client.Headers.Add(HttpRequestHeader.Authorization, "Basic " + encoded);
                    client.Headers.Add(HttpRequestHeader.ContentType, (uploadXML ? Structs.WebApiContentTypes.XML : Structs.WebApiContentTypes.PDF));
                    client.Headers.Add(HttpRequestHeader.Accept, Structs.WebApiContentTypes.JSON);
                    client.UploadData(url, Structs.WebApiCalls.PUT, fileData);
                }
                success = true;
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "MyCredsApiCalls.UploadFileByteContent", "Error: ", ex.ToString());
                }
            }

            return success;
        }
        #endregion

        #region public attributes

        public string ApiUsername { get { return _ApiUsername; } set { _ApiUsername = value; } }
        public string ApiPassword { get { return _ApiPassword; } set { _ApiPassword = value; } }
        public string ApiHost { get { return _ApiHost; } set { _ApiHost = value; } }
        public string DomainUrl { get { return _DomainUrl; } set { _DomainUrl = value; } }
        public string EndPointUrl { get { return _EndPointUrl; } set { _EndPointUrl = value; } }
        public string DocumentID { get { return _DocumentID; } set { _DocumentID = value; } }

        #endregion
    }

    #endregion

}