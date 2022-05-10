using Ellucian.Colleague.Configuration;
using Ellucian.Data.Colleague;
using Ellucian.Data.Colleague.Repositories;
using Ellucian.Dmi.Client;
using Lcapas.CollWebApi.DataContracts;
using Lcapas.Core.Library;
using Lcapas.Core.Logic;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Lcapas.CollWebApi
{
    public class MainDriver
    {

        public bool GetXmlTranscripts(string trangroup, string transactionTranscriptUuid)
        {
            bool success = false;
            string stuId = string.Empty;
            ColleagueTransactionInvoker invoker = null;

            try
            {
                Tuple<ColleagueDataReader, ColleagueTransactionInvoker> connectionTuple = EstablishConnectionAsync();
                invoker = connectionTuple.Item2;
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasAdmin, "GetXmlTranscripts", "EstablishConnectionAsync", "Error", "The login credentials are invalid! Error: " + ex.ToString());
                }
            }

            TransactionInvoker _TransactionInvoker = new TransactionInvoker();

            if (!string.IsNullOrWhiteSpace(transactionTranscriptUuid))
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    stuId = lcapasLogic.GetStuIdByTranTranUuid(transactionTranscriptUuid);
                }

                ContractRequest_GetXmlTran request = new ContractRequest_GetXmlTran()
                {
                    StudentId = stuId,
                    TranscriptGrouping = trangroup,
                };

                success = _TransactionInvoker.GetXmlTranscript(invoker, transactionTranscriptUuid, request);
            }
            return success;
        }

        public bool InsertCourse(CourseExportObj course)
        {
            bool success = false;
            ColleagueTransactionInvoker invoker = null;

            try
            {
                Tuple<ColleagueDataReader, ColleagueTransactionInvoker> connectionTuple = EstablishConnectionAsync();
                invoker = connectionTuple.Item2;
            }
            catch (Exception ex)
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasAdmin, "InsertCourse", "EstablishConnectionAsync", "Error", "The login credentials are invalid! Error: " + ex.ToString());
                }
            }

            TransactionInvoker _TransactionInvoker = new TransactionInvoker();

            success = _TransactionInvoker.InsertCourse(invoker, course);

            return success;
        }

        public bool SaveRequestLog(Core.Models.Lcappsdb.TransactionRequestLog _TransactionRequestLog)
        {
            bool success = false;
            bool duplicateRequest = false;
            DateTime? _DateProduced = null, _RequestDate = null;
            string _RecipientId = null;
            ColleagueTransactionInvoker invoker = null;

            if (_TransactionRequestLog != null)
            {
                // Get Produced Date (When a transcript or response was sent)
                using (LcapasLogic lcapasLogic = new LcapasLogic())
                {
                    Core.Models.Lcappsdb.TransmissionData _TransmissionData = lcapasLogic.GetTransmissionDataByUUID(_TransactionRequestLog.TransactionRequestLogUuid);
                    Core.Models.Lcappsdb.Request _Request = lcapasLogic.GetRequestByRequestTrackingId(_TransmissionData.RequestTrackingID);
                    if (_Request != null && _Request.ReceivedDateTime != null)
                    {
                        _RequestDate = _Request.ReceivedDateTime.Value.Date;  // .ToString("yyyy-MM-dd hh:mm:ss.fff");
                    }
                    Core.Models.Lcappsdb.Response _Response = lcapasLogic.GetResponseByRequestTrackingId(_TransmissionData.RequestTrackingID);
                    if (_Response != null &&
                        (!_Response.ResponseStatusType.HasValue || _Response.ResponseStatusType == Core.Library.Apas.AcademicRecord.ResponseStatusType.TranscriptSent) &&
                        _Response.SentDateTime != null) {
                        _DateProduced = _Response.SentDateTime.Value.Date;
                    }

                    // Check if Recipient ID is null and try to update it
                    _RecipientId = _TransactionRequestLog.RecipientId;
                    if (string.IsNullOrWhiteSpace(_RecipientId) && !string.IsNullOrWhiteSpace(_TransmissionData.DestinationInstitution.LocalOrganizationID))
                    {
                        using (ColleagueLogic collLogic = new ColleagueLogic())
                        {
                            _RecipientId = collLogic.GetColleagueInstitutionId(_TransmissionData.DestinationInstitution.LocalOrganizationID);
                        }
                        lcapasLogic.SaveTransactionRequestLogRecipientId(_TransactionRequestLog.TransactionRequestLogUuid, _RecipientId);
                    }
                }

                // Load request information to be saved into Colleague
                ContractRequest_InsertReqLog request = new ContractRequest_InsertReqLog()
                {
                    StudentId = _TransactionRequestLog.StudentId,
                    TranscriptGrouping = _TransactionRequestLog.TranscriptGrouping,
                    RequestType = _TransactionRequestLog.RequestType,
                    RequestDate = _RequestDate.Value.Date.FormatColleagueDate(),
                    RecipientId = _RecipientId,
                    RecipientName = _TransactionRequestLog.RecipientName.ToTitleCase(),
                    RecipientAddressLines = _TransactionRequestLog.RecipientAddressLines.ToTitleCase(),
                    RecipientCity = _TransactionRequestLog.RecipientCity.ToTitleCase(),
                    RecipientState = _TransactionRequestLog.RecipientState,
                    RecipientPostalCode = _TransactionRequestLog.RecipientPostalCode,
                    RecipientCountryCode = _TransactionRequestLog.RecipientCountryCode,
                    NumberOfCopies = _TransactionRequestLog.NumberOfCopies,
                    Comments = _TransactionRequestLog.Comments,
                    RequestHoldCode = _TransactionRequestLog.RequestHoldCode,
                };

                if (_DateProduced != null)
                {
                    request.DateProduced = _DateProduced.Value.Date.FormatColleagueDate();
                }

                // Check for duplicate Request
                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    DateTime requestDate = DateTime.Now;
                    if (!DateTime.TryParse(_RequestDate.ToString(), out requestDate)) { requestDate = DateTime.Now; }
                    duplicateRequest = collLogic.CheckRequestAlreadyExists(request.StudentId, request.RecipientId, requestDate, _DateProduced);
                }

                if (!string.IsNullOrWhiteSpace(request.StudentId) && !duplicateRequest)
                {
                    // Establish Connection with Colleague
                    try
                    {
                        Tuple<ColleagueDataReader, ColleagueTransactionInvoker> connectionTuple = EstablishConnectionAsync();
                        invoker = connectionTuple.Item2;
                    }
                    catch (Exception ex)
                    {
                        using (LcapasLogic lcapasLogic = new LcapasLogic())
                        {
                            lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Project.LcapasCore, "SaveRequestLog", "Error", "The login credentials are invalid! Error: " + ex.ToString());
                        }
                    }

                    // Insert Request into Colleague
                    TransactionInvoker _TransactionInvoker = new TransactionInvoker();
                    success = _TransactionInvoker.InsertStudentRequest(invoker, _TransactionRequestLog.TransactionRequestLogUuid, request);
                } else
                {
                    if (duplicateRequest)
                    {
                        // If TRRQ already exists: Mark Request as Sent to TRRQ and close/complete pending job
                        using (LcapasLogic lcapasLogic = new LcapasLogic())
                        {
                            success = lcapasLogic.MarkRequestAsSentToTRRQ(_TransactionRequestLog.TransactionRequestLogUuid);
                        }
                    }
                }
            }
            return success;
        }

        public bool SaveMyCredsRequestLog(Core.Models.Lcappsdb.TransactionRequestLog _TransactionRequestLog, DateTime? _RequestedDate = null, DateTime? _ProducedDate = null)
        {
            bool success = false;
            bool duplicateRequest = false;
            DateTime? _DateRequest = null;
            ColleagueTransactionInvoker invoker = null;

            if (_TransactionRequestLog != null)
            {
                // Load request information to be saved into Colleague
                _DateRequest = (_RequestedDate ?? _TransactionRequestLog.CreatedDateTime ?? DateTime.Now);
                ContractRequest_InsertReqLog request = new ContractRequest_InsertReqLog()
                {
                    StudentId = _TransactionRequestLog.StudentId,
                    TranscriptGrouping = _TransactionRequestLog.TranscriptGrouping,
                    RequestType = _TransactionRequestLog.RequestType,
                    RequestDate = _DateRequest.Value.Date.FormatColleagueDate(),
                    RecipientId = _TransactionRequestLog.RecipientId,
                    RecipientName = _TransactionRequestLog.RecipientName.ToTitleCase(),
                    RecipientAddressLines = _TransactionRequestLog.RecipientAddressLines.ToTitleCase(),
                    RecipientCity = _TransactionRequestLog.RecipientCity.ToTitleCase(),
                    RecipientState = _TransactionRequestLog.RecipientState,
                    RecipientPostalCode = _TransactionRequestLog.RecipientPostalCode,
                    RecipientCountryCode = _TransactionRequestLog.RecipientCountryCode,
                    NumberOfCopies = _TransactionRequestLog.NumberOfCopies,
                    Comments = _TransactionRequestLog.Comments,
                    RequestHoldCode = _TransactionRequestLog.RequestHoldCode,
                };

                if (_ProducedDate != null)
                {
                    request.DateProduced = _ProducedDate.Value.Date.FormatColleagueDate();
                }

                // Check for duplicate Request
                using (ColleagueLogic collLogic = new ColleagueLogic())
                {
                    DateTime requestDate = DateTime.Now;
                    if (!DateTime.TryParse(_DateRequest.ToString(), out requestDate)) { requestDate = DateTime.Now; }
                    duplicateRequest = collLogic.CheckRequestAlreadyExists(request.StudentId, request.RecipientId, requestDate, _ProducedDate);
                }

                if (!string.IsNullOrWhiteSpace(request.StudentId) && !duplicateRequest)
                {
                    // Establish Connection with Colleague
                    try
                    {
                        Tuple<ColleagueDataReader, ColleagueTransactionInvoker> connectionTuple = EstablishConnectionAsync();
                        invoker = connectionTuple.Item2;
                    }
                    catch (Exception ex)
                    {
                        using (LcapasLogic lcapasLogic = new LcapasLogic())
                        {
                            lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Project.LcapasCore, "SaveMyCredsRequestLog", "Error", "The login credentials are invalid! Error: " + ex.ToString());
                        }
                    }

                    // Insert Request into Colleague
                    TransactionInvoker _TransactionInvoker = new TransactionInvoker();
                    success = _TransactionInvoker.InsertStudentRequest(invoker, _TransactionRequestLog.TransactionRequestLogUuid, request, false);

                    // Update Delivery Method to EDI
                    if (!string.IsNullOrWhiteSpace(_TransactionRequestLog.TransactionRequestLogUuid))
                    {
                        => SaveMyCredsRequestLog Delivery Method
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Connects to the application server's application listener asynchronously using a web user login. Returns 
        /// authenticated data reader and transaction invoker instances.
        /// </summary>
        /// <param name="reader">The authenticated data reader.</param>
        /// <param name="invoker">The authenticated transaction invoker.</param>
        /// <exception cref="System.Exception">The login/password you entered is invalid.</exception>
        private Tuple<ColleagueDataReader, ColleagueTransactionInvoker> EstablishConnectionAsync()
        {
            // read dmi from web config
            //ColleagueLogin login = new ColleagueLogin();

            //string configvalue1 = ConfigurationManager.AppSettings[""];

            StandardLoginRequest loginReq = new StandardLoginRequest();
            DmiSettings dmiSettings = new DmiSettings();
            using (LcapasLogic lcapasLogic = new LcapasLogic())
            {
                // Set Colleague Access Account
                loginReq.UserID = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LcIntegrationUserName);
                loginReq.Password = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LcIntegrationPassword);

                // Set Dmi Settings, based on the actual environment
                string env = Functions.GetEnvironment();
                switch (env)
                {
                    case Structs.Environment.Test:
                        dmiSettings.AccountName = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.DmiTestAccountName);
                        dmiSettings.IpAddress = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.DmiTestIpAddress);
                        dmiSettings.Port = lcapasLogic.GetSetting(Structs.SettingTypes.Integer, Structs.Settings.DmiTestPort);
                        dmiSettings.Secure = lcapasLogic.GetSetting(Structs.SettingTypes.Boolean, Structs.Settings.DmiTestSecure);
                        dmiSettings.SharedSecret = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.DmiTestSharedSecret);
                        break;
                    case Structs.Environment.ROTest:
                        dmiSettings.AccountName = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.DmiROTestAccountName);
                        dmiSettings.IpAddress = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.DmiROTestIpAddress);
                        dmiSettings.Port = lcapasLogic.GetSetting(Structs.SettingTypes.Integer, Structs.Settings.DmiROTestPort);
                        dmiSettings.Secure = lcapasLogic.GetSetting(Structs.SettingTypes.Boolean, Structs.Settings.DmiROTestSecure);
                        dmiSettings.SharedSecret = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.DmiROTestSharedSecret);
                        break;
                    case Structs.Environment.Patch:
                        dmiSettings.AccountName = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.DmiPatchAccountName);
                        dmiSettings.IpAddress = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.DmiPatchIpAddress);
                        dmiSettings.Port = lcapasLogic.GetSetting(Structs.SettingTypes.Integer, Structs.Settings.DmiPatchPort);
                        dmiSettings.Secure = lcapasLogic.GetSetting(Structs.SettingTypes.Boolean, Structs.Settings.DmiPatchSecure);
                        dmiSettings.SharedSecret = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.DmiPatchSharedSecret);
                        break;
                    case Structs.Environment.Dev:
                        dmiSettings.AccountName = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.DmiDevAccountName);
                        dmiSettings.IpAddress = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.DmiDevIpAddress);
                        dmiSettings.Port = lcapasLogic.GetSetting(Structs.SettingTypes.Integer, Structs.Settings.DmiDevPort);
                        dmiSettings.Secure = lcapasLogic.GetSetting(Structs.SettingTypes.Boolean, Structs.Settings.DmiDevSecure);
                        dmiSettings.SharedSecret = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.DmiDevSharedSecret);
                        break;
                    default:  // Prod
                        dmiSettings.AccountName = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.DmiProdAccountName);
                        dmiSettings.IpAddress = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.DmiProdIpAddress);
                        dmiSettings.Port = lcapasLogic.GetSetting(Structs.SettingTypes.Integer, Structs.Settings.DmiProdPort);
                        dmiSettings.Secure = lcapasLogic.GetSetting(Structs.SettingTypes.Boolean, Structs.Settings.DmiProdSecure);
                        dmiSettings.SharedSecret = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.DmiProdSharedSecret);
                        break;
                }

                dmiSettings.ConnectionPoolSize = 1;
                dmiSettings.HostNameOverride = "";
            }

            ColleagueLogin login = new ColleagueLogin(dmiSettings);

            // Async
            // StandardDmiSession session = await login.StandardColleagueLoginAsync(loginReq);

            // Synchronous
            StandardDmiSession session = login.StandardColleagueLogin(loginReq);

            if (string.IsNullOrWhiteSpace(session.SecurityToken))
            {
                using (LcapasLogic lcapasLogic = new LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasAdmin, "TransactionInvoker", "EstablishConnectionAsync", "Error", "The login credentials are invalid! - LcIntegrationUserName: " + loginReq.UserID + " - Dmi Account Name: " + dmiSettings.AccountName);
                }
            }

            ColleagueDataReader reader = new ColleagueDataReader(session);
            ColleagueTransactionInvoker invoker = new ColleagueTransactionInvoker(session);
            // Console.WriteLine("Successful.");

            return new Tuple<ColleagueDataReader, ColleagueTransactionInvoker>(reader, invoker);
        }
    }
}



