using Lcapas.Core.Library;
using Lcapas.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Lcapas.Core.Models.Lcappsdb;
using Newtonsoft.Json;
using System.Windows.Input;
using Jitbit.Utils;

namespace Lcapas.Core.Logic
{
    public class LcapasLogic : IDisposable
    {
        // Must change at each publish in web.config
        private lcapasdb_entities ctx = new lcapasdb_entities();
        private string _UserName = string.Empty;
        private DateTime _XmlCreatedDate = DateTime.Now;
        // include active schema properties and selective parsing
        private string _xlmns;

        public LcapasLogic()
        {
            _UserName = Functions.GetCurrentUser().ToString();
        }

        #region Applications

        public bool ImportPastApplications(string UUID = null)
        {
            bool success = false;

            try
            {
                // Get sent applications as part of parallel testing phase
                // these admissions applications will be of limited value because LC specific data will be missing
                //ApasLogic apasLogic = new ApasLogic();

                // Get application by UUID
                using (ApasLogic apasLogic = new ApasLogic()) {
                    List<string> appuuids = apasLogic.GetPastApplications(1, UUID);
                }
                    

                // Get applications from the last 30 days
                //List<string> appuuids = apasLogic.ImportPastApplications(30);

                // Filter by date from - to
                //string fromDate = "Jan 17 2017 12:01 pm";
                //string toDate = "Jan 18 2017 06:01 am";

                //DateTime fromDateValue, toDateValue;
                //DateTime.TryParse(fromDate, out fromDateValue);
                //DateTime.TryParse(toDate, out toDateValue);

                //List<string> appuuids = apasLogic.GetPastApplications(0, fromDate, toDate);


                // Get application information from old Submissiom database
                //appuuids = Models.Subdb.SubmissionsDAL.ImportSubmissionsData(fromDate, toDate);

                // Export Applications
                //ApplicationsFilter filters = LoadApplications(appuuids);
                //filters.ReceivedDateTime = fromDateValue;
                //filters.Export();

                //=================================================================

                //ExportStudentApplications(appuuids, toDate);

                //int hourInterval = 18;  // 18 hours - from previous day at 12:00 pm until 6:00 am
                //fromDateValue = fromDateValue.AddHours(-hourInterval);

                //// Loop through date range
                //while (fromDateValue < toDateValue)
                //{
                //    // Create interval dates
                //    fromDate = fromDateValue.ToString("MMM dd yyyy hh:mm tt");
                //    fromDateValue = fromDateValue.AddHours(hourInterval);
                //    toDate = fromDateValue.ToString("MMM dd yyyy hh:mm tt");
                //    if (hourInterval > 6) { hourInterval = 6; } else { hourInterval = 18; };

                //    // Get production application uuids (serves no other purpose)
                //    List<string> appuuids = apasLogic.GetPastApplications(0, fromDate, toDate);

                //    // parse the production applications
                //    foreach (string uuid in appuuids)
                //    {
                //        ParseApplicationMessage(uuid, toDate);
                //    }

                //    Models.Subdb.SubmissionsDAL.ImportSubmissionsData(fromDate, toDate);

                //    ExportStudentApplications(appuuids, toDate);

                //}
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ImportPastApplications", "Error: ", ex.ToString());
            }
            return success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ParseArchivedApplications()
        {
            List<string> uuids = new List<string>();
            string failedUUIDs = "";

            try
            {
                uuids = GetUnimportedMessages();

                foreach (string uuid in uuids)
                {
                    if (!ParseApplicationMessage(uuid))
                    {
                        failedUUIDs += uuid + ", ";
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "importNewApplications", "Error: ", "failed uuids:" + failedUUIDs + "; " + ex.ToString());
            }

            return failedUUIDs;
        }

        public string SaveApplications(XDocument webapps)
        {
            string result = "";

            // Save Application for DataShare
            try
            {
                // Save Application for Datashare
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveApplications", "Error: ", ex.ToString().Substring(0, 200));
            }

            return result;
        }

        public bool QueueCompletePaidUnsetApplications()
        {
            bool success = false;

            try
            {
                // Retrieve List of application to be processed
                List<string> _ApplicationUuidList = GetPaidUnsentApplications();

                foreach (string _ApplicationUuid in _ApplicationUuidList)
                {
                    // Queue each application to be mark as paid and sent to APAS
                    QueueJob(_ApplicationUuid, Enums.JobTypeType.CompletePaidUnsetApplication);
                }

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "QueueCompletePaidUnsetApplications", "Error", ex.ToString());
            }
            return success;
        }

        public List<string> GetPaidUnsentApplications()
        {
            List<string> _ApplicationList = null;

            try
            {
                _ApplicationList = (from a in ctx.ApplicationMessages
                                    where a.PayPalResponses.Any(x => x.RESULT == "0")  // Approved payment
                                    && a.ReturnedDateTime == null
                                    && a.CancelledDateTime == null
                                    && !ctx.ApplicationMessages.Any(x => x.ApplicationID == a.ApplicationID && x.PaidDateTime != null && x.ReturnedDateTime != null)  // And has no other application paid for
                                    select a).OrderByDescending(x => x.CreatedDateTime).Select(s => s.UUID).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetPaidUnsentApplications", "Error", ex.ToString());
            }
            return _ApplicationList;
        }

        public bool ResendPaidUnsentApplications(string uuid = null)
        {
            bool success = false;
            try
            {
                var applicationMessages = (from a in ctx.ApplicationMessages
                                           where a.PayPalResponses.Any(x => x.RESULT == "0")  // Approved payment
                                           && a.ReturnedDateTime == null
                                           && a.CancelledDateTime == null
                                           && !ctx.ApplicationMessages.Any(x => x.ApplicationID == a.ApplicationID && x.PaidDateTime != null && x.ReturnedDateTime != null)  // And has no other application paid for
                                           select a).OrderByDescending(x => x.CreatedDateTime).ToList();

                // Run just a single application (optional)
                if (!string.IsNullOrWhiteSpace(uuid))
                {
                    applicationMessages = applicationMessages.Where(x => x.UUID == uuid).ToList();
                }

                if (applicationMessages.Any())
                {
                    using (ApasLogic apasLogic = new ApasLogic())
                    {
                        foreach (var msg in applicationMessages)
                        {
                            if (msg.PaidDateTime == null && !ConfirmPaymentComplete(msg.UUID))  // Not previously paid
                            {
                                msg.PaidDateTime = msg.PayPalResponses.Where(x => x.RESULT == "0").OrderByDescending(y => y.ModifiedDateTime).Select(y => y.ModifiedDateTime).FirstOrDefault();
                                ctx.SaveChanges();
                            }
                            bool returned = apasLogic.SendApasApplication(msg.UUID);
                            if (returned)
                            {
                                msg.ReturnedDateTime = DateTime.Now;
                                ctx.SaveChanges();

                                // Send payment confirmation email to student
                                new SendEmail(Structs.EmailType.ApplicationPaymentConfirm, Structs.EmailReceiverGroup.Student, msg.UUID, returned);
                            }
                            else
                            {
                                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ResendPaidUnsentApplications", "Error", "Unable to resend paid application uuid: " + uuid);
                            }
                        }
                    }
                }

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ResendPaidUnsentApplications", "Error", ex.ToString());
            }
            return success;
        }

        public bool SaveExportedDateTime(List<string> uuidStrList)
        {
            bool success = false;

            try
            {
                var appMsgList = (from a in ctx.ApplicationMessages where uuidStrList.Contains(a.UUID) select a);

                foreach (ApplicationMessage am in appMsgList)
                {
                    am.ExportedDateTime = DateTime.Now;
                    am.ModifiedBy = _UserName;
                    am.ModifiedDateTime = DateTime.Now;
                }

                ctx.SaveChanges();

                success = true;

            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "saveExportedDateTime", "Error", ex.ToString());
            }
            return success;
        }

        public bool SaveReceivedDateTime(string uuid)
        {
            bool success = false;

            try
            {
                var applicationMessages = (from a in ctx.ApplicationMessages where a.UUID.Contains(uuid) select a);

                foreach (ApplicationMessage am in applicationMessages)
                {
                    am.ReceivedDateTime = DateTime.Now;
                    am.ModifiedBy = _UserName;
                    am.ModifiedDateTime = DateTime.Now;
                }

                ctx.SaveChanges();

                success = true;

            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveReceivedDateTime", "Error", ex.ToString());
            }

            return success;
        }

        public bool CreatePayPalResponse(PayPalResponse response)
        {
            bool success = false;

            try
            {
                var query = (from a in ctx.ApplicationMessages
                             where a.UUID.Contains(response.UUID)
                             select a).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();

                if (query != null)
                {
                    response.CreatedBy = _UserName;
                    response.CreatedDateTime = DateTime.Now;
                    response.ModifiedBy = _UserName;
                    response.ModifiedDateTime = DateTime.Now;
                    response.ApplicationMessage = query;
                    ctx.PayPalResponses.Add(response);
                    ctx.SaveChanges();

                    success = true;
                }

            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "createPayPalResponse", "Error", ex.ToString());
            }
            return success;
        }

        public bool ApplicationReturned(string securityToken = null)
        {
            bool success = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(securityToken))
                {
                    var query = (from r in ctx.ApplicationMessages
                                 where r.SecurityToken.Contains(securityToken)
                                 select r).FirstOrDefault();

                    if (query != null)
                    {
                        if (query.ReturnedDateTime != null)
                        {
                            success = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ApplicationReturned", "Error", ex.ToString());
            }

            return success;
        }

        public string UpdatePayPalResponse(System.Web.HttpRequestBase request)
        {
            string retval = string.Empty;
            string secureToken = request["SECURETOKEN"];
            PayPalResponse _PayPalResponse = new PayPalResponse();
            try
            {
                if (!string.IsNullOrWhiteSpace(secureToken))
                {
                    var responses = (from r in ctx.PayPalResponses
                                     where r.SECURETOKEN.ToUpper().Trim() == secureToken.ToUpper().Trim()
                                     select r);

                    if (responses.Any())
                    {
                        _PayPalResponse = responses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                    }
                    else
                    {
                        _PayPalResponse.CreatedBy = _UserName;
                        _PayPalResponse.CreatedDateTime = DateTime.Now;
                        ctx.PayPalResponses.Add(_PayPalResponse);
                    }

                    _PayPalResponse.ModifiedBy = _UserName;
                    _PayPalResponse.ModifiedDateTime = DateTime.Now;
                    //_PayPalResponse.SECURETOKEN = request["SECURETOKEN"];
                    _PayPalResponse.AUTHCODE = request["AUTHCODE"];
                    _PayPalResponse.PNREF = request["PNREF"];
                    _PayPalResponse.BILLTOCOUNTRY = request["BILLTOCOUNTRY"];
                    _PayPalResponse.BILLTOEMAIL = request["BILLTOEMAIL"];
                    _PayPalResponse.BILLTOFIRSTNAME = request["BILLTOFIRSTNAME"];
                    _PayPalResponse.BILLTOLASTNAME = request["BILLTOLASTNAME"];
                    _PayPalResponse.BILLTOPHONE = request["BILLTOPHONE"];
                    _PayPalResponse.BILLTOSTREET = request["BILLTOSTREET"];
                    _PayPalResponse.BILLTOZIP = request["BILLTOZIP"];
                    _PayPalResponse.BILLTOPHONE = request["BILLTOPHONE"];
                    _PayPalResponse.RESPMSG = request["RESPMSG"];
                    _PayPalResponse.RESULT = request["RESULT"];
                    _PayPalResponse.ACCT = request["ACCT"];  // Last 4 digts of Credit Card
                    if (!string.IsNullOrWhiteSpace(request["METHOD"]))
                    {
                        _PayPalResponse.METHOD = (Enums.PaymentMethodType)Enum.Parse(typeof(Enums.PaymentMethodType), request["METHOD"]);
                    }

                    if (_PayPalResponse.METHOD == Enums.PaymentMethodType.CC)
                    {
                        if (!string.IsNullOrWhiteSpace(request["CARDTYPE"]))
                        {
                            _PayPalResponse.CARDTYPE = (Enums.PayPalCardType)Enum.Parse(typeof(Enums.PayPalCardType), request["CARDTYPE"]);
                        }
                    }
                    else
                    {
                        _PayPalResponse.CARDTYPE = Enums.PayPalCardType.PAYMTPPL;
                    }

                    // If payment was approved by PayPal (Result = 0), mark application Message as paid
                    if (_PayPalResponse.RESULT.Trim() == "0")
                    {
                        MarkApplicationMessageAsPaid(_PayPalResponse.ApplicationMessage.UUID, _PayPalResponse.ApplicationMessage.ApplicationMessageId);
                    }

                    ctx.SaveChanges();

                    retval = _PayPalResponse.UUID;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "UpdatePayPalResponse", "Error", ex.ToString());
            }
            return retval;
        }

        public bool MarkApplicationMessageAsPaid(string uuid, int? applicationMessageId = null)
        {
            bool success = false;
            ApplicationMessage applicationMessage = null;
            try
            {
                // Search by ApplicationMessageId
                if (applicationMessageId != null)
                {
                    applicationMessage = ctx.ApplicationMessages.Where(x => x.ApplicationMessageId == applicationMessageId)
                                                                .OrderByDescending(o => o.CreatedDateTime).FirstOrDefault();
                }

                // Search by ApplicationMessage UUID
                if (applicationMessage == null && !string.IsNullOrWhiteSpace(uuid))
                {
                    applicationMessage = ctx.ApplicationMessages.Where(x => x.UUID == uuid)
                                                                .OrderByDescending(o => o.CreatedDateTime).FirstOrDefault();
                }

                // Mark ApplicationMessage as Paid
                if (applicationMessage != null && applicationMessage.PaidDateTime == null)
                {
                    applicationMessage.PaidDateTime = DateTime.Now;
                    ctx.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "UpdatePayPalResponse", "Error", ex.ToString());
            }
            return success;
        }

        public string GetErrorCode(System.Web.HttpRequestBase request)
        {
            string errorCode = string.Empty;
            string paypalResult = request["RESULT"];

            try
            {
                if (!string.IsNullOrWhiteSpace(paypalResult))
                {
                    errorCode = paypalResult;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetResultCode", "Error", ex.ToString());
            }

            return errorCode;
        }

        public string CheckPaypalError(System.Web.HttpRequestBase request)
        {
            string errorMessage = string.Empty;
            string paypalResult = request["RESULT"];

            try
            {
                if (!string.IsNullOrWhiteSpace(paypalResult))
                {
                    switch (paypalResult)
                    {
                        case Structs.PaypalErrors.UserAuthenticationFailed:
                            errorMessage = Structs.PaypalErrorMessages.UserAuthenticationFailedMessage;
                            break;
                        case Structs.PaypalErrors.TimeOut:
                            errorMessage = Structs.PaypalErrorMessages.TimeOutMessage;
                           break;
                        case Structs.PaypalErrors.Declined:
                            errorMessage = Structs.PaypalErrorMessages.DeclinedMessage;
                            break;
                        case Structs.PaypalErrors.Referral:
                            errorMessage = Structs.PaypalErrorMessages.ReferralMessage;
                            break;
                        case Structs.PaypalErrors.InvalidAccount:
                        case Structs.PaypalErrors.InvalidExpirationDate:
                            errorMessage = Structs.PaypalErrorMessages.InvalidAccountExpirationDateMessage;
                            break;
                        case Structs.PaypalErrors.InvalidTransaction:
                        case Structs.PaypalErrors.InvalidHostMapping:
                            errorMessage = Structs.PaypalErrorMessages.InvalidTransactionMessage;
                            break;
                        case Structs.PaypalErrors.DuplicateTransaction:
                            errorMessage = Structs.PaypalErrorMessages.DuplicateTransactionMessage;
                            break;
                        case Structs.PaypalErrors.SecurityCodeMismatch:
                            errorMessage = Structs.PaypalErrorMessages.SecurityCodeMismatchMessage;
                            break;
                        case Structs.PaypalErrors.SecureTokenAlreadyBeenUsed:
                        case Structs.PaypalErrors.SecureTokenExpired:
                            errorMessage = Structs.PaypalErrorMessages.SecureTokenExpiredMessage;
                            break;
                        default:
                            errorMessage = Structs.PaypalErrorMessages.UnspecifiedMessage;
                            SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "CheckPaypalError", "Unspecified Message", "RESULT: " + request["RESULT"] + " - RESPMSG: " + request["RESPMSG"] + " - PNREF: " + request["PNREF"]);
                            break;
                    }
                }

                if (paypalResult != Structs.PaypalErrors.DuplicateTransaction)
                {
                    SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "CheckPaypalError", paypalResult, "RESULT: " + request["RESULT"] + " - RESPMSG: " + request["RESPMSG"] + " - PNREF: " + request["PNREF"]);
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "CheckPaypalError", "Error", ex.ToString());
            }
            return errorMessage;
        }

        public bool SaveMessage(ApasMessage message)
        {
            bool success = false;

            try
            {
                if (!ApasMessageExists(message.UUID, message.MessageType))
                {
                    message.CreatedBy = _UserName;
                    message.CreatedDateTime = DateTime.Now;
                    ctx.ApasMessages.Add(message);
                }

                message.ModifiedBy = _UserName;
                message.ModifiedDateTime = DateTime.Now;

                ctx.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "saveMessage", "Error", ex.ToString());
            }
            return success;
        }

        public bool SaveNewAcknowledgement(Acknowledgement acknowledgement, string uuid, bool hasTransmissionData = false)
        {
            bool success = false;

            try
            {
                if (acknowledgement != null)
                {
                    acknowledgement.CreatedBy = _UserName;
                    acknowledgement.CreatedDateTime = DateTime.Now;
                    acknowledgement.ModifiedBy = _UserName;
                    acknowledgement.ModifiedDateTime = DateTime.Now;
                    if (hasTransmissionData)
                    {
                        acknowledgement.TransmissionData = GetTransmissionDataByUUID(uuid);
                    } else
                    {
                        acknowledgement.ApplicationMessage = GetApplicationMessageByUuid(uuid);
                    }

                    ctx.Acknowledgements.Add(acknowledgement);
                    ctx.SaveChanges();

                    success = acknowledgement.IsSuccessful;

                    if (!acknowledgement.IsSuccessful)
                    {
                        SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveAppAcknowledgement", "Acknowledgement Error", acknowledgement.AcknowledgementValue);
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveNewAcknowledgement", "Error", ex.ToString());
            }

            return success;
        }

        public bool UpdateStudentApplication(LandingObj landingApplication)
        {
            bool success = false;
            try
            {
                if (landingApplication != null && !string.IsNullOrWhiteSpace(landingApplication.UUID))
                {
                    var applicationMessage = ctx.ApplicationMessages.Where(x => x.UUID.Trim().ToUpper() == landingApplication.UUID.Trim().ToUpper()).OrderByDescending(b => b.ModifiedDateTime).FirstOrDefault();

                    if (applicationMessage != null)
                    {
                        applicationMessage.ModifiedBy = _UserName;
                        applicationMessage.ModifiedDateTime = DateTime.Now;
                        applicationMessage.StudentApplication.PreviouslyApplied = landingApplication.PreviouslyApplied;
                        applicationMessage.StudentApplication.StartingYear = landingApplication.StartingYear;
                        applicationMessage.StudentApplication.StudyLoad = landingApplication.FullTime;
                        applicationMessage.StudentApplication.ProgramDetail = GetProgramDetails(landingApplication.ProgramSelected, landingApplication.AppliedSession, landingApplication.Campus);
                        applicationMessage.StudentApplication.ReferenceType = GetReferenceTypeByName(landingApplication.ReferenceSource);
                        applicationMessage.StudentApplication.Student.PrevSNumber = landingApplication.PreviousStuId;

                        Disability _Disability = new Disability()
                        {
                            CreatedBy = _UserName,
                            CreatedDateTime = DateTime.Now,
                            ModifedBy = _UserName,
                            ModifiedDateTime = DateTime.Now,
                            Disabled = landingApplication.AdditionalSupport,
                        };

                        if (!ctx.Disabilities.Any(x => x.Person.PersonId == applicationMessage.StudentApplication.Student.Person.PersonId && x.Person.Disabilities.OrderByDescending(z => z.CreatedDateTime).Select(y => y.Disabled).FirstOrDefault() == landingApplication.AdditionalSupport))
                        {
                            applicationMessage.StudentApplication.Student.Person.Disabilities.Add(_Disability);
                        }

                        // Add emergency contact, if it was provided
                        SaveEmergencyContact(landingApplication);

                        ctx.SaveChanges();

                        if (applicationMessage.StudentApplication.ProgramDetail != null &&
                            applicationMessage.StudentApplication.ProgramDetail.ApplicationProgram_ApplicationProgramId == landingApplication.ProgramSelected)
                        {
                            success = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "UpdateStudentApplication", "Error", ex.ToString());
            }

            return success;
        }

        /// <summary>
        /// Add/Update Application Emergency Contact
        /// </summary>
        /// <param name="landingApplication"></param>
        /// <returns></returns>
		public bool SaveEmergencyContact(LandingObj landingApplication)
        {
            bool success = false;
            if (!string.IsNullOrWhiteSpace(landingApplication.ContactName))
            {
                ContactPerson _EmergencyContact = new ContactPerson();
                ContactPhone _EmergencyContactPhone = new ContactPhone();

                try
                {
                    // Set or update student emergency contact
                    var students = ctx.Students.Where(x => x.StudentApplications.Any(y => y.ApplicationMessages.Any(z => z.UUID.Contains(landingApplication.UUID))));

                    if (students.Any())
                    {
                        _EmergencyContact.Student = students.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();

                        // sort out and set first/last name
                        string[] fullName = Functions.SeparateNames(landingApplication.ContactName);
                        _EmergencyContact.ContactFirstName = fullName[0];
                        if (!string.IsNullOrWhiteSpace(fullName[1])) { _EmergencyContact.ContactFirstName += " " + fullName[1]; }
                        _EmergencyContact.ContactLastName = fullName[2];

                        var contactpersons = _EmergencyContact.Student.ContactPersons.Where(x => x.ContactFirstName == _EmergencyContact.ContactFirstName && x.ContactLastName == _EmergencyContact.ContactLastName);

                        // create new or use existing contact person
                        if (contactpersons.Any())
                        {
                            // use existing
                            _EmergencyContact = contactpersons.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                        }
                        else
                        {
                            // create new
                            _EmergencyContact.CreatedBy = _UserName;
                            _EmergencyContact.CreatedDateTime = DateTime.Now;
                            ctx.ContactPersons.Add(_EmergencyContact);
                        }

                        var contactphones = _EmergencyContact.ContactPhones.Where(x => x.ContactPhoneNumber == landingApplication.ContactPhone);

                        if (contactphones.Any())
                        {
                            // use existing
                            _EmergencyContactPhone = contactphones.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                        }
                        else
                        {
                            // no match - add new
                            _EmergencyContactPhone.CreatedBy = _UserName;
                            _EmergencyContactPhone.CreatedDateTime = DateTime.Now;
                            _EmergencyContactPhone.ContactPhoneNumber = landingApplication.ContactPhone;
                            _EmergencyContact.ContactPhones.Add(_EmergencyContactPhone);
                        }

                        // set modified info on emergency contact phone
                        _EmergencyContactPhone.ModifiedBy = Functions.GetCurrentUser();
                        _EmergencyContactPhone.ModifiedDateTime = DateTime.Now;

                        // set modified on emergency contact (whether new or update)
                        _EmergencyContact.ModifiedBy = _UserName;
                        _EmergencyContact.ModifiedDateTime = DateTime.Now;

                        ctx.SaveChanges();
                        success = true;
                    }

                }
                catch (Exception ex)
                {
                    SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveEmergencyContact", "Error", ex.ToString());
                }
            }
            return success;
        }

        // TODO: is this the best way to do this?
        public string GetLatestUuidByApplicationID(string applicationID)
        {
            string result = string.Empty;
            try
            {
                var query = (from s in ctx.ApplicationMessages
                             where s.StudentApplication.ApplicationID == applicationID
                             select s).OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();

                if (query != null && !string.IsNullOrWhiteSpace(query.UUID))
                {
                    result = query.UUID.TrimEnd();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetLatestUuidByApplicationID", "ApplicationID: " + applicationID + " - Error: ", ex.ToString());
            }

            return result;
        }

        public string GetPaidUuidByApplicationID(string applicationID)
        {
            string result = string.Empty;
            try
            {
                var query = (from s in ctx.ApplicationMessages
                             where s.StudentApplication.ApplicationID == applicationID
                             && s.PaidDateTime != null
                             select s).OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();

                if (query != null && !string.IsNullOrWhiteSpace(query.UUID))
                {
                    result = query.UUID.TrimEnd();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetLatestUuidByApplicationID", "ApplicationID: " + applicationID + " - Error: ", ex.ToString());
            }

            return result;
        }

        /// <summary>
        /// Cancels all ApplicationMessages for specific ApplicationID
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        public bool CancelStudentApplication(string applicationId)
        {
            bool success = false;

            try
            {
                var applicationMessages = ctx.ApplicationMessages.Where(s => s.StudentApplication.ApplicationID == applicationId && s.CancelledDateTime == null && s.ExportedDateTime == null);

                foreach (ApplicationMessage msg in applicationMessages)
                {
                    msg.CancelledDateTime = DateTime.Now;
                    msg.ModifiedBy = _UserName;
                    msg.ModifiedDateTime = DateTime.Now;
                    // TODO: Double Check this
                    //msg.ReturnedDateTime = DateTime.Now;
                }

                ctx.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "CancelStudentApplication", "Error", ex.ToString());
            }

            return success;
        }

        public bool UpdateApplication(ApplicationMessage applicationMessage)
        {
            bool success = false;

            ApplicationMessage _ApplicationMessage = new ApplicationMessage();

            try
            {
                var query = (from a in ctx.ApplicationMessages
                             where a.UUID.Trim().ToUpper() == applicationMessage.UUID.Trim().ToUpper()
                             select a);

                if (query.Any())
                {
                    _ApplicationMessage = query.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _ApplicationMessage.UUID = applicationMessage.UUID;
                    _ApplicationMessage.CreatedBy = applicationMessage.CreatedBy;  // need to setup an operator
                    _ApplicationMessage.CreatedDateTime = applicationMessage.CreatedDateTime;
                    ctx.ApplicationMessages.Add(applicationMessage);
                }

                if (applicationMessage.Administrator != _ApplicationMessage.Administrator) _ApplicationMessage.Administrator = applicationMessage.Administrator;
                if (applicationMessage.Debugging != _ApplicationMessage.Debugging) _ApplicationMessage.Debugging = applicationMessage.Debugging;
                if (applicationMessage.Production != _ApplicationMessage.Production) _ApplicationMessage.Production = applicationMessage.Production;
                if (applicationMessage.ModifiedBy != _ApplicationMessage.ModifiedBy) _ApplicationMessage.ModifiedBy = applicationMessage.ModifiedBy;
                if (applicationMessage.ModifiedDateTime != _ApplicationMessage.ModifiedDateTime) _ApplicationMessage.ModifiedDateTime = applicationMessage.ModifiedDateTime;
                if (applicationMessage.ReceivedDateTime != null && applicationMessage.ReceivedDateTime != _ApplicationMessage.ReceivedDateTime) _ApplicationMessage.ReceivedDateTime = applicationMessage.ReceivedDateTime;
                if (applicationMessage.ParsedDateTime != null && applicationMessage.ParsedDateTime != _ApplicationMessage.ParsedDateTime) _ApplicationMessage.ParsedDateTime = applicationMessage.ParsedDateTime;
                if (applicationMessage.PaidDateTime != null && applicationMessage.PaidDateTime != _ApplicationMessage.PaidDateTime) _ApplicationMessage.PaidDateTime = applicationMessage.PaidDateTime;
                if (applicationMessage.SubmittedDateTime != null && applicationMessage.SubmittedDateTime != _ApplicationMessage.SubmittedDateTime) _ApplicationMessage.SubmittedDateTime = applicationMessage.SubmittedDateTime;
                if (applicationMessage.CancelledDateTime != null && applicationMessage.CancelledDateTime != _ApplicationMessage.CancelledDateTime) _ApplicationMessage.CancelledDateTime = applicationMessage.CancelledDateTime;
                if (applicationMessage.ReturnedDateTime != null && applicationMessage.ReturnedDateTime != _ApplicationMessage.ReturnedDateTime) _ApplicationMessage.ReturnedDateTime = applicationMessage.ReturnedDateTime;
                if (applicationMessage.SecurityToken != null && applicationMessage.SecurityToken != _ApplicationMessage.SecurityToken) _ApplicationMessage.SecurityToken = applicationMessage.SecurityToken;
                if (applicationMessage.SessionId != null && applicationMessage.SessionId != _ApplicationMessage.SessionId) _ApplicationMessage.SessionId = applicationMessage.SessionId;
                if (applicationMessage.Username != _ApplicationMessage.Username && !Functions.IsUserNameEmpty(_ApplicationMessage.Username)) _ApplicationMessage.Username = applicationMessage.Username;
                if (applicationMessage.Valid != _ApplicationMessage.Valid) _ApplicationMessage.Valid = applicationMessage.Valid;

                _ApplicationMessage.ModifiedBy = _UserName;
                _ApplicationMessage.ModifiedDateTime = DateTime.Now;

                ctx.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "UpdateApplication", "Error", ex.ToString());
            }

            return success;
        }

        public ApplicationProgram AddPrograms(string programCode, string programDesc, bool active = true)
        {
            ApplicationProgram _ApplicationProgram = new ApplicationProgram();
            try
            {
                var applicationPrograms = ctx.ApplicationPrograms.Where(x => x.ProgramCode.Contains(programCode));

                if (!applicationPrograms.Any())
                {
                    _ApplicationProgram.ProgramCode = programCode;
                    _ApplicationProgram.ProgramDesc = programDesc;
                    _ApplicationProgram.Active = active;

                    _ApplicationProgram.ModifiedBy = _UserName;
                    _ApplicationProgram.ModifiedDateTime = DateTime.Now;
                    _ApplicationProgram.CreatedBy = _UserName;
                    _ApplicationProgram.CreatedDateTime = DateTime.Now;
                    ctx.ApplicationPrograms.Add(_ApplicationProgram);
                    ctx.SaveChanges();
                    return _ApplicationProgram;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "AddPrograms", "Error", ex.ToString());
            }

            return null;
        }

        public ProgramCampus AddProgramCampus(string campusCode, string campusDesc, bool active = true)
        {
            ProgramCampus _ProgramCampus = new ProgramCampus();
            try

            {
                var programCampuses = ctx.ProgramCampuses.Where(p => p.CampusCode.Contains(campusCode));

                if (!programCampuses.Any())
                {
                    _ProgramCampus.CampusCode = campusCode;
                    _ProgramCampus.CampusDesc = campusDesc;
                    _ProgramCampus.Active = active;

                    _ProgramCampus.ModifiedBy = _UserName;
                    _ProgramCampus.ModifiedDateTime = DateTime.Now;
                    _ProgramCampus.CreatedBy = _UserName;
                    _ProgramCampus.CreatedDateTime = DateTime.Now;
                    ctx.ProgramCampuses.Add(_ProgramCampus);
                    ctx.SaveChanges();
                    return _ProgramCampus;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "AddProgramCampus", "Error", ex.ToString());
            }

            return null;
        }

        public ProgramTerm AddProgramTerms(string termCode, string termDesc, bool active = true)
        {
            ProgramTerm _ProgramTerm = new ProgramTerm();
            try
            {
                var programTerms = ctx.ProgramTerms.Where(x => x.TermCode.Contains(termCode));

                if (!programTerms.Any())
                {
                    _ProgramTerm.TermCode = termCode;
                    _ProgramTerm.TermDesc = termDesc;
                    _ProgramTerm.Active = active;

                    _ProgramTerm.ModifiedBy = _UserName;
                    _ProgramTerm.ModifiedDateTime = DateTime.Now;
                    _ProgramTerm.CreatedBy = _UserName;
                    _ProgramTerm.CreatedDateTime = DateTime.Now;
                    ctx.ProgramTerms.Add(_ProgramTerm);
                    ctx.SaveChanges();
                    return _ProgramTerm;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "AddProgramTerms", "Error", ex.ToString());
            }

            return null;
        }

        public User AddUsers(string snumber,string firstname, string lastname, bool active = true)
        {
            User _User = null;
            try
            {
                var Users = ctx.Users.Where(x => x.sNumber.Contains(snumber));

                if (!Users.Any())
                {
                    _User = new User()
                    {
                        sNumber = snumber,
                        FirstName = firstname,
                        LastName = lastname,
                        FullName = firstname + " " + lastname,
                        Active = active,

                        ModifiedBy = _UserName,
                        ModifiedDateTime = DateTime.Now,
                        CreatedBy = _UserName,
                        CreatedDateTime = DateTime.Now
                    };

                    ctx.Users.Add(_User);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "AddUsers", "Error", ex.ToString());
            }

            return _User;
        }

        public ReferenceType AddReferenceType(string refName, string refDesc)
        {

            ReferenceType _ReferenceType = new ReferenceType();
            try
            {
                var refTypes = ctx.ReferenceTypes.Where(x => x.ReferenceTypeName.Contains(refName));

                if (!refTypes.Any())
                {
                    _ReferenceType.ReferenceTypeName = refName;
                    _ReferenceType.ReferenceTypeDesc = refDesc;
                    _ReferenceType.CreatedBy = _UserName;

                    _ReferenceType.CreatedDateTime = DateTime.Now;
                    _ReferenceType.ModifiedBy = _UserName;
                    _ReferenceType.ModifiedDateTime = DateTime.Now;
                    ctx.ReferenceTypes.Add(_ReferenceType);
                    ctx.SaveChanges();
                    return _ReferenceType;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "AddReferenceType", "Error", ex.ToString());
            }

            return null;
        }

        public ApplicationFee AddApplicationFee(DateTime startDateTime, double feeAmount, string message)
        {
            ApplicationFee _ApplicationFee = new ApplicationFee();
            try
            {
                var appFee = ctx.ApplicationFees.Where(x => x.StartDateTime == startDateTime && x.ApplicationFeeAmt == feeAmount);

                if (!appFee.Any())
                {
                    _ApplicationFee.StartDateTime = startDateTime;
                    _ApplicationFee.ApplicationFeeAmt = feeAmount;
                    _ApplicationFee.Message = message;

                    _ApplicationFee.CreatedBy = _UserName;
                    _ApplicationFee.CreatedDateTime = DateTime.Now;
                    _ApplicationFee.ModifiedBy = _UserName;
                    _ApplicationFee.ModifiedDateTime = DateTime.Now;

                    ctx.ApplicationFees.Add(_ApplicationFee);
                    ctx.SaveChanges();
                    
                    return _ApplicationFee;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "AddApplicationFee", "Error", ex.ToString());
            }

            return null;
        }

        public ProgramDetail AddProgramDetails(bool monthlyBased, DateTime typeStartDate, int programId, int termId, int campusId)
        {
            ProgramDetail _ProgramDetails = new ProgramDetail();
            try
            {
                var progDetails = ctx.ProgramDetails.Where(p =>
                                                           //p.MonthlyBased == monthlyBased &&
                                                           //(p.StartDate.Value.Year == typeStartDate.Year &&
                                                           // p.StartDate.Value.Month == typeStartDate.Month) &&
                                                           p.ApplicationProgram.ApplicationProgramId == programId &&
                                                           p.ProgramTerm.ProgramTermId == termId &&
                                                           p.ProgramCampus.ProgramCampusId == campusId);

                if (progDetails.Any())
                {
                    _ProgramDetails = progDetails.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _ProgramDetails.CreatedBy = _UserName;
                    _ProgramDetails.CreatedDateTime = DateTime.Now;
                    ctx.ProgramDetails.Add(_ProgramDetails);
                }

                _ProgramDetails.ModifiedBy = _UserName;
                _ProgramDetails.ModifiedDateTime = DateTime.Now;
                _ProgramDetails.MonthlyBased = monthlyBased;
                _ProgramDetails.StartDate = typeStartDate;
                if (_ProgramDetails.ProgramDetailOrder == null)
                {
                    _ProgramDetails.ProgramDetailOrder = (ctx.ProgramDetails.Where(a => a.ApplicationProgram_ApplicationProgramId == programId && a.ProgramCampus_ProgramCampusId == campusId).Max(x => x.ProgramDetailOrder) + 1) ?? 1;
                };
                _ProgramDetails.ApplicationProgram = ctx.ApplicationPrograms.Find(programId);
                _ProgramDetails.ProgramTerm = ctx.ProgramTerms.Find(termId);
                _ProgramDetails.ProgramCampus = ctx.ProgramCampuses.Find(campusId);

                ctx.SaveChanges();

            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "AddProgramDetails", "Error", ex.ToString());
            }

            return _ProgramDetails;
        }

        public bool SaveSetting(string settingName, dynamic value)
        {
            Setting _Setting = new Setting();
            SettingCategory cat = new SettingCategory();
            bool success = false;

            try
            {
                var setting = GetSetting(Structs.SettingTypes.String, settingName);

                var settingCategories = ctx.SettingCategories.Where(c => c.CategoryName.Contains("APAS"));


                if (setting != null)
                {
                    _Setting = setting;
                }
                else
                {
                    _Setting.Name = settingName;
                    _Setting.SettingCategory = settingCategories.FirstOrDefault();
                    _Setting.CreatedBy = _UserName;
                    _Setting.CreatedDateTime = DateTime.Now;
                    _Setting.ModifiedBy = _UserName;
                    _Setting.ModifiedDateTime = DateTime.Now;
                    ctx.Settings.Add(_Setting);
                }


                if (value.GetType().Equals(typeof(string)))
                {
                    ctx.ShortStringValues.Add(new ShortStringValue { Value = value, Setting = _Setting });
                }
                else if (value.GetType().Equals(typeof(int)))
                {
                    ctx.IntegerValues.Add(new IntegerValue { Value = value, Setting = _Setting });
                }
                else if (value.GetType().Equals(typeof(double)))
                {
                    ctx.DoubleValues.Add(new DoubleValue { Value = value, Setting = _Setting });
                }
                else if (value.GetType().Equals(typeof(DateTime)))
                {
                    ctx.DateTimeValues.Add(new DateTimeValue { Value = value, Setting = _Setting });
                }
                else
                {
                    _Setting = null;
                }

                ctx.SaveChanges();

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveSetting", "Error", ex.ToString());
            }
            return success;
        }

        public LandingObj GetLanding(string uuid)
        {
            LandingObj _Landing = new LandingObj();
            try
            {
                _Landing.UUID = uuid;

                var query = (from a in ctx.ApplicationMessages
                             where a.UUID.Contains(uuid)
                             && a.CancelledDateTime == null // Don't use Cancelled applications
                             && a.ExportedDateTime == null // Don't use Exported applications
                             select a).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();

                if (query != null && query.StudentApplication != null)
                {
                    _Landing.FullTime = query.StudentApplication.StudyLoad ?? string.Empty;
                    _Landing.StartingYear = query.StudentApplication.StartingYear ?? 1;
                    _Landing.PreviouslyApplied = query.StudentApplication.PreviouslyApplied ?? false;
                    _Landing.PreviousStuId = query.StudentApplication.Student.PrevSNumber ?? string.Empty;
                    if (query.StudentApplication.Student.Person.Disabilities.Any())
                    {
                        _Landing.AdditionalSupport = query.StudentApplication.Student.Person.Disabilities.OrderByDescending(x => x.CreatedDateTime).Select(y => y.Disabled).FirstOrDefault();
                    }

                    if (query.StudentApplication.Student != null)
                    {
                        if (query.StudentApplication.Student.ASNs.Any())
                        {
                            _Landing.ASN = query.StudentApplication.Student.ASNs.OrderByDescending(x => x.CreatedDateTime).Select(y => y.AgencyAssignedID).FirstOrDefault() ?? string.Empty;
                        }
                        if (query.StudentApplication.Student.Person != null)
                        {
                            if (query.StudentApplication.Student.Person.Names.Any())
                            {
                                _Landing.FirstName = query.StudentApplication.Student.Person.Names.OrderByDescending(x => x.CreatedDateTime).Select(y => y.FirstName).FirstOrDefault() ?? string.Empty;
                                _Landing.LastName = query.StudentApplication.Student.Person.Names.OrderByDescending(x => x.CreatedDateTime).Select(y => y.LastName).FirstOrDefault() ?? string.Empty;
                            }
                        }
                    }

                    if (query.StudentApplication.ProgramDetail != null)
                    {
                        if (query.StudentApplication.ProgramDetail.ApplicationProgram != null)
                        {
                            _Landing.ProgramSelected = query.StudentApplication.ProgramDetail.ApplicationProgram.ApplicationProgramId;
                        }
                        if (query.StudentApplication.ProgramDetail.ProgramTerm != null)
                        {
                            _Landing.AppliedSession = query.StudentApplication.ProgramDetail.ProgramTerm.ProgramTermId;
                        }
                        if (query.StudentApplication.ProgramDetail.ProgramCampus != null)
                        {
                            _Landing.Campus = query.StudentApplication.ProgramDetail.ProgramCampus.ProgramCampusId;
                        }
                    }

                    if (query.StudentApplication.Student.ContactPersons.Any())
                    {
                        ContactPerson _ContactPerson = query.StudentApplication.Student.ContactPersons.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();

                        _Landing.ContactName = _ContactPerson.ContactFirstName ?? string.Empty;

                        if (!string.IsNullOrWhiteSpace(_ContactPerson.ContactLastName))
                        {
                            _Landing.ContactName += " " + _ContactPerson.ContactLastName;
                        }

                        if (query.StudentApplication.Student.ContactPersons.Any(x => x.ContactPhones.Any()))
                        {
                            ContactPhone _ContactPhone = _ContactPerson.ContactPhones.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();

                            _Landing.ContactPhone = _ContactPhone.ContactPhoneNumber;
                            if (!string.IsNullOrWhiteSpace(_ContactPhone.ContactPhoneExt)) { _Landing.ContactPhone += " " + _ContactPhone.ContactPhoneExt; }
                        }

                        if (query.StudentApplication.ReferenceType != null)
                        {
                            _Landing.ReferenceSource = query.StudentApplication.ReferenceType.ReferenceTypeName ?? string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, "LandingController", "GetLanding", "Error: ", ex.ToString());
            }
            return _Landing;
        }

        public bool LoadAdmissionsApplications(AdmissionsApplicationObj applications)
        {
            bool success = false;
            try
            {
                var _ApplicationMessages = ctx.ApplicationMessages.Where(d => d.PaidDateTime != null && d.ReturnedDateTime != null).AsQueryable();

                //table1.GroupBy(x => x.Text).Select(x => x.First());
                // Get all active application
                //var _ApplicationMessages = (from a in ctx.ApplicationMessages
                //                            where a.CancelledDateTime == null
                //                            select a);

                //var _ApplicationMessages = (from am in ctx.ApplicationMessages
                //                             where am.CancelledDateTime == null
                //                             orderby am.CreatedDateTime descending
                //                             select am).GroupBy(x=>x.ApplicationID).Distinct();

                //var _ApplicationMessages = ctx.ApplicationMessages.Where(c=>c.CancelledDateTime == null).OrderByDescending(a => a.CreatedDateTime).GroupBy(b => b.ApplicationID).Distinct().ToList();

                // Filter by from date
                if (applications.ApplicationSearchFilter.FromDate != null)
                {
                    if (applications.ApplicationSearchFilter.Exported == Enums.Exported.Exported)
                    {
                        _ApplicationMessages = _ApplicationMessages.Where(a => a.ExportedDateTime >= applications.ApplicationSearchFilter.FromDate.Value);
                    }
                    else
                    {
                        _ApplicationMessages = _ApplicationMessages.Where(a => a.SubmittedDateTime >= applications.ApplicationSearchFilter.FromDate.Value);
                    }
                }

                // Filter by to date
                if (applications.ApplicationSearchFilter.ToDate != null)
                {
                    DateTime newDateTime = applications.ApplicationSearchFilter.ToDate.Value.AddDays(1).AddSeconds(-1);
                    if (applications.ApplicationSearchFilter.Exported == Enums.Exported.Exported)
                    {
                        _ApplicationMessages = _ApplicationMessages.Where(a => a.ExportedDateTime <= newDateTime);
                    }
                    else
                    {
                        _ApplicationMessages = _ApplicationMessages.Where(a => a.SubmittedDateTime <= newDateTime);
                    }
                }

                // Filter by last name
                if (!string.IsNullOrWhiteSpace(applications.ApplicationSearchFilter.LastName))
                {
                    _ApplicationMessages = _ApplicationMessages.Where(a => a.StudentApplication.Student.Person.Names.OrderByDescending(n => n.CreatedDateTime).Select(y => y.LastName).FirstOrDefault().ToUpper().Contains(applications.ApplicationSearchFilter.LastName.ToUpper()));
                }

                // Filter by first name
                if (!string.IsNullOrWhiteSpace(applications.ApplicationSearchFilter.FirstName))
                {
                    _ApplicationMessages = _ApplicationMessages.Where(a => a.StudentApplication.Student.Person.Names.OrderByDescending(n => n.CreatedDateTime).Select(y => y.FirstName).FirstOrDefault().ToUpper().Contains(applications.ApplicationSearchFilter.FirstName.ToUpper()));
                }

                // Filter by middle name
                if (!string.IsNullOrWhiteSpace(applications.ApplicationSearchFilter.MiddleName))
                {
                    _ApplicationMessages = _ApplicationMessages.Where(a => a.StudentApplication.Student.Person.Names.OrderByDescending(n => n.CreatedDateTime).Select(y => y.MiddleNames).FirstOrDefault().ToUpper().Contains(applications.ApplicationSearchFilter.MiddleName.ToUpper()));
                }

                // Filter by ASN
                if (!string.IsNullOrWhiteSpace(applications.ApplicationSearchFilter.ASN))
                {
                    _ApplicationMessages = _ApplicationMessages.Where(a => a.StudentApplication.Student.ASNs.Any(n => n.AgencyAssignedID.ToUpper().Contains(applications.ApplicationSearchFilter.ASN.ToUpper()))); 
                }

                // Filter by Gender
                if (!string.IsNullOrWhiteSpace(applications.ApplicationSearchFilter.Gender.ToString()))
                {
                    _ApplicationMessages = _ApplicationMessages.Where(a => a.StudentApplication.Student.Person.Genders.OrderByDescending(x => x.CreatedDateTime).Select(y => y.GenderCodeType).FirstOrDefault() == (Library.Apas.CoreMain.GenderCodeType)applications.ApplicationSearchFilter.Gender);
                }

                // Filter by Citizenship Status
                if (applications.ApplicationSearchFilter.CitizenshipStatus != null && applications.ApplicationSearchFilter.CitizenshipStatus.Any())
                {
                    _ApplicationMessages = _ApplicationMessages.Where(a => applications.ApplicationSearchFilter.CitizenshipStatus.Contains(a.StudentApplication.Student.Person.Citizenships.OrderByDescending(x => x.CreatedDateTime).Select(y => y.CitizenshipStatusCodeType.ToString()).FirstOrDefault()));
                }

                // Filter by Status
                //if (!string.IsNullOrWhiteSpace(applications.ApplicationSearchFilter.Status.ToString()))
                //{
                //    _ApplicationMessages = _ApplicationMessages.Where(a => a.StudentApplication.Student.Person.PreviousStatus == (Library.Apas.AdmissionsRecord.PreviousStatusCodeType)applications.ApplicationDialogFilter.Status);
                //}

                // Filter by Program
                if (!string.IsNullOrWhiteSpace(applications.ApplicationSearchFilter.Program))
                {
                    _ApplicationMessages = _ApplicationMessages.Where(a => a.StudentApplication.ProgramDetail.ApplicationProgram.ApplicationProgramId.ToString() == applications.ApplicationSearchFilter.Program);
                }

                // Filter by Term
                //if (!string.IsNullOrWhiteSpace(applications.ApplicationSearchFilter.Term))
                if (applications.ApplicationSearchFilter.Term != null && applications.ApplicationSearchFilter.Term.Any())
                {
                    //_ApplicationMessages = _ApplicationMessages.Where(a => a.StudentApplication.ProgramDetail.ProgramTerm.ProgramTermId.ToString() == applications.ApplicationSearchFilter.Term);
                    _ApplicationMessages = _ApplicationMessages.Where(a => applications.ApplicationSearchFilter.Term.Contains(a.StudentApplication.ProgramDetail.ProgramTerm.ProgramTermId.ToString()));
                }

                // Filter by Amount
                if (applications.ApplicationSearchFilter.Amount != null && applications.ApplicationSearchFilter.Amount > 0)
                {
                    _ApplicationMessages = _ApplicationMessages.Where(a => a.StudentApplication.ApplicationFee.ApplicationFeeAmt == applications.ApplicationSearchFilter.Amount);
                }

                if (applications.ApplicationSearchFilter.Exported != Enums.Exported.All)
                {
                    // Filter by Exported
                    if (applications.ApplicationSearchFilter.Exported == Enums.Exported.Exported)
                    {
                        _ApplicationMessages = _ApplicationMessages.Where(a => a.ExportedDateTime != null).OrderByDescending(b => b.ExportedDateTime);
                    }
                    else
                    {
                        _ApplicationMessages = _ApplicationMessages.Where(a => a.ExportedDateTime == null);
                    }

                    // Filter by Stopped
                    if (applications.ApplicationSearchFilter.Exported == Enums.Exported.Stopped)
                    {
                        _ApplicationMessages = _ApplicationMessages.Where(a => a.CancelledDateTime != null).OrderByDescending(b => b.CancelledDateTime);
                    }
                    //else
                    //{
                    //    _ApplicationMessages = _ApplicationMessages.Where(a => a.CancelledDateTime == null);
                    //}

                    // Don't show application marked to do not export in the new list view
                    if (applications.ApplicationSearchFilter.Exported == Enums.Exported.New)
                    {
                        _ApplicationMessages = _ApplicationMessages.Where(a => a.DontExportDateTime == null);
                    }
                }

                if (applications.Datashare)
                {
                    // U of L Applications (Nursing)
                    _ApplicationMessages = _ApplicationMessages.Where(a => a.DatashareDateTime != null);
                }

                // Order by created date and time
                //_ApplicationMessages = _ApplicationMessages.OrderByDescending(x => x.CreatedDateTime);

                // Create ApplicationSearchFilter
                List<ApplicationSearchResultsFilter> filters = new List<ApplicationSearchResultsFilter>();
                foreach (ApplicationMessage m in _ApplicationMessages.OrderByDescending(e => e.CreatedDateTime).GroupBy(z => z.ApplicationID).Select(x => x.FirstOrDefault()).ToList())
                {
                    ApplicationSearchResultsFilter filter = new ApplicationSearchResultsFilter() { Uuid = m.UUID };

                    filter.ASN = m.StudentApplication.Student.ASNs.OrderByDescending(n => n.CreatedDateTime).Select(y => y.AgencyAssignedID).FirstOrDefault().EdiASCISafe();
                    filter.CreatedDateTime = m.CreatedDateTime;
                    filter.SubmittedDateTime = m.PaidDateTime;
                    filter.ExportedDateTime = m.ExportedDateTime;
                    filter.CancelledDateTime = m.CancelledDateTime;
                    if (m.StudentApplication.Student.Person.Names.Any())
                    {
                        filter.LastName = m.StudentApplication.Student.Person.Names.Where(x => x.NameType == Structs.Name.PersonalNameType).OrderByDescending(n => n.CreatedDateTime).Select(y => y.LastName).FirstOrDefault().EdiASCISafe();
                        filter.FirstName = m.StudentApplication.Student.Person.Names.Where(x => x.NameType == Structs.Name.PersonalNameType).OrderByDescending(n => n.CreatedDateTime).Select(y => y.FirstName).FirstOrDefault().EdiASCISafe();
                        filter.MiddleName = m.StudentApplication.Student.Person.Names.Where(x => x.NameType == Structs.Name.PersonalNameType).OrderByDescending(n => n.CreatedDateTime).Select(y => y.MiddleNames).FirstOrDefault().EdiASCISafe();
                    }
                    if (m.StudentApplication.Student.Person.Emails.Any())
                    {
                        filter.Email = m.StudentApplication.Student.Person.Emails.OrderByDescending(n => n.CreatedDateTime).Select(y => y.EmailAddress).FirstOrDefault().EdiASCISafe();
                    }
                    if (m.StudentApplication.Student.Person.Genders.Any())
                    {
                        filter.Gender = m.StudentApplication.Student.Person.Genders.OrderByDescending(x => x.CreatedDateTime).Select(y => y.GenderCodeType).FirstOrDefault() ?? Library.Apas.CoreMain.GenderCodeType.Unreported;
                    }

                    // Citizenship Status (Alien Status)
                    string countryCodeType = Structs.ApasCodeTypes.Countries.EdiASCISafe();
                    string statusCodeType = Structs.ApasCodeTypes.CitizenshipStatus.EdiASCISafe();

                    if (m.StudentApplication.Student.Person.Citizenships.Any(x => x.CitizenshipStatusCodeType != Library.Apas.CoreMain.CitizenshipStatusCodeType.ResidentAlien))
                    {
                        string citCountryCode = m.StudentApplication.Student.Person.Citizenships.Where(b => b.CitizenshipStatusCodeType != Library.Apas.CoreMain.CitizenshipStatusCodeType.ResidentAlien).OrderByDescending(x => x.CreatedDateTime).Select(y => y.CountryCodeType).FirstOrDefault().EdiASCISafe();
                        string citStatusCode = m.StudentApplication.Student.Person.Citizenships.Where(b => b.CitizenshipStatusCodeType != Library.Apas.CoreMain.CitizenshipStatusCodeType.ResidentAlien).OrderByDescending(x => x.CreatedDateTime).Select(y => y.CitizenshipStatusCodeType).FirstOrDefault().EdiASCISafe();

                        //var query1 = (from v in ctx.ValCodeLookups
                        //              where v.ApasCode.ApasCodeCode.Contains(citCountryCode)
                        //                 && v.CodeType.TypeCode.Contains(countryCodeType)
                        //              select v.ValCode).FirstOrDefault();
                        //if (query1 != null)
                        //{
                        //    filter.CitizenshipCountry = query1.ValDesc.EdiNonASCISafe();
                        //}

                        var query2 = (from v in ctx.ValCodeLookups
                                      where v.ApasCode.ApasCodeCode.Contains(citStatusCode)
                                         && v.CodeType.TypeCode.Contains(statusCodeType)
                                      select v.ValCode).FirstOrDefault();

                        if (query2 != null)
                        {
                            filter.CitizenshipStatus = query2.ValDesc.EdiNonASCISafe();
                        }
                        else
                        {  // If didn't find any use "O" - Other Citizenship Status
                            query2 = (from v in ctx.ValCodeLookups
                                      where v.ApasCode.ApasCodeCode.Contains("Other")
                                         && v.CodeType.TypeCode.Contains(statusCodeType)
                                      select v.ValCode).FirstOrDefault();
                            if (query2 != null)
                            {
                                filter.CitizenshipStatus = query2.ValDesc.EdiNonASCISafe();
                            }
                        }
                    }
                    if (m.StudentApplication.ProgramDetail != null)
                    {
                        if (m.StudentApplication.ProgramDetail.ApplicationProgram != null)
                        {
                            filter.ProgramCode = m.StudentApplication.ProgramDetail.ApplicationProgram.ProgramCode.EdiASCISafe();
                            filter.Program = m.StudentApplication.ProgramDetail.ApplicationProgram.ProgramDesc.EdiASCISafe();
                        }

                        if (m.StudentApplication.ProgramDetail.ProgramTerm != null)
                        {
                            filter.Term = m.StudentApplication.ProgramDetail.ProgramTerm.TermCode.EdiASCISafe();
                        }
                    }

                    if (m.StudentApplication.ApplicationFee != null)
                    {
                        filter.Amount = m.StudentApplication.ApplicationFee.ApplicationFeeAmt;
                    }

                    filter.Count = _ApplicationMessages.Count();

                    filters.Add(filter);
                }

                applications.Pagination.RecCount = filters.Count();
                applications.Pagination.PageCount = 1;
                if (filters.Count() > 0)
                {
                    applications.ApplicationSearchResultsFilter = filters.OrderByDescending(x => x.CreatedDateTime).Skip((applications.Pagination.PageIndex - 1) * applications.Pagination.PageSize).Take(applications.Pagination.PageSize).ToList();
                    applications.Pagination.PageCount = (filters.Count() + applications.Pagination.PageSize - 1) / applications.Pagination.PageSize;
                }
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "LoadAdmissionsApplications", "Error", ex.ToString());
            }

            return success;
        }

        public List<LoginHistoryReportSearchResultsFilter> LoginHistoryQuery(LoginHistoryReportViewObj _loginHistoryReportViewObj)
        {
            List<LoginHistoryReportSearchResultsFilter> _ListLoginHistoryReportSearchResultsFilter = null;

            try
            {
                var _ReportMessages =
                            (from HS in ctx.Histories
                             join AT in ctx.ActionTypes on HS.ActionType_ActionId equals AT.ActionId
                             join US in ctx.Users on HS.User_UserId equals US.UserId

                             select new LoginHistoryReportSearchResultsFilter()
                             {
                                 HistoryID = HS.HistoryId.ToString(),
                                 UserID = US.UserId.ToString(),
                                 UserFullName = US.FullName,
                                 ActionID = AT.ActionId.ToString(),
                                 ActionDesc = AT.ActionDesc,
                                 CreatedBy = HS.CreatedBy,
                                 ModifiedBy = HS.ModifiedBy,
                                 ModifiedBydDate = HS.ModifiedDateTime,
                                 CreatedDate = HS.CreatedDateTime,
                             });

                // Filter by user ID
                if (!string.IsNullOrWhiteSpace(_loginHistoryReportViewObj.LoginHistoryReportSearchFilter.UserID))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.UserID.Trim().Contains(_loginHistoryReportViewObj.LoginHistoryReportSearchFilter.UserID));
                }

                // Filter by user full name
                if (!string.IsNullOrWhiteSpace(_loginHistoryReportViewObj.LoginHistoryReportSearchFilter.UserFullName))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.UserFullName.ToUpper().Trim().Contains(_loginHistoryReportViewObj.LoginHistoryReportSearchFilter.UserFullName.ToUpper().Trim()));
                }

                // Filter by action ID
                if (!string.IsNullOrWhiteSpace(_loginHistoryReportViewObj.LoginHistoryReportSearchFilter.ActionID))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ActionID.Trim().Contains(_loginHistoryReportViewObj.LoginHistoryReportSearchFilter.ActionID.Trim()));
                }

                // Filter by Action Description
                if (!string.IsNullOrWhiteSpace(_loginHistoryReportViewObj.LoginHistoryReportSearchFilter.ActionDesc))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ActionDesc.ToUpper().Trim().Contains(_loginHistoryReportViewObj.LoginHistoryReportSearchFilter.ActionDesc.ToUpper().Trim()));
                }

                // Filter by createdBy
                if (!string.IsNullOrWhiteSpace(_loginHistoryReportViewObj.LoginHistoryReportSearchFilter.CreatedBy))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.CreatedBy.ToUpper().Trim().Contains(_loginHistoryReportViewObj.LoginHistoryReportSearchFilter.CreatedBy.ToUpper().Trim()));
                }

                // Filter by ModifiedBy
                if (!string.IsNullOrWhiteSpace(_loginHistoryReportViewObj.LoginHistoryReportSearchFilter.ModifiedBy))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ModifiedBy.ToUpper().Trim().Contains(_loginHistoryReportViewObj.LoginHistoryReportSearchFilter.ModifiedBy.ToUpper().Trim()));
                }

                // Filter by CreatedByDate
                if (_loginHistoryReportViewObj.LoginHistoryReportSearchFilter.CreatedDate != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.CreatedDate) == DbFunctions.TruncateTime(_loginHistoryReportViewObj.LoginHistoryReportSearchFilter.CreatedDate));
                }

                // Filter by ModifiededByDate
                if (_loginHistoryReportViewObj.LoginHistoryReportSearchFilter.ModifiedBydDate != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.ModifiedBydDate) == DbFunctions.TruncateTime(_loginHistoryReportViewObj.LoginHistoryReportSearchFilter.ModifiedBydDate));
                }

                _ListLoginHistoryReportSearchResultsFilter = _ReportMessages.ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "LoginHistoryQuery", "Error", ex.ToString());
            }

            return _ListLoginHistoryReportSearchResultsFilter;
        }

        public bool LoginHistoryReports(LoginHistoryReportViewObj _loginHistoryReportViewObj)
        {
            bool success = false;
            try
            {
                var _ReportMessages = LoginHistoryQuery(_loginHistoryReportViewObj);

                _loginHistoryReportViewObj.Pagination.RecCount = _ReportMessages.Count();
                _loginHistoryReportViewObj.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    _loginHistoryReportViewObj.LoginHistoryReportSearchResultsFilter = _ReportMessages.OrderByDescending(x => x.CreatedDate).ToList().Skip((_loginHistoryReportViewObj.Pagination.PageIndex - 1) * _loginHistoryReportViewObj.Pagination.PageSize).Take(_loginHistoryReportViewObj.Pagination.PageSize).ToList();
                    _loginHistoryReportViewObj.Pagination.PageCount = (_ReportMessages.Count() + _loginHistoryReportViewObj.Pagination.PageSize - 1) / _loginHistoryReportViewObj.Pagination.PageSize;
                }
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "LoginHistoryReports", "Error", ex.ToString());
            }

            return success;
        }

        public List<ExceptionErrorsReportSearchResultsFilter> ExceptionErrorsQuery(ExceptionErrorsReportViewObj _ExceptionErrorsReportViewObj)
        {
            List<ExceptionErrorsReportSearchResultsFilter> _ListExceptionErrorsReportSearchResultsFilter = null;

            try
            {
                var _ReportMessages =
                            (from ER in ctx.ExceptionRecords

                             select new ExceptionErrorsReportSearchResultsFilter()
                             {
                                 StatusTrackingID = ER.StatusTrackingId.ToString(),
                                 Project = ER.Project,
                                 Page = ER.Page,
                                 Function = ER.Function,
                                 Value = ER.Value.Substring(0, 255),
                                 CreatedBy = ER.CreatedBy,
                                 CreatedDateTime = ER.CreatedDateTime,

                             });

                // Filter by Status Tracking ID
                if (!string.IsNullOrWhiteSpace(_ExceptionErrorsReportViewObj.ExceptionErrorsReportSearchFilter.StatusTrackingID))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.StatusTrackingID.Trim().Contains(_ExceptionErrorsReportViewObj.ExceptionErrorsReportSearchFilter.StatusTrackingID));
                }

                // Filter by Project
                if (!string.IsNullOrWhiteSpace(_ExceptionErrorsReportViewObj.ExceptionErrorsReportSearchFilter.Project))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Project.ToUpper().Trim().Contains(_ExceptionErrorsReportViewObj.ExceptionErrorsReportSearchFilter.Project.ToUpper().Trim()));
                }

                // Filter by Page
                if (!string.IsNullOrWhiteSpace(_ExceptionErrorsReportViewObj.ExceptionErrorsReportSearchFilter.Page))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Page.ToUpper().Trim().Contains(_ExceptionErrorsReportViewObj.ExceptionErrorsReportSearchFilter.Page.ToUpper().Trim()));
                }

                // Filter by Function
                if (!string.IsNullOrWhiteSpace(_ExceptionErrorsReportViewObj.ExceptionErrorsReportSearchFilter.Function))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Function.ToUpper().Trim().Contains(_ExceptionErrorsReportViewObj.ExceptionErrorsReportSearchFilter.Function.ToUpper().Trim()));
                }

                // Filter by value
                if (!string.IsNullOrWhiteSpace(_ExceptionErrorsReportViewObj.ExceptionErrorsReportSearchFilter.Value))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Value.ToUpper().Trim().Contains(_ExceptionErrorsReportViewObj.ExceptionErrorsReportSearchFilter.Value.ToUpper().Trim()));
                }

                // Filter by CreatedBy
                if (!string.IsNullOrWhiteSpace(_ExceptionErrorsReportViewObj.ExceptionErrorsReportSearchFilter.CreatedBy))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.CreatedBy.ToUpper().Trim().Contains(_ExceptionErrorsReportViewObj.ExceptionErrorsReportSearchFilter.CreatedBy.ToUpper().Trim()));
                }

                // Filter by CreatedByDate
                if (_ExceptionErrorsReportViewObj.ExceptionErrorsReportSearchFilter.CreatedDateTime != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.CreatedDateTime) == DbFunctions.TruncateTime(_ExceptionErrorsReportViewObj.ExceptionErrorsReportSearchFilter.CreatedDateTime));
                }

                _ListExceptionErrorsReportSearchResultsFilter = _ReportMessages.ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ExceptionErrorsQuery", "Error", ex.ToString());
            }

            return _ListExceptionErrorsReportSearchResultsFilter;
        }

        public bool ExceptionErrorsReports(ExceptionErrorsReportViewObj _ExceptionErrorsReportViewObj)
        {
            bool success = false;
            try
            {
                var _ReportMessages = ExceptionErrorsQuery(_ExceptionErrorsReportViewObj);

                _ExceptionErrorsReportViewObj.Pagination.RecCount = _ReportMessages.Count();
                _ExceptionErrorsReportViewObj.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    _ExceptionErrorsReportViewObj.ExceptionErrorsReportSearchResultsFilter = _ReportMessages.OrderByDescending(x => x.CreatedDateTime).ToList().Skip((_ExceptionErrorsReportViewObj.Pagination.PageIndex - 1) * _ExceptionErrorsReportViewObj.Pagination.PageSize).Take(_ExceptionErrorsReportViewObj.Pagination.PageSize).ToList();
                    _ExceptionErrorsReportViewObj.Pagination.PageCount = (_ReportMessages.Count() + _ExceptionErrorsReportViewObj.Pagination.PageSize - 1) / _ExceptionErrorsReportViewObj.Pagination.PageSize;
                }
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ExceptionErrorsReports", "Error", ex.ToString());
            }

            return success;
        }

        public List<ApplicationReportSearchResultsFilter> ApplicationReportsQuery(ApplicationReportViewObj _ApplicationReportViewObj)
        {
            List<ApplicationReportSearchResultsFilter> _ListApplicationsReportSearchResultsFilters = null;
            try
            {
                var _ReportMessages =
                            (from AR in ctx.StudentApplications
                             from STU in ctx.Students.Where(s => s.StudentId == AR.Student_StudentId).DefaultIfEmpty()
                             from PR in ctx.Persons.Where(p => p.PersonId == STU.PersonId).DefaultIfEmpty()
                             from PN in ctx.PersonNames.Where(n => n.Person_PersonId == PR.PersonId && n.NameType == Structs.Name.PersonalNameType
                                       && n.NameId == (from PN2 in ctx.PersonNames where (PN2.NameType == n.NameType && PN2.Person.PersonId == n.Person.PersonId) select PN2).Max(m => m.NameId))
                                       .DefaultIfEmpty()
                             from AD in ctx.Addresses.Where(a => a.PersonId == STU.PersonId && (a.AddressType == Structs.Address.PermanentAddressType || a.AddressType == Structs.Address.CurrentAddressType)
                                       && a.AddressId == (from AD2 in ctx.Addresses where ((AD2.AddressType == Structs.Address.PermanentAddressType || AD2.AddressType == Structs.Address.CurrentAddressType) && AD2.PersonId == a.PersonId) select AD2).Max(m => m.AddressId))
                                       .DefaultIfEmpty()
                             from PH in ctx.Phones.Where(h => h.PersonId == STU.PersonId && h.PhoneType == Enums.PhoneTypes.PermanentPhoneType
                                       && h.PhoneId == (from PH2 in ctx.Phones where (PH2.PhoneType == h.PhoneType && PH2.PersonId == h.PersonId) select PH2).Max(m => m.PhoneId))
                                       .DefaultIfEmpty()
                             from GE in ctx.Genders.Where(g => g.Person.PersonId == STU.PersonId
                                       && g.GenderId == (from GE2 in ctx.Genders where (GE2.Person.PersonId == g.Person.PersonId && GE2.Person.PersonId == g.Person.PersonId) select GE2).Max(m => m.GenderId))
                                       .DefaultIfEmpty()
                             from EM in ctx.Emails.Where(e => e.PersonId == STU.PersonId && e.EmailType == Structs.Email.PrimaryEmailType
                                       && e.EmailId == (from EM2 in ctx.Emails where (EM2.EmailType == e.EmailType && EM2.PersonId == e.PersonId) select EM2).Max(m => m.EmailId))
                                       .DefaultIfEmpty()
                             from IM in ctx.Immigrations.Where(i => i.Person.PersonId == STU.PersonId
                                       && i.ImmigrationId == (from IM2 in ctx.Immigrations where (IM2.Person.PersonId == i.Person.PersonId) select IM2).Max(m => m.ImmigrationId))
                                       .DefaultIfEmpty()
                             from SN in ctx.sNumbers.Where(s => s.Student.StudentId == STU.StudentId
                                       && s.sNumId == (from SN2 in ctx.sNumbers where (SN2.Student.StudentId == s.Student.StudentId) select SN2).Max(m => m.sNumId))
                                       .DefaultIfEmpty()
                             from PD in ctx.ProgramDetails.Where(d => d.ProgramDetailsId == AR.ProgramDetail_ProgramDetailsId).DefaultIfEmpty()
                             from AP in ctx.ApplicationPrograms.Where(p => p.ApplicationProgramId == PD.ApplicationProgram_ApplicationProgramId).DefaultIfEmpty()
                             from PT in ctx.ProgramTerms.Where(p => p.ProgramTermId == PD.ProgramTerm_ProgramTermId).DefaultIfEmpty()
                             from PC in ctx.ProgramCampuses.Where(p => p.ProgramCampusId == PD.ProgramCampus_ProgramCampusId).DefaultIfEmpty()
                             from ASN in ctx.ASNs.Where(p => p.Student_StudentId == AR.Student_StudentId
                                       && p.ASNId == (from ASN2 in ctx.ASNs where (ASN2.Student_StudentId == p.Student_StudentId) select ASN2).Max(m => m.ASNId))
                                       .DefaultIfEmpty()
                             from RT in ctx.ReferenceTypes.Where(p => p.ReferenceTypeId == AR.ReferenceType_ReferenceTypeId).DefaultIfEmpty()
                             from DA in ctx.Disabilities.Where(p => p.Person.PersonId == STU.PersonId
                                       && p.DisabilityId == (from DA2 in ctx.Disabilities where (DA2.Person.PersonId == p.Person.PersonId) select DA2).Max(m => m.DisabilityId))
                                       .DefaultIfEmpty()

                             join AM in ctx.ApplicationMessages on AR.StudentApplicationId equals AM.StudentApplication.StudentApplicationId
                             where AM.PaidDateTime != null && AM.CancelledDateTime == null && AM.ReturnedDateTime != null && AR.ProgramDetail_ProgramDetailsId != null

                             select new ApplicationReportSearchResultsFilter()
                             {
                                 FullName = PN.LastName + ", " + PN.FirstName + ", " + PN.MiddleNames,
                                 PrevSNumber = STU.PrevSNumber.Substring(0, 8),
                                 AgencyAssignedID = ASN.AgencyAssignedID,
                                 AddressLine1 = AD.AddressLine1,
                                 AddressLine2 = AD.AddressLine2,
                                 City = AD.City,
                                 Province = AD.Province.Substring(0, 3),
                                 PostalCode = AD.PostalCode,
                                 Country = AD.Country,
                                 AreaCode = PH.AreaCode,
                                 PhoneNumber = PH.PhoneNumber,
                                 Gender = (GE.GenderCodeType == Library.Apas.CoreMain.GenderCodeType.Female ? Structs.Gender.Female :
                                                         GE.GenderCodeType == Library.Apas.CoreMain.GenderCodeType.Male ? Structs.Gender.Male :
                                                         GE.GenderCodeType == Library.Apas.CoreMain.GenderCodeType.Unreported ? Structs.Gender.Unreported :
                                                         GE.GenderCodeType == Library.Apas.CoreMain.GenderCodeType.Unspecified ? Structs.Gender.Unspecified : string.Empty),

                                 BirthDate = PR.BirthDate,
                                 EthnicityRace = (PR.CanadianEthnicityRace == Library.Apas.CoreMain.CanadianEthnicityCodeType.FirstNationStatus ? Structs.EthnicityRace.FirstNationStatus :
                                                             PR.CanadianEthnicityRace == Library.Apas.CoreMain.CanadianEthnicityCodeType.FirstNationsNonStatus ? Structs.EthnicityRace.FirstNationsNonStatus :
                                                             PR.CanadianEthnicityRace == Library.Apas.CoreMain.CanadianEthnicityCodeType.Inuit ? Structs.EthnicityRace.Inuit :
                                                             PR.CanadianEthnicityRace == Library.Apas.CoreMain.CanadianEthnicityCodeType.Metis ? Structs.EthnicityRace.Metis : string.Empty),

                                 EmailAddress = EM.EmailAddress,
                                 TermCode = PT.TermCode.Substring(0, 4),
                                 StudyLoad = AR.StudyLoad,
                                 PreviouslyApplied = AR.PreviouslyApplied.ToString(),
                                 ProgramCode = AP.ProgramCode,
                                 CampusCode = PC.CampusCode.Substring(0, 4),
                                 AdmitStatus = AR.StartingYear.ToString().Substring(0, 1),
                                 LanguageCode = PR.LanguageCode.Substring(0, 5),
                                 ReferenceTypeDesc = RT.ReferenceTypeDesc,
                                 Disability = DA.Disabled.ToString().Substring(0, 5),
                                 ApplicationID = AR.ApplicationID.Substring(0, 10),
                                 FromDate = AM.CreatedDateTime,
                                 ReceivedDateTime = AM.ReceivedDateTime,
                                 PaidDateTime = AM.PaidDateTime,
                                 FirstEntryIntoCountry = IM.FirstEntryIntoCountryDateTime,
                                 FamilyAttendedCollege = STU.FamilyAttendedCollege,
                                 sNumber = SN.sNumVal
                             });


                // Filter by Status LastName
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.FullName))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.FullName.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.FullName.ToUpper().Trim()));
                }

                // Filter by PrevSNumber
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.PrevSNumber))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.PrevSNumber.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.PrevSNumber.ToUpper().Trim()));
                }

                // Filter by sNumber
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.sNumber))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.sNumber.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.sNumber.ToUpper().Trim()));
                }

                // Filter by AgencyAssignedID
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.AgencyAssignedID))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.AgencyAssignedID.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.AgencyAssignedID.ToUpper().Trim()));
                }

                // Filter by AddressLine1
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.AddressLine1))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.AddressLine1.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.AddressLine1.ToUpper().Trim()));
                }

                // Filter by AddressLine2
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.AddressLine2))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.AddressLine2.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.AddressLine2.ToUpper().Trim()));
                }

                // Filter by City
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.City))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.City.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.City.ToUpper().Trim()));
                }

                // Filter by Province
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.Province))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Province.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.Province.ToUpper().Trim()));
                }

                // Filter by PostalCode
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.PostalCode))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.PostalCode.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.PostalCode.ToUpper().Trim()));
                }

                // Filter by Country
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.Country))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Country.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.Country.ToUpper().Trim()));
                }

                // Filter by AreaCode
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.AreaCode))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.AreaCode.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.AreaCode.ToUpper().Trim()));
                }

                // Filter by PhoneNumber
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.PhoneNumber))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.PhoneNumber.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.PhoneNumber.ToUpper().Trim()));
                }

                // Filter by Gender
                if (_ApplicationReportViewObj.ApplicationReportSearchFilter.Gender != null && _ApplicationReportViewObj.ApplicationReportSearchFilter.Gender.Any())
                {
                    _ReportMessages = _ReportMessages.Where(a => _ApplicationReportViewObj.ApplicationReportSearchFilter.Gender.Contains(a.Gender.Trim().ToUpper()));
                }

                // Filter by BirthDate
                if (_ApplicationReportViewObj.ApplicationReportSearchFilter.BirthDate != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.BirthDate) == DbFunctions.TruncateTime(_ApplicationReportViewObj.ApplicationReportSearchFilter.BirthDate));
                }

                // Filter by EthnicityRace
                if (_ApplicationReportViewObj.ApplicationReportSearchFilter.EthnicityRace != null && _ApplicationReportViewObj.ApplicationReportSearchFilter.EthnicityRace.Any())
                {
                    _ReportMessages = _ReportMessages.Where(a => _ApplicationReportViewObj.ApplicationReportSearchFilter.EthnicityRace.Contains(a.EthnicityRace.Trim().ToUpper()));
                }

                // Filter by EmailAddress
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.EmailAddress))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.EmailAddress.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.EmailAddress.ToUpper().Trim()));
                }

                // Filter by TermCode
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.TermCode))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.TermCode.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.TermCode.ToUpper().Trim()));
                }

                // Filter by StudyLoad
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.StudyLoad))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.StudyLoad.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.StudyLoad.ToUpper().Trim()));
                }

                // Filter by PreviouslyApplied
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.PreviouslyApplied))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.PreviouslyApplied.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.PreviouslyApplied.ToUpper().Trim()));
                }

                // Filter by ProgramCode
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.ProgramCode))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ProgramCode.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.ProgramCode.ToUpper().Trim()));
                }

                // Filter by CampusCode
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.CampusCode))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.CampusCode.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.CampusCode.ToUpper().Trim()));
                }

                // Filter by AdmitStatus
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.AdmitStatus))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.AdmitStatus.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.AdmitStatus.ToUpper().Trim()));
                }

                // Filter by LanguageCode
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.LanguageCode))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.LanguageCode.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.LanguageCode.ToUpper().Trim()));
                }

                // Filter by LanguageListCode
                if (_ApplicationReportViewObj.ApplicationReportSearchFilter.LanguageListCode != null && _ApplicationReportViewObj.ApplicationReportSearchFilter.LanguageListCode.Any())
                {
                    _ReportMessages = _ReportMessages.Where(a => _ApplicationReportViewObj.ApplicationReportSearchFilter.LanguageListCode.Contains(a.LanguageCode.Trim().ToUpper()));
                }

                // Filter by ReferenceTypeDesc
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.ReferenceTypeDesc))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ReferenceTypeDesc.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.ReferenceTypeDesc.ToUpper().Trim()));
                }

                // Filter by Disabled
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.Disability))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Disability.ToUpper().Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.Disability.ToUpper().Trim()));
                }

                // Filter by ApplicationID
                if (!string.IsNullOrWhiteSpace(_ApplicationReportViewObj.ApplicationReportSearchFilter.ApplicationID))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ApplicationID.Trim().Contains(_ApplicationReportViewObj.ApplicationReportSearchFilter.ApplicationID.Trim()));
                }

                // Filter by FromDate
                if (_ApplicationReportViewObj.ApplicationReportSearchFilter.FromDate != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.FromDate) == DbFunctions.TruncateTime(_ApplicationReportViewObj.ApplicationReportSearchFilter.FromDate));
                }

                // Filter by ReceivedDateTime
                if (_ApplicationReportViewObj.ApplicationReportSearchFilter.ReceivedDateTime != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.ReceivedDateTime) == DbFunctions.TruncateTime(_ApplicationReportViewObj.ApplicationReportSearchFilter.ReceivedDateTime));
                }

                // Filter by PaidDateTime
                if (_ApplicationReportViewObj.ApplicationReportSearchFilter.PaidDateTime != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.PaidDateTime) == DbFunctions.TruncateTime(_ApplicationReportViewObj.ApplicationReportSearchFilter.PaidDateTime));
                }

                // Filter by FirstEntryIntoCountry
                if (_ApplicationReportViewObj.ApplicationReportSearchFilter.FirstEntryIntoCountry != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.FirstEntryIntoCountry) == DbFunctions.TruncateTime(_ApplicationReportViewObj.ApplicationReportSearchFilter.FirstEntryIntoCountry));
                }

                // Filter by FamilyAttendedCollegeListCode
                if (_ApplicationReportViewObj.ApplicationReportSearchFilter.FamilyAttendedCollegeListCode != null && _ApplicationReportViewObj.ApplicationReportSearchFilter.FamilyAttendedCollegeListCode.Any() &&
                    !_ApplicationReportViewObj.ApplicationReportSearchFilter.FamilyAttendedCollegeListCode.Contains(null))
                {
                    _ReportMessages = _ReportMessages.Where(a => _ApplicationReportViewObj.ApplicationReportSearchFilter.FamilyAttendedCollegeListCode.Contains(a.FamilyAttendedCollege));
                }

                _ListApplicationsReportSearchResultsFilters = _ReportMessages.ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ApplicationReportsQuery", "Error", ex.ToString());
            }

            return _ListApplicationsReportSearchResultsFilters;

        }

        public bool ApplicationReports(ApplicationReportViewObj _ApplicationReportViewObj)
        {
            bool success = false;
            try
            {
                var _ReportMessages = ApplicationReportsQuery(_ApplicationReportViewObj);

                _ApplicationReportViewObj.Pagination.RecCount = _ReportMessages.Count();
                _ApplicationReportViewObj.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    _ApplicationReportViewObj.ApplicationReportSearchResultsFilter = _ReportMessages.OrderByDescending(x => x.ReceivedDateTime).ThenBy(b => b.FullName).ToList().Skip((_ApplicationReportViewObj.Pagination.PageIndex - 1) * _ApplicationReportViewObj.Pagination.PageSize).Take(_ApplicationReportViewObj.Pagination.PageSize).ToList();
                    _ApplicationReportViewObj.Pagination.PageCount = (_ReportMessages.Count() + _ApplicationReportViewObj.Pagination.PageSize - 1) / _ApplicationReportViewObj.Pagination.PageSize;
                }
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ApplicationReports", "Error", ex.ToString());
            }

            return success;
        }

        public List<PaymentReportSearchResultsFilter> PaymentQuery(PaymentReportViewObj _PaymentReportViewObj)
        {
            List<PaymentReportSearchResultsFilter> _ListPaymentReportSearchResultsFilter = null;

            try
            {
                var _ReportMessages =
                            (from AR in ctx.StudentApplications
                             from STU in ctx.Students.Where(s => s.StudentId == AR.Student_StudentId).DefaultIfEmpty()
                             from PR in ctx.Persons.Where(p => p.PersonId == STU.PersonId).DefaultIfEmpty()
                             from PN in ctx.PersonNames.Where(n => n.Person_PersonId == PR.PersonId && n.NameType == "Personal"
                                                         && n.ModifiedDateTime == (from PN2 in ctx.PersonNames where (n.NameType == PN2.NameType && n.Person.PersonId == PN2.Person.PersonId) select PN2).Max(m => m.ModifiedDateTime))
                                                         .DefaultIfEmpty()
                             from AD in ctx.Addresses.Where(a => a.PersonId == STU.PersonId && a.AddressType == "PermanentAddress"
                                                        && a.ModifiedDateTime == (from AD2 in ctx.Addresses where (a.AddressType == AD2.AddressType && AD2.PersonId == a.PersonId) select AD2).Max(m => m.ModifiedDateTime))
                                                       .DefaultIfEmpty()
                             from PH in ctx.Phones.Where(h => h.PersonId == STU.PersonId && h.PhoneType == Enums.PhoneTypes.PermanentPhoneType
                                                   && h.CreatedDateTime == (from PH2 in ctx.Phones where (h.PhoneType == PH2.PhoneType && PH2.PersonId == AD.PersonId) select PH2.CreatedDateTime).FirstOrDefault())
                                                       .DefaultIfEmpty()

                             from EM in ctx.Emails.Where(e => e.PersonId == STU.PersonId && e.EmailType == "PrimaryEmail"
                                                   && e.CreatedDateTime == (from EM2 in ctx.Emails where (e.EmailType == EM2.EmailType && AD.PersonId == EM2.PersonId) select EM2.CreatedDateTime).FirstOrDefault())
                                                       .DefaultIfEmpty()

                             from PD in ctx.ProgramDetails.Where(d => d.ProgramDetailsId == AR.ProgramDetail_ProgramDetailsId).DefaultIfEmpty()
                             from AP in ctx.ApplicationPrograms.Where(p => p.ApplicationProgramId == PD.ApplicationProgram_ApplicationProgramId).DefaultIfEmpty()
                             from PT in ctx.ProgramTerms.Where(p => p.ProgramTermId == PD.ProgramTerm_ProgramTermId).DefaultIfEmpty()
                             from PC in ctx.ProgramCampuses.Where(p => p.ProgramCampusId == PD.ProgramCampus_ProgramCampusId).DefaultIfEmpty()

                             join AM in ctx.ApplicationMessages on AR.StudentApplicationId equals AM.StudentApplication.StudentApplicationId
                             where AM.PaidDateTime != null && AM.CancelledDateTime == null && AM.ReturnedDateTime != null && AR.ProgramDetail_ProgramDetailsId != null

                             from PPR in ctx.PayPalResponses.Where(p => p.ApplicationMessage_ApplicationMessageId == AM.ApplicationMessageId
                                                            && p.RESULT != null && AR.ProgramDetail_ProgramDetailsId != null)


                             select new PaymentReportSearchResultsFilter()
                             {
                                 FullName = PN.LastName + ", " + PN.FirstName + ", " + PN.MiddleNames,
                                 PrevSNumber = STU.PrevSNumber,
                                 AddressLine1 = AD.AddressLine1,
                                 AddressLine2 = AD.AddressLine2,
                                 City = AD.City,
                                 Province = AD.Province,
                                 PostalCode = AD.PostalCode,
                                 Country = AD.Country,
                                 AreaCode = PH.AreaCode,
                                 PhoneNumber = PH.PhoneNumber,
                                 EmailAddress = EM.EmailAddress,
                                 BirthDate = PR.BirthDate,
                                 TermCode = PT.TermCode,
                                 ProgramCode = AP.ProgramCode,
                                 CampusCode = PC.CampusCode,
                                 LanguageCode = PR.LanguageCode,
                                 PaypalResponseId = PPR.PayPalResponseId.ToString(),
                                 Uuid = PPR.UUID,
                                 BilltoName = PPR.BILLTOLASTNAME + " " + PPR.BILLTOFIRSTNAME,
                                 Billtoemail = PPR.BILLTOEMAIL,
                                 Billtostreet = PPR.BILLTOSTREET,
                                 Billtocountry = PPR.BILLTOCOUNTRY,
                                 Billtozip = PPR.BILLTOZIP,
                                 Billtophone = PPR.BILLTOPHONE,
                                 Cardtype = _PaymentReportViewObj.PaymentReportSearchFilter.CardType,
                                 Method = _PaymentReportViewObj.PaymentReportSearchFilter.Method,
                                 Respmsg = _PaymentReportViewObj.PaymentReportSearchFilter.RespMsg,
                                 Result = _PaymentReportViewObj.PaymentReportSearchFilter.Result,
                                 PaidDateTime = AM.PaidDateTime
                             });


                // Filter by Status LastName
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.FullName))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.FullName.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.FullName.ToUpper().Trim()));
                }

                // Filter by PrevSNumber
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.PrevSNumber))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.PrevSNumber.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.PrevSNumber.ToUpper().Trim()));
                }

                // Filter by AddressLine1
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.AddressLine1))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.AddressLine1.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.AddressLine1.ToUpper().Trim()));
                }

                // Filter by AddressLine2
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.AddressLine2))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.AddressLine2.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.AddressLine2.ToUpper().Trim()));
                }

                // Filter by City
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.City))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.City.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.City.ToUpper().Trim()));
                }

                // Filter by Province
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.Province))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Province.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.Province.ToUpper().Trim()));
                }

                // Filter by PostalCode
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.PostalCode))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.PostalCode.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.PostalCode.ToUpper().Trim()));
                }

                // Filter by Country
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.Country))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Country.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.Country.ToUpper().Trim()));
                }

                // Filter by AreaCode
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.AreaCode))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.AreaCode.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.AreaCode.ToUpper().Trim()));
                }

                // Filter by PhoneNumber
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.PhoneNumber))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.PhoneNumber.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.PhoneNumber.ToUpper().Trim()));
                }

                // Filter by BirthDate
                if (_PaymentReportViewObj.PaymentReportSearchFilter.BirthDate != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.BirthDate) == DbFunctions.TruncateTime(_PaymentReportViewObj.PaymentReportSearchFilter.BirthDate));
                }

                // Filter by EmailAddress
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.EmailAddress))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.EmailAddress.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.EmailAddress.ToUpper().Trim()));
                }

                // Filter by TermCode
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.TermCode))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.TermCode.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.TermCode.ToUpper().Trim()));
                }

                // Filter by ProgramCode
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.ProgramCode))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ProgramCode.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.ProgramCode.ToUpper().Trim()));
                }

                // Filter by CampusCode
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.CampusCode))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.CampusCode.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.CampusCode.ToUpper().Trim()));
                }

                // Filter by LanguageCode
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.LanguageCode))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.LanguageCode.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.LanguageCode.ToUpper().Trim()));
                }

                // Filter by UUID
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.Uuid))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Uuid.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.Uuid.ToUpper().Trim()));
                }
                // Filter by BILLTONAME
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.BilltoName))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.BilltoName.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.BilltoName.ToUpper().Trim()));
                }
                // Filter by BILLTOEMAIL
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.BilltoEmail))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Billtoemail.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.BilltoEmail.ToUpper().Trim()));
                }
                // Filter by BILLTOSTREET
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.BilltoStreet))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Billtostreet.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.BilltoStreet.ToUpper().Trim()));
                }
                // Filter by BILLTOZIP
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.BilltoZip))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Billtozip.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.BilltoZip.ToUpper().Trim()));
                }
                // Filter by BILLTOPHONE
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.BilltoPhone))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Billtophone.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.BilltoPhone.ToUpper().Trim()));
                }
                // Filter by CARDTYPE
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.CardType))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Cardtype.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.CardType.ToUpper().Trim()));
                }
                // Filter by METHOD
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.Method))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Method.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.Method.ToUpper().Trim()));
                }
                // Filter by RESPMSG
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.RespMsg))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Respmsg.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.RespMsg.ToUpper().Trim()));
                }
                // Filter by RESULT
                if (!string.IsNullOrWhiteSpace(_PaymentReportViewObj.PaymentReportSearchFilter.Result))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Result.ToUpper().Trim().Contains(_PaymentReportViewObj.PaymentReportSearchFilter.Result.ToUpper().Trim()));
                }

                // Filter by PaiddDateTime
                if (_PaymentReportViewObj.PaymentReportSearchFilter.PaidDateTime != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.PaidDateTime) == DbFunctions.TruncateTime(_PaymentReportViewObj.PaymentReportSearchFilter.PaidDateTime));
                }

                _ListPaymentReportSearchResultsFilter = _ReportMessages.ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "PaymentQuery", "Error", ex.ToString());
            }

            return _ListPaymentReportSearchResultsFilter;
        }

        public bool PaymentReports(PaymentReportViewObj _PaymentReportViewObj)
        {
            bool success = false;
            try
            {
                var _ReportMessages = PaymentQuery(_PaymentReportViewObj);

                _PaymentReportViewObj.Pagination.RecCount = _ReportMessages.Count();
                _PaymentReportViewObj.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    _PaymentReportViewObj.PaymentReportSearchResultsFilter = _ReportMessages.OrderByDescending(x => x.TermCode).ToList().Skip((_PaymentReportViewObj.Pagination.PageIndex - 1) * _PaymentReportViewObj.Pagination.PageSize).Take(_PaymentReportViewObj.Pagination.PageSize).ToList();
                    _PaymentReportViewObj.Pagination.PageCount = (_ReportMessages.Count() + _PaymentReportViewObj.Pagination.PageSize - 1) / _PaymentReportViewObj.Pagination.PageSize;
                }
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "PaymentReports", "Error", ex.ToString());
            }

            return success;
        }

        public List<SentEmailReportSearchResultsFilter> SentEmailQuery(SentEmailReportViewObj _SentEmailReportViewObj)
        {
            List<SentEmailReportSearchResultsFilter> _ListSentEmailReportSearchResultsFilter = null;

            try
            {
                var _ReportMessages =
                            (from EML in ctx.SentEmails

                             select new SentEmailReportSearchResultsFilter()
                             {
                                 SentEmailId = EML.SentEmailId.ToString(),
                                 EmailType = EML.EmailType,
                                 From = EML.From,
                                 To = EML.To,
                                 Subject = EML.Subject,
                                 Body = EML.Body,
                                 CreatedBy = EML.CreatedBy,
                                 CreatedDateTime = EML.CreatedDateTime,
                                 ModifiedBy = EML.ModifiedBy,
                                 ModifiedDateTime = EML.ModifiedDateTime,

                             });

                // Filter by emailType
                if (!string.IsNullOrWhiteSpace(_SentEmailReportViewObj.SentEmailReportSearchFilter.EmailType))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.EmailType.ToUpper().Trim().Contains(_SentEmailReportViewObj.SentEmailReportSearchFilter.EmailType.ToUpper().Trim())).OrderByDescending(p => p.EmailType);
                }

                // Filter by From
                if (!string.IsNullOrWhiteSpace(_SentEmailReportViewObj.SentEmailReportSearchFilter.From))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.From.ToUpper().Trim().Contains(_SentEmailReportViewObj.SentEmailReportSearchFilter.From.ToUpper().Trim())).OrderByDescending(i => i.From);
                }

                // Filter by To
                if (!string.IsNullOrWhiteSpace(_SentEmailReportViewObj.SentEmailReportSearchFilter.To))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.To.ToUpper().Trim().Contains(_SentEmailReportViewObj.SentEmailReportSearchFilter.To.ToUpper().Trim())).OrderByDescending(i => i.To);
                }

                // Filter by Subject
                if (!string.IsNullOrWhiteSpace(_SentEmailReportViewObj.SentEmailReportSearchFilter.Subject))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Subject.ToUpper().Trim().Contains(_SentEmailReportViewObj.SentEmailReportSearchFilter.Subject.ToUpper().Trim())).OrderByDescending(i => i.Subject);
                }

                // Filter by Body
                if (!string.IsNullOrWhiteSpace(_SentEmailReportViewObj.SentEmailReportSearchFilter.Body))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Body.ToUpper().Trim().Contains(_SentEmailReportViewObj.SentEmailReportSearchFilter.Body.ToUpper().Trim())).OrderByDescending(i => i.Body);
                }

                // Filter by CreatedBy
                if (!string.IsNullOrWhiteSpace(_SentEmailReportViewObj.SentEmailReportSearchFilter.CreatedBy))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.CreatedBy.ToUpper().Trim().Contains(_SentEmailReportViewObj.SentEmailReportSearchFilter.CreatedBy.ToUpper().Trim())).OrderByDescending(i => i.CreatedBy);
                }

                // Filter by CreatedDateTime
                if (_SentEmailReportViewObj.SentEmailReportSearchFilter.CreatedDateTime != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.CreatedDateTime) == DbFunctions.TruncateTime(_SentEmailReportViewObj.SentEmailReportSearchFilter.CreatedDateTime));
                }

                // Filter by ModifiedBy
                if (!string.IsNullOrWhiteSpace(_SentEmailReportViewObj.SentEmailReportSearchFilter.ModifiedBy))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ModifiedBy.ToUpper().Trim().Contains(_SentEmailReportViewObj.SentEmailReportSearchFilter.ModifiedBy.ToUpper().Trim())).OrderByDescending(i => i.ModifiedBy);
                }

                // Filter by ModifiedDateTime
                if (_SentEmailReportViewObj.SentEmailReportSearchFilter.ModifiedDateTime != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.ModifiedDateTime) == DbFunctions.TruncateTime(_SentEmailReportViewObj.SentEmailReportSearchFilter.ModifiedDateTime));
                }

                _ListSentEmailReportSearchResultsFilter = _ReportMessages.ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SentEmailQuery", "Error", ex.ToString());
            }

            return _ListSentEmailReportSearchResultsFilter;
        }

        public bool SentEmailReports(SentEmailReportViewObj _SentEmailReportViewObj)
        {
            bool success = false;
            try
            {
                var _ReportMessages = SentEmailQuery(_SentEmailReportViewObj);

                _SentEmailReportViewObj.Pagination.RecCount = _ReportMessages.Count();
                _SentEmailReportViewObj.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    _SentEmailReportViewObj.SentEmailReportSearchResultsFilter = _ReportMessages.OrderByDescending(x => x.CreatedDateTime).ToList().Skip((_SentEmailReportViewObj.Pagination.PageIndex - 1) * _SentEmailReportViewObj.Pagination.PageSize).Take(_SentEmailReportViewObj.Pagination.PageSize).ToList();
                    _SentEmailReportViewObj.Pagination.PageCount = (_ReportMessages.Count() + _SentEmailReportViewObj.Pagination.PageSize - 1) / _SentEmailReportViewObj.Pagination.PageSize;
                }
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SentEmailReports", "Error", ex.ToString());
            }

            return success;
        }

        public bool CreateProgramTerm(ApplicationProgramViewObj _ApplicationProgramViewObj)
        {

            bool success = false;
            try
            {
                var _ReportMessages =
                           (from p in ctx.ApplicationPrograms.Where(p => p.Active == true)
                                 
                            select new ApplicationProgramSearchResultsFilter()
                            {

                                ApplicationProgramId = p.ApplicationProgramId,
                                ProgramCode = p.ProgramCode,
                                ProgramDesc = p.ProgramDesc,
                                Active = p.Active,
                                CreatedBy = p.CreatedBy,
                                CreatedDateTime = p.CreatedDateTime,
                                ModifiedBy = p.ModifiedBy,
                                ModifiedDateTime = p.ModifiedDateTime,

                            });


                // Filter by ProgramCode
                if (_ApplicationProgramViewObj.ApplicationProgramSearchFilter.ProgramCode != null)
                    {
                        _ReportMessages = _ReportMessages.Where(a => a.ProgramCode == _ApplicationProgramViewObj.ApplicationProgramSearchFilter.ProgramCode);
                    }

                // Filter by ProgramDesc
                if (_ApplicationProgramViewObj.ApplicationProgramSearchFilter.ProgramDesc != null)
                    {
                        _ReportMessages = _ReportMessages.Where(a => a.ProgramDesc == _ApplicationProgramViewObj.ApplicationProgramSearchFilter.ProgramDesc);
                    }

                // Filter by CreatedBy
                if (!string.IsNullOrWhiteSpace(_ApplicationProgramViewObj.ApplicationProgramSearchFilter.CreatedBy))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.CreatedBy.ToUpper().Trim().Contains(_ApplicationProgramViewObj.ApplicationProgramSearchFilter.CreatedBy.ToUpper().Trim())).OrderByDescending(i => i.CreatedBy);
                }

                // Filter by CreatedDateTime
                if (_ApplicationProgramViewObj.ApplicationProgramSearchFilter.CreatedDateTime != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.CreatedDateTime) == DbFunctions.TruncateTime(_ApplicationProgramViewObj.ApplicationProgramSearchFilter.CreatedDateTime));
                }

                // Filter by ModifiedBy
                if (!string.IsNullOrWhiteSpace(_ApplicationProgramViewObj.ApplicationProgramSearchFilter.ModifiedBy))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ModifiedBy.ToUpper().Trim().Contains(_ApplicationProgramViewObj.ApplicationProgramSearchFilter.ModifiedBy.ToUpper().Trim())).OrderByDescending(i => i.ModifiedBy);
                }

                // Filter by ModifiedDateTime
                if (_ApplicationProgramViewObj.ApplicationProgramSearchFilter.ModifiedDateTime != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.ModifiedDateTime) == DbFunctions.TruncateTime(_ApplicationProgramViewObj.ApplicationProgramSearchFilter.ModifiedDateTime));
                }

                _ApplicationProgramViewObj.Pagination.RecCount = _ReportMessages.Count();
                _ApplicationProgramViewObj.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    _ApplicationProgramViewObj.ApplicationProgramSearchResultsFilter = _ReportMessages.OrderBy(x => x.CreatedDateTime).ToList().Skip((_ApplicationProgramViewObj.Pagination.PageIndex - 1) * _ApplicationProgramViewObj.Pagination.PageSize).Take(_ApplicationProgramViewObj.Pagination.PageSize).ToList();
                    _ApplicationProgramViewObj.Pagination.PageCount = (_ReportMessages.Count() + _ApplicationProgramViewObj.Pagination.PageSize - 1) / _ApplicationProgramViewObj.Pagination.PageSize;
                }

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "CreateProgramTerm", "Error", ex.ToString());
            }

            return success;
        }

        public bool DeactivateProgramTerm(ProgramDetailViewObj _ProgramDetailViewObj)
        {
            bool success = false;
            try
            {
                var _ReportMessages =
                            (from PD in ctx.ProgramDetails
                             from AP in ctx.ApplicationPrograms.Where(x => x.ApplicationProgramId == PD.ApplicationProgram_ApplicationProgramId)

                             select new ProgramDetailSearchResultsFilter()
                             {
                                 ProgramDetailsId = PD.ProgramDetailsId.ToString(),
                                 StartDate = PD.StartDate,
                                 MonthlyBased = PD.MonthlyBased,
                                 CreatedBy = PD.CreatedBy,
                                 Active = PD.Active.ToString(),
                                 CreatedDateTime = PD.CreatedDateTime,
                                 ModifiedBy = _UserName,
                                 ModifiedDateTime = DateTime.Now,
                                 ProgramTerm_ProgramTermId = PD.ProgramTerm_ProgramTermId.ToString(),
                                 ProgramCampus_ProgramCampusId = PD.ProgramCampus_ProgramCampusId.ToString(),
                                 ApplicationProgram_ApplicationProgramId = PD.ApplicationProgram_ApplicationProgramId.ToString(),
                                 ApplicationProgramDescription = AP.ProgramDesc + "(" + AP.ProgramCode + ")"

                             });

                // Filter by StartedDate
                if (_ProgramDetailViewObj.ProgramDetailSearchFilter.StartDate != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.StartDate) == DbFunctions.TruncateTime(_ProgramDetailViewObj.ProgramDetailSearchFilter.StartDate));
                }

                // Filter by MonthlyBased
                if (_ProgramDetailViewObj.ProgramDetailSearchFilter.MonthlyBased)
                {
                    _ReportMessages = _ReportMessages.Where(a => a.MonthlyBased ==_ProgramDetailViewObj.ProgramDetailSearchFilter.MonthlyBased);
                }

                // Filter by CreatedBy
                if (!string.IsNullOrWhiteSpace(_ProgramDetailViewObj.ProgramDetailSearchFilter.CreatedBy))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.CreatedBy.ToUpper().Trim().Contains(_ProgramDetailViewObj.ProgramDetailSearchFilter.CreatedBy.ToUpper().Trim())).OrderByDescending(i => i.CreatedBy);
                }

                // Filter by CreatedDateTime
                if (_ProgramDetailViewObj.ProgramDetailSearchFilter.CreatedDateTime != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.CreatedDateTime) == DbFunctions.TruncateTime(_ProgramDetailViewObj.ProgramDetailSearchFilter.CreatedDateTime));
                }

                // Filter by ModifiedBy
                if (!string.IsNullOrWhiteSpace(_ProgramDetailViewObj.ProgramDetailSearchFilter.ModifiedBy))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ModifiedBy.ToUpper().Trim().Contains(_ProgramDetailViewObj.ProgramDetailSearchFilter.ModifiedBy.ToUpper().Trim())).OrderByDescending(i => i.ModifiedBy);
                }

                // Filter by ModifiedDateTime
                if (_ProgramDetailViewObj.ProgramDetailSearchFilter.ModifiedDateTime != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.ModifiedDateTime) == DbFunctions.TruncateTime(_ProgramDetailViewObj.ProgramDetailSearchFilter.ModifiedDateTime));
                }

                // Filter by ProgramTermId
                if (!string.IsNullOrWhiteSpace(_ProgramDetailViewObj.ProgramDetailSearchFilter.ProgramTerm_ProgramTermId))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ProgramTerm_ProgramTermId.ToUpper().Trim().Contains(_ProgramDetailViewObj.ProgramDetailSearchFilter.ProgramTerm_ProgramTermId.ToUpper().Trim())).OrderByDescending(i => i.ProgramTerm_ProgramTermId);
                }

                // Filter by ProgramCampusId
                if (!string.IsNullOrWhiteSpace(_ProgramDetailViewObj.ProgramDetailSearchFilter.ProgramCampus_ProgramCampusId))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ProgramCampus_ProgramCampusId.ToUpper().Trim().Contains(_ProgramDetailViewObj.ProgramDetailSearchFilter.ProgramCampus_ProgramCampusId.ToUpper().Trim())).OrderByDescending(i => i.ProgramCampus_ProgramCampusId);
                }

                //Filter by Active
                if (!string.IsNullOrWhiteSpace(_ProgramDetailViewObj.ProgramDetailSearchFilter.Active))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Active == _ProgramDetailViewObj.ProgramDetailSearchFilter.Active);
                }

                // Filter by Application Program Description
                if (!string.IsNullOrWhiteSpace(_ProgramDetailViewObj.ProgramDetailSearchFilter.ApplicationProgramDescription))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ApplicationProgramDescription.ToUpper().Trim().Contains(_ProgramDetailViewObj.ProgramDetailSearchFilter.ApplicationProgramDescription.ToUpper().Trim())).OrderByDescending(i => i.ApplicationProgramDescription);
                }

                _ProgramDetailViewObj.Pagination.RecCount = _ReportMessages.Count();
                _ProgramDetailViewObj.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    _ProgramDetailViewObj.ProgramDetailSearchResultsFilter = _ReportMessages.OrderBy(x => x.ApplicationProgramDescription).ToList().Skip((_ProgramDetailViewObj.Pagination.PageIndex - 1) * _ProgramDetailViewObj.Pagination.PageSize).Take(_ProgramDetailViewObj.Pagination.PageSize).ToList();
                    _ProgramDetailViewObj.Pagination.PageCount = (_ReportMessages.Count() + _ProgramDetailViewObj.Pagination.PageSize - 1) / _ProgramDetailViewObj.Pagination.PageSize;
                }

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "DeactivateProgramTerm", "Error", ex.ToString());
            }

            return success;
        }

        public bool DeleteKeyValueTempCache()
        {
            bool success = false;

            try
            {
                // Delete all data that it is a day old
                DateTime _DayOld = DateTime.Now.AddDays(-1);
                var _KeyValueTempCaches = (from a in ctx.KeyValueTempCaches
                                           where a.CreatedDateTime <= _DayOld
                                           select a);

                ctx.KeyValueTempCaches.RemoveRange(_KeyValueTempCaches);
                ctx.SaveChanges();

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "DeleteKeyValueTempCache", "Error: ", ex.ToString().Substring(0, 200));
            }

            return success;
        }

        public string SaveKeyValueTempCache(string[] values)
        {
            string key = null;

            try
            {
                if (values.Length != 0)
                {
                    key = Guid.NewGuid().ToString();
                    foreach (string item in values)
                    {
                        KeyValueTempCache _KeyValueTempCache = new KeyValueTempCache()
                        {
                            Key = key,
                            Value = item,
                            CreatedBy = _UserName,
                            CreatedDateTime = DateTime.Now,
                            ModifiedBy = _UserName,
                            ModifiedDateTime = DateTime.Now
                        };

                        ctx.KeyValueTempCaches.Add(_KeyValueTempCache);
                    }
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveKeyValueTempCache", "Error: ", ex.ToString().Substring(0, 200));
            }

            return key;
        }

        public List<KeyValueTempCache> GetKeyValueTempCache(string key)
        {
            List<KeyValueTempCache> _KeyValueTempCacheList = new List<KeyValueTempCache>();

            try
            {
                _KeyValueTempCacheList = ctx.KeyValueTempCaches.Where(x => x.Key == key).ToList();

                // Delete key/values
                DeleteKeyValueTempCache();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetKeyValueTempCache", "Error: ", ex.ToString().Substring(0, 200));
            }

            return _KeyValueTempCacheList;
        }

        public List<SelectListItem> GetStartingYears()
        {
            List<SelectListItem> _StartingYears = new List<SelectListItem>();

            try
            {
                for (int i = 1; i <= 4; i++)
                {
                    _StartingYears.Add(new SelectListItem { Value = i.ToString(), Text = "Start Year " + i.ToString() });
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "getStartingYears", "Error: ", ex.ToString());
            }
            return _StartingYears;
        }

        public List<SelectListItem> GetFullpart()
        {
            List<SelectListItem> _FullPart = new List<SelectListItem>();

            try
            {
                for (int i = 1; i <= 2; i++)
                {
                    _FullPart.Add(new SelectListItem { Value = (i == 1) ? "F" : "P", Text = (i == 1) ? "F" : "P" });
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetFullpart", "Error: ", ex.ToString());
            }
            return _FullPart;
        }

        public List<SelectListItem> GetTrueFalse()
        {
            List<SelectListItem> _TrueFalse = new List<SelectListItem>();

            try
            {
                for (int i = 1; i <= 2; i++)
                {
                    _TrueFalse.Add(new SelectListItem { Value = (i == 1) ? "True" : "False", Text = (i == 1) ? "True" : "False" });
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetTrueFalse", "Error: ", ex.ToString());
            }
            return _TrueFalse;
        }

        public List<SelectListItem> GetEthnicityRace()
        {
            List<SelectListItem> _EthnicityRace = new List<SelectListItem>();

            try
            {
                foreach (var Ethnic in typeof(Structs.EthnicityRace).GetFields())
                {
                    _EthnicityRace.Add(new SelectListItem { Text = Ethnic.Name, Value = Ethnic.GetValue(null).ToString() });
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetEthnicityRace", "Error: ", ex.ToString());
            }
            return _EthnicityRace.OrderBy(o => o.Text).ToList();
        }

        public List<SelectListItem> GetFamilyAttendedCollege()
        {
            List<SelectListItem> _FamilyAttendedCollege = new List<SelectListItem>();

            try
            {
                foreach (var FamilyAttendedCollege in Enum.GetValues(typeof(Library.Apas.CoreMain.AttendedCollegeCodeType)))
                {
                    _FamilyAttendedCollege.Add(new SelectListItem { Text = Enum.GetName(typeof(Library.Apas.CoreMain.AttendedCollegeCodeType), FamilyAttendedCollege),
                                                                    Value = FamilyAttendedCollege.ToString() });
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetFamilyAttendedCollege", "Error: ", ex.ToString());
            }
            return _FamilyAttendedCollege.OrderBy(o => o.Text).ToList();
            ;
        }

        public List<SelectListItem> GetGender()
        {
            List<SelectListItem> _Gender = new List<SelectListItem>();

            try
            {
                foreach (var Gender in typeof(Structs.Gender).GetFields())
                {
                    _Gender.Add(new SelectListItem { Text = Gender.Name, Value = Gender.GetValue(null).ToString() });
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetGender", "Error: ", ex.ToString());
            }
            return _Gender.OrderBy(o => o.Text).ToList();
        }

        public List<SelectListItem> GetCitizenshipStatus()
        {
            List<SelectListItem> _CitizenshipStatus = new List<SelectListItem>();

            try
            {
                foreach (Enums.CitizenshipStatus CitizenshipStatus in Enum.GetValues(typeof(Enums.CitizenshipStatus)))
                {
                    _CitizenshipStatus.Add(new SelectListItem
                    {
                        Text = CitizenshipStatus.GetDisplayName(),
                        Value = CitizenshipStatus.ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetCitizenshipStatus", "Error: ", ex.ToString());
            }
            return _CitizenshipStatus.OrderBy(o => o.Text).ToList();
        }


        public List<String> GetBatchDates()
        {
            List<String> batches = new List<String>();
            List<DateTime> aa = new List<DateTime>();

            try
            {
                aa = (from a in ctx.StudentApplications
                      select a.CreatedDateTime).OrderByDescending(d => d).ToList<DateTime>();

                batches = aa.Select(b => b.Date).Distinct().Select(v => v.ToString("yyyy/MM/dd")).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetBatchDates", "Error", ex.ToString());
            }

            return batches;
        }

        public bool GetPermissions(UserAccessObj _UserAccess)
        {
            bool success = false;

            try
            {
                var results = (from u in ctx.Users
                               where u.sNumber.Contains(_UserAccess.UserId)
                               select new
                               {
                                   AccessGroups = (from p in ctx.AccessGroups
                                                   where p.Users.Any(z => z.sNumber.Contains(u.sNumber))
                                                   select p.AccessGroupType).ToList(),
                                   Controllers = (from c in ctx.ControllerTypes
                                                  where c.ActionTypes.Any(x => x.PermissionRecords.Any(y => y.AccessGroup.Users.Any(z => z.sNumber.Contains(u.sNumber) && y.Enabled == true)))
                                                  select new ControllerTypeType()
                                                  {
                                                      ControllerType = c.ControllerTypeType,
                                                      ControllerDesc = c.ControllerDesc,
                                                      Actions = (from a in c.ActionTypes
                                                                 where a.PermissionRecords.Any(y => y.AccessGroup.Users.Any(z => z.sNumber.Contains(u.sNumber) && y.Enabled == true))
                                                                 select new ActionTypeType()
                                                                 {
                                                                     ActionType = a.ActionTypeType,
                                                                     ActionDesc = a.ActionDesc,
                                                                 }).ToList()
                                                  }).ToList(),
                               }).FirstOrDefault();

                if (results != null)
                {
                    _UserAccess.IsInitialized = true;
                    _UserAccess.AccessGroups = results.AccessGroups;
                    _UserAccess.Controllers = results.Controllers;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetPermissions", "Error", ex.ToString());
            }

            return success;
        }

        public string GetValCode(string apasCode, string codeType)
        {
            string valCode = "";
            try
            {
                valCode = (from v in ctx.ValCodeLookups
                           where v.ApasCode.ApasCodeCode.Contains(apasCode) && v.CodeType.TypeCode.Contains(codeType)
                           select v.ValCode.ValCodeCode).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.Lcapas, "getAccessLevel", "Error", ex.ToString());
            }

            return valCode;
        }

        public List<string> GetInstitutionsByUUID(string uuid, bool fromAlberta)

        {
            List<string> institutionList = new List<string>();
            try
            {
                var query = (from n in ctx.InstitutionNames
                             where n.Institution.AcademicRecords.Any(a => a.Student.StudentApplications.Any(b => b.ApplicationMessages.Any(r => r.UUID.Contains(uuid))))
                             select n);

                if (query != null)
                {
                    if (fromAlberta)
                    {
                        institutionList = query.Where(i => i.Institution.Addresses.OrderByDescending(o => o.CreatedDateTime).Select(n => n.Province).FirstOrDefault() == "AB").Select(t => t.Name).Distinct().ToList();
                    }
                    else
                    {
                        institutionList = query.Where(i => i.Institution.Addresses.OrderByDescending(o => o.CreatedDateTime).Select(n => n.Province).FirstOrDefault() != "AB").Select(t => t.Name).Distinct().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetInstitutionsByUUID", "Error", ex.ToString());
            }

            return institutionList;
        }

        public StudentApplication GetStudentApplication(string uuid)
        {
            StudentApplication _StudentApplication = new StudentApplication();
            try
            {
                var query = (from a in ctx.ApplicationMessages
                             where a.UUID.Contains(uuid)
                             && a.CancelledDateTime == null
                             && a.ExportedDateTime == null
                             select a).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();

                if (query != null)
                {
                    _StudentApplication = query.StudentApplication;
                }
                else
                {
                    _StudentApplication = null;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "getStudentApplicationByUUID", "Error", ex.ToString());
            }

            return _StudentApplication;
        }

        public StudentApplication GetStudentApplicationByApplicationID(string applicationID)
        {
            StudentApplication _StudentApplication = new StudentApplication();
            try
            {
                var query = (from a in ctx.ApplicationMessages
                             where a.StudentApplication.ApplicationID == applicationID
                             select a).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();

                if (query != null)
                {
                    _StudentApplication = query.StudentApplication;
                }
                else
                {
                    _StudentApplication = null;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetStudentApplicationByApplicationID", "Error", ex.ToString());
            }

            return _StudentApplication;
        }

        public List<Institution> GetInstitutions()
        {
            List<Institution> _Institutions = new List<Institution>();
            try
            {
                _Institutions = (from i in ctx.Institutions
                                 select i).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetInstitutions", "Error", ex.ToString());
            }


            return _Institutions;
        }

        public List<ApplicationProgram> GetApplicationPrograms(bool onlyActive = true, bool hasActiveRelationship = true)
        {
            List<ApplicationProgram> _Programs = new List<ApplicationProgram>();
            try
            {
                var query = (from p in ctx.ApplicationPrograms
                             select p);

                if (onlyActive)
                {
                    query = query.Where(p => p.Active == true);
                }

                if (hasActiveRelationship)
                {
                    query = query.Where(p => p.ProgramDetails.Any(d => d.Active == true && d.ApplicationProgram.Active == true && d.ProgramTerm.Active == true && d.ProgramCampus.Active == true));
                }

                _Programs = query.OrderBy(o => o.ProgramOrder).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetApplicationPrograms", "Error", ex.ToString());
            }
            return _Programs;
        }

        public bool GetApplicationProgramsForProgramTerm(ApplicationProgramViewObj _ApplicationProgramViewObj)
        {
            bool success = false;
            try
            {
                var _ReportMessages = (from p in ctx.ApplicationPrograms.Where(c => c.Active == true)
                                       select new ApplicationProgramSearchResultsFilter() {
                                            ApplicationProgramId = p.ApplicationProgramId,
                                            ProgramCode = p.ProgramCode,
                                            ProgramDesc = p.ProgramDesc,
                                            Active = p.Active,
                                            CreatedBy = p.CreatedBy,
                                            CreatedDateTime = p.CreatedDateTime,
                                            ModifiedBy = p.ModifiedBy,
                                            ModifiedDateTime = p.ModifiedDateTime,
                                        });

               
                // Filter by ProgramCode
                if (!string.IsNullOrWhiteSpace(_ApplicationProgramViewObj.ApplicationProgramSearchFilter.ProgramCode))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ProgramCode.ToUpper().Trim().Contains(_ApplicationProgramViewObj.ApplicationProgramSearchFilter.ProgramCode.ToUpper().Trim())).OrderByDescending(i => i.ProgramCode);
                }

                // Filter by ProgramDesc
                if (!string.IsNullOrWhiteSpace(_ApplicationProgramViewObj.ApplicationProgramSearchFilter.ProgramDesc))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ProgramDesc.ToUpper().Trim().Contains(_ApplicationProgramViewObj.ApplicationProgramSearchFilter.ProgramDesc.ToUpper().Trim())).OrderByDescending(i => i.ProgramDesc);
                }
                // Filter by CreatedBy
                if (!string.IsNullOrWhiteSpace(_ApplicationProgramViewObj.ApplicationProgramSearchFilter.CreatedBy))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.CreatedBy.ToUpper().Trim().Contains(_ApplicationProgramViewObj.ApplicationProgramSearchFilter.CreatedBy.ToUpper().Trim())).OrderByDescending(i => i.CreatedBy);
                }

                // Filter by CreatedDateTime
                if (_ApplicationProgramViewObj.ApplicationProgramSearchFilter.CreatedDateTime != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.CreatedDateTime) == DbFunctions.TruncateTime(_ApplicationProgramViewObj.ApplicationProgramSearchFilter.CreatedDateTime));
                }

                // Filter by ModifiedBy
                if (!string.IsNullOrWhiteSpace(_ApplicationProgramViewObj.ApplicationProgramSearchFilter.ModifiedBy))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ModifiedBy.ToUpper().Trim().Contains(_ApplicationProgramViewObj.ApplicationProgramSearchFilter.ModifiedBy.ToUpper().Trim())).OrderByDescending(i => i.ModifiedBy);
                }

                // Filter by ModifiedDateTime
                if (_ApplicationProgramViewObj.ApplicationProgramSearchFilter.ModifiedDateTime != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.ModifiedDateTime) == DbFunctions.TruncateTime(_ApplicationProgramViewObj.ApplicationProgramSearchFilter.ModifiedDateTime));
                }

                _ApplicationProgramViewObj.Pagination.RecCount = _ReportMessages.Count();
                _ApplicationProgramViewObj.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    _ApplicationProgramViewObj.ApplicationProgramSearchResultsFilter = _ReportMessages.OrderBy(x => x.ProgramCode).ToList().Skip((_ApplicationProgramViewObj.Pagination.PageIndex - 1) * _ApplicationProgramViewObj.Pagination.PageSize).Take(_ApplicationProgramViewObj.Pagination.PageSize).ToList();
                    _ApplicationProgramViewObj.Pagination.PageCount = (_ReportMessages.Count() + _ApplicationProgramViewObj.Pagination.PageSize - 1) / _ApplicationProgramViewObj.Pagination.PageSize;
                }

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetApplicationProgramsForProgramTerm", "Error", ex.ToString());
            }

            return success;
        }

        public ProgramDetail GetProgramDetails(Int32 programId, Int32 termId, Int32 campusId)
        {
            ProgramDetail _ProgramDetails = new ProgramDetail();
            try
            {
                _ProgramDetails = (from t in ctx.ProgramDetails
                                   where (t.ApplicationProgram.ApplicationProgramId == programId)
                                   && (t.ProgramTerm.ProgramTermId == termId)
                                   && (t.ProgramCampus.ProgramCampusId == campusId)
                                   && (t.Active == true)
                                   select t).OrderByDescending(d => d.StartDate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "getProgramDetails", "Error", ex.ToString());
            }
            return _ProgramDetails;
        }

    
    public ProgramDetail GetProgramDetailsByCampusTerm(int CampusID, int TermID)
    {
        ProgramDetail _ProgramDetails = new ProgramDetail();
        try
        {
            _ProgramDetails = (from t in ctx.ProgramDetails
                               where (t.ProgramTerm.ProgramTermId == TermID)
                               && (t.ProgramCampus.ProgramCampusId == CampusID)
                               select t).OrderByDescending(d => d.StartDate).FirstOrDefault();
        }
        catch (Exception ex)
        {
            SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetProgramDetailsByCampusTerm", "Error", ex.ToString());
        }
        return _ProgramDetails;
    }


    public List<ProgTermCampObj> GetProgramDetailsByProgramId(string programId)
        {
            List<ProgTermCampObj> _ProgramTermCampus = new List<ProgTermCampObj>();
            try
            {
                if (!string.IsNullOrWhiteSpace(programId))
                {
                    Int32 intProgramId;

                    if (Int32.TryParse(programId, out intProgramId))
                    {
                        var _ProgramDetails = (from t in ctx.ProgramDetails
                                               where t.ApplicationProgram.ApplicationProgramId == intProgramId
                                               && t.Active == true
                                               select t);

                        _ProgramTermCampus = _ProgramDetails.Select(d => new ProgTermCampObj
                        {
                            Program = d.ApplicationProgram.ApplicationProgramId,
                            Term = d.ProgramTerm.ProgramTermId,
                            TermCode = d.ProgramTerm.TermCode,
                            TermDesc = d.ProgramTerm.TermDesc,
                            Campus = d.ProgramCampus.ProgramCampusId,
                            Order = d.ProgramDetailOrder,
                            Active = d.Active
                        }).ToList();
                    }
                    else
                    {
                        SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetProgramDetailsByProgramId", "Error: ", "Invalid Program Id: " + programId);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetProgramDetailsByProgramId", "Error: ", ex.ToString());
            }
            return _ProgramTermCampus;
        }

        public List<ProgramTerm> GetTerms(bool onlyActive = true)
        {
            List<ProgramTerm> _ProgramTerms = new List<ProgramTerm>();
            try
            {
                var query = (from p in ctx.ProgramTerms
                             select p);

                if (onlyActive)
                {
                    query = query.Where(p => p.Active == true);
                }

                _ProgramTerms = query.OrderBy(o => o.TermOrder).ToList();

            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetTerms", "Error", ex.ToString());
            }
            return _ProgramTerms;
        }

        public List<SelectListItem> GetLanguages()
        {
            List<SelectListItem> _Languages = new List<SelectListItem>();
            try
            {
                var query = (from p in ctx.Persons
                             where p.LanguageCode != null
                             select p.LanguageCode).Distinct().Select(s => new SelectListItem() { Text = s, Value = s });

                _Languages = query.ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetLanguages", "Error", ex.ToString());
            }
            return _Languages;
        }

        public List<User> GetUsers(bool onlyActive = true)
        {
            List<User> _Users = new List<User>();
            try
            {
                var query = (from p in ctx.Users
                             select p);

                if (onlyActive)
                {
                    query = query.Where(p => p.Active == true);
                }

                _Users = query.OrderBy(o => o.FullName).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "Users", "Error", ex.ToString());
            }
            return _Users;
        }

        public List<AccessGroup> GetAccessGroup()
        {
            List<AccessGroup> _AccessGroup = new List<AccessGroup>();

            try
            {
                _AccessGroup = (from p in ctx.AccessGroups
                             select p).OrderBy(x => x.AccessGroupId).ToList();                
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetAccessGroup", "Error", ex.ToString());
            }
            return _AccessGroup;
        }

        public List<ActionType> GetActionTypes()
        {
            List<ActionType> _ActionTypes = new List<ActionType>();

            try
            {
                _ActionTypes = (from p in ctx.ActionTypes select p).OrderBy(x => x.ActionId).ToList();
                //_ActionTypes = _ActionTypes.Where(p => p.ExternalAction == true).OrderBy(x => x.ActionId).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetActionTypes", "Error", ex.ToString());
            }
            return _ActionTypes;
        }

        public List<ControllerType> GetControllerTypes()
        {
            List<ControllerType> _ControllerTypes = new List<ControllerType>();

            try
            {
                _ControllerTypes = ctx.ControllerTypes.OrderBy(x => x.ControllerDesc).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetControllerTypes", "Error", ex.ToString());
            }
            return _ControllerTypes;
        }

        public List<PermissionRecord> GetPermissionRecords()
        {
            List<PermissionRecord> _PermissionRecords = new List<PermissionRecord>();

            try
            {
                _PermissionRecords = ctx.PermissionRecords.OrderBy(x => x.PermissionRecordNote).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetPermissionRecords", "Error", ex.ToString());
            }
            return _PermissionRecords;
        }

        public ControllerType SaveControllerType(string strType, string description)
        {
            ControllerType _ControllerType = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(strType) && Int32.TryParse(strType, out int intType) && !string.IsNullOrWhiteSpace(description))
                {
                    var query = ctx.ControllerTypes.Where(x => x.ControllerTypeType == (Enums.ControllerTypeType)intType);
                    if (query != null && query.Any())
                    {
                        _ControllerType = query.OrderByDescending(o => o.CreatedDateTime).FirstOrDefault();
                    }
                    else
                    {
                        _ControllerType = new ControllerType()
                        {
                            ControllerTypeType = (Enums.ControllerTypeType)intType,
                            CreatedBy = _UserName,
                            CreatedDateTime = DateTime.Now
                        };
                        ctx.ControllerTypes.Add(_ControllerType);
                    };

                    _ControllerType.ControllerDesc = description;
                    _ControllerType.ModifiedBy = _UserName;
                    _ControllerType.ModifiedDateTime = DateTime.Now;
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveControllerType", "Error", ex.ToString());
            }
            return _ControllerType;
        }

        public UserResultObj CreateControllerType(string type, string description)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                if (!string.IsNullOrWhiteSpace(type) && !string.IsNullOrWhiteSpace(description))
                {
                    ControllerType _ControllerType = SaveControllerType(type, description);

                    if (_ControllerType == null)
                    {
                        _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotCreateItem;
                    }
                    else
                    {
                        _UserResultModel.Success = true;
                    }
                }
                else
                {
                    _UserResultModel.Success = false;
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorCreatingItem;
                }
            }
            catch (Exception ex)
            {
                _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorCreatingItem;
                SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "CreateControllerType", "Error: ", ex.ToString());
            }
            return _UserResultModel;
        }

        public UserResultObj DeleteControllerType(int id)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                // Check if Controller has any Actions before deleting
                var query = ctx.ActionTypes.Where(x => x.ControllerType.ControllerId == id);
                if (query == null || !query.Any())
                {
                    ControllerType _ControllerType = ctx.ControllerTypes.Find(id);
                    ctx.ControllerTypes.Remove(_ControllerType);
                    ctx.SaveChanges();
                    _UserResultModel.Success = true;
                }
                else
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotDeleteItem;
                }
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotDeleteItem;
                }
                else
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorDeletingItem;
                    SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "DeleteControllerType", "Error: ", ex.ToString());
                }
            }
            return _UserResultModel;
        }

        public ActionType SaveActionType(string strType, string description, bool externalAction, string strControllerId)
        {
            ActionType _ActionType = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(strType) && Int32.TryParse(strType, out int intType) &&
                    !string.IsNullOrWhiteSpace(description) &&
                    !string.IsNullOrWhiteSpace(strControllerId) && Enum.TryParse(strControllerId, out Enums.ControllerTypeType enumControllerId))
                {
                    var query = ctx.ActionTypes.Where(x => x.ActionTypeType == (Enums.ActionTypeType)intType);
                    if (query != null && query.Any())
                    {
                        _ActionType = query.OrderByDescending(o => o.CreatedDateTime).FirstOrDefault();
                    }
                    else
                    {
                        _ActionType = new ActionType()
                        {
                            ActionTypeType = (Enums.ActionTypeType)intType,
                            CreatedBy = _UserName,
                            CreatedDateTime = DateTime.Now
                        };
                        ctx.ActionTypes.Add(_ActionType);
                    };

                    _ActionType.ActionDesc = description;
                    _ActionType.ExternalAction = externalAction;
                    _ActionType.ControllerType_ControllerId = (Int32)enumControllerId;
                    _ActionType.ModifiedBy = _UserName;
                    _ActionType.ModifiedDateTime = DateTime.Now;
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveActionType", "Error", ex.ToString());
            }
            return _ActionType;
        }

        public UserResultObj CreateActionType(string type, string description, bool externalAction, string controllerId)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                if (!string.IsNullOrWhiteSpace(type) && !string.IsNullOrWhiteSpace(description) && !string.IsNullOrWhiteSpace(controllerId))
                {
                    ActionType _ActionType = SaveActionType(type, description, externalAction, controllerId);

                    if (_ActionType == null)
                    {
                        _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotCreateItem;
                    }
                    else
                    {
                        _UserResultModel.Success = true;
                    }
                }
                else
                {
                    _UserResultModel.Success = false;
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorCreatingItem;
                }
            }
            catch (Exception ex)
            {
                _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorCreatingItem;
                SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "CreateActionType", "Error: ", ex.ToString());
            }
            return _UserResultModel;
        }

        public UserResultObj DeleteActionType(int id)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                // Check if Action has any Permissions before deleting
                var query = ctx.PermissionRecords.Where(x => x.ActionType.ActionId == id);
                if (query == null || !query.Any())
                {
                    ActionType _ActionType = ctx.ActionTypes.Find(id);
                    ctx.ActionTypes.Remove(_ActionType);
                    ctx.SaveChanges();
                    _UserResultModel.Success = true;
                } else
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotDeleteItem;
                }
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotDeleteItem;
                }
                else
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorDeletingItem;
                    SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "DeleteActionType", "Error: ", ex.ToString());
                }
            }
            return _UserResultModel;
        }

        public PermissionRecord SavePermissionRecord(string strActionId, string description, bool enabled, string strAccessGroupId)
        {
            PermissionRecord _PermissionRecord = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(strActionId) && Int32.TryParse(strActionId, out int intActionId) &&
                    !string.IsNullOrWhiteSpace(description) &&
                    !string.IsNullOrWhiteSpace(strAccessGroupId) && Enum.TryParse(strAccessGroupId, out Enums.AccessGroupType enumAccessGroupId))
                {
                    var query = ctx.PermissionRecords.Where(x => x.ActionType_ActionId == intActionId &&
                                                                x.AccessGroup_AccessGroupId == (Int32)enumAccessGroupId);
                    if (query != null && query.Any())
                    {
                        _PermissionRecord = query.OrderByDescending(o => o.CreatedDateTime).FirstOrDefault();
                    }
                    else
                    {
                        _PermissionRecord = new PermissionRecord()
                        {
                            ActionType_ActionId = intActionId,
                            AccessGroup_AccessGroupId = (Int32)enumAccessGroupId,
                            CreatedBy = _UserName,
                            CreatedDateTime = DateTime.Now
                        };
                        ctx.PermissionRecords.Add(_PermissionRecord);
                    };

                    _PermissionRecord.PermissionRecordNote = description;
                    _PermissionRecord.Enabled = enabled;
                    _PermissionRecord.ModifiedBy = _UserName;
                    _PermissionRecord.ModifiedDateTime = DateTime.Now;
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SavePermissionRecord", "Error", ex.ToString());
            }
            return _PermissionRecord;
        }

        public UserResultObj CreatePermissionRecord(string actionType, string description, bool enabled, string accessGroupId)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                if (!string.IsNullOrWhiteSpace(actionType) && !string.IsNullOrWhiteSpace(description) && !string.IsNullOrWhiteSpace(accessGroupId))
                {
                    PermissionRecord _PermissionRecord = SavePermissionRecord(actionType, description, enabled, accessGroupId);

                    if (_PermissionRecord == null)
                    {
                        _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotCreateItem;
                    }
                    else
                    {
                        _UserResultModel.Success = true;
                    }
                }
                else
                {
                    _UserResultModel.Success = false;
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorCreatingItem;
                }
            }
            catch (Exception ex)
            {
                _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorCreatingItem;
                SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "CreatePermissionRecord", "Error: ", ex.ToString());
            }
            return _UserResultModel;
        }

        public UserResultObj DeletePermissionRecord(int id)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                PermissionRecord _PermissionRecord = ctx.PermissionRecords.Find(id);
                ctx.PermissionRecords.Remove(_PermissionRecord);
                ctx.SaveChanges();
                _UserResultModel.Success = true;
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotDeleteItem;
                }
                else
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorDeletingItem;
                    SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "DeletePermissionRecord", "Error: ", ex.ToString());
                }
            }
            return _UserResultModel;
        }

        #region userAccess
        public bool SetAccessGroup(int? userId, int[] accessGroupIdList)
        {
            bool returnVal = false;
            try
            {
                if (userId != null)
                {
                    User _User = ctx.Users.Find(userId);
                    List<AccessGroup> _NewAccessGroupList = new List<AccessGroup>();

                    if (accessGroupIdList != null && accessGroupIdList.Any())
                    {
                        _NewAccessGroupList = ctx.AccessGroups.Where(x => accessGroupIdList.Contains(x.AccessGroupId)).ToList();
                    }

                    _User.AccessGroups.ToList().ForEach(x => _User.AccessGroups.Remove(x));  // Remove existing access

                    _NewAccessGroupList.ForEach(x => _User.AccessGroups.Add(x));  // Add new list of access

                    ctx.SaveChanges();

                    returnVal = true;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SetAccessGroup", "Error", ex.ToString());
            }
            return returnVal;
        }
        #endregion

        public List<ProgramCampus> GetCampuses(bool onlyActive = true)
        {
            List<ProgramCampus> _ProgramCampuses = new List<ProgramCampus>();
            try
            {
                var query = (from p in ctx.ProgramCampuses
                             select p);

                if (onlyActive)
                {
                    query = query.Where(p => p.Active == true);
                }

                _ProgramCampuses = query.OrderBy(o => o.CampusOrder).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetCampuses", "Error", ex.ToString());
            }
            return _ProgramCampuses;
        }

        public List<ReferenceType> GetReferenceTypes()
        {
            List<ReferenceType> _ReferenceType = new List<ReferenceType>();
            try
            {
                _ReferenceType = ctx.ReferenceTypes.ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetReferenceTypes", "Error", ex.ToString());
            }
            return _ReferenceType;
        }

        public ReferenceType GetReferenceTypeByName(string name)
        {
            ReferenceType _ReferenceType = new ReferenceType();
            try
            {
                _ReferenceType = (from a in ctx.ReferenceTypes
                                  where a.ReferenceTypeName.Contains(name)
                                  select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetReferenceTypeByName", "Error", ex.ToString());
            }

            return _ReferenceType;
        }

        public List<ApplicationFee> GetApplicationFees()
        {
            List<ApplicationFee> _ApplicationFees = new List<ApplicationFee>();
            try
            {
                _ApplicationFees = ctx.ApplicationFees.ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetApplicationFees", "Error", ex.ToString());
            }
            return _ApplicationFees;
        }

        public List<BooleanValue> GetBooleanSettings()
        {
            List<BooleanValue> _BooleanSettings = new List<BooleanValue>();
            try
            {
                _BooleanSettings = ctx.BooleanValues.Where(b => b.Show == true).OrderBy(o => o.GroupName).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetBooleanSettings", "Error", ex.ToString());
            }
            return _BooleanSettings;
        }

        public bool SaveBooleanSettings(List<BooleanValue> booleanSettings)
        {
            bool success = false;
            try
            {
                foreach (var booleanSetting in booleanSettings)
                {
                    if (booleanSetting != null && booleanSetting.BooleanValueId != 0)
                    {
                        BooleanValue _BooleanSettings = ctx.BooleanValues.Find(booleanSetting.BooleanValueId);
                        if (_BooleanSettings != null && _BooleanSettings.Value != booleanSetting.Value)
                        {
                            _BooleanSettings.Value = booleanSetting.Value;
                            _BooleanSettings.ModifiedBy = _UserName;
                            _BooleanSettings.ModifiedDateTime = DateTime.Now;
                            ctx.SaveChanges();
                            success = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveBooleanSettings", "Error", ex.ToString());
            }
            return success;
        }

        public Dictionary<string, string> GetServerSettings()
        {
            Dictionary<string, string> _ServerSettings = new Dictionary<string, string>();
            try
            {
                string environment = Functions.GetEnvironment();
                _ServerSettings = ctx.ShortStringValues.Where(s => s.CheckServer == true && s.Environment.Contains(environment))
                                                       .OrderBy(o => o.Setting.Name)
                                                       .ToDictionary(x => x.Value.ToString().ToLower().Replace("\\","").Replace("https://","").Replace("/",""), x => x.Services);
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetServerSettings", "Error", ex.ToString());
            }
            return _ServerSettings;
        }

        public string GetTranscriptTermDesc(string termCode)
        {
            string termDesc = termCode.Trim().ToUpper();

            try
            {
                termDesc = ctx.TranscriptTermDescs.Where(x => x.TermCode == termDesc).OrderByDescending(y => y.ModifiedDateTime).Select(z => z.TermDesc).FirstOrDefault() ?? termDesc;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetTranscriptTermDesc", "Error", ex.ToString());
            }
            return termDesc;
        }

        public ApasMessage GetMessageByUUID(string uuid, Enums.MessageTypes messageType)
        {
            ApasMessage _ApplicationMessage = new ApasMessage();
            try
            {
                _ApplicationMessage = (from r in ctx.ApasMessages
                                       where r.MessageType == messageType
                                        && r.UUID.Contains(uuid)
                                       select r).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "getMessageByUUID", "Error", ex.ToString());
            }

            return _ApplicationMessage;
        }

        public List<string> GetUnimportedMessages()
        {
            List<string> _NewMessages = new List<string>();

            try
            {
                _NewMessages = ((from rm in ctx.ApasMessages
                                 join ram in ctx.ApplicationMessages on rm.UUID equals ram.UUID
                                 into combinedResult
                                 from c in combinedResult.DefaultIfEmpty()
                                 select new
                                 {
                                     UUID = rm.UUID,
                                     New = (c == null ? "" : c.UUID)
                                 }).Where(x => x.New == "")).Select(x => x.UUID).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetUnimportedMessages", "Error", ex.ToString());
            }

            return _NewMessages;
        }

        public Person GetPerson(int personId)
        {
            Person _Person = new Person();
            try
            {
                _Person = (from p in ctx.Persons
                           where p.PersonId == personId
                           select p).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetPerson", "Error", ex.ToString());
            }

            return _Person;
        }

        public Student GetStudent(int stuId)
        {
            Student _Student = null;
            try
            {
                _Student = (from s in ctx.Students
                            where s.StudentId == stuId
                            select s).OrderByDescending(o => o.CreatedDateTime).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetStudent", "Error", ex.ToString());
            }

            return _Student;
        }

        public Student GetStudentBySNumber(string sNumber)
        {
            Student _Student = null;
            try
            {
                _Student = (from s in ctx.Students
                            where s.sNumbers.Any(x => x.sNumVal.Trim().ToUpper() == sNumber.Trim().ToUpper())
                            select s).OrderByDescending(o => o.CreatedDateTime).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetStudentBySNumber", "Error", ex.ToString());
            }

            return _Student;
        }

        public Student GetStudentByResponseTransmissionDataUUID(string uuid)
        {
            Student _Student = null;
            List<string> uuidList = new List<string>();
            try
            {
                if (!string.IsNullOrWhiteSpace(uuid))
                {
                    uuidList.Add(uuid);
                    string uuidValue = GetKeyValueTempCache(uuid).Select(a => a.Value).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(uuidValue)) { uuidList.Add(uuidValue); }

                    _Student = (from s in ctx.Responses
                                where uuidList.Contains(s.TransmissionData.Uuid)
                               select s.RequestedStudent).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetStudent", "Error", ex.ToString());
            }

            return _Student;
        }

        public Student FindStudentByStudentRecord(StudentRecordObj studentRecord)
        {
            Student _Student = null;
            try
            {
                var result = (from s in ctx.Students select s);

                // StudentId
                if (studentRecord.StudentId != null)
                {
                    result = result.Where(x => x.StudentId == studentRecord.StudentId);
                }

                // ASN
                if (!string.IsNullOrWhiteSpace(studentRecord.ASN))
                {
                    result = result.Where(x => x.ASNs.Any(y => y.AgencyAssignedID == studentRecord.ASN));
                }

                // sNumber
                if (!string.IsNullOrWhiteSpace(studentRecord.Snumber))
                {
                    result = result.Where(x => x.sNumbers.Any(y => y.sNumVal == studentRecord.Snumber));
                }

                // First Name
                if (!string.IsNullOrWhiteSpace(studentRecord.FirstName))
                {
                    result = result.Where(x => x.Person.Names.Any(y => y.FirstName == studentRecord.FirstName));
                }

                // Last Name
                if (!string.IsNullOrWhiteSpace(studentRecord.LastName))
                {
                    result = result.Where(x => x.Person.Names.Any(y => y.LastName == studentRecord.LastName));
                }

                _Student = result.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "FindStudentByStudentRecord", "Error", ex.ToString());
            }

            return _Student;
        }

        public Person GetPersonByApplicationId(string uuid)
        {
            Person _Person = new Person();
            try
            {
                var query = (from a in ctx.Students
                             where a.StudentApplications.Any(y => y.ApplicationMessages.Any(x => x.UUID == uuid))
                             select a.Person);

                if (query.Any())
                {
                    _Person = query.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Person = null;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetPersonByApplicationId", "Error", ex.ToString());
            }

            return _Person;
        }

        public Email GetEmailByEmailAddress(string emailAddress)
        {
            Email _Email = null;
            try
            {
                var query = (from a in ctx.Emails
                             where a.EmailAddress.Trim().ToUpper() == emailAddress.Trim().ToUpper()
                             select a);

                if (query.Any())
                {
                    _Email = query.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetEmailByEmailAddress", "Error", ex.ToString());
            }

            return _Email;
        }

        public ApplicationMessage GetApplicationMessage(string uuid)
        {
            ApplicationMessage _ApplicationMessage = new ApplicationMessage();

            try
            {
                var query = (from a in ctx.ApplicationMessages
                             where a.UUID.Contains(uuid)
                             select a);

                if (query.Any())
                {
                    _ApplicationMessage = query.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetApplicationMessage", "Error", ex.ToString());
            }
            return _ApplicationMessage;
        }

        public ApplicationMessage GetApplicationMessageByUuid(string uuid)
        {
            ApplicationMessage _ApplicationMessage = new ApplicationMessage();
            uuid = uuid ?? string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(uuid))
                {
                    var query = (from a in ctx.ApplicationMessages
                                 where a.UUID.ToLower().Trim() == uuid.ToLower().Trim()
                                 select a);

                    if (query.Any())
                    {
                        _ApplicationMessage = query.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetApplicationMessageByUuid", "Error", "UUID: " + uuid + ", Exception: " + ex.ToString());
            }
            return _ApplicationMessage;
        }

        public XmlDocument GetNursingApplications(List<string> data)
        {
            XmlDocument result = new XmlDocument();

            // Get Nursing Applications for DataShare
            try
            {
                // Get Nursing Applications for DataShare
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetNursingApplications", "Error: ", ex.ToString().Substring(0, 200));
            }

            return result;
        }

        public string GetUuidByApplicationId(string applicationId)
        {
            string retval = string.Empty;
            try
            {
                if (!string.IsNullOrWhiteSpace(applicationId))
                {
                    var query = ctx.ApplicationMessages.Where(x => x.StudentApplication.ApplicationID == applicationId).OrderByDescending(y => y.ModifiedDateTime).Select(y => y.UUID).FirstOrDefault();

                    if (query != null)
                    {
                        retval = query;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetUuidByApplicationId", "Error", ex.ToString());
            }
            return retval;
        }

        public string GetUUIdByPayPalCancelResp(PayPalResponse response)
        {
            string retval = string.Empty;
            try
            {
                if (!string.IsNullOrWhiteSpace(response.SECURETOKEN))
                {
                    var query = (from r in ctx.PayPalResponses
                                 where r.SECURETOKEN.Contains(response.SECURETOKEN)
                                 select r).OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();

                    if (query != null)
                    {
                        retval = query.UUID;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetUUIdByPayPalCancelResp", "Error", ex.ToString());
            }
            return retval;
        }

        public bool CheckApplicationExists(string uuid)
        {
            bool success = false;
            ApplicationMessage _ApplicationMessage = new ApplicationMessage();
            try
            {
                success = (from r in ctx.ApplicationMessages
                           where r.UUID.ToLower().Trim() == uuid.ToLower().Trim()
                           select r).Any();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "CheckApplicationExists", "Error", ex.ToString());
            }

            return success;
        }

        public bool ApasMessageExists(string uuid, Enums.MessageTypes messageType)
        {
            bool success = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(uuid))
                {
                    success = ctx.ApasMessages.Any(o => o.UUID.ToLower().Trim() == uuid.ToLower().Trim() && o.MessageType == messageType);
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ApasMessageExists", "Error", ex.ToString());
            }
            return success;
        }

        public bool CheckReceivedErrorExists(string uuid)
        {
            bool success = false;
            try
            {
                if (!string.IsNullOrWhiteSpace(uuid) && ctx.ReceivedErrors.Any(o => o.UUID == uuid))
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "CheckReceivedErrorExists", "Error", ex.ToString());
            }

            return success;
        }

        public bool ConfirmPaymentComplete(string uuid)
        {
            bool _PrevPaid = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(uuid))
                {
                    //StudentApplication studentApplication = ctx.StudentApplications.Where(a => a.ApplicationMessages.Any(c => c.UUID == uuid)).FirstOrDefault().Student.Person.Students.FirstOrDefault().StudentApplications.Where(sa => sa.ProgramDetail != null).FirstOrDefault();

                    //_PrevPaid = ctx.ApplicationMessages.Where(x => x.StudentApplication.Student.Person.PersonId == studentApplication.Student.Person.PersonId
                    //                        && x.PaidDateTime != null
                    //                        && x.StudentApplication.ProgramDetail != null
                    //                        && x.StudentApplication.ProgramDetail.ProgramTerm != null
                    //                        && x.StudentApplication.ProgramDetail.ProgramTerm.ProgramTermId == studentApplication.ProgramDetail.ProgramTerm.ProgramTermId
                    //                        && x.StudentApplication.ProgramDetail.ApplicationProgram != null
                    //                        && x.StudentApplication.ProgramDetail.ApplicationProgram.ApplicationProgramId == studentApplication.ProgramDetail.ApplicationProgram.ApplicationProgramId
                    //                        ).Any();


                    Person person = ctx.ApplicationMessages.Where(am => am.UUID.Trim().ToUpper() == uuid.Trim().ToUpper()).FirstOrDefault().StudentApplication.Student.Person;

                    ProgramDetail programDetail = ctx.StudentApplications.Where(sa => ctx.ApplicationMessages.Any(am => am.UUID.Trim().ToUpper() == uuid.Trim().ToUpper() && am.StudentApplication == sa)).FirstOrDefault().ProgramDetail;

                    _PrevPaid = ctx.ApplicationMessages.Where(x => x.StudentApplication.Student.Person.PersonId == person.PersonId
                             && x.PaidDateTime != null
                             && x.StudentApplication.ProgramDetail != null
                             && x.StudentApplication.ProgramDetail.ProgramTerm != null
                             && x.StudentApplication.ProgramDetail.ProgramTerm.ProgramTermId == programDetail.ProgramTerm.ProgramTermId
                             && x.StudentApplication.ProgramDetail.ApplicationProgram != null
                             && x.StudentApplication.ProgramDetail.ApplicationProgram.ApplicationProgramId == programDetail.ApplicationProgram.ApplicationProgramId
                             ).Any();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ConfirmPaymentComplete", "Error", ex.ToString());
            }

            return _PrevPaid;
        }

        /// <summary>
        /// Load ApplicationFilters and Print to Xml
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetApplicationXml(List<string> keys)
        {
            return LoadApplications(keys, true).ToXml();
        }

        public ApplicationsObj LoadApplications(List<string> uuidStrList, bool reportOnly = false)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            ApplicationsObj _ApplicationsFilter = new ApplicationsObj();

            try
            {
                List<ApplicationObj> _Filters = new List<ApplicationObj>();

                _ApplicationsFilter.UuidStrList = uuidStrList;

                var msgs = (from a in ctx.ApplicationMessages
                            where uuidStrList.Contains(a.UUID)
                            orderby a.ReceivedDateTime
                            select a);

                foreach (ApplicationMessage a in msgs)
                {
                    ApplicationObj filter = new ApplicationObj
                    {
                        ApplicationID = a.StudentApplication.ApplicationID.EdiASCISafe(),

                        ASN = a.StudentApplication.Student.ASNs.OrderByDescending(x => x.CreatedDateTime).Select(y => y.AgencyAssignedID).FirstOrDefault().EdiASCISafe(),

                        ReceivedDateTime = a.ReceivedDateTime != null ? a.ReceivedDateTime.Value.ToString("MMddyyyy") : string.Empty,
                    };

                    if (a.StudentApplication.Student.Person != null)
                    {
                        filter.CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
                        filter.PrevSNumber = a.StudentApplication.Student.PrevSNumber.EdiASCISafe();
                        filter.PreviouslyApplied = a.StudentApplication.PreviouslyApplied.ToString();

                        if (a.StudentApplication.Student.Person.Disabilities.Any())
                        {
                            filter.Disability = a.StudentApplication.Student.Person.Disabilities.OrderByDescending(x => x.CreatedDateTime).Select(y => y.Disabled).FirstOrDefault().ToString();
                        }

                        if (a.StudentApplication.Student.Person.CanadianEthnicityRace != null)
                        {
                            string codeType = Structs.ApasCodeTypes.AboriginalStatus;
                            string code = a.StudentApplication.Student.Person.CanadianEthnicityRace.ToString();

                            var query = (from v in ctx.ValCodeLookups
                                         where v.ApasCode.ApasCodeCode.Contains(code)
                                              && v.CodeType.TypeCode.Contains(codeType)
                                         select v.ValCode).FirstOrDefault();

                            if (query != null)
                            {
                                filter.RaceEthnicity = query.ValCodeCode.EdiASCISafe();
                                filter.RaceEthnicityDesc = query.ValDesc.EdiNonASCISafe();
                            }
                        }

                        if (a.StudentApplication.Student.Person.Names.Any())
                        {
                            filter.FirstName = a.StudentApplication.Student.Person.Names.Where(n => n.NameType == Structs.Name.PersonalNameType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.FirstName).FirstOrDefault().EdiASCISafe();
                            filter.LastName = a.StudentApplication.Student.Person.Names.Where(n => n.NameType == Structs.Name.PersonalNameType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.LastName).FirstOrDefault().EdiASCISafe();
                            filter.MiddleName = string.Concat(a.StudentApplication.Student.Person.Names.Where(n => n.NameType == Structs.Name.PersonalNameType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.MiddleNames).FirstOrDefault(), " ").EdiASCISafe();

                            filter.FirstNameDesc = a.StudentApplication.Student.Person.Names.Where(n => n.NameType == Structs.Name.PersonalNameType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.FirstName).FirstOrDefault().EdiNonASCISafe();
                            filter.LastNameDesc = a.StudentApplication.Student.Person.Names.Where(n => n.NameType == Structs.Name.PersonalNameType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.LastName).FirstOrDefault().EdiNonASCISafe();
                            filter.MiddleNameDesc = string.Concat(a.StudentApplication.Student.Person.Names.Where(n => n.NameType == Structs.Name.PersonalNameType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.MiddleNames).FirstOrDefault(), " ").EdiNonASCISafe();
                        }

                        if (a.StudentApplication.Student.Person.Addresses.Any())
                        {
                            // Use Current Address by default
                            string _AddressType = Structs.Address.CurrentAddressType;

                            // If Current Address is empty use Permanent Address
                            if (!a.StudentApplication.Student.Person.Addresses.Any(y => y.AddressType == _AddressType)) { _AddressType = Structs.Address.PermanentAddressType; }

                            filter.InCanada = a.StudentApplication.Student.Person.Addresses.Where(y => y.AddressType == _AddressType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.Country).FirstOrDefault().EdiASCISafe() == "XCA";
                            filter.AddressLine1 = a.StudentApplication.Student.Person.Addresses.Where(y => y.AddressType == _AddressType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.AddressLine1).FirstOrDefault().EdiASCISafe();
                            filter.AddressLine2 = a.StudentApplication.Student.Person.Addresses.Where(y => y.AddressType == _AddressType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.AddressLine2).FirstOrDefault().EdiASCISafe();
                            filter.AddressCity = a.StudentApplication.Student.Person.Addresses.Where(y => y.AddressType == _AddressType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.City).FirstOrDefault().EdiASCISafe();

                            filter.AddressLine1Desc = a.StudentApplication.Student.Person.Addresses.Where(y => y.AddressType == _AddressType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.AddressLine1).FirstOrDefault().EdiNonASCISafe();
                            filter.AddressLine2Desc = a.StudentApplication.Student.Person.Addresses.Where(y => y.AddressType == _AddressType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.AddressLine2).FirstOrDefault().EdiNonASCISafe();
                            filter.AddressCityDesc = a.StudentApplication.Student.Person.Addresses.Where(y => y.AddressType == _AddressType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.City).FirstOrDefault().EdiNonASCISafe();

                            filter.AddressState = a.StudentApplication.Student.Person.Addresses.Where(y => y.AddressType == _AddressType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.Province).FirstOrDefault().EdiASCISafe();
                            filter.AddressPostalCode = a.StudentApplication.Student.Person.Addresses.Where(y => y.AddressType == _AddressType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.PostalCode).FirstOrDefault().EdiASCISafe();

                            string countryCodeType = Structs.ApasCodeTypes.Countries.EdiASCISafe();

                            string citCountryCode = a.StudentApplication.Student.Person.Addresses.Where(y => y.AddressType == _AddressType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.Country).FirstOrDefault().EdiASCISafe();

                            if (!string.IsNullOrWhiteSpace(citCountryCode))
                            {
                                var query1 = (from v in ctx.ValCodeLookups
                                              where v.ApasCode.ApasCodeCode.Contains(citCountryCode)
                                              && v.CodeType.TypeCode.Contains(countryCodeType)
                                              select v.ValCode).FirstOrDefault();

                                if (query1 != null)
                                {
                                    filter.AddressCountry = query1.ValCodeCode.EdiASCISafe();
                                    filter.AddressCountryDesc = query1.ValDesc.EdiNonASCISafe();
                                }
                            }
                        }

                        if (a.StudentApplication.Student.Person.Phones.Any())
                        {
                            filter.PermanentPhoneNumber = Functions.FixDomesticPhone(a.StudentApplication.Student.Person.Phones.Where(t => t.PhoneType == Enums.PhoneTypes.PermanentPhoneType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.PhoneNumber).FirstOrDefault().EdiASCISafe(), a.StudentApplication.Student.Person.Phones.Where(t => t.PhoneType == Enums.PhoneTypes.PermanentPhoneType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.AreaCode).FirstOrDefault().EdiASCISafe());
                            filter.DayPhoneNumber = Functions.FixDomesticPhone(a.StudentApplication.Student.Person.Phones.Where(t => t.PhoneType == Enums.PhoneTypes.DayPhoneType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.PhoneNumber).FirstOrDefault().EdiASCISafe(), a.StudentApplication.Student.Person.Phones.Where(t => t.PhoneType == Enums.PhoneTypes.DayPhoneType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.AreaCode).FirstOrDefault().EdiASCISafe());
                            filter.DayPhoneExtensionNumber = a.StudentApplication.Student.Person.Phones.Where(t => t.PhoneType == Enums.PhoneTypes.DayPhoneType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.PhoneNumberExtension).FirstOrDefault().EdiASCISafe();
                            filter.MobilePhoneNumber = Functions.FixDomesticPhone(a.StudentApplication.Student.Person.Phones.Where(t => t.PhoneType == Enums.PhoneTypes.MobilePhoneType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.PhoneNumber).FirstOrDefault().EdiASCISafe(), a.StudentApplication.Student.Person.Phones.Where(t => t.PhoneType == Enums.PhoneTypes.MobilePhoneType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.AreaCode).FirstOrDefault().EdiASCISafe());
                            filter.PreferredPhoneNumber = Functions.FixDomesticPhone(a.StudentApplication.Student.Person.Phones.Where(t => t.PhoneType == Enums.PhoneTypes.PreferredPhoneType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.PhoneNumber).FirstOrDefault().EdiASCISafe(), a.StudentApplication.Student.Person.Phones.Where(t => t.PhoneType == Enums.PhoneTypes.PreferredPhoneType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.AreaCode).FirstOrDefault().EdiASCISafe());
                        }

                        if (a.StudentApplication.Student.Person.BirthDate != null)
                        {
                            DateTime bday = (DateTime)a.StudentApplication.Student.Person.BirthDate;
                            filter.BirthDateEdi = bday.ToString("MMddyyyy").EdiASCISafe();
                            filter.BirthDateView = bday.ToString("yyyy/MM/dd").EdiASCISafe();
                        }

                        if (a.StudentApplication.Student.Person.Genders.Any())
                        {
                            filter.Gender = GetGender(a.StudentApplication.Student.Person.Genders.OrderByDescending(x => x.CreatedDateTime).Select(y => y.GenderCodeType).FirstOrDefault().EdiASCISafe());
                            filter.GenderDesc = a.StudentApplication.Student.Person.Genders.OrderByDescending(x => x.CreatedDateTime).Select(y => y.GenderCodeType).FirstOrDefault().EdiASCISafe();
                        }

                        //if (a.StudentApplication.Student.FamilyAttendedCollege != null)
                        //{
                            filter.FamilyAttendedCollege = GetFamilyAttendance(a.StudentApplication.Student.FamilyAttendedCollege);
                        //}

                        if (a.StudentApplication.Student.Person.Emails.Any())
                        {
                            filter.Email = a.StudentApplication.Student.Person.Emails.OrderByDescending(x => x.CreatedDateTime).Select(y => y.EmailAddress).FirstOrDefault().EdiASCISafe();
                        }

                        if (a.StudentApplication.ProgramDetail != null)
                        {
                            filter.Term = a.StudentApplication.ProgramDetail.ProgramTerm.TermCode.EdiASCISafe();
                            filter.Program = a.StudentApplication.ProgramDetail.ApplicationProgram.ProgramCode.EdiASCISafe();
                            filter.ProgramLocation = a.StudentApplication.ProgramDetail.ProgramCampus.CampusCode.EdiASCISafe();
                        }

                        filter.AdmitStatus = a.StudentApplication.StartingYear.ToString().EdiASCISafe();

                        filter.StudyLoad = a.StudentApplication.StudyLoad.EdiASCISafe();
                        filter.StudyLoadDesc = GetStudyLoadDesc(a.StudentApplication.StudyLoad.EdiNonASCISafe());

                        if (a.StudentApplication.ReferenceType != null && !string.IsNullOrWhiteSpace(a.StudentApplication.ReferenceType.ReferenceTypeName))
                        {
                            filter.FirstLearned = a.StudentApplication.ReferenceType.ReferenceTypeName.EdiASCISafe();
                            filter.FirstLearnedDesc = a.StudentApplication.ReferenceType.ReferenceTypeDesc.EdiNonASCISafe();
                        }

                        if (a.StudentApplication.Student.Person.Residencies.Any())
                        {
                            var residency = a.StudentApplication.Student.Person.Residencies.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();
                            string countryCode = residency.CountryCodeType.EdiASCISafe();
                            string countryCodeType = Structs.ApasCodeTypes.Countries.EdiASCISafe();

                            filter.ResidencyCountryDesc = residency.ResidencyCountry.EdiNonASCISafe();

                            filter.ResidencyCountryCode = (from v in ctx.ValCodeLookups
                                                           where v.ApasCode.ApasCodeCode.Contains(countryCode)
                                                              && v.CodeType.TypeCode.Contains(countryCodeType)
                                                           select v.ValCode.ValCodeCode).FirstOrDefault().EdiASCISafe();

                            filter.ResidencyEntryDateView = a.StudentApplication.Student.Person.Residencies.OrderByDescending(x => x.CreatedDateTime).Select(y => y.ResidencyFirstEntryDate).FirstOrDefault().ToString("yyyy/MM/dd").EdiNonASCISafe();
                            filter.ResidencyEntryDateEdi = a.StudentApplication.Student.Person.Residencies.OrderByDescending(x => x.CreatedDateTime).Select(y => y.ResidencyFirstEntryDate).FirstOrDefault().ToString("MMddyyyy").EdiASCISafe();
                        }

                        if (a.StudentApplication.Student.Person.Citizenships.Any())
                        {
                            string countryCodeType = Structs.ApasCodeTypes.Countries.EdiASCISafe();
                            string statusCodeType = Structs.ApasCodeTypes.CitizenshipStatus.EdiASCISafe();

                            if (a.StudentApplication.Student.Person.Citizenships.Any(x => x.CitizenshipStatusCodeType != Library.Apas.CoreMain.CitizenshipStatusCodeType.ResidentAlien))
                            {
                                string citCountryCode = a.StudentApplication.Student.Person.Citizenships.Where(b => b.CitizenshipStatusCodeType != Library.Apas.CoreMain.CitizenshipStatusCodeType.ResidentAlien).OrderByDescending(x => x.CreatedDateTime).Select(y => y.CountryCodeType).FirstOrDefault().EdiASCISafe();
                                string citStatusCode = a.StudentApplication.Student.Person.Citizenships.Where(b => b.CitizenshipStatusCodeType != Library.Apas.CoreMain.CitizenshipStatusCodeType.ResidentAlien).OrderByDescending(x => x.CreatedDateTime).Select(y => y.CitizenshipStatusCodeType).FirstOrDefault().EdiASCISafe();

                                var query1 = (from v in ctx.ValCodeLookups
                                              where v.ApasCode.ApasCodeCode.Contains(citCountryCode)
                                                 && v.CodeType.TypeCode.Contains(countryCodeType)
                                              select v.ValCode).FirstOrDefault();
                                if (query1 != null)
                                {
                                    filter.CitizenshipCountryCode = query1.ValCodeCode.EdiASCISafe();
                                    filter.CitizenshipCountryDesc = query1.ValDesc.EdiNonASCISafe();
                                }

                                var query2 = (from v in ctx.ValCodeLookups
                                              where v.ApasCode.ApasCodeCode.Contains(citStatusCode)
                                                 && v.CodeType.TypeCode.Contains(statusCodeType)
                                              select v.ValCode).FirstOrDefault();

                                if (query2 != null)
                                {
                                    filter.CitizenshipStatusCode = query2.ValCodeCode.EdiASCISafe();
                                    filter.CitizenshipStatusDesc = query2.ValDesc.EdiNonASCISafe();
                                }
                                else
                                {  // If didn't find any use "O" - Other Citizenship Status
                                    query2 = (from v in ctx.ValCodeLookups
                                              where v.ApasCode.ApasCodeCode.Contains("Other")
                                                 && v.CodeType.TypeCode.Contains(statusCodeType)
                                              select v.ValCode).FirstOrDefault();
                                    if (query2 != null)
                                    {
                                        filter.CitizenshipStatusCode = query2.ValCodeCode.EdiASCISafe();
                                        filter.CitizenshipStatusDesc = query2.ValDesc.EdiNonASCISafe();
                                    }
                                }
                            }

                            // using secondary citizenship code to set residency values
                            if (a.StudentApplication.Student.Person.Citizenships.Any(x => x.CitizenshipStatusCodeType == Library.Apas.CoreMain.CitizenshipStatusCodeType.ResidentAlien))
                            {
                                string resCountryCode = a.StudentApplication.Student.Person.Citizenships.Where(b => b.CitizenshipStatusCodeType == Library.Apas.CoreMain.CitizenshipStatusCodeType.ResidentAlien).OrderByDescending(x => x.CreatedDateTime).Select(y => y.CountryCodeType).FirstOrDefault().EdiASCISafe();
                                string resStatusCode = a.StudentApplication.Student.Person.Citizenships.Where(b => b.CitizenshipStatusCodeType == Library.Apas.CoreMain.CitizenshipStatusCodeType.ResidentAlien).OrderByDescending(x => x.CreatedDateTime).Select(y => y.CitizenshipStatusCodeType).FirstOrDefault().EdiASCISafe();

                                var query1 = (from v in ctx.ValCodeLookups
                                              where v.ApasCode.ApasCodeCode.Contains(resCountryCode)
                                              && v.CodeType.TypeCode.Contains(countryCodeType)
                                              select v.ValCode).FirstOrDefault();

                                if (query1 != null)
                                {
                                    filter.ResidencyCountryCode = query1.ValCodeCode.EdiASCISafe();
                                    filter.ResidencyCountryDesc = query1.ValDesc.EdiNonASCISafe();
                                }


                                var query2 = (from v in ctx.ValCodeLookups
                                              where v.ApasCode.ApasCodeCode.Contains(resStatusCode)
                                                 && v.CodeType.TypeCode.Contains(statusCodeType)
                                              select v.ValCode).FirstOrDefault();

                                if (query2 != null)
                                {
                                    filter.ResidencyStatusCode = query2.ValCodeCode.EdiASCISafe();
                                    filter.ResidencyStatusDesc = query2.ValDesc.EdiNonASCISafe();
                                }
                            }
                        }

                        if (a.StudentApplication.Student.Person.Immigrations.Any())
                        {
                            DateTime firstEntry = a.StudentApplication.Student.Person.Immigrations.OrderByDescending(x => x.CreatedDateTime).Select(y => y.FirstEntryIntoCountryDateTime).FirstOrDefault();

                            filter.ImmigrationEntryDateView = firstEntry.ToString("yyyy/MM/dd");
                            filter.ImmigrationEntryDateEdi = firstEntry.ToString("MMddyyyy");
                        }

                        if (!string.IsNullOrWhiteSpace(a.StudentApplication.Student.Person.LanguageCode))
                        {
                            string code = a.StudentApplication.Student.Person.LanguageCode.EdiASCISafe();
                            string codeType = Structs.ApasCodeTypes.FirstLanguage.EdiASCISafe();

                            filter.FirstLanguage = (from v in ctx.ValCodeLookups
                                                    where v.ApasCode.ApasCodeCode.Contains(code)
                                                       && v.CodeType.TypeCode.Contains(codeType)
                                                    select v.ValCode.ValCodeCode).FirstOrDefault().EdiASCISafe();
                        }

                        if (a.StudentApplication.Student.Person.Names.Where(x => x.NameType == Structs.Name.FormerType).Any())
                        {
                            //filter.FormerNames = a.StudentApplication.Student.Person.Names.Where(n => n.NameType == Structs.Name.FormerType).OrderByDescending(x => x.CreatedDateTime).Select(f => string.Concat(f.FirstName, " ", f.LastName)).FirstOrDefault().EdiASCISafe();
                            filter.FormerFirstName = a.StudentApplication.Student.Person.Names.Where(n => n.NameType == Structs.Name.FormerType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.FirstName).FirstOrDefault().EdiASCISafe();
                            filter.FormerLastName = a.StudentApplication.Student.Person.Names.Where(n => n.NameType == Structs.Name.FormerType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.LastName).FirstOrDefault().EdiASCISafe();
                            filter.FormerMiddleName = a.StudentApplication.Student.Person.Names.Where(n => n.NameType == Structs.Name.FormerType).OrderByDescending(x => x.CreatedDateTime).Select(y => y.MiddleNames).FirstOrDefault().EdiASCISafe();
                        }
                    }

                    if (a.StudentApplication.Student.ContactPersons.Any())
                    {
                        filter.EmergencyContactName = a.StudentApplication.Student.ContactPersons.OrderByDescending(x => x.CreatedDateTime).Select(x => string.Concat(x.ContactFirstName, " ", x.ContactLastName)).FirstOrDefault().EdiASCISafe();
                        filter.EmergencyContactPhone = a.StudentApplication.Student.ContactPersons.OrderByDescending(x => x.CreatedDateTime).Select(y => y.ContactPhones).FirstOrDefault().OrderByDescending(x => x.CreatedDateTime).Select(p => string.Concat(p.ContactPhoneNumber, " ", p.ContactPhoneExt)).FirstOrDefault().EdiASCISafe();
                    }

                    filter.ApplicationFee = a.StudentApplication.ApplicationFee.ApplicationFeeAmt * 100;  // Remove decimals

                    if (a.PayPalResponses.Any())
                    {
                        filter.PayPalCardType = a.PayPalResponses.OrderByDescending(x => x.CreatedDateTime).Select(y => y.CARDTYPE).FirstOrDefault().EdiASCISafe();
                    }

                    //var records = (from r in ctx.AcademicRecords
                    //               where r.Student.StudentId == a.StudentApplication.Student.StudentId
                    //               select r).Distinct();

                    //var records = ctx.AcademicRecords.Where(x => x.Student.StudentId == a.StudentApplication.Student.StudentId && x.Responses == null)  // Don't include transcript academic records
                    var records = ctx.AcademicRecords.Where(x => x.Student.StudentId == a.StudentApplication.Student.StudentId && x.ApplicationId == a.ApplicationID)  // Include from application not in the transcript academic records only
                                                     .GroupBy(r => new { r.StudentLevel, r.FirstDateAttended, r.LastDateAttended, r.School, r.Student })
                                                     .Select(y => y.FirstOrDefault());

                    List<ApplInstObj> institutions = new List<ApplInstObj>();
                    int? academicRecordId = null;

                    // collect institutions
                    foreach (AcademicRecord record in records)
                    {
                        string instName = null;

                        academicRecordId = record.AcademicRecordId;

                        ApplInstObj institution = new ApplInstObj();
                        if (record.School != null)
                        {
                            if (!string.IsNullOrWhiteSpace(record.School.LocalOrganizationID))
                            {
                                institution.LocalOrganizationID = record.School.LocalOrganizationID.EdiASCISafe();
                            }

                            if (record.School.Addresses.Count() > 0)
                            {
                                institution.CityProv = record.School.Addresses.OrderByDescending(x => x.ModifiedDateTime).Select(y => y.City).FirstOrDefault().EdiASCISafe() + " " + record.School.Addresses.OrderByDescending(y => y.ModifiedDateTime).Select(y => y.Province).FirstOrDefault().EdiASCISafe();
                            }

                            instName = record.School.InstitutionNames.OrderByDescending(x => x.CreatedDateTime).Select(n => n.Name).FirstOrDefault().EdiASCISafe();
                        }

                        // &#x000A;
                        // Need to truncate
                        institution.InstitutionName = textInfo.ToTitleCase(instName ?? string.Empty).Replace("Of", "of");

                        // First Date Attended (old Academic Session Start Date field)
                        if (Functions.CheckForNullDate(record.FirstDateAttended) != null)
                        {
                            institution.StartYear = record.FirstDateAttended.Value.Year.EdiASCISafe();
                        }

                        // Last Date Attended (old Academic Session End Date field)
                        if (Functions.CheckForNullDate(record.LastDateAttended) != null)
                        {
                            institution.EndYear = record.LastDateAttended.Value.Year.EdiASCISafe();
                        }

                        // Duration
                        if (!string.IsNullOrWhiteSpace(institution.StartYear.EdiASCISafe()))
                        {
                            institution.Duration = institution.StartYear.EdiASCISafe();

                            if (!string.IsNullOrWhiteSpace(institution.EndYear.EdiASCISafe()))
                            {
                                institution.Duration = institution.Duration + " - " + institution.EndYear.EdiASCISafe();
                            }
                        }

                        // Highest Grade
                        if (record.AcademicAwards.OrderByDescending(y => y.CreatedDateTime).Any())
                        {
                            var awardLevel = record.AcademicAwards.OrderByDescending(y => y.CreatedDateTime).Select(y => y.AcademicAwardLevel).FirstOrDefault().GetXmlEnumAttribute(typeof(Library.Apas.CoreMain.AcademicAwardLevelType)).EdiASCISafe();

                            var grade = (from v in ctx.ValCodeLookups
                                         where v.ApasCode.ApasCodeCode.Trim().ToUpper() == awardLevel.Trim().ToUpper()
                                         && v.CodeType.TypeCode.Contains(Structs.ApasCodeTypes.HighestGrade)
                                         select v.ValCode).FirstOrDefault();
                            if (grade != null)
                            {
                                institution.HighestGrade = grade.ValCodeCode.EdiASCISafe();
                                institution.HighestGradeDesc = grade.ValDesc.EdiASCISafe();
                            }
                        }

                        // Student Level
                        if (!string.IsNullOrWhiteSpace(record.StudentLevel.EdiASCISafe()))
                        {
                            institution.StudentLevel = record.StudentLevel.EdiASCISafe();
                        }

                        institutions.Add(institution);

                    }

                    // Add Alberta Education as institution attended, so when exporting a high school transcript it will be all under Alberta Education
                    if (!reportOnly)
                    {
                        ApplInstObj albertaEducation = AddAlbertaEducationInstitution(institutions);

                        if (albertaEducation != null)
                        {
                            institutions.Add(albertaEducation);
                        }
                    }

                    filter.ApplicationInstitutions = institutions.OrderBy(x => x.StartYear).ToList();

                    var courses = ctx.Courses.Where(x => x.AcademicRecord.Student.StudentId == a.StudentApplication.Student.StudentId &&
                                                    //x.AcademicRecord.Responses == null &&  // Don't include transcript courses
                                                    x.AcademicRecord.ApplicationId == a.ApplicationID &&  // Include from application not in the transcript academic records only
                                                    x.AcademicRecord.AcademicRecordId == (academicRecordId ?? x.AcademicRecord.AcademicRecordId))
                                             .GroupBy(r => new { r.AgencyCourseID, r.Title, r.AcademicGrade, r.CreditEarned, r.NarrativeExplanationGrade, r.EndDate })
                                             .Select(y => y.FirstOrDefault());

                    List<ApplCrsObj> applicationCourses = new List<ApplCrsObj>();

                    foreach (Course course in courses)
                    {
                        ApplCrsObj applicationCourse = new ApplCrsObj
                        {
                            CourseId = course.AgencyCourseID.EdiASCISafe(),
                            CourseName = course.Title.EdiASCISafe(),
                            Grade = course.AcademicGrade.EdiASCISafe(),
                            Credit = course.CreditEarned.EdiASCISafe(),
                            Status = course.NarrativeExplanationGrade.EdiASCISafe(),
                            EndDate = course.EndDate != null ? course.EndDate.Value.ToString("yyyy/MM/dd").EdiASCISafe() : null,
                        };

                        applicationCourses.Add(applicationCourse);
                    }

                    filter.ApplicationCourses = applicationCourses;

                    _Filters.Add(filter);
                }

                _ApplicationsFilter.ApplicationFilters = _Filters;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "LoadApplications", "Error", ex.ToString());
            }

            return _ApplicationsFilter;
        }

        private ApplInstObj AddAlbertaEducationInstitution(List<ApplInstObj> institutions)
        {
            ApplInstObj albertaEducation = null;
            try
            {
                // Add Alberta Education as institution attended, so when exporting a high school transcript it will be all under Alberta Education
                int? highestStudentLevel = null;
                string highestGrade = null;
                int? StartYear = null, EndYear = null;

                // Identify if it doesn't have post secondary institution, then it's high school application (Alberta Education)
                foreach (var institution in institutions)
                {
                    // Get highest level
                    if (!string.IsNullOrWhiteSpace(institution.StudentLevel) &&
                        Int32.TryParse(institution.StudentLevel, out int studentLevel) &&
                        studentLevel < (int)Library.Apas.CoreMain.StudentLevelCodeType.Postsecondary &&
                        (highestStudentLevel == null || studentLevel > highestStudentLevel))
                    {
                        highestStudentLevel = studentLevel;
                    }

                    // Get highest grade
                    if (!string.IsNullOrWhiteSpace(institution.HighestGrade))
                    {
                        if (string.IsNullOrWhiteSpace(highestGrade))
                        {
                            highestGrade = institution.HighestGrade;
                        }
                        else
                        {
                            // Sort and get the highest grade
                            List<string> grades = new List<string>();
                            grades.Add(highestGrade);
                            grades.Add(institution.HighestGrade);
                            highestGrade = grades.OrderBy(x => x).FirstOrDefault();
                        }
                    }

                    // Get Start Year
                    if (!string.IsNullOrWhiteSpace(institution.StartYear) &&
                        Int32.TryParse(institution.StartYear, out int startYearInteger) &&
                        (StartYear == null || startYearInteger < StartYear))
                    {
                        StartYear = startYearInteger;
                    }

                    // Get End Year
                    if (!string.IsNullOrWhiteSpace(institution.EndYear) &&
                        Int32.TryParse(institution.EndYear, out int endYearInteger) &&
                        (EndYear == null || endYearInteger < EndYear))
                    {
                        EndYear = endYearInteger;
                    }
                }

                // When it's a high school institution, no post secondary found, add Alberta Education as an attended institution
                if (highestStudentLevel == null ||
                    highestStudentLevel != null && highestStudentLevel < (int)Library.Apas.CoreMain.StudentLevelCodeType.Postsecondary)
                {
                    if (string.IsNullOrWhiteSpace(highestGrade)) highestGrade = "12";  // High School level

                    if (EndYear == null)
                    {
                        DateTime currentDate = DateTime.Now;
                        if (currentDate.Month >= 7)  // July
                        {
                            EndYear = currentDate.Year;  // Graduated same year
                        }
                        else
                        {
                            EndYear = currentDate.Year - 1;   // Graduated previous year
                        }
                    }
                    if (StartYear == null && EndYear != null)
                    {
                        StartYear = EndYear - 4;  // Started hogh school 4 years ago
                    }

                    albertaEducation = new ApplInstObj()
                    {
                        StudentLevel = highestStudentLevel.ToString(),
                        HighestGrade = highestGrade,
                        StartYear = StartYear.ToString(),
                        EndYear = EndYear.ToString(),
                        LocalOrganizationID = "AB00000000",
                        InstitutionName = ctx.InstitutionNames.Where(x => x.Institution.LocalOrganizationID == "AB00000000").OrderByDescending(o => o.CreatedDateTime).Select(s => s.Name).FirstOrDefault(),
                    };
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "AddAlbertaEducationInstitution", "Error", ex.ToString());
            }
            return albertaEducation;
        }

        private string GetHSGradType(string code)
        {
            string val = string.Empty;
            try
            {
                switch (code)
                {
                    case "B18":
                        val = "High School Diploma"; //	12
                        break;
                    case "B26":
                        val = "IB-International Baccalaureate"; //	12
                        break;
                    case "B19":
                        val = "AP-Advanced Placement College Board"; //	12
                        break;
                    case "B25":
                        val = "Other HS Equivalency Diploma"; //	12
                        break;
                    case "B24":
                        val = "GED"; //	12
                        break;
                    case "B17":
                        val = "Did not complete HS Diploma"; //	11
                        break;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetHSGradType", "Error", ex.ToString());
            }
            return val;
        }

        private string GetHSGradTypeDesc(string code)
        {
            string val = string.Empty;
            try
            {
                switch (code)
                {
                    case "B18":
                        val = "12";
                        break;
                    case "B26":
                        val = "12";
                        break;
                    case "B19":
                        val = "12";
                        break;
                    case "B25":
                        val = "12";
                        break;
                    case "B24":
                        val = "12";
                        break;
                    case "B17":
                        val = "11";
                        break;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetHSGradTypeDesc", "Error", ex.ToString());
            }
            return val;
        }

        private string GetGraduationType(string type)
        {
            string retval = string.Empty;
            try
            {
                switch (type)
                {
                    case "4.4":
                        retval = "DD";
                        break;
                    case "4.2":
                        retval = "MD";
                        break;
                    case "4.1":
                        retval = "GCD";
                        break;
                    case "3.1":
                        retval = "FPD";
                        break;
                    case "2.4":
                        retval = "BD";
                        break;
                    case "2":
                        retval = "UDC";
                        break;
                    case "2.7":
                        retval = "CPD";
                        break;
                    case "2.3":
                        retval = "UAD";
                        break;
                    case "2.6":
                        retval = "CAD";
                        break;
                    case "1.2":
                        retval = "JTC";
                        break;
                    case "2.1":
                        retval = "CTC";
                        break;
                    case "1.1":
                        retval = "OCB";
                        break;
                    case "2.2":
                        retval = "APAD";
                        break;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetGraduationType", "Error", ex.ToString());
            }
            return retval;
        }

        private string GetGraduationTypeDesc(string type)
        {
            string retval = string.Empty;
            try
            {
                switch (type)
                {
                    case "4.4":
                        retval = "Doctoral Degree";
                        break;
                    case "4.2":
                        retval = "Master's Degree";
                        break;
                    case "4.1":
                        retval = "Graduate Certificate/Diploma";
                        break;
                    case "3.1":
                        retval = "First Professional Degree";
                        break;
                    case "2.4":
                        retval = "Bachelor Degree";
                        break;
                    case "2":
                        retval = "UDC";
                        break;
                    case "2.7":
                        retval = "College Post-Dipl (3or4)";
                        break;
                    case "2.3":
                        retval = "University Associate Diploma";
                        break;
                    case "2.6":
                        retval = "College Diploma (2 Year)";
                        break;
                    case "1.2":
                        retval = "Journeyman Trade Certificate";
                        break;
                    case "2.1":
                        retval = "Career Trade Cert (1 Yr)";
                        break;
                    case "1.1":
                        retval = "Other Complete-beyon HS";
                        break;
                    case "2.2":
                        retval = "Applied Arts & Tech Degree";
                        break;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetGraduationTypeDesc", "Error", ex.ToString());
            }
            return retval;
        }

        private string GetPreviousStatusCode(string StatusCode, string StatusLocation)
        {
            string val = StatusCode.ToUpper() + " " + StatusLocation.ToUpper();
            try
            {
                switch (val)
                {
                    case "STUDENT INALBERTA":
                        val = "AHS";
                        break;
                    case "WORK INALBERTA":
                        val = "AW";
                        break;
                    case "OTHER INALBERTA":
                        val = "AO";
                        break;
                    case "STUDENT INOTHERPROVINCES":
                        val = "OHS";
                        break;
                    case "WORK INOTHERPROVINCES":
                        val = "OW";
                        break;
                    case "OTHER INOTHERPROVINCES":
                        val = "OO";
                        break;
                    case "STUDENT OUTSIDECANADA":
                        val = "IHS";
                        break;
                    case "WORK OUTSIDECANADA":
                        val = "IW";
                        break;
                    case "OTHER OUTSIDECANADA":
                        val = "IO";
                        break;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetPreviousStatusCode", "Error", ex.ToString());
            }
            return val;
        }

        private string GetResidencyStatusDesc(string StatusCode, string StatusLocation)
        {
            string val = StatusCode.ToUpper() + " " + StatusLocation.ToUpper();
            try
            {
                switch (val)
                {
                    case "STUDENT INALBERTA":
                        val = "Alberta Student";
                        break;
                    case "WORK INALBERTA":
                        val = "Alberta Working";
                        break;
                    case "OTHER INALBERTA":
                        val = "Alberta Other";
                        break;
                    case "STUDENT INOTHERPROVINCES":
                        val = "Alberta Province Student";
                        break;
                    case "WORK INOTHERPROVINCES":
                        val = "Other Province Working";
                        break;
                    case "OTHER INOTHERPROVINCES":
                        val = "Other Province Other";
                        break;
                    case "STUDENT OUTSIDECANADA":
                        val = "Outside Canada Student";
                        break;
                    case "WORK OUTSIDECANADA":
                        val = "Outside Canada Working";
                        break;
                    case "OTHER OUTSIDECANADA":
                        val = "Outside Canada Other";
                        break;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetResidencyStatusDesc", "Error", ex.ToString());
            }
            return val;
        }

        private string GetGender(string gender)
        {
            string retval = string.Empty;

            try
            {
                switch (gender)
                {
                    case "Female":
                        retval = "F";
                        break;
                    case "Male":
                        retval = "M";
                        break;
                    case "Unreported":
                        retval = "U";
                        break;
                    case "Unspecified":
                        retval = "X";
                        break;
                    default:
                        retval = "O";
                        break;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetGender", "Error", ex.ToString());
            }

            return retval;
        }

        private Library.Apas.CoreMain.GenderCodeType GetGenderCodeType(string gender)
        {
            Library.Apas.CoreMain.GenderCodeType retval = Library.Apas.CoreMain.GenderCodeType.Unreported;

            try
            {
                switch (gender.Substring(0, 1).ToUpper())
                {
                    case "F":
                        retval = Library.Apas.CoreMain.GenderCodeType.Female;
                        break;
                    case "M":
                        retval = Library.Apas.CoreMain.GenderCodeType.Male;
                        break;
                    case "U":
                        retval = Library.Apas.CoreMain.GenderCodeType.Unreported;
                        break;
                    case "X":
                        retval = Library.Apas.CoreMain.GenderCodeType.Unspecified;
                        break;
                    default:
                        retval = Library.Apas.CoreMain.GenderCodeType.Unreported;
                        break;
                }


            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetGenderCodeType", "Error", ex.ToString());
            }

            return retval;
        }

        private string GetFamilyAttendance(Library.Apas.CoreMain.AttendedCollegeCodeType? code)
        {
            string retval = "N";

            try
            {
                switch (code)
                {
                    case Library.Apas.CoreMain.AttendedCollegeCodeType.EitherParent:
                    case Library.Apas.CoreMain.AttendedCollegeCodeType.GrandParent:
                    case Library.Apas.CoreMain.AttendedCollegeCodeType.Siblings:
                    case Library.Apas.CoreMain.AttendedCollegeCodeType.Spouse:
                        retval = "Y";
                        break;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetFamilyAttendance", "Error", ex.ToString());
            }

            return retval;
        }

        private string GetStudyLoadDesc(string code)
        {
            string retval = string.Empty;

            try
            {
                switch (code)
                {
                    case "F":
                        retval = "FullTime";
                        break;
                    case "P":
                        retval = "PartTime";
                        break;
                    default:
                        retval = "UnKnown";
                        break;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetStudyLoadDesc", "Error", ex.ToString());
            }

            return retval;
        }

        public Library.Apas.StatisticalData.StatisticalDataType CreateStatisticalData(StudentApplication localApplication)
        {
            Library.Apas.StatisticalData.StatisticalDataType _StatisticalData = new Library.Apas.StatisticalData.StatisticalDataType();

            try
            {
                _StatisticalData.CreateDateTime = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.ffZ");
                _StatisticalData.ApplicationID = Int32.Parse(localApplication.ApplicationID);
                _StatisticalData.ApplicantASN = localApplication.Student.ASNs.OrderByDescending(x => x.CreatedDateTime).First().AgencyAssignedID;
                _StatisticalData.ApplicationStage = Library.Apas.StatisticalData.ApplicationStageType.Submitted;

                // Create Local Organization
                Library.Apas.CoreMain.LocalOrganizationIDType _InstitutionID = new Library.Apas.CoreMain.LocalOrganizationIDType()
                {
                    LocalOrganizationIDCode = GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                    LocalOrganizationIDQualifier = Library.Apas.CoreMain.LocalOrganizationIDCodeQualifierType.AB  // Default Value
                };
                _StatisticalData.InstitutionID = _InstitutionID;

                // Create Academic Session Detail
                Library.Apas.CoreMain.AcademicSessionDetailType _AcademicSession = new Library.Apas.CoreMain.AcademicSessionDetailType()
                {
                    SessionDesignator = localApplication.SessionDesignator,
                    SessionName = localApplication.ProgramDetail != null ? localApplication.ProgramDetail.ProgramTerm.TermDesc : "null"
                };
                _StatisticalData.AcademicSession = _AcademicSession;

                // Create Academic Program
                Library.Apas.StatisticalData.AcademicProgramTypeType _ApplicationDegreeProgram = new Library.Apas.StatisticalData.AcademicProgramTypeType()
                {
                    AcademicProgramType = Library.Apas.CoreMain.AcademicProgramTypeType.Major,  // Default value
                    AcademicProgramName = localApplication.ProgramDetail != null ? localApplication.ProgramDetail.ApplicationProgram.ProgramDesc : "null",
                    AcademicProgramCode = localApplication.ProgramDetail != null ? localApplication.ProgramDetail.ApplicationProgram.ProgramCode : "null",
                    AcademicProgramCampus = localApplication.ProgramDetail != null ? localApplication.ProgramDetail.ProgramCampus.CampusCode : "null"
                };

                //_ApplicationDegreeProgram.AcademicProgramDegreeLevel = null;
                //_ApplicationDegreeProgram.AcademicProgramPriority = null;
                _StatisticalData.ApplicationDegreeProgram = _ApplicationDegreeProgram;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "CreateStatisticalData", "Error", ex.ToString());
            }
            return _StatisticalData;
        }

        public bool ReturnPaidApplications()
        {
            bool success = false;
            try
            {
                var applicationMessages = (from a in ctx.ApplicationMessages
                                           where a.PaidDateTime != null
                                           && a.ReturnedDateTime == null
                                           && a.CancelledDateTime == null
                                           select a).OrderByDescending(x => x.CreatedDateTime).ToList();

                foreach (var msg in applicationMessages)
                {
                    using (ApasLogic apasLogic = new ApasLogic())
                    {
                        bool returned = apasLogic.SendApasApplicationStatisticalData(msg.UUID);
                        if (returned)
                        {
                            msg.ReturnedDateTime = DateTime.Now;
                            ctx.SaveChanges();
                        }
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ReturnPaidApplications", "Error", ex.ToString());
            }
            return success;
        }

        public bool SetApplicationProgramActiveStatus(int applicationProgramId, bool status)
        {
            bool success = false;
            try
            {
                ApplicationProgram _ApplicationProgram = ctx.ApplicationPrograms.Find(applicationProgramId);
                _ApplicationProgram.Active = status;
                _ApplicationProgram.ModifiedBy = _UserName;
                _ApplicationProgram.ModifiedDateTime = DateTime.Now;
                ctx.SaveChanges();

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SetApplicationProgramActiveStatus", "Error", ex.ToString());
            }
            return success;
        }

        public UserResultObj DeleteApplicationProgram(int applicationProgramId)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                ApplicationProgram _ApplicationProgram = ctx.ApplicationPrograms.Find(applicationProgramId);
                ctx.ApplicationPrograms.Remove(_ApplicationProgram);
                ctx.SaveChanges();
                _UserResultModel.Success = true;
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotDeleteItem;
                }
                else
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorDeletingItem;
                    SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "DeleteApplicationProgram", "Error", ex.ToString());
                }

            }
            return _UserResultModel;
        }

        public bool SaveProgramOrder(List<int> programOrder)
        {
            bool success = false;
            try
            {
                int order = 1;
                foreach (var program in programOrder)
                {
                    ApplicationProgram _ApplicationProgram = ctx.ApplicationPrograms.Find(program);
                    _ApplicationProgram.ProgramOrder = order;
                    order++;
                }
                ctx.SaveChanges();

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveProgramOrder", "Error", ex.ToString());
            }
            return success;
        }

        public bool SetApplicationProgramPendingStatus(int applicationProgramId, bool pending)
        {
            bool success = false;
            try
            {
                ApplicationProgram _ApplicationProgram = ctx.ApplicationPrograms.Find(applicationProgramId);
                if (pending)
                {
                    // Add Pending text
                    if (!_ApplicationProgram.ProgramDesc.Contains(Structs.Settings.PendingGovernmentApproval))
                    {
                        _ApplicationProgram.ProgramDesc += " " + Structs.Settings.PendingGovernmentApproval;
                    }
                } else
                {
                    // Remove Pending text
                    if (_ApplicationProgram.ProgramDesc.Contains(Structs.Settings.PendingGovernmentApproval))
                    {
                        _ApplicationProgram.ProgramDesc = _ApplicationProgram.ProgramDesc.Replace(Structs.Settings.PendingGovernmentApproval, "").Trim();
                    }
                }
                _ApplicationProgram.ModifiedBy = _UserName;
                _ApplicationProgram.ModifiedDateTime = DateTime.Now;
                ctx.SaveChanges();

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SetApplicationProgramActiveStatus", "Error", ex.ToString());
            }
            return success;
        }

        public UserResultObj CreateCampus(string itemCode, string itemDesc, bool active)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                var _ProgramCampus = AddProgramCampus(itemCode, itemDesc, active);

                if (_ProgramCampus == null)
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotCreateItem;
                }
                else
                {
                    _UserResultModel.Success = true;
                    _UserResultModel.ItemId = _ProgramCampus.ProgramCampusId.ToString();
                }
            }
            catch (Exception ex)
            {
                _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorCreatingItem;
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "CreateCampus", "Error", ex.ToString());
            }
            return _UserResultModel;
        }

        public bool ActivateCampus(int id, bool active)
        {
            bool success = false;
            try
            {
                ProgramCampus _ProgramCampus = ctx.ProgramCampuses.Find(id);
                _ProgramCampus.Active = active;
                _ProgramCampus.ModifiedBy = _UserName;
                _ProgramCampus.ModifiedDateTime = DateTime.Now;
                ctx.SaveChanges();

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ActivateCampus", "Error", ex.ToString());
            }
            return success;
        }

        public UserResultObj DeleteCampus(int id)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                ProgramCampus _ProgramCampus = ctx.ProgramCampuses.Find(id);
                ctx.ProgramCampuses.Remove(_ProgramCampus);
                ctx.SaveChanges();

                _UserResultModel.Success = true;
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotDeleteItem;
                }
                else
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorDeletingItem;
                    SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "DeleteCampus", "Error", ex.ToString());
                }
            }
            return _UserResultModel;
        }

        public bool SaveCampusOrder(List<int> campusOrder)
        {
            bool success = false;
            try
            {
                int order = 1;
                foreach (var campus in campusOrder)
                {
                    ProgramCampus _ProgramCampus = ctx.ProgramCampuses.Find(campus);
                    _ProgramCampus.CampusOrder = order;
                    order++;
                }
                ctx.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveCampusOrder", "Error", ex.ToString());
            }
            return success;
        }

        public UserResultObj CreateTerm(string itemCode, string itemDesc, bool active)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                var _ProgramTerm = AddProgramTerms(itemCode, itemDesc, active);

                if (_ProgramTerm == null)
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotCreateItem;
                }
                else
                {
                    _UserResultModel.Success = true;
                    _UserResultModel.ItemId = _ProgramTerm.ProgramTermId.ToString();
                }
            }
            catch (Exception ex)
            {
                _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorCreatingItem;
                SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "CreateTerm", "Error: ", ex.ToString());
            }
            return _UserResultModel;
        }

        public UserResultObj CreateUser(string snumber, string firstname,string lastname, bool active)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                var _User = AddUsers(snumber, firstname, lastname, active);

                if (_User == null)
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotCreateItem;
                }
                else
                {
                    _UserResultModel.Success = true;
                    _UserResultModel.ItemId = _User.UserId.ToString();
                }
            }
            catch (Exception ex)
            {
                _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorCreatingItem;
                SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "CreateUser", "Error: ", ex.ToString());
            }
            return _UserResultModel;
        }

        public bool ActivateTerm(int id, bool active)
        {
            bool success = false;
            try
            {
                ProgramTerm _ProgramTerm = ctx.ProgramTerms.Find(id);
                _ProgramTerm.Active = active;
                _ProgramTerm.ModifiedBy = _UserName;
                _ProgramTerm.ModifiedDateTime = DateTime.Now;
                ctx.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "ActivateTerm", "Error: ", ex.ToString());
            }
            return success;
        }

        public bool ActivateUser(int id, bool active)
        {
            bool success = false;
            try
            {
                User _User = ctx.Users.Find(id);
                _User.Active = active;
                _User.ModifiedBy = _UserName;
                _User.ModifiedDateTime = DateTime.Now;
                ctx.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "ActivateUser", "Error: ", ex.ToString());
            }
            return success;
        }

        public UserResultObj DeleteTerm(int id)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                ProgramTerm _ProgramTerm = ctx.ProgramTerms.Find(id);
                ctx.ProgramTerms.Remove(_ProgramTerm);
                ctx.SaveChanges();
                _UserResultModel.Success = true;
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotDeleteItem;
                }
                else
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorDeletingItem;
                    SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "DeleteTerm", "Error: ", ex.ToString());
                }
            }
            return _UserResultModel;
        }
        public UserResultObj DeleteUser(int id)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                User _User = ctx.Users.Find(id);
                ctx.Users.Remove(_User);
                ctx.SaveChanges();
                _UserResultModel.Success = true;
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotDeleteItem;
                }
                else
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorDeletingItem;
                    SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "DeleteUser", "Error: ", ex.ToString());
                }
            }
            return _UserResultModel;
        }


        public bool OrderUser(List<int> userOrder)
        {
            bool success = false;
            try
            {
                int order = 1;
                foreach (var userorder in userOrder)
                {
                    User _User = ctx.Users.Find(userorder);
                    _User.UserOrder = order;
                    order++;
                }
                ctx.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "OrderUser", "Error: ", ex.ToString());
            }
            return success;
        }

        
        public bool OrderTerm(List<int> termOrder)
        {
            bool success = false;
            try
            {
                int order = 1;
                foreach (var term in termOrder)
                {
                    ProgramTerm _ProgramTerm = ctx.ProgramTerms.Find(term);
                    _ProgramTerm.TermOrder = order;
                    order++;
                }
                ctx.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "OrderTerm", "Error: ", ex.ToString());
            }
            return success;
        }


        public UserResultObj CreateReferenceType(string itemCode, string itemDesc)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                var _ReferenceType = AddReferenceType(itemCode, itemDesc);

                if (_ReferenceType == null)
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotCreateItem;
                }
                else
                {
                    _UserResultModel.Success = true;
                    _UserResultModel.ItemId = _ReferenceType.ReferenceTypeId.ToString();
                }
            }
            catch (Exception ex)
            {
                _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorCreatingItem;
                SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "CreateReferenceType", "Error: ", ex.ToString());
            }
            return _UserResultModel;
        }

        public UserResultObj DeleteReferenceType(int id)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                ReferenceType _ReferenceType = ctx.ReferenceTypes.Find(id);
                ctx.ReferenceTypes.Remove(_ReferenceType);
                ctx.SaveChanges();
                _UserResultModel.Success = true;
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotDeleteItem;
                }
                else
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorDeletingItem;
                    SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "DeleteReferenceType", "Error: ", ex.ToString());
                }
            }
            return _UserResultModel;
        }

        public bool OrderReferenceType(List<int> referenceOrder)
        {
            bool success = false;
            try
            {
                int order = 1;
                foreach (var reference in referenceOrder)
                {
                    ReferenceType _ReferenceType = ctx.ReferenceTypes.Find(reference);
                    _ReferenceType.ReferenceTypeOrder = order;
                    order++;
                }
                ctx.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "OrderReferenceType", "Error: ", ex.ToString());
            }
            return success;
        }

        public UserResultObj CreateApplicationFee(string strStartDateTime, string strFeeAmount, string message)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                if (DateTime.TryParse(strStartDateTime, out DateTime dteStartDateTime) &&
                    Double.TryParse(strFeeAmount.Replace('$',' ').Trim(), out double doubleFeeAmount))
                {
                    var _ApplicationFee = AddApplicationFee(dteStartDateTime, doubleFeeAmount, message);

                    if (_ApplicationFee == null)
                    {
                        _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotCreateItem;
                    }
                    else
                    {
                        _UserResultModel.Success = true;
                        _UserResultModel.ItemId = _ApplicationFee.ApplicationFeeId.ToString();
                    }
                }
                else
                {
                    _UserResultModel.Success = false;
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorCreatingItem;
                }
            }
            catch (Exception ex)
            {
                _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorCreatingItem;
                SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "CreateApplicationFee", "Error: ", ex.ToString());
            }
            return _UserResultModel;
        }

        public UserResultObj DeleteApplicationFee(int id)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                ApplicationFee _ApplicationFee = ctx.ApplicationFees.Find(id);
                ctx.ApplicationFees.Remove(_ApplicationFee);
                ctx.SaveChanges();
                _UserResultModel.Success = true;
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotDeleteItem;
                }
                else
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorDeletingItem;
                    SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "DeleteApplicationFee", "Error: ", ex.ToString());
                }
            }
            return _UserResultModel;
        }

        public List<Setting> GetSettings()
        {
            List<Setting> _Settings = new List<Setting>();
            try
            {
                _Settings = ctx.Settings.ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetSettings", "Error", ex.ToString());
            }
            return _Settings;
        }

        public bool OrderSettings(List<int> settingOrder)
        {
            bool success = false;
            try
            {
                int order = 1;
                foreach (var setting in settingOrder)
                {
                    Setting _Setting = ctx.Settings.Find(setting);
                    _Setting.SettingOrder = order;
                    order++;
                }
                ctx.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "OrderSettings", "Error", ex.ToString());
            }
            return success;
        }

        public bool OrderSettingCategories(List<int> categoryOrder)
        {
            bool success = false;
            try
            {
                int order = 1;
                foreach (var category in categoryOrder)
                {
                    SettingCategory _SettingCategories = ctx.SettingCategories.Find(category);
                    _SettingCategories.CategoryOrder = order;
                    order++;
                }
                ctx.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "OrderSettingCategories", "Error", ex.ToString());
            }
            return success;
        }

        public List<ProgTermCampObj> GetProgramDetails(int programId)
        {
            List<ProgTermCampObj> _ProgramDetails = new List<ProgTermCampObj>();
            try
            {
                _ProgramDetails = (from d in ctx.ProgramDetails
                                   where d.ApplicationProgram.ApplicationProgramId == programId
                                   && d.ApplicationProgram.Active == true
                                   && d.ProgramTerm.Active == true
                                   && d.ProgramCampus.Active == true
                                   select new ProgTermCampObj
                                   {
                                       Program = d.ApplicationProgram.ApplicationProgramId,
                                       Term = d.ProgramTerm.ProgramTermId,
                                       TermCode = d.ProgramTerm.TermCode,
                                       TermDesc = d.ProgramTerm.TermDesc,
                                       Campus = d.ProgramCampus.ProgramCampusId,
                                       Order = d.ProgramDetailOrder,
                                       Active = d.Active  // && d.ProgramTerm.Active && d.ProgramCampus.Active && d.ApplicationProgram.Active
                                   }).OrderBy(p => p.Order).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetProgramDetails", "Error", ex.ToString());
            }
            return _ProgramDetails;
        }

        public bool SaveProgramDetails(int programId, int campusId, int termId, bool active = true)
        {
            bool success = false;
            ProgramDetail _ProgramDetail = new ProgramDetail();
            try
            {
                var query = (from d in ctx.ProgramDetails
                             where d.ApplicationProgram.ApplicationProgramId == programId
                                && d.ProgramCampus.ProgramCampusId == campusId
                                && d.ProgramTerm.ProgramTermId == termId
                             select d).ToList();

                if (!query.Any())
                {
                    _ProgramDetail.ApplicationProgram = ctx.ApplicationPrograms.Where(p => p.ApplicationProgramId == programId).FirstOrDefault();
                    _ProgramDetail.ProgramCampus = ctx.ProgramCampuses.Where(c => c.ProgramCampusId == campusId).FirstOrDefault();
                    _ProgramDetail.ProgramTerm = ctx.ProgramTerms.Where(t => t.ProgramTermId == termId).FirstOrDefault();
                    _ProgramDetail.StartDate = DateTime.Now;
                    _ProgramDetail.Active = active;

                    _ProgramDetail.CreatedBy = _UserName;
                    _ProgramDetail.CreatedDateTime = DateTime.Now;
                    _ProgramDetail.ModifiedBy = _UserName;
                    _ProgramDetail.ModifiedDateTime = DateTime.Now;

                    ctx.ProgramDetails.Add(_ProgramDetail);
                    ctx.SaveChanges();
                }
                else
                {
                    _ProgramDetail = query.OrderByDescending(d => d.StartDate).First();
                }

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "SaveProgramDetails", "Error: ", ex.ToString());
            }
            return success;
        }

        public UserResultObj DeleteProgramDetails(int programId, int campusId, int termId)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                var query = (from d in ctx.ProgramDetails
                             where d.ApplicationProgram.ApplicationProgramId == programId
                                && d.ProgramCampus.ProgramCampusId == campusId
                                && d.ProgramTerm.ProgramTermId == termId
                             select d).ToList();

                if (query.Any())
                {
                    ctx.ProgramDetails.RemoveRange(query);
                    ctx.SaveChanges();
                    _UserResultModel.Success = true;
                }
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotDeleteItem;
                }
                else
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorDeletingItem;
                    SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "DeleteProgramDetails", "Error: ", ex.ToString());
                }
            }
            return _UserResultModel;
        }

        //public UserResultObj ActivateProgramDetails(int programId, int campusId, int termId)
        //{
        //    UserResultObj _UserResultModel = new UserResultObj() { Success = false };
        //    try
        //    {
        //        var query = (from d in ctx.ProgramDetails
        //                     where d.ApplicationProgram.ApplicationProgramId == programId
        //                        && d.ProgramCampus.ProgramCampusId == campusId
        //                        && d.ProgramTerm.ProgramTermId == termId
        //            select new ProgramDetail
        //            {
        //                ProgramDetailsId = d.ApplicationProgram.ApplicationProgramId,
        //                MonthlyBased = true,
        //                ProgramCampus_ProgramCampusId = campusId,
        //                ProgramTerm_ProgramTermId = termId,
        //                Active = true,
        //            }).OrderBy(p => p.ProgramDetailsId).ToList();

        //        if (query.Any())
        //        {
        //            ctx.ProgramDetails.Select(m => m.Active) ;
        //            ctx.SaveChanges();
        //            _UserResultModel.Success = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex is DbUpdateException)
        //        {
        //            _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotDeleteItem;
        //        }
        //        else
        //        {
        //            _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorDeletingItem;
        //            SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "ActivateProgramDetails", "Error: ", ex.ToString());
        //        }
        //    }
        //    return _UserResultModel;
        //}

        public bool ActivateProgramDetails(int programId, int campusId, int termId, bool active)
        {
            bool success = false;
            try
            {
                ProgramDetail _ProgramDetail = ctx.ProgramDetails.Where(x => x.ApplicationProgram.ApplicationProgramId == programId &&
                                                                             x.ProgramCampus.ProgramCampusId == campusId &&
                                                                             x.ProgramTerm.ProgramTermId == termId).FirstOrDefault();
                if (_ProgramDetail != null)
                {
                    _ProgramDetail.Active = active;
                    _ProgramDetail.ModifiedBy = _UserName;
                    _ProgramDetail.ModifiedDateTime = DateTime.Now;
                    ctx.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "ActivateProgramDetails", "Error: ", ex.ToString());
            }
            return success;
        }

        public bool OrderProgramDetails(int programId, int campusId, List<int> termListOrder)
        {
            bool success = false;
            try
            {
                if (termListOrder != null)
                {
                    int order = 1;
                    foreach (var term in termListOrder)
                    {
                        var query = (from d in ctx.ProgramDetails
                                     where d.ApplicationProgram.ApplicationProgramId == programId
                                         && d.ProgramCampus.ProgramCampusId == campusId
                                         && d.ProgramTerm.ProgramTermId == term
                                     select d).ToList();
                        if (query.Any())
                        {
                            ProgramDetail _ProgramDetails = query.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                            _ProgramDetails.ProgramDetailOrder = order;
                            order++;
                            ctx.SaveChanges();
                            success = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.AdminController, "OrderProgramDetails", "Error: ", ex.ToString());
            }
            return success;
        }

        public UserResultObj CreateProgram(string itemCode, string itemDesc, bool active)
        {
            UserResultObj _UserResultModel = new UserResultObj() { Success = false };
            try
            {
                var _ApplicationProgram = AddPrograms(itemCode, itemDesc, active);

                if (_ApplicationProgram == null)
                {
                    _UserResultModel.Message = Structs.ApplicationSettingMessages.CannotCreateItem;
                }
                else
                {
                    _UserResultModel.Success = true;
                    _UserResultModel.ItemId = _ApplicationProgram.ApplicationProgramId.ToString();
                }
            }
            catch (Exception ex)
            {
                _UserResultModel.Message = Structs.ApplicationSettingMessages.ErrorCreatingItem;
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "CreateProgram", "Error: ", ex.ToString());
            }
            return _UserResultModel;
        }

        public ApplicationFee GetApplicationFee()
        {
            ApplicationFee _ApplicationFee = new ApplicationFee();

            try
            {
                var query = ctx.ApplicationFees.Where(a => a.StartDateTime <= DateTime.Now)
                            .OrderByDescending(t => t.StartDateTime).ToList();

                if (!query.Any())
                {
                    _ApplicationFee.ApplicationFeeAmt = Convert.ToDouble(100);
                    _ApplicationFee.CreatedBy = _UserName;
                    _ApplicationFee.CreatedDateTime = _XmlCreatedDate;

                    ctx.ApplicationFees.Add(_ApplicationFee);
                }
                else
                {
                    _ApplicationFee = query.OrderByDescending(x => x.StartDateTime).FirstOrDefault();
                }

                _ApplicationFee.ModifiedBy = _UserName;
                _ApplicationFee.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetApplicationFee", "Error", ex.ToString());
            }
            return _ApplicationFee;
        }

        public IEnumerable<SelectListItem> GetPrograms()
        {
            IEnumerable<SelectListItem> _Destinations;
            try
            {
                _Destinations = (from p in ctx.ApplicationPrograms.OrderBy(x => x.ProgramOrder)
                                 where p.Active == true
                                 select new SelectListItem
                                 {
                                     Value = p.ApplicationProgramId.ToString(),
                                     Text = p.ProgramCode,
                                 }).ToList();

            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetPrograms", "Error", ex.ToString());
                return null;
            }

            return _Destinations;
        }

        public IEnumerable<SelectListItem> GetProgramCode(string _ProgramDesc)
        {
            IEnumerable<SelectListItem> _Destinations;
            try
            {
                _Destinations = (from p in ctx.ApplicationPrograms.OrderBy(x => x.ProgramOrder)
                                 where p.Active == true
                                 select new SelectListItem
                                 {
                                     Value = p.ApplicationProgramId.ToString(),
                                     Text = p.ProgramCode,
                                 }).ToList();

            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetPrograms", "Error", ex.ToString());
                return null;
            }

            return _Destinations;
        }


        public int? GetProgramId(string program_code)
        {
            int? id = null;
            try
            {
                id = (from p in ctx.ApplicationPrograms
                      where p.ProgramCode.ToUpper().Trim() == program_code.ToUpper().Trim()
                      select p.ApplicationProgramId).FirstOrDefault();

                if (id == 0) { id = null; };
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetProgramId", "Error", ex.ToString());
            }

            return id;
        }

        public int? GetTermId(string term_code)
        {
            int? id = null;
            try
            {
                id = (from p in ctx.ProgramTerms
                      where p.TermCode.ToUpper().Trim() == term_code.ToUpper().Trim()
                      select p.ProgramTermId).FirstOrDefault();

                if (id == 0) { id = null; };
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetTermId", "Error", ex.ToString());
            }

            return id;
        }

        public IEnumerable<SelectListItem> GetTermList()
        {
            IEnumerable<SelectListItem> _Destinations;
            try
            {
                _Destinations = (from t in ctx.ProgramTerms.OrderBy(x => x.TermOrder)
                                 where t.Active == true
                                 select new SelectListItem
                                 {
                                     Value = t.ProgramTermId.ToString(),
                                     Text = t.TermCode,
                                 }).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetTermList", "Error", ex.ToString());
                return null;
            }

            return _Destinations;
        }

        public int? GetCampusId(string campus_code)
        {
            int? id = null;
            try
            {
                id = (from p in ctx.ProgramCampuses
                      where p.CampusCode.ToUpper().Trim() == campus_code.ToUpper().Trim()
                      select p.ProgramCampusId).FirstOrDefault();

                if (id == 0) { id = null; };
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetCampusId", "Error", ex.ToString());
            }

            return id;
        }

        #endregion Applications

        #region Settings

        public dynamic GetSetting(string settingType, string settingName)
        {
            Setting _Setting = new Setting();
            dynamic _RetVal = "";
            try
            {
                _Setting = (from s in ctx.Settings
                            where s.Name.Contains(settingName)
                            select s).FirstOrDefault();

                switch (settingType)
                {
                    case Structs.SettingTypes.Boolean:
                        _RetVal = _Setting.BooleanValues.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().Value;
                        break;
                    case Structs.SettingTypes.Integer:
                        _RetVal = _Setting.IntegerValues.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().Value;
                        break;
                    case Structs.SettingTypes.String:
                        _RetVal = _Setting.ShortStringValues.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().Value;
                        break;
                    case Structs.SettingTypes.Double:
                        _RetVal = _Setting.DoubleValues.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().Value;
                        break;
                    case Structs.SettingTypes.DteTime:
                        _RetVal = _Setting.DateTimeValues.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().Value;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetSetting", "Error", ex.ToString());
            }

            return _RetVal;
        }

        #endregion Settings

        #region Parse Xml Documents

        /// <summary>
        /// Create/Update Application Message
        /// Uniqueness determined by DocumentID
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public bool ParseApplicationMessage(string uuid, string createdDatetime = null)
        {
            bool success = false;

            try
            {
                // instantiate objects
                ApplicationMessage _ApplicationMessage = new ApplicationMessage();
                Library.Apas.AdmissionsApplication.AdmissionsApplication _AdmissionsApplication = new Library.Apas.AdmissionsApplication.AdmissionsApplication();

                // get APAS Message (Received Application) by UUID
                ApasMessage _ReceivedMessage = GetApasMessage(uuid, Enums.MessageTypes.Application);

                // deserialize XML message to Admissions Application Object
                _AdmissionsApplication = DeserializeAdmApp(_AdmissionsApplication, _ReceivedMessage.MessageXML.CheckPrefix());

                if (_AdmissionsApplication.TransmissionData != null && _AdmissionsApplication.TransmissionData.CreatedDateTime != null)
                {
                    if (createdDatetime != null)
                    {
                        _XmlCreatedDate = Convert.ToDateTime(createdDatetime);
                    }
                    else
                    {
                        _XmlCreatedDate = _AdmissionsApplication.TransmissionData.CreatedDateTime;
                    }
                }

                // extract parsed fields
                _ApplicationMessage.DocumentID = _AdmissionsApplication.TransmissionData.DocumentID;

                var applicationMessages = ctx.ApplicationMessages.Where(x => x.DocumentID == _AdmissionsApplication.TransmissionData.DocumentID);

                if (applicationMessages.Any())
                {
                    _ApplicationMessage = applicationMessages.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _ApplicationMessage.CreatedBy = _UserName;
                    _ApplicationMessage.CreatedDateTime = _XmlCreatedDate;
                    ctx.ApplicationMessages.Add(_ApplicationMessage);
                }

                // create/update ApplicationMessage properties
                _ApplicationMessage.UUID = _ReceivedMessage.UUID;
                _ApplicationMessage.ApplicationBody = _ReceivedMessage.MessageXML;
                _ApplicationMessage.ModifiedBy = _UserName;
                _ApplicationMessage.ModifiedDateTime = _XmlCreatedDate;

                // Add SourceInstitution if new
                Institution _Source = ParseAcadRecOrganizationType(_AdmissionsApplication.TransmissionData.Source.Organization);
                if (_ApplicationMessage.ApplicationMessageId < 1 || _ApplicationMessage.SourceInstitution.InstitutionId != _Source.InstitutionId)
                {
                    _ApplicationMessage.SourceInstitution = _Source;
                }

                // Add DestinationInstitution if new
                Institution _Destination = ParseAcadRecOrganizationType(_AdmissionsApplication.TransmissionData.Destination.Organization);
                if (_ApplicationMessage.ApplicationMessageId < 1 || _ApplicationMessage.DestinationInstitution.InstitutionId != _Destination.InstitutionId)
                {
                    _ApplicationMessage.DestinationInstitution = _Destination;
                }

                // Add StudentApplication if new
                StudentApplication _StudentApplication = ParseApplication(_AdmissionsApplication.Applicant);
                if (_ApplicationMessage.ApplicationMessageId < 1 || _ApplicationMessage.StudentApplication.StudentApplicationId != _StudentApplication.StudentApplicationId)
                {
                    _ApplicationMessage.StudentApplication = _StudentApplication;
                }

                if (_AdmissionsApplication.Applicant.Application.Count == 1)
                {
                    _ApplicationMessage.ApplicationID = _AdmissionsApplication.Applicant.Application.Select(y => y.ApplicationID).FirstOrDefault();
                }

                ctx.SaveChanges();

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseApplicationMessage", "Error: ", ex.ToString());
            }
            return success;
        }

        /// <summary>
        /// Parse/Create TransmissionData
        /// </summary>
        /// <param name="transmissionData"></param>
        /// <returns></returns>
        public TransmissionData ParseTransmissionData(Library.Apas.AcademicRecord.TransmissionDataType transmissionData, string uuid, Enums.MessageTypes? messageType = null)
        {
            TransmissionData _TransmissionData = new TransmissionData();

            try
            {
                var trandatas = ctx.TransmissionDatas.Where(t => t.DocumentID == transmissionData.DocumentID);

                if (!trandatas.Any())
                {
                    _TransmissionData.Uuid = uuid;
                    _TransmissionData.DocumentID = transmissionData.DocumentID;

                    if (!string.IsNullOrWhiteSpace(transmissionData.RequestTrackingID))
                    {
                        _TransmissionData.RequestTrackingID = transmissionData.RequestTrackingID;
                    }

                    if (transmissionData.CreatedDateTime != null)
                    {
                        // Check if date and time is in the future, convert from UTC to local time
                        if (transmissionData.CreatedDateTime >= DateTime.Now)
                        {
                            transmissionData.CreatedDateTime = Functions.ConvertLocalTime(transmissionData.CreatedDateTime);
                        }
                        _XmlCreatedDate = transmissionData.CreatedDateTime;
                    }

                    _TransmissionData.CreatedDateTime = _XmlCreatedDate;
                    _TransmissionData.CreatedBy = _UserName;
                    _TransmissionData.TransmissionTypeType = transmissionData.TransmissionType;
                    _TransmissionData.DocumentOfficialCodeType = transmissionData.DocumentOfficialCode;
                    _TransmissionData.DocumentCompleteCodeType = transmissionData.DocumentCompleteCode;

                    // Make sure the resceived responses are marked as Response Document Type
                    if (messageType != null && messageType == Enums.MessageTypes.ReceivedResponse)
                    {
                        _TransmissionData.DocumentTypeCode = Library.Apas.CoreMain.DocumentTypeCodeType.Response;
                    }
                    else
                    {
                        _TransmissionData.DocumentTypeCode = transmissionData.DocumentTypeCode;
                    }

                    _TransmissionData.SourceInstitution = ParseAcadRecInstitutionType(transmissionData.Source);

                    _TransmissionData.DestinationInstitution = ParseAcadRecInstitutionType(transmissionData.Destination);


                    _TransmissionData.ModifiedBy = _UserName;
                    _TransmissionData.ModifiedDateTime = _XmlCreatedDate;

                }
                else {
                    _TransmissionData = trandatas.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseTransmissionData", "Error", ex.ToString());
            }
            return _TransmissionData;
        }

        /// <summary>
        /// Parse/Update DocumentType
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public Response ParseResponseType(Library.Apas.AcademicRecord.ResponseType response, string documentID)
        {
            Response _Response = new Response();

            try
            {
                var responses = ctx.Responses.Where(r => r.TransmissionData.DocumentID == documentID);

                if (responses.Any())
                {
                    _Response = responses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Response.RequestTrackingID = response.RequestTrackingID;
                    _Response.CreatedBy = _UserName;
                    _Response.CreatedDateTime = _XmlCreatedDate;
                }

                _Response.ResponseStatusType = response.ResponseStatus;
                if (string.IsNullOrWhiteSpace(_Response.ModifiedBy) || _Response.ModifiedBy == Functions.systemUserName)
                {
                    _Response.ModifiedBy = _UserName;
                    _Response.ModifiedDateTime = _XmlCreatedDate;
                }

                _Response.RequestedStudent = ParseStudentType(response.RequestedStudent);

                // Parse Holds
                foreach (Library.Apas.AcademicRecord.ResponseHoldType hold in response.ResponseHold)
                {
                    ResponseHold _ResponseHold = ParseResponseHoldType(hold, _Response.ResponseId);
                    if (_ResponseHold.ResponseHoldTypeId < 1 || !_Response.ResponseHolds.Any(x => x.ResponseHoldTypeId == _ResponseHold.ResponseHoldTypeId))
                    {
                        _Response.ResponseHolds.Add(_ResponseHold);
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseResponseType", "Error", ex.ToString());
            }

            return _Response;
        }

        public Response ParseCollegeTranscriptType(Library.Apas.CollegeTranscript.CollegeTranscript collegeTranscript, Enums.MessageTypes messageType)
        {
            string DocumentID = string.Empty;
            bool? collegeData = null;
            Response _Response = new Response();

            try
            {
                DocumentID = collegeTranscript.TransmissionData.DocumentID;

                var responses = ctx.Responses.Where(r => r.TransmissionData.DocumentID == DocumentID);

                if (responses.Any())
                {
                    _Response = responses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Response.CreatedBy = _UserName;
                    _Response.CreatedDateTime = _XmlCreatedDate;
                }

                if (string.IsNullOrWhiteSpace(_Response.ModifiedBy) || _Response.ModifiedBy == Functions.systemUserName)
                {
                    _Response.ModifiedBy = _UserName;
                    _Response.ModifiedDateTime = _XmlCreatedDate;
                }

                if (messageType == Enums.MessageTypes.SentResponse) { collegeData = true; }
                _Response.RequestedStudent = ParseAcadRecStudentType(collegeTranscript.Student, collegeData);

                foreach (Library.Apas.AcademicRecord.AcademicRecordType rec in collegeTranscript.Student.AcademicRecord)
                {
                    AcademicRecord _AcademicRecord = ParseAcadRec(rec, _Response);
                    if (_AcademicRecord.AcademicRecordId < 1 || _Response.AcademicRecords.Any(y => y.AcademicRecordId != _AcademicRecord.AcademicRecordId))
                    {
                        _Response.AcademicRecords.Add(_AcademicRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseCollegeTranscriptType", "Error", ex.ToString());
            }

            return _Response;
        }

        public Response ParseHighSchoolTranscriptType(Library.Apas.HighSchoolTranscript.HighSchoolTranscript highSchoolTranscript)
        {
            string DocumentID = string.Empty;
            Response _Response = new Response();

            try
            {
                DocumentID = highSchoolTranscript.TransmissionData.DocumentID;

                var responses = ctx.Responses.Where(r => r.TransmissionData.DocumentID == DocumentID);

                if (responses.Any())
                {
                    _Response = responses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Response.CreatedBy = _UserName;
                    _Response.CreatedDateTime = _XmlCreatedDate;
                }

                if (string.IsNullOrWhiteSpace(_Response.ModifiedBy) || _Response.ModifiedBy == Functions.systemUserName)
                {
                    _Response.ModifiedBy = _UserName;
                    _Response.ModifiedDateTime = _XmlCreatedDate;
                }

                _Response.RequestedStudent = ParseAcadRecK12StudentType(highSchoolTranscript.Student);

                foreach (Library.Apas.AcademicRecord.AcademicRecordType rec in highSchoolTranscript.Student.AcademicRecord)
                {
                    AcademicRecord _AcademicRecord = ParseAcadRec(rec, _Response);
                    if (_AcademicRecord.AcademicRecordId < 1 || !_Response.AcademicRecords.Any(x => x.AcademicRecordId == _AcademicRecord.AcademicRecordId))
                    {
                        _Response.AcademicRecords.Add(_AcademicRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseHighSchoolTranscriptType", "Error", ex.ToString());
            }

            return _Response;
        }

        public bool AddAdmissionApasMessage(AdmissionOutstandingRequest request)
        {
            bool success = false;
            ApasMessage _ApasMessage = new ApasMessage();
            try
            {
                var apasMessage = ctx.ApasMessages.Where(x => x.UUID.Trim().ToUpper() == request.UUID.Trim().ToUpper() && x.MessageType == Enums.MessageTypes.SentRequest);

                if (!apasMessage.Any())
                {
                    _ApasMessage.UUID = request.UUID;
                    _ApasMessage.ASN = request.ASN;
                    _ApasMessage.MessageType = Enums.MessageTypes.SentRequest;
                    _ApasMessage.MessageXML = request.MessageXML;
                    _ApasMessage.SenderId = request.SenderId;
                    _ApasMessage.ReceiverId = request.ReceiverId;
                    _ApasMessage.CreatedBy = request.CreatedBy;
                    _ApasMessage.CreatedDateTime = request.CreatedDateTime;
                    _ApasMessage.ModifiedBy = request.ModifiedBy;
                    _ApasMessage.ModifiedDateTime = request.ModifiedDateTime;

                    ctx.ApasMessages.Add(_ApasMessage);
                    ctx.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "AddAdmissionApasMessage", "Error", ex.ToString());
            }

            return success;
        }

        public bool AddRecordApasMessage(RecordsOutstandingRequest request)
        {
            bool success = false;
            ApasMessage _ApasMessage = new ApasMessage();
            try
            {
                var apasMessage = ctx.ApasMessages.Where(x => x.UUID.Trim().ToUpper() == request.UUID.Trim().ToUpper() && x.MessageType == Enums.MessageTypes.ReceivedRequest);

                if (!apasMessage.Any())
                {
                    _ApasMessage.UUID = request.UUID;
                    _ApasMessage.ASN = request.ASN;
                    _ApasMessage.MessageType = Enums.MessageTypes.ReceivedRequest;
                    _ApasMessage.MessageXML = request.MessageXML;
                    _ApasMessage.SenderId = request.SenderId;
                    _ApasMessage.ReceiverId = request.ReceiverId;
                    _ApasMessage.CreatedBy = request.CreatedBy;
                    _ApasMessage.CreatedDateTime = request.CreatedDateTime;
                    _ApasMessage.ModifiedBy = request.ModifiedBy;
                    _ApasMessage.ModifiedDateTime = request.ModifiedDateTime;

                    ctx.ApasMessages.Add(_ApasMessage);
                    ctx.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "AddRecordApasMessage", "Error", ex.ToString());
            }

            return success;
        }

        public bool AddRecordHoldResponseApasMessage(RecordsHoldRespons response)
        {
            bool success = false;
            ApasMessage _ApasMessage = new ApasMessage();
            try
            {
                var apasMessage = ctx.ApasMessages.Where(x => x.UUID.Trim().ToUpper() == response.UUID.Trim().ToUpper() && x.MessageType == Enums.MessageTypes.ReceivedRequest);

                if (!apasMessage.Any())
                {
                    _ApasMessage.UUID = response.UUID;
                    _ApasMessage.ASN = response.ASN;
                    _ApasMessage.MessageType = Enums.MessageTypes.ReceivedRequest;
                    _ApasMessage.MessageXML = response.MessageXML;
                    _ApasMessage.SenderId = response.SenderId;
                    _ApasMessage.ReceiverId = response.ReceiverId;
                    _ApasMessage.CreatedBy = response.CreatedBy;
                    _ApasMessage.CreatedDateTime = response.CreatedDateTime;
                    _ApasMessage.ModifiedBy = response.ModifiedBy;
                    _ApasMessage.ModifiedDateTime = response.ModifiedDateTime;

                    ctx.ApasMessages.Add(_ApasMessage);
                    ctx.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "AddRecordApasMessage", "Error", ex.ToString());
            }

            return success;
        }

        public bool AddAdmissionOfflineSearchResponseApasMessage(AdmissionOfflineSearchRespons response)
        {
            bool success = false;
            ApasMessage _ApasMessage = new ApasMessage();
            try
            {
                var apasMessage = ctx.ApasMessages.Where(x => x.UUID.Trim().ToUpper() == response.UUID.Trim().ToUpper() && x.MessageType == Enums.MessageTypes.ReceivedRequest);

                if (!apasMessage.Any())
                {
                    _ApasMessage.UUID = response.UUID;
                    _ApasMessage.ASN = response.ASN;
                    _ApasMessage.MessageType = Enums.MessageTypes.ReceivedRequest;
                    _ApasMessage.MessageXML = response.MessageXML;
                    _ApasMessage.SenderId = response.SenderId;
                    _ApasMessage.ReceiverId = response.ReceiverId;
                    _ApasMessage.CreatedBy = response.CreatedBy;
                    _ApasMessage.CreatedDateTime = response.CreatedDateTime;
                    _ApasMessage.ModifiedBy = response.ModifiedBy;
                    _ApasMessage.ModifiedDateTime = response.ModifiedDateTime;

                    ctx.ApasMessages.Add(_ApasMessage);
                    ctx.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "AddAdmissionOfflineSearchResponseApasMessage", "Error", ex.ToString());
            }

            return success;
        }

        public bool ReSendAlbertaInstitutionRequests()
        {
            bool success = false;
            try
            {
                var requests = ctx.Requests_Alberta_Education.OrderBy(x => x.CreatedDateTime).ToList();

                using (Logic.ApasLogic apasLogic = new ApasLogic())
                {
                    foreach (var request in requests)
                    {
                        TransmissionData transData = ctx.TransmissionDatas.Find(request.TransmissionData_TransmissionDataId);

                        success = apasLogic.SendTransferRequest(transData.Uuid);
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ReSendAlbertaInstitutionRequests", "Error", ex.ToString());
            }
            return success;
        }

        public bool ImportAdmissionOfflineSearchResponses()
        {
            bool success = false;
            try
            {
                var admissionOfflineSearchResponses = ctx.AdmissionOfflineSearchResponses.OrderBy(x => x.CreatedDateTime).ToList();

                foreach (var response in admissionOfflineSearchResponses)
                {
                    success = AddAdmissionOfflineSearchResponseApasMessage(response);

                    success = ParseResponseXml(response.UUID, Enums.MessageTypes.ReceivedResponse);
                }
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ImportRecordHoldResponses", "Error", ex.ToString());
            }
            return success;
        }

        public bool ImportRecordHoldResponses()
        {
            bool success = false;
            try
            {
                var recordsHoldResponses = ctx.RecordsHoldResponses.OrderBy(x => x.CreatedDateTime).ToList();

                foreach (var response in recordsHoldResponses)
                {
                    success = AddRecordHoldResponseApasMessage(response);

                    success = ParseResponseXml(response.UUID, Enums.MessageTypes.SentResponse);
                }
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ImportRecordHoldResponses", "Error", ex.ToString());
            }
            return success;
        }

        public bool ImportRecordRequests()
        {
            bool success = false;
            try
            {
                var recordsRequests = ctx.RecordsOutstandingRequests.OrderBy(x => x.CreatedDateTime).ToList();

                foreach (var request in recordsRequests)
                {
                    success = AddRecordApasMessage(request);

                    success = ParseRequestXml(request.UUID, Enums.MessageTypes.ReceivedRequest);
                }
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ImportAdmissionRequests", "Error", ex.ToString());
            }
            return success;
        }

        public bool ImportAdmissionRequests()
        {
            bool success = false;
            try
            {
                var admissionRequests = ctx.AdmissionOutstandingRequests.OrderBy(x => x.CreatedDateTime).ToList();

                foreach (var request in admissionRequests)
                {
                    success = AddAdmissionApasMessage(request);

                    success = ParseRequestXml(request.UUID, Enums.MessageTypes.SentRequest);
                }
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ImportAdmissionRequests", "Error", ex.ToString());
            }
            return success;
        }

        public bool ReParseAcademicRecordResponses()
        {
            bool success = false;
            List<string> docType = new List<string>();
            Enums.MessageTypes messageType = Enums.MessageTypes.ReceivedResponse;
            List<Enums.MessageTypes> messageTypeList = new List<Enums.MessageTypes>();
            DateTime backLoad = DateTime.Now.AddMonths(-3);  // Responses from the last 3 months 

            try
            {
                //docType.Add(Structs.DocumentType.CollegeTranscript);
                docType.Add(Structs.DocumentType.HighSchoolTranscript);

                messageTypeList.Add(Enums.MessageTypes.ReceivedResponse);
                //messageTypeList.Add(Enums.MessageTypes.SentResponse);

                List<Response> responses = ctx.Responses.Where(x => x.TransmissionData.DocumentTypeCode == Library.Apas.CoreMain.DocumentTypeCodeType.Response &&
                                                                    x.TransmissionData.ExportedDateTime == null &&  // Not yet exported
                                                                    x.TransmissionData.SourceInstitution_InstitutionId == 1377 &&  // Alberta Education
                                                                    x.TransmissionData.DestinationInstitution_InstitutionId == 317 &&  // Lethbridge College
                                                                    x.ReceivedDateTime != null &&
                                                                    (x.ResponseStatusType == null || x.ResponseStatusType == 0) &&
                                                                    x.RequestTrackingID != null &&
                                                                    x.CreatedDateTime >= backLoad // Responses from the last 3 months 
                                                                   //&& x.RequestedStudent.sNumbers.Any(s => s.sNumVal == "0536697")
                                                                   //&& !x.AcademicRecords.Any()
                                                                   ).OrderByDescending(x => x.CreatedDateTime).ToList();

                foreach (Response response in responses)
                {
                    if (docType.Contains(GetDocumentVersion(response.TransmissionData.Xml.ToString())) &&
                        response.TransmissionData.DocumentID.Length <= 35 && response.TransmissionData.RequestTrackingID.Length <= 35)
                    {
                        // Delete Old Courses
                        List<Course> deleteCourses = ctx.Courses.Where(c => c.AcademicRecord.Response_ResponseId == response.ResponseId).ToList();
                        ctx.Courses.RemoveRange(deleteCourses);
                        ctx.SaveChanges();

                        // Re-parse Responses
                        messageType = ctx.ApasMessages.Where(x => x.UUID == response.TransmissionData.Uuid && messageTypeList.Contains(x.MessageType)).OrderByDescending(o => o.CreatedDateTime).Select(s => s.MessageType).FirstOrDefault();
                        success = ParseApasMessage(response.TransmissionData.Uuid, messageType);
                    }
                }
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ReParseAcademicRecordResponses", "Error", ex.ToString());
            }
            return success;
        }

        public bool DeleteResponseCourses(string uuid)
        {
            bool success = false;
            List<string> docType = new List<string>();

            try
            {
                if (!string.IsNullOrWhiteSpace(uuid))
                {
                    docType.Add(Structs.DocumentType.CollegeTranscript);
                    docType.Add(Structs.DocumentType.HighSchoolTranscript);

                    Response response = ctx.Responses.Where(x => x.TransmissionData.Uuid == uuid).OrderByDescending(o => o.CreatedDateTime).FirstOrDefault();

                    if (response != null && response.TransmissionData != null &&
                        docType.Contains(GetDocumentVersion(response.TransmissionData.Xml.ToString())) &&
                        response.TransmissionData.DocumentID.Length <= 35 && response.TransmissionData.RequestTrackingID.Length <= 35)
                    {
                        // Delete Old Courses
                        List<Course> deleteCourses = ctx.Courses.Where(c => c.AcademicRecord.Response_ResponseId == response.ResponseId).ToList();
                        if (deleteCourses != null && deleteCourses.Any())
                        {
                            ctx.Courses.RemoveRange(deleteCourses);
                            ctx.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "DeleteResponseCourses", "Error", ex.ToString());
            }
            return success;
        }

        public bool ParseApasMessage(string uuid, Enums.MessageTypes messageType)
        {
            bool success = false;

            try
            {
                // internal transmission type is saved in apasMessages, not documentType
                var apasMessages = ctx.ApasMessages.Where(x => x.UUID == uuid && x.MessageType == messageType);

                if (apasMessages.Any())
                {
                    string xml = apasMessages.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault().MessageXML;

                    switch (messageType)
                    {
                        case Enums.MessageTypes.ReceivedResponse:
                        case Enums.MessageTypes.SentResponse:
                            success = ParseResponseXml(uuid, messageType);
                            break;
                        case Enums.MessageTypes.ReceivedRequest:
                        case Enums.MessageTypes.SentRequest:
                            success = ParseRequestXml(uuid, messageType);
                            break;
                        case Enums.MessageTypes.Application:
                            success = ParseApplicationMessage(uuid);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseRequests", "Error", ex.ToString());
            }
            return success;
        }

        public bool ParseRequestXml(string uuid, Enums.MessageTypes messageType)
        {
            bool success = false;

            try
            {
                string xml = ctx.ApasMessages.Where(x => x.UUID == uuid).Select(y => y.MessageXML).FirstOrDefault();

                if (GetDocumentVersion(xml) == Structs.XmlDocType.Request)
                {
                    Library.Apas.TranscriptRequest.TranscriptRequest _TranscriptRequest = DeserializeTranscriptRequest(xml);

                    TransmissionData _TransmissionData = ParseTransmissionData(_TranscriptRequest.TransmissionData, uuid, messageType);

                    if (_TransmissionData.DocumentID != null)
                    {
                        _TransmissionData.Xml = xml;

                        foreach (Library.Apas.AcademicRecord.RequestType request in _TranscriptRequest.Request)
                        {
                            Request _Request = new Request();
                            List<Recipient> _Recipients = new List<Recipient>();

                            Student _Student = ParseStudentType(request.RequestedStudent);

                            foreach (Library.Apas.AcademicRecord.RecipientType recipient in request.Recipient)
                            {
                                Recipient _Recipient = ParseRecipient(recipient);
                                _Recipients.Add(_Recipient);

                                if (!string.IsNullOrWhiteSpace(recipient.RequestTrackingID))
                                {
                                    _TransmissionData.RequestTrackingID = recipient.RequestTrackingID;
                                }
                            }

                            var recList = _Recipients.Select(x => x.RecipientId).ToList();
                            var requests = ctx.Requests.Where(a => a.TransmissionData.TransmissionDataId == _TransmissionData.TransmissionDataId &&
                                                                   a.Student_StudentId == _Student.StudentId &&
                                                                   a.Recipients.Any(b => recList.Contains(b.RecipientId)));
                            if (requests.Any())
                            {
                                _Request = requests.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                            }
                            else
                            {
                                _Request.Recipients = _Recipients;
                                _Request.RequestedStudent = _Student;
                                _Request.CreatedBy = _UserName;
                                _Request.CreatedDateTime = _XmlCreatedDate;
                                _Request.TransmissionData = _TransmissionData;
                                ctx.Requests.Add(_Request);
                            }

                            if (string.IsNullOrWhiteSpace(_Request.ModifiedBy) || _Request.ModifiedBy == Functions.systemUserName)
                            {
                                _Request.ModifiedBy = _UserName;
                                _Request.ModifiedDateTime = _XmlCreatedDate;
                            }

                            // Save when Request was received
                            if (messageType == Enums.MessageTypes.ReceivedRequest && _Request.ReceivedDateTime == null)
                            {
                                _Request.ReceivedDateTime = _XmlCreatedDate;
                            }

                            // SentDateTime should be set after it is sent!!!
                            //_Request.SentDateTime = _XmlCreatedDate;

                            // TODO: Do I want to test/do this?
                            //if (request.Requestor.Item.GetType() == typeof(Library.Apas.AcademicRecord.RequestorReceiverOrganizationType)) {

                            //    Library.Apas.AcademicRecord.RequestorReceiverOrganizationType _RequestorReceiverOrganizationType = (Library.Apas.AcademicRecord.RequestorReceiverOrganizationType)request.Requestor.Item;

                            //    if (_RequestorReceiverOrganizationType.OrganizationName.Any()) {
                            //        new RequestorReceiver()
                            //        {
                            //            ReceiverType = "Institution",
                            //            Receiver = _RequestorReceiverOrganizationType.OrganizationName[0].ToString(),
                            //        };
                            //    }

                            //}
                            //else  if (request.Requestor.Item.GetType() == typeof(Library.Apas.AcademicRecord.PersonType)) {
                            //    Library.Apas.AcademicRecord.PersonType _PersonType = (Library.Apas.AcademicRecord.PersonType)request.Requestor.Item;

                            //    if (_PersonType.Name != null) {
                            //        new RequestorReceiver()
                            //        {
                            //            ReceiverType = "Person",
                            //            Receiver = _PersonType.Name.FirstName + " " + _PersonType.Name.LastName.ToString(),
                            //        };
                            //    }
                            //}                 

                            if (ctx.SaveChanges() > 0)
                            {
                                bool autoRespond = GetSetting(Structs.SettingTypes.Boolean, Structs.Settings.AutoRespond);
                                bool aliasStudentResponseOnly = IsActiveAliasStudent(_Request.RequestedStudent.StudentId, true);

                                // If a request was received for an active Alias (Everywhere) Student => autorespond
                                if (messageType == Enums.MessageTypes.ReceivedRequest && (autoRespond || aliasStudentResponseOnly))
                                {
                                    //SendAutoResponse(_Request.TransmissionData.Uuid);
                                    // Queue Auto Response
                                    QueueJob(_Request.TransmissionData.Uuid, Enums.JobTypeType.SendAutoResponse);
                                }

                                // Pre-load transcript response, speed up process getting transcript from Colleague before hand
                                PreLoadTranscriptResponse(_Request.TransmissionData.Uuid);
                            }
                        }

                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseRequestXml", "Error", ex.ToString());
            }
            return success;
        }

        public bool ParseResponseXml(string uuid, Enums.MessageTypes messageType)
        {
            bool success = false;
            try
            {
                string xml = ctx.ApasMessages.Where(x => x.UUID == uuid).Select(y => y.MessageXML).FirstOrDefault();

                Response _Response = null;
                TransmissionData _TransmissionData = new TransmissionData();
                List<NoteMessage> _NoteMessages = new List<NoteMessage>();

                switch (GetDocumentVersion(xml))
                {
                    case Structs.DocumentType.CollegeTranscript:
                        Library.Apas.CollegeTranscript.CollegeTranscript _CollegeTranscript = DeserializeCollegeTranscript(xml);

                        if (_CollegeTranscript.TransmissionData != null &&
                            ((_CollegeTranscript.TransmissionData.DocumentID != null && _CollegeTranscript.TransmissionData.DocumentID.Length > 35) || 
                             (_CollegeTranscript.TransmissionData.RequestTrackingID != null && _CollegeTranscript.TransmissionData.RequestTrackingID.Length > 35)))
                            break;

                        _TransmissionData = ParseTransmissionData(_CollegeTranscript.TransmissionData, uuid, messageType);

                        if (_TransmissionData.DocumentID != null)
                        {
                            DeleteResponseCourses(uuid);  // Delete any old courses

                            _Response = ParseCollegeTranscriptType(_CollegeTranscript, messageType);

                            ParseNoteMessageType(_CollegeTranscript.NoteMessage, ref _NoteMessages);
                        }

                        break;
                    case Structs.DocumentType.Response:
                        Library.Apas.TranscriptResponse.TranscriptResponse _TranscriptResponse = DeserializeTranscriptResponse(xml);

                        _TransmissionData = ParseTransmissionData(_TranscriptResponse.TransmissionData, uuid, messageType);

                        if (_TransmissionData.DocumentID != null && _TranscriptResponse.Response != null)
                        {
                            foreach (Library.Apas.AcademicRecord.ResponseType responseType in _TranscriptResponse.Response)
                            {
                                _Response = ParseResponseType(responseType, _TranscriptResponse.TransmissionData.DocumentID);

                                ParseNoteMessageType(responseType.NoteMessage, ref _NoteMessages);
                            }
                        }

                        break;
                    case Structs.DocumentType.HighSchoolTranscript:
                        Library.Apas.HighSchoolTranscript.HighSchoolTranscript _HighSchoolTranscript = DeserializeHighSchoolTranscript(xml);

                        _TransmissionData = ParseTransmissionData(_HighSchoolTranscript.TransmissionData, uuid, messageType);

                        if (_TransmissionData.DocumentID != null)
                        {
                            DeleteResponseCourses(uuid);  // Delete any old courses
                         
                            _Response = ParseHighSchoolTranscriptType(_HighSchoolTranscript);

                            ParseNoteMessageType(_HighSchoolTranscript.NoteMessage, ref _NoteMessages);
                        }

                        break;
                }

                if (_TransmissionData.DocumentID != null && _Response != null)
                {
                    // Make sure Response has the Request Tracking ID
                    if (_Response.RequestTrackingID == null && _TransmissionData.RequestTrackingID != null)
                    {
                        _Response.RequestTrackingID = _TransmissionData.RequestTrackingID;
                    }

                    // See if we have a matching request which we can save the response status to...
                    if (_Response.ResponseStatusType != null)
                    {
                        var recipient = ctx.Recipients.Where(x => x.RequestTrackingID == _Response.RequestTrackingID).OrderByDescending(y => y.ModifiedDateTime).FirstOrDefault();

                        if (recipient != null && !recipient.ResponseStatuses.Any(z => z.ResponseStatusType == _Response.ResponseStatusType))
                        {
                            recipient.ResponseStatuses.Add(new ResponseStatus()
                            {
                                CreatedBy = _UserName,
                                CreatedDateTime = _Response.CreatedDateTime,
                                ModifiedBy = _UserName,
                                ModifiedDateTime = _XmlCreatedDate,
                                ResponseStatusType = _Response.ResponseStatusType,
                            });
                        }
                    }

                    _TransmissionData.Xml = xml;
                    _TransmissionData.NoteMessages = _NoteMessages;

                    _Response.TransmissionData = _TransmissionData;

                    // Save when Response was received
                    if (messageType == Enums.MessageTypes.ReceivedResponse && _Response.ReceivedDateTime == null)
                    {
                        _Response.ReceivedDateTime = _XmlCreatedDate;
                    }

                    // Check if it's a new Response/Transcript
                    if (_Response.ResponseId < 1 || 
                        !ctx.Responses.Any(x => x.TransmissionData.TransmissionDataId == _Response.TransmissionData.TransmissionDataId &&
                                                x.RequestTrackingID == _Response.RequestTrackingID &&
                                                x.RequestedStudent.StudentId == _Response.RequestedStudent.StudentId))
                    {
                        ctx.Responses.Add(_Response);
                    }

                    ctx.SaveChanges();

                    success = true;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseResponseXml", "Error", ex.ToString());
            }

            return success;
        }

        public bool SendAutoResponse(string uuid)
        {
            bool success = false;

            try
            {
                // Retrieve Request
                Request _Request = GetRequest(uuid);

                if (_Request != null)
                {
                    // Load Student
                    StuLookupListViewObj _StudentLookup = new StuLookupListViewObj();
                    _StudentLookup.SearchFilter.SelectedStuID = _Request.RequestedStudent.StudentId;
                    _StudentLookup.SearchFilter.SelectedTransID = _Request.TransmissionData.TransmissionDataId;
                    _StudentLookup.SearchFilter.TransactionTranscriptUuid = _Request.TransmissionData.Uuid;
                    _StudentLookup.SearchFilter.DestAction = Structs.DestActions.CreateTranscript;  // Trying sending a Transcript at first

                    _StudentLookup.LoadObject();

                    // Populate CreateResponseObj and pass into CreateApasResponse
                    CreateMsgViewObj _ResponseObj = new CreateMsgViewObj(_StudentLookup.SearchFilter)
                    {
                        // Link Request to the Response
                        //TransDataID = _Request.TransmissionData.TransmissionDataId,
                        RequestTrackingId = _Request.TransmissionData.RequestTrackingID
                    };

                    // Load Destination Institution, Response Status and Restriction/Holds
                    _ResponseObj.LoadObject();

                    bool aliasStudentResponseOnly = IsActiveAliasStudent(_Request.RequestedStudent.StudentId, true);

                    // Check if the Request has a Hold Type (AfterGradePosted, AfterDegreeAwarded, ...)
                    bool holdType = ctx.TranscriptHolds.Where(x => x.Recipient.Request.RequestId == _Request.RequestId && x.HoldType != Library.Apas.AcademicRecord.HoldTypeType.Now).Any();

                    // Check if Student has academic records/sessions in Colleague
                    bool hasAcademicRecord = true;  // Assume it has academic record by default
                    var transactionTranscript = ctx.TransactionTranscripts.Where(x => x.TransactionTranscriptUuid == _ResponseObj.TransactionTranscriptUuid &&
                                                                                      x.StudentId == _ResponseObj.StudentRecord.Snumber)
                                                                          .OrderByDescending(o => o.CreatedDateTime).FirstOrDefault();
                    if (transactionTranscript != null && !string.IsNullOrWhiteSpace(transactionTranscript.TranscriptText) &&
                       !transactionTranscript.TranscriptText.Contains("<AcademicSession>"))
                    {
                        hasAcademicRecord = false;
                    }

                    // If it's not a Everywhere Student and Only 1 match was found into Colleague and no restrictions (Holds)
                    if (!aliasStudentResponseOnly && _StudentLookup.StudentRecords != null && _StudentLookup.StudentRecords.Count() == 1 &&
                        _ResponseObj.DestAction == Structs.DestActions.CreateTranscript && !_ResponseObj.StudentRestriction &&
                        !_ResponseObj.StudentMissingASN && !holdType && hasAcademicRecord)
                    {
                        // Respond with a Transcript
                        _ResponseObj.ResponseStatusType = Library.Apas.AcademicRecord.ResponseStatusType.TranscriptSent;
                    }
                    else
                    {
                        // Respond with a Response

                        // Don't send Transcript when Academic info is empty, send a response with "No Record" instead
                        if (!hasAcademicRecord)
                        {
                            _ResponseObj.DestAction = Structs.DestActions.CreateResponse;
                            _ResponseObj.ResponseStatusType = Library.Apas.AcademicRecord.ResponseStatusType.NoRecord;
                        }

                        //TODO: Don't include other types of responses to the automation yet (TEMPORARY)
                        //if (_ResponseObj.StudentRestriction)
                        //{
                        //    _ResponseObj.ResponseStatusType = Library.Apas.AcademicRecord.ResponseStatusType.Hold;
                        //}
                        //if (_ResponseObj.ResponseStatusType != null && _ResponseObj.ResponseStatusType == Library.Apas.AcademicRecord.ResponseStatusType.Hold)
                        //{
                        //    _ResponseObj.HoldReasonType = Library.Apas.AcademicRecord.HoldReasonType.Financial;
                        //}
                    }

                    _ResponseObj.DestinationDetails.ForEach(x => x.ResponseStatusType = _ResponseObj.ResponseStatusType);

                    // Send Transcript/Response if there is no transcript or response/status sent already
                    if (_ResponseObj.ResponseStatusType != null && GetResponseByRequestTrackingId(_ResponseObj.RequestTrackingId, _ResponseObj.ResponseStatusType) == null)
                    {
                        SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SendAutoResponse", "Send Transcript/Response", "UUID: " + uuid + ", RequestTrackingId: " + _ResponseObj.RequestTrackingId + ",  ResponseStatus: " + _ResponseObj.ResponseStatusType + ", DestAction: " + _ResponseObj.DestAction, false);

                        // Generate Transmission Data (Transcript or Response)
                        success = _ResponseObj.GeneratePreview();

                        // Send Transcript or Response
                        if (success && !string.IsNullOrWhiteSpace(_ResponseObj.TransmissionDataUUID))
                        {
                            _ResponseObj.Submit();
                        }
                    }
                    else
                    {
                        success = true;  // Complete Job, response was already sent or it has a hold type!
                        //SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SendAutoResponse", "Response already sent", "RequestTrackingId: " + _ResponseObj.RequestTrackingId + ",  ResponseStatus: " + _ResponseObj.ResponseStatusType, false);
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SendAutoResponse", "Error", ex.ToString());
            }
            return success;
        }

        public bool PreLoadTranscriptResponse(string uuid)
        {
            bool success = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(uuid))
                {
                    // Retrieve Request
                    Request _Request = GetRequest(uuid);

                    if (_Request != null)
                    {
                        // Load Student
                        StuLookupListViewObj _StudentLookup = new StuLookupListViewObj();
                        _StudentLookup.SearchFilter.SelectedStuID = _Request.RequestedStudent.StudentId;
                        _StudentLookup.SearchFilter.SelectedTransID = _Request.TransmissionData.TransmissionDataId;
                        _StudentLookup.SearchFilter.TransactionTranscriptUuid = _Request.TransmissionData.Uuid;
                        _StudentLookup.SearchFilter.DestAction = Structs.DestActions.CreateTranscript;  // Trying sending a Transcript at first

                        _StudentLookup.LoadObject();  // The load will queue the GetColleagueTranscript call

                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "PreLoadTranscriptResponse", "Error", ex.ToString());
            }
            return success;
        }

        public bool SaveStudentsToBulkSendTranscript(string[] selectedSnumbers, int? institutionId = null, bool allSelected = false, string filterFields = null)
        {
            bool success = false;
            List<string> sNumberList = new List<string>();

            try
            {
                if (selectedSnumbers != null && selectedSnumbers.Count() > 0 && institutionId != null)
                {
                    // Deserialize filter fields
                    UnsolicitedBatchListViewObj _UnsolicitedBatchListViewObj = new UnsolicitedBatchListViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _UnsolicitedBatchListViewObj.SearchFilter = JsonConvert.DeserializeObject<UnsolicitedBatchSearchObj>(filterFields);
                    };

                    using (ColleagueLogic collLogic = new ColleagueLogic())
                    {
                        var query = collLogic.UnsolicitedBatchTranscriptQuery(_UnsolicitedBatchListViewObj);

                        if (!allSelected)
                        {
                            // filter by Selected Ids
                            query = query.Where(a => selectedSnumbers.Contains(a.sNumber.ToUpper().Trim())).ToList();
                        }

                        sNumberList = query.OrderBy(x => x.FullName).Select(s => s.sNumber).ToList();
                    };

                    foreach (string sNumber in sNumberList)
                    {
                        // Check if it was already created and not sent yet
                        var query = ctx.BulkSendTranscripts.Where(x => x.sNumber.Trim().ToUpper() == sNumber.Trim().ToUpper() &&
                                                                        x.Institution_InstitutionId == institutionId &&
                                                                        x.SentDateTime == null);
                        if (!query.Any())
                        {
                            // If it doesn't exists, create a record
                            BulkSendTranscript _BulkSendTranscript = new BulkSendTranscript()
                            {
                                sNumber = sNumber,
                                Institution_InstitutionId = institutionId,
                                CreatedBy = _UserName,
                                CreatedDateTime = DateTime.Now,
                                ModifiedBy = _UserName,
                                ModifiedDateTime = DateTime.Now,
                            };

                            ctx.BulkSendTranscripts.Add(_BulkSendTranscript);
                            success = true;
                        }
                    }
                    if (success) ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveStudentsToBulkSendTranscript", "Error", ex.ToString());
                success = false;
            }
            return success;
        }

        public bool QueueSendBulkTranscript()
        {
            bool success = false;

            try
            {
                // Retrieve List of sNumbers
                List<BulkSendTranscript> sNumberList = GetBulkSnumberList();

                foreach (var _sNumber in sNumberList)
                {
                    // Load Student and queue get colleague transcript
                    StuLookupListViewObj _StudentLookup = new StuLookupListViewObj();
                    _StudentLookup.SearchFilter.StudentRecord.Snumber = _sNumber.sNumber;
                    _StudentLookup.SearchFilter.DestAction = Structs.DestActions.CreateTranscript;  // Trying sending a Transcript at first

                    _StudentLookup.LoadObject();

                    // Queue each individual transcript transmission
                    success = QueueJob(_StudentLookup.SearchFilter.TransactionTranscriptUuid, Enums.JobTypeType.SendBulkTranscript);
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "QueueSendBulkTranscript", "Error", ex.ToString());
            }
            return success;
        }

        public bool SendBulkTranscript(string transactionTranscriptUuid)
        {
            bool success = false;

            try
            {
                // Retrieve sNumber
                string sNumber = GetStuIdByTranTranUuid(transactionTranscriptUuid, true);
                BulkSendTranscript _BulkTranscript = GetBulkSnumber(sNumber);

                if (_BulkTranscript != null)
                {
                    // Load Student
                    StuLookupListViewObj _StudentLookup = new StuLookupListViewObj();
                    _StudentLookup.SearchFilter.StudentRecord.Snumber = _BulkTranscript.sNumber;
                    _StudentLookup.SearchFilter.TransactionTranscriptUuid = transactionTranscriptUuid;
                    _StudentLookup.SearchFilter.DestAction = Structs.DestActions.CreateTranscript;  // Trying sending a Transcript at first

                    _StudentLookup.LoadObject();

                    // Populate CreateResponseObj and pass into CreateApasResponse
                    CreateMsgViewObj _ResponseObj = new CreateMsgViewObj(_StudentLookup.SearchFilter)
                    {
                        // Set Destination Institution
                        PreferredDestination = _BulkTranscript.Institution,
                    };

                    // Load Destination Institution, Response Status and Restriction/Holds
                    _ResponseObj.LoadObject();

                    // If it's not a Everywhere Student and Only 1 match was found into Colleague and no restrictions (Holds)
                    if (_StudentLookup.StudentRecords != null && _StudentLookup.StudentRecords.Count() == 1 &&
                        _ResponseObj.DestAction == Structs.DestActions.CreateTranscript && !_ResponseObj.StudentRestriction && !_ResponseObj.StudentMissingASN)
                    {
                        _ResponseObj.ResponseStatusType = Library.Apas.AcademicRecord.ResponseStatusType.TranscriptSent;
                    }
                    // TODO: Don't include responses to the automation (TEMPORARY)
                    //else
                    //{
                    //    // Respond with a Response
                    //    _ResponseObj.DestAction = Structs.DestActions.CreateResponse;
                    //    if (_ResponseObj.StudentRestriction)
                    //    {
                    //        _ResponseObj.ResponseStatusType = Library.Apas.AcademicRecord.ResponseStatusType.Hold;
                    //    }
                    //    if (_ResponseObj.ResponseStatusType != null && _ResponseObj.ResponseStatusType == Library.Apas.AcademicRecord.ResponseStatusType.Hold)
                    //    {
                    //        _ResponseObj.HoldReasonType = Library.Apas.AcademicRecord.HoldReasonType.Financial;
                    //    }
                    //}

                    _ResponseObj.DestinationDetails.ForEach(x => x.ResponseStatusType = _ResponseObj.ResponseStatusType);

                    // Send Transcript/Response if there is no transcript or response/status sent already
                    if (_ResponseObj.ResponseStatusType != null && GetResponseByRequestTrackingId(_ResponseObj.RequestTrackingId, _ResponseObj.ResponseStatusType) == null)
                    {
                        SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SendBulkTranscript", "Send Transcript/Response", "sNumber: " + _ResponseObj.StudentRecord.Snumber + " Institution: " + _ResponseObj.DestinationDetails.FirstOrDefault().Destination.InstitutionName + " RequestTrackingId: " + _ResponseObj.RequestTrackingId + ",  ResponseStatus: " + _ResponseObj.ResponseStatusType + ", DestAction: " + _ResponseObj.DestAction, false);

                        // Generate Transmission Data (Transcript or Response)
                        success = _ResponseObj.GeneratePreview();

                        // Send Transcript or Response
                        if (success && !string.IsNullOrWhiteSpace(_ResponseObj.TransmissionDataUUID))
                        {
                            _ResponseObj.Submit();
                        }

                        // Save Sent DateTime
                        if (success)
                        {
                            UpdateBulkSendTranscript(ref _BulkTranscript, _ResponseObj.TransmissionDataUUID, _ResponseObj.StudentRestriction);
                        }
                    }
                    else
                    {
                        // Saving Student Restriction and mark the trascript sent to not reprocess it
                        if (_ResponseObj.StudentRestriction)
                        {
                            UpdateBulkSendTranscript(ref _BulkTranscript, _ResponseObj.TransmissionDataUUID, _ResponseObj.StudentRestriction);
                        }
                        success = true;  // Complete Job, response was already sent or it has a hold type!
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SendBulkTranscript", "Error", ex.ToString());
            }
            return success;
        }

        public bool UpdateBulkSendTranscript(ref BulkSendTranscript _BulkTranscript, string transDataUUID, bool studentRestriction = false)
        {
            bool success = false;
            try
            {
                _BulkTranscript.SentDateTime = DateTime.Now;
                _BulkTranscript.ModifiedBy = _UserName;
                _BulkTranscript.ModifiedDateTime = DateTime.Now;
                if (string.IsNullOrWhiteSpace(_BulkTranscript.CreatedBy)) { _BulkTranscript.CreatedBy = _UserName; }
                if (_BulkTranscript.CreatedDateTime == null) { _BulkTranscript.CreatedDateTime = DateTime.Now; }
                _BulkTranscript.StudentRestriction = studentRestriction;
                // Save Student
                _BulkTranscript.Student = GetStudentByResponseTransmissionDataUUID(transDataUUID);
                // Save sNumber into the tollkit database
                SaveStudentSNumber(_BulkTranscript.Student.StudentId, _BulkTranscript.sNumber);

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "UpdateBulkSendTranscript", "Error", ex.ToString());
            }

            return success;
        }

        public bool QueueLookupSNumberByANS()
        {
            bool success = false;
            int maxSizeList = GetSetting(Structs.SettingTypes.Integer, Structs.Settings.JobsMaxSizeListSNumberLookup) ?? 100;

            try
            {
                // Retrieve List of Students that are missing sNumbers
                List<int> _StudentIdList = GetStudentIdMissingSnumber(maxSizeList);

                foreach (int _StudentId in _StudentIdList)
                {
                    // Queue each Student ASN to lookup in Colleague for the sNumber
                    success = QueueJob(_StudentId.ToString(), Enums.JobTypeType.LookupSNumberByASN);
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "QueueLookupSNumberByANS", "Error", ex.ToString());
            }
            return success;
        }

        public bool QueueMyCredsTRRQRequestResponses()
        {
            bool success = false;
            int maxSizeList = GetSetting(Structs.SettingTypes.Integer, Structs.Settings.JobsMaxSizeListSNumberLookup) ?? 100;
            bool MyCredsAutoRespond = GetSetting(Structs.SettingTypes.Boolean, Structs.Settings.MyCredsAutoRespond) ?? false;
            List<string> _RequestIdList = new List<string>();

            try
            {
                if (MyCredsAutoRespond)
                {
                    // Retrieve List of TRRQ Pending Requests
                    using (ColleagueLogic collLogic = new ColleagueLogic())
                    {
                        _RequestIdList = collLogic.GetMyCredsPendingTRRQRequests(maxSizeList);
                    }

                    foreach (string _RequestId in _RequestIdList)
                    {
                        // Queue each Response
                        success = QueueJob(_RequestId, Enums.JobTypeType.QueueMyCredsTRRQRequestResponses);
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "QueueMyCredsTRRQRequestResponses", "Error", ex.ToString());
            }
            return success;
        }

        public bool UpdateStudentMissingSNumber(string studentId)
        {
            bool success = false;
            Int32 intStudentId;

            try
            {
                if (!string.IsNullOrWhiteSpace(studentId) && Int32.TryParse(studentId, out intStudentId))
                {
                    // Retrieve Student by the Id
                    Student _Student = GetStudent(intStudentId);

                    // Check if Student has no sNumber but has ASN to be used in the lookup
                    if (_Student != null && !_Student.sNumbers.Any() && _Student.ASNs.Any())
                    {
                        string _sNumberVal = string.Empty;
                        List<string> _ASNs = null;

                        // Get ASN numbers, loop throught them until find and sNumber in Colleague
                        _ASNs = _Student.ASNs.OrderByDescending(o => o.CreatedDateTime).Select(s => s.AgencyAssignedID).ToList();
                        using (ColleagueLogic collLogic = new ColleagueLogic())
                        {
                            foreach (string _ASN in _ASNs)
                            {
                                if (string.IsNullOrWhiteSpace(_sNumberVal))
                                {
                                    // Retrieve sNumber from Colleague
                                    _sNumberVal = collLogic.GetStudentSNumberByASN(_ASN);
                                }
                            }
                        };

                        // sNumber found in Colleague
                        if (!string.IsNullOrWhiteSpace(_sNumberVal))
                        {
                            // Save sNumber
                            sNumber _sNumber = new sNumber()
                            {
                                sNumVal = _sNumberVal,
                                CreatedBy = _UserName,
                                CreatedDateTime = DateTime.Now,
                            };

                            _Student.sNumbers.Add(_sNumber);
                            ctx.SaveChanges();
                            success = true;
                        } else
                        {
                            // If not sNumber was found just close the job
                            success = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "UpdateStudentMissingSNumber", "Error", ex.ToString());
            }
            return success;
        }

        public bool SaveRequestMatchedStudent(string transDataUuid, int studentId)
        {
            bool success = false;
            try
            {
                Request _Request = ctx.Requests.Where(x => x.TransmissionData.Uuid == transDataUuid).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(_Request.ModifiedBy) || _Request.ModifiedBy == Functions.systemUserName)
                {
                    _Request.ModifiedBy = _UserName;
                    _Request.ModifiedDateTime = _XmlCreatedDate;
                }
                _Request.Matched_StudentId = studentId;

                if (ctx.SaveChanges() > 0)
                {
                    success = true;
                };
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveRequestMatchedStudent", "Error", ex.ToString());
            }
            return success;
        }

        public bool SaveResponseMatchedStudent(string transDataId, int studentId)
        {
            bool success = false;
            try
            {
                if (!string.IsNullOrWhiteSpace(transDataId) && studentId > 0)
                {
                    Response _Response = ctx.Responses.Where(x => x.TransmissionData.TransmissionDataId.ToString() == transDataId).FirstOrDefault();
                    if (_Response != null) {
                        if (string.IsNullOrWhiteSpace(_Response.ModifiedBy) || _Response.ModifiedBy == Functions.systemUserName)
                        {
                            _Response.ModifiedBy = _UserName;
                            _Response.ModifiedDateTime = _XmlCreatedDate;
                        }
                        _Response.Matched_StudentId = studentId;

                        if (ctx.SaveChanges() > 0)
                        {
                            success = true;
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveResponseMatchedStudent", "Error", ex.ToString());
            }
            return success;
        }

        public bool SaveRequestXml(string requestTrackingId, string xml)
        {
            bool success = false;
            try
            {
                Request _Request = ctx.Requests.Where(x => x.TransmissionData.RequestTrackingID == requestTrackingId).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(_Request.ModifiedBy) || _Request.ModifiedBy == Functions.systemUserName)
                {
                    _Request.ModifiedBy = _UserName;
                    _Request.ModifiedDateTime = _XmlCreatedDate;
                }
                _Request.TransmissionData.Xml = xml;
                _Request.TransmissionData.ModifiedBy = _UserName;
                _Request.TransmissionData.ModifiedDateTime = DateTime.Now;

                if (ctx.SaveChanges() > 0)
                {
                    success = true;
                };
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveRequestXml", "Error", ex.ToString());
            }
            return success;
        }

        public bool SaveResponseXml(string requestTrackingId, string xml)
        {
            bool success = false;
            try
            {
                Response _Response = ctx.Responses.Where(x => x.RequestTrackingID == requestTrackingId).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(_Response.ModifiedBy) || _Response.ModifiedBy == Functions.systemUserName)
                { 
                    _Response.ModifiedBy = _UserName;
                    _Response.ModifiedDateTime = _XmlCreatedDate;
                }

                _Response.TransmissionData.Xml = xml;
                _Response.TransmissionData.ModifiedBy = _UserName;
                _Response.TransmissionData.ModifiedDateTime = _XmlCreatedDate;

                if (ctx.SaveChanges() > 0)
                {
                    success = true;
                };
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveResponseXml", "Error", ex.ToString());
            }
            return success;
        }

        public Recipient ParseRecipient(Library.Apas.AcademicRecord.RecipientType recipientType)
        {
            Recipient _Recipient = new Recipient();

            try
            {
                _Recipient.RequestTrackingID = recipientType.RequestTrackingID ?? string.Empty;

                var recipients = ctx.Recipients.Where(x => x.RequestTrackingID == _Recipient.RequestTrackingID);

                if (recipients.Any())
                {
                    _Recipient = recipients.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Recipient.CreatedBy = _UserName;
                    _Recipient.CreatedDateTime = _XmlCreatedDate;
                }

                _Recipient.ModifiedBy = _UserName;
                _Recipient.ModifiedDateTime = _XmlCreatedDate;

                if (recipientType.TranscriptHold != null)
                {
                    _Recipient.TranscriptHolds.Add(ParseTranscriptHoldType(recipientType.TranscriptHold));
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.TranscriptsController, "ParseRecipient", "Error", ex.ToString());
            }

            return _Recipient;
        }

        /// <summary>
        /// Parse/Create TranscriptHoldType
        /// </summary>
        /// <param name="holdtype"></param>
        /// <returns></returns>
        public TranscriptHold ParseTranscriptHoldType(Library.Apas.AcademicRecord.TranscriptHoldType holdtype)
        {
            TranscriptHold _TranscriptHold = new TranscriptHold();

            try
            {
                //var holdMatches = ctx.TranscriptHolds.AsQueryable();

                //holdMatches = holdMatches.Where(x => x.HoldType == holdtype.HoldType);
                //if (holdtype.ReleaseDateSpecified && holdtype.ReleaseDate != null) { holdMatches = holdMatches.Where(x => x.ReleaseDate == holdtype.ReleaseDate); }
                //if (holdtype.SessionName != null) { holdMatches = holdMatches.Where(x => x.SessionName == holdtype.SessionName); }
                //if (holdtype.SessionDesignator != null) { holdMatches = holdMatches.Where(x => x.SessionDesignator == holdtype.SessionDesignator); }               
                //if (!string.IsNullOrWhiteSpace(holdtype.NoteMessage)) { holdMatches = holdMatches.Where(x => x.HoldTypeData == holdtype.NoteMessage); }    



                //if (holdMatches.Any())
                //{
                //    // found a match
                //    _TranscriptHold = holdMatches.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                //}
                //else
                //{
                // create new record
                _TranscriptHold.CreatedBy = _UserName;
                _TranscriptHold.CreatedDateTime = _XmlCreatedDate;

                _TranscriptHold.HoldType = holdtype.HoldType;

                if (Functions.CheckForNullDate(holdtype.ReleaseDate) != null) { _TranscriptHold.ReleaseDate = holdtype.ReleaseDate; }
                if (!string.IsNullOrWhiteSpace(holdtype.SessionName)) { _TranscriptHold.SessionName = holdtype.SessionName; }
                if (!string.IsNullOrWhiteSpace(holdtype.SessionDesignator)) { _TranscriptHold.SessionDesignator = holdtype.SessionDesignator; }
                if (!string.IsNullOrWhiteSpace(holdtype.NoteMessage)) { _TranscriptHold.HoldTypeData = holdtype.NoteMessage; }
                //}

                _TranscriptHold.ModifiedBy = _UserName;
                _TranscriptHold.ModifiedDateTime = _XmlCreatedDate;

                foreach (string courseTitle in holdtype.CourseTitle)
                {
                    CourseTitle _CourseTitle = ParseCourseTitle(courseTitle);
                    if (_CourseTitle.CourseTitleId < 1 || !_TranscriptHold.CourseTitles.Any(x => x.CourseTitleId == _CourseTitle.CourseTitleId))
                    {
                        _TranscriptHold.CourseTitles.Add(_CourseTitle);
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.TranscriptsController, "ParseTranscriptHoldType", "Error", ex.ToString());
            }

            return _TranscriptHold;
        }

        /// <summary>
        /// Parse/Create CourseTitle
        /// </summary>
        /// <param name="courseTitle"></param>
        /// <returns></returns>
        public CourseTitle ParseCourseTitle(string courseTitle)
        {
            CourseTitle _CourseTitle = new CourseTitle();

            try
            {
                _CourseTitle.CourseTitleValue = courseTitle;

                var courseTitles = ctx.CourseTitles.Where(c => c.CourseTitleValue == courseTitle);

                if (courseTitles.Any())
                {
                    _CourseTitle = courseTitles.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _CourseTitle.CreatedBy = _UserName;
                    _CourseTitle.CreatedDateTime = _XmlCreatedDate;
                }

                _CourseTitle.ModifiedBy = _UserName;
                _CourseTitle.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.TranscriptsController, "ParseCourseTitle", "Error", ex.ToString());
            }
            return _CourseTitle;
        }

        public Request GetRequestByRequestTrackingId(string requestTrackingId)
        {
            Request _Request = null;

            try
            {
                _Request = ctx.Requests.Where(x => x.TransmissionData.RequestTrackingID == requestTrackingId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.TranscriptsController, "GetRequestByRequestTrackingId", "Error", ex.ToString());
            }
            return _Request;
        }

        public Response GetResponseByRequestTrackingId(string requestTrackingId, Library.Apas.AcademicRecord.ResponseStatusType? responseStatus = null)
        {
            Response _Response = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(requestTrackingId))
                {
                    var result = ctx.Responses.Where(x => x.TransmissionData.RequestTrackingID == requestTrackingId);
                    if (result.Any())
                    {
                        if (responseStatus != null)
                        {
                            result = result.Where(x => x.ResponseStatusType == responseStatus);
                        }

                        _Response = result.OrderByDescending(y => y.CreatedDateTime).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.TranscriptsController, "GetResponseByRequestTrackingId", "Error", ex.ToString());
            }
            return _Response;
        }

        #region Parse AdmissionsRecord

        /// <summary>
        /// Parse/Update Applicant
        /// </summary>
        /// <param name="applicant"></param>
        /// <returns></returns>
        public Student ParseApplicant(Library.Apas.AdmissionsRecord.ApplicantType applicant, bool? collegeData = true)
        {
            Student _Student = new Student();

            try
            {
                // Parse Person
                Person _Person = ParseAdmRecApplicantPersonType(applicant.Person, collegeData);

                if (_Person.PersonId < 1 || !_Person.Students.Any())
                {
                    _Student.CreatedBy = _UserName;
                    _Student.CreatedDateTime = _XmlCreatedDate;
                    _Student.Person = _Person;
                }
                else
                {
                    _Student = _Person.Students.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }

                _Student.ModifiedBy = _UserName;
                _Student.ModifiedDateTime = _XmlCreatedDate;

                // Parse ANSs
                if (applicant.Person != null && applicant.Person.AgencyIdentifier != null && applicant.Person.AgencyIdentifier.Count() > 0)
                {
                    foreach (Library.Apas.CoreMain.AgencyIdentifierType agencyIdentifier in applicant.Person.AgencyIdentifier)
                    {
                        ASN _ASN = ParseASN(agencyIdentifier, _Student.StudentId, collegeData);
                        if (_ASN.ASNId < 1 || !_Student.ASNs.Any(x => x.ASNId == _ASN.ASNId))
                        {
                            _Student.ASNs.Add(_ASN);
                        }
                    }
                }

                // Parse Family Member Attended College
                if (applicant.Family != null)
                {
                    if (applicant.Family.FamilyMember.AttendedCollegeCode != null)
                    {
                        _Student.FamilyAttendedCollege = applicant.Family.FamilyMember.AttendedCollegeCode;
                    }
                    else
                    {
                        // No family member attendance indicated
                        _Student.FamilyAttendedCollege = Library.Apas.CoreMain.AttendedCollegeCodeType.NoFamilyMemberAttendance;
                    }
                }

                // Parse SelfReportedAcademicRecord, they are part of the Applicant and not Application anymore.
                if (applicant.SelfReportedAcademicRecord != null)
                {
                    foreach (Library.Apas.AdmissionsRecord.AcademicRecordType academicRecord in applicant.SelfReportedAcademicRecord)
                    {
                        AcademicRecord _AcademicRecord = ParseAdmRecSelfReportedAcademicRecordType(applicant.Application.FirstOrDefault(), academicRecord);
                        if (_AcademicRecord.AcademicRecordId < 1 || _AcademicRecord.Student.StudentId != _Student.StudentId)
                        {
                            _Student.AcademicRecords.Add(_AcademicRecord);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseApplicant", "Error", ex.ToString());
            }

            return _Student;
        }

        /// <summary>
        /// Parse/Update Student Application
        /// </summary>
        /// <param name="applicant"></param>
        /// <returns></returns>
        public StudentApplication ParseApplication(Library.Apas.AdmissionsRecord.ApplicantType applicant)
        {
            StudentApplication _StudentApplication = new StudentApplication();
            try
            {
                // Parse Application
                foreach (Library.Apas.AdmissionsRecord.ApplicationType application in applicant.Application)
                {

                    var studentApplications = ctx.StudentApplications.Where(x => x.ApplicationID == application.ApplicationID);

                    if (studentApplications.Any())
                    {
                        _StudentApplication = studentApplications.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                    }
                    else
                    {
                        _StudentApplication.CreatedBy = _UserName;
                        _StudentApplication.CreatedDateTime = _XmlCreatedDate;
                    }

                    _StudentApplication.ModifiedBy = _UserName;
                    _StudentApplication.ModifiedDateTime = _XmlCreatedDate;
                    _StudentApplication.ApplicationID = application.ApplicationID;
                    _StudentApplication.SessionDesignator = application.ApplicationDetail.FirstOrDefault().ApplicationSession.SessionDesignator;
                    _StudentApplication.ApplicationSource = application.ApplicationSource.ToString();
                    _StudentApplication.ApplicationFee = GetApplicationFee();

                    _StudentApplication.Student = ParseApplicant(applicant);
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseApplication", "Error", ex.ToString());
            }
            return _StudentApplication;
        }

        /// <summary>
        /// Parse/Update Academic Award
        /// </summary>
        /// <param name="academicAward"></param>
        /// <returns></returns>
        public AcademicAward ParseAdmRecAcademicAwardType(string appId, Library.Apas.AdmissionsRecord.AcademicAwardType academicAward)
        {
            AcademicAward _AcademicAward = new AcademicAward();
            try
            {
                _AcademicAward.AcademicAwardLevel = academicAward.AcademicAwardLevel;  // .GetXmlEnumAttribute(typeof(Library.Apas.CoreMain.AcademicAwardLevelType));
                _AcademicAward.AcademicAwardTitle = academicAward.AcademicAwardTitle;
                _AcademicAward.AcademicAwardDate = Functions.CheckForNullDate(academicAward.AcademicAwardDate);

                var academicAwards = ctx.AcademicAwards.Where(x => x.AcademicAwardLevel == _AcademicAward.AcademicAwardLevel &&
                                                                   x.AcademicAwardTitle == _AcademicAward.AcademicAwardTitle &&
                                                                   x.AcademicAwardDate == _AcademicAward.AcademicAwardDate &&
                                                                   x.AcademicRecord.Student.StudentApplications.Any(y => y.ApplicationID == appId));

                if (academicAwards.Any())
                {
                    _AcademicAward = academicAwards.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _AcademicAward.CreatedBy = _UserName;
                    _AcademicAward.CreatedDateTime = _XmlCreatedDate;
                }

                if (!string.IsNullOrWhiteSpace(academicAward.AcademicAwardTitle))
                {
                    _AcademicAward.AcademicAwardTitle = academicAward.AcademicAwardTitle;
                }

                _AcademicAward.ModifiedBy = _UserName;
                _AcademicAward.ModifiedDateTime = _XmlCreatedDate;

            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAdmRecAcademicAwardType", "Error", ex.ToString());
            }
            return _AcademicAward;
        }

        /// <summary>
        /// Parse/Update Academic Record
        /// </summary>
        /// <param name="academicRecord"></param>
        /// <returns></returns>
        public AcademicRecord ParseAdmRecSelfReportedAcademicRecordType(Library.Apas.AdmissionsRecord.ApplicationType application, Library.Apas.AdmissionsRecord.AcademicRecordType academicRecord)
        {
            AcademicRecord _NewRecord = new AcademicRecord();

            try
            {
                // Parse School
                if (academicRecord.School != null)
                {
                    _NewRecord.School = ParseAdmRecSchoolType(academicRecord.School);
                }

                // Parse Academic Awards
                Library.Apas.AdmissionsRecord.AcademicAwardType awardTypeJoined = new Library.Apas.AdmissionsRecord.AcademicAwardType();
                foreach (Library.Apas.AdmissionsRecord.AcademicAwardType award in academicRecord.AcademicAward)
                {
                    if (award.AcademicAwardLevel != null) { awardTypeJoined.AcademicAwardLevel = award.AcademicAwardLevel; }
                    if (award.AcademicAwardTitle != null) { awardTypeJoined.AcademicAwardTitle = award.AcademicAwardTitle; }
                    if (Functions.CheckForNullDate(award.AcademicAwardDate) != null) { awardTypeJoined.AcademicAwardDate = award.AcademicAwardDate; }
                }

                if (awardTypeJoined.AcademicAwardLevel != null || awardTypeJoined.AcademicAwardTitle != null)
                {
                    AcademicAward _AcademicAward = ParseAdmRecAcademicAwardType(application.ApplicationID, awardTypeJoined);
                    if (_AcademicAward.AcademicAwardId < 1 || !_NewRecord.AcademicAwards.Any(x => x.AcademicAwardId == _AcademicAward.AcademicAwardId))
                    {
                        _NewRecord.AcademicAwards.Add(_AcademicAward);
                    }
                }

                // Parse Academic Sessions
                foreach (Library.Apas.AdmissionsRecord.AcademicSessionType session in academicRecord.AcademicSession)
                {
                    AcademicSession _AcademicSession = ParseAdmRecAcademicSessionType(application.ApplicationID, session);
                    if (_AcademicSession. AcademicSessionId < 1 || !_NewRecord.AcademicSessions.Any(x => x.AcademicSessionId == _AcademicSession.AcademicSessionId))
                    {
                        _NewRecord.AcademicSessions.Add(_AcademicSession);
                    }
                }

                // Parse Courses
                foreach (Library.Apas.AdmissionsRecord.CourseType1 course in academicRecord.Course)
                {
                    Course _Course = ParseAdmRecCourseType1(application.ApplicationID, course);
                    if (_Course.CourseId < 1 || !_NewRecord.Courses.Any(x => x.CourseId == _Course.CourseId))
                    {
                        _NewRecord.Courses.Add(_Course);
                    }
                }

                var records = (from ar in ctx.AcademicRecords
                               where ar.Student.StudentApplications.Any(x => x.ApplicationID == application.ApplicationID)
                               && ar.School.InstitutionId == _NewRecord.School.InstitutionId
                               select ar).AsEnumerable();

                // Check if it is the same Academic Session
                if (Functions.CheckForNullDate(academicRecord.FirstDateAttended) != null)
                {
                    records = records.Where(x => x.FirstDateAttended == academicRecord.FirstDateAttended);
                }

                if (records.Any())
                {
                    _NewRecord = records.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _NewRecord.CreatedBy = _UserName;
                    _NewRecord.CreatedDateTime = _XmlCreatedDate;
                }

                if (Functions.CheckForNullDate(academicRecord.FirstDateAttended) != null)
                {
                    _NewRecord.FirstDateAttended = academicRecord.FirstDateAttended;
                }
                if (Functions.CheckForNullDate(academicRecord.LastDateAttended) != null)
                {
                    _NewRecord.LastDateAttended = academicRecord.LastDateAttended;
                }
                if (academicRecord.StudentLevel != null)
                {
                    _NewRecord.StudentLevel = academicRecord.StudentLevel.StudentLevelCode;
                }

                // Include Application ID
                if (!string.IsNullOrWhiteSpace(application.ApplicationID))
                {
                    _NewRecord.ApplicationId = application.ApplicationID;
                }

                _NewRecord.ModifiedBy = _UserName;
                _NewRecord.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAdmRecSelfReportedAcademicRecordType", "Error", ex.ToString());
            }

            return _NewRecord;
        }

        /// <summary>
        /// Parse/Update Academic Sessions
        /// </summary>
        /// <param name="academicSession"></param>
        /// <returns></returns>
        public AcademicSession ParseAdmRecAcademicSessionType(string appId, Library.Apas.AdmissionsRecord.AcademicSessionType academicSession)
        {
            AcademicSession _AcademicSession = new AcademicSession();
            try
            {
                // Set/Update properties
                _AcademicSession.StartDate = academicSession.AcademicSessionDetail.SessionBeginDate;
                _AcademicSession.EndDate = academicSession.AcademicSessionDetail.SessionEndDate;
                _AcademicSession.Term = Functions.GetLcTerm(academicSession.AcademicSessionDetail.SessionDesignator);

                var academicSessions = ctx.AcademicSessions.Where(a => a.EndDate == _AcademicSession.EndDate &&
                                                                   a.StartDate == _AcademicSession.StartDate &&
                                                                   a.Term == _AcademicSession.Term &&
                                                                   a.AcademicRecord.Student.StudentApplications.Any(b => b.ApplicationID == appId));
                if (academicSessions.Any())
                {
                    _AcademicSession = academicSessions.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _AcademicSession.CreatedBy = _UserName;
                    _AcademicSession.CreatedDateTime = _XmlCreatedDate;
                }

                _AcademicSession.ModifiedBy = _UserName;
                _AcademicSession.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAdmRecAcademicSessionType", "Error", ex.ToString());
            }
            return _AcademicSession;
        }

        public Person ParseAdmRecApplicantPersonType(Library.Apas.AdmissionsRecord.ApplicantPersonType person, bool? collegeData = null)
        {
            //DateTime? temp;
            Person _Person = new Person();
            DateTime? _BirthDate = null;
            try
            {
                // Check birthdate
                if (person.Birth != null) { _BirthDate = Functions.CheckForNullDate(person.Birth.BirthDate); }

                // Create ASN list to find a person
                List<string> asnList = person.AgencyIdentifier != null && person.AgencyIdentifier.Count() > 0 ? person.AgencyIdentifier.Select(x => x.AgencyAssignedID).ToList() : null;

                // Find person
                var persons = FindPerson(person.Name.FirstName, person.Name.LastName, person.Name.MiddleName, asnList, _BirthDate);

                if (persons.Any())
                {
                    _Person = persons.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Person.CreatedBy = _UserName;
                    _Person.CreatedDateTime = _XmlCreatedDate;
                }

                if (_BirthDate != null) { _Person.BirthDate = _BirthDate; }

                // Parse Personal Names
                //if (!(_Person.PersonId >= 1 && _Person.Names != null && _Person.Names.Any()))
                if (person.Name != null)
                {
                    PersonName _Name = ParseCoreMainNameType(person.Name, Structs.Name.PersonalNameType, _Person.PersonId, collegeData);
                    if (_Name != null && _Name.NameId < 1 && (!_Person.Names.Any(x => x.Person_PersonId == (_Name.Person_PersonId != null ? _Name.Person_PersonId : x.Person_PersonId) && 
                                                             (x.FirstName ?? string.Empty).Trim().ToUpper() == (_Name.FirstName != null ? _Name.FirstName.Trim().ToUpper() : (x.FirstName ?? string.Empty).Trim().ToUpper()) &&
                                                             (x.LastName ?? string.Empty).Trim().ToUpper() == (_Name.LastName != null ? _Name.LastName.Trim().ToUpper() : (x.LastName ?? string.Empty).Trim().ToUpper()) &&
                                                             (x.MiddleNames ?? string.Empty).Trim().ToUpper() == (_Name.MiddleNames != null ? _Name.MiddleNames.Trim().ToUpper() : (x.MiddleNames ?? string.Empty).Trim().ToUpper()))))
                    {
                        _Person.Names.Add(_Name);
                    }
                }

                // Parse Former Names
                if (person.AlternateName != null)
                {
                    foreach (Library.Apas.CoreMain.NameType altname in person.AlternateName)
                    {
                        PersonName _AltName = ParseCoreMainNameType(altname, Structs.Name.FormerType, _Person.PersonId, collegeData);
                        if (_AltName != null && (!_Person.Names.Any(x => x.Person_PersonId == (_AltName.Person_PersonId != null ? _AltName.Person_PersonId : x.Person_PersonId) &&
                                                                    (x.FirstName ?? string.Empty).Trim().ToUpper() == (_AltName.FirstName != null ? _AltName.FirstName.Trim().ToUpper() : (x.FirstName ?? string.Empty).Trim().ToUpper()) &&
                                                                    (x.LastName ?? string.Empty).Trim().ToUpper() == (_AltName.LastName != null ? _AltName.LastName.Trim().ToUpper() : (x.LastName ?? string.Empty).Trim().ToUpper()) &&
                                                                    (x.MiddleNames ?? string.Empty).Trim().ToUpper() == (_AltName.MiddleNames != null ? _AltName.MiddleNames.Trim().ToUpper() : (x.MiddleNames ?? string.Empty).Trim().ToUpper()))))
                        {
                            _Person.Names.Add(_AltName);
                        }
                    }
                }

                if (person.Contacts != null)
                { 
                    // Permanent Address
                    if (person.Contacts.PermanentAddress != null)
                    {
                        Address _Address = ParseAdmRecAddressType1(person.Contacts.PermanentAddress, Structs.Address.PermanentAddressType, _Person.PersonId);
                        if (!_Person.Addresses.Any(x => x.PersonId == _Address.PersonId)) { _Person.Addresses.Add(_Address); }
                    }

                    // Current Address
                    if (person.Contacts.CurrentAddress != null)
                    {
                        Address _Address1 = ParseAdmRecAddressType1(person.Contacts.CurrentAddress, Structs.Address.CurrentAddressType, _Person.PersonId);
                        if (_Address1.AddressId < 1) { _Person.Addresses.Add(_Address1); }
                    }

                    // Primary Email
                    if (person.Contacts.PrimaryEmail != null)
                    {
                        Email _Email = ParseCoreMainEmailType(person.Contacts.PrimaryEmail, Structs.Email.PrimaryEmailType, _Person.PersonId);
                        if (_Email.EmailId < 1 || !_Person.Emails.Any(x => x.EmailId == _Email.EmailId)) {
                            _Person.Emails.Add(_Email);
                        }
                    }

                    // Permanent Phone
                    if (person.Contacts.PermanentPhone != null)
                    {
                        Phone _PermanentPhone = ParseCoreMainPhoneType(person.Contacts.PermanentPhone, Enums.PhoneTypes.PermanentPhoneType, _Person.PersonId);
                        if (!_Person.Phones.Any(x => x.PhoneNumber == _PermanentPhone.PhoneNumber && x.PhoneType == _PermanentPhone.PhoneType))
                        {
                            _Person.Phones.Add(_PermanentPhone);
                        }
                    }

                    // Day Phone
                    if (person.Contacts.DayPhone != null)
                    {
                        Phone _DayPhone = ParseCoreMainPhoneType(person.Contacts.DayPhone, Enums.PhoneTypes.DayPhoneType, _Person.PersonId);
                        if (!_Person.Phones.Any(x => x.PhoneNumber == _DayPhone.PhoneNumber && x.PhoneType == _DayPhone.PhoneType))
                        {
                            _Person.Phones.Add(_DayPhone);
                        }
                    }

                    // Mobile Phone
                    if (person.Contacts.MobilePhone != null)
                    {
                        Phone _MobilePhone = ParseCoreMainPhoneType(person.Contacts.MobilePhone, Enums.PhoneTypes.MobilePhoneType, _Person.PersonId);
                        if (!_Person.Phones.Any(x => x.PhoneNumber == _MobilePhone.PhoneNumber && x.PhoneType == _MobilePhone.PhoneType))
                        {
                            _Person.Phones.Add(_MobilePhone);
                        }
                    }

                    // Preferred Phone
                    if (person.Contacts.PreferredPhone != null)
                    {
                        Phone _PreferredPhone = ParseCoreMainPhoneType(person.Contacts.PreferredPhone, Enums.PhoneTypes.PreferredPhoneType, _Person.PersonId);
                        if (!_Person.Phones.Any(x => x.PhoneNumber == _PreferredPhone.PhoneNumber && x.PhoneType == _PreferredPhone.PhoneType))
                        {
                            _Person.Phones.Add(_PreferredPhone);
                        }
                    }
                }

                // LANGUAGE
                if (person.Language != null)
                {
                    _Person.LanguageCode = person.Language.LanguageCode;
                    _Person.LanguageUsage = person.Language.LanguageUsage;
                }

                // GENDER
                if (person.Gender != null)
                {
                    Gender _Gender = new Gender();

                    var results = ctx.Genders.Where(x => x.Person.PersonId == _Person.PersonId &&
                                                       x.GenderCodeType == person.Gender.GenderCode);

                    if (results.Any())
                    {
                        _Gender = results.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();
                    }
                    else
                    {
                        _Gender.CreatedBy = _UserName;
                        _Gender.CreatedDateTime = _XmlCreatedDate;
                        _Gender.GenderCodeType = person.Gender.GenderCode;

                        _Person.Genders.Add(_Gender);
                    }
                    
                    // Set College Data to True, it is False by Default
                    if (collegeData != null && collegeData == true)
                    {
                        _Gender.CollegeData = true;
                    }

                    _Gender.ModifiedBy = _UserName;
                    _Gender.ModifiedDateTime = _XmlCreatedDate;
                }
                //else
                //{
                //    Gender _Gender = new Gender()
                //    {
                //        CreatedBy = _UserName,
                //        CreatedDateTime = _XmlCreatedDate,
                //        ModifiedBy = _UserName,
                //        ModifiedDateTime = _XmlCreatedDate,
                //        GenderCodeType = Library.Apas.CoreMain.GenderCodeType.Unreported,
                //    };
                //    _Person.Genders.Add(_Gender);
                //}

                // Canadian Ethnicity Race
                if (person.CanadianEthnicityRace != null)
                {
                    _Person.CanadianEthnicityRace = person.CanadianEthnicityRace.CanadianEthnicityCode;
                }

                // IMMIGRATION
                if (person.Immigration != null)
                {
                    if (Functions.CheckForNullDate(person.Immigration.FirstEntryIntoHostCountryDate) != null)
                    {
                        Immigration _Immigration = new Immigration();

                        var results = ctx.Immigrations.Where(x => x.Person.PersonId == _Person.PersonId &&
                                                             x.FirstEntryIntoCountryDateTime == person.Immigration.FirstEntryIntoHostCountryDate);
                        if (results.Any())
                        {
                            _Immigration = results.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                        }
                        else
                        {
                            _Immigration.CreatedBy = _UserName;
                            _Immigration.CreatedDateTime = _XmlCreatedDate;
                            _Person.Immigrations.Add(_Immigration);
                        }

                        if (Functions.CheckForNullDate(person.Immigration.FirstEntryIntoHostCountryDate) != null)
                        {
                            _Immigration.FirstEntryIntoCountryDateTime = person.Immigration.FirstEntryIntoHostCountryDate;
                        }

                        if (person.Immigration.VisaDetail != null && !string.IsNullOrWhiteSpace(person.Immigration.VisaDetail.NonImmigrantVisaNumber))
                        {
                            _Immigration.NonImmigrantVisaNumber = person.Immigration.VisaDetail.NonImmigrantVisaNumber;
                        }
                        if (person.Immigration.VisaDetail != null && Functions.CheckForNullDate(person.Immigration.VisaDetail.VisaExpirationDate) != null)
                        {
                            _Immigration.VisaExpirationDateTime = person.Immigration.VisaDetail.VisaExpirationDate;
                        }
                        _Immigration.ModifiedBy = _UserName;
                        _Immigration.ModifiedDateTime = _XmlCreatedDate;
                    }
                }

                // RESIDENCY
                if (person.Residency != null)
                {
                    if (person.Residency.CountryCodeSpecified)
                    {
                        Residency _Residency = new Residency();

                        var results = ctx.Residencies.Where(x => x.Person.PersonId == _Person.PersonId &&
                                                                 x.CountryCodeType == person.Residency.CountryCode).OrderByDescending(y => y.CreatedDateTime);
                        if (results.Any())
                        {
                            _Residency = results.OrderByDescending(x => x.ModifiedBy).FirstOrDefault();
                        }
                        else
                        {
                            _Residency.CreatedBy = _UserName;
                            _Residency.CreatedDateTime = _XmlCreatedDate;
                            _Residency.ResidencyCountry = person.Residency.Country;
                            _Residency.CountryCodeType = person.Residency.CountryCode;
                            _Person.Residencies.Add(_Residency);
                        }

                        _Residency.ModifiedBy = _UserName;
                        _Residency.ModifiedDateTime = _XmlCreatedDate;
                    }
                }

                // CITIZENSHIP
                if (person.Citizenship != null)
                {
                    foreach (Library.Apas.AdmissionsRecord.CitizenshipType1 citizenship in person.Citizenship)
                    {
                        Citizenship _Citizenship = new Citizenship();

                        var results = _Person.Citizenships.Where(x => x.CountryCodeType == citizenship.CitizenshipCountryCode &&
                                                                        x.CitizenshipStatusCodeType == citizenship.CitizenshipStatusCode);
                        if (results.Any())
                        {
                            _Citizenship = results.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                        }
                        else
                        {
                            _Citizenship.CreatedBy = _UserName;
                            _Citizenship.CreatedDateTime = _XmlCreatedDate;
                            _Person.Citizenships.Add(_Citizenship);
                        }

                        if (citizenship.CitizenshipCountryCodeSpecified)
                        {
                            _Citizenship.CountryCodeType = citizenship.CitizenshipCountryCode;
                        }
                        else
                        {
                            _Citizenship.CountryCodeType = Library.Apas.CoreMain.CountryCodeType.CA;
                        }
                        if (citizenship.CitizenshipStatusCodeSpecified)
                        {
                            _Citizenship.CitizenshipStatusCodeType = citizenship.CitizenshipStatusCode;
                        }
                        else
                        {
                            _Citizenship.CitizenshipStatusCodeType = Library.Apas.CoreMain.CitizenshipStatusCodeType.Citizen;
                        }

                        _Citizenship.ModifiedBy = _UserName;
                        _Citizenship.ModifiedDateTime = _XmlCreatedDate;
                    }
                }

                _Person.ModifiedBy = _UserName;
                _Person.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAdmRecApplicantPersonType", "Error", ex.ToString());
            }
            return _Person;
        }

        /// <summary>
        /// Parse/Create Address Type 1
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Address ParseAdmRecAddressType1(Library.Apas.AdmissionsRecord.AddressType1 address, string addressType, int personId)
        {
            Address _TempAddress = new Address();
            Address _RetAddress = new Address();
            string[] prov = { "AB", "BC", "MB", "NB", "NL", "NT", "NS", "NU", "ON", "PE", "QC", "SK", "YT" };

            try
            {
                // Instantiate and set local db copy of incoming address
                _TempAddress.City = address.City;
                _TempAddress.AddressType = addressType;

                if (address.Items.Any())
                {
                    for (int i = 0; i < address.Items.Count(); i++)
                    {
                        switch (address.ItemsElementName[i])
                        {
                            case Library.Apas.CoreMain.ItemsChoiceType.CountryCode:
                                _TempAddress.Country = address.Items[i].ToString();
                                break;
                            case Library.Apas.CoreMain.ItemsChoiceType.PostalCode:
                                _TempAddress.PostalCode = address.Items[i].ToString();
                                break;
                            case Library.Apas.CoreMain.ItemsChoiceType.StateProvince:
                            case Library.Apas.CoreMain.ItemsChoiceType.StateProvinceCode:
                                _TempAddress.Province = address.Items[i].ToString();
                                break;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(_TempAddress.Country) && prov.Contains(_TempAddress.Province))
                    {
                        _TempAddress.Country = "XCA";
                    }

                    // Normalize space in Postal Code
                    _TempAddress.PostalCode = _TempAddress.PostalCode.NormalizePostalCode(_TempAddress.Country);
                }

                if (address.AddressLine.ToList().Count >= 1) { _TempAddress.AddressLine1 = address.AddressLine.ElementAt(0).CleanAddressLine(); }
                if (address.AddressLine.ToList().Count >= 2) { _TempAddress.AddressLine2 = address.AddressLine.ElementAt(1).CleanAddressLine(); }
                if (address.AddressLine.ToList().Count >= 3) { _TempAddress.AddressLine2 = string.Concat(_TempAddress.AddressLine2, " " + address.AddressLine.ElementAt(2)).CleanAddressLine(); }

                var addresses = ctx.Addresses.Where(a => a.PostalCode == _TempAddress.PostalCode &&
                                                         a.Province == _TempAddress.Province &&
                                                         a.City == _TempAddress.City &&
                                                         a.Country == _TempAddress.Country &&
                                                         a.AddressLine1 == _TempAddress.AddressLine1 &&
                                                         a.AddressLine2 == _TempAddress.AddressLine2 &&
                                                         a.AddressType == _TempAddress.AddressType &&
                                                         a.PersonId == personId);
                if (addresses.Any())
                {
                    _RetAddress = addresses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _RetAddress.CreatedBy = _UserName;
                    _RetAddress.CreatedDateTime = _XmlCreatedDate;
                }

                _RetAddress.AddressLine1 = _TempAddress.AddressLine1;
                _RetAddress.AddressLine2 = _TempAddress.AddressLine2;
                _RetAddress.City = _TempAddress.City;
                _RetAddress.Country = _TempAddress.Country;
                _RetAddress.Province = _TempAddress.Province;
                _RetAddress.PostalCode = _TempAddress.PostalCode;
                _RetAddress.AddressType = _TempAddress.AddressType;
                _RetAddress.ModifiedBy = _UserName;
                _RetAddress.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAdmRecAddressType1", "Error", ex.ToString());
            }
            return _RetAddress;
        }

        /// <summary>
        /// Parse/Create Phone Type 1
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public Phone ParseAdmRecPhoneType1(Library.Apas.CoreMain.PhoneType phone, Enums.PhoneTypes phoneType)
        {
            Phone _Phone = new Phone();
            try
            {
                _Phone.CountryCode = phone.CountryPrefixCode ?? "1";
                _Phone.AreaCode = phone.AreaCityCode;
                _Phone.PhoneNumber = phone.PhoneNumber;
                _Phone.PhoneType = phoneType;

                var phones = ctx.Phones.Where(x => x.AreaCode == _Phone.AreaCode &&
                                                    x.CountryCode == _Phone.CountryCode &&
                                                    x.PhoneNumber == _Phone.PhoneNumber &&
                                                    x.PhoneType == _Phone.PhoneType);
                if (phones.Any())
                {
                    _Phone = phones.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Phone.CreatedBy = _UserName;
                    _Phone.CreatedDateTime = _XmlCreatedDate;
                }

                _Phone.ModifiedBy = _UserName;
                _Phone.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAdmRecPhoneType1", "Error", ex.ToString());
            }
            return _Phone;
        }

        /// <summary>
        /// Parse/Create Email Type 1
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Email ParseAdmRecEmailType1(Library.Apas.AdmissionsRecord.EmailType1 email, string emailType)
        {
            Email _Email = new Email();
            try
            {
                _Email.EmailAddress = email.EmailAddress;
                _Email.EmailType = emailType;

                var emails = ctx.Emails.Where(e => e.EmailAddress == _Email.EmailAddress &&
                                                    e.EmailType == _Email.EmailType);
                if (emails.Any())
                {
                    _Email = emails.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Email.CreatedBy = _UserName;
                    _Email.CreatedDateTime = _XmlCreatedDate;
                }

                _Email.ModifiedBy = _UserName;
                _Email.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAdmRecEmailType1", "Error", ex.ToString());
            }
            return _Email;
        }

        /// <summary>
        /// Parse/Create Course Type 1
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public Course ParseAdmRecCourseType1(string appId, Library.Apas.AdmissionsRecord.CourseType1 course)
        {
            Course _Course = new Course();

            try
            {
                if (!string.IsNullOrWhiteSpace(course.CourseTitle)) _Course.Title = course.CourseTitle;
                if (!string.IsNullOrWhiteSpace(course.CourseNumber)) _Course.CourseNumber = course.CourseNumber;
                if (!string.IsNullOrWhiteSpace(course.OriginalCourseID)) _Course.OriginalCourseID = course.OriginalCourseID;
                if (!string.IsNullOrWhiteSpace(course.AgencyCourseID)) _Course.AgencyCourseID = course.AgencyCourseID;
                if (!string.IsNullOrWhiteSpace(course.CourseSectionNumber)) _Course.SectionNumber = course.CourseSectionNumber;
                if (!string.IsNullOrWhiteSpace(course.CourseAcademicGrade)) _Course.AcademicGrade = course.CourseAcademicGrade;
                if (Functions.CheckForNullDate(course.CourseBeginDate) != null) _Course.BeginDate = course.CourseBeginDate;
                if (Functions.CheckForNullDate(course.CourseEndDate) != null) _Course.EndDate = course.CourseEndDate;

                var courses = ctx.Courses.Where(c => c.Title == _Course.Title &&
                                                     c.CourseNumber == _Course.CourseNumber &&
                                                     c.OriginalCourseID == _Course.OriginalCourseID &&
                                                     c.AgencyCourseID == _Course.AgencyCourseID &&
                                                     c.SectionNumber == _Course.SectionNumber &&
                                                     c.AcademicGrade == _Course.AcademicGrade &&
                                                     c.BeginDate == _Course.BeginDate &&
                                                     c.EndDate == _Course.EndDate &&
                                                     c.AcademicRecord.Student.StudentApplications.Any(d => d.ApplicationID == appId));

                if (courses.Any())
                {
                    _Course = courses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Course.CreatedBy = _UserName;
                    _Course.CreatedDateTime = _XmlCreatedDate;
                }

                _Course.ModifiedBy = _UserName;
                _Course.ModifiedDateTime = _XmlCreatedDate;

                _Course.CreditBasis = course.CourseCreditBasis;
                if (!string.IsNullOrWhiteSpace(course.CourseAcademicGradeScaleCode)) { _Course.AcademicGradeScaleCode = course.CourseAcademicGradeScaleCode.ToString(); }
                _Course.AcademicGrade = course.CourseAcademicGrade;
                _Course.NarrativeExplanationGrade = course.CourseNarrativeExplanationGrade;
                _Course.SubjectAbbreviation = course.CourseSubjectAbbreviation;
                _Course.OverrideSchoolCourseNumber = course.OverrideSchoolCourseNumber;
                _Course.InstructionSiteName = course.CourseInstructionSiteName;
                _Course.NoteMessage = Functions.JoinStrings(course.NoteMessage);

                if (course.CourseQualityPointsEarnedSpecified) { _Course.QualityPointsEarned = course.CourseQualityPointsEarned.ToString(); }
                if (course.CourseGPAApplicabilityCodeSpecified) { _Course.GPAApplicabilityCode = course.CourseGPAApplicabilityCode; }
                if (course.CourseApplicabilitySpecified) { _Course.Applicability = course.CourseApplicability; }
                if (course.CourseInstructionSiteSpecified) { _Course.InstructionSite = course.CourseInstructionSite; }
                if (course.CourseRepeatCodeSpecified) { _Course.RepeatCode = course.CourseRepeatCode; }
                if (course.CourseAcademicGradeStatusCodeSpecified) { _Course.AcademicGradeStatusCode = course.CourseAcademicGradeStatusCode; }
                if (course.CourseAddDateSpecified) { _Course.AddDate = course.CourseAddDate; }
                if (course.CourseDropDateSpecified) { _Course.DropDate = course.CourseDropDate; }
                if (course.CourseBeginDateSpecified) { _Course.BeginDate = course.CourseBeginDate; }
                if (course.CourseEndDateSpecified) { _Course.EndDate = course.CourseEndDate; }
                if (course.CourseLevelSpecified) { _Course.Level = course.CourseLevel; }
                if (course.CourseCreditEarnedSpecified) { _Course.CreditEarned = course.CourseCreditEarned.ToString(); }
                if (course.CourseCreditLevelSpecified) { _Course.CreditLevel = course.CourseCreditLevel; }
                if (course.CourseCreditUnitsSpecified) { _Course.CreditUnits = course.CourseCreditUnits; }
                if (course.CourseCreditValueSpecified) { _Course.CreditValue = course.CourseCreditValue.ToString(); }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAdmRecCourseType1", "Error", ex.ToString());
            }

            return _Course;
        }

        /// <summary>
        /// Parse/Create Institution Type
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        public Institution ParseAdmRecSchoolType(Library.Apas.AdmissionsRecord.SchoolType school)
        {
            Institution _Institution = new Institution();

            try
            {
                if (school.LocalOrganizationID != null)
                {
                    _Institution.LocalOrganizationID = school.LocalOrganizationID.LocalOrganizationIDCode;
                }
                else
                {
                    // Did not complete High School
                    if (school.OrganizationName.Trim().ToUpper() == Structs.Institution.DidNotCompleteOrganizationName.Trim().ToUpper())
                    {
                        _Institution.LocalOrganizationID = Structs.Institution.DidNotCompleteLocalOrganizationIDCode;
                    }
                }

                var institutions = ctx.Institutions.Where(i => i.LocalOrganizationID == _Institution.LocalOrganizationID);

                if (institutions.Any())
                {
                    _Institution = institutions.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Institution.CreatedBy = _UserName;
                    _Institution.CreatedDateTime = _XmlCreatedDate;
                }

                _Institution.ModifiedBy = _UserName;
                _Institution.ModifiedDateTime = _XmlCreatedDate;

                if (!string.IsNullOrWhiteSpace(school.ESIS)) { _Institution.ESIS = school.ESIS; }
                if (!string.IsNullOrWhiteSpace(school.APAS)) { _Institution.APAS = school.APAS; }

                InstitutionName _InstitutionName = ParseInstitutionName(school.OrganizationName, _Institution.LocalOrganizationID);
                if (!_Institution.InstitutionNames.Any(x => x.InstitutionNameId == _InstitutionName.InstitutionNameId))
                {
                    _Institution.InstitutionNames.Add(_InstitutionName);
                }

                foreach (Library.Apas.CoreMain.ContactsType contact in school.Contacts)
                {
                    foreach (Library.Apas.CoreMain.AddressType address in contact.Address)
                    {
                        Address _Address = ParseCoreMainAddressType(address, Structs.Address.InstitutionType, _Institution.LocalOrganizationID);
                        if (!_Institution.Addresses.Any(x => x.AddressId == _Address.AddressId))
                        {
                            _Institution.Addresses.Add(_Address);
                        }
                    }

                    foreach (Library.Apas.CoreMain.EmailType email in contact.Email)
                    {
                        Email _Email = ParseCoreMainEmailType(email, Structs.Email.InstitutionEmailType, null, _Institution.InstitutionId);
                        if (_Email.EmailId < 1)
                        {
                            _Institution.Emails.Add(_Email);
                        }
                    }

                    foreach (Library.Apas.CoreMain.PhoneType phone in contact.Phone)
                    {
                        Phone _Phone = ParseCoreMainPhoneType(phone, Enums.PhoneTypes.InstitutionPhoneType, null, _Institution.InstitutionId);
                        if (_Phone.PhoneId < 1)
                        {
                            _Institution.Phones.Add(_Phone);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAdmRecSchoolType", "Error", ex.ToString());
            }
            return _Institution;
        }

        #endregion AdmissionsRecord

        #region Parse AcademicRecord
        public AcademicRecord ParseAcadRec(Library.Apas.AcademicRecord.AcademicRecordType acadRec, Response response)
        {
            DateTime? beginDate = null, endDate = null;
            AcademicRecord _AcademicRecord = new AcademicRecord();

            try
            {
                if (response.RequestedStudent != null) { _AcademicRecord.Student = response.RequestedStudent; }
                if (acadRec.StudentLevel != null) { _AcademicRecord.StudentLevel = acadRec.StudentLevel.StudentLevelCode; };

                // TODO: Add Institution/School
                //_AcademicRecord.School = acadRec.School;

                var academicRecords = ctx.AcademicRecords.Where(s => s.Student.StudentId == _AcademicRecord.Student.StudentId).AsQueryable();

                // Filter by Response Id
                if (response != null && response.ResponseId > 0)
                {
                    academicRecords = academicRecords.Where(x => x.Responses.ResponseId == response.ResponseId);
                }

                // Parse Acedemic Awards
                foreach (Library.Apas.AcademicRecord.AcademicAwardType award in acadRec.AcademicAward)
                {
                    AcademicAward _AcademicAward = ParseAcadRecAcademicAwardType(award, response);
                    if (_AcademicAward.AcademicAwardId < 1 || !academicRecords.Any(y => y.AcademicAwards.Any(x => x.AcademicAwardId == _AcademicAward.AcademicAwardId)))
                    {
                        _AcademicRecord.AcademicAwards.Add(_AcademicAward);
                    }
                    
                    if (_AcademicAward != null && _AcademicAward.AcademicAwardId > 0)
                    {
                        academicRecords = academicRecords.Where(x => x.AcademicAwards.Any(y => y.AcademicAwardId == _AcademicAward.AcademicAwardId));
                    }
                }

                // Parse Acedemic Sessions
                foreach (Library.Apas.AcademicRecord.AcademicSessionType session in acadRec.AcademicSession)
                {
                    AcademicSession _AcademicSession = ParseAcadRecSessionType(session, response);
                    if (_AcademicSession.AcademicSessionId < 1 || !academicRecords.Any(y => y.AcademicSessions.Any(x => x.AcademicSessionId == _AcademicSession.AcademicSessionId)))
                    {
                        _AcademicRecord.AcademicSessions.Add(_AcademicSession);
                    }
                    
                    if (_AcademicSession != null && _AcademicSession.AcademicSessionId > 0)
                    {
                        academicRecords = academicRecords.Where(x => x.AcademicSessions.Any(y => y.AcademicSessionId == _AcademicSession.AcademicSessionId));
                    }

                    // Parse Courses
                    foreach (Library.Apas.AcademicRecord.CourseType course in session.Course)
                    {
                        if (session.AcademicSessionDetail != null)
                        {
                            if (session.AcademicSessionDetail.SessionBeginDateSpecified && Functions.CheckForNullDate(session.AcademicSessionDetail.SessionBeginDate) != null)
                            {
                                beginDate = session.AcademicSessionDetail.SessionBeginDate;
                            }
                            else
                            {
                                beginDate = null;
                            }

                            if (session.AcademicSessionDetail.SessionEndDateSpecified && Functions.CheckForNullDate(session.AcademicSessionDetail.SessionEndDate) != null)
                            {
                                endDate = session.AcademicSessionDetail.SessionEndDate;
                            }
                            else
                            {
                                endDate = null;
                            }
                        }

                        Course _Course = ParseAcadRecCourse(course, response, beginDate, endDate);
                        if (_Course.CourseId < 1 || !academicRecords.Any(y => y.Courses.Any(x => x.CourseId == _Course.CourseId)))
                        {
                            _AcademicRecord.Courses.Add(_Course);

                            // TODO: Kick off Job to export this course - Automatic option
                            //QueueJob(_Course.CourseId.ToString(), Enums.JobTypeType.SaveColleagueExtCourse);
                        }

                        if (_Course != null && _Course.CourseId > 0)
                        {
                            academicRecords = academicRecords.Where(x => x.Courses.Any(y => y.CourseId == _Course.CourseId));
                        }
                    }

                    // Parse Academic Session School
                    if (session.School != null)
                    {
                        Institution _School = ParseAcadRecSchoolType(session.School, response);
                        if (_School.InstitutionId < 1 || !academicRecords.Any(y => y.School_InstitutionId == _School.InstitutionId))
                        {
                            _AcademicRecord.School = _School;
                        }

                        if (_School != null && _School.InstitutionId > 0)
                        {
                            academicRecords = academicRecords.Where(y => y.School_InstitutionId == _School.InstitutionId);
                        }
                    }
                }

                // Parse Courses
                foreach (Library.Apas.AcademicRecord.CourseType course in acadRec.Course)
                {
                    Course _Course = ParseAcadRecCourse(course, response);
                    if (_Course.CourseId < 1 || !academicRecords.Any(y => y.Courses.Any(x => x.CourseId == _Course.CourseId)))
                    {
                        _AcademicRecord.Courses.Add(_Course);
                    }
                    
                    if (_Course != null && _Course.CourseId > 0)
                    {
                        academicRecords = academicRecords.Where(x => x.Courses.Any(y => y.CourseId == _Course.CourseId));
                    }
                }

                // Parse Academic Record School
                if (acadRec.School != null)
                {
                    Institution _School = ParseAcadRecSchoolType(acadRec.School, response);
                    if (_School.InstitutionId < 1 || !academicRecords.Any(y => y.School_InstitutionId == _School.InstitutionId))
                    {
                        _AcademicRecord.School = _School;
                    }

                    if (_School != null && _School.InstitutionId > 0)
                    {
                        academicRecords = academicRecords.Where(y => y.School_InstitutionId == _School.InstitutionId);
                    }
                }

                if (academicRecords.Any())
                {
                    // if we have a match, make sure we update it if necessary
                    var match = academicRecords.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();

                    foreach (AcademicAward award in _AcademicRecord.AcademicAwards)
                    {
                        if (!match.AcademicAwards.Any(x => x.Equals(award)))
                        {
                            match.AcademicAwards.Add(award);
                        }
                    }
                    foreach (AcademicSession session in _AcademicRecord.AcademicSessions)
                    {
                        if (!match.AcademicSessions.Any(x => x.Equals(session)))
                        {
                            match.AcademicSessions.Add(session);
                        }
                    }
                    foreach (Course course in _AcademicRecord.Courses)
                    {
                        if (!match.Courses.Any(x => x.Equals(course)))
                        {
                            match.Courses.Add(course);
                        }
                    }
                    _AcademicRecord = match;
                }
                else
                {
                    _AcademicRecord.CreatedBy = _UserName;
                    _AcademicRecord.CreatedDateTime = _XmlCreatedDate;
                }

                _AcademicRecord.ModifiedBy = _UserName;
                _AcademicRecord.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAcadRec", "Error", ex.ToString());
            }

            return _AcademicRecord;
        }

        /// <summary>
        /// Parse/Create AcademicRecord.School
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        private Institution ParseAcadRecSchoolType(Library.Apas.AcademicRecord.SchoolType school, Response response)
        {
            Library.Apas.AcademicRecord.SchoolType _SchoolType = new Library.Apas.AcademicRecord.SchoolType();
            Institution _Institution = new Institution();

            try
            {
                if (school.LocalOrganizationID != null)
                {
                    _Institution.LocalOrganizationID = school.LocalOrganizationID.LocalOrganizationIDCode;
                } else
                {
                    // If there is no LocalOrgId for Academic Institution, then uses Source institution (HighSchool use Alberta Education)
                    if (response != null && response.TransmissionData != null && response.TransmissionData.SourceInstitution != null &&
                        response.TransmissionData.SourceInstitution.InstitutionNames != null && response.TransmissionData.SourceInstitution.InstitutionNames.Any()){
                        string sourceInstitutionName = response.TransmissionData.SourceInstitution.InstitutionNames.OrderByDescending(o => o.ModifiedDateTime).Select(s => s.Name).FirstOrDefault();
                        // If Institution names match, we can use source institution LocalOrgId
                        if (school.OrganizationName.Trim().ToUpper() == sourceInstitutionName.Trim().ToUpper() &&
                            !string.IsNullOrWhiteSpace(response.TransmissionData.SourceInstitution.LocalOrganizationID))
                        {
                            _Institution.LocalOrganizationID = response.TransmissionData.SourceInstitution.LocalOrganizationID;
                        }
                    }
                }

                var institutions = ctx.Institutions.Where(s => !string.IsNullOrEmpty(_Institution.LocalOrganizationID) ? s.LocalOrganizationID == _Institution.LocalOrganizationID :
                                                            s.InstitutionNames.Any(n => n.Name.Trim().ToUpper() == school.OrganizationName.Trim().ToUpper()));

                if (institutions.Any())
                {
                    _Institution = institutions.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Institution.CreatedBy = _UserName;
                    _Institution.CreatedDateTime = _XmlCreatedDate;
                }

                if (!string.IsNullOrWhiteSpace(school.ESIS)) { _Institution.ESIS = school.ESIS; }
                if (!string.IsNullOrWhiteSpace(school.APAS)) { _Institution.APAS = school.APAS; }

                _Institution.ModifiedBy = _UserName;
                _Institution.ModifiedDateTime = _XmlCreatedDate;

                // Parse Institution Name
                InstitutionName _InstitutionName = ParseInstitutionName(school.OrganizationName, _Institution.LocalOrganizationID);
                if ((_InstitutionName.InstitutionNameId < 1 || !_Institution.InstitutionNames.Any(x => x.InstitutionNameId == _InstitutionName.InstitutionNameId)) && !_Institution.InstitutionNames.Any(y => y.Name == _InstitutionName.Name))
                {
                    _Institution.InstitutionNames.Add(_InstitutionName);
                }

                // Create/Update Additional Properties
                foreach (Library.Apas.AcademicRecord.ContactsType contact in school.Contacts)
                {
                    foreach (Library.Apas.CoreMain.AddressType address in contact.Address)
                    {
                        Address _Address = ParseCoreMainAddressType(address, Structs.Address.InstitutionType);
                        if (_Address.AddressId < 1 || !_Institution.Addresses.Any(x => x.AddressId == _Address.AddressId))
                        {
                            _Institution.Addresses.Add(_Address);
                        }
                    }

                    foreach (Library.Apas.AcademicRecord.EmailType1 email in contact.Email)
                    {
                        Email _Email = ParseAcadRecEmailType1(email, Structs.Email.InstitutionEmailType);
                        if ((_Email.EmailId < 1 || !_Institution.Emails.Any(x => x.EmailId == _Email.EmailId)) && !_Institution.Emails.Any(y => y.EmailAddress == _Email.EmailAddress && y.EmailType == _Email.EmailType))
                        {
                            _Institution.Emails.Add(_Email);
                        }
                    }

                    foreach (Library.Apas.CoreMain.PhoneType phone in contact.Phone)
                    {
                        Phone _Phone = ParseAcadRecPhoneType1(phone, Enums.PhoneTypes.InstitutionPhoneType);
                        if (_Phone.PhoneId < 1 || !_Institution.Phones.Any(x => x.PhoneId == _Phone.PhoneId))
                        {
                            _Institution.Phones.Add(_Phone);
                        }
                    }

                    foreach (Library.Apas.CoreMain.PhoneType faxphone in contact.FaxPhone)
                    {
                        Phone _FaxPhone = ParseAcadRecPhoneType1(faxphone, Enums.PhoneTypes.FaxPhoneType);
                        if (_FaxPhone.PhoneId < 1 || !_Institution.Phones.Any(x => x.PhoneId == _FaxPhone.PhoneId))
                        {
                            _Institution.Phones.Add(_FaxPhone);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAcadRecSchoolType", "Error", ex.ToString());
            }

            return _Institution;
        }

        /// <summary>
        /// Parse/Create Phone Type 1
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public Phone ParseAcadRecPhoneType1(Library.Apas.CoreMain.PhoneType phone, Enums.PhoneTypes phoneType)
        {
            Phone _Phone = new Phone();
            try
            {
                _Phone.CountryCode = phone.CountryPrefixCode ?? "1";
                _Phone.AreaCode = phone.AreaCityCode;
                _Phone.PhoneNumber = phone.PhoneNumber;
                _Phone.PhoneType = phoneType;

                var phones = ctx.Phones.Where(p => p.AreaCode == _Phone.AreaCode &&
                                                   p.CountryCode == _Phone.CountryCode &&
                                                   p.PhoneNumber == _Phone.PhoneNumber &&
                                                   p.PhoneType == _Phone.PhoneType);

                if (phones.Any())
                {
                    _Phone = phones.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Phone.CreatedBy = _UserName;
                    _Phone.CreatedDateTime = _XmlCreatedDate;
                }

                _Phone.ModifiedBy = _UserName;
                _Phone.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAcadRecPhoneType1", "Error", ex.ToString());
            }
            return _Phone;
        }

        /// <summary>
        /// Parse/Create Email Type 1
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Email ParseAcadRecEmailType1(Library.Apas.AcademicRecord.EmailType1 email, string emailType)
        {
            Email _Email = new Email();
            try
            {
                _Email.EmailAddress = email.EmailAddress;
                _Email.EmailType = emailType;

                var emails = ctx.Emails.Where(x => x.EmailAddress == _Email.EmailAddress && x.EmailType == _Email.EmailType);

                if (emails.Any())
                {
                    _Email = emails.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Email.CreatedBy = _UserName;
                    _Email.CreatedDateTime = _XmlCreatedDate;
                }

                _Email.ModifiedBy = _UserName;
                _Email.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "saveEmail", "Error", ex.ToString());
            }
            return _Email;
        }

        /// <summary>
        /// Parse/Create AcademicRecord.Course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        private Course ParseAcadRecCourse(Library.Apas.AcademicRecord.CourseType course, Response response, DateTime? startDate = null, DateTime? endDate = null)
        {
            Course _Course = new Course();

            try
            {
                if (!string.IsNullOrWhiteSpace(course.CourseTitle)) _Course.Title = course.CourseTitle;
                if (!string.IsNullOrWhiteSpace(course.CourseNumber)) _Course.CourseNumber = course.CourseNumber;
                if (!string.IsNullOrWhiteSpace(course.OriginalCourseID)) _Course.OriginalCourseID = course.OriginalCourseID;
                if (!string.IsNullOrWhiteSpace(course.AgencyCourseID)) _Course.AgencyCourseID = course.AgencyCourseID;
                if (!string.IsNullOrWhiteSpace(course.CourseSectionNumber)) _Course.SectionNumber = course.CourseSectionNumber;

                var courses = ctx.Courses.Where(c => c.Title == _Course.Title &&
                                                     c.CourseNumber == _Course.CourseNumber &&
                                                     c.OriginalCourseID == _Course.OriginalCourseID &&
                                                     c.AgencyCourseID == _Course.AgencyCourseID &&
                                                     c.SectionNumber == _Course.SectionNumber);

                // Parse Course Override School
                if (course.CourseOverrideSchool != null)
                {
                    Institution _School = ParseAcadRecSchoolType(course.CourseOverrideSchool, response);
                    if (_School.InstitutionId < 1 || !courses.Any(y => y.CourseOverrideSchool_InstitutionId == _School.InstitutionId))
                    {
                        _Course.CourseOverrideSchool = _School;
                    }
                    else
                    {
                        courses = courses.Where(y => y.CourseOverrideSchool_InstitutionId == _School.InstitutionId || y.CourseOverrideSchool_InstitutionId == null);
                    }
                }

                if (response != null)
                {
                    // Filter by Student
                    if (response.RequestedStudent != null)
                    {
                        courses = courses.Where(c => c.AcademicRecord.Student.StudentId == response.RequestedStudent.StudentId);
                    }

                    // Filter by Response Id
                    if (response.ResponseId > 0)
                    {
                        courses = courses.Where(c => c.AcademicRecord.Responses.ResponseId == response.ResponseId);
                    }
                }

                // Update existing or create new record
                if (courses.Any())
                {
                    _Course = courses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                    if (courses.Count() > 1)
                    {
                        // If more than one found, try to separate by credit earned
                        courses = courses.Where(x => x.CreditEarned == course.CourseCreditEarned.ToString());
                        if (courses.Any())
                        {
                            _Course = courses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                        }
                    }
                }
                else
                {
                    _Course.CreatedBy = _UserName;
                    _Course.CreatedDateTime = _XmlCreatedDate;
                }

                _Course.ModifiedBy = _UserName;
                _Course.ModifiedDateTime = _XmlCreatedDate;

                // TODO: Do we need to parse addtinal course information stored into the UserDefinedExtensions complex element ???
                // It includes:
                //      • Course Title (full name exceeding maximum length limits defined by PESC)
                //      • CTS Occupational Area and Level - Ex: <OccupationalAreaName>Health Care Services</OccupationalAreaName><LevelName>Intermediate</LevelName>
                //      • End Notes attached to the course - Ex.: <EndNoteType>: MissingMark, CourseEquivalency, ExamExemptions / and <EndNoteDescription>: Note Tpe description

                // Create/Update remaining properties
                _Course.CreditBasis = course.CourseCreditBasis;
                if (!string.IsNullOrWhiteSpace(course.CourseNarrativeExplanationGrade)) _Course.NarrativeExplanationGrade = course.CourseNarrativeExplanationGrade;
                if (!string.IsNullOrWhiteSpace(course.CourseSubjectAbbreviation)) _Course.SubjectAbbreviation = course.CourseSubjectAbbreviation;
                if (!string.IsNullOrWhiteSpace(course.OverrideSchoolCourseNumber)) _Course.OverrideSchoolCourseNumber = course.OverrideSchoolCourseNumber;
                if (!string.IsNullOrWhiteSpace(course.CourseInstructionSiteName)) _Course.InstructionSiteName = course.CourseInstructionSiteName;
                if (!string.IsNullOrWhiteSpace(course.CourseAcademicGradeScaleCode)) _Course.AcademicGradeScaleCode = course.CourseAcademicGradeScaleCode.ToString();
                if (!string.IsNullOrWhiteSpace(course.CourseAcademicGrade))
                 {
                    _Course.AcademicGrade = course.CourseAcademicGrade.Trim().ToUpper().Replace(".000", "");

                    // Call GetColleagueExternalGradeId when parsing a received transcript and send an email if grade doesn't exists in Colleague
                    try
                    {
                        using (ColleagueLogic collLogic = new ColleagueLogic())
                        {
                            string gradeId = collLogic.GetColleagueExternalGradeId(_Course.AcademicGrade);
                            // If Grade is not in Colleague, send email
                            if (string.IsNullOrWhiteSpace(gradeId))
                            {
                                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAcadRecCourse", "Grade doesn't exists in Colleague", "Grade Code: " + _Course.AcademicGrade);
                                // Send email
                                SendGradeMissingEmail(_Course);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAcadRecCourse", "Error: Grade doesn't exists in Colleague", ex.ToString());
                    }
                }
                if (course.NoteMessage.Count > 0) _Course.NoteMessage = Functions.JoinStrings(course.NoteMessage);

                if (course.CourseQualityPointsEarnedSpecified) { _Course.QualityPointsEarned = course.CourseQualityPointsEarned.ToString(); }
                if (course.CourseGPAApplicabilityCodeSpecified) { _Course.GPAApplicabilityCode = course.CourseGPAApplicabilityCode; }
                if (course.CourseApplicabilitySpecified) { _Course.Applicability = course.CourseApplicability; }
                if (course.CourseInstructionSiteSpecified) { _Course.InstructionSite = course.CourseInstructionSite; }
                if (course.CourseRepeatCodeSpecified) { _Course.RepeatCode = course.CourseRepeatCode; }
                if (course.CourseAcademicGradeStatusCodeSpecified) { _Course.AcademicGradeStatusCode = course.CourseAcademicGradeStatusCode; }
                if (course.CourseAddDateSpecified) { _Course.AddDate = course.CourseAddDate; }
                if (course.CourseDropDateSpecified) { _Course.DropDate = course.CourseDropDate; }
                if (course.CourseBeginDateSpecified) { _Course.BeginDate = course.CourseBeginDate; } else { if (Functions.CheckForNullDate(startDate) != null) { _Course.BeginDate = startDate; } }  // Use Academic Session begin/end date if course date is empty
                if (course.CourseEndDateSpecified) { _Course.EndDate = course.CourseEndDate; } else { if (Functions.CheckForNullDate(endDate) != null) { _Course.EndDate = endDate; } }  // Use Academic Session begin/end date if course date is empty
                if (course.CourseLevelSpecified) { _Course.Level = course.CourseLevel; }
                if (course.CourseCreditEarnedSpecified) { _Course.CreditEarned = course.CourseCreditEarned.ToString(); }
                if (course.CourseCreditLevelSpecified) { _Course.CreditLevel = course.CourseCreditLevel; }
                if (course.CourseCreditUnitsSpecified) { _Course.CreditUnits = course.CourseCreditUnits; }
                if (course.CourseCreditValueSpecified) { _Course.CreditValue = course.CourseCreditValue.ToString(); }

                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAcadRecCourse", "Error", ex.ToString());
            }

            return _Course;
        }

        public bool SendGradeMissingEmail(Course course)
        {
            bool success = false;
            try
            {
                string courseName = string.Empty;
                string studentName = string.Empty;
                string ASN = string.Empty;
                string sNumber = string.Empty;
                string institutionName = string.Empty;

                if (course != null && !string.IsNullOrWhiteSpace(course.AcademicGrade))
                {
                    if (!string.IsNullOrWhiteSpace(course.SubjectAbbreviation)) courseName = course.SubjectAbbreviation + " - ";
                    courseName += course.Title;

                    if (course.AcademicRecord != null)
                    {
                        if (course.AcademicRecord.Student != null)
                        {
                            if (course.AcademicRecord.Student.Person != null && course.AcademicRecord.Student.Person.Names != null && course.AcademicRecord.Student.Person.Names.Count() > 0)
                            {
                                studentName = course.AcademicRecord.Student.Person.Names.OrderByDescending(o => o.CreatedDateTime).Select(s => s.FirstName + " " + s.LastName).FirstOrDefault();
                            }
                            if (course.AcademicRecord.Student.ASNs != null && course.AcademicRecord.Student.ASNs.Count() > 0)
                            {
                                ASN = course.AcademicRecord.Student.ASNs.OrderByDescending(o => o.CreatedDateTime).Select(s => s.AgencyAssignedID).FirstOrDefault();
                            }
                            if (course.AcademicRecord.Student.sNumbers != null && course.AcademicRecord.Student.sNumbers.Count() > 0)
                            {
                                sNumber = course.AcademicRecord.Student.sNumbers.OrderByDescending(o => o.CreatedDateTime).Select(s => s.sNumVal).FirstOrDefault();
                            }
                        }
                        if (course.AcademicRecord.Responses != null && course.AcademicRecord.Responses.TransmissionData != null &&
                            course.AcademicRecord.Responses.TransmissionData.SourceInstitution != null &&
                            course.AcademicRecord.Responses.TransmissionData.SourceInstitution.InstitutionNames.Any())
                        {
                            institutionName = course.AcademicRecord.Responses.TransmissionData.SourceInstitution.InstitutionNames.OrderByDescending(o => o.CreatedDateTime).Select(s => s.Name).FirstOrDefault();
                        }
                    }
                    // If settings allow to send missing grade email
                    if (GetSetting(Structs.SettingTypes.Boolean, Structs.EmailSettings.MissingGradeSendEmailEnabled))
                    {
                        new SendEmail(course.AcademicGrade, courseName, studentName, ASN, sNumber, institutionName);
                    }
                    success = true;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SendGradeMissingEmail", "Error", ex.ToString());
            }
            return success;
        }

        /// <summary>
        /// Parse/Create AcademicRecord.Session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        private AcademicSession ParseAcadRecSessionType(Library.Apas.AcademicRecord.AcademicSessionType session, Response response)
        {
            AcademicSession _AcademicSession = new AcademicSession();

            try
            {
                var academicSessions = ctx.AcademicSessions.AsQueryable();

                if (session.AcademicSessionDetail != null) {
                    // Name
                    if (!string.IsNullOrWhiteSpace(session.AcademicSessionDetail.SessionName))
                    {
                        _AcademicSession.Term = session.AcademicSessionDetail.SessionName;
                        academicSessions = academicSessions.Where(s => s.Term == _AcademicSession.Term);
                    }

                    // Session End date
                    if (session.AcademicSessionDetail.SessionEndDateSpecified)
                    {
                        _AcademicSession.EndDate = Functions.CheckForNullDate(session.AcademicSessionDetail.SessionEndDate);
                        academicSessions = academicSessions.Where(s => s.EndDate == _AcademicSession.EndDate);
                    }

                    // Session Begin date
                    if (session.AcademicSessionDetail.SessionBeginDateSpecified)
                    {
                        _AcademicSession.StartDate = Functions.CheckForNullDate(session.AcademicSessionDetail.SessionBeginDate);
                        academicSessions = academicSessions.Where(s => s.StartDate == _AcademicSession.StartDate);
                    }

                    // Session Designator
                    if (!string.IsNullOrWhiteSpace(session.AcademicSessionDetail.SessionDesignator))
                    {
                        _AcademicSession.Designator = session.AcademicSessionDetail.SessionDesignator;
                        academicSessions = academicSessions.Where(s => s.Designator == _AcademicSession.Designator);
                    }

                    // Session Type
                    if (session.AcademicSessionDetail.SessionType != null)
                    {
                        _AcademicSession.Type = session.AcademicSessionDetail.SessionType;
                        academicSessions = academicSessions.Where(s => s.Type == _AcademicSession.Type);
                    }

                    // Session School Year
                    if (!string.IsNullOrWhiteSpace(session.AcademicSessionDetail.SessionSchoolYear))
                    {
                        _AcademicSession.SchoolYear = session.AcademicSessionDetail.SessionSchoolYear;
                        academicSessions = academicSessions.Where(s => s.SchoolYear == _AcademicSession.SchoolYear);
                    }
                }

                if (response != null)
                {
                    // Filter by Student
                    if (response.RequestedStudent != null)
                    {
                        academicSessions = academicSessions.Where(s => s.AcademicRecord.Student.StudentId == response.RequestedStudent.StudentId);
                    }

                    // Filter by Response Id
                    if (response.ResponseId > 0)
                    {
                        academicSessions = academicSessions.Where(s => s.AcademicRecord.Responses.ResponseId == response.ResponseId);
                    }
                }

                // Verify if Academic Session already exists or create a new one
                if (academicSessions.Any())
                {
                    _AcademicSession = academicSessions.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _AcademicSession.CreatedBy = _UserName;
                    _AcademicSession.CreatedDateTime = _XmlCreatedDate;
                }

                _AcademicSession.ModifiedBy = _UserName;
                _AcademicSession.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAcadRecSessionType", "Error", ex.ToString());
            }
            return _AcademicSession;
        }

        /// <summary>
        /// Parse/Create AcademicRecord.Award
        /// </summary>
        /// <param name="academicAward"></param>
        /// <returns></returns>
        private AcademicAward ParseAcadRecAcademicAwardType(Library.Apas.AcademicRecord.AcademicAwardType academicAward, Response response)
        {
            AcademicAward _AcademicAward = new AcademicAward();

            try
            {
                _AcademicAward.AcademicAwardLevel = academicAward.AcademicAwardLevel;  // .GetXmlEnumAttribute(typeof(Library.Apas.CoreMain.AcademicAwardLevelType));
                _AcademicAward.AcademicAwardTitle = academicAward.AcademicAwardTitle;
                _AcademicAward.AcademicAwardDate = Functions.CheckForNullDate(academicAward.AcademicAwardDate);

                var academicAwards = ctx.AcademicAwards.Where(a => a.AcademicAwardLevel == _AcademicAward.AcademicAwardLevel &&
                                                                    a.AcademicAwardTitle == _AcademicAward.AcademicAwardTitle &&
                                                                    a.AcademicAwardDate == _AcademicAward.AcademicAwardDate);

                if (response != null)
                {
                    // Filter by Student
                    if (response.RequestedStudent != null)
                    {
                        academicAwards = academicAwards.Where(x => x.AcademicRecord.Student.StudentId == response.RequestedStudent.StudentId);
                    }

                    // Filter by Response Id
                    if (response.ResponseId > 0)
                    {
                        academicAwards = academicAwards.Where(x => x.AcademicRecord.Responses.ResponseId == response.ResponseId);
                    }
                }

                if (academicAwards.Any())
                {
                    _AcademicAward = academicAwards.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _AcademicAward.CreatedBy = _UserName;
                    _AcademicAward.CreatedDateTime = _XmlCreatedDate;
                }

                _AcademicAward.ModifiedBy = _UserName;
                _AcademicAward.ModifiedDateTime = _XmlCreatedDate;

            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAcadRecAcademicAwardType", "Error", ex.ToString());
            }
            return _AcademicAward;
        }

        /// <summary>
        /// Parse/Create AcademicRecord.Organization
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
		public Institution ParseAcadRecOrganizationType(Library.Apas.AcademicRecord.OrganizationType organization)
        {
            Institution _Institution = new Institution();

            try
            {
                _Institution.LocalOrganizationID = organization.LocalOrganizationID.LocalOrganizationIDCode;

                var institutions = ctx.Institutions.Where(i => i.LocalOrganizationID.Contains(_Institution.LocalOrganizationID));

                if (institutions.Any())
                {
                    _Institution = institutions.OrderByDescending(t => t.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Institution.CreatedBy = _UserName;
                    _Institution.CreatedDateTime = _XmlCreatedDate;
                }

                if (!string.IsNullOrWhiteSpace(organization.ESIS)) { _Institution.ESIS = organization.ESIS; }
                if (!string.IsNullOrWhiteSpace(organization.APAS)) { _Institution.APAS = organization.APAS; }

                _Institution.ModifiedBy = _UserName;
                _Institution.ModifiedDateTime = _XmlCreatedDate;

                foreach (string institutionName in organization.OrganizationName)
                {
                    InstitutionName _InstitutionName = ParseInstitutionName(institutionName, organization.LocalOrganizationID.LocalOrganizationIDCode);
                    if (_InstitutionName.InstitutionNameId < 1 || !_Institution.InstitutionNames.Any(x => x.InstitutionNameId == _InstitutionName.InstitutionNameId))
                    {
                        _Institution.InstitutionNames.Add(_InstitutionName);
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAcadRecOrganizationType", "Error", ex.ToString());
            }

            return _Institution;
        }

        /// <summary>
        /// Parse/Create AcademicRecord.Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public Student ParseAcadRecStudentType(Library.Apas.AcademicRecord.StudentType student, bool? collegeData = null)
        {
            Student _Student = new Student();

            try
            {
                Person _Person = ParseAcadRecPersonType(student.Person, collegeData);

                var students = ctx.Students.Where(a => a.Person.PersonId == _Person.PersonId);

                if (students.Any())
                {
                    _Student = students.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Student.CreatedBy = _UserName;
                    _Student.CreatedDateTime = _XmlCreatedDate;
                    _Student.Person = _Person;
                }

                _Student.ModifiedBy = _UserName;
                _Student.ModifiedDateTime = _XmlCreatedDate;

                // Parse ASN
                foreach (Library.Apas.CoreMain.AgencyIdentifierType agencyIdentifier in student.Person.AgencyIdentifier)
                {
                    ASN _ASN = ParseASN(agencyIdentifier, _Student.StudentId, collegeData);
                    if (_ASN.ASNId < 1 || !_Student.ASNs.Any(x => x.ASNId == _ASN.ASNId))
                    {
                        _Student.ASNs.Add(_ASN);
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAcadRecStudentType", "Error", ex.ToString());
            }

            return _Student;
        }

        /// <summary>
        /// Parse/Create AcademicRecord.Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public Student ParseAcadRecK12StudentType(Library.Apas.AcademicRecord.K12StudentType student)
        {
            Student _Student = new Student();

            try
            {
                Person _Person = ParseAcadRecK12PersonType(student.Person);

                var students = ctx.Students.Where(a => a.Person.PersonId == _Person.PersonId);

                if (students.Any())
                {
                    _Student = students.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Student.CreatedBy = _UserName;
                    _Student.CreatedDateTime = _XmlCreatedDate;
                    _Student.Person = _Person;
                }

                foreach (Library.Apas.CoreMain.AgencyIdentifierType agencyIdentifier in student.Person.AgencyIdentifier)
                {
                    ASN _ASN = ParseASN(agencyIdentifier, _Student.StudentId);
                    if (_ASN.ASNId < 1 || !_Student.ASNs.Any(x => x.ASNId == _ASN.ASNId))
                    {
                        _Student.ASNs.Add(_ASN);
                    }
                }

                _Student.ModifiedBy = _UserName;
                _Student.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAcadRecK12StudentType", "Error", ex.ToString());
            }

            return _Student;
        }

        /// <summary>
        /// Parse Create Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public Student ParseStudentType(Library.Apas.AcademicRecord.RequestedStudentType student)
        {
            Student _Student = new Student();

            try
            {
                Person _Person = ParseAcadRecPersonType(student.Person);

                var students = ctx.Students.Where(a => a.Person.PersonId == _Person.PersonId);

                if (students.Any())
                {
                    _Student = students.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Student.CreatedBy = _UserName;
                    _Student.CreatedDateTime = _XmlCreatedDate;
                    _Student.Person = _Person;
                }

                foreach (Library.Apas.CoreMain.AgencyIdentifierType agencyIdentifier in student.Person.AgencyIdentifier)
                {
                    ASN _ASN = ParseASN(agencyIdentifier, _Student.StudentId);
                    if (_ASN.ASNId < 1 || !_Student.ASNs.Any(x => x.ASNId == _ASN.ASNId))
                    {
                        _Student.ASNs.Add(_ASN);
                    }
                }

                _Student.ModifiedBy = _UserName;
                _Student.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseStudentType", "Error", ex.ToString());
            }

            return _Student;
        }

        /// <summary>
        /// Parse/Create ResponseHold
        /// </summary>
        /// <param name="hold"></param>
        /// <returns></returns>
        public ResponseHold ParseResponseHoldType(Library.Apas.AcademicRecord.ResponseHoldType hold, int? responseId = null)
        {
            ResponseHold _ResponseHold = new ResponseHold();

            try
            {
                _ResponseHold.HoldReason = hold.HoldReason;

                var responseHolds = ctx.ResponseHolds.Where(x => x.HoldReason == _ResponseHold.HoldReason);

                if (responseId != null)
                {
                    responseHolds = responseHolds.Where(x => x.Response_ResponseId == responseId);
                }

                if (hold.PlannedReleaseDateSpecified)
                {
                    _ResponseHold.PlannedReleaseDate = Functions.CheckForNullDate(hold.PlannedReleaseDate);
                    responseHolds = responseHolds.Where(x => x.PlannedReleaseDate == _ResponseHold.PlannedReleaseDate);
                }

                if (responseHolds.Any())
                {
                    _ResponseHold = responseHolds.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _ResponseHold.CreatedBy = _UserName;
                    _ResponseHold.CreatedDateTime = _XmlCreatedDate;
                }

                _ResponseHold.ModifiedBy = _UserName;
                _ResponseHold.ModifiedDateTime = _XmlCreatedDate;

            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseRequestedStudentType", "Error", ex.ToString());
            }

            return _ResponseHold;
        }

        /// <summary>
        /// Parse Multiple Note Messages
        /// </summary>
        /// <param name="noteMessages"></param>
        /// <returns></returns>
        public void ParseNoteMessageType(List<string> noteMessages, ref List<NoteMessage> _NoteMessages)
        {
            try
            {
                foreach (string noteMessage in noteMessages)
                {
                    NoteMessage _NoteMessage = ParseSingleNoteMessageType(noteMessage);
                    if (_NoteMessage.NoteMessageId < 1 || !_NoteMessages.Any(x => x.NoteMessageId == _NoteMessage.NoteMessageId))
                    {
                        _NoteMessages.Add(_NoteMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseNoteMessageType", "Error", ex.ToString());
            }
        }

        /// <summary>
        /// Parse/Create NoteMessageType
        /// </summary>
        /// <param name="noteMessage"></param>
        /// <returns></returns>
        public NoteMessage ParseSingleNoteMessageType(string noteMessage)
        {
            NoteMessage _NoteMessage = new NoteMessage();

            try
            {
                _NoteMessage.NoteMessageContent = noteMessage;
                var noteMessages = ctx.NoteMessages.Where(x => x.NoteMessageContent.Trim().ToUpper() == _NoteMessage.NoteMessageContent.Trim().ToUpper());

                if (noteMessages.Any())
                {
                    _NoteMessage = noteMessages.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _NoteMessage.CreatedBy = _UserName;
                    _NoteMessage.CreatedDateTime = _XmlCreatedDate;
                }

                _NoteMessage.ModifiedBy = _UserName;
                _NoteMessage.ModifiedDateTime = _XmlCreatedDate;

            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseSingleNoteMessageType", "Error", ex.ToString());
            }

            return _NoteMessage;
        }

        /// <summary>
        /// Parse/Create AcademicRecord.Person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public Person ParseAcadRecPersonType(Library.Apas.AcademicRecord.PersonType person, bool? collegeData = null)
        {
            Person _Person = new Person();
            DateTime? _BirthDate = null;

            try
            {
                // Handle possible null birthdates
                if (person.Birth != null) { _BirthDate = Functions.CheckForNullDate(person.Birth.BirthDate); }

                // Create ASN list to find a person
                List<string> asnList = person.AgencyIdentifier != null && person.AgencyIdentifier.Count() > 0 ? person.AgencyIdentifier.Select(x => x.AgencyAssignedID).ToList() : null;

                // Find person
                var persons = FindPerson(person.Name.FirstName, person.Name.LastName, person.Name.MiddleName, asnList, _BirthDate);

                if (persons.Any())
                {
                    _Person = persons.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Person.CreatedBy = _UserName;
                    _Person.CreatedDateTime = _XmlCreatedDate;
                }

                if (_BirthDate != null) { _Person.BirthDate = _BirthDate; }

                // Parse Name
                if (!(_Person.PersonId >= 1 && _Person.Names != null && _Person.Names.Any()))
                {
                    PersonName _Name = ParseCoreMainNameType(person.Name, Structs.Name.PersonalNameType, _Person.PersonId, collegeData);
                    if (_Name != null && (_Name.NameId < 1 || !_Person.Names.Any(x => x.NameId == _Name.NameId)))
                    {
                        _Person.Names.Add(_Name);
                    }
                }

                // Parse Alternate/Former Names
                foreach (Library.Apas.CoreMain.NameType altname in person.AlternateName)
                {
                    PersonName _AltName = ParseCoreMainNameType(altname, Structs.Name.FormerType, _Person.PersonId, collegeData);
                    if (_AltName != null && (_AltName.Person_PersonId < 1 || 
                                             !_Person.Names.Any(x => x.Person_PersonId == (_AltName.Person_PersonId != null ? _AltName.Person_PersonId : x.Person_PersonId) && 
                                                                (x.FirstName ?? string.Empty).Trim().ToUpper() == (_AltName.FirstName != null ? _AltName.FirstName.Trim().ToUpper() : (x.FirstName ?? string.Empty).Trim().ToUpper()) &&
                                                                (x.LastName ?? string.Empty).Trim().ToUpper() == (_AltName.LastName != null ? _AltName.LastName.Trim().ToUpper() : (x.LastName ?? string.Empty).Trim().ToUpper()) &&
                                                                (x.MiddleNames ?? string.Empty).Trim().ToUpper() == (_AltName.MiddleNames != null ? _AltName.MiddleNames.Trim().ToUpper() : (x.MiddleNames ?? string.Empty).Trim().ToUpper()))))
                    {
                        _Person.Names.Add(_AltName);
                    }
                }

                // Parse Gender
                if (person.Gender != null)
                {
                    Gender _Gender = new Gender();

                    var results = _Person.Genders.Where(x => x.GenderCodeType == person.Gender.GenderCode);

                    if (results.Any())
                    {
                        _Gender = results.FirstOrDefault();
                    }
                    else
                    {
                        _Gender.GenderCodeType = person.Gender.GenderCode;
                        _Gender.CreatedBy = _UserName;
                        _Gender.CreatedDateTime = _XmlCreatedDate;

                        _Person.Genders.Add(_Gender);
                    }

                    // Set College Data to True, it is False by Default
                    if (collegeData != null && collegeData == true)
                    {
                        _Gender.CollegeData = true;
                    }

                    _Gender.ModifiedBy = _UserName;
                    _Gender.ModifiedDateTime = _XmlCreatedDate;
                }

                // Parse Contact
                foreach (Library.Apas.AcademicRecord.ContactsType contact in person.Contacts)
                {
                    foreach (Library.Apas.CoreMain.EmailType email in contact.Email)
                    {
                        Email _Email = ParseCoreMainEmailType(email, Structs.Email.PrimaryEmailType, _Person.PersonId);
                        if (_Email.EmailId < 1 || !_Person.Emails.Any(x => x.EmailId == _Email.EmailId))
                        {
                            _Person.Emails.Add(_Email);
                        }
                    }

                    foreach (Library.Apas.CoreMain.PhoneType phone in contact.Phone)
                    {
                        Phone _Phone = ParseCoreMainPhoneType(phone, Enums.PhoneTypes.PermanentPhoneType, _Person.PersonId);
                        if (_Phone.PhoneId < 1 || !_Person.Phones.Any(x => x.PhoneId == _Phone.PhoneId))
                        {
                            _Person.Phones.Add(_Phone);
                        }
                    }

                    foreach (Library.Apas.CoreMain.PhoneType faxphone in contact.FaxPhone)
                    {
                        Phone _FaxPhone = ParseCoreMainPhoneType(faxphone, Enums.PhoneTypes.FaxPhoneType, _Person.PersonId);
                        if (_FaxPhone.PhoneId < 1 || !_Person.Phones.Any(x => x.PhoneId == _FaxPhone.PhoneId))
                        {
                            _Person.Phones.Add(_FaxPhone);
                        }
                    }

                    foreach (Library.Apas.CoreMain.AddressType address in contact.Address)
                    {
                        Address _Address = ParseCoreMainAddressType(address, Structs.Address.PermanentAddressType);
                        if (_Address.AddressId < 1 || !_Person.Addresses.Any(x => x.AddressId == _Address.AddressId))
                        {
                            _Person.Addresses.Add(_Address);
                        }
                    }
                }

                _Person.ModifiedBy = _UserName;
                _Person.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "parseApplicantPerson", "Error", ex.ToString());
            }
            return _Person;
        }

        /// <summary>
        /// Parse/Create Person Record
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public Person ParseAcadRecK12PersonType(Library.Apas.AcademicRecord.K12PersonType person)
        {
            Person _Person = new Person();
            DateTime? _BirthDate = null;

            try
            {
                // Handle BirthDate and MiddleNames
                if (person.Birth != null) { _BirthDate = Functions.CheckForNullDate(person.Birth.BirthDate); }

                // Create ASN list to find a person
                List<string> asnList = person.AgencyIdentifier != null && person.AgencyIdentifier.Count() > 0 ? person.AgencyIdentifier.Select(x => x.AgencyAssignedID).ToList() : null;

                // Check if person already exists
                var persons = FindPerson(person.Name.FirstName, person.Name.LastName, person.Name.MiddleName, asnList, _BirthDate);

                // Add new person or grab old if exists
                if (persons.Any())
                {
                    _Person = persons.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Person.CreatedBy = _UserName;
                    _Person.CreatedDateTime = _XmlCreatedDate;
                }

                if (_BirthDate != null) { _Person.BirthDate = _BirthDate; }

                // Person Names
                if (!(_Person.PersonId >= 1 && _Person.Names != null && _Person.Names.Any()))
                {
                    PersonName _Name = ParseCoreMainNameType(person.Name, Structs.Name.PersonalNameType, _Person.PersonId);
                    if (_Name != null && ( _Name.NameId < 1 || !_Person.Names.Any(x => x.NameId == _Name.NameId)))
                    {
                        _Person.Names.Add(_Name);
                    }
                }

                // Parse Gender
                if (person.Gender != null)
                {
                    Gender _Gender = new Gender();

                    var results = _Person.Genders.Where(x => x.GenderCodeType == person.Gender.GenderCode);

                    if (results.Any())
                    {
                        _Gender = results.FirstOrDefault();
                    }
                    else
                    {
                        _Gender.GenderCodeType = person.Gender.GenderCode;
                        _Gender.CreatedBy = _UserName;
                        _Gender.CreatedDateTime = _XmlCreatedDate;

                        _Person.Genders.Add(_Gender);
                    }

                    _Gender.ModifiedBy = _UserName;
                    _Gender.ModifiedDateTime = _XmlCreatedDate;
                }

                foreach (Library.Apas.AcademicRecord.ContactsType contact in person.Contacts)
                {
                    foreach (Library.Apas.CoreMain.EmailType email in contact.Email)
                    {
                        Email _Email = ParseCoreMainEmailType(email, Structs.Email.PrimaryEmailType, _Person.PersonId);
                        if (_Email.EmailId < 1 || !_Person.Emails.Any(x => x.EmailId == _Email.EmailId))
                        {
                            _Person.Emails.Add(_Email);
                        }
                    }

                    foreach (Library.Apas.CoreMain.PhoneType phone in contact.Phone)
                    {
                        Phone _Phone = ParseCoreMainPhoneType(phone, Enums.PhoneTypes.PermanentPhoneType, _Person.PersonId);
                        if (_Phone.PhoneId < 1 || !_Person.Phones.Any(x => x.PhoneId == _Phone.PhoneId))
                        {
                            _Person.Phones.Add(_Phone);
                        }
                    }

                    foreach (Library.Apas.CoreMain.PhoneType faxphone in contact.FaxPhone)
                    {
                        Phone _FaxPhone = ParseCoreMainPhoneType(faxphone, Enums.PhoneTypes.FaxPhoneType, _Person.PersonId);
                        if (_FaxPhone.PhoneId < 1 || !_Person.Phones.Any(x => x.PhoneId == _FaxPhone.PhoneId))
                        {
                            _Person.Phones.Add(_FaxPhone);
                        }
                    }

                    foreach (Library.Apas.CoreMain.AddressType address in contact.Address)
                    {
                        Address _Address = ParseCoreMainAddressType(address, Structs.Address.PermanentAddressType);
                        if (_Address.AddressId < 1 || !_Person.Addresses.Any(x => x.AddressId == _Address.AddressId))
                        {
                            _Address.AddressType = "ContactAddress";
                            _Person.Addresses.Add(_Address);
                        }
                    }
                }
                _Person.ModifiedBy = _UserName;
                _Person.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "parseApplicantPerson", "Error", ex.ToString());
            }
            return _Person;
        }

        /// <summary>
        /// Parse/Create AcademicRecord.Institution
        /// </summary>
        /// <param name="institution"></param>
        /// <returns></returns>
        public Institution ParseAcadRecInstitutionType(Library.Apas.AcademicRecord.SourceDestinationType institution)
        {
            Institution _Institution = new Institution();

            try
            {
                _Institution.LocalOrganizationID = institution.Organization.LocalOrganizationID.LocalOrganizationIDCode;

                var institutions = ctx.Institutions.Where(x => x.LocalOrganizationID.Contains(_Institution.LocalOrganizationID));

                if (institutions.Any())
                {
                    _Institution = institutions.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Institution.CreatedBy = _UserName;
                    _Institution.CreatedDateTime = _XmlCreatedDate;
                }

                if (!string.IsNullOrWhiteSpace(institution.Organization.ESIS)) { _Institution.ESIS = institution.Organization.ESIS; }
                if (!string.IsNullOrWhiteSpace(institution.Organization.APAS)) { _Institution.APAS = institution.Organization.APAS; }

                _Institution.ModifiedBy = _UserName;
                _Institution.ModifiedDateTime = _XmlCreatedDate;

                foreach (string instName in institution.Organization.OrganizationName)
                {
                    InstitutionName _InstitutionName = ParseInstitutionName(instName, institution.Organization.LocalOrganizationID.LocalOrganizationIDCode);
                    if (_InstitutionName.InstitutionNameId < 1 || !_Institution.InstitutionNames.Any(x => x.InstitutionNameId == _InstitutionName.InstitutionNameId))
                    {
                        _Institution.InstitutionNames.Add(_InstitutionName);
                    }
                }

                foreach (Library.Apas.AcademicRecord.ContactsType contact in institution.Organization.Contacts)
                {
                    foreach (Library.Apas.CoreMain.AddressType address in contact.Address)
                    {
                        Address _Address = ParseCoreMainAddressType(address, Structs.Address.InstitutionType);
                        if (_Address.AddressId < 1 || !_Institution.Addresses.Any(x => x.AddressId == _Address.AddressId))
                        {
                            _Institution.Addresses.Add(_Address);
                        }
                    }

                    foreach (Library.Apas.CoreMain.EmailType email in contact.Email)
                    {
                        Email _Email = ParseCoreMainEmailType(email, Structs.Email.InstitutionEmailType, _Institution.InstitutionId);
                        if (_Email.EmailId < 1 || !_Institution.Emails.Any(x => x.EmailId == _Email.EmailId))
                        {
                            _Institution.Emails.Add(_Email);
                        }
                    }

                    foreach (Library.Apas.CoreMain.PhoneType phone in contact.Phone)
                    {
                        Phone _Phone = ParseCoreMainPhoneType(phone, Enums.PhoneTypes.InstitutionPhoneType, null, _Institution.InstitutionId);
                        if (_Phone.PhoneId < 1 || !_Institution.Phones.Any(x => x.PhoneId == _Phone.PhoneId))
                        {
                            _Institution.Phones.Add(_Phone);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseAdmRecInstitutionType", "Error", ex.ToString());
            }
            return _Institution;
        }

        public CourseExportObj GetCourseToExport(int courseId, ref string institution, ref bool alreadyMarkedExported)
        {
            CourseExportObj course = null;
                
            try
            {
                var query = ctx.Courses.Where(x => x.CourseId == courseId);

                if (query.Any())
                {
                    Course _Course = query.OrderByDescending(y => y.CreatedDateTime).FirstOrDefault();

                    course = new CourseExportObj()
                    {
                        CourseId = _Course.CourseId,
                        RequestTrackingID = _Course.AcademicRecord.Responses.RequestTrackingID,
                    };

                    //if (RequestStudent)
                    if (true)
                    {
                        course.sNumber = _Course.AcademicRecord.Responses.RequestedStudent.sNumbers.OrderByDescending(a => a.CreatedDateTime).Select(b => b.sNumVal).FirstOrDefault();
                    //} else
                    //{
                    //    course.sNumber = _Course.AcademicRecord.Responses.MatchedStudent.sNumbers.OrderByDescending(a => a.CreatedDateTime).Select(b => b.sNumVal).FirstOrDefault();
                    }

                    // Check if external transcript was already exported to Colleague
                    alreadyMarkedExported = _Course.AcademicRecord.Responses.TransmissionData.ExportedDateTime != null;

                    if (!string.IsNullOrWhiteSpace(course.sNumber) && !string.IsNullOrWhiteSpace(course.RequestTrackingID))
                    {
                        // Get Colleague Institution Id
                        string collInstitutionId = null;
                        string collExternalGradeId = null;
                        using (ColleagueLogic collLogic = new ColleagueLogic())
                        {
                            string localOgrId = null;
                            string institutionName = null;
                            // Look for Transmission Data Source Institution
                            if (string.IsNullOrWhiteSpace(localOgrId) && _Course.AcademicRecord != null && _Course.AcademicRecord.Responses != null && _Course.AcademicRecord.Responses.TransmissionData != null &&
                               !string.IsNullOrWhiteSpace(_Course.AcademicRecord.Responses.TransmissionData.SourceInstitution.LocalOrganizationID))
                            {
                                localOgrId = _Course.AcademicRecord.Responses.TransmissionData.SourceInstitution.LocalOrganizationID;
                                institutionName = _Course.AcademicRecord.Responses.TransmissionData.SourceInstitution.InstitutionNames.OrderByDescending(o => o.CreatedDateTime).Select(s => s.Name).FirstOrDefault();
                            }
                            // Look for Academic Record Institution
                            if (string.IsNullOrWhiteSpace(localOgrId) && _Course.AcademicRecord != null && _Course.AcademicRecord.School != null &&
                               !string.IsNullOrWhiteSpace(_Course.AcademicRecord.School.LocalOrganizationID))
                            {
                                localOgrId = _Course.AcademicRecord.School.LocalOrganizationID;
                                institutionName = _Course.AcademicRecord.School.InstitutionNames.OrderByDescending(o => o.CreatedDateTime).Select(s => s.Name).FirstOrDefault();
                            }
                            // Look for Course Institution
                            if (string.IsNullOrWhiteSpace(localOgrId) && _Course.CourseOverrideSchool != null && !string.IsNullOrWhiteSpace(_Course.CourseOverrideSchool.LocalOrganizationID))
                            {
                                localOgrId = _Course.CourseOverrideSchool.LocalOrganizationID;
                                institutionName = _Course.CourseOverrideSchool.InstitutionNames.OrderByDescending(o => o.CreatedDateTime).Select(s => s.Name).FirstOrDefault();
                            }
                            institution = institutionName + " (" + localOgrId + ")";
                            // Look for Colleague Institution Id
                            if (!string.IsNullOrWhiteSpace(localOgrId))
                            {
                                collInstitutionId = collLogic.GetColleagueInstitutionId(localOgrId);
                            }
                            // Look for Colleague Grade Id using External Transcript Schema
                            if (!string.IsNullOrWhiteSpace(_Course.AcademicGrade))
                            {
                                collExternalGradeId = collLogic.GetColleagueExternalGradeId(_Course.AcademicGrade);
                            } else
                            {
                                // If grade is empty and the sender is University of Lethridge (48009000), use "TR" grade (Transfer Credit)
                                if (_Course.AcademicRecord.Responses.TransmissionData.SourceInstitution.LocalOrganizationID.Trim() == "48009000")
                                {
                                    collExternalGradeId = collLogic.GetColleagueExternalGradeId("TR");
                                }
                            }
                        }

                        // Credit Earned
                        if (!string.IsNullOrWhiteSpace(_Course.CreditEarned) && (_Course.CreditEarned.Length < 3 || _Course.CreditEarned.Contains(".")) &&
                            Double.TryParse(_Course.CreditEarned, out double creditEarned))
                        {
                            _Course.CreditEarned = (creditEarned * 100).ToString();  // Include decimals
                        }

                        // Load Transaction Course Object
                        course.Course = new CollWebApi.DataContracts.ContractRequest_InsertExtCrs()
                        {
                            AExternalTranscriptsId = _Course.AcademicRecord.Responses.ResponseId.ToString(),
                            AExtlPersonId = course.sNumber,
                            AExtlInstitution = collInstitutionId,
                            AExtlPersInstIdx = course.sNumber + collInstitutionId,
                            AExtlCourse = (_Course.AgencyCourseID ?? _Course.SubjectAbbreviation + _Course.CourseNumber).TruncateString(15),
                            AExtlTitle = _Course.Title.TruncateString(30),  // Max size allowed in Colleague
                            AExtlCred = _Course.CreditEarned.TruncateString(8),  // Max size allowed in Colleague
                            AExtlGrade = collExternalGradeId,
                            AExtlGradeScheme = Structs.Courses.GradeScheme,
                            AExtlStartDate = _Course.BeginDate != null ? _Course.BeginDate.Value.FormatColleagueDate() : null,
                            AExtlEndDate = _Course.EndDate != null ? _Course.EndDate.Value.FormatColleagueDate() : null,
                            AExtlOpr = _UserName.ToUpper(),
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetCourseToExport", "Error", ex.ToString());
            }
            return course;
        }

        public Student ParseStudent(StudentRecordObj _StudentObj, bool? collegeData = null)
        {
            Student _Student = null;
            try
            {
                if (_StudentObj != null)
                {
                    // Prepare student information
                    Library.Apas.AdmissionsRecord.ApplicantPersonType _Person = new Library.Apas.AdmissionsRecord.ApplicantPersonType();

                    // Birth Date
                    if (_StudentObj.BirthDate != null)
                    {
                        _Person.Birth = new Library.Apas.CoreMain.BirthType();
                        _Person.Birth.BirthDate = _StudentObj.BirthDate.Value;
                    }

                    // Name
                    if (!string.IsNullOrWhiteSpace(_StudentObj.FirstName) || !string.IsNullOrWhiteSpace(_StudentObj.MiddleName) || !string.IsNullOrWhiteSpace(_StudentObj.LastName))
                    {
                        _Person.Name = new Library.Apas.CoreMain.NameType();
                        if (!string.IsNullOrWhiteSpace(_StudentObj.FirstName)) _Person.Name.FirstName = _StudentObj.FirstName;
                        if (!string.IsNullOrWhiteSpace(_StudentObj.MiddleName)) _Person.Name.MiddleName = new List<string>() { _StudentObj.MiddleName };
                        if (!string.IsNullOrWhiteSpace(_StudentObj.LastName)) _Person.Name.LastName = _StudentObj.LastName;
                    }

                    // Former Names
                    if (_StudentObj.FormerNames != null && _StudentObj.FormerNames.Count() > 0)
                    {
                        _Person.AlternateName = new List<Library.Apas.CoreMain.NameType>();
                        foreach (StuNameObj _FormerName in _StudentObj.FormerNames)
                        {
                            if (!string.IsNullOrWhiteSpace(_FormerName.FirstName) || !string.IsNullOrWhiteSpace(_FormerName.MiddleName) || !string.IsNullOrWhiteSpace(_FormerName.LastName))
                            {
                                Library.Apas.CoreMain.NameType _CollName = new Library.Apas.CoreMain.NameType();
                                if (!string.IsNullOrWhiteSpace(_FormerName.FirstName)) _CollName.FirstName = _FormerName.FirstName;
                                if (!string.IsNullOrWhiteSpace(_FormerName.MiddleName)) _CollName.MiddleName = new List<string>() { _FormerName.MiddleName };
                                if (!string.IsNullOrWhiteSpace(_FormerName.LastName)) _CollName.LastName = _FormerName.LastName;
                                if (!string.IsNullOrWhiteSpace(_CollName.FirstName) || (_CollName.MiddleName != null && _CollName.MiddleName.Count() > 0) || !string.IsNullOrWhiteSpace(_CollName.LastName))
                                {
                                    _Person.AlternateName.Add(_CollName);
                                }
                            }
                        }
                    }

                    // ASN
                    if (!string.IsNullOrWhiteSpace(_StudentObj.ASN))
                    {
                        ASN _ASN = ctx.ASNs.Where(x => x.AgencyAssignedID == _StudentObj.ASN && x.CollegeData == true).OrderByDescending(y => y.ModifiedDateTime).FirstOrDefault();

                        _Person.AgencyIdentifier = new List<Library.Apas.CoreMain.AgencyIdentifierType>() {
                            new Library.Apas.CoreMain.AgencyIdentifierType() {
                                AgencyAssignedID = _StudentObj.ASN,
                                AgencyCode = Library.Apas.CoreMain.AgencyCodeType.Province,
                                AgencyName = _ASN != null && !string.IsNullOrWhiteSpace(_ASN.AgencyName) ? _ASN.AgencyName : Structs.Institution.AlbertaEducation,
                                CountryCode = _ASN != null && _ASN.CountryCode != null ? (Library.Apas.CoreMain.CountryCodeType)_ASN.CountryCode : Library.Apas.CoreMain.CountryCodeType.CA,
                                StateProvinceCode = _ASN != null && _ASN.StateProvinceCode != null ? (Library.Apas.CoreMain.StateProvinceCodeType)_ASN.StateProvinceCode : Library.Apas.CoreMain.StateProvinceCodeType.AB,
                            }
                        };
                    }

                    // Gender
                    if (_StudentObj.GenderCode != null)
                    {
                        _Person.Gender = new Library.Apas.CoreMain.GenderType();
                        _Person.Gender.GenderCode = _StudentObj.GenderCode.Value;
                    }

                    Library.Apas.AdmissionsRecord.ApplicantType _Applicant = new Library.Apas.AdmissionsRecord.ApplicantType(){
                        Person = _Person,
                    };

                    _Student = ParseApplicant(_Applicant, collegeData);
                    if (_Student != null && _Student.StudentId < 1)
                    {
                        ctx.Students.Add(_Student);
                    }

                    // Parse student information
                    //Person _DbPerson = ParseAdmRecApplicantPersonType(_Person, collegeData);
                    //if (_DbPerson.PersonId < 1 || _DbPerson.SourceInstitution.InstitutionId != _Source.InstitutionId)
                    //{
                    //    ctx.Persons.Add(_DbPerson);
                    //}

                    int res = ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptObject, "StudentLookup.ParseStudent", "Error", ex.ToString());
                _Student = null;
            }

            return _Student;
        }

        public bool SaveStudentSNumber(int? stuId, string sNum) {
            bool success = false;
            string sNumber = sNum.ToLower().Trim() ?? string.Empty;
            try
            {
                if (stuId != null && !string.IsNullOrWhiteSpace(sNum)) {
                    var student = ctx.Students.Where(x => x.StudentId == stuId).FirstOrDefault();

                    if (!student.sNumbers.Any() || student.sNumbers.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault().sNumVal != sNumber)
                    {
                        sNumber _sNumber = new sNumber()
                        {
                            sNumVal = sNumber,
                            CreatedBy = _UserName,
                            CreatedDateTime = DateTime.Now,
                        };

                        student.sNumbers.Add(_sNumber);
                        ctx.SaveChanges();
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveStudentSNumber", "Error", ex.ToString());
            }
            return success;
        }

        public bool GetLocalAliasedTestStudent(StuLookupListViewObj lookup) {
            bool success = false;

            try
            {
                var results = (from stu in ctx.Students
                               where stu.StudentId == lookup.SearchFilter.StudentRecord.StudentId
                               select new StudentRecordObj
                               {
                                   Snumber = stu.sNumbers.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault().sNumVal,
                                   ASN = stu.ASNs.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault().AgencyAssignedID,
                                   FirstName = stu.Person.Names.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().FirstName,
                                   MiddleName = stu.Person.Names.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().MiddleNames,
                                   LastName = stu.Person.Names.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().LastName,
                                   BirthDate = stu.Person.BirthDate,
                                   Gender = stu.Person.Genders.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().GenderCodeType.ToString(),
                                   Addr1 = stu.Person.Addresses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().AddressLine1 + " " + stu.Person.Addresses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().AddressLine2,
                                   City = stu.Person.Addresses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().City,
                                   State = stu.Person.Addresses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().Province,
                                   Zip = stu.Person.Addresses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().PostalCode,
                                   Country = stu.Person.Addresses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().Country ?? "CA",
                                   Phone = stu.Person.Phones.OrderByDescending(x=>x.ModifiedDateTime).FirstOrDefault().PhoneNumber,
                                   Email = stu.Person.Emails.Where(y=>y.EmailType == "PrimaryEmail").OrderByDescending(x=>x.ModifiedDateTime).FirstOrDefault().EmailAddress,
                                   FormerNames = (from f in ctx.PersonNames
                                                  where f.Person_PersonId == stu.PersonId
                                                  select new StuNameObj
                                                  {
                                                      LastName = f.LastName,
                                                      FirstName = f.FirstName,
                                                      MiddleName = f.MiddleNames,
                                                  }).ToList()
                               });

                lookup.StudentRecords.Add((StudentRecordObj)results.FirstOrDefault());
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetAliasedTestStudent", "Error", ex.ToString());
            }
            return success;
        }

        #endregion AcademicRecord

        #region Parse CoreMain

        /// <summary>
        /// Parses/Updates deserialized CoreMain.PhoneType to local database
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public Phone ParseCoreMainPhoneType(Library.Apas.CoreMain.PhoneType phone, Enums.PhoneTypes phoneType, int? personId = null, int? institutionId = null)
        {
            Phone _Phone = new Phone();

            try
            {
                _Phone.CountryCode = phone.CountryPrefixCode ?? "1";
                _Phone.AreaCode = phone.AreaCityCode ?? string.Empty;
                _Phone.PhoneNumber = phone.PhoneNumber ?? string.Empty;
                _Phone.PhoneType = !phone.NoteMessage.Any() ? phoneType : phone.NoteMessage.First() == Structs.Phone.FaxPhoneType ? Enums.PhoneTypes.FaxPhoneType : phone.NoteMessage.First() == Structs.Phone.MobilePhoneType ? Enums.PhoneTypes.MobilePhoneType : phoneType;
                _Phone.PhoneNumberExtension = phone.PhoneNumberExtension;

                var phones = ctx.Phones.Where(p => p.CountryCode.Trim().ToUpper() == _Phone.CountryCode.Trim().ToUpper() &&
                                                   p.AreaCode.Trim().ToUpper() == _Phone.AreaCode.Trim().ToUpper() &&
                                                   p.PhoneNumber.Trim().ToUpper() == _Phone.PhoneNumber.Trim().ToUpper() &&
                                                   p.PhoneType == _Phone.PhoneType);

                if (personId != null) { phones = phones.Where(x => x.PersonId == personId); }
                if (institutionId != null) { phones = phones.Where(x => x.InstitutionId == institutionId); }

                if (phones.Any())
                {
                    _Phone = phones.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Phone.CreatedBy = _UserName;
                    _Phone.CreatedDateTime = _XmlCreatedDate;
                }

                _Phone.ModifiedBy = _UserName;
                _Phone.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseCoreMainPhoneType", "Error", ex.ToString());
            }
            return _Phone;
        }

        /// <summary>
        /// Parses/Updates deserialized CoreMain.EmailType to local database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Email ParseCoreMainEmailType(Library.Apas.CoreMain.EmailType email, string emailType, int? personId = null, int? institutionId = null)
        {
            Email _Email = new Email();

            try
            {
                _Email.EmailAddress = email.EmailAddress;
                _Email.EmailType = emailType;

                var emails = ctx.Emails.Where(x => x.EmailAddress.Trim().ToLower() == _Email.EmailAddress.Trim().ToLower() && x.EmailType == emailType.ToString());

                if (personId != null) { emails = emails.Where(x => x.PersonId == personId); }
                if (institutionId != null) { emails = emails.Where(x => x.InstitutionId == institutionId); }

                if (emails.Any())
                {
                    _Email = emails.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _Email.CreatedBy = _UserName;
                    _Email.CreatedDateTime = _XmlCreatedDate;
                }

                _Email.ModifiedBy = _UserName;
                _Email.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseCoreMainEmailType", "Error", ex.ToString());
            }
            return _Email;
        }

        /// <summary>
        /// Parses/Updates deserialized CoreMain.AddressType to local database
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Address ParseCoreMainAddressType(Library.Apas.CoreMain.AddressType address, string addressType, string localOrganizationId = null)
        {
            Address _TempAddress = new Address();
            Address _RetAddress = new Address();
            string[] prov = { "AB", "BC", "MB", "NB", "NL", "NT", "NS", "NU", "ON", "PE", "QC", "SK", "YT" };

            localOrganizationId = localOrganizationId ?? string.Empty;

            try
            {
                // Instantiate and set local db copy of incoming address
                _TempAddress.City = address.City;
                _TempAddress.AddressType = addressType;

                if (address.Items != null && address.Items.Any())
                {
                    for (int i = 0; i < address.Items.Count(); i++)
                    {
                        switch (address.ItemsElementName[i])
                        {
                            case Library.Apas.CoreMain.ItemsChoiceType.CountryCode:
                                _TempAddress.Country = address.Items[i].ToString();
                                break;
                            case Library.Apas.CoreMain.ItemsChoiceType.PostalCode:
                                _TempAddress.PostalCode = address.Items[i].ToString();
                                break;
                            case Library.Apas.CoreMain.ItemsChoiceType.StateProvince:
                            case Library.Apas.CoreMain.ItemsChoiceType.StateProvinceCode:
                                _TempAddress.Province = address.Items[i].ToString();
                                break;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(_TempAddress.Country) && prov.Contains(_TempAddress.Province))
                    {
                        _TempAddress.Country = "XCA";
                    }

                    // Normalize space in Postal Code
                    _TempAddress.PostalCode = _TempAddress.PostalCode.NormalizePostalCode(_TempAddress.Country);
                }

                if (address.AddressLine.ToList().Count >= 1) { _TempAddress.AddressLine1 = address.AddressLine.ElementAt(0).CleanAddressLine(); }
                if (address.AddressLine.ToList().Count >= 2) { _TempAddress.AddressLine2 = address.AddressLine.ElementAt(1).CleanAddressLine(); }
                if (address.AddressLine.ToList().Count >= 3) { _TempAddress.AddressLine2 = string.Concat(_TempAddress.AddressLine2, " " + address.AddressLine.ElementAt(2)).CleanAddressLine(); }

                var addresses = ctx.Addresses.Where(a => (a.Institution.LocalOrganizationID == localOrganizationId) ||
                                                        (a.Province.Trim().ToUpper() == _TempAddress.Province.Trim().ToUpper() &&
                                                         a.City.Trim().ToUpper() == _TempAddress.City.Trim().ToUpper() &&
                                                         a.PostalCode.Trim().ToUpper() == _TempAddress.PostalCode.Trim().ToUpper() &&
                                                         a.AddressLine1.Trim().ToUpper() == _TempAddress.AddressLine1.Trim().ToUpper() &&
                                                         a.AddressLine2.Trim().ToUpper() == _TempAddress.AddressLine2.Trim().ToUpper()));

                if (!string.IsNullOrWhiteSpace(localOrganizationId))
                {
                    addresses = addresses.Where(x => x.Institution.LocalOrganizationID.Trim().ToUpper() == localOrganizationId.Trim().ToUpper());
                }

                if (addresses.Any())
                {
                    _RetAddress = addresses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _RetAddress.CreatedBy = _UserName;
                    _RetAddress.CreatedDateTime = _XmlCreatedDate;
                }

                _RetAddress.AddressLine1 = _TempAddress.AddressLine1;
                _RetAddress.AddressLine2 = _TempAddress.AddressLine2;
                _RetAddress.City = _TempAddress.City;
                _RetAddress.Province = _TempAddress.Province;
                _RetAddress.PostalCode = _TempAddress.PostalCode;
                _RetAddress.Country = _TempAddress.Country;
                _RetAddress.AddressType = _TempAddress.AddressType;
                _RetAddress.ModifiedBy = _UserName;
                _RetAddress.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseCoreMainAddressType", "Error", ex.ToString());
            }
            return _RetAddress;
        }

        /// <summary>
        /// Parses/Updates deserialized CoreMain.NameType to local database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PersonName ParseCoreMainNameType(Library.Apas.CoreMain.NameType name, string nameType, int? PersonId = null, bool? collegeData = null)
        {
            PersonName _Name = new PersonName();

            try
            {
                _Name.FirstName = Functions.CleanString(name.FirstName);
                _Name.LastName = Functions.CleanString(name.LastName);

                // Join Middle Names
                string middleName = string.Empty;
                if (name.MiddleName != null && name.MiddleName.Any()) middleName = Functions.JoinStrings(name.MiddleName.Distinct().ToList());  // Remove duplicate middle names
                _Name.MiddleNames = Functions.CleanString(middleName);

                if (!string.IsNullOrWhiteSpace(_Name.FirstName) || !string.IsNullOrWhiteSpace(_Name.LastName) || !string.IsNullOrWhiteSpace(_Name.MiddleNames))
                {
                    var names = ctx.PersonNames.Where(e => e.FirstName.Trim().ToUpper() == _Name.FirstName.Trim().ToUpper() &&
                                                        e.LastName.Trim().ToUpper() == _Name.LastName.Trim().ToUpper() &&
                                                        e.MiddleNames.Trim().ToUpper() == _Name.MiddleNames.Trim().ToUpper());

                    if (PersonId != null)
                    {
                        names = names.Where(e => e.Person_PersonId == PersonId);
                    }

                    if (names.Any())
                    {
                        _Name = names.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                    }
                    else
                    {
                        _Name.NameType = nameType;
                        _Name.CreatedBy = _UserName;
                        _Name.CreatedDateTime = _XmlCreatedDate;
                    }

                    // Set College Data to True, it is False by Default
                    if (collegeData != null && collegeData == true)
                    {
                        _Name.CollegeData = (bool)collegeData;
                    }

                    _Name.ModifiedBy = _UserName;
                    _Name.ModifiedDateTime = _XmlCreatedDate;
                } else
                {
                    _Name = null;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseCoreMainNameType", "Error", ex.ToString());
            }

            return _Name;
        }
        #endregion Parse CoreMain

        #region Parse Strings

        /// <summary>
        /// Parse/Create ASN
        /// </summary>
        /// <param name="asn"></param>
        /// <returns></returns>
        public ASN ParseASN(Library.Apas.CoreMain.AgencyIdentifierType agencyIdentifier, int? studentId = null, bool? collegeData = null)
        {
            ASN _ASN = new ASN();

            try
            {
                _ASN.AgencyAssignedID = Functions.CleanString(agencyIdentifier.AgencyAssignedID).Trim() ?? string.Empty;
                _ASN.AgencyCode = agencyIdentifier.AgencyCode;
                _ASN.AgencyName = agencyIdentifier.AgencyName != null ? agencyIdentifier.AgencyName.Trim() : agencyIdentifier.AgencyName;
                _ASN.CountryCode = agencyIdentifier.CountryCode;
                _ASN.StateProvinceCode = agencyIdentifier.StateProvinceCode;

                var asns = ctx.ASNs.Where(x => x.AgencyAssignedID == _ASN.AgencyAssignedID &&
                                          x.AgencyCode == _ASN.AgencyCode &&
                                          x.AgencyName.Trim().ToUpper() == _ASN.AgencyName.Trim().ToUpper() &&
                                          x.CountryCode == _ASN.CountryCode &&
                                          x.StateProvinceCode == _ASN.StateProvinceCode);

                // Filter by Student ID
                if (studentId != null && studentId > 0)
                {
                    asns = asns.Where(s => s.Student.StudentId == studentId);
                }

                if (asns.Any())
                {
                    _ASN = asns.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _ASN.CreatedBy = _UserName;
                    _ASN.CreatedDateTime = _XmlCreatedDate;
                }

                //_ASN.StateProvinceCode = Library.Apas.CoreMain.StateProvinceCodeType.AB;

                // Set College Data to True, it is False by Default
                if (collegeData != null && collegeData == true)
                {
                    _ASN.CollegeData = (bool)collegeData;
                }

                _ASN.ModifiedBy = _UserName;
                _ASN.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseASN", "Error", ex.ToString());
            }

            return _ASN;
        }

        /// <summary>
        /// Parse/Create Institution Name
        /// </summary>
        /// <param name="institutionName"></param>
        /// <returns></returns>
        public InstitutionName ParseInstitutionName(string institutionName, string localOrganizationId = null)
        {
            InstitutionName _InstitutionName = new InstitutionName();

            institutionName = institutionName ?? string.Empty;
            localOrganizationId = localOrganizationId ?? string.Empty;

            try
            {
                _InstitutionName.Name = Functions.CleanString(institutionName.ToString().ToUpper()) ?? string.Empty;

                var institutionNames = ctx.InstitutionNames.Where(x => x.Name.Trim().ToUpper() == _InstitutionName.Name.Trim().ToUpper() ||
                                                                      (x.Institution.LocalOrganizationID == localOrganizationId));

                if (!string.IsNullOrWhiteSpace(localOrganizationId))
                {
                    institutionNames = institutionNames.Where(x => x.Institution.LocalOrganizationID.Trim().ToUpper() == localOrganizationId.Trim().ToUpper());
                }

                if (institutionNames.Any())
                {
                    _InstitutionName = institutionNames.OrderByDescending(t => t.ModifiedDateTime).FirstOrDefault();
                }
                else
                {
                    _InstitutionName.CreatedBy = _UserName;
                    _InstitutionName.CreatedDateTime = _XmlCreatedDate;
                }

                _InstitutionName.ModifiedBy = _UserName;
                _InstitutionName.ModifiedDateTime = _XmlCreatedDate;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "saveInstitutionName", "Error", ex.ToString());
            }
            return _InstitutionName;
        }

        #endregion Parse Strings

        #region Deserialize Messages
        public Library.Apas.AdmissionsApplication.AdmissionsApplication DeserializeAdmApp(Library.Apas.AdmissionsApplication.AdmissionsApplication app, string xmlString)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(app.GetType());
                TextReader tr = new StringReader(xmlString);
                app = (Library.Apas.AdmissionsApplication.AdmissionsApplication)ser.Deserialize(tr);
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "DeserializeAdmApp", "Error: ", ex.ToString());
            }
            return app;
        }

        public static Library.Apas.AdmissionsApplication.AdmissionsApplication DeserializeAdmApp(string xmlString)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Library.Apas.AdmissionsApplication.AdmissionsApplication));
            TextReader tr = new StringReader(xmlString);
            return (Library.Apas.AdmissionsApplication.AdmissionsApplication)ser.Deserialize(tr);
        }

        public static Library.Apas.StatisticalData.StatisticalDataType DeserializeStatData(string xmlResponse)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Library.Apas.StatisticalData.StatisticalDataType));
            TextReader tr = new StringReader(xmlResponse);
            return (Library.Apas.StatisticalData.StatisticalDataType)ser.Deserialize(tr);
        }

        /// <summary> Turn XML response document into a transcript response object.</summary>
        /// <param name="theResponseXml">String containing the XML version of the object.</param>
        /// <returns>TranscriptRequestSchema.TranscriptRequest object resulting from deserializing the Xml passed in.</returns>
        public static Library.Apas.TranscriptResponse.TranscriptResponse DeserializeTranscriptResponse(string xmlResponse)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Library.Apas.TranscriptResponse.TranscriptResponse));
            TextReader tr = new StringReader(xmlResponse);
            return (Library.Apas.TranscriptResponse.TranscriptResponse)ser.Deserialize(tr);
        }

        /// <summary> Turn XML response document into a transcript response object. </summary>
        /// <param name="theResponseXml">String containing the XML version of the object.</param>
        /// <returns>HighSchoolSchema.HighSchoolTranscript object resulting from deserializing the Xml passed in.</returns>
        /// public HighSchoolSchema.HighSchoolTranscript GetHighSchoolTranscriptObject(string theResponseXml)
        public static Library.Apas.HighSchoolTranscript.HighSchoolTranscript DeserializeHighSchoolTranscript(string xmlResponse)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Library.Apas.HighSchoolTranscript.HighSchoolTranscript));
            TextReader tr = new StringReader(xmlResponse);
            return (Library.Apas.HighSchoolTranscript.HighSchoolTranscript)ser.Deserialize(tr);
        }

        /// <summary> Turn XML response document into a transcript response object. </summary>
        /// <param name="theResponseXml">String containing the XML version of the object.</param>
        /// <returns>CollegeSchema.CollegeTranscript object resulting from deserializing the Xml passed in.</returns>
        /// public lcapas.Models.CollegeTranscript GetCollegeTranscriptObject(string theResponseXml)
        public static Library.Apas.CollegeTranscript.CollegeTranscript DeserializeCollegeTranscript(string xmlResponse)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Library.Apas.CollegeTranscript.CollegeTranscript));
            TextReader tr = new StringReader(xmlResponse);
            return (Library.Apas.CollegeTranscript.CollegeTranscript)ser.Deserialize(tr);
        }

        /// <summary> Turn XML response document into a transcript response object. </summary>
        /// <param name="theResponseXml">String containing the XML version of the object.</param>
        /// <returns>CollegeSchema.CollegeTranscript object resulting from deserializing the Xml passed in.</returns>
        /// public lcapas.Models.CollegeTranscript GetCollegeTranscriptObject(string theResponseXml)
        public static Library.Apas.TranscriptRequest.TranscriptRequest DeserializeTranscriptRequest(string xmlRequest)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Library.Apas.TranscriptRequest.TranscriptRequest));
            TextReader tr = new StringReader(xmlRequest);
            return (Library.Apas.TranscriptRequest.TranscriptRequest)ser.Deserialize(tr);
        }

        public static Library.Apas.ErrorMessage.ErrorMessageType DeserializeErrorMessage(string xmlErrorMessage)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Library.Apas.ErrorMessage.ErrorMessageType));
            TextReader tr = new StringReader(xmlErrorMessage);
            return (Library.Apas.ErrorMessage.ErrorMessageType)ser.Deserialize(tr);
        }

        #endregion Deserialize Messages

        #endregion

        #region Transcripts

        public Library.Apas.TranscriptRequest.TranscriptRequest GetNewTranscriptRequest(string snum = null)
        {
            Library.Apas.TranscriptRequest.TranscriptRequest _TranscriptRequest = new Library.Apas.TranscriptRequest.TranscriptRequest();
            try
            {
                _TranscriptRequest.TransmissionData = new Library.Apas.AcademicRecord.TransmissionDataType()
                {
                    DocumentID = Guid.NewGuid().ToString(),
                    CreatedDateTime = DateTime.Now,
                    DocumentTypeCode = Library.Apas.CoreMain.DocumentTypeCodeType.InstitutionRequest,
                    TransmissionType = Library.Apas.CoreMain.TransmissionTypeType.Original,
                    DocumentProcessCode = Library.Apas.CoreMain.DocumentProcessCodeType.PRODUCTION,
                    DocumentOfficialCode = Library.Apas.CoreMain.DocumentOfficialCodeType.Official,
                    DocumentCompleteCode = Library.Apas.CoreMain.DocumentCompleteCodeType.Complete,
                    Source = new Library.Apas.AcademicRecord.SourceDestinationType
                    {
                        Organization = new Library.Apas.AcademicRecord.OrganizationType
                        {
                            APAS = GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                            LocalOrganizationID = new Library.Apas.CoreMain.LocalOrganizationIDType()
                            {
                                LocalOrganizationIDCode = GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                                LocalOrganizationIDQualifier = Library.Apas.CoreMain.LocalOrganizationIDCodeQualifierType.AB,
                            },
                            OrganizationName = new List<string>()
                            {
                                //GetInstitutionName()
                            },
                            Contacts = new List<Library.Apas.AcademicRecord.ContactsType>
                            {
                                // Get Contacts
                            },
                        },
                    },
                    RequestTrackingID = Functions.GetNewRequestTrackingId(),
                    NoteMessage = new List<string>()
                    {
                        // GetNoteMessages
                    }
                };

                _TranscriptRequest.Request[0] = new Library.Apas.AcademicRecord.RequestType()
                {

                };

            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetNewTranscriptRequest", "Error", ex.ToString());
            }

            return _TranscriptRequest;
        }

        public Response GetResponse(string uuid)
        {
            Response _Response = new Response();

            try
            {
                _Response = (from r in ctx.Responses
                             where r.TransmissionData.Uuid == uuid
                             select r).OrderByDescending(x => x.TransmissionData.CreatedDateTime).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetResponse", "Error", ex.ToString());
            }
            return _Response;
        }

        public Response GetResponseByTransDataId(string transDataId)
        {
            Response _Response = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(transDataId))
                {
                    _Response = (from r in ctx.Responses
                                 where r.TransmissionData.TransmissionDataId.ToString() == transDataId
                                 select r).OrderByDescending(x => x.TransmissionData.CreatedDateTime).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetResponseByTransDataId", "Error", ex.ToString());
            }
            return _Response;
        }

        public bool CheckResponseStudentSNumber(string transDataId, bool matchedStudent = false)
        {
            bool success = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(transDataId))
                {
                    Response _Response = ctx.Responses.Where(x => x.TransmissionData.TransmissionDataId.ToString() == transDataId).OrderByDescending(o => o.CreatedDateTime).FirstOrDefault();

                    if (matchedStudent)
                    {
                        if (_Response != null && _Response.MatchedStudent != null && _Response.MatchedStudent.sNumbers != null && _Response.MatchedStudent.sNumbers.Count() > 0)
                        {
                            success = true;
                        }
                    }
                    else
                    {
                        if (_Response != null && _Response.RequestedStudent != null && _Response.RequestedStudent.sNumbers != null && _Response.RequestedStudent.sNumbers.Count() > 0)
                        {
                            success = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "CheckResponseStudentSNumber", "Error", ex.ToString());
            }
            return success;
        }

        public bool LoadTransferRequests(RequestListViewObj requestListView, bool? reportFlag = null)
        {
            bool success = false;
            List<string> SrcLOrgID = null;
            string DestLOrgID = string.Empty;
            IQueryable<RequestObj> results = null;
            DateTime recentRequests;
            bool queryFiltered = false;

            List<Library.Apas.CoreMain.DocumentTypeCodeType> _Matches = new List<Library.Apas.CoreMain.DocumentTypeCodeType>() { };
            _Matches.Add(Library.Apas.CoreMain.DocumentTypeCodeType.Request);
            _Matches.Add(Library.Apas.CoreMain.DocumentTypeCodeType.ThirdPartyRequest);
            _Matches.Add(Library.Apas.CoreMain.DocumentTypeCodeType.InstitutionRequest);
            _Matches.Add(Library.Apas.CoreMain.DocumentTypeCodeType.StudentRequest);

            List<Library.Apas.AcademicRecord.HoldTypeType?> _ReportHoldTypes = new List<Library.Apas.AcademicRecord.HoldTypeType?>();
            _ReportHoldTypes.Add(Library.Apas.AcademicRecord.HoldTypeType.AfterDegreeAwarded);
            _ReportHoldTypes.Add(Library.Apas.AcademicRecord.HoldTypeType.AfterGradesPosted);
            _ReportHoldTypes.Add(Library.Apas.AcademicRecord.HoldTypeType.AfterSpecifiedDate);
            _ReportHoldTypes.Add(Library.Apas.AcademicRecord.HoldTypeType.AfterSpecifiedTerm);

            List<Library.Apas.AcademicRecord.ResponseStatusType> _HideResponseStatus = new List<Library.Apas.AcademicRecord.ResponseStatusType>() { };
            _HideResponseStatus.Add(Library.Apas.AcademicRecord.ResponseStatusType.TranscriptSent);
            _HideResponseStatus.Add(Library.Apas.AcademicRecord.ResponseStatusType.Canceled);
            _HideResponseStatus.Add(Library.Apas.AcademicRecord.ResponseStatusType.OfflineRecordSent);

            try
            {
                try
                {
                    recentRequests = DateTime.Now.AddDays(-1 * GetSetting(Structs.SettingTypes.Integer, Structs.Settings.RequestMaxLoad));
                }
                catch (Exception ex)
                {
                    SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "LoadAdmissionsOutbox", "Error", ex.ToString());
                    recentRequests = DateTime.Now.AddDays(-10);  // Load last 10 days responses by default
                };

                SrcLOrgID = requestListView.SearchFilter.LocalOrganizationIDs ?? new List<string>(){
                    GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                    GetSetting(Structs.SettingTypes.String, Structs.Settings.PescApasCode),
                };
                if (requestListView.SearchFilter.DestinationOrganization > 0)
                {
                    DestLOrgID = GetLocalOrganizationID(requestListView.SearchFilter.DestinationOrganization);
                    queryFiltered = true;
                }

                results = ctx.Requests.Where(x => _Matches.Contains(x.TransmissionData.DocumentTypeCode ?? 0) && (reportFlag != null && reportFlag == true ? x.Recipients.Any(h => h.TranscriptHolds.Any(t => _ReportHoldTypes.Contains(t.HoldType))) : true))
                        .Select(y => new RequestObj(){
                            Request = y,
                            ResponseStatus = ctx.Responses.Where(r => y.Recipients.Any(z => z.RequestTrackingID == r.RequestTrackingID) && (requestListView.DestAction == Structs.DestActions.AdmissionsOutbox ? r.ReceivedDateTime != null : r.SentDateTime != null)).OrderByDescending(o => o.CreatedDateTime).Select(e => e.ResponseStatusType).FirstOrDefault(),
                            ResponseCount = ctx.Responses.Where(r => y.Recipients.Any(z => z.RequestTrackingID == r.RequestTrackingID) && (requestListView.DestAction == Structs.DestActions.AdmissionsOutbox ? r.ReceivedDateTime != null : r.SentDateTime != null)).Count(),
                            TranscriptCount = ctx.Responses.Count(r => y.Recipients.Any(z => z.RequestTrackingID == r.RequestTrackingID) && (r.ResponseStatusType == null || r.ResponseStatusType == Library.Apas.AcademicRecord.ResponseStatusType.TranscriptSent) && (requestListView.DestAction == Structs.DestActions.AdmissionsOutbox ? r.ReceivedDateTime != null : r.SentDateTime != null)),
                            ErrorMessageCount = ctx.ReceivedErrors.Count(r => r.RequestTrackingID == y.TransmissionData.RequestTrackingID),
                            //RequestOrder = 0,  // ctx.Responses.Where(r => y.Recipients.Any(z => z.RequestTrackingID == r.RequestTrackingID)).OrderByDescending(o => o.ModifiedDateTime).Select(e => e.ResponseStatusType).FirstOrDefault() == Library.Apas.AcademicRecord.ResponseStatusType.Hold ? 1 : 0,
                            Operator = ctx.Users.Where(u => u.sNumber == y.ModifiedBy).OrderByDescending(o => o.CreatedDateTime).Select(t => t.FirstName + " " + t.LastName.Substring(0, 1)).FirstOrDefault(),
                        }).AsQueryable();

                if (results.Any())
                {
                    // setup source and destination
                    switch (requestListView.DestAction)
                    {
                        case Structs.DestActions.AdmissionsOutbox:
                            results = results.Where(x => SrcLOrgID.Contains(x.Request.TransmissionData.SourceInstitution.LocalOrganizationID) && x.Request.SentDateTime != null);
                            if (!string.IsNullOrWhiteSpace(DestLOrgID)) results = results.Where(x => x.Request.TransmissionData.DestinationInstitution.LocalOrganizationID.Contains(DestLOrgID));
                            break;
                        case Structs.DestActions.RecordsInbox:
                            results = results.Where(x => SrcLOrgID.Contains(x.Request.TransmissionData.DestinationInstitution.LocalOrganizationID) && x.Request.ReceivedDateTime != null);
                            if (!string.IsNullOrWhiteSpace(DestLOrgID)) results = results.Where(x => x.Request.TransmissionData.SourceInstitution.LocalOrganizationID.Contains(DestLOrgID));
                            break;
                    }

                    if (requestListView.SearchFilter.SelectedTransID != null)
                    {
                        var _RequestTrackingId = ctx.TransmissionDatas.Where(x => x.TransmissionDataId == requestListView.SearchFilter.SelectedTransID).FirstOrDefault().RequestTrackingID;

                        if (!string.IsNullOrWhiteSpace(_RequestTrackingId))
                        {
                            results = results.Where(x => x.Request.TransmissionData.RequestTrackingID.Trim().ToUpper() == _RequestTrackingId.Trim().ToUpper());
                            queryFiltered = true;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(requestListView.SearchFilter.StudentRecord.Snumber))
                    {
                        results = results.Where(x => x.Request.RequestedStudent.sNumbers.Any(y => y.sNumVal.Trim().ToUpper().Contains(requestListView.SearchFilter.StudentRecord.Snumber.Trim().ToUpper())));
                        queryFiltered = true;
                    }

                    if (!string.IsNullOrWhiteSpace(requestListView.SearchFilter.StudentRecord.ASN))
                    {
                        results = results.Where(x => x.Request.RequestedStudent.ASNs.Any(y => y.AgencyAssignedID.Trim().ToUpper().Contains(requestListView.SearchFilter.StudentRecord.ASN.Trim().ToUpper())));
                        queryFiltered = true;
                    }

                    switch (requestListView.SearchFilter.ListType)
                    {
                        case Enums.TransListTypes.New:
                            results = results.Where(x => x.Request.TransmissionData.ExportedDateTime == null);
                            queryFiltered = true;
                            break;
                        case Enums.TransListTypes.Exported:
                            results = results.Where(x => x.Request.TransmissionData.ExportedDateTime != null);
                            queryFiltered = true;
                            break;
                    }

                    switch (requestListView.SearchFilter.RespTransType)
                    {
                        case Enums.RespTransTypes.Resp:
                            results = results.Where(x => x.Request.TransmissionData.Responses.Any(y => y.ResponseStatusType != null));
                            queryFiltered = true;
                            break;
                        case Enums.RespTransTypes.Trans:
                            results = results.Where(x => x.Request.TransmissionData.Responses.Any(y => y.ResponseStatusType == null));
                            queryFiltered = true;
                            break;
                    }

                    if (!string.IsNullOrWhiteSpace(requestListView.SearchFilter.HoldType))
                    {
                        int holdId = Convert.ToInt32(requestListView.SearchFilter.HoldType);
                        results = results.Where(x => x.Request.Recipients.Any(y => y.TranscriptHolds.Any(z => z.HoldType == (Library.Apas.AcademicRecord.HoldTypeType)holdId)));
                        queryFiltered = true;
                    }

                    if (!string.IsNullOrWhiteSpace(requestListView.SearchFilter.HoldTypeData))
                    {
                        if (!string.IsNullOrWhiteSpace(requestListView.SearchFilter.HoldType) &&
                            Enum.TryParse(requestListView.SearchFilter.HoldType, out Library.Apas.AcademicRecord.HoldTypeType holdTypeId) &&
                            Enum.IsDefined(typeof(Library.Apas.AcademicRecord.HoldTypeType), holdTypeId))
                        {
                            switch (holdTypeId)
                            {
                                case Library.Apas.AcademicRecord.HoldTypeType.AfterSpecifiedTerm:
                                case Library.Apas.AcademicRecord.HoldTypeType.AfterGradesPosted:
                                    results = results.Where(x => x.Request.Recipients.Any(y => y.TranscriptHolds.Any(z => z.SessionDesignator.Contains(requestListView.SearchFilter.HoldTypeData))));
                                    queryFiltered = true;
                                    break;
                                case Library.Apas.AcademicRecord.HoldTypeType.AfterSpecifiedDate:
                                    results = results.Where(x => x.Request.Recipients.Any(y => y.TranscriptHolds.Any(z => z.ReleaseDate.ToString().Contains(requestListView.SearchFilter.HoldTypeData))));
                                    queryFiltered = true;
                                    break;
                                default:
                                    results = results.Where(x => x.Request.Recipients.Any(y => y.TranscriptHolds.Any(z => z.HoldTypeData == requestListView.SearchFilter.HoldTypeData)));
                                    queryFiltered = true;
                                    break;
                            };
                        }
                        else
                        {
                            results = results.Where(x => x.Request.Recipients.Any(y => y.TranscriptHolds.Any(z => z.HoldTypeData == requestListView.SearchFilter.HoldTypeData)));
                            queryFiltered = true;
                        };
                    }

                    if (requestListView.SearchFilter.Status != null)
                    {
                        results = results.Where(x => x.ResponseStatus == (Library.Apas.AcademicRecord.ResponseStatusType)requestListView.SearchFilter.Status);
                        queryFiltered = true;
                    }

                    if (requestListView.SearchFilter.Operator != null)
                    {
                        results = results.Where(x => x.Operator.Trim().ToUpper().Contains(requestListView.SearchFilter.Operator.Trim().ToUpper()));
                        queryFiltered = true;
                    }

                    if (requestListView.SearchFilter.FromStatusDate != null)
                    {
                        results = results.Where(x => x.Request.CreatedDateTime >= requestListView.SearchFilter.FromStatusDate);
                        queryFiltered = true;
                    }
                    if (requestListView.SearchFilter.ToStatusDate != null)
                    {
                        results = results.Where(x => x.Request.CreatedDateTime <= requestListView.SearchFilter.ToStatusDate);
                        queryFiltered = true;
                    }

                    if (requestListView.SearchFilter.StudentRecord.FullName != null && !string.IsNullOrWhiteSpace(requestListView.SearchFilter.StudentRecord.FullName))
                    {
                        string[] _StudentNames = Functions.SeparateNames(requestListView.SearchFilter.StudentRecord.FullName);
                        string firstName = string.Empty; string middleName = string.Empty; string lastName = string.Empty;

                        int cnt = 0;
                        if (!string.IsNullOrWhiteSpace(_StudentNames[0])) { firstName = _StudentNames[0].Trim().ToUpper(); cnt++; }
                        if (!string.IsNullOrWhiteSpace(_StudentNames[1])) { middleName = _StudentNames[1].Trim().ToUpper(); cnt++; }
                        if (!string.IsNullOrWhiteSpace(_StudentNames[2])) { lastName = _StudentNames[2].Trim().ToUpper(); cnt++; }

                        if (!string.IsNullOrWhiteSpace(firstName))
                        {
                            if (cnt == 1) {
                                results = results.Where(x => x.Request.RequestedStudent.Person.Names.Any(y => y.LastName.Trim().ToUpper().Contains(firstName) || y.FirstName.Trim().ToUpper().Contains(firstName) || y.MiddleNames.Trim().ToUpper().Contains(firstName)));
                                queryFiltered = true;
                            }
                            if (!string.IsNullOrWhiteSpace(lastName))
                            {
                                if (cnt == 2) {
                                    results = results.Where(x => x.Request.RequestedStudent.Person.Names.Any(y => y.FirstName.Trim().ToUpper().Contains(firstName) && y.LastName.Trim().ToUpper().Contains(lastName)));
                                    queryFiltered = true;
                                }
                                if (!string.IsNullOrWhiteSpace(middleName))
                                {
                                    if (cnt == 3) {
                                        results = results.Where(x => x.Request.RequestedStudent.Person.Names.Any(y => y.LastName.Trim().ToUpper().Contains(firstName) && y.FirstName.Trim().ToUpper().Contains(firstName) && y.MiddleNames.Trim().ToUpper().Contains(firstName)));
                                        queryFiltered = true;
                                    }
                                }
                            }
                        }
                    }

                    // Bring only the recent requests to speed up the query, including not viewed requests, if there are no search filter fields
                    if (!queryFiltered)
                    {
                        switch (requestListView.DestAction)
                        {
                            case Structs.DestActions.AdmissionsOutbox:
                                results = results.Where(x => x.Request.SentDateTime >= recentRequests ||
                                                            (x.Request.ViewedDateTime == null &&
                                                                ((x.TranscriptCount == 0 && x.ResponseCount == 0) ||
                                                                (x.ResponseCount > 0 && x.ResponseStatus != null &&
                                                                 !_HideResponseStatus.Contains((Library.Apas.AcademicRecord.ResponseStatusType)x.ResponseStatus)))
                                                                 ));
                                break;
                            case Structs.DestActions.RecordsInbox:
                                results = results.Where(x => x.Request.ReceivedDateTime >= recentRequests || 
                                                            (x.Request.ViewedDateTime == null && x.Request.SentToColleagueTRRQ == null &&
                                                                ((x.TranscriptCount == 0 && x.ResponseCount == 0) ||
                                                                (x.ResponseCount > 0 && x.ResponseStatus != null &&
                                                                 !_HideResponseStatus.Contains((Library.Apas.AcademicRecord.ResponseStatusType)x.ResponseStatus)))
                                                            ));
                                break;
                        }
                    }

                    requestListView.Pagination.RecCount = results.Count();
                    requestListView.Pagination.PageCount = 1;
                    if (results.Count() > 0)
                    {
                        if (!queryFiltered)
                        {
                            // Default load, no search field, show older requestes first/top
                            switch (requestListView.DestAction)
                            {
                                case Structs.DestActions.AdmissionsOutbox:
                                    results = results.OrderBy(r => r.Request.SentDateTime < recentRequests ? r.Request.SentDateTime : DateTime.Now)
                                                     .ThenBy(o => o.Request.ViewedDateTime == null ? 0 : 1)
                                                     .ThenByDescending(x => x.Request.TransmissionData.CreatedDateTime);
                                    break;
                                case Structs.DestActions.RecordsInbox:
                                    results = results.OrderBy(r => r.Request.ReceivedDateTime < recentRequests ? r.Request.ReceivedDateTime : DateTime.Now)
                                                     .ThenBy(o => o.Request.ViewedDateTime == null ? 0 : 1)
                                                     .ThenByDescending(x => x.Request.TransmissionData.CreatedDateTime);
                                    break;
                            }
                        } else
                        {
                            // If there is a search field, the show the most recent first/top
                            results = results.OrderByDescending(x => x.Request.TransmissionData.CreatedDateTime);
                        }

                        requestListView.TranscriptRequests = results.ToList().Skip((requestListView.Pagination.PageIndex - 1) * requestListView.Pagination.PageSize).Take(requestListView.Pagination.PageSize).ToList();
                        requestListView.Pagination.PageCount = (results.Count() + requestListView.Pagination.PageSize - 1) / requestListView.Pagination.PageSize;
                    }
                }

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "LoadAdmissionsOutbox", "Error", ex.ToString());
            }

            return success;

        }

        public bool LoadTransferResponses(ResponseListViewObj responses)
        {
            bool success = false;
            List<string> SrcLOrgID = null;
            string DestLOrgID = string.Empty;
            DateTime recentResponses;
            bool queryFiltered = false;

            List<Library.Apas.CoreMain.DocumentTypeCodeType> _Matches = new List<Library.Apas.CoreMain.DocumentTypeCodeType>() { };
            _Matches.Add(Library.Apas.CoreMain.DocumentTypeCodeType.Response);
            _Matches.Add(Library.Apas.CoreMain.DocumentTypeCodeType.InstitutionRequest);
            _Matches.Add(Library.Apas.CoreMain.DocumentTypeCodeType.ThirdPartyRequest);

            List<Library.Apas.AcademicRecord.ResponseStatusType> _HideResponseStatus = new List<Library.Apas.AcademicRecord.ResponseStatusType>() { };
            _HideResponseStatus.Add(Library.Apas.AcademicRecord.ResponseStatusType.TranscriptSent);
            _HideResponseStatus.Add(Library.Apas.AcademicRecord.ResponseStatusType.Canceled);
            _HideResponseStatus.Add(Library.Apas.AcademicRecord.ResponseStatusType.OfflineRecordSent);

            try
            {
                try
                {
                    recentResponses = DateTime.Now.AddDays(-1 * GetSetting(Structs.SettingTypes.Integer, Structs.Settings.ResponseMaxLoad));
                }
                catch (Exception ex)
                {
                    SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "LoadRecordsOutbox", "Error", ex.ToString());
                    recentResponses = DateTime.Now.AddDays(-10);  // Load last 10 days responses by default
                };

                SrcLOrgID = responses.SearchFilter.LocalOrganizationIDs ?? new List<string>(){
                    GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                    GetSetting(Structs.SettingTypes.String, Structs.Settings.PescApasCode),
                };

                if (responses.SearchFilter.RequestingInstitution > 0)
                {
                    DestLOrgID = GetLocalOrganizationID(responses.SearchFilter.RequestingInstitution);
                    queryFiltered = true;
                }

                var results = ctx.Responses.Where(x => _Matches.Contains(x.TransmissionData.DocumentTypeCode ?? 0))
                                .Select(y => new ResponseObj() {
                                    Response = y,
                                    RequestCount = ctx.Requests.Where(r => r.TransmissionData.RequestTrackingID == y.RequestTrackingID).Count(),
                                    RequestSentToTRRQ = ctx.Requests.Where(r => r.TransmissionData.RequestTrackingID == y.RequestTrackingID).OrderByDescending(o => o.CreatedDateTime).Select(t => t.SentToColleagueTRRQ).FirstOrDefault(),
                                    RequestTransDataUUID = ctx.Requests.Where(r => r.TransmissionData.RequestTrackingID == y.RequestTrackingID).OrderByDescending(o => o.CreatedDateTime).Select(t => t.TransmissionData.Uuid).FirstOrDefault(),
                                    ErrorMessageCount = ctx.ReceivedErrors.Count(r => r.RequestTrackingID == y.TransmissionData.RequestTrackingID),
                                    //ResponseOrder = 0,  // y.ResponseStatusType == Library.Apas.AcademicRecord.ResponseStatusType.Hold ? 1 : 0,
                                    //Operator = ctx.Users.Where(u => u.sNumber == y.ModifiedBy).OrderByDescending(o => o.CreatedDateTime).Select(t => t.FirstName).FirstOrDefault(),
                                    Operator = ctx.Users.Where(u => u.sNumber == (responses.DestAction == Structs.DestActions.AdmissionsInbox ? ctx.Requests.Where(r => r.TransmissionData.RequestTrackingID == y.RequestTrackingID).OrderByDescending(d => d.CreatedDateTime).Select(s => s.ModifiedBy).FirstOrDefault() : y.ModifiedBy)).OrderByDescending(o => o.CreatedDateTime).Select(t => t.FirstName + " " + t.LastName.Substring(0, 1)).FirstOrDefault(),
                                }).AsQueryable();

                switch (responses.DestAction)
                {
                    case Structs.DestActions.AdmissionsInbox:
                        results = results.Where(x => SrcLOrgID.Contains(x.Response.TransmissionData.DestinationInstitution.LocalOrganizationID) && x.Response.ReceivedDateTime != null);
                        if (!string.IsNullOrWhiteSpace(DestLOrgID)) results = results.Where(x => x.Response.TransmissionData.SourceInstitution.LocalOrganizationID == DestLOrgID);
                        break;
                    case Structs.DestActions.RecordsOutbox:
                        results = results.Where(x => SrcLOrgID.Contains(x.Response.TransmissionData.SourceInstitution.LocalOrganizationID) && x.Response.SentDateTime != null);
                        if (!string.IsNullOrWhiteSpace(DestLOrgID)) results = results.Where(x => x.Response.TransmissionData.DestinationInstitution.LocalOrganizationID == DestLOrgID);
                        break;
                }

                if (responses.SearchFilter.SelectedTransID != null)
                {
                    var _RequestTrackingId = ctx.TransmissionDatas.Where(x => x.TransmissionDataId == responses.SearchFilter.SelectedTransID).FirstOrDefault().RequestTrackingID;

                    if (!string.IsNullOrWhiteSpace(_RequestTrackingId))
                    {
                        results = results.Where(x => x.Response.TransmissionData.RequestTrackingID.Trim().ToUpper() == _RequestTrackingId.Trim().ToUpper());
                        queryFiltered = true;
                    }
                }

                if (!string.IsNullOrWhiteSpace(responses.SearchFilter.StudentRecord.Snumber))
                {
                    results = results.Where(x => x.Response.RequestedStudent.sNumbers.Any(y => y.sNumVal.Trim().ToUpper().Contains(responses.SearchFilter.StudentRecord.Snumber.Trim().ToUpper())));
                    queryFiltered = true;
                }

                if (!string.IsNullOrWhiteSpace(responses.SearchFilter.StudentRecord.ASN))
                {
                    results = results.Where(x => x.Response.RequestedStudent.ASNs.Any(y => y.AgencyAssignedID.Trim().ToUpper().Contains(responses.SearchFilter.StudentRecord.ASN.Trim().ToUpper())));
                    queryFiltered = true;
                }

                switch (responses.SearchFilter.ListType)
                {
                    case Enums.TransListTypes.New:
                        results = results.Where(x => x.Response.TransmissionData.ExportedDateTime == null);
                        queryFiltered = true;
                        break;
                    case Enums.TransListTypes.Exported:
                        results = results.Where(x => x.Response.TransmissionData.ExportedDateTime != null);
                        queryFiltered = true;
                        break;
                }

                switch (responses.SearchFilter.RespTransType)
                {
                    case Enums.RespTransTypes.Resp:
                        results = results.Where(x => x.Response.ResponseStatusType != null);
                        queryFiltered = true;
                        break;
                    case Enums.RespTransTypes.Trans:
                        results = results.Where(x => x.Response.ResponseStatusType == null);
                        queryFiltered = true;
                        break;
                }

                if (responses.SearchFilter.HoldReasonType != null)
                {
                    results = results.Where(x => x.Response.ResponseHolds.OrderByDescending(y => y.CreatedDateTime).FirstOrDefault().HoldReason == (Library.Apas.AcademicRecord.HoldReasonType)responses.SearchFilter.HoldReasonType);
                    queryFiltered = true;
                }

                if (responses.SearchFilter.HoldTypeData != null)
                {
                    results = results.Where(x => x.Response.ResponseHolds.OrderByDescending(y => y.CreatedDateTime).FirstOrDefault().PlannedReleaseDate.ToString().Contains(responses.SearchFilter.HoldTypeData));
                    queryFiltered = true;
                }

                if (responses.SearchFilter.Status != null)
                {
                    results = results.Where(x => x.Response.ResponseStatusType == (Library.Apas.AcademicRecord.ResponseStatusType)responses.SearchFilter.Status);
                    queryFiltered = true;
                }

                if (responses.SearchFilter.Operator != null)
                {
                    results = results.Where(x => x.Operator.Trim().ToUpper().Contains(responses.SearchFilter.Operator.Trim().ToUpper()));
                    queryFiltered = true;
                }

                if (responses.SearchFilter.FromStatusDate != null)
                {
                    results = results.Where(x => x.Response.CreatedDateTime >= responses.SearchFilter.FromStatusDate);
                    queryFiltered = true;
                }
                if (responses.SearchFilter.ToStatusDate != null)
                {
                    results = results.Where(x => x.Response.CreatedDateTime <= responses.SearchFilter.ToStatusDate);
                    queryFiltered = true;
                }

                if (responses.SearchFilter.StudentRecord != null && !string.IsNullOrWhiteSpace(responses.SearchFilter.StudentRecord.FullName))
                {
                    string[] _StudentNames = Functions.SeparateNames(responses.SearchFilter.StudentRecord.FullName);
                    string firstName = string.Empty; string middleName = string.Empty; string lastName = string.Empty;

                    int cnt = 0;
                    if (!string.IsNullOrWhiteSpace(_StudentNames[0])) { firstName = _StudentNames[0].Trim().ToUpper(); cnt++; }
                    if (!string.IsNullOrWhiteSpace(_StudentNames[1])) { middleName = _StudentNames[1].Trim().ToUpper(); cnt++; }
                    if (!string.IsNullOrWhiteSpace(_StudentNames[2])) { lastName = _StudentNames[2].Trim().ToUpper(); cnt++; }

                    if (!string.IsNullOrWhiteSpace(firstName))
                    {
                        if (cnt == 1) {
                            results = results.Where(x => x.Response.RequestedStudent.Person.Names.Any(y => y.LastName.Trim().ToUpper().Contains(firstName) || y.FirstName.Trim().ToUpper().Contains(firstName) || y.MiddleNames.Trim().ToUpper().Contains(firstName)));
                            queryFiltered = true;
                        }
                        if (!string.IsNullOrWhiteSpace(lastName))
                        {
                            if (cnt == 2) {
                                results = results.Where(x => x.Response.RequestedStudent.Person.Names.Any(y => y.FirstName.Trim().ToUpper().Contains(firstName) && y.LastName.Trim().ToUpper().Contains(lastName)));
                                queryFiltered = true;
                            }
                            if (!string.IsNullOrWhiteSpace(middleName))
                            {
                                if (cnt == 3) {
                                    results = results.Where(x => x.Response.RequestedStudent.Person.Names.Any(y => y.LastName.Trim().ToUpper().Contains(lastName) && y.FirstName.Trim().ToUpper().Contains(firstName) && y.MiddleNames.Trim().ToUpper().Contains(middleName)));
                                    queryFiltered = true;
                                }
                            }
                        }
                    }
                }

                // Bring only the recent responses to speed up the query, including not viewed responses, if there are no search filter fields
                if (!queryFiltered)
                {
                    switch (responses.DestAction)
                    {
                        case Structs.DestActions.AdmissionsInbox:
                            results = results.Where(x => x.Response.ReceivedDateTime >= recentResponses || 
                                                        (x.Response.ViewedDateTime == null &&
                                                            (x.Response.ResponseStatusType.HasValue &&
                                                             !_HideResponseStatus.Contains((Library.Apas.AcademicRecord.ResponseStatusType)x.Response.ResponseStatusType))));
                            break;
                        case Structs.DestActions.RecordsOutbox:
                            results = results.Where(x => x.Response.SentDateTime >= recentResponses ||
                                                        (x.Response.ViewedDateTime == null && x.RequestSentToTRRQ == null &&
                                                            (x.Response.ResponseStatusType == null ||
                                                             !_HideResponseStatus.Contains((Library.Apas.AcademicRecord.ResponseStatusType)x.Response.ResponseStatusType))));
                            break;
                    }
                }

                responses.Pagination.RecCount = results.Count();
                responses.Pagination.PageCount = 1;
                if (results.Count() > 0)
                {
                    if (!queryFiltered)
                    {
                        // Default load, no search field, show older requestes first/top
                        switch (responses.DestAction)
                        {
                            case Structs.DestActions.AdmissionsInbox:
                                results = results.OrderBy(r => r.Response.ReceivedDateTime < recentResponses ? r.Response.ReceivedDateTime : DateTime.Now)
                                                 .ThenBy(o => o.Response.ViewedDateTime == null ? 0 : 1)
                                                 .ThenByDescending(x => x.Response.TransmissionData.CreatedDateTime);
                                break;
                            case Structs.DestActions.RecordsOutbox:
                                results = results.OrderBy(r => r.Response.SentDateTime < recentResponses ? r.Response.SentDateTime : DateTime.Now)
                                                 .ThenBy(o => o.Response.ViewedDateTime == null ? 0 : 1)
                                                 .ThenByDescending(x => x.Response.TransmissionData.CreatedDateTime);
                                break;
                        }
                    }
                    else
                    {
                        // If there is a search field, the show the most recent first/top
                        results = results.OrderByDescending(x => x.Response.TransmissionData.CreatedDateTime);
                    }

                    responses.TranscriptResponses = results.ToList().Skip((responses.Pagination.PageIndex - 1) * responses.Pagination.PageSize).Take(responses.Pagination.PageSize).ToList();
                    responses.Pagination.PageCount = (results.Count() + responses.Pagination.PageSize - 1) / responses.Pagination.PageSize;
                }
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "LoadRecordsOutbox", "Error", ex.ToString());
            }
            return success;
        }

        public Request GetRequestByTransDataId(int? transDataId)
        {
            Request _Request = new Request();
            try
            {
                _Request = ctx.Requests.Where(x => x.TransmissionData.TransmissionDataId == transDataId).OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetRequestByTransDataId", "Error", ex.ToString());
            }

            return _Request;
        }

        public Request GetRequest(string uuid)
        {
            Request _Request = null;

            try
            {
                _Request = (from r in ctx.Requests
                            where r.TransmissionData.Uuid == uuid
                            select r).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetRequest", "Error", ex.ToString());
            }
            return _Request;
        }

        public bool CheckRequestStudentSNumber(string uuid, bool matchedStudent = false)
        {
            bool success = false;

            try
            {
                Request _Request = GetRequest(uuid);

                if (matchedStudent)
                {
                    if (_Request != null && _Request.MatchedStudent != null && _Request.MatchedStudent.sNumbers != null && _Request.MatchedStudent.sNumbers.Count() > 0)
                    {
                        success = true;
                    }
                }
                else
                {
                    if (_Request != null && _Request.RequestedStudent != null && _Request.RequestedStudent.sNumbers != null && _Request.RequestedStudent.sNumbers.Count() > 0)
                    {
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "CheckRequestStudentSNumber", "Error", ex.ToString());
            }
            return success;
        }

        public bool MarkRequestSent(string uuid)
        {
            bool success = false;
            Request _Request = new Request();

            try
            {
                _Request = (from r in ctx.Requests
                            where r.TransmissionData.Uuid == uuid
                            select r).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();

                _Request.SentDateTime = DateTime.Now;

                ctx.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "MarkRequestSent", "Error", ex.ToString());
            }
            return success;
        }

        public bool MarkRequestViewed(string uuid)
        {
            bool success = false;
            Request _Request = new Request();

            try
            {
                _Request = (from r in ctx.Requests
                            where r.TransmissionData.Uuid == uuid
                            select r).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();

                if (_Request != null)
                {
                    _Request.ViewedDateTime = DateTime.Now;

                    ctx.SaveChanges();
                }

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "MarkRequestViewed", "Error", ex.ToString());
            }
            return success;
        }

        public bool MarkRequestViewedByRequestTrackingId(string requestTrackingID)
        {
            bool success = false;
            Request _Request = new Request();

            try
            {
                _Request = (from r in ctx.Requests
                            where r.TransmissionData.RequestTrackingID == requestTrackingID
                            select r).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();

                if (_Request != null)
                {
                    _Request.ViewedDateTime = DateTime.Now;

                    ctx.SaveChanges();
                }

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "MarkRequestViewedByRequestTrackingId", "Error", ex.ToString());
            }
            return success;
        }

        public bool MarkResponseViewed(string uuid)
        {
            bool success = false;
            Response _Response = new Response();

            try
            {
                _Response = (from r in ctx.Responses
                            where r.TransmissionData.Uuid == uuid
                            select r).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();

                if (_Response != null)
                {
                    _Response.ViewedDateTime = DateTime.Now;

                    ctx.SaveChanges();
                }

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "MarkResponseViewed", "Error", ex.ToString());
            }
            return success;
        }

        public TransmissionData GetTransmissionData(string documentId)
        {
            TransmissionData _TransmissionData = new TransmissionData();

            try
            {
                _TransmissionData = (from r in ctx.TransmissionDatas
                                     where r.DocumentID == documentId
                                     select r).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetTransmissionData", "Error", ex.ToString());
            }
            return _TransmissionData;
        }

        public TransmissionData GetTransmissionDataByUUID(string uuid)
        {
            TransmissionData _TransmissionData = null;

            try
            {
                _TransmissionData = (from r in ctx.TransmissionDatas
                                     where r.Uuid == uuid
                                     select r).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetTransmissionData", "Error", ex.ToString());
            }
            return _TransmissionData;
        }

        public Library.Apas.CoreMain.DocumentTypeCodeType? GetDocumentType(string tranUUID)
        {
            Library.Apas.CoreMain.DocumentTypeCodeType? _DocumentType = null;
            try
            {

                _DocumentType = (from r in ctx.TransmissionDatas
                                where r.Uuid == tranUUID
                                select r.DocumentTypeCode).FirstOrDefault();

                // If can't find Transmission Data, try at the KeyValueTemp table
                if (_DocumentType == null)
                {
                    string uuid = GetKeyValueTempCache(tranUUID).Select(a => a.Value).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(uuid))
                    {
                        _DocumentType = (from r in ctx.TransmissionDatas
                                         where r.Uuid == uuid
                                         select r.DocumentTypeCode).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetDocumentType", "Error", ex.ToString());
            }
            return _DocumentType;
        }

        public string GetTransmissionDataXmlString(List<string> transmissionDataUUID, out string rootAttributes)
        {
            List<string> xmlStringList = new List<string>();
            string xmlString = string.Empty;
            rootAttributes = string.Empty;

            try
            {
                foreach (string uuid in transmissionDataUUID)
                {
                    xmlString = (from x in ctx.TransmissionDatas
                                 where x.Uuid == uuid
                                 select x.Xml).FirstOrDefault();

                    if (!string.IsNullOrWhiteSpace(xmlString))
                    {
                        xmlString = xmlString.Replace("{", "").Replace("}", "").Replace("Xml = ", "").Trim();

                        xmlStringList.Add(xmlString);

                        if (string.IsNullOrWhiteSpace(rootAttributes))
                        {
                            var attributes = (from a in XElement.Parse(xmlString).Elements()
                                                   select a.Parent.Attributes()).FirstOrDefault();
                            if (attributes.Any())
                            {
                                rootAttributes = string.Join(" ", attributes);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetTransmissionDataXmlString", "Error", ex.ToString());
            }
            return string.Join("", xmlStringList.ToArray());
        }

         public string GetErrorMessageXmlString(string requestTrackingID)
        {
            string xmlString = string.Empty;
            string tempString = string.Empty;

            try
            {
                var result = (from x in ctx.ReceivedErrors
                              where x.RequestTrackingID == requestTrackingID
                              select x).OrderByDescending(x => x.CreatedDateTime);
                if (result.Any()) {
                    xmlString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
                    xmlString += "<ErrorMessages>";

                    foreach (var errorMessage in result)
                    {
                        if (!string.IsNullOrWhiteSpace(errorMessage.ErrorMessage))
                        {
                            tempString = errorMessage.ErrorMessage.Replace("{", "").Replace("}", "").Replace("Xml = ", "").Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "").Trim();
                            xmlString += tempString.Insert(tempString.LastIndexOf("</ErrMsg:ErrorMessage>"), "<CreatedDateTime>"+errorMessage.CreatedDateTime.ToString("s")+"</CreatedDateTime>");
                        }
                    }
                    xmlString += "</ErrorMessages>";
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetErrorMessageXmlString", "Error", ex.ToString());
            }
            return xmlString;
        }

        public List<LoginHistoryReportSearchResultsFilter> GetLoginHistoryReportByIds(string[] Id, bool allSelected = false, string filterFields = null)
        {
            List<LoginHistoryReportSearchResultsFilter> _LoginHistory = new List<LoginHistoryReportSearchResultsFilter>();

            try
            {
                if (Id != null && Id.Any())
                {
                    // Deserialize filter fields
                    LoginHistoryReportViewObj _LoginHistoryReportViewObj = new LoginHistoryReportViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _LoginHistoryReportViewObj.LoginHistoryReportSearchFilter = JsonConvert.DeserializeObject<LoginHistoryReportSearchFilter>(filterFields);
                    };

                    var query = LoginHistoryQuery(_LoginHistoryReportViewObj);

                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => Id.Contains(a.HistoryID.ToUpper().Trim())).ToList();
                    }

                    _LoginHistory = query.OrderByDescending(x => x.CreatedDate).ToList();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetLoginHistoryReportByIds", "Error", ex.ToString());
            }
            return _LoginHistory;
        }

        public List<ExceptionErrorsReportSearchResultsFilter> GetExceptionErrorReportByIds(string[] Id, bool allSelected = false, string filterFields = null)
        {
            List<ExceptionErrorsReportSearchResultsFilter> _ExceptionErrors = new List<ExceptionErrorsReportSearchResultsFilter>();

            try
            {
                if (Id != null && Id.Any())
                {
                    // Deserialize filter fields
                    ExceptionErrorsReportViewObj _ExceptionErrorsReportViewObj = new ExceptionErrorsReportViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _ExceptionErrorsReportViewObj.ExceptionErrorsReportSearchFilter = JsonConvert.DeserializeObject<ExceptionErrorsReportSearchFilter>(filterFields);
                    };

                    var query = ExceptionErrorsQuery(_ExceptionErrorsReportViewObj);

                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => Id.Contains(a.StatusTrackingID.ToUpper().Trim())).ToList();
                    }

                    _ExceptionErrors = query.OrderByDescending(x => x.CreatedDateTime).ToList();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetExceptionErrorReportByIds", "Error", ex.ToString());
            }
            return _ExceptionErrors;
        }

        public List<BulkSendTranscript> GetBulkSnumberList()
        {
            List<BulkSendTranscript> _Snumbers = null;

            try
            {
                _Snumbers = (from r in ctx.BulkSendTranscripts
                             where r.SentDateTime == null
                             select r).OrderByDescending(x => x.CreatedDateTime).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetBulkSnumberList", "Error", ex.ToString());
            }
            return _Snumbers;
        }

        public BulkSendTranscript GetBulkSnumber(string sNumber)
        {
            BulkSendTranscript _Snumber = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(sNumber))
                {
                    _Snumber = (from r in ctx.BulkSendTranscripts
                             where r.SentDateTime == null && r.sNumber.Trim().ToLower() == sNumber.Trim().ToLower()
                             select r).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetBulkSnumber", "Error", ex.ToString());
            }
            return _Snumber;
        }

        public List<int> GetStudentIdMissingSnumber(int maxSizeList = 100)
        {
            List<int> _StudentIdList = null;

            try
            {
                _StudentIdList = (from a in ctx.ASNs
                            where !a.Student.sNumbers.Any()
                            select a).OrderByDescending(x => x.CreatedDateTime).Select(s => s.Student_StudentId).Take(maxSizeList).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetStudentMissingSnumberList", "Error", ex.ToString());
            }
            return _StudentIdList;
        }

        public List<ApplicationReportSearchResultsFilter> GetApplicationReportByIds(string[] applicationIds, bool allSelected = false, string filterFields = null)
        {
            List<ApplicationReportSearchResultsFilter> _Applications = new List<ApplicationReportSearchResultsFilter>();

            try
            {
                if (applicationIds != null && applicationIds.Any())
                {
                    // Deserialize filter fields
                    ApplicationReportViewObj _ApplicationReportViewObj = new ApplicationReportViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _ApplicationReportViewObj.ApplicationReportSearchFilter = JsonConvert.DeserializeObject<ApplicationReportSearchFilter>(filterFields);
                    };

                    var query = ApplicationReportsQuery(_ApplicationReportViewObj);

                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => applicationIds.Contains(a.ApplicationID.ToUpper().Trim())).ToList();
                    }

                    _Applications = query.OrderByDescending(x => x.ReceivedDateTime).ThenBy(b => b.FullName).ToList();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetApplicationReportByIds", "Error", ex.ToString());
            }
            return _Applications;
        }

        public List<PaymentReportSearchResultsFilter> GetPaymentReportByIds(string[] Id, bool allSelected = false, string filterFields = null)
        {
            List<PaymentReportSearchResultsFilter> _PaymentList = new List<PaymentReportSearchResultsFilter>();

            try
            {
                if (Id != null && Id.Any())
                {
                    // Deserialize filter fields
                    PaymentReportViewObj _PaymentReportViewObj = new PaymentReportViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _PaymentReportViewObj.PaymentReportSearchFilter = JsonConvert.DeserializeObject<PaymentReportSearchFilter>(filterFields);
                    };

                    var query = PaymentQuery(_PaymentReportViewObj);

                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => Id.Contains(a.PaypalResponseId.ToUpper().Trim())).ToList();
                    }

                    _PaymentList = query.OrderByDescending(x => x.TermCode).ToList();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetPaymentReportByIds", "Error", ex.ToString());
            }
            return _PaymentList;
        }

        public List<HoldTypeReportSearchResultsFilter> GetHoldTypeReportByIds(string[] Id, string destAction = null, string filterFields = null)
        {
            List<HoldTypeReportSearchResultsFilter> _HoldTypes = new List<HoldTypeReportSearchResultsFilter>();
            DateTime recentRequests;

            try
            {
                if (Id != null && Id.Count() > 0)
                {
                    try
                    {
                        recentRequests = DateTime.Now.AddDays(-1 * GetSetting(Structs.SettingTypes.Integer, Structs.Settings.RequestMaxLoad));
                    }
                    catch (Exception ex)
                    {
                        SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "LoadAdmissionsOutbox", "Error", ex.ToString());
                        recentRequests = DateTime.Now.AddDays(-10);  // Load last 10 days responses by default
                    };

                    // Deserialize filter fields
                    HoldTypeReportViewObj _HoldTypeReportViewObj = new HoldTypeReportViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _HoldTypeReportViewObj.SearchFilter = JsonConvert.DeserializeObject<RequestSearchObj>(filterFields);
                    };


                    var results = (from RE in ctx.Requests
                                 from TH in ctx.TranscriptHolds.Where(q => RE.Recipients.Any(rc => rc.RecipientId == q.Recipient.RecipientId)
                                                         && q.ModifiedDateTime == (from TH2 in ctx.TranscriptHolds where (q.Recipient.RecipientId == TH2.Recipient.RecipientId) select TH2).Max(m => m.ModifiedDateTime))
                                                         .DefaultIfEmpty()
                                 where Id.Contains(RE.TransmissionData.Uuid)
                                 select new HoldTypeReportSearchResultsFilter()
                                {
                                     FullName = RE.RequestedStudent.Person.Names.Where(n => n.NameType == Structs.Name.PersonalNameType).OrderByDescending(o => o.CreatedDateTime).Select(s => s.LastName + ", " + s.FirstName).FirstOrDefault(),
                                     SNumber = RE.RequestedStudent.sNumbers.OrderByDescending(o => o.CreatedDateTime).Select(s => s.sNumVal).FirstOrDefault(),
                                     ASN = RE.RequestedStudent.ASNs.OrderByDescending(o => o.CreatedDateTime).Select(s => s.AgencyAssignedID).FirstOrDefault(),
                                     FromRequestingInstitution = (destAction == Structs.DestActions.AdmissionsOutbox ?
                                            RE.TransmissionData.DestinationInstitution.InstitutionNames.OrderByDescending(o => o.CreatedDateTime).Select(s => s.Name).FirstOrDefault() :
                                            RE.TransmissionData.SourceInstitution.InstitutionNames.OrderByDescending(o => o.CreatedDateTime).Select(s => s.Name).FirstOrDefault()),
                                     ColleagueStatus = RE.TransmissionData.ExportedDateTime == null ? Enums.TransListTypes.New.ToString() : Enums.TransListTypes.Exported.ToString(),
                                     Operator = ctx.Users.Where(u => u.sNumber == RE.ModifiedBy).OrderByDescending(o => o.CreatedDateTime).Select(t => t.FirstName).FirstOrDefault(),
                                     //ResponseStatus = ctx.Responses.Where(r => RE.Recipients.Any(z => z.RequestTrackingID == r.RequestTrackingID) && r.SentDateTime != null).OrderByDescending(o => o.CreatedDateTime).Select(e => e.ResponseStatusType.ToString()).FirstOrDefault(),
                                     //ResponseCount = ctx.Responses.Where(r => RE.Recipients.Any(z => z.RequestTrackingID == r.RequestTrackingID) && r.SentDateTime != null).Count(),
                                     //TranscriptCount = ctx.Responses.Count(r => RE.Recipients.Any(z => z.RequestTrackingID == r.RequestTrackingID) && (r.ResponseStatusType == null || r.ResponseStatusType == Library.Apas.AcademicRecord.ResponseStatusType.TranscriptSent) && r.SentDateTime != null),
                                     //ErrorMessageCount = ctx.ReceivedErrors.Count(r => r.RequestTrackingID == RE.TransmissionData.RequestTrackingID),

                                     ResponseStatus = ctx.Responses.Where(r => RE.Recipients.Any(z => z.RequestTrackingID == r.RequestTrackingID) && (destAction == Structs.DestActions.AdmissionsOutbox ? r.ReceivedDateTime != null : r.SentDateTime != null)).OrderByDescending(o => o.CreatedDateTime).Select(e => e.ResponseStatusType.ToString()).FirstOrDefault(),
                                     ResponseCount = ctx.Responses.Where(r => RE.Recipients.Any(z => z.RequestTrackingID == r.RequestTrackingID) && (destAction == Structs.DestActions.AdmissionsOutbox ? r.ReceivedDateTime != null : r.SentDateTime != null)).Count(),
                                     TranscriptCount = ctx.Responses.Count(r => RE.Recipients.Any(z => z.RequestTrackingID == r.RequestTrackingID) && (r.ResponseStatusType == null || r.ResponseStatusType == Library.Apas.AcademicRecord.ResponseStatusType.TranscriptSent) && (destAction == Structs.DestActions.AdmissionsOutbox ? r.ReceivedDateTime != null : r.SentDateTime != null)),
                                     ErrorMessageCount = ctx.ReceivedErrors.Count(r => r.RequestTrackingID == RE.TransmissionData.RequestTrackingID),
                                     SentTRRQ = RE.SentToColleagueTRRQ,
                                     ViewedDateTime = RE.ViewedDateTime,
                                     FromStatus = RE.CreatedDateTime,
                                     //ToStatus = RE.CreatedDateTime,
                                     HoldType = TH.HoldType.ToString(),
                                     HoldTypeData = TH.SessionDesignator,
                                     SentDateTime = RE.SentDateTime,
                                     ReceivedDateTime = RE.ReceivedDateTime,
                                     CreatedDateTime = RE.TransmissionData.CreatedDateTime,
                                     DestAction = destAction,
                                 }).OrderByDescending(e => e.FromStatus).AsQueryable();

                    // Bring only the recent requests to speed up the query, including not viewed requests, if there are no search filter fields
                    if (!_HoldTypeReportViewObj.SearchFilter.HasValues && !_HoldTypeReportViewObj.SearchFilter.SearchHasValues)
                    {
                        // Default load, no search field, show older requestes first/top
                        switch (destAction)
                        {
                            case Structs.DestActions.AdmissionsOutbox:
                                results = results.OrderBy(r => r.SentDateTime < recentRequests ? r.SentDateTime : DateTime.Now)
                                                 .ThenBy(o => o.ViewedDateTime == null ? 0 : 1)
                                                 .ThenByDescending(x => x.CreatedDateTime);
                                break;
                            case Structs.DestActions.RecordsInbox:
                                results = results.OrderBy(r => r.ReceivedDateTime < recentRequests ? r.ReceivedDateTime : DateTime.Now)
                                                 .ThenBy(o => o.ViewedDateTime == null ? 0 : 1)
                                                 .ThenByDescending(x => x.CreatedDateTime);
                                break;
                        }
                    }

                    _HoldTypes = results.ToList();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetHoldTypeReportByIds", "Error", ex.ToString());
            }
            return _HoldTypes;
        }

        public string GetCollQueryXmlString(string DocType)
        {
            string xmlString = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(DocType))
                {
                    var result = (from QU in ctx.ReportQueries where QU.ReportName == DocType select QU.QueryString).ToList();

                    if (result.Any())
                    {
                        xmlString = result.FirstOrDefault().Serialize();
                    }
                }

            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetQueryXmlString", "Error", ex.ToString());
            }
            return xmlString;
        }

        public List<SentEmailReportSearchResultsFilter> GetSentEmailReportByIds(string[] Id, bool allSelected = false, string filterFields = null)
        {
            List<SentEmailReportSearchResultsFilter> _SentEmails = new List<SentEmailReportSearchResultsFilter>();

            try
            {
                if (Id != null && Id.Any())
                {
                    // Deserialize filter fields
                    SentEmailReportViewObj _SentEmailReportViewObj = new SentEmailReportViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _SentEmailReportViewObj.SentEmailReportSearchFilter = JsonConvert.DeserializeObject<SentEmailReportSearchFilter>(filterFields);
                    };

                    var query = SentEmailQuery(_SentEmailReportViewObj);

                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => Id.Contains(a.SentEmailId.ToUpper().Trim())).ToList();
                    }

                    _SentEmails = query.OrderByDescending(x => x.CreatedDateTime).ToList();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetSentEmailReportByIds", "Error", ex.ToString());
            }
            return _SentEmails;
        }

        public string GetSentEmailReportXmlString(string item)
        {
            string xmlString = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    var result = (from EML in ctx.SentEmails
                                 
                                  select new SentEmailReportSearchResultsFilter()
                                  {
                                      SentEmailId = EML.SentEmailId.ToString(),
                                      EmailType = EML.EmailType,
                                      From = EML.From,
                                      To = EML.To,
                                      Subject = EML.Subject,
                                      Body = EML.Body,
                                      CreatedDateTime = EML.CreatedDateTime,
                                      ModifiedBy = EML.ModifiedBy,
                                      ModifiedDateTime = EML.ModifiedDateTime,

                                  });

                    if (result.Any())
                    {
                        xmlString = result.FirstOrDefault().Serialize();
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetSentEmailReportXmlString", "Error", ex.ToString());
            }
            return xmlString;
        }


        public List<Recipient> GetRecipients()
        {
            List<Recipient> _Recipients = new List<Recipient>();

            try
            {
                string localcode = GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode);
                _Recipients = (from r in ctx.Recipients
                               where r.Request.TransmissionData.DocumentTypeCode == Library.Apas.CoreMain.DocumentTypeCodeType.InstitutionRequest
                               where r.Request.TransmissionData.DestinationInstitution.LocalOrganizationID.Contains(localcode)
                               select r).OrderByDescending(x => x.Request.TransmissionData.CreatedDateTime).ToList();

            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetRecipients", "Error", ex.ToString());
            }
            return _Recipients;

        }

        public string GetInstitutionName(int institutionId)
        {
            string _InstitutionName = string.Empty;
            try
            {
                _InstitutionName = ctx.Institutions
                    .Where(x => x.InstitutionId == institutionId).OrderByDescending(z => z.CreatedDateTime).FirstOrDefault()
                    .InstitutionNames.OrderByDescending(d => d.CreatedDateTime).Select(y => y.Name).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetInstitutionName", "Error", ex.ToString());
            }
            return _InstitutionName;
        }

        public Address GetInstitutionAddress(int institutionId)
        {
            Address _InstitutionAddress = null;
            try
            {
                _InstitutionAddress = ctx.Addresses
                                    .Where(x => x.InstitutionId == institutionId)
                                    .OrderByDescending(y => y.CreatedDateTime).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetInstitutionAddress", "Error", ex.ToString());
            }
            return _InstitutionAddress;
        }

        public string GetLocalOrganizationID(int? institutionId)
        {
            string _LocalOrganizationID = string.Empty;
            try
            {
                if (institutionId != null)
                {
                    _LocalOrganizationID = (from i in ctx.Institutions
                                            where i.InstitutionId == institutionId
                                            select i.LocalOrganizationID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetLocalOrganizationID", "Error", ex.ToString());
            }
            return _LocalOrganizationID;
        }

        public string GetLocalInstName(string localOrganizationID)
        {
            string _InstitutionName = string.Empty;
            try
            {
                _InstitutionName = ctx.Institutions
                    .Where(x => x.ESIS == localOrganizationID).FirstOrDefault()
                    .InstitutionNames.OrderByDescending(d => d.CreatedDateTime).Select(y => y.Name).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetLocalInstName", "Error", ex.ToString());
            }
            return _InstitutionName;
        }

        public Institution GetPreferredDestination()
        {
            Institution _PreferredInstitution = null;
            try
            {
                // Get preferred destination institution
                _PreferredInstitution = (from i in ctx.Institutions
                                       where i.PreferredInstitution == true
                                       //where i.LocalOrganizationID == "48027000"  // Lethbridge College
                                       //where i.LocalOrganizationID == "48009000"  // University of Lethbridge
                                       select i).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetPreferredDestinationId", "Error", ex.ToString());
            }

            return _PreferredInstitution;
        }

        public List<SelectListItem> GetDestinations(string defaultInstitutionId = null)
        {
            List<SelectListItem> _Destinations;
            try
            {
                var list = (from i in ctx.Institutions
                            where i.InstitutionNames.Any()
                            && i.ApasInstitution == true
                            select new SelectListItem
                            {
                                Text = i.InstitutionNames.OrderByDescending(x => x.CreatedDateTime).Select(y => y.Name).FirstOrDefault() + " (" + i.LocalOrganizationID + ")",
                                Value = i.InstitutionId.ToString(),
                            }).OrderBy(x => x.Text).ToList();



                if (!string.IsNullOrWhiteSpace(defaultInstitutionId))
                {
                    list.Find(x => x.Value.Contains(defaultInstitutionId)).Selected = true;
                }

                _Destinations = list;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetDestinations", "Error", ex.ToString());
                return null;
            }

            return _Destinations;
        }

        public Institution GetInstitution(string institutionId)
        {
            Institution _Institutions = new Institution();

            try
            {
                int? receiverId = Functions.ToInt32(institutionId);

                if (receiverId != null) {
                    _Institutions = (from i in ctx.Institutions
                                     where i.InstitutionId == receiverId
                                     select i).FirstOrDefault();
                }            
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetDestinations", "Error", ex.ToString());
            }

            return _Institutions;
        }

        public Institution GetInstByLOrgID(string localOrgID)
        {
            Institution _Institution = new Institution();

            try
            {
                _Institution = (from i in ctx.Institutions
                                where i.LocalOrganizationID == localOrgID
                                select i).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetInstByLOrgID", "Error", ex.ToString());
            }

            return _Institution;
        }

        public List<string> GetTranscriptRestrictionCodes(bool? OnlyRules = null)
        {
            List<TranscriptRestriction> _RestrictionCodeList = new List<TranscriptRestriction>();

            try
            {
                _RestrictionCodeList = (from r in ctx.TranscriptRestrictions
                                        where r.Active == true
                                        select r).ToList();

                if (OnlyRules != null)
                {
                    _RestrictionCodeList = _RestrictionCodeList.Where(x => x.IsRule == OnlyRules).ToList();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetTranscriptRestrictionCodes", "Error", ex.ToString());
            }

            return _RestrictionCodeList.Select(x => x.TranscriptRestrictionCode).ToList();
        }

        public bool SaveTranscriptResponseStatus(string uuid, Library.Apas.AcademicRecord.ResponseStatusType responseStatus)
        {
            bool success = false;
            Response _Response = new Response();

            try
            {
                _Response = (from r in ctx.Responses
                            where r.TransmissionData.Uuid == uuid
                            select r).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();

                _Response.ResponseStatusType = responseStatus;
                _Response.SentDateTime = DateTime.Now;

                ctx.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveTranscriptResponseStatus", "Error", ex.ToString());
            }
            return success;
        }

        public List<SelectListItem> GetHoldReasonTypes()
        {
            try
            {
                return (Enum.GetNames(typeof(Library.Apas.AcademicRecord.HoldReasonType)).Select((value, key) => new SelectListItem { Value = key.ToString(), Text = value })).OrderBy(x => x.Text).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetHoldReasonTypes", "Error", ex.ToString());
                return null;
            }
        }

        public List<SelectListItem> GetHoldTypes()
        {
            try
            {
                return (Enum.GetNames(typeof(Library.Apas.AcademicRecord.HoldTypeType)).Select((value, key) => new SelectListItem { Value = key.ToString(), Text = value })).OrderBy(x => x.Text).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetHoldReasonTypes", "Error", ex.ToString());
                return null;
            }
        }

        public List<SelectListItem> GetStatuses()
        {
            try
            {
                return (Enum.GetNames(typeof(Library.Apas.AcademicRecord.ResponseStatusType)).Select((value, key) => new SelectListItem { Value = key.ToString(), Text = value, })).OrderBy(x => x.Text).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetStatuses", "Error", ex.ToString());
                return null;
            }
        }

        public List<SelectListItem> GetTransListTypes()
        {
            try
            {
                return (Enum.GetNames(typeof(Enums.TransListTypes)).Select((value, key) => new SelectListItem { Value = key.ToString(), Text = value, })).OrderBy(x => x.Text).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetTransListTypes", "Error", ex.ToString());
                return null;
            }
        }

        public List<SelectListItem> GetRespTransTypes()
        {
            try
            {
                return (Enum.GetNames(typeof(Enums.RespTransTypes)).Select((value, key) => new SelectListItem { Value = key.ToString(), Text = value, })).OrderBy(x => x.Text).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetRespTransTypes", "Error", ex.ToString());
                return null;
            }
        }

        public IEnumerable<SelectListItem> PageSizes
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Text = "10", Value = "10", Selected = true },
                    new SelectListItem { Text = "25", Value = "25"},
                    new SelectListItem { Text = "50", Value = "50"},
                    new SelectListItem { Text = "75", Value = "75"},
                    new SelectListItem { Text = "100", Value = "100"},
                    new SelectListItem { Text = "200", Value = "200"}
                };
            }
        }

        //public string GetResponseType(string xmlString)
        //{
        //    string type = Structs.DocumentType.NotSet;

        //    var rootAttributes = (from a in XElement.Parse(xmlString).Elements()
        //                          select a.Parent.Attributes()).FirstOrDefault();

        //    foreach (XAttribute attr in rootAttributes)
        //    {
        //        _xlmns = attr.Value;
        //        if (attr.Value.Contains(" ")) { _xlmns = attr.Value.Substring(0, attr.Value.IndexOf(" ")); }

        //        switch (_xlmns)
        //        {
        //            case Structs.SchemaVersion.CollegeTranscript_v1_0_0a:
        //            case Structs.SchemaVersion.CollegeTranscript_v1_6_0:
        //                type = Structs.DocumentType.CollegeTranscript;
        //                break;
        //            case Structs.SchemaVersion.TranscriptResponse_v1_0_0:
        //            case Structs.SchemaVersion.TranscriptResponse_v1_4_0:
        //                type = Structs.DocumentType.Response;
        //                break;
        //            case Structs.SchemaVersion.HighSchoolTranscript_v1_0_0a:
        //            case Structs.SchemaVersion.HighSchoolTranscript_v1_5_0:
        //                type = Structs.DocumentType.HighSchoolTranscript;
        //                break;
        //            // Case new PASI Schema
        //        }
        //        if (type != Structs.DocumentType.NotSet) { break; }
        //    }
        //    return type;
        //}

        /// <summary>
        /// Scans Xml Document to extract schema version and return document type
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public string GetDocumentVersion(string xmlString)
        {
            string type = Structs.DocumentType.NotSet;

            // TODO: Needs to be a more surgical, precise lookup - not looping!
            var rootAttributes = (from a in XElement.Parse(xmlString).Elements()
                                  select a.Parent.Attributes()).FirstOrDefault();

            foreach (XAttribute attr in rootAttributes)
            {
                _xlmns = attr.Value;
                if (attr.Value.Contains(" ")) { _xlmns = attr.Value.Substring(0, attr.Value.IndexOf(" ")); }

                switch (_xlmns)
                {
                    case Structs.SchemaVersion.CollegeTranscript_v1_0_0a:
                        //break;
                    case Structs.SchemaVersion.CollegeTranscript_v1_6_0:
                        type = Structs.XmlDocType.CollegeTranscript;
                        break;
                    case Structs.SchemaVersion.TranscriptResponse_v1_0_0:
                        break;
                    case Structs.SchemaVersion.TranscriptResponse_v1_4_0:
                        type = Structs.XmlDocType.Response;
                        break;
                    case Structs.SchemaVersion.HighSchoolTranscript_v1_0_0a:
                        break;
                    case Structs.SchemaVersion.HighSchoolTranscript_v1_5_0:
                        type = Structs.XmlDocType.HighSchoolTranscript;
                        break;
                    case Structs.SchemaVersion.TranscriptRequest_v1_0_0:
                        break;
                    case Structs.SchemaVersion.TranscriptRequest_v1_4_0:
                        type = Structs.XmlDocType.Request;
                        break;
                        // Case new PASI Schema
                }
                if (type != Structs.DocumentType.NotSet) { break; }
            }
            return type;
        }

        public void ImportReceivedRequests()
        {
            string xmlns = string.Empty;

            var requests = (from m in ctx.ApasMessages
                            where m.MessageType == Enums.MessageTypes.ReceivedRequest
                            select m.MessageXML).ToArray();

            foreach (string request in requests.ToArray())
            {
                Library.Apas.TranscriptRequest.TranscriptRequest receivedRequest = DeserializeTranscriptRequest(request);
            }
        }

        public string CreateColleagueTranscript(CreateMsgViewObj createMsgObj)
        {
            string _NewMessageTransmissionDataUUID = string.Empty;
            string _ColleagueXmlStr = string.Empty;
            try
            {
                _ColleagueXmlStr = GetTransactionTranscriptResult(createMsgObj.TransactionTranscriptUuid);

                if (!string.IsNullOrWhiteSpace(_ColleagueXmlStr))
                {
                    List<Library.Apas.AcademicRecord.AcademicRecordType> _ApasAcademicRecords = ParseColleagueXmlString(_ColleagueXmlStr);

                    if (createMsgObj.MyCredsMessage)
                    {
                        // Parse Colleague Transcript and build it for MyCreds
                        createMsgObj.StudentRecord = ParseColleagueStudentXmlString(_ColleagueXmlStr);
                        _NewMessageTransmissionDataUUID = BuildMyCredsXmlTranscripts(createMsgObj, _ApasAcademicRecords);
                    }
                    else
                    {
                        // Build Transcript for APAS
                        _NewMessageTransmissionDataUUID = BuildXmlTranscripts(createMsgObj, _ApasAcademicRecords);
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "CreateColleagueTranscript", "Error: ", ex.ToString());
            }
            return _NewMessageTransmissionDataUUID;
        }

        public List<Library.Apas.AcademicRecord.AcademicRecordType> ParseColleagueXmlString(string xmlString) {
            
            List<Library.Apas.AcademicRecord.AcademicRecordType> _AcademicRecords = new List<Library.Apas.AcademicRecord.AcademicRecordType>();
            //bool transferCredit = false;
            
            try
            {
                xmlString = CleanXml(xmlString);

                XmlDocument xml = new XmlDocument();

                xml.LoadXml(xmlString);

                Library.Apas.AcademicRecord.AcademicRecordType _AcademicRecord = new Library.Apas.AcademicRecord.AcademicRecordType()
                {
                    
                    StudentLevel = new Library.Apas.CoreMain.StudentLevelType()
                    {
                        StudentLevelCode = Library.Apas.CoreMain.StudentLevelCodeType.Postsecondary,
                    },
                };

                _AcademicRecord.AcademicSession = new List<Library.Apas.AcademicRecord.AcademicSessionType>();

                foreach (XmlNode node in xml.FirstChild.LastChild.LastChild.ChildNodes) {

                    switch (node.Name)
                    {
                        case "AcademicSession":
                            Library.Apas.AcademicRecord.AcademicSessionType _AcademicSession = new Library.Apas.AcademicRecord.AcademicSessionType()
                            {
                                Course = new List<Library.Apas.AcademicRecord.CourseType>(),
                            };

                            //transferCredit = false;  // Reset for next Academic Session

                            foreach (XmlNode AcadSessNode in node.ChildNodes)
                            {
                                switch (AcadSessNode.Name)
                                {
                                    case "AcademicSessionDetail":
                                        Library.Apas.CoreMain.AcademicSessionDetailType _SessionDetailType = new Library.Apas.CoreMain.AcademicSessionDetailType();
                                        foreach (XmlNode sessionDetail in AcadSessNode.ChildNodes)
                                        {
                                            if (sessionDetail.Name == "SessionName")
                                            {
                                                _SessionDetailType.SessionDesignator = Functions.GetLcSessionDesignator(sessionDetail.InnerText);
                                                _SessionDetailType.SessionName = Functions.GetLcTermFullDescription(sessionDetail.InnerText);
                                                if (!string.IsNullOrWhiteSpace(_SessionDetailType.SessionDesignator) && !string.IsNullOrWhiteSpace(_SessionDetailType.SessionName))
                                                {
                                                    if (sessionDetail.InnerText.ToLower().Contains("s"))
                                                    {
                                                        _SessionDetailType.SessionType = Library.Apas.CoreMain.SessionTypeType.SummerSession;
                                                        _SessionDetailType.SessionTypeSpecified = true;
                                                    }
                                                    else
                                                    {
                                                        _SessionDetailType.SessionType = Library.Apas.CoreMain.SessionTypeType.Semester;
                                                        _SessionDetailType.SessionTypeSpecified = true;
                                                    }
                                                }
                                                if (_SessionDetailType.SessionDesignator == null)
                                                {
                                                    foreach (XmlNode acadSessionItem in node.ParentNode.ChildNodes)
                                                    {
                                                        if (acadSessionItem.Name == "AcademicSession")
                                                        {
                                                            foreach (XmlNode acadSessionDetailItem in acadSessionItem.ChildNodes)
                                                            {
                                                                if (acadSessionDetailItem.Name == "AcademicSessionDetail")
                                                                {
                                                                    foreach (XmlNode sessionNameItem in acadSessionDetailItem.ChildNodes)
                                                                    {
                                                                        if (sessionNameItem.Name == "SessionName" && sessionNameItem.InnerText.Trim().ToUpper() != "Non Term".ToUpper() && _SessionDetailType.SessionDesignator == null)
                                                                        {
                                                                            _SessionDetailType.SessionDesignator = Functions.GetLcSessionDesignator(sessionNameItem.InnerText);
                                                                            _SessionDetailType.SessionName = Functions.GetLcTermFullDescription(sessionNameItem.InnerText);
                                                                        }
                                                                        if (_SessionDetailType.SessionDesignator != null) { break; }
                                                                    }
                                                                }
                                                                if (_SessionDetailType.SessionDesignator != null) { break; }
                                                            }
                                                        }
                                                        if (_SessionDetailType.SessionDesignator != null) { break; }
                                                    }
                                                }
                                            }
                                        }
                                        _AcademicSession.AcademicSessionDetail = _SessionDetailType;
                                        break;
                                    case "NoteMessage":
                                        _AcademicSession.NoteMessage = new List<string>();
                                        foreach (XmlNode n in AcadSessNode.ChildNodes)
                                        {
                                            string txt = Regex.Replace(n.InnerText, @"\s+", " ").Trim();
                                            int maxLength = 80;

                                            // Create multiple lines if content exceeds max length of 80 characters
                                            while (txt.Length > maxLength)
                                            {
                                                _AcademicSession.NoteMessage.Add(txt.TruncateString(maxLength));
                                                txt = txt.Substring(maxLength, txt.Length - maxLength);
                                            }

                                            _AcademicSession.NoteMessage.Add(txt);
                                        }
                                        break;
                                    case "Course":
                                        Library.Apas.AcademicRecord.CourseType _NewCourse = new Library.Apas.AcademicRecord.CourseType();
                                        foreach (XmlNode n in AcadSessNode.ChildNodes)
                                        {
                                            if (!string.IsNullOrWhiteSpace(n.InnerText))
                                            {
                                                switch (n.Name)
                                                {
                                                    case "CourseSubjectAbbreviation":
                                                        _NewCourse.CourseSubjectAbbreviation = n.InnerText;
                                                        break;
                                                    case "CourseNumber":
                                                        _NewCourse.CourseNumber = n.InnerText;
                                                        break;
                                                    case "CourseTitle":
                                                        _NewCourse.CourseTitle = n.InnerText;
                                                        break;
                                                    case "CourseAcademicGrade":
                                                        _NewCourse.CourseAcademicGrade = n.InnerText;
                                                        if (_NewCourse.CourseAcademicGrade.Trim().ToUpper() == "TR")  // Transfer Credit
                                                        {
                                                            //transferCredit = true;
                                                            _NewCourse.CourseAcademicGradeStatusCode = Library.Apas.CoreMain.CourseAcademicGradeStatusCodeType.TransferNoGrade;
                                                            _NewCourse.CourseAcademicGradeStatusCodeSpecified = true;
                                                        }
                                                        break;
                                                    case "CourseCreditValue":
                                                        if (!string.IsNullOrWhiteSpace(n.InnerText))
                                                        {
                                                            _NewCourse.CourseCreditValueSpecified = true;
                                                            _NewCourse.CourseCreditValue = Convert.ToDecimal(n.InnerText);
                                                        }
                                                        break;
                                                    case "CourseCreditEarned":
                                                        if (!string.IsNullOrWhiteSpace(n.InnerText))
                                                        {
                                                            _NewCourse.CourseCreditEarned = Convert.ToDecimal(n.InnerText);
                                                            _NewCourse.CourseCreditEarnedSpecified = true;
                                                        }
                                                        break;
                                                    //case "CourseCreditforGPA":
                                                    //    _NewCourse. = Convert.ToDecimal(n.InnerText);
                                                    //    break;
                                                    case "CourseQualityPointsEarned":
                                                        if (!string.IsNullOrWhiteSpace(n.InnerText))
                                                        {
                                                            _NewCourse.CourseQualityPointsEarned = Convert.ToDecimal(n.InnerText);
                                                            _NewCourse.CourseQualityPointsEarnedSpecified = true;
                                                        }
                                                        break;
                                                    case "CourseBeginDate":
                                                        if (DateTime.TryParseExact(n.InnerText, "yy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime beginDate))
                                                        {
                                                            _NewCourse.CourseBeginDate = beginDate;
                                                            _NewCourse.CourseBeginDateSpecified = true;
                                                        }
                                                        break;
                                                    case "CourseEndDate":
                                                        if (DateTime.TryParseExact(n.InnerText, "yy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
                                                        {
                                                            _NewCourse.CourseEndDate = endDate;
                                                            _NewCourse.CourseEndDateSpecified = true;
                                                        }
                                                        break;
                                                    case "OtherNote":
                                                        if (!string.IsNullOrWhiteSpace(n.InnerText))
                                                        {
                                                            if (_NewCourse.NoteMessage == null) _NewCourse.NoteMessage = new List<string>();
                                                            _NewCourse.NoteMessage.Add(n.InnerText);  // Usually used for transfer credit: "Transferred from 'Institution name'"
                                                        }
                                                        break;
                                                }
                                            }
                                        }
                                        _AcademicSession.Course.Add(_NewCourse);
                                        break;
                                    case "AcademicSummary":
                                        _AcademicSession.AcademicSummary = new List<Library.Apas.AcademicRecord.AcademicSummaryFType>();
                                        Library.Apas.AcademicRecord.AcademicSummaryFType _Summary = new Library.Apas.AcademicRecord.AcademicSummaryFType();

                                        // If it has transfer credit
                                        //if (transferCredit)
                                        //{
                                        //    _Summary.AcademicSummaryType = Library.Apas.CoreMain.AcademicSummaryTypeType.TransferOnly;
                                        //    _Summary.AcademicSummaryTypeSpecified = true;
                                        //}

                                        foreach (XmlNode n in AcadSessNode.ChildNodes)
                                        {
                                            switch (n.Name)
                                            {
                                                case "GPA":
                                                    _Summary.GPA = new Library.Apas.CoreMain.GPAType();
                                                    foreach (XmlNode child in n.ChildNodes)
                                                    {
                                                        switch (child.Name)
                                                        {
                                                            case "CreditHoursAttempted":
                                                                if (!string.IsNullOrWhiteSpace(child.InnerText))
                                                                {
                                                                    _Summary.GPA.CreditHoursAttempted = Convert.ToDecimal(child.InnerText);
                                                                    _Summary.GPA.CreditHoursAttemptedSpecified = true;
                                                                }
                                                                break;
                                                            case "CreditHoursEarned":
                                                                if (!string.IsNullOrWhiteSpace(child.InnerText))
                                                                {
                                                                    _Summary.GPA.CreditHoursEarned = Convert.ToDecimal(child.InnerText);
                                                                    _Summary.GPA.CreditHoursEarnedSpecified = true;
                                                                }
                                                                break;
                                                            case "CreditHoursforGPA":
                                                                if (!string.IsNullOrWhiteSpace(child.InnerText))
                                                                {
                                                                    _Summary.GPA.CreditHoursforGPA = Convert.ToDecimal(child.InnerText);
                                                                    _Summary.GPA.CreditHoursforGPASpecified = true;
                                                                }
                                                                break;
                                                            case "TotalQualityPoints":
                                                                if (!string.IsNullOrWhiteSpace(child.InnerText))
                                                                {
                                                                    _Summary.GPA.TotalQualityPoints = Convert.ToDecimal(child.InnerText);
                                                                    _Summary.GPA.TotalQualityPointsSpecified = true;
                                                                }
                                                                break;
                                                            case "GradePointAverage":
                                                                if (!string.IsNullOrWhiteSpace(child.InnerText))
                                                                {
                                                                    _Summary.GPA.GradePointAverage = Convert.ToDecimal(child.InnerText);
                                                                    _Summary.GPA.GradePointAverageSpecified = true;
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case "TermStanding":
                                                    _Summary.AcademicHonors = new Library.Apas.CoreMain.AcademicHonorsType()
                                                    {
                                                        HonorsTitle = n.InnerText,
                                                    };
                                                    break;
                                            }
                                        }
                                        _AcademicSession.AcademicSummary.Add(_Summary);
                                        break;
                                    case "AcademicProgram":
                                        _AcademicSession.AcademicProgram = new List<Library.Apas.AcademicRecord.AcademicProgramType>();
                                        Library.Apas.AcademicRecord.AcademicProgramType _AcademicProgram = new Library.Apas.AcademicRecord.AcademicProgramType();
                                        foreach (XmlNode nd in AcadSessNode.ChildNodes)
                                        {
                                            switch (nd.Name)
                                            {
                                                case "AcademicProgramName":
                                                    if (string.IsNullOrWhiteSpace(_AcademicProgram.AcademicProgramName))
                                                    {
                                                        _AcademicProgram.AcademicProgramName = nd.InnerText;
                                                    }
                                                    break;

                                                case "ProgramSummary":
                                                    if (nd.FirstChild.Name == "GPA")
                                                    {
                                                        _AcademicProgram.AcademicProgramTypeType = Library.Apas.CoreMain.AcademicProgramTypeType.Major;
                                                        _AcademicProgram.ProgramSummary = new Library.Apas.AcademicRecord.AcademicSummaryE2Type
                                                        {
                                                            GPA = new Library.Apas.CoreMain.GPAType(),
                                                        };

                                                        foreach (XmlNode n in nd.FirstChild.ChildNodes)
                                                        {
                                                            switch (n.Name)
                                                            {
                                                                case "CreditHoursAttempted":
                                                                    if (!string.IsNullOrWhiteSpace(n.InnerText))
                                                                    {
                                                                        _AcademicProgram.ProgramSummary.GPA.CreditHoursAttempted = Convert.ToDecimal(n.InnerText);
                                                                        _AcademicProgram.ProgramSummary.GPA.CreditHoursAttemptedSpecified = true;
                                                                    }
                                                                    break;
                                                                case "CreditHoursEarned":
                                                                    if (!string.IsNullOrWhiteSpace(n.InnerText))
                                                                    {
                                                                        _AcademicProgram.ProgramSummary.GPA.CreditHoursEarned = Convert.ToDecimal(n.InnerText);
                                                                        _AcademicProgram.ProgramSummary.GPA.CreditHoursEarnedSpecified = true;
                                                                    }
                                                                    break;
                                                                case "CreditHoursforGPA":
                                                                    if (!string.IsNullOrWhiteSpace(n.InnerText))
                                                                    {
                                                                        _AcademicProgram.ProgramSummary.GPA.CreditHoursforGPA = Convert.ToDecimal(n.InnerText);
                                                                        _AcademicProgram.ProgramSummary.GPA.CreditHoursforGPASpecified = true;
                                                                    }
                                                                    break;
                                                                case "TotalQualityPoints":
                                                                    if (!string.IsNullOrWhiteSpace(n.InnerText))
                                                                    {
                                                                        _AcademicProgram.ProgramSummary.GPA.TotalQualityPoints = Convert.ToDecimal(n.InnerText);
                                                                        _AcademicProgram.ProgramSummary.GPA.TotalQualityPointsSpecified = true;
                                                                    }
                                                                    break;
                                                                case "GradePointAverage":
                                                                    if (!string.IsNullOrWhiteSpace(n.InnerText))
                                                                    {
                                                                        _AcademicProgram.ProgramSummary.GPA.GradePointAverage = Convert.ToDecimal(n.InnerText);
                                                                        _AcademicProgram.ProgramSummary.GPA.GradePointAverageSpecified = true;
                                                                    }
                                                                    break;
                                                            }
                                                        }
                                                        _AcademicSession.AcademicProgram.Add(_AcademicProgram);
                                                    }
                                                    break;
                                            }

                                        }
                                        break;
                                }
                            }
                            _AcademicRecord.AcademicSession.Add(_AcademicSession);
                            break;
                        case "AcademicAward":
                            if (_AcademicRecord.AcademicAward == null)
                            {
                                _AcademicRecord.AcademicAward = new List<Library.Apas.AcademicRecord.AcademicAwardType>();
                            }
                            Library.Apas.AcademicRecord.AcademicAwardType _AcademicAward = new Library.Apas.AcademicRecord.AcademicAwardType();

                            foreach (XmlNode AcadAwardNode in node.ChildNodes)
                            {
                                switch (AcadAwardNode.Name)
                                {
                                    case "Certificate":
                                        foreach (XmlNode n in AcadAwardNode.ChildNodes)
                                        {
                                            switch (n.Name)
                                            {
                                                case "CertificateTitle":
                                                    int pos = n.InnerText.IndexOf(" - ");
                                                    if (pos == -1)
                                                    {
                                                        _AcademicAward.AcademicAwardTitle = n.InnerText;
                                                    }
                                                    else
                                                    {
                                                        pos += 3;  // Remove character " - ", Size = 3
                                                        _AcademicAward.AcademicAwardTitle = n.InnerText.Substring(pos, n.InnerText.Length - pos);
                                                    }
                                                    break;
                                                case "CertificateDate":
                                                    if (DateTime.TryParseExact(n.InnerText, "MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime awardDate))
                                                    {
                                                        _AcademicAward.AcademicAwardDate = awardDate;
                                                        _AcademicAward.AcademicAwardDateSpecified = true;
                                                    }
                                                    break;
                                                case "CertificateProgramName":
                                                    if (!string.IsNullOrWhiteSpace(n.InnerText.Trim()))
                                                    {
                                                        _AcademicAward.AcademicAwardProgram = new List<Library.Apas.AcademicRecord.AcademicProgramType>()
                                                        {
                                                            new Library.Apas.AcademicRecord.AcademicProgramType()
                                                            {
                                                                AcademicProgramName = n.InnerText,
                                                                AcademicProgramTypeType = Library.Apas.CoreMain.AcademicProgramTypeType.Major,
                                                                AcademicProgramTypeSpecified = true,
                                                            }
                                                        };
                                                    }
                                                    break;
                                            }
                                        }
                                        break;
                                    case "AcademicHonors":
                                        _AcademicAward.AcademicHonors = new List<Library.Apas.CoreMain.AcademicHonorsType>();
                                        foreach (XmlNode n in AcadAwardNode.ChildNodes)
                                        {
                                            Library.Apas.CoreMain.AcademicHonorsType _AcademicHonor = new Library.Apas.CoreMain.AcademicHonorsType();
                                            int pos = n.InnerText.IndexOf(" - ");
                                            if (pos == -1)
                                            {
                                                _AcademicHonor.HonorsTitle = n.InnerText;
                                            } else
                                            {
                                                pos += 3;  // Remove character " - ", Size = 3
                                                _AcademicHonor.HonorsTitle = n.InnerText.Substring(pos, n.InnerText.Length - pos);
                                            }
                                            _AcademicAward.AcademicHonors.Add(_AcademicHonor);
                                        }
                                        break;
                                }
                            }
                            _AcademicRecord.AcademicAward.Add(_AcademicAward);

                            break;
                    }
                }

                if (_AcademicRecord.AcademicSession.Count() > 0)
                {
                    _AcademicRecords.Add(_AcademicRecord);
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseColleagueXmlString", "Error: ", "xmlString:" + xmlString + ", Exception: " + ex.ToString());
            }
            return _AcademicRecords;
        }

        public StudentRecordObj ParseColleagueStudentXmlString(string xmlString)
        {
            StudentRecordObj _StudentRecords = new StudentRecordObj();

            try
            {
                xmlString = CleanXml(xmlString);

                XmlDocument xml = new XmlDocument();

                xml.LoadXml(xmlString);

                foreach (XmlNode node in xml.FirstChild.LastChild.FirstChild.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Name":
                            foreach (XmlNode nameDetail in node.ChildNodes)
                            {
                                if (nameDetail.Name == "CompositeName")
                                {
                                    string[] names = Functions.SeparateNames(nameDetail.InnerText.Trim());
                                    _StudentRecords.FullName = nameDetail.InnerText.Trim();
                                    _StudentRecords.FirstName = names[0];
                                    _StudentRecords.MiddleName = names[1];
                                    _StudentRecords.LastName = names[2];
                                }
                            }
                            break;
                        case "SchoolAssignedPersonID":
                            foreach (XmlNode sNumber in node.ChildNodes)
                            {
                                _StudentRecords.Snumber = sNumber.InnerText.Trim();
                            }
                            break;
                        case "Birth":
                            foreach (XmlNode birthDetail in node.ChildNodes)
                            {
                                if (birthDetail.Name == "BirthDate" && !string.IsNullOrWhiteSpace(birthDetail.InnerText) &&
                                    DateTime.TryParseExact(birthDetail.InnerText, "yy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime beginDate))
                                {
                                    _StudentRecords.BirthDate = beginDate;
                                }
                            }
                            break;
                        case "Contacts":
                            foreach (XmlNode addressDetail in node.ChildNodes)
                            {
                                if (addressDetail.Name == "Address")
                                {
                                    foreach (XmlNode address in addressDetail.ChildNodes)
                                    {
                                        switch (address.Name)
                                        {
                                            case "AddressLine":
                                                _StudentRecords.Addr1 = address.InnerText.Trim();
                                                break;
                                            case "City":
                                                _StudentRecords.City = address.InnerText.Trim();
                                                break;
                                            case "StateProvinceCode":
                                                _StudentRecords.State = address.InnerText.Trim();
                                                break;
                                            case "PostalCode":
                                                _StudentRecords.Zip = address.InnerText.Trim();
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ParseColleagueStudentXmlString", "Error: ", "xmlString:" + xmlString + ", Exception: " + ex.ToString());
            }
            return _StudentRecords;
        }

        public static string CleanXml(string dirty)
        {

            StringBuilder clean = new StringBuilder();

            dirty = System.Net.WebUtility.HtmlDecode(dirty);

            foreach (char c in dirty.Replace("&", "&amp;").ToCharArray())
            {
                if (IsLegalXmlChar(c))
                    clean.Append(c);
            }

            return clean.ToString();
        }

        /// <summary>   
        /// Whether a given character is allowed by XML 1.0.   
        /// </summary>   
        public static bool IsLegalXmlChar(int character)
        {
            return
            (
                 character == 0x9 /* == '\t' == 9   */          ||
                 character == 0xA /* == '\n' == 10  */          ||
                 character == 0xD /* == '\r' == 13  */          ||
                (character >= 0x20 && character <= 0xD7FF) ||
                (character >= 0xE000 && character <= 0xFFFD) ||
                (character >= 0x10000 && character <= 0x10FFFF)
            );
        }

        public bool CheckTransactionTranscriptComplete(string transactionTranscriptUuid)
        {
            bool success = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(transactionTranscriptUuid)) {
                    success = ctx.TransactionTranscripts.Where(x => x.TransactionTranscriptUuid == transactionTranscriptUuid && !string.IsNullOrEmpty(x.TranscriptText)).Any();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetTransactionTranscriptResult", "Error: ", "transactionTranscriptUuid:" + transactionTranscriptUuid + ", " + ex.ToString());
            }
            return success;
        }

        public string GetTransactionTranscriptResult(string transactionTranscriptUuid)
        {
            string result = string.Empty;
            int maxAttemps = 20;  // Seconds
            int attempts = 0;

            try
            {
                if (transactionTranscriptUuid != null)
                {
                    while (string.IsNullOrWhiteSpace(result) && attempts <= maxAttemps)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        attempts++;

                        result = ctx.TransactionTranscripts.Where(x => x.TransactionTranscriptUuid == transactionTranscriptUuid && !string.IsNullOrEmpty(x.TranscriptText)).Select(y=>y.TranscriptText).FirstOrDefault();
                    }

                    //SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetTransactionTranscriptResult", "Attepms", "Attepms: " + attempts + ",  TransmissionId: " + transactionTranscriptUuid + ", DateTime: " + DateTime.Now + ", Result: " + result, false);
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetTransactionTranscriptResult", "Error: ", "TransmissionId:" + transactionTranscriptUuid + ", " + ex.ToString());
            }
            return result;
        }

        public bool SaveTransactionTranscriptText(string transactionTranscriptUuid, string xmlString) {
            bool success = false;

            try
            {
                TransactionTranscript tran = ctx.TransactionTranscripts.Where(x => x.TransactionTranscriptUuid == transactionTranscriptUuid).FirstOrDefault();

                tran.TranscriptText = xmlString;
                tran.CompletedDateTime = DateTime.Now;

                ctx.SaveChanges();

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveTransactionTranscriptText", "Error: ", ex.ToString());
            }

            return success;
        }

        public bool SaveTransactionRequestLogCompletedDate(string transactionRequestLogUuid, string studentRequestLogsId) {
            bool success = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(transactionRequestLogUuid))
                {
                    TransactionRequestLog requestLog = ctx.TransactionRequestLogs.Where(x => x.TransactionRequestLogUuid == transactionRequestLogUuid).OrderByDescending(y => y.CreatedDateTime).FirstOrDefault();

                    requestLog.CompletedDateTime = DateTime.Now;
                    if (!string.IsNullOrWhiteSpace(studentRequestLogsId))
                    {
                        requestLog.StudentRequestLogsId = studentRequestLogsId;
                    }

                    ctx.SaveChanges();

                    success = true;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveTransactionRequestLogCompletedDate", "Error: ", ex.ToString());
            }

            return success;
        }

        public bool SaveTransactionRequestLogRecipientId(string transactionRequestLogUuid, string recipientId)
        {
            bool success = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(transactionRequestLogUuid) && !string.IsNullOrWhiteSpace(recipientId))
                {
                    TransactionRequestLog requestLog = ctx.TransactionRequestLogs.Where(x => x.TransactionRequestLogUuid == transactionRequestLogUuid).OrderByDescending(y => y.CreatedDateTime).FirstOrDefault();

                    requestLog.RecipientId = recipientId;

                    ctx.SaveChanges();

                    success = true;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveTransactionRequestLogCompletedDate", "Error: ", ex.ToString());
            }

            return success;
        }

        public bool SaveTransactionCourse(CourseExportObj course)
        {
            bool success = false;
            DateTime nowDate = DateTime.Now;
            TransactionCourse _Course = new TransactionCourse();

            try
            {
                if (course != null && course.Course != null)
                {
                    _Course.AExtlPersonId = course.Course.AExtlPersonId;
                    _Course.AExtlInstitution = course.Course.AExtlInstitution;
                    _Course.AExtlCourse = course.Course.AExtlCourse;
                    if (course.Course != null)
                    {
                        if (!string.IsNullOrWhiteSpace(course.Course.AExtlStartDate)) { _Course.AExtlStartDate = course.Course.AExtlStartDate.ConvertColleagueDaysToDate().ToString(); }
                        if (!string.IsNullOrWhiteSpace(course.Course.AExtlEndDate)) { _Course.AExtlEndDate = course.Course.AExtlEndDate.ConvertColleagueDaysToDate().ToString(); }
                    }

                    var query = ctx.TransactionCourses.Where(x => x.AExtlPersonId.Trim().ToUpper() == _Course.AExtlPersonId.Trim().ToUpper() &&
                                                                  x.AExtlInstitution.Trim().ToUpper() == _Course.AExtlInstitution.Trim().ToUpper() &&
                                                                  x.AExtlCourse.Trim().ToUpper() == _Course.AExtlCourse.Trim().ToUpper() &&
                                                                  x.AExtlStartDate.Trim().ToUpper() == _Course.AExtlStartDate.Trim().ToUpper() &&
                                                                  x.AExtlEndDate.Trim().ToUpper() == _Course.AExtlEndDate.Trim().ToUpper());
                    if (query.Any())
                    {
                        _Course = query.OrderByDescending(y => y.CreatedDateTime).FirstOrDefault();
                    }
                    else
                    {
                        _Course.CreatedDateTime = nowDate;
                    }

                    //_Course.AExternalTranscriptsId = course.Course.AExternalTranscriptsId;
                    _Course.AExtlPersInstIdx = course.Course.AExtlPersInstIdx;
                    _Course.AExtlTitle = course.Course.AExtlTitle;
                    _Course.AExtlCred = course.Course.AExtlCred;
                    _Course.AExtlGrade = course.Course.AExtlGrade;
                    _Course.AExtlGradeScheme = course.Course.AExtlGradeScheme;
                    _Course.AExtlOpr = course.Course.AExtlOpr;
                    _Course.ModifiedDateTime = nowDate;

                    if (!query.Any()) { ctx.TransactionCourses.Add(_Course); }

                    ctx.SaveChanges();

                    success = true;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveTransactionCourse", "Error: ", ex.ToString());
            }

            return success;
        }

        public void ExportTranscriptsToColleague(List<string> transmissionDataIdList, ref UserResultObj userResultObj, bool matchedStudent = false)
        {
            string transDataUUID = string.Empty;
            try
            {
                //TODO: Check if any course of the transcript has been sent to Colleague, check if it is a transfer credit TR (Equivalence == YES),
                //TODO: if not TR then ask if user wants to overwrite/update courses in Colleague

                foreach (string transDataId in transmissionDataIdList)
                {
                    //TODO: Delete Old course and re-parse response, ParseResponse includes the course deletion
                    transDataUUID = ctx.TransmissionDatas.Where(x => x.TransmissionDataId.ToString() == transDataId).OrderByDescending(o => o.CreatedDateTime)
                                    .Select(s => s.Uuid).FirstOrDefault();
                    ParseResponseXml(transDataUUID, Enums.MessageTypes.ReceivedResponse);

                    // Group course to avoid inserting duplicate courses
                    List<string> courseIdList = ctx.Courses.Where(x => x.AcademicRecord.Responses.TransmissionData.TransmissionDataId.ToString() == transDataId)
                                                            .GroupBy(s => new { s.Title, s.CourseNumber, s.SubjectAbbreviation, s.CreditValue, s.CreditLevel, s.CreditBasis, s.BeginDate, s.EndDate, s.AcademicGrade })
                                                            .Select(s => s.FirstOrDefault())
                                                            .OrderByDescending(o => o.EndDate)
                                                            .Select(y => y.CourseId.ToString())
                                                            .ToList();

                    if (courseIdList.Any())
                    {
                        // Check if Student has no sNumber in Colleague
                        if (CheckResponseStudentSNumber(transDataId, matchedStudent))
                        {
                            // Send transcrip courses to Colleague External Transcript
                            using (ColleagueLogic collLogic = new ColleagueLogic())
                            {
                                foreach (string courseId in courseIdList)
                                {
                                    string errorMessage = null;
                                    userResultObj.Success = collLogic.ExportCourseToColleague(courseId, ref errorMessage, transDataId);
                                    // Stop processing if one course fails
                                    if (!userResultObj.Success) {
                                        userResultObj.Message = errorMessage;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Student has no sNumber in Colleague: Redirect to the Student Lookup page using JQuery
                            userResultObj.Message = Structs.ErrorMessages.NoSNumberFound;
                        }
                    }
                    else
                    {
                        userResultObj.Message = Structs.Export.EmptyCourse;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ExportTranscriptsToColleague", "Error: ", "transmissionDataIdList:" + transmissionDataIdList.ToString() + ", " + ex.ToString());
            }
        }

        public bool MarkAsNotViewed(string transDataUUID, string docType)
        {
            bool success = false;
            try
            {
                if (transDataUUID != null)
                {
                    switch (docType)
                    {
                        case Structs.DocumentType.Request:
                            Request _Request = (from r in ctx.Requests
                                                                where r.TransmissionData.Uuid == transDataUUID
                                                                select r).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();

                            if (_Request != null)
                            {
                                _Request.ViewedDateTime = null;
                                ctx.SaveChanges();
                            }

                            success = true;

                            break;
                        case Structs.DocumentType.Response:
                            Response _Response = (from r in ctx.Responses
                                                                  where r.TransmissionData.Uuid == transDataUUID
                                                                  select r).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();

                            if (_Response != null)
                            {
                                _Response.ViewedDateTime = null;
                                ctx.SaveChanges();
                            }

                            success = true;

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "MarkAsNotViewed", "Error: ", "transDataUUID:" + transDataUUID.ToString() + ", docType: "+ docType + ", " + ex.ToString());
            }

            return success;
        }

        public bool MarkRequestAsSentToTRRQ(string transDataUUID)
        {
            bool success = false;
            try
            {
                if (transDataUUID != null)
                {
                    Request _Request = (from r in ctx.Requests
                                        where r.TransmissionData.Uuid == transDataUUID
                                        select r).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();

                    if (_Request != null)
                    {
                        _Request.SentToColleagueTRRQ = DateTime.Now;
                        ctx.SaveChanges();
                    }

                    success = true;
                    }
                }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "MarkRequestAsSentToTRRQ", "Error: ", "transDataUUID:" + transDataUUID.ToString() + ", Error: " + ex.ToString());
            }

            return success;
        }

        public static MemoryStream TransformXmltoHtml(string inputXml, string xsltString)
        {
            var transform = new System.Xml.Xsl.XslCompiledTransform();
            using (var reader = XmlReader.Create(new StringReader(xsltString)))
            {
                transform.Load(reader);
            }
            var memoryStream = new MemoryStream();
            var results = new StreamWriter(memoryStream);
            using (var reader = XmlReader.Create(new StringReader(inputXml)))
            {
                transform.Transform(reader, null, results);
            }
            memoryStream.Position = 0;
            return memoryStream;
        }

        public RequestBodyObj PrepareMyCredsRequestObj(MyCredsTransactionObj myCredsTransaction, bool uploadXML = false)
        {
            RequestBodyObj requestBodyObj = null;
            try
            {
                requestBodyObj = new RequestBodyObj()
                {
                    user = new RequestBodyUserObj()
                    {
                        id = myCredsTransaction.sNumber,
                        full_name = myCredsTransaction.FullName,
                        email = myCredsTransaction.Email,
                        initial_login = new RequestBodyInitialLoginObj()
                        {
                            idp = Structs.MyCredsRequestSettings.Digitary,
                            type = Structs.MyCredsRequestSettings.Type,
                            value = myCredsTransaction.Email,
                        }
                    },
                    document = new RequestBodyDocumentObj()
                    {
                        type = Structs.MyCredsRequestSettings.Transcript,
                        format = uploadXML ? Structs.MyCredsRequestSettings.DocumentFormatXml : Structs.MyCredsRequestSettings.DocumentFormatPdf,
                        custom_fields = new List<RequestBodyCustomFieldsObj>(),
                    },
                    settings = new RequestBodySettingsObj()
                    {
                        overwrite_duplicates = true,
                        generate_pdf = !uploadXML,
                        display_name = Structs.MyCredsDocumentTypes.Transcript,
                        metadata_update = true,
                    },
                    auto_certification = new RequestBodyCertificationObj()
                    {
                        certification_key_id = Structs.MyCredsRequestSettings.CertificationKeyId,
                        suppress_email = false,
                        live_document = true,
                    },
                    access_charge = new RequestBodyAccessChargeObj()
                    {
                        amount = Structs.MyCredsCharges.Amount,
                        currency = Structs.MyCredsCharges.Currency,
                        charge_method = Structs.MyCredsCharges.Method.ToUpper(),
                    }
                };

                RequestBodyCustomFieldsObj requestBodyCustomFieldsObj = new RequestBodyCustomFieldsObj()
                {
                    name = Structs.MyCredsRequestSettings.BatchCode,
                    value = myCredsTransaction.BatchCode,
                };

                requestBodyObj.document.custom_fields.Add(requestBodyCustomFieldsObj);
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "PrepareMyCredsRequestObj", "Error: ", ex.ToString());
            }

            return requestBodyObj;
        }

        public bool ExportStudentTranscriptAPI(MyCredsTransactionObj myCredsTransaction, byte[] fileBytes = null, bool uploadXML = false)
        {
            bool success = false;
            string documentID = string.Empty;
            string requestJsonBody = string.Empty;
            RequestBodyObj requestBodyObj = null;

            try
            {
                // Create function to load a list of RequestBodyObj
                requestBodyObj = PrepareMyCredsRequestObj(myCredsTransaction, uploadXML);

                // Process web api call for each student
                requestJsonBody = JsonConvert.SerializeObject(requestBodyObj);

                // Initial request to create document
                MyCredsApiCalls myCredsApiCalls = new MyCredsApiCalls();
                documentID = myCredsApiCalls.CreateDocumentRequest(requestJsonBody);

                if (!string.IsNullOrWhiteSpace(documentID))
                {
                    // Use DocumentID to upload Pdf/Xml bytes
                    if (uploadXML && !string.IsNullOrWhiteSpace(myCredsTransaction.XmlContent))
                    {
                        // Update file bytes with the Xml content
                        fileBytes = Encoding.UTF8.GetBytes(myCredsTransaction.XmlContent);
                        myCredsTransaction.FileName = myCredsTransaction.FileName.Replace(".pdf", ".xml");
                    }
                    success = myCredsApiCalls.UploadFileByteContent(documentID, myCredsTransaction.FileName, fileBytes, uploadXML);
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ExportStudentTranscriptAPI", "Error: ", ex.ToString());
            }

            return success;
        }

        public void ExportTranscriptsToFilesByStudentIDs(Dictionary<string, List<string>> studentRequestIdList, ref UserResultObj userResultObj, bool useMyCredsXsl = false, bool uploadMyCredsAPI = false)
        {
            bool success = false;
            string fileLocation = string.Empty;
            string myCredsPath = string.Empty;
            string fileName = string.Empty;
            byte[] fileArray = null;

            string transactionTranscriptUuid = string.Empty;
            List<MyCredsTransactionObj> MyCredsTransactionObjList = new List<MyCredsTransactionObj>();
            Dictionary<string, string> folderSettings = new Dictionary<string, string>();
            CsvBatchObj csvBatchObj = null;
            List<CsvBatchObj> csvBatchObjList = new List<CsvBatchObj>();

            MyCredsMessage myCredsMessage = null;

            System.Web.HtmlString TransformedDocument = new System.Web.HtmlString(Structs.Literals.DocumentNotRendered);

            try
            {
                // Steps:
                // 1 - Retrieve student transcript
                // 2 - Generate Xml data
                // 3 - Convert XML to PDF and save files into a folder

                // 1 - Retrieve student transcripts and queue GetColleagueTranscript for all request selected
                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    foreach (var student in studentRequestIdList)
                    {
                        if (!string.IsNullOrWhiteSpace(student.Key))
                        {
                            // Create TransactionTranscript and queue GetColleagueTranscript
                            transactionTranscriptUuid = CreateQueueTransactionTranscript(null, student.Key);
                            // Save UUID for further processing
                            MyCredsTransactionObj _MyCredsTransaction = new MyCredsTransactionObj()
                            {
                                UUID = transactionTranscriptUuid,
                                sNumber = student.Key,
                                RequestIdList = student.Value,
                                RequestedDate = _XmlCreatedDate,
                                ProducedDate = _XmlCreatedDate,
                            };
                            MyCredsTransactionObjList.Add(_MyCredsTransaction);
                        }
                    }
                }

                // Setup MyCreds folder
                string domain = GetSetting(Structs.SettingTypes.String, Structs.Settings.LcDomain);
                folderSettings.Add("domain", domain);
                string user = GetSetting(Structs.SettingTypes.String, Structs.Settings.LcIntegrationUserName);
                folderSettings.Add("user", user);
                string pwd = GetSetting(Structs.SettingTypes.String, Structs.Settings.LcIntegrationPassword);
                folderSettings.Add("password", pwd);
                myCredsPath = SetupMyCredsFolder(Structs.MyCredsDocumentTypes.Transcript);
                folderSettings.Add("path", myCredsPath);

                foreach (var transaction in MyCredsTransactionObjList)
                {
                    if (!string.IsNullOrWhiteSpace(transaction.UUID) && !string.IsNullOrWhiteSpace(transaction.sNumber))
                    {
                        // 2 - Generate Xml data
                        CreateMsgViewObj createMsgObj = new CreateMsgViewObj()
                        {
                            TransactionTranscriptUuid = transaction.UUID,
                            DestinationDetails = new List<DestInstObj>(),
                            MyCredsMessage = true,
                            useMyCredsXsl = useMyCredsXsl,
                            StudentRequestLogIDList = transaction.RequestIdList,
                        };

                        // Adding Destination Institution, required for the Transcript XML creation
                        DestInstObj destInstObj = new DestInstObj()
                        {
                            Destination = new InstitutionObj(),
                        };
                        Institution institution = GetInstByLOrgID(GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode));
                        destInstObj.Destination.InstitutionID = institution.InstitutionId;
                        createMsgObj.DestinationDetails.Add(destInstObj);

                        // Creating XML Transcripts
                        CreateColleagueTranscript(createMsgObj);

                        // 3 - Convert XML to PDF and save files into a folder
                        // Transform XMl and XSL to HTML
                        // Then convert HTML to PDF
                        myCredsMessage = GetMyCredsMessage(transaction.UUID, Enums.MyCredsDocumentTypes.Transcript);

                        if (myCredsMessage != null && myCredsMessage.MessageXML != null)
                        {
                            string defaultXslPath = Structs.StyleSheetPaths.MyCredsTranscript;
                            defaultXslPath = Functions.GetApplicationPath() + defaultXslPath;
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(myCredsMessage.MessageXML);
                            TransformXml xmlToTransform = new TransformXml(xmlDoc, Enums.XsltAllow.DocumentFunction | Enums.XsltAllow.DtdProcessing | Enums.XsltAllow.ExternalResources, new Uri(defaultXslPath, UriKind.Absolute));

                            TransformedDocument = new System.Web.HtmlString(xmlToTransform.ApplyTransformation());

                            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter()
                            {
                                Size = NReco.PdfGenerator.PageSize.Letter,
                            };

                            if (useMyCredsXsl)
                            {
                                // Add space at the top and bottom margins for MyCreds overlay
                                NReco.PdfGenerator.PageMargins pageMargins = new NReco.PdfGenerator.PageMargins()
                                {
                                    Top = 35,  // mm
                                    Bottom = 30, // mm
                                };
                                htmlToPdf.Margins = pageMargins;
                            }

                            // Generate Pdf transcript file
                            fileArray = htmlToPdf.GeneratePdf(TransformedDocument.ToHtmlString());

                            fileName = Structs.MyCredsDocumentTypes.Transcript + "_" + myCredsMessage.sNumber + ".pdf"; //  + "_" + DateTime.Now.ToString("dd_MMM_yyyy_HHmmss") + ".pdf";

                            // Save PDF files to MyCreds folder
                            transaction.BatchCode = myCredsMessage.BatchCode;
                            transaction.FullName = myCredsMessage.FullName;
                            transaction.FileName = fileName;
                            transaction.Email = myCredsMessage.Email;
                            transaction.XmlContent = myCredsMessage.MessageXML;
                            fileLocation = SaveToMyCredsFolder(fileArray, folderSettings, fileName, transaction);

                            if (!string.IsNullOrWhiteSpace(fileLocation))
                            {
                                csvBatchObj = new CsvBatchObj()
                                {
                                    sNumber = myCredsMessage.sNumber,
                                    FullName = myCredsMessage.FullName,
                                    Email = myCredsMessage.Email,
                                    DocumentType = Structs.MyCredsDocumentTypes.Transcript,
                                    BatchCode = myCredsMessage.BatchCode,
                                    FileName = fileName,
                                };
                                csvBatchObjList.Add(csvBatchObj);
                            }

                            // API calls to upload Student Transcript to MyCreds - Possibility of queueing in the future
                            if (uploadMyCredsAPI)
                            {
                                // Upload pdf file
                                ExportStudentTranscriptAPI(transaction, fileArray);

                                // Upload/attach xml content
                                // ExportStudentTranscriptAPI(transaction, null, true);  => Check with MyCreds on how to attach the Transcript Xml content ????
                            }
                        }
                    }
                }

                success = !string.IsNullOrWhiteSpace(fileLocation);

                if (success)
                {
                    // Save Csv Metadata batch file
                    success = SaveCsvMetadata(csvBatchObjList, folderSettings, Structs.MyCredsDocumentTypes.Transcript);
                }

                userResultObj.Success = success;
                if (uploadMyCredsAPI)
                {
                    // Api upload message
                    userResultObj.Message = (success ? Structs.Export.BatchUploadedWithSucess : Structs.Export.BatchFilesNotUploaded);
                } else
                {
                    // Manual upload message
                    userResultObj.Message = (success ? Structs.Export.BatchExportedWithSucess + "<br/><br/> Batch location: <b> " + fileLocation + "</b>" : Structs.Export.BatchFilesNotSaved);
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ExportTranscriptsToColleague", "Error: ", "transmissionDataIdList:" + studentRequestIdList.ToString() + ", " + ex.ToString());
            }
        }

        // Setup file path by environment
        public string SetupMyCredsFolder(string documentType)
        {
            string dirpath = string.Empty;
            string UNCPath = string.Empty;
            string impdir = string.Empty;

            try
            {
                // Select proper sever and directory for the environment
                string env = Functions.GetEnvironment();
                switch (env)
                {
                    case Structs.Environment.Test:
                        UNCPath = GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCTestPath);
                        impdir = GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCMyCredsTestDirectory);
                        break;
                    case Structs.Environment.ROTest:
                        UNCPath = GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCROTestPath);
                        impdir = GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCMyCredsROTestDirectory);
                        break;
                    case Structs.Environment.Patch:
                        UNCPath = GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCPatchPath);
                        impdir = GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCMyCredsPatchDirectory);
                        break;
                    case Structs.Environment.Dev:
                        UNCPath = GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCDevPath);
                        impdir = GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCMyCredsDevDirectory);
                        break;
                    default:  // Prod
                        UNCPath = GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCProdPath);
                        impdir = GetSetting(Structs.SettingTypes.String, Structs.Settings.collUNCMyCredsProdDirectory);
                        break;
                }

                // Select document type and folder
                switch (documentType)
                {
                    case Structs.MyCredsDocumentTypes.Transcript:
                    case Structs.MyCredsDocumentTypes.TranscriptPre1995:
                        impdir += @"\" + Structs.MyCredsFolders.Transcripts + @"\" + Structs.MyCredsFolders.Export;
                        break;
                    case Structs.MyCredsDocumentTypes.ConfirmationGraduation:
                    case Structs.MyCredsDocumentTypes.ConfirmationGraduationInternational:
                        impdir += @"\" + Structs.MyCredsFolders.Credentials;
                        break;
                    case Structs.MyCredsDocumentTypes.ConfirmationEnrolment:
                        impdir += @"\" + Structs.MyCredsFolders.Conf_Enrollment;
                        break;
                    case Structs.MyCredsDocumentTypes.Parchment:
                        impdir += @"\" + Structs.MyCredsFolders.Badges;
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrWhiteSpace(UNCPath) && !string.IsNullOrWhiteSpace(impdir))
                {
                    dirpath = Path.Combine(UNCPath, impdir);
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "SetupMyCredsFolder", "Error", ex.ToString());
            }

            return dirpath;
        }

        // Save MyCreds Pdf files
        public string SaveToMyCredsFolder(byte[] fileArray, Dictionary<string, string> folderSettings, string fileName, MyCredsTransactionObj myCredsTransaction)
        {
            bool success = false;
            string returnLocation = string.Empty;

            try
            {
                if (fileArray != null && fileArray.Any() && !string.IsNullOrWhiteSpace(fileName) && folderSettings != null && folderSettings.Any())
                {
                    string dirpath = folderSettings["path"];
                    string user = folderSettings["user"];
                    string pwd = folderSettings["password"];
                    string domain = folderSettings["domain"];

                    string filepath = Path.Combine(dirpath, fileName);

                    try
                    {
                        new UNCAccessWithCredentials.UNCAccessWithCredentials().NetUseDelete();

                        using (UNCAccessWithCredentials.UNCAccessWithCredentials unc = new UNCAccessWithCredentials.UNCAccessWithCredentials())
                        {
                            if (unc.NetUseWithCredentials(dirpath, user, domain, pwd))
                            {
                                Directory.CreateDirectory(dirpath);
                                var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                                fileStream.Write(fileArray, 0, fileArray.Length);
                                fileStream.Close();
                                success = true;
                            }
                            else
                            {
                                SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "SaveToMyCredsFolder", "Error", "Failed to connect to " + dirpath + "\r\nLastError = " + unc.LastError.ToString());
                            }
                            unc.NetUseDelete();
                        }
                    }
                    catch (Exception ex)
                    {
                        SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "SaveToMyCredsFolder", "Error", "Unable to write to UNCPath: " + fileName + ". " + ex.ToString());
                    }

                    // Save Date Produced in Colleague TRRQ Request
                    if (success)
                    {
                        using (ColleagueLogic collLogic = new ColleagueLogic())
                        {
                            success = collLogic.SaveTRRQRequestDateProduced(myCredsTransaction);
                        }
                    }

                    if (success) { returnLocation = dirpath; }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "SaveToMyCredsFolder", "Error", ex.ToString());
            }
            
            return returnLocation;
        }

        public bool SaveCsvMetadata(List<CsvBatchObj> csvBatchObjList, Dictionary<string, string> folderSettings, string documentType)
        {
            bool success = false;
            string fileName = string.Empty;

            try
            {
                if (csvBatchObjList != null && csvBatchObjList.Any() && folderSettings != null && folderSettings.Any())
                {
                    string dirpath = folderSettings["path"];
                    string user = folderSettings["user"];
                    string pwd = folderSettings["password"];
                    string domain = folderSettings["domain"];

                    // Generate the Csv metadata file
                    StringBuilder contentFile = new StringBuilder();

                    // If you want headers for your file
                    var header = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                                               "id",
                                               "fullName",
                                               "email",
                                               "documentType",
                                               "Batch Code",
                                               "file",
                                               "display_name",
                                               "access_charge_method",
                                               "access_charge_amount",
                                               "access_charge_currency"
                                              );
                    contentFile.AppendLine(header);
                    foreach (var csvBatch in csvBatchObjList)
                    {
                        var listResults = string.Format("\"{0}\",{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                                        csvBatch.sNumber,
                                        csvBatch.FullName,
                                        csvBatch.Email,
                                        csvBatch.DocumentType.ToLower(),
                                        csvBatch.BatchCode,
                                        csvBatch.FileName,
                                        csvBatch.DocumentType,
                                        Structs.MyCredsCharges.Method,
                                        Structs.MyCredsCharges.Amount.Substring(0, Structs.MyCredsCharges.Amount.Length-2),  // Remove decimal part for csv metadata
                                        Structs.MyCredsCharges.Currency
                                       );
                        contentFile.AppendLine(listResults);
                    }
                    byte[] fileArray = Encoding.ASCII.GetBytes(contentFile.ToString());

                    fileName = documentType + "_batch_" + DateTime.Now.ToString("dd_MMM_yyyy_HHmmss") + ".csv";
                    string filepath = Path.Combine(dirpath, fileName);

                    try
                    {
                        new UNCAccessWithCredentials.UNCAccessWithCredentials().NetUseDelete();

                        using (UNCAccessWithCredentials.UNCAccessWithCredentials unc = new UNCAccessWithCredentials.UNCAccessWithCredentials())
                        {
                            if (unc.NetUseWithCredentials(dirpath, user, domain, pwd))
                            {
                                Directory.CreateDirectory(dirpath);
                                var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                                fileStream.Write(fileArray, 0, fileArray.Length);
                                fileStream.Close();
                                success = true;
                            }
                            else
                            {
                                SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "SaveCsvMetadata", "Error", "Failed to connect to " + dirpath + "\r\nLastError = " + unc.LastError.ToString());
                            }
                            unc.NetUseDelete();
                        }
                    }
                    catch (Exception ex)
                    {
                        SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "SaveCsvMetadata", "Error", "Unable to write to UNCPath: " + fileName + ". " + ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.Utility, "SaveCsvMetadata", "Error", ex.ToString());
            }

            return success;
        }

        public bool SendMyCredsTranscriptResponse(string requestId)
        {
            Dictionary<string, List<string>> requestIdList = new Dictionary<string, List<string>>();
            UserResultObj userResultObj = new UserResultObj()
            {
                Success = false,
                Message = Structs.Literals.ContactHelpDesk,
            };

            try
            {
                if (!string.IsNullOrWhiteSpace(requestId))
                {
                    using (ColleagueLogic collLogic = new ColleagueLogic())
                    {
                        // Retrieve Student Ids by TRRQ Request Ids
                        requestIdList = collLogic.GetMyCredsTRRQRequest(requestId);
                    }

                    if (requestIdList != null && requestIdList.Any())
                    {
                        // Export Files by Student IDs
                        ExportTranscriptsToFilesByStudentIDs(requestIdList, ref userResultObj, true, true);
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SendMyCredsTranscriptResponse", "Error", ex.ToString());
            }
            return userResultObj.Success;
        }

        public bool GetAliasStudent(StuLookupListViewObj lookup) {
            bool success = false;

            try

            {
                var results = (from s in ctx.Students
                where s.StudentAlias.OrderByDescending(y => y.ModifiedDateTime).FirstOrDefault().AliasActive == true
                select new StudentRecordObj
                {
                    Snumber = s.StudentAlias.Where(z => z.AliasActive == true).OrderByDescending(g => g.CreatedDateTime).FirstOrDefault().AliasNumber ?? s.sNumbers.OrderByDescending(y=>y.CreatedDateTime).FirstOrDefault().sNumVal ?? string.Empty,
                    StudentId = s.StudentId,
                    ASN = s.ASNs.Where(x => x.CollegeData == true).OrderByDescending(y => y.ModifiedDateTime).FirstOrDefault().AgencyAssignedID,
                    FirstName = s.Person.Names.Where(y => y.NameType == Structs.Name.PersonalNameType && y.CollegeData == true).OrderByDescending(x=>x.ModifiedDateTime).FirstOrDefault().FirstName,
                    MiddleName = s.Person.Names.Where(y => y.NameType == Structs.Name.PersonalNameType && y.CollegeData == true).OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().MiddleNames,
                    LastName = s.Person.Names.Where(y => y.NameType == Structs.Name.PersonalNameType && y.CollegeData == true).OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().LastName,
                    BirthDate = s.Person.BirthDate,
                    Gender = s.Person.Genders.Where(z => z.CollegeData == true).OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().GenderCodeType.ToString().Substring(0,1),
                    Addr1 = s.Person.Addresses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().AddressLine1 + " " + s.Person.Addresses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().AddressLine2,
                    City = s.Person.Addresses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().City,
                    State = s.Person.Addresses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().Province,
                    Zip = s.Person.Addresses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().PostalCode,
                    Country = s.Person.Addresses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().Country ?? "CA",
                    Phone = s.Person.Phones.OrderByDescending(x => x.ModifiedDateTime).Select(y => y.AreaCode + " " + y.PhoneNumber).FirstOrDefault(),
                    Email = s.Person.Emails.Where(y => y.EmailType == Structs.Email.PrimaryEmailType).OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault().EmailAddress,
                    FormerNames = (from n in ctx.PersonNames.Where(x => x.Person.PersonId == s.PersonId && x.NameType == Structs.Name.FormerType && x.CollegeData == true)
                                   select new StuNameObj
                                   {
                                       LastName = n.LastName,
                                       FirstName = n.FirstName,
                                       MiddleName = n.MiddleNames,
                                   }).ToList()
                });

                if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.Snumber))
                {
                    if (lookup.SearchFilter.StudentRecord.Snumber.Contains("..."))
                    {
                        lookup.SearchFilter.StudentRecord.Snumber.Replace("...", "");
                        results = results.Where(x => x.Snumber.StartsWith(lookup.SearchFilter.StudentRecord.Snumber));
                    }
                    else
                    {
                        results = results.Where(x => x.Snumber.Contains(lookup.SearchFilter.StudentRecord.Snumber));
                    }
                }

                if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.ASN))
                {
                    if (lookup.SearchFilter.StudentRecord.ASN.Contains("..."))
                    {
                        lookup.SearchFilter.StudentRecord.ASN.Replace("...", "");
                        results = results.Where(x => x.ASN.StartsWith(lookup.SearchFilter.StudentRecord.ASN));
                    }
                    else
                    {
                        results = results.Where(x => x.ASN.Contains(lookup.SearchFilter.StudentRecord.ASN));
                    }
                }

                if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.LastName))
                {
                    if (lookup.SearchFilter.StudentRecord.LastName.Contains("..."))
                    {
                        lookup.SearchFilter.StudentRecord.LastName = lookup.SearchFilter.StudentRecord.LastName.Replace("...", "");
                        results = results.Where(x => x.LastName.ToLower().StartsWith(lookup.SearchFilter.StudentRecord.LastName.ToLower())
                                        || x.LastName.ToLower().StartsWith(lookup.SearchFilter.StudentRecord.LastName.ToLower()));
                    }
                    else
                    {
                        results = results.Where(x => x.LastName.ToLower().Contains(lookup.SearchFilter.StudentRecord.LastName.ToLower())
                                    || x.LastName.ToLower().Contains(lookup.SearchFilter.StudentRecord.LastName.ToLower()));
                    }
                }

                if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.FirstName))
                {
                    if (lookup.SearchFilter.StudentRecord.FirstName.Contains("..."))
                    {
                        lookup.SearchFilter.StudentRecord.FirstName = lookup.SearchFilter.StudentRecord.FirstName.Replace("...", "");
                        results = results.Where(x => x.FirstName.ToLower().StartsWith(lookup.SearchFilter.StudentRecord.FirstName.ToLower())
                                        || x.FirstName.ToLower().StartsWith(lookup.SearchFilter.StudentRecord.FirstName.ToLower()));
                    }
                    else
                    {
                        results = results.Where(x => x.FirstName.ToLower().Contains(lookup.SearchFilter.StudentRecord.FirstName.ToLower())
                                    || x.FirstName.ToLower().Contains(lookup.SearchFilter.StudentRecord.FirstName.ToLower()));
                    }
                }

                if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.MiddleName))
                {
                    if (lookup.SearchFilter.StudentRecord.MiddleName.Contains("..."))
                    {
                        lookup.SearchFilter.StudentRecord.MiddleName = lookup.SearchFilter.StudentRecord.MiddleName.Replace("...", "");
                        results = results.Where(x => x.MiddleName.ToLower().StartsWith(lookup.SearchFilter.StudentRecord.MiddleName.ToLower())
                                        || x.MiddleName.ToLower().StartsWith(lookup.SearchFilter.StudentRecord.MiddleName.ToLower()));
                    }
                    else
                    {
                        results = results.Where(x => x.MiddleName.ToLower().Contains(lookup.SearchFilter.StudentRecord.MiddleName.ToLower())
                                    || x.MiddleName.ToLower().Contains(lookup.SearchFilter.StudentRecord.MiddleName.ToLower()));
                    }
                }

                if (lookup.SearchFilter.StudentRecord.BirthDate != null)
                {
                    results = results.Where(x => x.BirthDate == lookup.SearchFilter.StudentRecord.BirthDate);
                }

                //if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.Gender) && lookup.SearchFilter.StudentRecord.Gender != Library.Apas.CoreMain.GenderCodeType.Unreported.ToString())
                //{
                //    results = results.Where(a => a.GenderCode == lookup.SearchFilter.StudentRecord.GenderCode);
                //}

                lookup.Pagination.RecCount = results.Count();
                lookup.Pagination.PageCount = 1;
                if (results.Count() == 1) {
                    lookup.StudentRecords = results.ToList();
                }
                else if (results.Count() > 0)
                {
                    lookup.StudentRecords = results.OrderBy(c => c.FirstName).ThenBy(n => n.LastName).Skip((lookup.Pagination.PageIndex - 1) * lookup.Pagination.PageSize).Take(lookup.Pagination.PageSize).ToList();
                    lookup.Pagination.PageCount = (results.Count() + lookup.Pagination.PageSize - 1) / lookup.Pagination.PageSize;
                }

                if (results.Count() > 0) { success = true; }

            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetLocalStudent", "Error: ", ex.ToString());
            }
            return success;
        }

        public Library.Apas.AcademicRecord.ResponseStatusType? GetAliasResponseType(StudentRecordObj studentRecord)
        {
            Library.Apas.AcademicRecord.ResponseStatusType? responseStatusType = null;
            try
            {
                if (studentRecord.StudentId != null) {
                    responseStatusType = ctx.StudentAlias.Where(x => x.AliasActive == true && x.Student.StudentId == studentRecord.StudentId).OrderByDescending(z => z.ModifiedDateTime).Select(y => y.AliasStatusType).FirstOrDefault(); // ?? Library.Apas.AcademicRecord.ResponseStatusType.Hold;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetAliasResponseType", "Error: ", ex.ToString());
            }
            return responseStatusType;
        }

        public bool IsActiveAliasStudent(int? studentId, bool onlyResponseTypes = false)
        {
            bool active = false;
            try
            {
                if (studentId != null)
                {
                    var result = ctx.StudentAlias.Where(x => x.Student.StudentId == studentId && x.AliasActive == true);

                    if (onlyResponseTypes) {
                        result = result.Where(x => x.AliasStatusType != Library.Apas.AcademicRecord.ResponseStatusType.TranscriptSent);
                    };

                    active = result.Any();
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "IsActiveAliasStudent", "Error: ", ex.ToString());
            }
            return active;
        }

        public bool SetValidApasInstitions(List<quesvc.RegisteredEducationalInstitution> institutions) {
            bool success = false;
            try
            {
                Institution _TempInstitution = new Institution();

                foreach (quesvc.RegisteredEducationalInstitution i in institutions)
                {

                    bool insertNewName = false;
                    DateTime _Now = DateTime.Now;

                    _TempInstitution = ctx.Institutions.Where(x => x.LocalOrganizationID == i.SourceId).OrderByDescending(z=>z.CreatedDateTime).FirstOrDefault();

                    if (_TempInstitution != null)
                    {
                        _TempInstitution.ApasInstitution = true;

                        InstitutionName _InstitutionName = _TempInstitution.InstitutionNames.Where(x => x.Name.ToUpper().Trim() == i.EducationalInstitutionName.ToUpper().Trim()).OrderByDescending(y => y.CreatedDateTime).FirstOrDefault();

                        if (_InstitutionName != null)
                        {
                            _InstitutionName.ModifiedDateTime = _Now;
                        }
                        else {
                            insertNewName = true;
                        }
                    }
                    else {

                        _TempInstitution = new Institution()
                        {
                            LocalOrganizationID = i.SourceId,
                            DefaultInstitution = true,
                            ApasInstitution = true,
                            APAS = i.SourceId,
                            ESIS = i.SourceId,
                            CreatedBy = _UserName,
                            CreatedDateTime = _Now,
                            ModifiedBy = _UserName,
                            ModifiedDateTime = _Now,
                        };

                        ctx.Institutions.Add(_TempInstitution);

                        insertNewName = true;
                    }

                    if (insertNewName)
                    {
                        InstitutionName _NewInstitutionName = new InstitutionName()
                        {
                            Name = i.EducationalInstitutionName.ToUpper().Trim(),
                            DefaultName = true,
                            CreatedBy = _UserName,
                            CreatedDateTime = _Now,
                            ModifiedBy = _UserName,
                            ModifiedDateTime = _Now,
                            Institution = _TempInstitution,
                        };

                        ctx.InstitutionNames.Add(_NewInstitutionName);
                    }

                    ctx.SaveChanges();
                }


                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetLocalDbInstitutions", "Error: ", ex.ToString());
            }
            return success;
        }

        public bool SaveTransmissionDataExportedDateTime(string requestTrackingId = null, string transDataId = null, DateTime? exportedDate = null)
        {
            bool success = false;
            TransmissionData transDataObj = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(transDataId) && Int32.TryParse(transDataId, out Int32 intTransDataId))
                {
                    // Search by TransmissionDataId
                    transDataObj = ctx.TransmissionDatas.Find(intTransDataId);
                } else
                {
                    if (!string.IsNullOrWhiteSpace(requestTrackingId))
                    {
                        // Search by RequestTrackingId
                        transDataObj = ctx.TransmissionDatas.Where(a => a.RequestTrackingID == requestTrackingId && a.ExportedDateTime == null).OrderByDescending(o => o.CreatedDateTime).FirstOrDefault();
                    }
                }

                // If the Transmissiondata exists and it was not exported already
                if (transDataObj != null && transDataObj.ExportedDateTime == null)
                {
                    transDataObj.ExportedDateTime = exportedDate ?? DateTime.Now;
                    transDataObj.ModifiedBy = _UserName;
                    transDataObj.ModifiedDateTime = DateTime.Now;
                }

                ctx.SaveChanges();

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveTransmissionDataExportedDateTime", "Error", ex.ToString());
            }
            return success;
        }

        #endregion End Transcripts

        #region Build Objects

        /// <summary>
        /// Create Apas Request from local request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string CreateApasRequest(CreateMsgViewObj requestObj)
        {
            List<string> _Uuids = new List<string>();
            string xmlString = string.Empty;
            string nsPrefix = "TR";
            string uri = Structs.SchemaVersion.TranscriptRequest_v1_4_0;
            string schema = "TranscriptRequest_v1.4.0.xsd";
            string _RequestTrackingID = string.Empty;
            string _DocumentID = string.Empty;
            string _UUID = string.Empty;
            string _TransmissionDataUUID = string.Empty;

            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                // set electronic delivery
                Library.Apas.AcademicRecord.ElectronicDeliveryType _ElectronicDelivery = new Library.Apas.AcademicRecord.ElectronicDeliveryType()
                {
                    ElectronicFormat = Library.Apas.AcademicRecord.ElectronicFormatType.EDI,
                    ElectronicMethod = Library.Apas.AcademicRecord.ElectronicMethodType.ServiceProvider,
                    ServiceProvider = "Apply Alberta",
                };

                foreach (DestInstObj dest in requestObj.DestinationDetails)
                {
                    if (dest.Destination.InstitutionID > 0)  // Verify if Destination Institution was selected
                    {
                        List<Library.Apas.AcademicRecord.RequestType> _Requests = new List<Library.Apas.AcademicRecord.RequestType>();

                        _UUID = Guid.NewGuid().ToString();
                        _RequestTrackingID = Functions.GetNewRequestTrackingId();
                        _DocumentID = Functions.GetNewLcDocumentID();

                        // get local apas code
                        Library.Apas.AcademicRecord.SourceDestinationType _Source = BuildSourceDestinationType(GetInstByLOrgID(GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode)), true);

                        // get destination
                        Library.Apas.AcademicRecord.SourceDestinationType _Destination = BuildSourceDestinationType(GetInstitution(dest.Destination.InstitutionID.ToString()));

                        // setup TransmissionData
                        string env = Functions.GetEnvironment();
                        Library.Apas.AcademicRecord.TransmissionDataType _TransmissionData = new Library.Apas.AcademicRecord.TransmissionDataType()
                        {
                            CreatedDateTime = DateTime.Now,
                            DocumentID = _DocumentID,
                            RequestTrackingID = _RequestTrackingID,
                            DocumentTypeCode = Library.Apas.CoreMain.DocumentTypeCodeType.InstitutionRequest,
                            TransmissionType = Library.Apas.CoreMain.TransmissionTypeType.Original,
                            DocumentCompleteCode = Library.Apas.CoreMain.DocumentCompleteCodeType.Complete,
                            DocumentCompleteCodeSpecified = true,
                            DocumentOfficialCode = Library.Apas.CoreMain.DocumentOfficialCodeType.Official,
                            DocumentOfficialCodeSpecified = true,
                            DocumentProcessCode = (env == Structs.Environment.Prod ? Library.Apas.CoreMain.DocumentProcessCodeType.PRODUCTION : Library.Apas.CoreMain.DocumentProcessCodeType.TEST),
                            DocumentProcessCodeSpecified = true,
                            Destination = _Destination,
                            Source = _Source,
                        };

                        // Set Transcript Hold
                        Library.Apas.AcademicRecord.TranscriptHoldType _TranscriptHold = new Library.Apas.AcademicRecord.TranscriptHoldType()
                        {
                            // TODO: needs to be result of drop down hold type
                            // is this a per request item or for all requests?
                            HoldType = dest.HoldTypeType,
                            SessionDesignator = dest.HoldTypeData, // Fomat yyyy-mm
                            //NoteMessage = dest.HoldTypeData,
                            // TODO: What about these other fields
                            //SessionDesignator = ,
                            //SessionName = ,
                        };

                        // TODO: each request will just have one recipient (the way we built this - will change if requested by Student Services during testing)
                        List<Library.Apas.AcademicRecord.RecipientType> _Recipients = new List<Library.Apas.AcademicRecord.RecipientType>() {
                            new Library.Apas.AcademicRecord.RecipientType()
                            {
                                RequestTrackingID = _RequestTrackingID,
                                TranscriptHold = _TranscriptHold,
                                TranscriptPurpose = Library.Apas.AcademicRecord.TranscriptPurposeType.Admission,
                                TranscriptPurposeSpecified = true,
                                Receiver = new Library.Apas.AcademicRecord.RequestorReceiverType() {
                                     Item = new Library.Apas.AcademicRecord.RequestorReceiverOrganizationType() {
                                          OrganizationName = _Source.Organization.OrganizationName,
                                     },
                                }
                            },
                        };

                        // setup person
                        Library.Apas.AcademicRecord.PersonType _Person = BuildPersonType(requestObj.StudentRecord);

                        // setup Attendance
                        List<Library.Apas.AcademicRecord.AttendanceType> _AttendanceList = new List<Library.Apas.AcademicRecord.AttendanceType>();
                        Library.Apas.AcademicRecord.AttendanceType _Attendance = new Library.Apas.AcademicRecord.AttendanceType() {
                            School = new Library.Apas.AcademicRecord.SchoolType()
                            {
                                OrganizationName = _Destination.Organization.OrganizationName.FirstOrDefault(),
                            },
                        };
                        _AttendanceList.Add(_Attendance);
                        //Student _Student = FindStudentByStudentRecord(requestObj.StudentRecord);
                        //if (_Student != null) { _AttendanceList = BuildAttendanceTypes(_Student); }

                        // setup requested student
                        Library.Apas.AcademicRecord.RequestedStudentType _RequestedStudent = new Library.Apas.AcademicRecord.RequestedStudentType()
                        {
                            Person = _Person,
                            Attendance = _AttendanceList,
                            //ReleaseAuthorizedMethod = Library.Apas.AcademicRecord.ReleaseAuthorizedMethodType.ElectronicSignature, //requestObj.ReleaseAuthorizedMethod,
                            //ReleaseAuthorizedMethodSpecified = requestObj.ReleaseAuthorizedIndicator,
                            //ReleaseAuthorizedIndicator = requestObj.ReleaseAuthorizedIndicator,
                            ReleaseAuthorizedIndicator = true,
                        };

                        // setup request
                        Library.Apas.AcademicRecord.RequestType _Request = new Library.Apas.AcademicRecord.RequestType()
                        {
                            CreatedDateTime = DateTime.Now,
                            RequestedStudent = _RequestedStudent,
                            Recipient = _Recipients,
                        };

                        _Requests.Add(_Request);

                        // setup the Apas Transfer Request
                        Library.Apas.TranscriptRequest.TranscriptRequest _ApasTransferRequest = new Library.Apas.TranscriptRequest.TranscriptRequest()
                        {
                            TransmissionData = _TransmissionData,
                            Request = _Requests.ToArray(),
                        };

                        // Serialize Apas Transfer Request
                        string requestXml = _ApasTransferRequest.Serialize(nsPrefix, uri);

                        // Load XMLDOC
                        xmlDoc.LoadXml(requestXml);

                        // Setup xml document attributes
                        XmlElement root = xmlDoc.DocumentElement;
                        XmlAttribute schemaLocation = xmlDoc.CreateAttribute("xsi", "schemaLocation", Structs.XmlAttributes.XmlSchemaInstance);
                        schemaLocation.Value = uri + " " + schema;
                        root.SetAttributeNode(schemaLocation);

                        // save new request to local db
                        //success = ParseRequestXml(requestObj.UUID, xmlDoc.InnerXml);

                        ApasMessage _ApasMessage = new ApasMessage()
                        {
                            UUID = _UUID,
                            ASN = requestObj.StudentRecord.ASN,
                            MessageType = Enums.MessageTypes.SentRequest,
                            MessageXML = xmlDoc.InnerXml,
                            SenderId = _Source.Organization.LocalOrganizationID.LocalOrganizationIDCode,
                            ReceiverId = _Destination.Organization.LocalOrganizationID.LocalOrganizationIDCode,
                        };

                        // if request message saves properly then parse it
                        if (SaveMessage(_ApasMessage))
                        {
                            if (ParseApasMessage(_ApasMessage.UUID, Enums.MessageTypes.SentRequest))
                            {
                                // Store UUIDs for preview (Display Message)
                                _Uuids.Add(_ApasMessage.UUID);
                            }
                        }
                    }
                }

                // Save UUIDs to the KeyValueTemp table and return it to Display Message
                _TransmissionDataUUID = SaveKeyValueTempCache(_Uuids.ToArray());
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "CreateApasRequest", "Error", ex.ToString());
            }

            return _TransmissionDataUUID;
        }

        public string CreateQueueTransactionTranscript(string uuid, string stuId, string transcriptGrouping = "PS")
        {
            string _TransactionTranscriptUuid = string.IsNullOrWhiteSpace(uuid) ? Guid.NewGuid().ToString() : uuid;
            DateTime now = DateTime.Now;

            try
            {
                if (!string.IsNullOrWhiteSpace(stuId))
                {
                    // Get transaction by UUID and Student ID
                    var query = ctx.TransactionTranscripts.Where(x => x.TransactionTranscriptUuid == _TransactionTranscriptUuid && x.StudentId == stuId);

                    if (!query.Any())
                    {
                        // if it hasn't found a transcation yet, create a new one
                        TransactionTranscript tranTran = new TransactionTranscript()
                        {
                            CreatedDateTime = now,
                            ModifiedDateTime = now,
                            StudentId = stuId,
                            TranscriptGrouping = transcriptGrouping,
                            TransactionTranscriptUuid = _TransactionTranscriptUuid,
                        };

                        ctx.TransactionTranscripts.Add(tranTran);
                        ctx.SaveChanges();

                        // A new TranscationTranscript record was created: Queue job to process it!
                        if (!string.IsNullOrWhiteSpace(_TransactionTranscriptUuid))
                        {
                            QueueJob(_TransactionTranscriptUuid, Enums.JobTypeType.GetColleagueTranscript);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "CreateQueueTransactionTranscript", "Error", ex.ToString());
            }

            return _TransactionTranscriptUuid;
        }

        public string GetStuIdByTranTranUuid(string transactionTranscriptUuid, bool completed = false)
        {
            string stuId = string.Empty;
            try
            {
                if (!string.IsNullOrWhiteSpace(transactionTranscriptUuid)) {

                    var results = ctx.TransactionTranscripts.Where(x => x.TransactionTranscriptUuid == transactionTranscriptUuid);

                    if (completed)
                    {
                        results = results.Where(y => y.CompletedDateTime != null);
                    } else
                    {
                        results = results.Where(y => y.CompletedDateTime == null);
                    }

                    if (results.Any())
                    {
                        stuId = results.FirstOrDefault().StudentId;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "GetStuIdByTranTranUuid", "Error", ex.ToString());
            }

            return stuId;
        }

        public bool CreateRequestLog(string transcriptGrouping, string transctionRequestLogUuid)
        {
            bool success = false;
            DateTime now = DateTime.Now;

            try
            {
                // Get Request information
                Request _Request = GetRequest(transctionRequestLogUuid);

                // Check if Transaction Request Log doesn't alredy exists and Request exists
                if (GetTransactionRequestLogByUuid(transctionRequestLogUuid) == null && _Request != null &&
                    ((_Request.RequestedStudent != null && _Request.RequestedStudent.sNumbers != null && _Request.RequestedStudent.sNumbers.Count > 0) ||
                     (_Request.MatchedStudent != null && _Request.MatchedStudent.sNumbers != null && _Request.MatchedStudent.sNumbers.Count > 0)))
                {
                    Library.Apas.AcademicRecord.HoldTypeType? _HoldType = _Request.Recipients.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault().TranscriptHolds.OrderByDescending(y => y.CreatedDateTime).Select(z => z.HoldType).FirstOrDefault();

                    string _StudentId = null;
                    if (_Request.MatchedStudent != null && _Request.MatchedStudent.sNumbers != null && _Request.MatchedStudent.sNumbers.Count > 0)
                    {
                        // Use a Matched Student's sNumber, if request has one
                        _StudentId = _Request.MatchedStudent.sNumbers.OrderByDescending(x => x.CreatedDateTime).Select(y => y.sNumVal).FirstOrDefault();
                    } else
                    {
                        // Otherwise use Requested Student's sNumber
                        _StudentId = _Request.RequestedStudent.sNumbers.OrderByDescending(x => x.CreatedDateTime).Select(y => y.sNumVal).FirstOrDefault();
                    }

                    TransactionRequestLog reqLog = new TransactionRequestLog()
                    {
                        CreatedDateTime = now,
                        ModifiedDateTime = now,
                        StudentId = _StudentId,
                        TranscriptGrouping = transcriptGrouping,
                        TransactionRequestLogUuid = transctionRequestLogUuid,
                        RequestType = "T",   // requestLog.TranscriptType.ToString(),  // Transcript Request
                        NumberOfCopies = "1",  // Default
                        Comments = Structs.Literals.GeneratedByApasToolkit,
                        RequestHoldCode = _HoldType == Library.Apas.AcademicRecord.HoldTypeType.AfterGradesPosted ? Structs.TranscripHoldTypes.AfterGradesPosted : 
                                         (_HoldType == Library.Apas.AcademicRecord.HoldTypeType.AfterDegreeAwarded ? Structs.TranscripHoldTypes.AfterDegreeAwarded :
                                          _HoldType == Library.Apas.AcademicRecord.HoldTypeType.AfterSpecifiedTerm ? Structs.TranscripHoldTypes.AfterSpecifiedTerm : null),  // Just if there is a real hold for it
                    };

                    // Recipient: Source Institution Information
                    TransmissionData _TransmissionData = GetTransmissionDataByUUID(transctionRequestLogUuid);
                    if (_TransmissionData != null && _TransmissionData.SourceInstitution != null)
                    {
                        if (!string.IsNullOrWhiteSpace(_TransmissionData.SourceInstitution.LocalOrganizationID))
                        {
                            using (ColleagueLogic collLogic = new ColleagueLogic())
                            {
                                reqLog.RecipientId = collLogic.GetColleagueInstitutionId(_TransmissionData.SourceInstitution.LocalOrganizationID);
                            }
                        }

                        Address _InstitutionAddress = GetInstitutionAddress(_TransmissionData.SourceInstitution.InstitutionId);
                        if (_InstitutionAddress != null)
                        {
                            reqLog.RecipientAddressLines = _InstitutionAddress.AddressLine1 + (!string.IsNullOrWhiteSpace(_InstitutionAddress.AddressLine2) ? " " + _InstitutionAddress.AddressLine2 : "");
                            reqLog.RecipientCity = _InstitutionAddress.City;
                            reqLog.RecipientState = _InstitutionAddress.Province;
                            reqLog.RecipientPostalCode = _InstitutionAddress.PostalCode;
                            reqLog.RecipientCountryCode = _InstitutionAddress.Country == "CA" ? "XCA" : _InstitutionAddress.Country;
                        }
                    }

                    ctx.TransactionRequestLogs.Add(reqLog);
                    ctx.SaveChanges();

                    success = true;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "CreateRequestLog", "Error", ex.ToString());
            }

            return success;
        }

        public TransactionRequestLog CreateMyCredsRequestLog(MyCredsTransactionObj myCredsTransaction)
        {
            TransactionRequestLog reqLog = null;
            DateTime now = DateTime.Now;

            try
            {
                // If Transaction Request Log doesn't alredy exists, create one
                if (GetTransactionRequestLogByUuid(myCredsTransaction.UUID) == null && !string.IsNullOrWhiteSpace(myCredsTransaction.sNumber))
                {
                    reqLog = new TransactionRequestLog()
                    {
                        CreatedDateTime = now,
                        ModifiedDateTime = now,
                        StudentId = myCredsTransaction.sNumber,
                        RecipientId = myCredsTransaction.sNumber,
                        RecipientAddressLines = myCredsTransaction.AddressLine,
                        RecipientCity = myCredsTransaction.City,
                        RecipientState = myCredsTransaction.State,
                        RecipientPostalCode = myCredsTransaction.PostalCode,
                        RecipientCountryCode = myCredsTransaction.Country == "CA" ? "XCA" : myCredsTransaction.Country,
                        TranscriptGrouping = myCredsTransaction.TransGroup,
                        TransactionRequestLogUuid = myCredsTransaction.UUID,
                        RequestType = "T",   // Transcript Request
                        NumberOfCopies = "1",  // Default
                        Comments = MyCredsRequestComments(null, myCredsTransaction.BatchCode),
                    };

                    ctx.TransactionRequestLogs.Add(reqLog);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                reqLog = null;  // TranscationRequestLog creation failed
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "CreateMyCredsRequestLog", "Error", ex.ToString());
            }

            return reqLog;
        }

        public string MyCredsRequestComments(string comments, string batchCode = null)
        {
            string result = string.Empty;
            string batchCodeComments = string.Empty;

            try
            {
                // Include Batch Code, if you have it
                if (!string.IsNullOrWhiteSpace(batchCode))
                {
                    batchCodeComments = " - Batch Code: " + batchCode;
                }

                if (string.IsNullOrWhiteSpace(comments))
                {
                    result = Structs.Literals.GeneratedByMyCredsToolkit + batchCodeComments;
                }
                else
                {
                    if (!comments.ToUpper().Contains("MYCREDS"))
                    {
                        result = Structs.Literals.GeneratedByMyCredsToolkit + batchCodeComments + " - " + comments;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "MyCredsRequestComments", "Error", ex.ToString());
            }
            return result;
        }

        public TransactionRequestLog GetTransactionRequestLogByUuid(string transactionRequestLogtUuid, bool notCompleted = true)
        {
            TransactionRequestLog _TransactionRequestLog = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(transactionRequestLogtUuid))
                {
                    var results = ctx.TransactionRequestLogs.Where(x => x.TransactionRequestLogUuid == transactionRequestLogtUuid);

                    if (notCompleted)
                    {
                        results = results.Where(x => x.CompletedDateTime == null);
                    }

                    if (results.Any())
                    {
                        _TransactionRequestLog = results.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "GetStudentByTranReqLogUuid", "Error", ex.ToString());
            }

            return _TransactionRequestLog;
        }
        
        /// <summary>
        /// Build Transcript
        /// </summary>
        /// <param name="responseObj"></param>
        /// <param name="academicRecords"></param>
        /// <returns></returns>
        public string BuildXmlTranscripts(CreateMsgViewObj responseObj, List<Library.Apas.AcademicRecord.AcademicRecordType> academicRecords)
        {
            string _NsPrefix = "ColTrn";
            string _Uri = Structs.SchemaVersion.CollegeTranscript_v1_6_0;
            string _Schema = "CollegeTranscript_v1.6.0.xsd";
            List<string> _Uuids = new List<string>();
            string _TransmissionDataUUID = string.Empty;
            Library.Apas.CollegeTranscript.CollegeTranscript _ApasTranscript = new Library.Apas.CollegeTranscript.CollegeTranscript();

            try
            {
                List<Library.Apas.AcademicRecord.TransmissionDataType> _TransmissionDatas = BuildTransmissionDatas(responseObj);

                foreach (Library.Apas.AcademicRecord.TransmissionDataType transmissionData in _TransmissionDatas)
                {
                    _ApasTranscript.Student = BuildStudentType(responseObj.StudentRecord);
                    _ApasTranscript.Student.AcademicRecord = academicRecords;
                    _ApasTranscript.TransmissionData = transmissionData;

                    string _ResponseXml = _ApasTranscript.Serialize(_NsPrefix, _Uri);

                    XmlDocument xmlDoc = new XmlDocument();

                    var stylesheetReference = xmlDoc.CreateProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"https://orion.lethbridgecollege.ca/prod/styles/schemas/20180604TranscriptLC.xsl\"");
                    
                    xmlDoc.LoadXml(_ResponseXml);
                    xmlDoc.InsertAfter(stylesheetReference, xmlDoc.FirstChild);
                    XmlElement root = xmlDoc.DocumentElement;

                    XmlAttribute schemaLocation = xmlDoc.CreateAttribute("xsi", "schemaLocation", Structs.XmlAttributes.XmlSchemaInstance);
                    schemaLocation.Value = _Uri + " " + _Schema;
                    root.SetAttributeNode(schemaLocation);

                    XmlAttribute AcRecAttr = xmlDoc.CreateAttribute("xmlns", "AcRec", Structs.XmlAttributes.XmlNsInstance);
                    AcRecAttr.Value = Structs.SchemaVersion.AcademicRecord_v1_9_0;
                    root.SetAttributeNode(AcRecAttr);

                    XmlAttribute CoreMainAttr = xmlDoc.CreateAttribute("xmlns", "core", Structs.XmlAttributes.XmlNsInstance);
                    CoreMainAttr.Value = Structs.SchemaVersion.CoreMain_v1_14_0;
                    root.SetAttributeNode(CoreMainAttr);

                    string _Uuid = Guid.NewGuid().ToString();

                    // Create APAS Message
                    ApasMessage _Message = new ApasMessage()
                    {
                        UUID = _Uuid,
                        ASN = responseObj.StudentRecord.ASN,
                        MessageType = Enums.MessageTypes.SentResponse,
                        MessageXML = xmlDoc.InnerXml,
                        SenderId = transmissionData.Source.Organization.LocalOrganizationID.LocalOrganizationIDCode,
                        ReceiverId = transmissionData.Destination.Organization.LocalOrganizationID.LocalOrganizationIDCode,
                    };

                    if (SaveMessage(_Message))
                    {
                        if (ParseApasMessage(_Message.UUID, Enums.MessageTypes.SentResponse))
                        {
                            _Uuids.Add(_Message.UUID);
                        }
                    }
                }

                // Save UUIDs to the KeyValueTemp table and return it to Display Message
                _TransmissionDataUUID = SaveKeyValueTempCache(_Uuids.ToArray());
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildXmlTranscripts", "Error", ex.ToString());
            }

            return _TransmissionDataUUID;
        }

        public string BuildMyCredsXmlTranscripts(CreateMsgViewObj myCredsMsgViewObj, List<Library.Apas.AcademicRecord.AcademicRecordType> academicRecords)
        {
            string _NsPrefix = "ColTrn";
            string _Uri = Structs.SchemaVersion.CollegeTranscript_v1_6_0;
            string _Schema = "CollegeTranscript_v1.6.0.xsd";
            XmlProcessingInstruction stylesheetReference = null;
            Library.Apas.CollegeTranscript.CollegeTranscript _ApasTranscript = new Library.Apas.CollegeTranscript.CollegeTranscript();

            try
            {
                List<Library.Apas.AcademicRecord.TransmissionDataType> _TransmissionDatas = BuildTransmissionDatas(myCredsMsgViewObj);
                foreach (Library.Apas.AcademicRecord.TransmissionDataType transmissionData in _TransmissionDatas)
                {

                    _ApasTranscript.Student = BuildStudentType(myCredsMsgViewObj.StudentRecord);
                    _ApasTranscript.Student.AcademicRecord = academicRecords;
                    _ApasTranscript.TransmissionData = transmissionData;

                    string _ResponseXml = _ApasTranscript.Serialize(_NsPrefix, _Uri);

                    XmlDocument xmlDoc = new XmlDocument();

                    // Check if it is a full transcript or just the data for the overlay
                    if (!myCredsMsgViewObj.useMyCredsXsl)
                    {
                        stylesheetReference = xmlDoc.CreateProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"https://orion.lethbridgecollege.ca/prod/styles/schemas/20180604TranscriptLC.xsl\"");
                    }

                    xmlDoc.LoadXml(_ResponseXml);
                    if (stylesheetReference != null) { xmlDoc.InsertAfter(stylesheetReference, xmlDoc.FirstChild); }
                    XmlElement root = xmlDoc.DocumentElement;

                    XmlAttribute schemaLocation = xmlDoc.CreateAttribute("xsi", "schemaLocation", Structs.XmlAttributes.XmlSchemaInstance);
                    schemaLocation.Value = _Uri + " " + _Schema;
                    root.SetAttributeNode(schemaLocation);

                    XmlAttribute AcRecAttr = xmlDoc.CreateAttribute("xmlns", "AcRec", Structs.XmlAttributes.XmlNsInstance);
                    AcRecAttr.Value = Structs.SchemaVersion.AcademicRecord_v1_9_0;
                    root.SetAttributeNode(AcRecAttr);

                    XmlAttribute CoreMainAttr = xmlDoc.CreateAttribute("xmlns", "core", Structs.XmlAttributes.XmlNsInstance);
                    CoreMainAttr.Value = Structs.SchemaVersion.CoreMain_v1_14_0;
                    root.SetAttributeNode(CoreMainAttr);

                    // Retrive student email
                    using (ColleagueLogic collLogic = new ColleagueLogic())
                    {
                        // Use email saved in the Student_Request_Log (TRRQ Request)
                        myCredsMsgViewObj.StudentRecord.Email = collLogic.GetStudentRequestLogEmail(myCredsMsgViewObj.StudentRecord.Snumber, myCredsMsgViewObj.StudentRequestLogIDList);

                        // Check if it's a valid email and it's not a Lethbride College email address
                        myCredsMsgViewObj.StudentRecord.Email.IsValidEmail();

                        // If it's not a valid email, use the Off campus email address
                        if (string.IsNullOrWhiteSpace(myCredsMsgViewObj.StudentRecord.Email))
                        {
                            myCredsMsgViewObj.StudentRecord.Email = collLogic.GetStudentPersonalEmail(myCredsMsgViewObj.StudentRecord.Snumber);
                        }
                    }

                    // Create MyCreds Message
                    MyCredsMessage _MyCredsMessage = new MyCredsMessage()
                    {
                        UUID = myCredsMsgViewObj.TransactionTranscriptUuid,
                        sNumber = myCredsMsgViewObj.StudentRecord.Snumber,
                        FullName = myCredsMsgViewObj.StudentRecord.FullName,
                        Email = myCredsMsgViewObj.StudentRecord.Email,
                        BatchCode = DateTime.Now.ToString("MMddyy"),
                        MessageType = Enums.MyCredsDocumentTypes.Transcript,
                        MessageXML = xmlDoc.InnerXml,
                        CreatedBy = _UserName,
                        CreatedDateTime = DateTime.Now,
                        ModifiedBy = _UserName,
                        ModifiedDateTime = DateTime.Now,
                    };
                    ctx.MyCredsMessages.Add(_MyCredsMessage);
                }
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildMyCredsXmlTranscripts", "Error", ex.ToString());
            }

            return myCredsMsgViewObj.TransactionTranscriptUuid;
        }

        /// <summary>
        /// BUILD RESPONSE
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        //public bool CreateApasResponse(CreateMsgViewObj responseObj)
        //{
        //    bool success = false;
        //    try
        //    {
        //        foreach (string uuid in CreateXmlResponses(responseObj))
        //        {
        //            if (success)
        //            {
        //                // Test Parse Response
        //                // success = ParseResponseXml(uuid);

        //                // For production we will want to queue this
        //                success = QueueJob(uuid, Enums.JobTypeType.ParseSentApasResponse);
        //            }

        //            if (success)
        //            {
        //                // TEST Send
        //                //using (ApasLogic apasLogic = new ApasLogic())
        //                //{
        //                //    // TODO: Test send Response
        //                //    success = apasLogic.SendTransferResponse(uuid);
        //                //}

        //                // For production we will want to queue this.
        //                success = QueueJob(uuid, Enums.JobTypeType.SendApasTransferResponse);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.ToString();
        //    }

        //    return success;
        //}

        //public List<string> CreateXmlResponses(CreateMsgViewObj responseObj)
        //{
        //    List<string> responseStrings = new List<string>();

        //    try
        //    {
        //        if (responseObj.DestAction == Structs.DestActions.CreateTranscript)
        //        {
        //            //responseStrings = BuildXmlTranscripts(responseObj);
        //        }
        //        else
        //        {
        //            responseStrings = BuildXmlResponses(responseObj);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.ToString();
        //    }
        //    return responseStrings;
        //}



        public string CreateApasResponse(CreateMsgViewObj responseObj)
        {
            string _NsPrefix = "TR";
            string _Uri = Structs.SchemaVersion.TranscriptResponse_v1_4_0;
            string _Schema = "TranscriptResponse_v1.4.0.xsd";
            List<string> _Uuids = new List<string>();
            string _TransmissionDataUUID = string.Empty;

            try
            {
                foreach (Library.Apas.AcademicRecord.TransmissionDataType transmissionData in BuildTransmissionDatas(responseObj))
                {
                    Library.Apas.TranscriptResponse.TranscriptResponse _TranscriptResponse = new Library.Apas.TranscriptResponse.TranscriptResponse()
                    {
                        Response = BuildResponses(responseObj),
                        TransmissionData = transmissionData,
                    };

                    string _ResponseXml = _TranscriptResponse.Serialize(_NsPrefix, _Uri);

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(_ResponseXml);
                    XmlElement root = xmlDoc.DocumentElement;


                    XmlAttribute schemaLocation = xmlDoc.CreateAttribute("xsi", "schemaLocation", Structs.XmlAttributes.XmlSchemaInstance);
                    schemaLocation.Value = _Uri + " " + _Schema;
                    root.SetAttributeNode(schemaLocation);

                    string _Uuid = Guid.NewGuid().ToString();

                    ApasMessage _Message = new ApasMessage()
                    {
                        UUID = _Uuid,
                        ASN = responseObj.StudentRecord.ASN,
                        MessageType = Enums.MessageTypes.SentResponse,
                        MessageXML = xmlDoc.InnerXml,
                        SenderId = transmissionData.Source.Organization.LocalOrganizationID.LocalOrganizationIDCode,
                        ReceiverId = transmissionData.Destination.Organization.LocalOrganizationID.LocalOrganizationIDCode,
                    };

                    // if message saves properly then parse it
                    if (SaveMessage(_Message))
                    {
                        if (ParseApasMessage(_Message.UUID, Enums.MessageTypes.SentResponse))
                        {
                            // Store UUIDs for preview (Display Message)
                            _Uuids.Add(_Message.UUID);
                        }
                    }
                }

                // Save UUIDs to the KeyValueTemp table and return it to Display Message
                _TransmissionDataUUID = SaveKeyValueTempCache(_Uuids.ToArray());
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "CreateApasResponse", "Error", ex.ToString());
            }

            return _TransmissionDataUUID;
        }

        public Library.Apas.AcademicRecord.SourceDestinationType BuildSourceDestinationType(Institution institution, bool source = false)
        {
            Library.Apas.AcademicRecord.SourceDestinationType _SourceDestinationType = new Library.Apas.AcademicRecord.SourceDestinationType();
            Library.Apas.AcademicRecord.ContactsType _ContactsType = new Library.Apas.AcademicRecord.ContactsType();

            try
            {
                _SourceDestinationType = new Library.Apas.AcademicRecord.SourceDestinationType()
                {
                    Organization = new Library.Apas.AcademicRecord.OrganizationType()
                    {
                        APAS = institution.LocalOrganizationID,

                        LocalOrganizationID = new Library.Apas.CoreMain.LocalOrganizationIDType()
                        {
                            LocalOrganizationIDCode = institution.LocalOrganizationID,
                            LocalOrganizationIDQualifier = GetInstitutionIDQualifier(institution, Library.Apas.CoreMain.LocalOrganizationIDCodeQualifierType.ZZ),
                        },

                        OrganizationName = new List<string>() { GetInstitutionName(institution.InstitutionId) },

                        Contacts = source ? BuildInstitutionContactsType(institution) : null,
                    }
                };
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildSourceDestinationType", "Error", ex.ToString());
            }
            return _SourceDestinationType;
        }

        public Library.Apas.AcademicRecord.ResponseType[] BuildResponses(CreateMsgViewObj responseObj)
        {
            List<Library.Apas.AcademicRecord.ResponseType> _Responses = new List<Library.Apas.AcademicRecord.ResponseType>();

            try
            {
                // Get related request
                Request _Request = GetRequestByTransDataId(responseObj.TransDataID);

                if (_Request != null)
                {
                    foreach (Recipient recipient in _Request.Recipients)
                    {
                        _Responses.Add(BuildResponseType(recipient, responseObj));
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildResponses", "Error", ex.ToString());
            }

            return _Responses.ToArray();
        }

        public Library.Apas.AcademicRecord.ResponseType BuildResponseType(Recipient recipient, CreateMsgViewObj responseObj)
        {
            Library.Apas.AcademicRecord.ResponseType _Response = new Library.Apas.AcademicRecord.ResponseType();

            try
            {
                _Response = new Library.Apas.AcademicRecord.ResponseType()
                {
                    CreatedDateTime = DateTime.Now,
                    RequestTrackingID = recipient.RequestTrackingID ?? recipient.Request.TransmissionData.RequestTrackingID ?? "NotFound",
                    RequestedStudent = BuildRequestedStudentType(recipient, responseObj.StudentRecord),
                };

                // Get Response Status provided on the create message view
                _Response.ResponseStatus = responseObj.ResponseStatusType ?? Library.Apas.AcademicRecord.ResponseStatusType.NoRecord;  // Default Value
                if (responseObj.DestinationDetails != null && responseObj.DestinationDetails.Any() && responseObj.DestinationDetails.FirstOrDefault().ResponseStatusType != null)
                {
                    if (responseObj.DestinationDetails.FirstOrDefault().ResponseStatusType != null)
                    {
                        _Response.ResponseStatus = (Library.Apas.AcademicRecord.ResponseStatusType)responseObj.DestinationDetails.FirstOrDefault().ResponseStatusType;
                    }

                    // Response Status Detail/Comments
                    if (!string.IsNullOrWhiteSpace(responseObj.DestinationDetails.FirstOrDefault().HoldTypeData))
                    {
                        _Response.NoteMessage = new List<string>() { responseObj.DestinationDetails.FirstOrDefault().HoldTypeData };
                    }
                }
                else
                {
                    if (recipient.ResponseStatuses != null && recipient.ResponseStatuses.Any())
                    {
                        _Response.ResponseStatus = (Library.Apas.AcademicRecord.ResponseStatusType)recipient.ResponseStatuses.OrderByDescending(x => x.CreatedDateTime).Select(y => y.ResponseStatusType).FirstOrDefault();
                    }
                }

                // Update Attendance with Response Status
                if (_Response.RequestedStudent.Attendance != null && _Response.RequestedStudent.Attendance.Count() > 0)
                {
                    _Response.RequestedStudent.Attendance.FirstOrDefault().School.OrganizationName = _Response.ResponseStatus.ToString();
                }

                // Hold Reason
                if (responseObj.HoldReasonType != null)
                {
                    _Response.ResponseHold = new List<Library.Apas.AcademicRecord.ResponseHoldType>() {
                        new Library.Apas.AcademicRecord.ResponseHoldType()
                        {
                            HoldReason = (Library.Apas.AcademicRecord.HoldReasonType)responseObj.HoldReasonType,
                        },
                    };
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildResponseType", "Error", ex.ToString());
            }

            return _Response;
        }

        public List<Library.Apas.AcademicRecord.TransmissionDataType> BuildTransmissionDatas(CreateMsgViewObj responseObj)
        {
            List<Library.Apas.AcademicRecord.TransmissionDataType> _TransmissionDatas = new List<Library.Apas.AcademicRecord.TransmissionDataType>();

            try
            {
                Library.Apas.AcademicRecord.SourceDestinationType _Source = BuildSourceDestinationType(GetInstByLOrgID(GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode)), true);

                foreach (DestInstObj dest in responseObj.DestinationDetails)
                {
                    _TransmissionDatas.Add(new Library.Apas.AcademicRecord.TransmissionDataType()
                    {
                        DocumentID = Functions.GetNewLcDocumentID(),
                        CreatedDateTime = DateTime.Now,

                        RequestTrackingID = responseObj.RequestTrackingId ?? Functions.GetNewRequestTrackingId(),

                        TransmissionType = Library.Apas.CoreMain.TransmissionTypeType.Original,
                        DocumentCompleteCode = Library.Apas.CoreMain.DocumentCompleteCodeType.Complete,
                        DocumentCompleteCodeSpecified = true,
                        DocumentOfficialCode = Library.Apas.CoreMain.DocumentOfficialCodeType.Official,
                        DocumentOfficialCodeSpecified = true,
                        DocumentProcessCode = Functions.GetProcessCode(),
                        DocumentProcessCodeSpecified = true,
                        DocumentTypeCode = Library.Apas.CoreMain.DocumentTypeCodeType.Response,
                        Source = _Source,
                        Destination = BuildSourceDestinationType(GetInstitution(dest.Destination.InstitutionID.ToString())),
                    });
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildTransmissionDatas", "Error", ex.ToString());
            }

            return _TransmissionDatas;
        }

        public Library.Apas.AcademicRecord.StudentType BuildStudentType(StudentRecordObj student)
        {
            Library.Apas.AcademicRecord.StudentType _StudentType = new Library.Apas.AcademicRecord.StudentType();

            try
            {
                _StudentType = new Library.Apas.AcademicRecord.StudentType()
                {
                    //AcademicRecord = BuildAcademicRecords(student),
                    //Health = BuildHealth(student),
                    Person = BuildPersonType(student),
                    //Tests = BuildTests(student),
                    //NoteMessage = AddNoteMessage("booh!"),
                };
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildStudentType", "Error", ex.ToString());
            }
            return _StudentType;
        }

        public Library.Apas.AcademicRecord.RequestedStudentType BuildRequestedStudentType(Recipient recipient, StudentRecordObj studentRecord)
        {
            Library.Apas.AcademicRecord.RequestedStudentType _RequestedStudentType = new Library.Apas.AcademicRecord.RequestedStudentType();

            try
            {
                // Load StudentRecordObj from recipient student
                LoadStudentRecordObj(recipient.Request.RequestedStudent, ref studentRecord);

                _RequestedStudentType = new Library.Apas.AcademicRecord.RequestedStudentType()
                {
                    Person = BuildPersonType(studentRecord),

                    Attendance = new List<Library.Apas.AcademicRecord.AttendanceType>(), //BuildAttendanceTypes(recipient.Request.RequestedStudent),  // Don't include Academic Hystory (Attendance) in our responses

                    ReleaseAuthorizedIndicator = true,

                    //ReleaseAuthorizedMethod = Library.Apas.AcademicRecord.ReleaseAuthorizedMethodType.LegitimateEducationalInterest,
                    //ReleaseAuthorizedMethodSpecified = true,

                    //FeeDiscountRequestCode = Library.Apas.AcademicRecord.FeeDiscountRequestCodeType.FinancialHardship,
                    //FeeDiscountRequestCodeSpecified = true,
                    //UpdateContactsInformation = Library.Apas.AcademicRecord.UpdateContactsInformationType.UpdateContacts,
                    //UpdateContactsInformationSpecified = true,
                };

                // Atendance is required field, if null include Request Destination Institution info
                if (_RequestedStudentType.Attendance.Count() <= 0)
                {
                    _RequestedStudentType.Attendance.Add(
                        new Library.Apas.AcademicRecord.AttendanceType()
                        {
                            School = new Library.Apas.AcademicRecord.SchoolType()
                            {
                                OrganizationName = "     ",// recipient.Request.TransmissionData.DestinationInstitution.InstitutionNames.OrderByDescending(x => x.ModifiedDateTime).Select(y => y.Name).FirstOrDefault(),
                            },
                        }
                    );
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildRequestedStudentType", "Error", ex.ToString());
            }
            return _RequestedStudentType;
        }

        private void LoadStudentRecordObj(Student student, ref StudentRecordObj studentRecord)
        {
            try
            {
                // ASN
                if (string.IsNullOrWhiteSpace(studentRecord.ASN)) { studentRecord.ASN = student.ASNs.OrderByDescending(x => x.ModifiedDateTime).Select(y => y.AgencyAssignedID).FirstOrDefault(); }

                // Birth
                if (studentRecord.BirthDate == null) { studentRecord.BirthDate = student.Person.BirthDate; }

                // Name
                PersonName personName = student.Person.Names.Where(z => z.NameType == Structs.Name.PersonalNameType).OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                if (personName != null)
                {
                    if (string.IsNullOrWhiteSpace(studentRecord.FirstName)) { studentRecord.FirstName = personName.FirstName; }
                    if (string.IsNullOrWhiteSpace(studentRecord.MiddleName)) { studentRecord.MiddleName = personName.MiddleNames; }
                    if (string.IsNullOrWhiteSpace(studentRecord.LastName)) { studentRecord.LastName = personName.LastName; }
                }

                // Alternate/Former Names
                if (!studentRecord.FormerNames.Any())
                {
                    List<PersonName> formerNames = student.Person.Names.Where(z => z.NameType == Structs.Name.FormerType).OrderByDescending(x => x.ModifiedDateTime).ToList();
                    foreach (PersonName name in formerNames)
                    {
                        StuNameObj stuName = new StuNameObj()
                        {
                            FirstName = name.FirstName,
                            MiddleName = name.MiddleNames,
                            LastName = name.LastName,
                        };
                        studentRecord.FormerNames.Add(stuName);
                    }
                }

                // Contact
                Address address = student.Person.Addresses.Where(x => x.AddressType == Structs.Address.PermanentAddressType).OrderByDescending(y => y.ModifiedDateTime).FirstOrDefault();
                if (address != null)
                {
                    if (string.IsNullOrWhiteSpace(studentRecord.Addr1)) { studentRecord.Addr1 = address.AddressLine1.CleanAddressLine(); }
                    if (string.IsNullOrWhiteSpace(studentRecord.Addr2)) { studentRecord.Addr2 = address.AddressLine2.CleanAddressLine(); }
                    if (string.IsNullOrWhiteSpace(studentRecord.City)) { studentRecord.City = address.City; }
                    if (string.IsNullOrWhiteSpace(studentRecord.State)) { studentRecord.State = address.Province; }
                    if (string.IsNullOrWhiteSpace(studentRecord.Zip)) { studentRecord.Zip = address.PostalCode; }
                    if (string.IsNullOrWhiteSpace(studentRecord.Country)) { studentRecord.Country = address.Country; }
                }

                Phone phone = student.Person.Phones.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(studentRecord.Phone) && phone != null) { studentRecord.Phone = Functions.FixDomesticPhone(phone.PhoneNumber, phone.AreaCode); }

                Email email = student.Person.Emails.Where(x => x.EmailType == Structs.Email.PrimaryEmailType).OrderByDescending(y => y.ModifiedDateTime).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(studentRecord.Email) && email != null) { studentRecord.Email = email.EmailAddress; }

                // Gender
                if (string.IsNullOrWhiteSpace(studentRecord.Gender)) { studentRecord.GenderCode = student.Person.Genders.OrderByDescending(x => x.ModifiedDateTime).Select(y => y.GenderCodeType).FirstOrDefault(); }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "LoadStudentRecordObj", "Error", ex.ToString());
            }
        }

        public List<Library.Apas.AcademicRecord.AttendanceType> BuildAttendanceTypes(Student student)
        {
            List<Library.Apas.AcademicRecord.AttendanceType> _AttendanceTypes = new List<Library.Apas.AcademicRecord.AttendanceType>();

            try
            {
                foreach (AcademicRecord academicRecord in student.AcademicRecords.Where(a => a.School != null).OrderByDescending(x => x.CreatedDateTime))
                {
                    _AttendanceTypes.Add(BuildAttendanceType(academicRecord));
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildAttendanceTypes", "Error", ex.ToString());
            }
            return _AttendanceTypes;
        }

        public Library.Apas.AcademicRecord.AttendanceType BuildAttendanceType(AcademicRecord academicRecord)
        {
            Library.Apas.AcademicRecord.AttendanceType _AttendanceType = new Library.Apas.AcademicRecord.AttendanceType();

            try
            {
                _AttendanceType = new Library.Apas.AcademicRecord.AttendanceType()
                {
                    School = BuildSchoolType(academicRecord),
                    AcademicAwardsReported = BuildAcademicAwardsReportedTypes(academicRecord),
                    CurrentEnrollmentIndicator = true,
                    CurrentEnrollmentIndicatorSpecified = true,
                };

                if (academicRecord.FirstDateAttended.HasValue)
                {
                    _AttendanceType.EnrollDate = academicRecord.FirstDateAttended.Value;
                    _AttendanceType.EnrollDateSpecified = true;
                }

                if (academicRecord.LastDateAttended.HasValue)
                {
                    _AttendanceType.ExitDate = academicRecord.LastDateAttended.Value;
                    _AttendanceType.ExitDateSpecified = true;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildAttendanceType", "Error", ex.ToString());
            }
            return _AttendanceType;
        }

        public List<Library.Apas.AcademicRecord.AcademicRecordType> BuildAcademicRecords(StudentRecordObj student)
        {

            List<Library.Apas.AcademicRecord.AcademicRecordType> _AcademicRecords = new List<Library.Apas.AcademicRecord.AcademicRecordType>();

            try
            {
                List<StuGradesObj> _StudentGrades = new List<StuGradesObj>();
                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    _StudentGrades = collLogic.GetStudentGrades(student.Snumber, "PS");
                }

                var currentYear = Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2));
                var grades = _StudentGrades.Where(x => Convert.ToInt32(x.CRS_TERM.Substring(0, 2)) > currentYear)
                                .OrderBy(x => x.CRS_TERM.Substring(0, 2))
                                .ThenByDescending(y => y.CRS_TERM.Substring(2, 2) == "WN")
                                .ThenByDescending(y => y.CRS_TERM.Substring(2, 2) == "SM")
                                .ThenByDescending(y => y.CRS_TERM.Substring(2, 2) == "SM1")
                                .ThenByDescending(y => y.CRS_TERM.Substring(2, 2) == "SM2")
                                .ThenByDescending(y => y.CRS_TERM.Substring(2, 2) == "FL")
                                .Union(_StudentGrades.Where(x => Convert.ToInt32(x.CRS_TERM.Substring(0, 2)) <= currentYear)
                                        .OrderBy(x => x.CRS_TERM.Substring(0, 2))
                                        .ThenByDescending(y => y.CRS_TERM.Substring(2, 2) == "WN")
                                        .ThenByDescending(y => y.CRS_TERM.Substring(2, 2) == "SM")
                                        .ThenByDescending(y => y.CRS_TERM.Substring(2, 2) == "SM1")
                                        .ThenByDescending(y => y.CRS_TERM.Substring(2, 2) == "SM2")
                                        .ThenByDescending(y => y.CRS_TERM.Substring(2, 2) == "FL")
                                );

                bool first_iteration = true;
                string selected_term = string.Empty;

                List<Library.Apas.AcademicRecord.AcademicAwardType> _AcademicAwards = new List<Library.Apas.AcademicRecord.AcademicAwardType>();
                List<Library.Apas.AcademicRecord.AcademicSessionType> _AcademicSessions = new List<Library.Apas.AcademicRecord.AcademicSessionType>();

                if (grades.Any()) { selected_term = grades.FirstOrDefault().CRS_TERM; }

                List<StuGradesObj> _SessionGrades = new List<StuGradesObj>();

                foreach (StuGradesObj grade in grades)
                {
                    if (first_iteration)
                    {
                        first_iteration = false;
                        _AcademicAwards = BuildAcademicAwards(student);
                    }
                    else
                    {
                        if (selected_term != grade.CRS_TERM)
                        {
                            selected_term = grade.CRS_TERM;
                            _AcademicSessions.Add(BuildAcademicSession(student, _SessionGrades));
                            _SessionGrades = new List<StuGradesObj>();

                        }
                    }
                    _SessionGrades.Add(grade);
                }
                _AcademicRecords.Add(new Library.Apas.AcademicRecord.AcademicRecordType()
                {
                    AcademicAward = _AcademicAwards,
                    AcademicSession = _AcademicSessions,
                    // if we have a different school, that would great a new academic record?
                });

            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return _AcademicRecords;
        }

        public List<Library.Apas.AcademicRecord.AcademicAwardType> BuildAcademicAwards(StudentRecordObj student)
        {

            List<Library.Apas.AcademicRecord.AcademicAwardType> _AcademicAwards = new List<Library.Apas.AcademicRecord.AcademicAwardType>();

            try
            {
                //_AcademicAwards = new List<Library.Apas.AcademicRecord.AcademicAwardType>() {
                //             new Library.Apas.AcademicRecord.AcademicAwardType() {
                //                 AcademicAwardDate = DateTime.Now,
                //                 AcademicAwardDateSpecified = true,
                //                 AcademicAwardLevel = new Library.Apas.CoreMain.AcademicAwardLevelType() {

                //                 },
                //                  AcademicAwardLevelSpecified = true,
                //                 AcademicAwardProgram = new List<Library.Apas.AcademicRecord.AcademicProgramType>() {
                //                     new Library.Apas.AcademicRecord.AcademicProgramType() {
                //                         AcademicProgramName = string.Empty,
                //                         AcademicProgramTypeSpecified = true,
                //                         AcademicProgramTypeType = new Library.Apas.CoreMain.AcademicProgramTypeType() {

                //                         },
                //                         ProgramESISCode = string.Empty,
                //                         ProgramSummary = new Library.Apas.AcademicRecord.AcademicSummaryE2Type() {
                //                             AcademicHonors = new Library.Apas.CoreMain.AcademicHonorsType() {
                //                                 HonorsLevel = Library.Apas.CoreMain.HonorsRecognitionLevelType.FirstHighest,
                //                                 HonorsLevelSpecified = true,
                //                                 HonorsTitle = string.Empty,
                //                             },
                //                             AcademicSummaryLevel = Library.Apas.CoreMain.AcademicSummaryLevelType.Undergraduate,
                //                             AcademicSummaryLevelSpecified = true,
                //                             AcademicSummaryType = Library.Apas.CoreMain.AcademicSummaryTypeType.All,
                //                             AcademicSummaryTypeSpecified = true,
                //                             ClassRank = string.Empty,
                //                             ClassSize = string.Empty,
                //                             Delinquencies = new List<Library.Apas.CoreMain.DelinquenciesType>() {
                //                                 Library.Apas.CoreMain.DelinquenciesType.GoodStanding,
                //                             },
                //                             ExitDate = DateTime.Now,
                //                             ExitDateSpecified = true,
                //                             GPA = new Library.Apas.CoreMain.GPAType() {
                //                                 CreditHoursAttempted = 1,
                //                                 CreditHoursAttemptedSpecified = true,
                //                                 CreditHoursEarned = 1,
                //                                 CreditHoursEarnedSpecified = true,
                //                                 CreditHoursforGPA = 1,
                //                                 CreditHoursforGPASpecified = true,
                //                                 CreditUnit = Library.Apas.CoreMain.CourseCreditUnitsType.CarnegieUnits,
                //                                 CreditUnitSpecified = true,
                //                                 GPARangeMaximum = 1,
                //                                 GPARangeMaximumSpecified = true,
                //                                 GPARangeMinimum = 1,
                //                                 GPARangeMinimumSpecified = true,
                //                                 GradePointAverage = 1,
                //                                 GradePointAverageSpecified = true,
                //                                 TotalQualityPoints = 1,
                //                                 TotalQualityPointsSpecified = true,
                //                             }
                //                         },
                //                         ProgramCIPCode = string.Empty,
                //                         ProgramCSISCode = string.Empty,
                //                         ProgramHEGISCode = string.Empty,
                //                         ProgramUSISCode = string.Empty
                //                     }
                //                 }
                //             }
                //         };
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return _AcademicAwards;
        }

        public List<Library.Apas.AcademicRecord.AcademicProgramType> BuildAcademicPrograms(StudentRecordObj student)
        {
            List<Library.Apas.AcademicRecord.AcademicProgramType> _AcademicPrograms = new List<Library.Apas.AcademicRecord.AcademicProgramType>();

            try
            {
                //_AcademicPrograms = new List<Library.Apas.AcademicRecord.AcademicProgramType>() {
                //    new Library.Apas.AcademicRecord.AcademicProgramType()
                //    {
                //        AcademicProgramName = string.Empty,
                //        AcademicProgramTypeSpecified = true,
                //        AcademicProgramTypeType = new Library.Apas.CoreMain.AcademicProgramTypeType()
                //        {

                //        },
                //        ProgramESISCode = string.Empty,
                //        ProgramSummary = new Library.Apas.AcademicRecord.AcademicSummaryE2Type()
                //        {
                //            AcademicHonors = new Library.Apas.CoreMain.AcademicHonorsType()
                //            {
                //                HonorsLevel = Library.Apas.CoreMain.HonorsRecognitionLevelType.FirstHighest,
                //                HonorsLevelSpecified = true,
                //                HonorsTitle = string.Empty,
                //            },
                //            AcademicSummaryLevel = Library.Apas.CoreMain.AcademicSummaryLevelType.Undergraduate,
                //            AcademicSummaryLevelSpecified = true,
                //            AcademicSummaryType = Library.Apas.CoreMain.AcademicSummaryTypeType.All,
                //            AcademicSummaryTypeSpecified = true,
                //            ClassRank = string.Empty,
                //            ClassSize = string.Empty,
                //            Delinquencies = new List<Library.Apas.CoreMain.DelinquenciesType>() {
                //                                 Library.Apas.CoreMain.DelinquenciesType.GoodStanding,
                //                             },
                //            ExitDate = DateTime.Now,
                //            ExitDateSpecified = true,
                //            GPA = new Library.Apas.CoreMain.GPAType()
                //            {
                //                CreditHoursAttempted = 1,
                //                CreditHoursAttemptedSpecified = true,
                //                CreditHoursEarned = 1,
                //                CreditHoursEarnedSpecified = true,
                //                CreditHoursforGPA = 1,
                //                CreditHoursforGPASpecified = true,
                //                CreditUnit = Library.Apas.CoreMain.CourseCreditUnitsType.CarnegieUnits,
                //                CreditUnitSpecified = true,
                //                GPARangeMaximum = 1,
                //                GPARangeMaximumSpecified = true,
                //                GPARangeMinimum = 1,
                //                GPARangeMinimumSpecified = true,
                //                GradePointAverage = 1,
                //                GradePointAverageSpecified = true,
                //                TotalQualityPoints = 1,
                //                TotalQualityPointsSpecified = true,
                //            }
                //        },
                //        ProgramCIPCode = string.Empty,
                //        ProgramCSISCode = string.Empty,
                //        ProgramHEGISCode = string.Empty,
                //        ProgramUSISCode = string.Empty
                //    },
                //};
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return _AcademicPrograms;
        }

        public List<Library.Apas.AcademicRecord.AcademicSummaryFType> BuildAcademicSummaries(IEnumerable<StuGradesObj> grades)
        {
            List<Library.Apas.AcademicRecord.AcademicSummaryFType> _AcademicSummaries = new List<Library.Apas.AcademicRecord.AcademicSummaryFType>();

            try
            {
                _AcademicSummaries = new List<Library.Apas.AcademicRecord.AcademicSummaryFType>() {
                new Library.Apas.AcademicRecord.AcademicSummaryFType()
                {
                    GPA = new Library.Apas.CoreMain.GPAType()
                    {
                        CreditHoursAttempted = (decimal)grades.FirstOrDefault().CRS_ATT_CRD,
                        CreditUnit = Library.Apas.CoreMain.CourseCreditUnitsType.Semester,
                    }
                }
                };
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return _AcademicSummaries;
        }

        public Library.Apas.AcademicRecord.PersonType BuildPersonType(StudentRecordObj student)
        {
            Library.Apas.AcademicRecord.PersonType _PersonType = new Library.Apas.AcademicRecord.PersonType();

            try
            {
                //List<Library.Apas.AcademicRecord.ContactsType> contactTypes = BuildContacts(student);  // Don't include student address in our Transcripts
                List<Library.Apas.CoreMain.AgencyIdentifierType> agencyIdentifiers = BuildAgencyIdentifierType(student);
                List<Library.Apas.CoreMain.NameType> alternateNames = BuildAlternateNameType(student);

                Library.Apas.CoreMain.NameType nameType = new Library.Apas.CoreMain.NameType()
                {
                    FirstName = student.FirstName,
                    MiddleName = (student.MiddleName != null && !string.IsNullOrWhiteSpace(student.MiddleName.Trim())) ? new List<string>() { student.MiddleName.Trim() } : null,
                    LastName = student.LastName,
                };

                Library.Apas.CoreMain.BirthType birthType = new Library.Apas.CoreMain.BirthType();
                if (student.BirthDate != null)
                {
                    if (DateTime.TryParse(student.BirthDate.ToString(), out DateTime dte))
                    {
                        birthType.BirthDate = dte;
                        birthType.BirthDateSpecified = true;
                    }
                    else {
                        birthType.BirthDate = DateTime.Parse("1900-01-01");
                    }
                }

                Library.Apas.CoreMain.GenderType genderType = null;
                if (!string.IsNullOrWhiteSpace(student.Gender))
                {
                    genderType = new Library.Apas.CoreMain.GenderType();
                    genderType.GenderCode = (Library.Apas.CoreMain.GenderCodeType)Enum.Parse(typeof(Library.Apas.CoreMain.GenderCodeType), student.Gender);
                }

                _PersonType = new Library.Apas.AcademicRecord.PersonType()
                {
                    SchoolAssignedPersonID = !string.IsNullOrWhiteSpace(student.Snumber) ? student.Snumber.Trim() : null,
                    Name = nameType,
                    Birth = birthType,
                    Gender = genderType,
                    //Contacts = contactTypes,
                    AgencyIdentifier = agencyIdentifiers,
                    AlternateName = alternateNames,
                };
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildPersonType", "Error", ex.ToString());
            }

            return _PersonType;
        }

        public Library.Apas.AcademicRecord.K12PersonType BuildK12PersonType(StudentRecordObj student)
        {
            Library.Apas.AcademicRecord.K12PersonType _PersonType = new Library.Apas.AcademicRecord.K12PersonType();

            try
            {
                _PersonType = new Library.Apas.AcademicRecord.K12PersonType()
                {
                    AlternateName = new List<Library.Apas.CoreMain.NameType>() {
                                 new Library.Apas.CoreMain.NameType() {
                                     FirstName = "",
                                     LastName = "",
                                 }
                             },
                    Birth = new Library.Apas.AcademicRecord.BirthType()
                    {
                        BirthCity = "",
                        BirthCountry = Library.Apas.CoreMain.CountryCodeType.CA,
                        BirthCountrySpecified = true,
                        BirthDate = DateTime.Now,
                        BirthDateSpecified = true,
                        Birthday = "",
                        BirthState = Library.Apas.CoreMain.StateProvinceCodeType.AB,
                        BirthStateSpecified = true,
                    },
                    Contacts = BuildContacts(student),
                    Deceased = new Library.Apas.CoreMain.DeceasedType()
                    {
                        DeceasedIndicator = false,
                    },
                    Gender = new Library.Apas.CoreMain.GenderType()
                    {
                        GenderCode = Library.Apas.CoreMain.GenderCodeType.Unreported
                    },
                    ParentGuardianName = new Library.Apas.CoreMain.NameType()
                    {

                    },
                    Name = BuildNameType(student),
                    RecipientAssignedID = string.Empty,
                    SIN = string.Empty,
                    SchoolAssignedPersonID = string.Empty,
                    RaceEthnicity = new Library.Apas.CoreMain.RaceEthnicityType()
                    {
                        LocalRaceEthnicityCode = new List<string>() {
                             Library.Apas.CoreMain.RaceEthnicityCodeType.Asian.ToString(),
                         }
                    }
                };
                _PersonType.AgencyIdentifier.Add(new Library.Apas.CoreMain.AgencyIdentifierType()
                {
                    AgencyAssignedID = "",
                });
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return _PersonType;
        }

        public Library.Apas.AcademicRecord.HighSchoolType BuildHighSchool(Student student)
        {
            Library.Apas.AcademicRecord.HighSchoolType _HighSchool = new Library.Apas.AcademicRecord.HighSchoolType();

            try
            {
                _HighSchool = new Library.Apas.AcademicRecord.HighSchoolType()
                {

                };
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                throw;
            }
            return _HighSchool;
        }

        public Library.Apas.AcademicRecord.SchoolType BuildSchoolType(AcademicRecord academicRecord)
        {
            Library.Apas.AcademicRecord.SchoolType _SchoolType = null;
            string _OrganizationName = string.Empty;
            try
            {
                if (academicRecord != null && academicRecord.School != null && academicRecord.School.InstitutionNames != null && academicRecord.School.InstitutionNames.Count() > 0)
                {
                    _OrganizationName = academicRecord.School.InstitutionNames.OrderByDescending(x => x.ModifiedDateTime).Select(y => y.Name).FirstOrDefault();

                    if (!string.IsNullOrWhiteSpace(_OrganizationName))
                    {
                        _SchoolType = new Library.Apas.AcademicRecord.SchoolType()
                        {
                            OrganizationName = _OrganizationName
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildSchoolType", "Error", ex.ToString());
            }
            return _SchoolType;
        }

        public Library.Apas.AcademicRecord.ResidencyType1 BuildResidencyType1(StudentRecordObj student)
        {
            Library.Apas.AcademicRecord.ResidencyType1 _Residency = new Library.Apas.AcademicRecord.ResidencyType1();

            try
            {
                _Residency = new Library.Apas.AcademicRecord.ResidencyType1()
                {
                    Country = string.Empty,
                    CountryCode = Library.Apas.CoreMain.CountryCodeType.CA,
                    CountryCodeSpecified = true,
                    County = string.Empty,
                    CountyCode = string.Empty,
                    ResidencyStatusCode = Library.Apas.CoreMain.ResidencyStatusCodeType.Resident,
                    ResidencyStatusCodeSpecified = true,
                    StateProvince = "Alberta",
                    StateProvinceCode = Library.Apas.CoreMain.StateProvinceCodeType.AB,
                    StateProvinceCodeSpecified = true,
                };
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                throw;
            }
            return _Residency;
        }

        public Library.Apas.CoreMain.HealthType BuildHealth(StudentRecordObj student)
        {
            Library.Apas.CoreMain.HealthType _HealthType = new Library.Apas.CoreMain.HealthType();

            try
            {
                _HealthType = new Library.Apas.CoreMain.HealthType()
                {

                };
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return _HealthType;
        }

        public List<Library.Apas.CoreMain.TestsType> BuildTests(StudentRecordObj student)
        {
            List<Library.Apas.CoreMain.TestsType> _TestsType = new List<Library.Apas.CoreMain.TestsType>();

            try
            {
                _TestsType.Add(BuildTest(student));
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return _TestsType;
        }

        public Library.Apas.CoreMain.TestsType BuildTest(StudentRecordObj student)
        {
            Library.Apas.CoreMain.TestsType _TestsType = new Library.Apas.CoreMain.TestsType();

            try
            {
                _TestsType = new Library.Apas.CoreMain.TestsType()
                {

                };
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return _TestsType;
        }

        public Library.Apas.CoreMain.NameType BuildNameType(StudentRecordObj student)
        {
            Library.Apas.CoreMain.NameType _NameType = new Library.Apas.CoreMain.NameType();

            try
            {
                _NameType = new Library.Apas.CoreMain.NameType()
                {
                    FirstName = "",
                    LastName = "",
                };
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }
            return _NameType;
        }

        public List<Library.Apas.AcademicRecord.ContactsType> BuildContacts(StudentRecordObj student)
        {
            List<Library.Apas.AcademicRecord.ContactsType> _Contacts = new List<Library.Apas.AcademicRecord.ContactsType>();

            try
            {
                Library.Apas.AcademicRecord.ContactsType _Contact = BuildContactsType(student);
                if (_Contact != null)
                {
                    _Contacts.Add(_Contact);
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildContacts", "Error", ex.ToString());
            }
            return _Contacts;
        }

        public Library.Apas.AcademicRecord.ContactsType BuildContactsType(StudentRecordObj student)
        {
            bool success = false;
            Library.Apas.AcademicRecord.ContactsType _Contact = new Library.Apas.AcademicRecord.ContactsType();

            try
            {
                // Address
                List<Library.Apas.AcademicRecord.AddressType1> _Addresses = new List<Library.Apas.AcademicRecord.AddressType1>();
                Library.Apas.AcademicRecord.AddressType1 _Address = new Library.Apas.AcademicRecord.AddressType1();
                bool hasAddress = false;
                if (!string.IsNullOrWhiteSpace(student.Addr1)) { _Address.AddressLine = new List<string>() { student.Addr1 }; hasAddress = true; }
                if (!string.IsNullOrWhiteSpace(student.Addr2)) {
                    if (_Address.AddressLine == null) {
                        _Address.AddressLine = new List<string>() { student.Addr2 };
                    } else {
                        _Address.AddressLine.Add(student.Addr2);
                    }
                    hasAddress = true;
                }
                if (!string.IsNullOrWhiteSpace(student.City)) { _Address.City = student.City; hasAddress = true; }
                if (!string.IsNullOrWhiteSpace(student.State) || !string.IsNullOrWhiteSpace(student.Zip) || !string.IsNullOrWhiteSpace(student.Country))
                {
                    List<Library.Apas.CoreMain.ItemsChoiceType> _ItemsElementName = new List<Library.Apas.CoreMain.ItemsChoiceType>() {
                                                                                        Library.Apas.CoreMain.ItemsChoiceType.StateProvince,
                                                                                        Library.Apas.CoreMain.ItemsChoiceType.PostalCode,
                                                                                        Library.Apas.CoreMain.ItemsChoiceType.CountryCode,
                                                                                    };
                    List<object> _Items = new List<object>() {
                                                student.State,
                                                student.Zip,
                                                (Library.Apas.CoreMain.CountryCodeType)Enum.Parse(typeof(Library.Apas.CoreMain.CountryCodeType), student.Country == "XCA" ? "CA": student.Country),
                                            };

                    // If address is not in Canada, add the country code
                    //if (!string.IsNullOrWhiteSpace(student.Country) && !student.Country.Contains("CA"))
                    //{
                    //    _ItemsElementName.Add(Library.Apas.CoreMain.ItemsChoiceType.CountryCode);
                    //    _Items.Add(student.Country);
                    //}

                    _Address.ItemsElementName = _ItemsElementName.ToArray();
                    _Address.Items = _Items.ToArray();

                    //_Address.ItemsElementName = new List<Library.Apas.CoreMain.ItemsChoiceType>() {
                    //    Library.Apas.CoreMain.ItemsChoiceType.StateProvince,
                    //    Library.Apas.CoreMain.ItemsChoiceType.PostalCode,
                    //    Library.Apas.CoreMain.ItemsChoiceType.CountryCode,
                    //}.ToArray();

                    //_Address.Items = new List<object>() {
                    //    student.State,
                    //    student.Zip,
                    //    student.Country == "XCA" ? "CA": student.Country,
                    //}.ToArray();

                    hasAddress = true;
                }
                if (hasAddress)
                {
                    _Addresses.Add(_Address);
                    _Contact.Address = _Addresses;
                    success = true;
                }

                // Email
                List<Library.Apas.AcademicRecord.EmailType1> _Emails = new List<Library.Apas.AcademicRecord.EmailType1>();
                Library.Apas.AcademicRecord.EmailType1 _Email = new Library.Apas.AcademicRecord.EmailType1();
                if (!string.IsNullOrWhiteSpace(student.Email)) { _Email.EmailAddress = student.Email; }
                if (!string.IsNullOrWhiteSpace(_Email.EmailAddress)) {
                    _Emails.Add(_Email);
                    _Contact.Email = _Emails;
                    success = true;
                }

                // Phone
                List<Library.Apas.CoreMain.PhoneType> _Phones = new List<Library.Apas.CoreMain.PhoneType>();
                Library.Apas.CoreMain.PhoneType _Phone = new Library.Apas.CoreMain.PhoneType();
                if (!string.IsNullOrWhiteSpace(student.Phone)) {
                    string[] parsedPhone = Functions.SeparatePhoneNumbers(student.Phone);
                    _Phone.CountryPrefixCode = !string.IsNullOrWhiteSpace(parsedPhone[0]) ? parsedPhone[0].Trim() : null;
                    _Phone.AreaCityCode = !string.IsNullOrWhiteSpace(parsedPhone[1]) ? parsedPhone[1].Trim() : null;
                    _Phone.PhoneNumber = !string.IsNullOrWhiteSpace(parsedPhone[2]) ? parsedPhone[2].Trim() : null;
                }
                if (!string.IsNullOrWhiteSpace(_Phone.PhoneNumber)) {
                    _Phones.Add(_Phone);
                    _Contact.Phone = _Phones;
                    success = true;
                }

                // Empty contact information
                if (!success) { _Contact = null; }

                //_Contact = new Library.Apas.AcademicRecord.ContactsType()
                //{
                //    Address = new List<Library.Apas.AcademicRecord.AddressType1>() {
                //                        new Library.Apas.AcademicRecord.AddressType1() {
                //                            AddressLine = new List<string>() {
                //                                student.Addr1,
                //                                student.Addr2,
                //                            },
                //                            City = student.City,
                //                            // TODO: Fails when serializing object
                //                            // (Error: "Value of ItemsElementName mismatches the type of Library.Apas.CoreMain.CountryCodeType; you need to set it to Library.Apas.CoreMain.ItemsChoiceType.@CountryCode.")
                //                            //ItemsElementName = new List<Library.Apas.CoreMain.ItemsChoiceType>() {
                //                            //    Library.Apas.CoreMain.ItemsChoiceType.CountryCode,
                //                            //    Library.Apas.CoreMain.ItemsChoiceType.StateProvinceCode,
                //                            //    Library.Apas.CoreMain.ItemsChoiceType.PostalCode,
                //                            //}.ToArray(),
                //                            //Items = new List<object>() {
                //                            //    student.Country,
                //                            //    student.State,
                //                            //    student.Zip,
                //                            //}.ToArray(),
                //                        }
                //                    },
                //    Email = new List<Library.Apas.AcademicRecord.EmailType1>() {
                //                        new Library.Apas.AcademicRecord.EmailType1() {
                //                            EmailAddress = student.Email,
                //                        }
                //                    },
                //    Phone = new List<Library.Apas.CoreMain.PhoneType>() {
                //                         new Library.Apas.CoreMain.PhoneType() {
                //                             //AreaCityCode = string.Empty,
                //                             //CountryPrefixCode = string.Empty,
                //                             PhoneNumber = student.Phone,
                //                             //PhoneNumberExtension = string.Empty,
                //                         }
                //                     },
                //    //URL = new List<Library.Apas.CoreMain.URLType>() {
                //    //                     new Library.Apas.CoreMain.URLType() {
                //    //                         URLAddress = string.Empty,
                //    //                     }
                //    //                 }
                //};
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildContactsType", "Error", ex.ToString());
            }
            return _Contact;
        }

        public List<Library.Apas.CoreMain.AgencyIdentifierType> BuildAgencyIdentifierType(StudentRecordObj student)
        {
            List<Library.Apas.CoreMain.AgencyIdentifierType> _AgencyIdentifier = new List<Library.Apas.CoreMain.AgencyIdentifierType>();

            try
            {
                // TODO: Improve code to handle multiple ASNs in our database
                // Ideally we would return multiples from colleage
                ASN _ASN = ctx.ASNs.Where(x => x.AgencyAssignedID == student.ASN && x.CollegeData == true).OrderByDescending(y => y.ModifiedDateTime).FirstOrDefault();

                if (_ASN != null || !string.IsNullOrWhiteSpace(student.ASN))
                {
                    Library.Apas.CoreMain.AgencyIdentifierType _AgencyIdentifierType = new Library.Apas.CoreMain.AgencyIdentifierType()
                    {
                        AgencyAssignedID = student.ASN.Trim(),
                        AgencyCode = _ASN != null ? (_ASN.AgencyCode ?? Library.Apas.CoreMain.AgencyCodeType.Province) : Library.Apas.CoreMain.AgencyCodeType.Province,
                        AgencyName = _ASN != null && !string.IsNullOrWhiteSpace(_ASN.AgencyName) ? _ASN.AgencyName : Structs.Institution.AlbertaEducation,
                        CountryCode = _ASN != null ? (_ASN.CountryCode ?? Library.Apas.CoreMain.CountryCodeType.CA) : Library.Apas.CoreMain.CountryCodeType.CA,
                        StateProvinceCode = _ASN != null ? (_ASN.StateProvinceCode ?? Library.Apas.CoreMain.StateProvinceCodeType.AB) : Library.Apas.CoreMain.StateProvinceCodeType.AB,
                        //StateProvinceCode = Library.Apas.CoreMain.StateProvinceCodeType.AB,  //TODO: REMOVE LINE!!!
                    };

                    _AgencyIdentifier.Add(_AgencyIdentifierType);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }
            return _AgencyIdentifier;
        }

        public List<Library.Apas.CoreMain.NameType> BuildAlternateNameType(StudentRecordObj student)
        {
            List<Library.Apas.CoreMain.NameType> _AlternateNames = new List<Library.Apas.CoreMain.NameType>();

            try
            {
                foreach (var altName in student.FormerNames)
                {
                    Library.Apas.CoreMain.NameType _NameType = new Library.Apas.CoreMain.NameType()
                    {
                        FirstName = altName.FirstName,
                        MiddleName = new List<string> { altName.MiddleName },
                        LastName = altName.LastName,
                    };
                    _AlternateNames.Add(_NameType);
                };
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }
            return _AlternateNames;
        }

        public List<Library.Apas.AcademicRecord.ContactsType> BuildInstitutionContactsType(Institution institution)
        {
            List<Library.Apas.AcademicRecord.ContactsType> _Contacts = new List<Library.Apas.AcademicRecord.ContactsType>();

            try
            {
                List<Address> _Address = new List<Address>() { institution.Addresses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault() };
                Phone _Phone = institution.Phones.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();

                Library.Apas.AcademicRecord.ContactsType _Contact = new Library.Apas.AcademicRecord.ContactsType();

                // Address
                if (_Address != null)
                {
                    _Contact.Address = BuildContactAddressType(_Address);

                    //List<Library.Apas.CoreMain.ItemsChoiceType> _ItemsElementName = new List<Library.Apas.CoreMain.ItemsChoiceType>() {
                    //                                                                     Library.Apas.CoreMain.ItemsChoiceType.StateProvince,
                    //                                                                     Library.Apas.CoreMain.ItemsChoiceType.PostalCode,
                    //                                                                 };
                    //List<object> _Items = new List<object>() {
                    //                            _Address.Province,
                    //                            _Address.PostalCode,
                    //                        };
                    //// If address is not in Canada, add the country code
                    //if (_Address.Country != null && !_Address.Country.Contains("CA"))
                    //{
                    //    _ItemsElementName.Add(Library.Apas.CoreMain.ItemsChoiceType.CountryCode);
                    //    _Items.Add(_Address.Country);
                    //}

                    //_Contact.Address = new List<Library.Apas.AcademicRecord.AddressType1>() {
                    //                         new Library.Apas.AcademicRecord.AddressType1() {
                    //                             AddressLine = new List<string>() {
                    //                                 _Address.AddressLine1,
                    //                                 _Address.AddressLine2,
                    //                             },
                    //                             City = _Address.City,
                    //                             ItemsElementName = _ItemsElementName.ToArray(),
                    //                             Items = _Items.ToArray(),
                    //                         }
                    //                     };
                };

                // Email
                if (institution.Emails != null && institution.Emails.Any())
                {
                    _Contact.Email = new List<Library.Apas.AcademicRecord.EmailType1>() {
                                             new Library.Apas.AcademicRecord.EmailType1() {
                                                 EmailAddress = institution.Emails.OrderByDescending(x => x.ModifiedDateTime).Select(y => y.EmailAddress).FirstOrDefault(),
                                             }
                                         };
                };

                // Phone
                if (_Phone != null)
                {
                    _Contact.Phone = new List<Library.Apas.CoreMain.PhoneType>() {
                                             new Library.Apas.CoreMain.PhoneType() {
                                                 AreaCityCode = _Phone.AreaCode,
                                                 CountryPrefixCode = _Phone.CountryCode,
                                                 PhoneNumber = _Phone.PhoneNumber,
                                                 PhoneNumberExtension = _Phone.PhoneNumberExtension,
                                             }
                                         };
                };

                // URL
                //_Contact.URL = new List<Library.Apas.CoreMain.URLType>() {
                //                     new Library.Apas.CoreMain.URLType() {
                //                         URLAddress = string.Empty,
                //                     }
                //                 };

                _Contacts.Add(_Contact);
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildInstitutionContactsType", "Error", ex.ToString());
            }
            return _Contacts;
        }

        public List<Library.Apas.AcademicRecord.AddressType1> BuildContactAddressType(List<Address> addresses)
        {
            List<Library.Apas.AcademicRecord.AddressType1> _Addresses = new List<Library.Apas.AcademicRecord.AddressType1>();
            try
            {
                foreach (Address address in addresses)
                {
                    List<Library.Apas.CoreMain.ItemsChoiceType> _ItemsElementName = new List<Library.Apas.CoreMain.ItemsChoiceType>() {
                                                                                        Library.Apas.CoreMain.ItemsChoiceType.StateProvince,
                                                                                        Library.Apas.CoreMain.ItemsChoiceType.PostalCode,
                                                                                        Library.Apas.CoreMain.ItemsChoiceType.CountryCode,
                                                                                    };

                    List<object> _Items = new List<object>() {
                                        address.Province,
                                        address.PostalCode,
                                        (Library.Apas.CoreMain.CountryCodeType)Enum.Parse(typeof(Library.Apas.CoreMain.CountryCodeType), address.Country == "XCA" ? "CA": address.Country),
                                    };

                    // If address is not in Canada, add the country code
                    //if (address.Country != null && !address.Country.Contains("CA"))
                    //{
                    //    _ItemsElementName.Add(Library.Apas.CoreMain.ItemsChoiceType.CountryCode);
                    //    _Items.Add(address.Country);
                    //}

                    Library.Apas.AcademicRecord.AddressType1 _Address = new Library.Apas.AcademicRecord.AddressType1() {
                                                                            AddressLine = new List<string>() {
                                                                                address.AddressLine1.CleanAddressLine(),
                                                                                address.AddressLine2.CleanAddressLine(),
                                                                            },
                                                                            City = address.City,
                                                                            ItemsElementName = _ItemsElementName.ToArray(),
                                                                            Items = _Items.ToArray(),
                                                                         };

                    _Addresses.Add(_Address);
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildContactAddressType", "Error", ex.ToString());
            }
            return _Addresses;
        }

        public List<Library.Apas.AcademicRecord.AcademicAwardsReportedType> BuildAcademicAwardsReportedTypes(AcademicRecord academicRecord)
        {
            List<Library.Apas.AcademicRecord.AcademicAwardsReportedType> _AcademicAwardsReportedType = new List<Library.Apas.AcademicRecord.AcademicAwardsReportedType>();

            try
            {
                foreach (AcademicAward academicAward in academicRecord.AcademicAwards.OrderByDescending(x => x.AcademicAwardDate))
                {
                    _AcademicAwardsReportedType.Add(BuildAcademicAwardsReportedType(academicAward));
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildAcademicAwardsReportedTypes", "Error", ex.ToString());
            }
            return _AcademicAwardsReportedType;
        }

        public Library.Apas.AcademicRecord.AcademicAwardsReportedType BuildAcademicAwardsReportedType(AcademicAward academicAward)
        {
            Library.Apas.AcademicRecord.AcademicAwardsReportedType _AcademicAwardsReportedType = new Library.Apas.AcademicRecord.AcademicAwardsReportedType();

            try
            {
                _AcademicAwardsReportedType = new Library.Apas.AcademicRecord.AcademicAwardsReportedType()
                {
                    AcademicAwardTitle = academicAward.AcademicAwardTitle,
                };

                DateTime? _AcademicAwardDate = academicAward.AcademicAwardDate;
                if (_AcademicAwardDate.HasValue)
                {
                    _AcademicAwardsReportedType.AcademicAwardDate = _AcademicAwardDate.Value;
                    _AcademicAwardsReportedType.AcademicAwardDateSpecified = true;
                }

            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasAdmin, Structs.Class.LcapasLogic, "BuildAcademicAwardsReportedType", "Error", ex.ToString());
            }

            return _AcademicAwardsReportedType;
        }

        public Library.Apas.AcademicRecord.K12StudentType BuildK12StudentType(StudentRecordObj student)
        {
            Library.Apas.AcademicRecord.K12StudentType _K12StudentType = new Library.Apas.AcademicRecord.K12StudentType();

            try
            {
                _K12StudentType = new Library.Apas.AcademicRecord.K12StudentType()
                {
                    AcademicRecord = BuildAcademicRecords(student),
                    Health = BuildHealth(student),
                    Person = BuildK12PersonType(student),
                    Tests = BuildTests(student),
                };
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return _K12StudentType;
        }

        public Library.Apas.AcademicRecord.CourseType BuildCourse(StuGradesObj grade)
        {
            Library.Apas.AcademicRecord.CourseType _Course = new Library.Apas.AcademicRecord.CourseType();

            try
            {
                _Course = new Library.Apas.AcademicRecord.CourseType()
                {
                    CourseCreditBasis = Library.Apas.CoreMain.CourseCreditBasisType.Regular,
                    CourseCreditValue = (decimal)grade.CRS_ATT_CRD,
                    CourseAcademicGrade = grade.CRS_GRADE,
                    CourseNumber = grade.CRS_CODE,
                    CourseTitle = grade.CRS_TITLE,
                };
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return _Course;
        }

        public Library.Apas.AcademicRecord.AcademicSessionType BuildAcademicSession(StudentRecordObj student, IEnumerable<StuGradesObj> grades)
        {
            List<Library.Apas.AcademicRecord.CourseType> _Courses = new List<Library.Apas.AcademicRecord.CourseType>();
            Library.Apas.AcademicRecord.AcademicSessionType _AcademicSession = new Library.Apas.AcademicRecord.AcademicSessionType();
            List<Library.Apas.AcademicRecord.AcademicProgramType> _AcademicPrograms = new List<Library.Apas.AcademicRecord.AcademicProgramType>();
            List<Library.Apas.AcademicRecord.AcademicSummaryFType> _AcademicSummary = new List<Library.Apas.AcademicRecord.AcademicSummaryFType>();
            Library.Apas.CoreMain.AcademicSessionDetailType _AcademicSessionDetailType = new Library.Apas.CoreMain.AcademicSessionDetailType();

            try
            {
                _AcademicPrograms = BuildAcademicPrograms(student);

                if (grades.Any())
                {
                    _AcademicSessionDetailType = BuildAcademicSessionDetails(grades);
                    _AcademicSummary = BuildAcademicSummaries(grades);

                    foreach (StuGradesObj grade in grades)
                    {
                        _Courses.Add(BuildCourse(grade));
                    }
                }

                _AcademicSession.Course = _Courses;
                _AcademicSession.AcademicProgram = _AcademicPrograms;
                _AcademicSession.AcademicSessionDetail = _AcademicSessionDetailType;
                _AcademicSession.AcademicSummary = _AcademicSummary;

            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return _AcademicSession;
        }

        public Library.Apas.CoreMain.AcademicSessionDetailType BuildAcademicSessionDetails(IEnumerable<StuGradesObj> grades)
        {

            Library.Apas.CoreMain.AcademicSessionDetailType _AcademicSessionDetailType = new Library.Apas.CoreMain.AcademicSessionDetailType();

            try
            {
                string lcterm = grades.FirstOrDefault().CRS_TERM;
                string sessionName = string.Empty;
                string sessionDesignator = string.Empty;
                string term = lcterm.Substring(2, 2);
                int year = Convert.ToInt32(lcterm.Substring(0, 2));
                int fourDigitYear = CultureInfo.CurrentCulture.Calendar.ToFourDigitYear(year);

                switch (term)
                {
                    case Structs.Terms.WN:
                        _AcademicSessionDetailType.SessionName = "WINTER " + fourDigitYear.ToString();
                        _AcademicSessionDetailType.SessionBeginDate = Functions.GetFirstDayOfWeek(new DateTime(fourDigitYear, 01, 1));

                        if (_AcademicSessionDetailType.SessionBeginDate.Day == 1)
                        {
                            _AcademicSessionDetailType.SessionBeginDate = Functions.GetFirstDayOfWeek(new DateTime(fourDigitYear, 01, 2));
                        }

                        _AcademicSessionDetailType.SessionEndDate = Functions.GetLastBusinessDayOfMonth(fourDigitYear, 04);
                        _AcademicSessionDetailType.SessionType = Library.Apas.CoreMain.SessionTypeType.Semester;

                        break;
                    case Structs.Terms.SM:
                        _AcademicSessionDetailType.SessionName = "SUMMER " + fourDigitYear.ToString();

                        _AcademicSessionDetailType.SessionBeginDate = Functions.GetFirstDayOfWeek(new DateTime(fourDigitYear, 05, 1));
                        _AcademicSessionDetailType.SessionEndDate = Functions.GetLastBusinessDayOfMonth(fourDigitYear, 08);
                        _AcademicSessionDetailType.SessionType = Library.Apas.CoreMain.SessionTypeType.SummerSession;

                        break;
                    case Structs.Terms.S1:
                        _AcademicSessionDetailType.SessionName = "SUMMER1 " + fourDigitYear.ToString();

                        _AcademicSessionDetailType.SessionBeginDate = Functions.GetFirstDayOfWeek(new DateTime(fourDigitYear, 07, 1));
                        _AcademicSessionDetailType.SessionEndDate = Functions.GetLastBusinessDayOfMonth(fourDigitYear, 08);
                        _AcademicSessionDetailType.SessionType = Library.Apas.CoreMain.SessionTypeType.SummerSession;

                        break;
                    case Structs.Terms.S2:
                        _AcademicSessionDetailType.SessionName = "SUMMER2 " + fourDigitYear.ToString();

                        _AcademicSessionDetailType.SessionBeginDate = Functions.GetFirstDayOfWeek(new DateTime(fourDigitYear, 05, 1));
                        _AcademicSessionDetailType.SessionEndDate = Functions.GetLastBusinessDayOfMonth(fourDigitYear, 06);
                        _AcademicSessionDetailType.SessionType = Library.Apas.CoreMain.SessionTypeType.SummerSession;

                        break;
                    case Structs.Terms.FL:
                        _AcademicSessionDetailType.SessionName = "AUTUMN " + fourDigitYear.ToString();

                        _AcademicSessionDetailType.SessionBeginDate = Functions.GetFirstDayOfWeek(new DateTime(fourDigitYear, 09, 1));
                        _AcademicSessionDetailType.SessionEndDate = Functions.GetLastBusinessDayOfMonth(fourDigitYear, 12);
                        _AcademicSessionDetailType.SessionEndDate = Functions.GetLastBusinessDayOfMonth(fourDigitYear, 06);
                        _AcademicSessionDetailType.SessionType = Library.Apas.CoreMain.SessionTypeType.Semester;

                        break;
                }

                _AcademicSessionDetailType.SessionDesignator = _AcademicSessionDetailType.SessionBeginDate.ToString("yyyy-MM");

                _AcademicSessionDetailType.SessionBeginDateSpecified = true;
                _AcademicSessionDetailType.SessionEndDateSpecified = true;
                _AcademicSessionDetailType.SessionTypeSpecified = true;

                // TODO: Start of S1, S2 needs to be one week after end of WN

            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return _AcademicSessionDetailType;
        }

        public List<string> AddNoteMessage(string msg, List<string> msgs = null)
        {
            List<string> _Messages = msgs ?? new List<string>();

            try
            {
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    _Messages.Add(msg);
                }
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
            }
            return _Messages;
        }

        //public string CreateTransactionTranscriptItem(string trangroup, string stuid)
        //{
        //    string _QueueId = string.Empty;

        //    try
        //    {
        //        TransactionQueue queue = CreateTransactionQueueItem();

        //        TransactionTranscript transcript = new TransactionTranscript()
        //        {
        //            TranscriptGrouping = trangroup,
        //            StudentId = stuid,
        //            TransactionQueue = queue,
        //        };

        //        _QueueId = queue.TransactionId.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.ToString();
        //    }

        //    return _QueueId;
        //}

        //public string CreateTransactionRequestItem(string trangroup, string stuid)
        //{
        //    string _QueueId = string.Empty;

        //    try
        //    {
        //        TransactionQueue queue = CreateTransactionQueueItem();

        //        TransactionTranscript transcript = new TransactionTranscript()
        //        {
        //            TranscriptGrouping = trangroup,
        //            StudentId = stuid,
        //            TransactionQueue = queue,
        //        };

        //        _QueueId = queue.TransactionId.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.ToString();
        //    }

        //    return _QueueId;
        //}

        //public string CreateTransactionCourseItem(string trangroup, string stuid)
        //{
        //    string _QueueId = string.Empty;

        //    try
        //    {
        //        TransactionQueue queue = CreateTransactionQueueItem();

        //        TransactionTranscript transcript = new TransactionTranscript()
        //        {
        //            TranscriptGrouping = trangroup,
        //            StudentId = stuid,
        //            TransactionQueue = queue,
        //        };

        //        _QueueId = queue.TransactionId.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.ToString();
        //    }

        //    return _QueueId;
        //}


        //public TransactionQueue CreateTransactionQueueItem()
        //{
        //    TransactionQueue queue = new TransactionQueue();
        //    try
        //    {
        //        queue.CreatedDateTime = DateTime.Now;
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.ToString();
        //    }

        //    return queue;
        //}

        #endregion Build Objects

        #region Utility 

        public bool SaveException(string proj, string page, string function, string variable, string value, bool sendEmail = true)
        {
            bool success = false;
            ExceptionRecord _ExceptionRecord = new ExceptionRecord();
            try
            {
                _ExceptionRecord.Project = proj;
                _ExceptionRecord.Page = page;
                _ExceptionRecord.Function = function;
                _ExceptionRecord.Variable = variable;
                _ExceptionRecord.Value = value;
                _ExceptionRecord.CreatedBy = _UserName;
                _ExceptionRecord.CreatedDateTime = DateTime.Now;
                _ExceptionRecord.ModifiedBy = _UserName;
                _ExceptionRecord.ModifiedDateTime = DateTime.Now;
                ctx.ExceptionRecords.Add(_ExceptionRecord);
                ctx.SaveChanges();

                if (sendEmail)
                {
                    new SendEmail(proj, page, function, variable, value);
                }

                success = true;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                //SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "importNewApplications", "Error: ", "failed uuids:" + failedUUIDs + "; " + ex.ToString());
            }
            return success;
        }

        public bool SaveApasError(ReceivedError error)
        {
            bool success = false;

            try
            {
                if (!CheckReceivedErrorExists(error.UUID.ToString()))
                {
                    error.CreatedBy = _UserName;
                    error.CreatedDateTime = DateTime.Now;
                    error.ModifiedBy = _UserName;
                    error.ModifiedDateTime = DateTime.Now;
                    ctx.ReceivedErrors.Add(error);
                    ctx.SaveChanges();
                }
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveApasError", "Error", ex.ToString());
            }

            return success;
        }

        public void SaveLoginHistory(string sNumber, string actionName)
        {
            DateTime _Now = DateTime.Now;
            try
            {
                if (!string.IsNullOrWhiteSpace(sNumber))
                {
                    User _User = new User();

                    _User = ctx.Users.Where(x => x.sNumber.ToLower().Trim() == sNumber.ToLower().Trim()).OrderByDescending(y => y.CreatedDateTime).FirstOrDefault();

                    History _History = new History()
                    {
                        User = _User,
                        CreatedBy = _UserName,
                        CreatedDateTime = _Now,
                        ModifiedBy = _UserName,
                        ModifiedDateTime = _Now
                    };

                    var results = ctx.Histories.Where(x => x.User.UserId == _User.UserId);

                    if (!string.IsNullOrWhiteSpace(actionName))
                    {
                        Enums.ActionTypeType actionType = (Enums.ActionTypeType)Enum.Parse(typeof(Enums.ActionTypeType), actionName);

                        ActionType _ActionType = new ActionType();

                        _ActionType = ctx.ActionTypes.Where(x => x.ActionTypeType == actionType).OrderByDescending(y => y.CreatedDateTime).FirstOrDefault();

                        _History.ActionType = _ActionType;

                        results = results.Where(x => x.ActionType.ActionId == _ActionType.ActionId);
                    }

                    if (!results.Any() || 
                        (results.Any() && _Now.Subtract(Functions.CheckForNullDate(results.OrderByDescending(y => y.CreatedDateTime).Select(a => a.CreatedDateTime).FirstOrDefault()) ?? _Now).TotalSeconds > 5))
                    {
                        ctx.Histories.Add(_History);
                        ctx.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveLoginHistory", "Error: ", ex.ToString());
            }
        }

        public bool SaveSentEmail(SendEmail sentEmail)
        {
            bool success = false;

            SentEmail _SentEmail = new SentEmail();

            try
            {
                if (sentEmail != null)
                {
                    _SentEmail.EmailType = sentEmail.ReceiverGroup;
                    _SentEmail.From = sentEmail.From;
                    _SentEmail.To = sentEmail.To;
                    _SentEmail.Subject = sentEmail.Title;
                    _SentEmail.Body = sentEmail.Body;

                    _SentEmail.CreatedBy = _UserName;
                    _SentEmail.CreatedDateTime = DateTime.Now;
                    _SentEmail.ModifiedBy = _UserName;
                    _SentEmail.ModifiedDateTime = DateTime.Now;

                    // Try to find email
                    _SentEmail.Email = GetEmailByEmailAddress(_SentEmail.To);

                    ctx.SentEmails.Add(_SentEmail);
                    ctx.SaveChanges();
                }
                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SaveSentEmail", "Error", ex.ToString());
            }

            return success;
        }

        public bool QueueJob(string key, Enums.JobTypeType type)
        {
            bool success = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    if (!ctx.TranscriptJobs.Where(x => x.JobType == type && x.JobKey == key).Any())
                    {
                        ctx.TranscriptJobs.Add(new TranscriptJob()
                        {
                            CreatedDateTime = DateTime.Now,
                            ModifiedDateTime = DateTime.Now,
                            JobType = type,
                            JobKey = key,
                        });

                        if (ctx.SaveChanges() > 0)
                        {
                            success = true;
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "QueueJob", "Error", ex.ToString());
            }

            return success;
        }

        public List<TranscriptJob> GetJobs()
        {
            int maxJobs = GetSetting(Structs.SettingTypes.Integer, Structs.Settings.JobsMaxPerExecution) ?? 20;
            List<TranscriptJob> jobs = new List<TranscriptJob>();

            try
            {
                jobs = ctx.TranscriptJobs.Where(b => b.CompletedDateTime == null && b.JobKilledDateTime == null && b.StartedDateTime == null).OrderBy(y => y.CreatedDateTime).Take(maxJobs).ToList();

                // Mark the selected jobs as "Started"
                jobs.ForEach(x => StartJob(x.JobId));
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetJobs", "Error", ex.ToString());
            }

            return jobs;
        }

        public TranscriptJob GetJob(string jobKey)
        {
            TranscriptJob job = new TranscriptJob();

            try
            {
                job = ctx.TranscriptJobs.Where(b => b.CompletedDateTime == null
                                                 //&& b.JobKilledDateTime == null
                                                 && b.JobKey == jobKey)
                                                 .OrderBy(y => y.CreatedDateTime).FirstOrDefault();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetJob", "Error", ex.ToString());
            }

            return job;
        }

        public bool CleanJobs()
        {
            bool success = false;
            DateTime fromDate = DateTime.Now.AddDays(-5);
            try
            {
                var jobsToRemove = ctx.TranscriptJobs.Where(y => (y.CompletedDateTime != null || y.JobKilledDateTime != null ) && y.CreatedDateTime < fromDate).AsEnumerable();

                ctx.TranscriptJobs.RemoveRange(jobsToRemove);

                ctx.SaveChanges();

                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "CleanJobs", "Error", ex.ToString());
            }
            return success;
        }

        public bool StartJob(int? jobID = null)
        {
            bool success = false;

            try
            {
                if (jobID != null)
                {
                    TranscriptJob _TranscriptJob = ctx.TranscriptJobs.Where(x => x.JobId == jobID).FirstOrDefault();

                    if (_TranscriptJob != null)
                    {
                        _TranscriptJob.StartedDateTime = DateTime.Now;

                        ctx.SaveChanges();

                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "StartJob", "Error", ex.ToString());
            }

            return success;
        }

        public bool CleanStartedDateTimeJob(int? jobID = null)
        {
            bool success = false;

            try
            {
                if (jobID != null)
                {
                    TranscriptJob _TranscriptJob = ctx.TranscriptJobs.Where(x => x.JobId == jobID).FirstOrDefault();

                    if (_TranscriptJob != null)
                    {
                        _TranscriptJob.StartedDateTime = null;

                        ctx.SaveChanges();

                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "StartJob", "Error", ex.ToString());
            }

            return success;
        }

        public bool CompleteJob(int? jobID = null)
        {
            bool success = false;

            try
            {
                if (jobID != null)
                {
                    TranscriptJob _TranscriptJob = ctx.TranscriptJobs.Where(x => x.JobId == jobID).FirstOrDefault();

                    if (_TranscriptJob != null)
                    {
                        _TranscriptJob.CompletedDateTime = DateTime.Now;

                        ctx.SaveChanges();

                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "CompleteJob", "Error", ex.ToString());
            }

            return success;
        }

        public bool CancelJob(string jobKey)
        {
            bool success = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(jobKey))
                {
                    TranscriptJob _TranscriptJob = ctx.TranscriptJobs.Where(x => x.JobKey == jobKey).FirstOrDefault();

                    if (_TranscriptJob != null)
                    {
                        _TranscriptJob.JobKilledDateTime = DateTime.Now;

                        ctx.SaveChanges();

                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "CancelJob", "Error", ex.ToString());
            }

            return success;
        }

        private List<Student> FindStudents(string firstName = "", string lastName = "", List<string> middleNames = null, List<string> asnList = null, DateTime? birthDate = null)
        {
            List<Student> _Students = new List<Student>();

            try
            {
                _Students = FindPerson(firstName, lastName, middleNames, asnList, birthDate).SelectMany(b => b.Students).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "FindStudent", "Error: ", ex.ToString());
            }
            return _Students;
        }

        private List<Person> FindPerson(string firstName = "", string lastName = "", List<string> middleNames = null, List<string> asnList = null, DateTime? birthDate = null)
        {
            List<Person> _Persons = new List<Person>();

            try
            {
                //string middlename = Functions.JoinStrings(middleNames);

                _Persons = ctx.Persons.Where(x => ((x.Students.Any(y => y.ASNs.Any(z => asnList.Contains(z.AgencyAssignedID))) || x.BirthDate == birthDate)
                                                    && (x.Names.Any(y => y.LastName == lastName
                                                        && y.FirstName == firstName)))
                                                    || (x.Students.Any(y => y.ASNs.Any(z => asnList.Contains(z.AgencyAssignedID)))
                                                        && birthDate != null
                                                        && x.BirthDate == birthDate
                                                        && x.Names.Any(y => y.LastName == lastName
                                                           || y.FirstName == firstName))).ToList();
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "FindPerson", "Error: ", ex.ToString());
            }
            return _Persons;
        }

        /// <summary>
        /// Not Currently being called
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public bool ExistsApplicationMessage(ApplicationMessage application)
        {
            bool success = false;
            ApplicationMessage _ApplicationMessage = new ApplicationMessage();

            try
            {
                var query = (from a in ctx.ApplicationMessages
                             where a.DocumentID.Contains(application.DocumentID)
                             select a);

                if (!query.Any())
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ExistsApplicationMessage", "Error", ex.ToString());
            }

            return success;
        }

        public ApasMessage GetApasMessage(string uuid, Enums.MessageTypes? messageType = null)
        {
            ApasMessage _ApasMessage = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(uuid))
                {
                    // Filter by UUID
                    var query = (from r in ctx.ApasMessages
                                 where r.UUID.Trim().ToUpper() == uuid.Trim().ToUpper()
                                 select r);

                    // Filter by Message Type
                    if (messageType != null)
                    {
                        query = query.Where(x => x.MessageType == messageType);
                    }

                    // If Apas Message found
                    if (query.Any())
                    {
                        _ApasMessage = query.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetApasMessage", "Error", ex.ToString());
            }

            return _ApasMessage;
        }

        public MyCredsMessage GetMyCredsMessage(string uuid, Enums.MyCredsDocumentTypes? documentType = null)
        {
            MyCredsMessage _MyCredsMessage = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(uuid))
                {
                    // Filter by UUID
                    var query = (from r in ctx.MyCredsMessages
                                 where r.UUID.Trim().ToUpper() == uuid.Trim().ToUpper()
                                 select r);

                    // Filter by Document Type
                    if (documentType != null)
                    {
                        query = query.Where(x => x.MessageType == documentType);
                    }

                    // If MyCreds Message found
                    if (query.Any())
                    {
                        _MyCredsMessage = query.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetMyCredsMessage", "Error", ex.ToString());
            }

            return _MyCredsMessage;
        }

        public string ValidateTestingUUID(string uuid = null)
        {
            string retval = string.Empty;

            try
            {
                var results = (from a in ctx.ApasMessages
                               where a.UUID.ToLower().Trim() == uuid.ToLower().Trim()
                               select a);

                if (results.Any())
                {
                    retval = results.OrderByDescending(n => n.CreatedDateTime).Select(y => y.UUID).FirstOrDefault();
                }
                else
                {
                    retval = uuid;
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "ValidateTestingUUID", "Error: ", ex.ToString().Substring(0, 200));
            }

            return retval;
        }

        public Library.Apas.CoreMain.LocalOrganizationIDCodeQualifierType GetInstitutionIDQualifier(Institution institution, Library.Apas.CoreMain.LocalOrganizationIDCodeQualifierType defaultValue)
        {
            Library.Apas.CoreMain.LocalOrganizationIDCodeQualifierType IDQualifier = defaultValue;

            try
            {
                // Try to get institution address province, otherwise use the default value passed
                var instAddress = institution.Addresses.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
                if (instAddress != null && !string.IsNullOrWhiteSpace(instAddress.Province) &&
                    Enum.TryParse(instAddress.Province, out Library.Apas.CoreMain.LocalOrganizationIDCodeQualifierType parseIDQualifier) &&
                    Enum.IsDefined(typeof(Library.Apas.CoreMain.LocalOrganizationIDCodeQualifierType), parseIDQualifier))
                {
                    IDQualifier = parseIDQualifier;
                }
                else
                {
                    // If APAS or Lethbridge College, default to AB (Alberta)
                    if (institution.LocalOrganizationID.Trim().ToUpper() == Structs.Class.Apas.Trim().ToUpper() ||
                        institution.LocalOrganizationID.Trim().ToUpper() == ((string)GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode)).Trim().ToUpper())
                    {
                        IDQualifier = Library.Apas.CoreMain.LocalOrganizationIDCodeQualifierType.AB;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "GetInstitutionIDQualifier", "Error: ", ex.ToString().Substring(0, 200));
            }

            return IDQualifier;
        }

        public void Dispose()
        {
            ctx.Dispose();
        }
        #endregion End Delete

        #region Seed
        public bool SeedSettings()
        {
            bool success = false;

            try
            {
                //// 2. save settings to db - comment out for productoin
                SaveSetting("LocalAPASCode", "48027000");
                SaveSetting("environment", "ProdSupport");
                SaveSetting("ApasSuppPath", "https://prodsupport.applyalberta.ca/");
                SaveSetting("ApasProdPath", "https://prod.applyalberta.ca/");
                SaveSetting("adminWriteGifPath", "APAS.Web.Administration/WriteGif.aspx");
                SaveSetting("applicantWriteGifPath", "APAS.Web.Public/WriteGif.aspx");
                SaveSetting("ApasRestoreLoginPath", "APAS.WebServices/RestoreLogin.aspx?SessionId={0}&SecurityToken={1}&ApplyInstitution={2}");
                SaveSetting("ApasRestoreAdminLoginPath", "APAS.WebServices/RestoreLogin.aspx?SessionId={0}&SecurityToken={1}&ApplyInstitution={2}&wf=SearchApplicantProfileWorkflow&vw=IApplicantProfileListView");
                SaveSetting("logoutUrl", "APAS.WebServices/RestoreLogin.aspx?SessionId={0}&SecurityToken={1}&wf=UnrestrictedWorkflow&vw=ILogOutView&ApplyInstitution={2}");
                SaveSetting("redirectToApas", "/APAS.WebServices/RestoreLogin.aspx?SessionId={0}&SecurityToken={1}&wf=MyApplicationsWorkflow&vw=IMyApplicationsView&ApplyInstitution={2}");

                /// paypal settings
                // credentials
                SaveSetting("PayPalProdAPIUser", "lethbrcctest40");
                SaveSetting("PayPalProdAPIPassword", "Snowman25");
                SaveSetting("PayPalProdAPIVendor", "lethbrcctest40");
                SaveSetting("PayPalProdAPIPartner", "datatel");

                SaveSetting("PayPalDevAPIUser", "lethbrcctest40");
                SaveSetting("PayPalDevAPIPassword", "Snowman25");
                SaveSetting("PayPalDevAPIVendor", "lethbrcctest40");
                SaveSetting("PayPalDevAPIPartner", "datatel");

                // prod Urls
                SaveSetting("PayPalProdHostPath", "www.paypal.com");
                SaveSetting("PayPalProdEndPointUrl", "https://payflowpro.paypal.com");

                // dev Urls
                SaveSetting("PayPalDevHostUrl", "www.sandbox.paypal.com");
                SaveSetting("PayPalDevEndPointUrl", "https://pilot-payflowpro.paypal.com");

                // settings
                SaveSetting("PayPalEnvironment", "pilot"); // change to bool?
                SaveSetting("PayPalBNCode", "PF-CCWizard");
                SaveSetting("PayPalTimeOut", 90000);
                SaveSetting("PayPalTender", "P");
                SaveSetting("PayPalAction", "S");
                SaveSetting("PayPalTRXType", "S");
                SaveSetting("PayPalErrorURL", "Payment/PaymentError/");
                SaveSetting("PayPalReturnURL", "Payment/ConfirmPayment/");
                SaveSetting("PayPalCancelURL", "Payment/Cancel Payment/");
                SaveSetting("ProcessingFeeAmt", 75.00);
                SaveSetting("ProcessingFeeAmt", 90.00);
                SaveSetting("PayPalCreateSecureToken", "Y");

                success = true;

            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SeedSettings", "Error: ", ex.ToString());
            }
            return success;

        }

        public bool SeedProgramCampusTerms()
        {
            bool success = false;
            try
            {

                {
                    var _ApplicationPrograms = new List<ApplicationProgram>
                    {
                        new ApplicationProgram { ProgramCode = "AHM.CERT", ProgramDesc = "Agricultural and Heavy Equipment Technician Certificate" },
                        new ApplicationProgram { ProgramCode = "AGR.ANM.DIP", ProgramDesc = "Agricultural Technology - Animal Science Diploma" },
                        new ApplicationProgram { ProgramCode = "AGR.PLT.DIP", ProgramDesc = "Agricultural Technology - Plant and Soil Science Diploma" },
                        new ApplicationProgram { ProgramCode = "AST.CERT", ProgramDesc = "Automotive Systems Certificate" },
                        new ApplicationProgram { ProgramCode = "BTC.CRAA", ProgramDesc = "Biotechnology - Cellular/Molecular Technician Credential of Academic Achievement" },
                        new ApplicationProgram { ProgramCode = "BUS.ACC.DIP", ProgramDesc = "Business Administration - Accounting Diploma" },
                        new ApplicationProgram { ProgramCode = "BUS.GEN.DIP", ProgramDesc = "Business Administration - General Diploma" },
                        new ApplicationProgram { ProgramCode = "BUS.MGT.DIP", ProgramDesc = "Business Administration - Management Diploma" },
                        new ApplicationProgram { ProgramCode = "BUS.MKT.DIP", ProgramDesc = "Business Administration - Marketing Diploma" },
                        new ApplicationProgram { ProgramCode = "BUS.PGM.DIP", ProgramDesc = "Business Administration - Professional Golf Management Diploma" },
                        new ApplicationProgram { ProgramCode = "CSP.CRAA", ProgramDesc = "Central Sterile Processing Credential of Academic Achievement" },
                        new ApplicationProgram { ProgramCode = "CYC.DIP", ProgramDesc = "Child and Youth Care Diploma" },
                        new ApplicationProgram { ProgramCode = "CIV.DIP", ProgramDesc = "Civil Engineering Technology Diploma" },
                        new ApplicationProgram { ProgramCode = "CUP.PC", ProgramDesc = "College and University Preparatory Upgarding Credential of Achievement" },
                        new ApplicationProgram { ProgramCode = "CAP.APR.DIP", ProgramDesc = "Communication Arts - Advertising and Public Relations Diploma" },
                        new ApplicationProgram { ProgramCode = "CAP.BRJ.DIP", ProgramDesc = "Communication Arts - Broadcast Journalism Diploma" },
                        new ApplicationProgram { ProgramCode = "CAP.PRJ.DIP", ProgramDesc = "Communication Arts - Print Journalism Diploma" },
                        new ApplicationProgram { ProgramCode = "CIT.DIP", ProgramDesc = "Computer Information Technology Diploma" },
                        new ApplicationProgram { ProgramCode = "CEN.ADEG", ProgramDesc = "Conservation Enforcement - Bachelor of Applied Science" },
                        new ApplicationProgram { ProgramCode = "COR.ADEG", ProgramDesc = "Correctional Studies - Bachelor of Applied Arts" },
                        new ApplicationProgram { ProgramCode = "COR.DIP", ProgramDesc = "Correctional Studies Diploma" },
                        new ApplicationProgram { ProgramCode = "CJP.FIP.CRAA", ProgramDesc = "Criminal Justice - Fire Investigation and Prevention Credential of Academic Achievement" },
                        new ApplicationProgram { ProgramCode = "CJP.FSA.CRAA", ProgramDesc = "Criminal Justice - Fire Service Administration Credential of Academic Achievement" },
                        new ApplicationProgram { ProgramCode = "CJP.POL.DIP", ProgramDesc = "Criminal Justice - Policing Diploma" },
                        new ApplicationProgram { ProgramCode = "CJA.CRAA", ProgramDesc = "Criminal Justice for Aboriginal Persons Credential of Academic Achievement" },
                        new ApplicationProgram { ProgramCode = "CUL.DIP", ProgramDesc = "Culinary Careers Diploma" },
                        new ApplicationProgram { ProgramCode = "DCR.CERT", ProgramDesc = "Disability and Community Rehabilitation Certificate" },
                        new ApplicationProgram { ProgramCode = "DCR.DIP", ProgramDesc = "Disability and Community Rehabilitation Diploma" },
                        new ApplicationProgram { ProgramCode = "ECE.CERT", ProgramDesc = "Early Childhood Education Certificate" },
                        new ApplicationProgram { ProgramCode = "ECE.DIP", ProgramDesc = "Early Childhood Education Diploma" },
                        new ApplicationProgram { ProgramCode = "EDD.DIP", ProgramDesc = "Engineering Design and Drafting Technology Diploma" },
                        new ApplicationProgram { ProgramCode = "ESL.REAC", ProgramDesc = "English as a Second Language Recognition of Achievement" },
                        new ApplicationProgram { ProgramCode = "EAR.DIP", ProgramDesc = "Environmental Assessment and Restoration Diploma" },
                        new ApplicationProgram { ProgramCode = "EXS.DIP", ProgramDesc = "Exercise Science Diploma" },
                        new ApplicationProgram { ProgramCode = "FMK.DIP", ProgramDesc = "Fashion Design and Marketing Diploma" },
                        new ApplicationProgram { ProgramCode = "FDM.CERT", ProgramDesc = "Fashion Design Certificate" },
                        new ApplicationProgram { ProgramCode = "FAS.CERT", ProgramDesc = "Fetal Alcohol Spectrum Disorder Education Certificate" },
                        new ApplicationProgram { ProgramCode = "FWL.CESP", ProgramDesc = "Fish and Wildlife Technology Certificate of Specialization" },
                        new ApplicationProgram { ProgramCode = "GNS.CDN.DIP", ProgramDesc = "General Studies - Canadian Studies Diploma" },
                        new ApplicationProgram { ProgramCode = "GNS.CLS.DIP", ProgramDesc = "General Studies - Cultural Studies Diploma" },
                        new ApplicationProgram { ProgramCode = "GNS.ENG.DIP", ProgramDesc = "General Studies - English Diploma" },
                        new ApplicationProgram { ProgramCode = "GNS.PSO.DIP", ProgramDesc = "General Studies - Psychology and Sociology Diploma" },
                        new ApplicationProgram { ProgramCode = "GNS.DIP", ProgramDesc = "General Studies Diploma" },
                        new ApplicationProgram { ProgramCode = "GET.DIP", ProgramDesc = "Geomatics Engineering Technology Diploma" },
                        new ApplicationProgram { ProgramCode = "IDM.DIP", ProgramDesc = "Interior Design Diploma" },
                        new ApplicationProgram { ProgramCode = "MAS.DIP", ProgramDesc = "Massage Therapy Diploma" },
                        new ApplicationProgram { ProgramCode = "MMP.DIP", ProgramDesc = "Multimedia Production Diploma" },
                        new ApplicationProgram { ProgramCode = "NRC.DIP", ProgramDesc = "Natural Resource Compliance Diploma" },
                        new ApplicationProgram { ProgramCode = "OAA.CERT", ProgramDesc = "Office Administration Certificate" },
                        new ApplicationProgram { ProgramCode = "OAA.DIP", ProgramDesc = "Office Administration Diploma" },
                        new ApplicationProgram { ProgramCode = "PEN.CRAA", ProgramDesc = "Perioperative Nursing Credential of Academic Achievement" },
                        new ApplicationProgram { ProgramCode = "PCT.CRAA", ProgramDesc = "Police Recruit Training Credential of Academic Achievement" },
                        new ApplicationProgram { ProgramCode = "PNG.DIP", ProgramDesc = "Practical Nursing Diploma" },
                        new ApplicationProgram { ProgramCode = "PGM.CESP", ProgramDesc = "Professional Golf Management Certificate of Specialization" },
                        new ApplicationProgram { ProgramCode = "RRM.DIP", ProgramDesc = "Renewable Resource Management Diploma" },
                        new ApplicationProgram { ProgramCode = "SNE.DIP", ProgramDesc = "Special Needs Educational Assistant Diploma" },
                        new ApplicationProgram { ProgramCode = "TRG.DIP", ProgramDesc = "Therapeutic Recreation - Gerontology Diploma" },
                        new ApplicationProgram { ProgramCode = "UCL.CRAA", ProgramDesc = "Unit Clerk Credential of Academic Achievement" },
                        new ApplicationProgram { ProgramCode = "WIN.CERT", ProgramDesc = "Wind Turbine Technician Certificate" }
                    };

                    // Add Terms
                    var _ProgramTerms = new List<ProgramTerm>
                    {
                        new ProgramTerm { TermCode = "16WN", TermDesc = "2016 Winter Term (Jan-Apr)" },
                        new ProgramTerm { TermCode = "16FL", TermDesc = "2016 Fall Term (Sep-Dec)" },
                        new ProgramTerm { TermCode = "16SM", TermDesc = "2016 Summer Term (May-Aug)" },
                        new ProgramTerm { TermCode = "16M", TermDesc = "Monthly" }
                    };

                    // Add Campuses
                    var _ProgramCampus = new List<ProgramCampus>
                    {
                        new ProgramCampus { CampusCode = "Main", CampusDesc = "Main Campus" },
                        new ProgramCampus { CampusCode = "Claresholm", CampusDesc = "Claresholm Campus" },
                        new ProgramCampus { CampusCode = "Fort Macleod", CampusDesc = "Fort Macleod Campus" },
                        new ProgramCampus { CampusCode = "Online", CampusDesc = "Online" }
                    };

                    // Add Program Details and Programs
                    _ApplicationPrograms.ForEach(p => _ProgramTerms.ForEach(t => _ProgramCampus.ForEach(c =>
                        p.ProgramDetails.Add(AddProgramDetails(false, DateTime.Now,
                                             AddPrograms(p.ProgramCode, p.ProgramDesc).ApplicationProgramId,
                                             AddProgramTerms(t.TermCode, t.TermDesc).ProgramTermId,
                                             AddProgramCampus(c.CampusCode, c.CampusDesc).ProgramCampusId)))));

                    // Add References
                    var _ReferenceType = new List<ReferenceType>
                    {
                        new ReferenceType { ReferenceTypeName = "FF", ReferenceTypeDesc = "Family/Friend" },
                        new ReferenceType { ReferenceTypeName = "HC", ReferenceTypeDesc = "High School Counsellor" },
                        new ReferenceType { ReferenceTypeName = "HV", ReferenceTypeDesc = "High School Visit/Recruiter" },
                        new ReferenceType { ReferenceTypeName = "CA", ReferenceTypeDesc = "Lethbridge College Advisor" },
                        new ReferenceType { ReferenceTypeName = "CS", ReferenceTypeDesc = "Other Lethbridge College Staff/Faculty" },
                        new ReferenceType { ReferenceTypeName = "CP", ReferenceTypeDesc = "Lethbridge College Publications/Website" },
                        new ReferenceType { ReferenceTypeName = "ST", ReferenceTypeDesc = "Lethbridge College Student/Alumni" },
                        new ReferenceType { ReferenceTypeName = "OH", ReferenceTypeDesc = "Open House/Career Conference" }
                    };
                    _ReferenceType.ForEach(r => AddReferenceType(r.ReferenceTypeName, r.ReferenceTypeDesc));

                    ctx.SaveChanges();
                }


                success = true;
            }
            catch (Exception ex)
            {
                SaveException(Structs.Project.LcapasCore, Structs.Class.LcapasLogic, "SeedProgramCampusTerms", "Error", ex.ToString());
            }

            return success;

        }


        #endregion

    
    }
}


/// <summary>
/// Parse/Create ElectronicDeliveryType
/// </summary>
/// <param name="deliveryType"></param>
/// <returns></returns>
//public ElectronicDelivery ParseElectronicDeliveryType(Library.Apas.AcademicRecord.ElectronicDeliveryType deliveryType)
//{
//    ElectronicDelivery _ElectronicDelivery = new ElectronicDelivery();

//    try
//    {
//        _ElectronicDelivery.ElectronicFormatType = deliveryType.ElectronicFormat;
//        _ElectronicDelivery.ElectronicMethodType = deliveryType.ElectronicMethod;
//        _ElectronicDelivery.ServiceProvider = deliveryType.ServiceProvider ?? string.Empty;

//        var electronicDelivery = ctx.ElectronicDeliveries.Where(e => e.ElectronicFormatType == _ElectronicDelivery.ElectronicFormatType &&
//                                                                       e.ElectronicMethodType == _ElectronicDelivery.ElectronicMethodType &&
//                                                                       e.ServiceProvider == _ElectronicDelivery.ServiceProvider).OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();

//        if (electronicDelivery != null)
//        {
//            _ElectronicDelivery = electronicDelivery;
//        }
//        else
//        {
//            _ElectronicDelivery.CreatedBy = _UserName;
//            _ElectronicDelivery.CreatedDateTime = _XmlCreatedDate;
//        }

//        _ElectronicDelivery.ModifiedBy = _UserName;
//        _ElectronicDelivery.ModifiedDateTime = _XmlCreatedDate;
//    }
//    catch (Exception ex)
//    {
//        SaveException(Structs.Project.LcapasCore, Structs.Class.TranscriptsController, "ParseElectronicDeliveryType", "Error", ex.ToString());
//    }

//    return _ElectronicDelivery;
//}

/// <summary>
/// Parse/Create Order Fee
/// </summary>
/// <param name="orderFee"></param>
/// <returns></returns>
//public OrderFee ParseOrderFee(Library.Apas.AcademicRecord.OrderFeeType orderFee)
//{
//    OrderFee _OrderFee = new OrderFee();

//    try
//    {
//        _OrderFee.FeeAmount = orderFee.FeeAmount;
//        _OrderFee.FeeStatusCodeType = orderFee.FeeStatusCode;
//        _OrderFee.FeeStatusReason = orderFee.FeeStatusReason;

//        var orderfees = ctx.OrderFees.Where(o => o.FeeAmount == _OrderFee.FeeAmount &&
//                                               o.FeeStatusCodeType == _OrderFee.FeeStatusCodeType &&
//                                               o.FeeStatusReason == _OrderFee.FeeStatusReason);

//        if (orderfees.Any())
//        {
//            _OrderFee = orderfees.OrderByDescending(x => x.ModifiedDateTime).FirstOrDefault();
//        }
//        else
//        {
//            _OrderFee.CreatedBy = _UserName;
//            _OrderFee.CreatedDateTime = _XmlCreatedDate;
//        }

//        _OrderFee.ModifiedBy = _UserName;
//        _OrderFee.ModifiedDateTime = _XmlCreatedDate;
//    }
//    catch (Exception ex)
//    {
//        SaveException(Structs.Project.LcapasCore, Structs.Class.TranscriptsController, "ParseOrderFee", "Error", ex.ToString());
//    }

//    return _OrderFee;
//}
