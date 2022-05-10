using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Lcapas.CollWebApi.DataContracts;
using Lcapas.Core.Library;
using Ellucian.Data.Colleague;
using Ellucian.Data.Colleague.Exceptions;

namespace Lcapas.CollWebApi
{
    class TransactionInvoker
    {

        private static ColleagueTransactionInvoker _transactionInvoker;

        /// <summary>
        /// GetXmlTranscript - Run Transaction
        /// </summary>
        /// <returns>string</returns>
        public bool GetXmlTranscript(ColleagueTransactionInvoker invoker, string transactionTranscriptUuid, ContractRequest_GetXmlTran request)
        {
            bool success = false;
            _transactionInvoker = invoker;

            try
            {
                GetXmlTranscriptResponse response = _transactionInvoker.Execute<ContractRequest_GetXmlTran, GetXmlTranscriptResponse>(request);

                if (!string.IsNullOrWhiteSpace(response.TranscriptText.ToString()))
                {
                    string xmlString = response.TranscriptText.ToString().Replace("ý", "");

                    if (!string.IsNullOrWhiteSpace(xmlString))
                    {
                        using (Core.Logic.LcapasLogic lcapasLogic = new Core.Logic.LcapasLogic())
                        {
                            success = lcapasLogic.SaveTransactionTranscriptText(transactionTranscriptUuid, xmlString);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (Core.Logic.LcapasLogic lcapasLogic = new Core.Logic.LcapasLogic()) {
                    lcapasLogic.SaveException(Structs.Project.LcapasAdmin, "TransactionInvoker", "GetXmlTranscript", "Error", "TransactionTranscriptId: " + transactionTranscriptUuid + ", Ex: " + ex, true);
                }
            }
            return success;
        }

        public bool InsertCourse(ColleagueTransactionInvoker invoker, CourseExportObj course)
        {
            bool success = false;
            _transactionInvoker = invoker;

            try
            {
                InsertCourseContractResponse response = _transactionInvoker.Execute<ContractRequest_InsertExtCrs, InsertCourseContractResponse>(course.Course);
                success = true;
            }
            catch (Exception ex)
            {
                using (Core.Logic.LcapasLogic lcapasLogic = new Core.Logic.LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasAdmin, "TransactionInvoker", "InsertCourse", "Error", ex.ToString(), true);
                }
            }
            return success;
        }

        public bool InsertStudentRequest(ColleagueTransactionInvoker invoker, string transactionTranscriptUuid, ContractRequest_InsertReqLog request, bool markRequestSent = true)
        {
            bool success = false;
            _transactionInvoker = invoker;

            try
            {
                InsertStuReqLogContractResponse response = _transactionInvoker.Execute<ContractRequest_InsertReqLog, InsertStuReqLogContractResponse>(request);

                // Mark TransctionRequestLog as Completed
                if (!string.IsNullOrWhiteSpace(response.StudentRequestLogsId.ToString()) && string.IsNullOrWhiteSpace(response.ErrorOccured))
                {
                    using (Core.Logic.LcapasLogic lcapasLogic = new Core.Logic.LcapasLogic())
                    {
                        success = lcapasLogic.SaveTransactionRequestLogCompletedDate(transactionTranscriptUuid, response.StudentRequestLogsId.ToString());
                        // Mark request as Sent to TRRQ
                        if (markRequestSent)
                        {
                            success = lcapasLogic.MarkRequestAsSentToTRRQ(transactionTranscriptUuid);
                        }
                    }
                } else {
                    using (Core.Logic.LcapasLogic lcapasLogic = new Core.Logic.LcapasLogic())
                    {
                        //lcapasLogic.QueueJob(transactionTranscriptUuid, Enums.JobTypeType.SaveColleagueRequest);  // Locking Student/Person in Colleague
                        lcapasLogic.SaveException(Structs.Project.LcapasAdmin, "TransactionInvoker", "InsertStudentRequest", "Job Queued - Error", "transactionTranscriptUuid: " + transactionTranscriptUuid +", Error: " + response.ErrorMessage, true);
                    }
                }
            }
            catch (Exception ex)
            {
                using (Core.Logic.LcapasLogic lcapasLogic = new Core.Logic.LcapasLogic())
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasAdmin, "TransactionInvoker", "InsertStudentRequest", "Error", ex.ToString(), true);
                }
            }
            return success;
        }
    }
}
