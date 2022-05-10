using Lcapas.Core.Library;
using Lcapas.Core.Models.Lcappsdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;

namespace Lcapas.Core.Logic
{
    public class ApasLogic : IDisposable
    {
        public quesvc.APASQueueService qs = new quesvc.APASQueueService();
        public quesvc2.APASQueueServiceExt2 qs2 = new quesvc2.APASQueueServiceExt2();
        public distsvc.DistributedSessionService distsvc = new distsvc.DistributedSessionService();
        public toksvc.RefreshTokenServices toksvc = new toksvc.RefreshTokenServices();
        public instsvc.InstitutionInformationAndCodesServices instsvc = new instsvc.InstitutionInformationAndCodesServices();

        private LcapasLogic lcapasLogic = new LcapasLogic();

        public ApasLogic() {
            string ApasUrl = string.Empty;
            string ProxyUrl = string.Empty;
            try
            {
                string env = Functions.GetEnvironment();

                switch (env) {
                    case Structs.Environment.Prod:
                        ApasUrl = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.ApasProdPath);
                        ProxyUrl = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.ApasProxyProdPath);
                        break;
                    case Structs.Environment.Dev:
                    case Structs.Environment.Test:
                    case Structs.Environment.ROTest:
                    case Structs.Environment.Patch:
                        ApasUrl = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.ApasSuppPath);
                        ProxyUrl = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.ApasProxyProdSuppPath);
                        break;
                }

                qs.Url = ProxyUrl + lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.QueueSvcPath);
                qs2.Url = ProxyUrl + lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.QueueSvc2Path);
                distsvc.Url = ApasUrl + lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.DistSessSvcPath);
                toksvc.Url = ApasUrl + lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.RefTokSvcPath);
                instsvc.Url = ApasUrl + lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.InstInfoPath);

            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "ApasLogic", "Error", ex.ToString());
            }
        }

        public bool GetApasHostInstitution()
        {
            bool success = false;

            try
            {
                quesvc.RegisteredEducationalInstitution hostInstitution = qs.GetHostInstitution();

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "getApasHostInstitution", "Error", ex.ToString());
            }

            return success;
        }

        public bool GetApasEducationalInstitutions()
        {
            bool success = false;

            try
            {
                IEnumerable<quesvc.RegisteredEducationalInstitution> institutions = qs.GetRegisteredEducationalInstitutions();

                string list = string.Empty;
                foreach (var item in institutions)
                {
                    list = list + item.SourceId + " - " + item.EducationalInstitutionName + ", ";
                }

                //success = lcapasLogic.SetValidApasInstitions(institutions.ToList());
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "getApasEducationalInstitutions", "Error", ex.ToString());
            }

            return success;
        }


        // APPLICATIONS - GET
        public List<string> GetPastApplications(int days, string UUID = null, string fromDate = null, string toDate = null)
        {
            List<string> _ReturnedAppIds = new List<string>();
            List<string> _AdmAppUUIDs = new List<string>();

            try
            {
                // set filters
                quesvc.SentApplicationsFilter _StatDataFilter = new quesvc.SentApplicationsFilter();
                quesvc.ReceivedApplicationsFilter _AdmAppfilter = new quesvc.ReceivedApplicationsFilter();

                if (!string.IsNullOrWhiteSpace(UUID))  // filter by UUID
                {
                    _StatDataFilter.UUID = UUID;
                    _AdmAppfilter.UUID = UUID;
                }
                else
                {
                    _StatDataFilter.SentFrom = DateTime.Now.AddDays(-days);
                    _AdmAppfilter.ReceivedFrom = DateTime.Now.AddDays(-days);

                    DateTime fromDateValue, toDateValue;
                    if (!string.IsNullOrWhiteSpace(fromDate) && DateTime.TryParse(fromDate, out fromDateValue))
                    {
                        _StatDataFilter.SentFrom = fromDateValue;
                        _AdmAppfilter.ReceivedFrom = fromDateValue;
                    }
                    if (!string.IsNullOrWhiteSpace(toDate) && DateTime.TryParse(toDate, out toDateValue))
                    {
                        _StatDataFilter.SentTo = toDateValue;
                        _AdmAppfilter.ReceivedTo = toDateValue;
                    }
                }

                IEnumerable<quesvc.ReceivedApplication> _ReceivedApplications = qs.GetReceivedApplications(_AdmAppfilter);
                IEnumerable<quesvc.SentApplication> _ReturnedApplications = qs.GetSentApplications(_StatDataFilter);

                foreach (quesvc.ReceivedApplication ra in _ReceivedApplications)
                {
                    foreach (quesvc.SentApplication sa in _ReturnedApplications)
                    {
                        Library.Apas.StatisticalData.StatisticalDataType _StatisticalData = LcapasLogic.DeserializeStatData(sa.Application.ApplicationBody);
                        if (_StatisticalData.ApplicationStage == Library.Apas.StatisticalData.ApplicationStageType.Submitted)
                        {
                            if (ra.Application.ApplicationBody.Contains(_StatisticalData.ApplicationID.ToString()) && !_AdmAppUUIDs.Contains(_StatisticalData.ApplicationID.ToString()))
                            {
                                ApasMessage _Message = new ApasMessage()
                                {
                                    UUID = ra.UUID.ToString(),
                                    ASN = ra.AgencyAssignedID,
                                    MessageType = Enums.MessageTypes.Application,
                                    MessageXML = ra.Application.ApplicationBody,
                                    SenderId = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                                    ReceiverId = ra.EducationalInstitutionSourceId,
                                };

                                lcapasLogic.SaveMessage(_Message);

                                lcapasLogic.ParseApplicationMessage(ra.UUID.ToString(), toDate);

                                _AdmAppUUIDs.Add(ra.UUID.ToString());
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "GetPastApplications", "Error", ex.ToString());
            }

            return _AdmAppUUIDs;
        }

        public bool GetApasSentApplicationByUUID(string uuid)
        {
            bool success = false;

            try
            {
                quesvc.SentApplicationsFilter sentApplicationsFilter = new quesvc.SentApplicationsFilter()
                {
                    UUID = uuid
                };

                success = GetApasSentApplications(sentApplicationsFilter);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "sentApasApplicationByUUID", "Error", ex.ToString());
            }

            return success;
        }

        public bool GetApasReceivedApplicationByUUID(string uuid)
        {
            bool success = false;

            try
            {
                quesvc.ReceivedApplicationsFilter receivedApplicationsFilter = new quesvc.ReceivedApplicationsFilter()
                {
                    UUID = uuid
                };
                success = GetApasReceivedApplications(receivedApplicationsFilter);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "retrieveApasApplicationByUUID", "Error", ex.ToString());
            }

            return success;
        }

        private bool GetApasReceivedApplications(quesvc.ReceivedApplicationsFilter receivedApplicationFilter)
        {
            bool success = false;
            try
            {
                IEnumerable<quesvc.ReceivedApplication> receivedApplications = qs.GetReceivedApplications(receivedApplicationFilter);

                foreach (quesvc.ReceivedApplication item in receivedApplications.ToArray())
                {
                    ApasMessage _Message = new ApasMessage()
                    {
                        UUID = item.UUID.ToString(),
                        ASN = item.AgencyAssignedID,
                        MessageType = Enums.MessageTypes.Application,
                        MessageXML = item.Application.ApplicationBody,
                        SenderId = item.EducationalInstitutionSourceId,
                        ReceiverId = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                    };
                    lcapasLogic.SaveMessage(_Message);
                }
                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "getApasReceivedApplications", "Error", ex.ToString());
            }

            return success;
        }

        private bool GetApasReceivedApplicationsByConsumer(string serviceConsumerId)
        {
            bool success = false;

            try
            {
                // get
                IEnumerable<quesvc2.ReceivedApplication> receivedApplications = qs2.GetReceivedApplicationsByConsumer(serviceConsumerId);

                foreach (quesvc2.ReceivedApplication item in receivedApplications.ToArray())
                {
                    ApasMessage _Message = new ApasMessage()
                    {
                        UUID = item.UUID.ToString(),
                        ASN = item.AgencyAssignedID,
                        MessageType = Enums.MessageTypes.Application,
                        MessageXML = item.Application.ApplicationBody,
                        SenderId = item.EducationalInstitutionSourceId,
                        ReceiverId = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                    };
                    lcapasLogic.SaveMessage(_Message);
                }

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "getApasReceivedApplicationsByConsumer", "Error", ex.ToString());
            }

            return success;
        }

        private bool GetApasSentApplications(quesvc.SentApplicationsFilter sentApplicationsFilter)
        {
            bool success = false;
            try
            {
                // Get sent application message used to close/stop apply alberta application
                IEnumerable<quesvc.SentApplication> sentApplications = qs.GetSentApplications(sentApplicationsFilter);

                foreach (quesvc.SentApplication item in sentApplications.ToArray())
                {
                    ApasMessage _Message = new ApasMessage()
                    {
                        UUID = item.UUID.ToString(),
                        ASN = item.AgencyAssignedId,
                        MessageType = Enums.MessageTypes.Application,
                        MessageXML = item.Application.ApplicationBody,
                        SenderId = item.RecipientSourceId,
                        ReceiverId = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                    };
                    lcapasLogic.SaveMessage(_Message);
                }
                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "getApasSentApplications", "Error", ex.ToString());
            }

            return success;
        }

        // APPLICATIONS - SEND
        public bool SendApasApplication(string uuid)
        {
            bool _Success = false;
            quesvc.Application _Application = new quesvc.Application();
            quesvc.SentApplication _ApplicationToSend = new quesvc.SentApplication();

            try
            {
                ApplicationMessage _ApplicationMessage = lcapasLogic.GetApplicationMessage(uuid);

                string tempXml = _ApplicationMessage.StudentApplication.ApplicationMessages.First().ApplicationBody;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(tempXml);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                nsmgr.AddNamespace("AdmApp", Structs.SchemaVersion.AdmissionsApplication_v1_3_0);

                XmlElement root = xmlDoc.DocumentElement;

                XmlNode transmissionData = root.SelectSingleNode("AdmApp:TransmissionData", nsmgr);
                if (transmissionData == null) { transmissionData = root.SelectSingleNode("TransmissionData", nsmgr); }

                XmlNode source = transmissionData.SelectSingleNode("Source");
                XmlNode destination = transmissionData.SelectSingleNode("Destination");

                SwapNodes(source, destination);
                RenameNode(source, "Destination");
                RenameNode(destination, "Source");

                _Application.AgencyAssignedId = _ApplicationMessage.StudentApplication.Student.ASNs.OrderByDescending(x => x.CreatedDateTime).First().AgencyAssignedID;
                _Application.ApplicationBody = xmlDoc.InnerXml;

                _ApplicationToSend.Application = _Application;

                _ApplicationToSend.RecipientSourceId = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode);
                _ApplicationToSend.Sent = DateTime.Now;

                _ApplicationToSend.AgencyAssignedId = _ApplicationMessage.StudentApplication.Student.ASNs.OrderByDescending(x => x.CreatedDateTime).First().AgencyAssignedID;
                _ApplicationToSend.UUID = Guid.NewGuid();

                _Success = SendApasApplication(_ApplicationToSend, uuid);

                _Success = SendApasApplicationStatisticalData(uuid);

            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "sendApasApplication", "Error", ex.ToString());
            }

            return _Success;
        }

        private bool SendApasApplication(quesvc.SentApplication applicationToSend, string uuid)
        {
            bool _Success = false;
            Acknowledgement _Acknowledgement = new Acknowledgement();
            try
            {
                ApplicationMessage _ApplicationMessage = lcapasLogic.GetApplicationMessage(uuid);

                quesvc.Acknowledgment _Message = qs.SendApplication(applicationToSend);

                if (_Message != null)
                {
                    _Acknowledgement.IsSuccessful = _Message.IsSuccessful;
                    _Acknowledgement.Originator = _Message.Originator;
                    _Acknowledgement.OriginatorAPASCode = _Message.OriginatorAPASCode;
                    _Acknowledgement.AcknowledgementValue = _Message.AcknowledgmentValue;

                    _Success = lcapasLogic.SaveNewAcknowledgement(_Acknowledgement, _ApplicationMessage.UUID);

                    if (!_Acknowledgement.IsSuccessful)
                    {
                        lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "SendApasApplication", "Acknowledgement", "AcknowledgementValue" + _Acknowledgement.AcknowledgementValue);
                    }

                    // Save sent application message
                    _Success = GetApasSentApplicationByUUID(_ApplicationMessage.UUID);

                    // Check if there was any Received Error
                    _Success = GetApasReceivedErrors();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "SendApasApplication", "Error", ex.ToString());
            }

            return _Success;
        }


        // TRANSCRIPTS - GET
        public bool GetApasSentTranscriptsByUUID(string uuid)
        {
            bool success = false;

            try
            {
                quesvc.SentTranscriptsFilter sentTranscriptsFilter = new quesvc.SentTranscriptsFilter()
                {
                    UUID = uuid
                };

                success = GetApasSentTranscripts(sentTranscriptsFilter);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "sentApasApplicationByUUID", "Error", ex.ToString());
            }

            return success;
        }

        public bool GetApasSentTranscripts(quesvc.SentTranscriptsFilter sentTranscriptsFilter)
        {
            bool success = false;

            try
            {
                IEnumerable<quesvc.SentTranscript> sentTranscripts = qs.GetSentTranscripts(sentTranscriptsFilter);

                foreach (quesvc.SentTranscript item in sentTranscripts.ToArray())
                {
                    ApasMessage _Message = new ApasMessage()
                    {
                        UUID = item.UUID,
                        ASN = item.AgencyAssignedId,
                        MessageType = Enums.MessageTypes.SentResponse,
                        MessageXML = item.Transcript.TranscriptBody,
                        SenderId = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                        ReceiverId = item.RecipientSourceId,
                    };
                    success = lcapasLogic.SaveMessage(_Message);

                    if (success)
                    {
                        success = lcapasLogic.ParseApasMessage(item.UUID, Enums.MessageTypes.SentResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "GetApasSentTranscripts", "Error", ex.ToString());
            }

            return success;
        }

        private bool GetApasReceivedTranscripts(quesvc.ReceivedTranscriptsFilter receivedTranscriptsFilter)
        {
            bool success = false;

            try
            {
                IEnumerable<quesvc.ReceivedTranscript> transcripts = qs.GetReceivedTranscripts(receivedTranscriptsFilter);

                foreach (quesvc.ReceivedTranscript item in transcripts.ToArray())
                {
                    ApasMessage _Message = new ApasMessage()
                    {
                        UUID = item.UUID,
                        ASN = item.AgencyAssignedID,
                        MessageType = Enums.MessageTypes.ReceivedResponse,
                        MessageXML = item.Transcript.TranscriptBody,
                        SenderId = item.EducationalInstitutionSourceId,
                        ReceiverId = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                    };
                    success = lcapasLogic.SaveMessage(_Message);

                    if (success)
                    {
                        success = lcapasLogic.ParseApasMessage(item.UUID, Enums.MessageTypes.ReceivedResponse);
                    }
                }

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "getApasReceivedTranscripts", "Error", ex.ToString());
            }

            return success;
        }

        public bool GetApasReceivedTranscriptsExt2(quesvc2.ReceivedTranscriptsFilter receivedTranscriptsFilter)
        {
            bool success = false;

            try
            {
                IEnumerable<quesvc2.ReceivedTranscript> receivedTranscripts = qs2.GetReceivedTranscripts(receivedTranscriptsFilter);

                foreach (quesvc2.ReceivedTranscript item in receivedTranscripts)
                {
                    ApasMessage _Message = new ApasMessage()
                    {
                        UUID = item.UUID,
                        ASN = item.AgencyAssignedID,
                        MessageType = Enums.MessageTypes.ReceivedResponse,
                        MessageXML = item.Transcript.TranscriptBody,
                        SenderId = item.EducationalInstitutionSourceId,
                        ReceiverId = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                    };

                    // if message saves correctly then queue response for parsing
                    if (lcapasLogic.SaveMessage(_Message))
                    {
                        success = lcapasLogic.QueueJob(item.UUID, Enums.JobTypeType.ParseReceivedApasResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "getApasReceivedTranscriptsExt2", "Error", ex.ToString());
            }

            return success;
        }

        private bool GetApasReceivedTranscriptsByConsumer(string serviceConsumerId, int max)
        {
            bool success = false;

            try
            {
                // get
                IEnumerable<quesvc2.ReceivedTranscript> receivedTranscripts = qs2.GetReceivedTranscriptsByConsumer(serviceConsumerId, max);

                foreach (quesvc2.ReceivedTranscript item in receivedTranscripts)
                {
                    ApasMessage _Message = new ApasMessage()
                    {
                        UUID = item.UUID,
                        ASN = item.AgencyAssignedID,
                        MessageType = Enums.MessageTypes.ReceivedResponse,
                        MessageXML = item.Transcript.TranscriptBody,
                        SenderId = item.EducationalInstitutionSourceId,
                        ReceiverId = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                    };
                    success = lcapasLogic.SaveMessage(_Message);

                    if (success)
                    {
                        success = lcapasLogic.ParseApasMessage(item.UUID, Enums.MessageTypes.ReceivedResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "getApasReceivedTranscriptsByConsumer", "Error", ex.ToString());
            }

            return success;
        }

        // TRANSCRIPTS - SEND

        public bool SendTransferRequest(string uuid)
        {
            bool success = false;

            try
            {
                Request _Request = lcapasLogic.GetRequest(uuid);

                if (_Request != null)
                {
                    // create the Request to send
                    quesvc.SentTransferRequest _RequestToSend = new quesvc.SentTransferRequest()
                    {
                        AgencyAssignedID = _Request.RequestedStudent.ASNs.OrderByDescending(x => x.CreatedDateTime).Select(y=>y.AgencyAssignedID).FirstOrDefault(),
                        RequestTrackingId = _Request.TransmissionData.RequestTrackingID,
                        Sent = DateTime.Now,
                        UUID = uuid,
                        TransferRequestBody = _Request.TransmissionData.Xml,
                        RecipientSourceId = _Request.TransmissionData.DestinationInstitution.LocalOrganizationID,
                    };

                    // Send Transfer Request
                    quesvc.Acknowledgment _Acknowledgement = qs.SendTransferRequest(_RequestToSend);

                    // Save the Acknowledgement
                    success = SaveApasAcknowledgement(_Acknowledgement, uuid);

                    // Mark Request as Sent
                    if (_Acknowledgement.IsSuccessful) {
                        lcapasLogic.MarkRequestSent(uuid);
                        lcapasLogic.MarkRequestViewed(uuid);
                    }

                    // if Acknowledgement Saved Successfully, save the Apas Message we sent
                    if (success)
                    {
                        ApasMessage _Message = new ApasMessage()
                        {
                            UUID = uuid,
                            ASN = _Request.RequestedStudent.ASNs.Where(a => a.StateProvinceCode == Library.Apas.CoreMain.StateProvinceCodeType.CA).OrderByDescending(x => x.ModifiedDateTime).Select(z => z.AgencyAssignedID).FirstOrDefault(),
                            MessageType = Enums.MessageTypes.SentRequest,
                            MessageXML = _Request.TransmissionData.Xml,
                            SenderId = _Request.TransmissionData.DestinationInstitution.LocalOrganizationID,
                            ReceiverId = _Request.TransmissionData.SourceInstitution.LocalOrganizationID,
                        };

                        success = lcapasLogic.SaveMessage(_Message);
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "SendTransferRequest", "Error", ex.ToString());
            }
            return success;
        }

        /// <summary>
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="transmissionType"></param>
        /// <returns></returns>
        public bool SendTransferResponse(string uuid)
        {
            bool success = false;

            try
            {
                Response _Response = lcapasLogic.GetResponse(uuid);

                if (_Response != null)
                {
                    // Create the Apas Response
                    quesvc.SentTranscript _ResponseToSend = new quesvc.SentTranscript()
                    {
                        AgencyAssignedId = _Response.RequestedStudent.ASNs.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault().AgencyAssignedID,
                        RecipientSourceId = _Response.TransmissionData.DestinationInstitution.LocalOrganizationID,
                        RequestTrackingId = _Response.RequestTrackingID,
                        Sent = DateTime.Now,
                        UUID = Guid.NewGuid().ToString(),
                        ErrorOccurred = false,
                        Transcript = new quesvc.Transcript()
                        {
                            AgencyAssignedId = _Response.RequestedStudent.ASNs.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault().AgencyAssignedID,
                            TranscriptBody = _Response.TransmissionData.Xml,
                        },
                    };

                    // Send Transfer Request
                    quesvc.Acknowledgment _Acknowledgement = qs.SendTrancript(_ResponseToSend);

                    // Save the Acknowledgement
                    success = SaveApasAcknowledgement(_Acknowledgement, uuid);

                    // Save Response Status as TranscriptSent
                    if (_Acknowledgement.IsSuccessful)
                    {
                        lcapasLogic.SaveTranscriptResponseStatus(uuid, _Response.ResponseStatusType ?? Library.Apas.AcademicRecord.ResponseStatusType.TranscriptSent);

                        // Create TransactionRequestLog table and SaveColleagueRequest when a Transcript or Response is sent
                        bool autoRespond = lcapasLogic.GetSetting(Structs.SettingTypes.Boolean, Structs.Settings.AutoRespond);
                        // Get Request TransmissionData UUID
                        Request _Request = lcapasLogic.GetRequestByRequestTrackingId(_Response.RequestTrackingID);

                        if (autoRespond && _Request != null && _Request.TransmissionData != null && !string.IsNullOrWhiteSpace(_Request.TransmissionData.Uuid) &&
                            !lcapasLogic.IsActiveAliasStudent(_Request.RequestedStudent.StudentId, false) &&  // It's not an Test/Alias Student
                            lcapasLogic.CreateRequestLog("PS", _Request.TransmissionData.Uuid))  // Queue new request to save in Colleague (Request Log)
                        {
                            // Queue Job to Import Request (TRRQ) into Colleague
                            lcapasLogic.QueueJob(_Request.TransmissionData.Uuid, Enums.JobTypeType.SaveColleagueRequest);
                        }                            
                    }

                    // if Acknowledgement Saved Successfully, save the Apas Message we sent
                    if (success)
                    {
                        ApasMessage _Message = new ApasMessage()
                        {
                            UUID = uuid,
                            ASN = _Response.RequestedStudent.ASNs.Where(a => a.StateProvinceCode == Library.Apas.CoreMain.StateProvinceCodeType.CA).OrderByDescending(x => x.ModifiedDateTime).Select(z => z.AgencyAssignedID).FirstOrDefault(),
                            MessageType = Enums.MessageTypes.SentResponse,
                            MessageXML = _Response.TransmissionData.Xml,
                            SenderId = _Response.TransmissionData.SourceInstitution.LocalOrganizationID,
                            ReceiverId = _Response.TransmissionData.DestinationInstitution.LocalOrganizationID,
                        };

                        success = lcapasLogic.SaveMessage(_Message);

                        lcapasLogic.MarkRequestViewedByRequestTrackingId(_Response.RequestTrackingID);  // Mark Request as viewed
                        lcapasLogic.MarkResponseViewed(uuid);  // Mark Response/Transcript as viewed
                    }
                } else
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "SendTransferResponse", "Error - Response Not Found!", "TransmissionData Not Found with this UUID: " + uuid);
                    // Cancel Job
                    lcapasLogic.CancelJob(uuid);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "SendTransferResponse", "Error", ex.ToString());
            }
            return success;
        }

        // REQUESTS - GET

        public bool GetApasSentRequestsByUUID(string uuid)
        {
            bool success = false;

            try
            {
                quesvc.SentTransferRequestsFilter sentRequestFilter = new quesvc.SentTransferRequestsFilter()
                {
                    UUID = uuid
                };

                success = GetApasSentTransferRequests(sentRequestFilter);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "sentApasApplicationByUUID", "Error", ex.ToString());
            }

            return success;
        }

        public bool GetApasSentTransferRequests(quesvc.SentTransferRequestsFilter sentTransferRequestsFilter)
        {
            bool success = false;

            try
            {
                // get
                IEnumerable<quesvc.SentTransferRequest> sentTransferRequests = qs.GetSentTransferRequests(sentTransferRequestsFilter);

                foreach (quesvc.SentTransferRequest item in sentTransferRequests.ToArray())
                {
                    ApasMessage _Message = new ApasMessage()
                    {
                        UUID = item.UUID,
                        ASN = item.AgencyAssignedID,
                        MessageType = Enums.MessageTypes.SentRequest,
                        MessageXML = item.TransferRequestBody,
                        SenderId = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                        ReceiverId = item.RecipientSourceId,
                    };
                    success = lcapasLogic.SaveMessage(_Message);

                    if (success)
                    {
                        success = lcapasLogic.ParseApasMessage(item.UUID, Enums.MessageTypes.SentRequest);
                    }
                }

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "getApasSentTransferRequests", "Error", ex.ToString());
            }

            return success;
        }

        public bool GetApasReceivedTransferRequests(quesvc.ReceivedTransferRequestsFilter receivedTransferRequestsFilter)
        {
            bool success = false;

            try
            {
                IEnumerable<quesvc.ReceivedTransferRequest> receivedTransferRequests = qs.GetReceivedTransferRequests(receivedTransferRequestsFilter);

                foreach (quesvc.ReceivedTransferRequest item in receivedTransferRequests.ToArray())
                {
                    string uuid = item.UUID.ToString();

                    ApasMessage _Message = new ApasMessage()
                    {
                        UUID = uuid,
                        ASN = item.AgencyAssignedID,
                        MessageType = Enums.MessageTypes.ReceivedRequest,
                        MessageXML = item.RequestBody,
                        SenderId = item.EducationalInstitutionSourceId,
                        ReceiverId = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                    };

                    // if request message saves properly then queue it for parsing
                    if (lcapasLogic.SaveMessage(_Message))
                    {
                        success = lcapasLogic.QueueJob(item.UUID, Enums.JobTypeType.ParseReceivedTransferRequest);
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "getApasReceivedTransferRequests", "Error", ex.ToString());
            }

            return success;
        }

        /// <summary>
        /// this function automatically gets the latest transfer requests using unique consumerId (might use this)
        /// </summary>
        /// <param name="serviceConsumerId"></param>
        /// <returns></returns>
        private bool GetApasReceivedTransferRequestsByConsumer(string serviceConsumerId)
        {
            bool success = false;

            try
            {
                // get
                IEnumerable<quesvc2.ReceivedTransferRequest> requests = qs2.GetReceivedTransferRequestsByConsumer(serviceConsumerId);


                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "getApasReceivedTransferRequestsByConsumer", "Error", ex.ToString());
            }

            return success;
        }

        // STATISTICAL DATA
        public bool SendApasApplicationStatisticalData(string uuid)
        {
            bool _Success = false;
            quesvc.Application _Application = new quesvc.Application();
            quesvc.SentApplication _ApplicationToSend = new quesvc.SentApplication();
            Library.Apas.StatisticalData.StatisticalDataType _StatisticalData = new Library.Apas.StatisticalData.StatisticalDataType();

            try
            {
                ApplicationMessage _ApplicationMessage = lcapasLogic.GetApplicationMessage(uuid);

                _StatisticalData = lcapasLogic.CreateStatisticalData(_ApplicationMessage.StudentApplication);

                XmlDocument xmlDoc = new XmlDocument();
                string tempXml = _StatisticalData.Serialize("StatData", "urn:ca:applyalberta:message:StatisticalData:v1.0.1");
                xmlDoc.LoadXml(tempXml);

                XmlElement root = xmlDoc.DocumentElement;

                const string xsi = "http://www.w3.org/2001/XMLSchema-instance";
                XmlAttribute schemaLocation = xmlDoc.CreateAttribute("xsi", "schemaLocation", xsi);
                schemaLocation.Value = "urn:ca:applyalberta:message:StatisticalData:v1.0.1 StatisticalData_v1.0.1.xsd";
                root.SetAttributeNode(schemaLocation);

                _Application.AgencyAssignedId = _ApplicationMessage.StudentApplication.Student.ASNs.OrderByDescending(x => x.CreatedDateTime).First().AgencyAssignedID;
                _Application.ApplicationBody = xmlDoc.InnerXml;

                _ApplicationToSend.Application = _Application;

                _ApplicationToSend.RecipientSourceId = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode);
                _ApplicationToSend.Sent = DateTime.Now;

                _ApplicationToSend.AgencyAssignedId = _ApplicationMessage.StudentApplication.Student.ASNs.OrderByDescending(x => x.CreatedDateTime).First().AgencyAssignedID;
                _ApplicationToSend.UUID = Guid.NewGuid();

                _Success = SendApasApplication(_ApplicationToSend, uuid);

                // Save sent Statistical Dataset in our Message table
                _Success = SaveStatisticalDataMessage(uuid, _StatisticalData, xmlDoc.InnerXml);

            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "sendApasApplication", "Error", ex.ToString());
            }

            return _Success;
        }

        private bool SaveStatisticalDataMessage(string uuid, Library.Apas.StatisticalData.StatisticalDataType statisticalData, string XmlMessage)
        {
            bool success = false;
            try
            {
                ApplicationMessage _ApplicationMessage = lcapasLogic.GetApplicationMessage(uuid);

                ApasMessage _Message = new ApasMessage()
                {
                    UUID = _ApplicationMessage.UUID,
                    ASN = _ApplicationMessage.StudentApplication.Student.ASNs.Where(a => a.StateProvinceCode == Library.Apas.CoreMain.StateProvinceCodeType.CA).OrderByDescending(x => x.ModifiedDateTime).Select(z => z.AgencyAssignedID).FirstOrDefault(),
                    MessageType = Enums.MessageTypes.StatisticalData,
                    MessageXML = XmlMessage,
                    SenderId = lcapasLogic.GetSetting(Structs.SettingTypes.String, Structs.Settings.LocalAPASCode),
                    ReceiverId = _ApplicationMessage.SourceInstitution.LocalOrganizationID,
                };
                lcapasLogic.SaveMessage(_Message);

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "getApasSentApplications", "Error", ex.ToString());
            }

            return success;
        }



        public bool SendApasMessage(string uuid)
        {
            bool success = false;

            try
            {
                quesvc.Acknowledgment _Acknowledgement = new quesvc.Acknowledgment();

                var _ApasMessage = lcapasLogic.GetApasMessage(uuid);

                if (_ApasMessage != null)
                {
                    switch (_ApasMessage.MessageType)
                    {
                        case Enums.MessageTypes.Application:
                            break;
                        case Enums.MessageTypes.ReceivedRequest:
                            break;
                        case Enums.MessageTypes.SentRequest:
                            // create the Request to send
                            quesvc.SentTransferRequest _RequestToSend = new quesvc.SentTransferRequest()
                            {
                                AgencyAssignedID = _ApasMessage.ASN,
                                RequestTrackingId = "RTD480270002018022614000890990fa123", // Functions.GetNewRequestTrackingId(),
                                //RequestTrackingId = _Request.TransmissionData.RequestTrackingID,
                                Sent = DateTime.Now,
                                UUID = uuid,
                                TransferRequestBody = _ApasMessage.MessageXML,
                                RecipientSourceId = _ApasMessage.SenderId,
                            };

                            // Send Transfer Request
                            _Acknowledgement = qs.SendTransferRequest(_RequestToSend);
                            break;
                        case Enums.MessageTypes.ReceivedResponse:
                            break;
                        case Enums.MessageTypes.SentResponse:
                            break;
                        case Enums.MessageTypes.StatisticalData:
                            break;
                        default:
                            break;
                    }

                    // Save the Acknowledgement
                    success = SaveApasAcknowledgement(_Acknowledgement, uuid);

                    // if Acknowledgement Saved Successfully, save the Apas Message we sent
                    if (success)
                    {
                        ApasMessage _Message = new ApasMessage()
                        {
                            UUID = uuid,
                            ASN = _ApasMessage.ASN,
                            MessageType = _ApasMessage.MessageType,
                            MessageXML = _ApasMessage.MessageXML,
                            SenderId = _ApasMessage.SenderId,
                            ReceiverId = _ApasMessage.ReceiverId,
                        };

                        success = lcapasLogic.SaveMessage(_Message);
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "SendApasMessage", "Error", ex.ToString());
            }
            return success;
        }

        /// <summary>
        /// TODO: Make sure this is sound logic and generic
        /// i.e. does this really represent the error at hand?
        /// I don't think it does
        /// </summary>
        /// <returns></returns>
        public bool GetApasReceivedErrors()
        {
            bool success = false;

            try
            {
                IEnumerable<quesvc.ReceivedError> receivedErrors = qs.GetReceivedErrors();

                if (receivedErrors.Any())
                {
                    foreach (quesvc.ReceivedError errormsg in receivedErrors)
                    {
                        string requestTrackingID = null;
                        try
                        {
                            requestTrackingID = errormsg.ErrorMessage.GetStringBetween("<RequestTrackingID>", "</RequestTrackingID>");
                        }
                        catch (Exception ex)
                        {
                            lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "getApasReceivedErrors - DeserializeErrorMessage", "Error", ex.ToString());
                        }

                        ReceivedError _RecievedError = new ReceivedError()
                        {
                            ErrorMessage = errormsg.ErrorMessage,
                            OriginalMessageBody = errormsg.OriginalMessageBody,
                            OriginalMessageType = errormsg.OriginalMessagePayloadType,
                            UUID = errormsg.UUID.ToString(),
                            RequestTrackingID = requestTrackingID,
                            ReceivedDateTime = errormsg.Received,
                        };
                        lcapasLogic.SaveApasError(_RecievedError);
                    }

                    success = false;  // Received error found!
                }
                else
                {
                    success = true;  // No received error found and no execution error.
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "getApasReceivedErrors", "Error", ex.ToString());
            }

            return success;
        }

        private bool GetApasReceivedErrorsByConsumer(string serviceConsumerId)
        {
            bool success = false;

            try
            {
                IEnumerable<quesvc2.ReceivedError> receivedErrors = qs2.GetReceivedErrorsByConsumer(serviceConsumerId);
                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "getApasReceivedErrorsByConsumer", "Error", ex.ToString());
            }

            return success;
        }


        public bool SaveApasAcknowledgement(quesvc.Acknowledgment acknowledgement, string uuid)
        {
            bool success = false;
            Acknowledgement _Acknowledgement = new Acknowledgement();
            try
            {

                if (_Acknowledgement != null)
                {
                    _Acknowledgement.IsSuccessful = acknowledgement.IsSuccessful;
                    _Acknowledgement.Originator = acknowledgement.Originator;
                    _Acknowledgement.OriginatorAPASCode = acknowledgement.OriginatorAPASCode;
                    _Acknowledgement.AcknowledgementValue = acknowledgement.AcknowledgmentValue;

                    success = lcapasLogic.SaveNewAcknowledgement(_Acknowledgement, uuid, true);

                    if (!_Acknowledgement.IsSuccessful)
                    {
                        lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "sendApasTranscript", "Acknowledgement", "AcknowledgementValue" + _Acknowledgement.AcknowledgementValue);
                    }

                    success = GetApasReceivedErrors();
                }

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "sendApasTransferRequest", "Error", ex.ToString());
            }

            return success;
        }

        private bool SendApasTranscript(quesvc.SentTranscript transcriptToSend, string uuid)
        {
            bool success = false;
            Acknowledgement _Acknowledgement = new Acknowledgement();
            try
            {
                quesvc.Acknowledgment _Message = qs.SendTrancript(transcriptToSend);

                if (_Acknowledgement != null)
                {
                    _Acknowledgement.IsSuccessful = _Message.IsSuccessful;
                    _Acknowledgement.Originator = _Message.Originator;
                    _Acknowledgement.OriginatorAPASCode = _Message.OriginatorAPASCode;
                    _Acknowledgement.AcknowledgementValue = _Message.AcknowledgmentValue;

                    success = lcapasLogic.SaveNewAcknowledgement(_Acknowledgement, uuid, true);

                    if (!_Acknowledgement.IsSuccessful)
                    {
                        lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "sendApasTranscript", "Acknowledgement", "AcknowledgementValue" + _Acknowledgement.AcknowledgementValue);
                    }

                    // Save sent application message
                    success = GetApasSentApplicationByUUID(transcriptToSend.UUID);

                    // Check if there was any Received Error
                    success = GetApasReceivedErrors();
                }

            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "sendApasTranscript", "Error", ex.ToString());
            }
            return success;
        }

        private bool SendApasTransferRequest(quesvc.SentTransferRequest transferRequestToSend, string uuid)
        {
            bool success = false;
            Acknowledgement _Acknowledgement = new Acknowledgement();
            try
            {
                quesvc.Acknowledgment _Message = qs.SendTransferRequest(transferRequestToSend);

                if (_Acknowledgement != null)
                {
                    _Acknowledgement.IsSuccessful = _Message.IsSuccessful;
                    _Acknowledgement.Originator = _Message.Originator;
                    _Acknowledgement.OriginatorAPASCode = _Message.OriginatorAPASCode;
                    _Acknowledgement.AcknowledgementValue = _Message.AcknowledgmentValue;

                    success = lcapasLogic.SaveNewAcknowledgement(_Acknowledgement, uuid, true);

                    if (!_Acknowledgement.IsSuccessful)
                    {
                        lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "sendApasTranscript", "Acknowledgement", "AcknowledgementValue" + _Acknowledgement.AcknowledgementValue);
                    }

                    success = GetApasSentApplicationByUUID(uuid);

                    success = GetApasReceivedErrors();
                }

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "sendApasTransferRequest", "Error", ex.ToString());
            }

            return success;
        }

      
        public string EncodeApasMessage(string message)
        {
            return qs.PrepareCompressedEncodedMessage(message);
        }

        public string DecodeApasMessage(string message)
        {
            return qs.GetDecompressedDecodedMessage(message);
        }

        public static bool ValidWriteGif(string writeGifUrl)
        {
            bool success = false;
            try
            {
                WebRequest req = WebRequest.Create(writeGifUrl);
                WebResponse response = req.GetResponse();
                if (response != null)
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return success;
        }

        static void SwapNodes(XmlNode a, XmlNode b)
        {
            XmlNode previous = a.PreviousSibling;
            if (previous == b) { previous = previous.PreviousSibling; }
            XmlNode parent = (a is XmlAttribute) ? ((XmlAttribute)a).OwnerElement : a.ParentNode;
            parent.RemoveChild(a);

            b.ParentNode.ReplaceChild(a, b);

            // now insert b before a
            if (previous != null) { parent.InsertAfter(b, previous); }
            else
            {
                previous = parent.FirstChild;
                parent.InsertBefore(b, previous);
            }
        }

        public static XmlNode RenameNode(XmlNode node, string name)
        {
            if (node.NodeType == XmlNodeType.Element)
            {
                XmlElement oldElement = (XmlElement)node;
                XmlElement newElement = node.OwnerDocument.CreateElement(name);
                while (oldElement.HasAttributes)
                {
                    newElement.SetAttributeNode(oldElement.RemoveAttributeNode(oldElement.Attributes[0]));
                }
                while (oldElement.HasChildNodes)
                {
                    newElement.AppendChild(oldElement.FirstChild);
                }
                if (oldElement.ParentNode != null)
                {
                    oldElement.ParentNode.ReplaceChild(newElement, oldElement);
                }
                return newElement;
            }
            else
            {
                return null;
            }
        }

        public distsvc.UserInfo GetApasUserInfo(string sessionId, string securityToken)
        {
            distsvc.UserInfo _UserInfo = new distsvc.UserInfo();

            try
            {
                var _info = distsvc.GetUserInfo(sessionId, securityToken);

                _UserInfo.IsAdministrationModuleUser = _info.IsAdministrationModuleUser;
                _UserInfo.UserName = _info.UserName;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "GetApasUserInfo", "Error", "SessionID: " + sessionId + ", Security Token: " + securityToken + "Error: "+ ex.ToString(), false);
            }

            return _UserInfo;
        }

        public string RefreshApasToken(string sessionId, string securityToken)
        {
            string result = "";
            try
            {
                //lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "RefreshApasToken", "Pre-Refresh Token", "SessionId: " + sessionId + ", SecurityToken: " + securityToken, false);
                result = toksvc.RefreshToken(sessionId, securityToken);
                //lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "RefreshApasToken", "Post-Refresh Token", "SessionId: " + sessionId + ", SecurityToken: " + securityToken + ", New Security Token: " + result, false);
            }
            catch (Exception ex)
            {
                //lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "RefreshApasToken", "Error variable statuses", "SessionId: " + sessionId + ", SecurityToken: " + securityToken + ", New Security Token: " + result, false);
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.Apas, "RefreshApasToken", "Error", ex.ToString() + " - SessionId: " + sessionId + ", SecurityToken: " + securityToken, false);
            }

            return result;
        }

        public void Dispose()
        {
            lcapasLogic.Dispose();
        }
    }
}
