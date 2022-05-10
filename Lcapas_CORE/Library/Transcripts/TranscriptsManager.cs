using Lcapas.CollWebApi;
using Lcapas.Core.Library;
using Lcapas.Core.Models.Lcappsdb;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lcapas.Core.Logic
{
    public class TranscriptsManager : IDisposable
    {
        #region Private Properties
        private ApasLogic apasLogic = new ApasLogic();
        private LcapasLogic lcapasLogic = new LcapasLogic();
        private ColleagueLogic collLogic = new ColleagueLogic();
        private lcapasdb_entities ctx = new lcapasdb_entities();

        private string _Environment = string.Empty;
        #endregion Private Properties

        #region Constructors
        public TranscriptsManager()
        {
            try
            {
                // troubleshoot ParseApasRequest
                //lcapasLogic.ParseApasMessage("6e07c797-db44-4b2e-b848-50f4d68f10a2", Library.Apas.CoreMain.DocumentTypeCodeType.Request);

                // 1. Refreshing Incoming Responses
                GetResponses();

                // 2. Refreshing Incoming Requests
                GetRequests();

                // 3. Lookup for Students Missing sNumber
                LookupStudentMissingSnumbers();

                // 4. Complete Paid application but unsent to APAS and mark application as paid and sent
                CompletePaidUnsetApplications();

                // 5. Respond MyCreds TRRQ requests
                MyCredsTRRQRequestResponses();

                // 6. Run Jobs
                RunJobs();

                // 7. Job Cleanup
                CleanupJobs();

                // 8. Check for any Error Received from APAS
                apasLogic.GetApasReceivedErrors();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsManager, "TranscriptsManager", "Error", ex.ToString());
            }
        }
        #endregion Constructors

        #region Queue Jobs

        public bool GetResponses()
        {
            bool success = false;
            Int32 checkdelay = 0;
            DateTime lastsync = new DateTime(2018, 11, 19, 00, 00, 00);

            try
            {
                checkdelay = Convert.ToInt32(lcapasLogic.GetSetting(Structs.SettingTypes.Integer, Structs.Settings.CheckTranscriptsFrequency));

                var responseDate = ctx.TranscriptJobs.Where(x => x.JobType == Enums.JobTypeType.ParseReceivedApasResponse && x.CompletedDateTime != null).OrderByDescending(y => y.CompletedDateTime);

                if (responseDate.Any()) { lastsync = responseDate.FirstOrDefault().CompletedDateTime ?? DateTime.Now.AddDays(-15); } else { lastsync = DateTime.Now.AddDays(-15); }

                if (DateTime.Now > lastsync.AddSeconds(checkdelay))
                {
                    quesvc2.ReceivedTranscriptsFilter responsefilter = new quesvc2.ReceivedTranscriptsFilter()
                    {
                        ReceivedFrom = lastsync.AddSeconds(checkdelay),
                    };

                    success = apasLogic.GetApasReceivedTranscriptsExt2(responsefilter);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsManager, "GetResponses", "Error", ex.ToString());
            }

            return success;
        }

        public bool GetRequests()
        {
            bool success = false;
            Int32 checkdelay = 0;
            DateTime lastsync = new DateTime(2018, 11, 20, 14, 55, 00);

            try
            {
                checkdelay = Convert.ToInt32(lcapasLogic.GetSetting(Structs.SettingTypes.Integer, Structs.Settings.CheckRequestsFrequency));

                var requestDate = ctx.TranscriptJobs.Where(x => x.JobType == Enums.JobTypeType.ParseReceivedTransferRequest && x.CompletedDateTime != null).OrderByDescending(y => y.CompletedDateTime);

                if (requestDate.Any()) { lastsync = requestDate.FirstOrDefault().CompletedDateTime ?? DateTime.Now.AddDays(-15); } else { lastsync = DateTime.Now.AddDays(-15); }

                if (DateTime.Now > lastsync.AddSeconds(checkdelay))
                {
                    quesvc.ReceivedTransferRequestsFilter receivedRequestFilter = new quesvc.ReceivedTransferRequestsFilter()
                    {
                        ReceivedFrom = lastsync.AddSeconds(checkdelay),
                    };

                    success = apasLogic.GetApasReceivedTransferRequests(receivedRequestFilter);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsManager, "GetRequests", "Error", ex.ToString());
            }

            return success;
        }

        public bool LookupStudentMissingSnumbers()
        {
            bool success = false;
            Int32 checkdelay = 1;  // One day
            DateTime lastsync = DateTime.Now.AddDays(-1);

            try
            {
                // Get last completed job
                var lookupDate = ctx.TranscriptJobs.Where(x => x.JobType == Enums.JobTypeType.LookupSNumberByASN && x.CompletedDateTime != null);

                if (lookupDate.Any())
                {
                    lastsync = lookupDate.OrderByDescending(y => y.CompletedDateTime).Select(s => s.CompletedDateTime).FirstOrDefault() ?? lastsync;
                }

                lastsync = lastsync.Date + new TimeSpan(19, 30, 0);  // Run at 7:30 pm;

                // Run just once a day
                if (DateTime.Now > lastsync.AddDays(checkdelay))
                {
                    success = lcapasLogic.QueueLookupSNumberByANS();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsManager, "LookupStudentMissingSnumbers", "Error", ex.ToString());
            }

            return success;
        }

        public bool CompletePaidUnsetApplications()
        {
            bool success = false;
            Int32 checkdelay = 1;  // One minute
            DateTime lastsync = DateTime.Now.AddDays(-1);

            try
            {
                // Get last completed job
                var lookupDate = ctx.TranscriptJobs.Where(x => x.JobType == Enums.JobTypeType.CompletePaidUnsetApplication && x.CompletedDateTime != null);

                if (lookupDate.Any())
                {
                    lastsync = lookupDate.OrderByDescending(y => y.CompletedDateTime).Select(s => s.CompletedDateTime).FirstOrDefault() ?? lastsync;
                }

                // Run every minute
                if (DateTime.Now > lastsync.AddMinutes(checkdelay))
                {
                    success = lcapasLogic.QueueCompletePaidUnsetApplications();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsManager, "LookupStudentMissingSnumbers", "Error", ex.ToString());
            }

            return success;
        }

        public bool MyCredsTRRQRequestResponses()
        {
            bool success = false;
            Int32 checkdelay = 1;  // One day
            DateTime lastsync = DateTime.Now.AddDays(-1);

            try
            {
                // Get last completed job
                var lookupDate = ctx.TranscriptJobs.Where(x => x.JobType == Enums.JobTypeType.QueueMyCredsTRRQRequestResponses && x.CompletedDateTime != null);

                if (lookupDate.Any())
                {
                    lastsync = lookupDate.OrderByDescending(y => y.CompletedDateTime).Select(s => s.CompletedDateTime).FirstOrDefault() ?? lastsync;
                }

                lastsync = lastsync.Date + new TimeSpan(18, 00, 0);  // Run at 6:00 pm;

                // Run just once a day
                if (DateTime.Now > lastsync.AddDays(checkdelay))
                {
                    success = lcapasLogic.QueueMyCredsTRRQRequestResponses();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsManager, "MyCredsTRRQRequestResponses", "Error", ex.ToString());
            }

            return success;
        }

        public void RunJobs()
        {
            try
            {
                List<TranscriptJob> jobs = lcapasLogic.GetJobs();

                foreach (TranscriptJob job in jobs)
                {
                    // No need to multithread (yet)
                    // comment out the threaded tasks for troubleshooting purposes
                    //Task t = Task.Factory.StartNew(() =>
                    //{
                    //    RunJob(job);
                    //});

                    RunJob(job);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsManager, "RunJobs", "Error", ex.ToString());
            }
        }

        public void CleanupJobs()
        {
            try
            {
                lcapasLogic.CleanJobs();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsManager, "CleanupJobs", "Error", ex.ToString());
            }
        }

        public void Dispose()
        {
            lcapasLogic.Dispose();
            apasLogic.Dispose();
            collLogic.Dispose();
            ctx.Dispose();
            this.Dispose();
        }

        #endregion QueueJobs

        #region Run Jobs
        public bool RunJob(TranscriptJob job)
        {
            bool success = false;

            try
            {
                if (job != null)
                {
                    //success = lcapasLogic.StartJob(job.JobId);  // Started when GetJobs() runs

                    //if (success)
                    //{
                        switch (job.JobType)
                        {
                            case Enums.JobTypeType.SendUnsentApps:
                                
                                break;
                            case Enums.JobTypeType.ParseReceivedTransferRequest:
                                success = lcapasLogic.ParseApasMessage(job.JobKey, Enums.MessageTypes.ReceivedRequest);
                                break;
                            case Enums.JobTypeType.ParseSentTransferRequest:
                                success = lcapasLogic.ParseApasMessage(job.JobKey, Enums.MessageTypes.SentRequest);
                                break;
                            case Enums.JobTypeType.ParseReceivedApasResponse:
                                success = lcapasLogic.ParseApasMessage(job.JobKey, Enums.MessageTypes.ReceivedResponse);
                                break;
                            case Enums.JobTypeType.ParseSentApasResponse:
                                success = lcapasLogic.ParseApasMessage(job.JobKey, Enums.MessageTypes.SentResponse);
                                break;
                            case Enums.JobTypeType.SendApasTransferRequest:
                                success = apasLogic.SendTransferRequest(job.JobKey);
                                break;
                            case Enums.JobTypeType.SendApasTransferResponse:
                                success = apasLogic.SendTransferResponse(job.JobKey);
                                break;
                            case Enums.JobTypeType.GetColleagueTranscript:
                                success = collLogic.GetColleagueTranscript(job.JobKey);
                                break;
                            case Enums.JobTypeType.SaveColleagueRequest:
                                success = collLogic.SaveColleagueRequest(job.JobKey);
                                break;
                            case Enums.JobTypeType.SaveColleagueExtCourse:
                                string errorMessage = null;
                                success = collLogic.ExportCourseToColleague(job.JobKey, ref errorMessage);
                                break;
                            case Enums.JobTypeType.SendAutoResponse:
                                success = lcapasLogic.SendAutoResponse(job.JobKey);
                                break;
                            case Enums.JobTypeType.SendBulkTranscript:
                                success = lcapasLogic.SendBulkTranscript(job.JobKey);
                                break;
                            case Enums.JobTypeType.LookupSNumberByASN:
                                success = lcapasLogic.UpdateStudentMissingSNumber(job.JobKey);
                                break;
                            case Enums.JobTypeType.CompletePaidUnsetApplication:
                                success = lcapasLogic.ResendPaidUnsentApplications(job.JobKey);
                                break;
                            case Enums.JobTypeType.QueueMyCredsTRRQRequestResponses:
                                success = lcapasLogic.SendMyCredsTranscriptResponse(job.JobKey);
                                break;
                            default:
                                success = false;
                                break;
                        }
                    //}
                }

                if (success)
                {
                    success = lcapasLogic.CompleteJob(job.JobId);
                } else
                {
                    // If it fails, clean StartedDateTime for next job run
                    lcapasLogic.CleanStartedDateTimeJob(job.JobId);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsManager, "RunJob", "Error", ex.ToString());
            }

            return success;
        }
        #endregion Run Jobs

        #region public properties
        public string Environment { get { return _Environment; } }
        #endregion public properties

    }
}