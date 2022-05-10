using System;
using System.Collections.Generic;
using System.Linq;
using Lcapas.Core.Library;
using Lcapas.Core.Models.Colldb;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Data.Entity;
using Newtonsoft.Json;
using Lcapas.Core.Library.Apas.ErrorMessage;
using System.Web.Management;
using System.Globalization;

namespace Lcapas.Core.Logic
{
    public class ColleagueLogic : IDisposable
    {
        private LcapasLogic lcapasLogic = new LcapasLogic();
        private colldb_entities ctx = new colldb_entities();
        private string _UserName = string.Empty;
        private DateTime _CreatedDate = DateTime.Now;

        public ColleagueLogic()
        {
            _UserName = Functions.GetCurrentUser().ToString();
        }

        public StudentRecordObj GetStudent(string sNumber)
        {
            StudentRecordObj _StudentRecord = new StudentRecordObj();

            try
            {
                var rec = (from per in ctx.PERSON.Where(p => ((p.PRIVACY_FLAG ?? "") != "X")
                                                              && ((p.PERSON_CORP_INDICATOR ?? "") != "Y"))
                           join add in ctx.ADDRESS on per.PREFERRED_ADDRESS equals add.ADDRESS_ID
                           join pho in ctx.ADR_PHONES on per.PREFERRED_ADDRESS equals pho.ADDRESS_ID
                           join als in ctx.ADDRESS_LS on pho.ADDRESS_ID equals als.ADDRESS_ID
                           join alt in ctx.PERSON_ALT on per.ID equals alt.ID into ab
                           from alts in ab.Where(x => x.PERSON_ALT_ID_TYPES.Contains("ALTA")).OrderBy(y => y.POS).Take(1).DefaultIfEmpty()
                           where per.ID.Contains(sNumber)
                           select new StudentRecordObj
                           {
                               Snumber = per.ID.Trim(),
                               ASN = (alts == null ? String.Empty : alts.PERSON_ALT_IDS.Trim()),
                               FirstName = per.FIRST_NAME.Trim(),
                               MiddleName = per.MIDDLE_NAME.Trim(),
                               LastName = per.LAST_NAME.Trim(),
                               BirthDate = per.BIRTH_DATE,
                               Gender = per.GENDER.Trim(),
                               Addr1 = als.ADDRESS_LINES.Trim(),
                               City = add.CITY.Trim(),
                               State = add.STATE.Trim(),
                               Zip = add.ZIP.Trim(),
                               Country = add.COUNTRY.Trim() ?? "CA",
                               Phone = pho.ADDRESS_PHONES.Trim(),
                               FormerNames = (from f in ctx.NAMEHIST
                                              where f.ID == per.ID
                                              select new StuNameObj
                                              {
                                                  LastName = f.NAME_HISTORY_LAST_NAME.Trim(),
                                                  FirstName = f.NAME_HISTORY_FIRST_NAME.Trim(),
                                                  MiddleName = f.NAME_HISTORY_LAST_NAME.Trim(),
                                              }).ToList<StuNameObj>()
                           }).FirstOrDefault();

                _StudentRecord = rec;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "getCollStudents", "Error", ex.ToString());
            }

            return _StudentRecord;
        }

        public string GetStudentSNumberByASN(string ASN)
        {
            string _sNumber = string.Empty;

            try
            {
                var query = (from alt in ctx.PERSON_ALT
                             where alt.PERSON_ALT_ID_TYPES == "ALTA"
                                && alt.PERSON_ALT_IDS.Trim().ToUpper() == ASN.Trim().ToUpper()
                             select alt).OrderBy(o => o.POS);

                if (query.Any())
                {
                    _sNumber = query.Select(s => s.ID.Trim()).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetStudentSNumberByASN", "Error", ex.ToString());
            }

            return _sNumber;
        }

        public List<StuGradesObj> GetStudentGrades(string sNumber, string acadLevel)
        {
            List<StuGradesObj> _StudentGrades = new List<StuGradesObj>();
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@strStudentID", sNumber),
                    new SqlParameter("@strAcadLevel", acadLevel),
                };

                _StudentGrades = ctx.Database.SqlQuery<StuGradesObj>(Structs.USP.GetStudentGrades.ToString() + " @strStudentID, @strAcadLevel", sqlParams).ToList();

            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "getCollTranscript", "Error", ex.ToString());
            }

            return _StudentGrades;
        }

        private IQueryable<StudentRecordObj> SetColleagueStudentQuery()
        {
            IQueryable<StudentRecordObj> results = null;
            try
            {
                results = (from per in ctx.PERSON.Where(p => ((p.PRIVACY_FLAG ?? "") != "X") && ((p.PERSON_CORP_INDICATOR ?? "") != "Y"))
                           join add in ctx.ADDRESS on per.PREFERRED_ADDRESS equals add.ADDRESS_ID
                           join pho in ctx.ADR_PHONES on per.PREFERRED_ADDRESS equals pho.ADDRESS_ID into ph
                           from phos in ph.Where(x => x.POS == 1).DefaultIfEmpty()
                           join als in ctx.ADDRESS_LS on phos.ADDRESS_ID equals als.ADDRESS_ID into ls
                           from alss in ls.Where(x => x.POS == 1).DefaultIfEmpty()
                           join alt in ctx.PERSON_ALT on per.ID equals alt.ID into ab
                           from alts in ab.Where(x => x.PERSON_ALT_ID_TYPES.Contains("ALTA")).OrderBy(y => y.POS).Take(1).DefaultIfEmpty()
                           join em in ctx.PEOPLE_EMAIL on per.ID equals em.ID into mail
                           from email in mail.Where(y => y.PERSON_PREFERRED_EMAIL.Contains("Y") && y.POS == 1).DefaultIfEmpty()
                           select new StudentRecordObj
                           {
                               Snumber = per.ID.Trim(),
                               ASN = (alts == null ? String.Empty : alts.PERSON_ALT_IDS.Trim()),
                               FirstName = per.FIRST_NAME.Trim(),
                               MiddleName = per.MIDDLE_NAME.Trim(),
                               LastName = per.LAST_NAME.Trim(),
                               BirthDate = per.BIRTH_DATE,
                               Gender = per.GENDER.Trim(),
                               Addr1 = alss.ADDRESS_LINES.Trim(),
                               City = add.CITY.Trim(),
                               State = add.STATE.Trim(),
                               Zip = add.ZIP.Trim(),
                               Country = add.COUNTRY.Trim() ?? "CA",
                               Phone = phos.ADDRESS_PHONES.Trim(),
                               Email = email.PERSON_EMAIL_ADDRESSES.Trim(),
                               FormerNames = (from f in ctx.NAMEHIST
                                               where f.ID == per.ID
                                               select new StuNameObj
                                               {
                                                   LastName = f.NAME_HISTORY_LAST_NAME.Trim(),
                                                   FirstName = f.NAME_HISTORY_FIRST_NAME.Trim(),
                                                   MiddleName = f.NAME_HISTORY_MIDDLE_NAME.Trim(),
                                               }).ToList()
                           });
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "SetColleagueStudentQuery", "Error", ex.ToString());
            }

            return results;
        }

        private bool FilterStudentLookup(Enums.FilterStudentLookupTypes type, string data, ref IQueryable<StudentRecordObj> results)
        {
            bool success = false;
            try
            {
                if (!string.IsNullOrWhiteSpace(data))
                {
                    data = data.Trim();
                    switch (type)
                    {
                        case Enums.FilterStudentLookupTypes.sNumber:
                            // Search by sNumber
                            if (data.Contains("..."))
                            {
                                data = data.Replace("...", "");
                                results = results.Where(x => x.Snumber.StartsWith(data));
                            }
                            else
                            {
                                results = results.Where(x => x.Snumber.Contains(data));
                            }
                            success = true;
                            break;
                        case Enums.FilterStudentLookupTypes.ASN:
                            // Search by ASN
                            if (data.Contains("..."))
                            {
                                data = data.Replace("...", "");
                                results = results.Where(x => x.ASN.StartsWith(data));
                            }
                            else
                            {
                                results = results.Where(x => x.ASN.Contains(data));
                            }
                            success = true;
                            break;
                        case Enums.FilterStudentLookupTypes.FirstName:
                            // Search by First Name
                            if (data.Contains("..."))
                            {
                                data = data.Replace("...", "");
                                results = results.Where(x => x.FirstName.ToLower().StartsWith(data.ToLower())
                                            || x.FormerNames.Any(y => y.FirstName.ToLower().StartsWith(data.ToLower())));
                            }
                            else
                            {
                                results = results.Where(x => x.FirstName.ToLower().Contains(data.ToLower())
                                            || x.FormerNames.Any(y => y.FirstName.ToLower().Contains(data.ToLower())));
                            }
                            success = true;
                            break;
                        case Enums.FilterStudentLookupTypes.MiddleName:
                            // Search by Middle Name
                            if (data.Contains("..."))
                            {
                                data = data.Replace("...", "");
                                results = results.Where(x => x.MiddleName.ToLower().StartsWith(data.ToLower())
                                                || x.FormerNames.Any(y => y.MiddleName.ToLower().StartsWith(data.ToLower())));
                            }
                            else
                            {
                                results = results.Where(x => x.MiddleName.ToLower().Contains(data.ToLower())
                                            || x.FormerNames.Any(y => y.MiddleName.ToLower().Contains(data.ToLower())));
                            }
                            success = true;
                            break;
                        case Enums.FilterStudentLookupTypes.LastName:
                            // Search by Last Name
                            if (data.Contains("..."))
                            {
                                data = data.Replace("...", "");
                                results = results.Where(x => x.LastName.ToLower().StartsWith(data.ToLower())
                                                || x.FormerNames.Any(y => y.LastName.ToLower().StartsWith(data.ToLower())));
                            }
                            else
                            {
                                results = results.Where(x => x.LastName.ToLower().Contains(data.ToLower())
                                            || x.FormerNames.Any(y => y.LastName.ToLower().Contains(data.ToLower())));
                            }
                            success = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "FilterStudentLookup", "Error", ex.ToString());
            }

            return success;
        }


        public bool GetColleagueStudents(StuLookupListViewObj lookup)
        {
            bool success = false;
            try
            {
                // Break Link Query down in a sub-function, and use the matching order to find a student:
                // It should look for in the following order:
                //  1) Regular search with all fields available
                //  2) If nothing found, search by ASN, First/Last name and BirthDate
                //  3) If nothing found, search by ASN, Former/Alternate First/Last name, BirthDate

                //  1) Regular search with all fields available
                var results = SetColleagueStudentQuery();

                // Search by sNumber
                if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.Snumber))
                {
                    FilterStudentLookup(Enums.FilterStudentLookupTypes.sNumber, lookup.SearchFilter.StudentRecord.Snumber, ref results);
                }
                
                // Search by ASN
                if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.ASN))
                {
                    FilterStudentLookup(Enums.FilterStudentLookupTypes.ASN, lookup.SearchFilter.StudentRecord.ASN, ref results);
                }

                // Search by First Name
                if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.FirstName))
                {
                    FilterStudentLookup(Enums.FilterStudentLookupTypes.FirstName, lookup.SearchFilter.StudentRecord.FirstName, ref results);
                }

                // Search by Middle Name
                if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.MiddleName))
                {
                    FilterStudentLookup(Enums.FilterStudentLookupTypes.MiddleName, lookup.SearchFilter.StudentRecord.MiddleName, ref results);
                }

                // Search by Last Name
                if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.LastName))
                {
                    FilterStudentLookup(Enums.FilterStudentLookupTypes.LastName, lookup.SearchFilter.StudentRecord.LastName, ref results);
                }

                // Search by BirthDate
                if (lookup.SearchFilter.StudentRecord.BirthDate != null)
                {
                    results = results.Where(x => x.BirthDate == lookup.SearchFilter.StudentRecord.BirthDate);
                }

                // Search by Gender
                if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.Gender) &&
                    lookup.SearchFilter.StudentRecord.Gender != Library.Apas.CoreMain.GenderCodeType.Unreported.ToString() &&
                    lookup.SearchFilter.StudentRecord.Gender != Library.Apas.CoreMain.GenderCodeType.Unspecified.ToString())
                {
                    results = results.Where(a => a.Gender == lookup.SearchFilter.StudentRecord.Gender.Substring(0, 1));
                }

                // Check for results
                if (results.Count() <= 0)
                {
                    //  2) First search by ASN, First/Last Name and BirthDate
                    if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.ASN) ||
                        !string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.FirstName) ||
                        !string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.LastName) ||
                        lookup.SearchFilter.StudentRecord.BirthDate != null)
                    {
                        results = SetColleagueStudentQuery();

                        // Search by ASN
                        if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.ASN))
                        {
                            FilterStudentLookup(Enums.FilterStudentLookupTypes.ASN, lookup.SearchFilter.StudentRecord.ASN, ref results);
                        }

                        // Search by First Name
                        if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.FirstName))
                        {
                            FilterStudentLookup(Enums.FilterStudentLookupTypes.FirstName, lookup.SearchFilter.StudentRecord.FirstName, ref results);
                        }

                        // Search by Last Name
                        if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.LastName))
                        {
                            FilterStudentLookup(Enums.FilterStudentLookupTypes.LastName, lookup.SearchFilter.StudentRecord.LastName, ref results);
                        }

                        // Search by BirthDate
                        if (lookup.SearchFilter.StudentRecord.BirthDate != null)
                        {
                            results = results.Where(x => x.BirthDate == lookup.SearchFilter.StudentRecord.BirthDate);
                        }
                    }

                    // Check for results
                    if (results.Count() <= 0)
                    {
                        //  3) If nothing found, search by ASN, Former/Alternate First/Last Name and BirthDate

                        // Search by Former/Alternate Name
                        if (lookup.SearchFilter.StudentRecord.FormerNames.Any())
                        {
                            foreach (var formerName in lookup.SearchFilter.StudentRecord.FormerNames)
                            {
                                results = SetColleagueStudentQuery();

                                // Search by ASN
                                if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.ASN))
                                {
                                    FilterStudentLookup(Enums.FilterStudentLookupTypes.ASN, lookup.SearchFilter.StudentRecord.ASN, ref results);
                                }

                                // Search by Former/Alternate First Name
                                if (!string.IsNullOrWhiteSpace(formerName.FirstName))
                                {
                                    FilterStudentLookup(Enums.FilterStudentLookupTypes.FirstName, formerName.FirstName, ref results);
                                }

                                // Search by Former/Alternate Last Name
                                if (!string.IsNullOrWhiteSpace(formerName.LastName))
                                {
                                    FilterStudentLookup(Enums.FilterStudentLookupTypes.LastName, formerName.LastName, ref results);
                                }

                                // Search by BirthDate
                                if (lookup.SearchFilter.StudentRecord.BirthDate != null)
                                {
                                    results = results.Where(x => x.BirthDate == lookup.SearchFilter.StudentRecord.BirthDate);
                                }

                                if (results.Count() > 0) break;  // If a result is found exit foreach loop
                            }
                        }
                    }
                }

                //// Search by sNumber
                //if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.Snumber))
                //{
                //    if (lookup.SearchFilter.StudentRecord.Snumber.Contains("..."))
                //    {
                //        lookup.SearchFilter.StudentRecord.Snumber.Replace("...", "");
                //        results = results.Where(x => x.Snumber.StartsWith(lookup.SearchFilter.StudentRecord.Snumber));
                //    }
                //    else
                //    {
                //        results = results.Where(x => x.Snumber.Contains(lookup.SearchFilter.StudentRecord.Snumber));
                //    }
                //}

                //// Search by Middle Name
                //if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.MiddleName))
                //{
                //    if (lookup.SearchFilter.StudentRecord.MiddleName.Contains("..."))
                //    {
                //        lookup.SearchFilter.StudentRecord.MiddleName = lookup.SearchFilter.StudentRecord.MiddleName.Replace("...", "");
                //        results = results.Where(x => x.MiddleName.ToLower().StartsWith(lookup.SearchFilter.StudentRecord.MiddleName.ToLower())
                //                    || x.FormerNames.Any(y => y.MiddleName.ToLower().StartsWith(lookup.SearchFilter.StudentRecord.MiddleName.ToLower())));
                //    }
                //    else
                //    {
                //        results = results.Where(x => x.MiddleName.ToLower().Contains(lookup.SearchFilter.StudentRecord.MiddleName.ToLower())
                //                    || x.FormerNames.Any(y => y.MiddleName.ToLower().Contains(lookup.SearchFilter.StudentRecord.MiddleName.ToLower())));
                //    }
                //}

                //// Search by Gender
                //if (!string.IsNullOrWhiteSpace(lookup.SearchFilter.StudentRecord.Gender) &&
                //    lookup.SearchFilter.StudentRecord.Gender != Library.Apas.CoreMain.GenderCodeType.Unreported.ToString() &&
                //    lookup.SearchFilter.StudentRecord.Gender != Library.Apas.CoreMain.GenderCodeType.Unspecified.ToString())
                //{
                //    results = results.Where(a => a.Gender == lookup.SearchFilter.StudentRecord.Gender.Substring(0,1));
                //}

                if (results.Count() > 0)
                {
                    lookup.StudentRecords = results.OrderBy(c => c.FirstName).ThenBy(n => n.LastName).Skip((lookup.Pagination.PageIndex - 1) * lookup.Pagination.PageSize).Take(lookup.Pagination.PageSize).ToList();

                    lookup.Pagination.RecCount = results.Count();
                    lookup.Pagination.PageCount = (results.Count() + lookup.Pagination.PageSize - 1) / lookup.Pagination.PageSize;

                    success = true;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueStudents", "Error", ex.ToString());
            }

            return success;
        }

        internal bool ExportCourseToColleague(string courseIdstr, ref string errorMessage, string transDataId = null)
        {
            bool success = false;
            int courseId = 0;
            try
            {
                bool successfullyParsed = int.TryParse(courseIdstr, out courseId);
                if (successfullyParsed)
                {
                    // Load Course Object
                    string institution = null;
                    bool alreadyMarkedExported = false;
                    CourseExportObj course = lcapasLogic.GetCourseToExport(courseId, ref institution, ref alreadyMarkedExported);

                    // Import only if course was found and it has an Institution Id from Colleague
                    if (course != null && !string.IsNullOrWhiteSpace(course.Course.AExtlInstitution))
                    {
                        // Save Transaction Course
                        lcapasLogic.SaveTransactionCourse(course);

                        // Verify if Course is not already in colleage before exporting
                        bool InColleague = true;
                        DateTime? whenExportedColleague = null;
                        InColleague = CheckCourseAlreadyExists(course, ref whenExportedColleague);

                        if (!InColleague)
                        {
                            CollWebApi.MainDriver driver = new CollWebApi.MainDriver();
                            success = driver.InsertCourse(course);

                            if (success)
                            {
                                // Mark Transcript as Exported
                                success = lcapasLogic.SaveTransmissionDataExportedDateTime(course.RequestTrackingID, transDataId);
                            } else
                            {
                                errorMessage = Structs.Export.CourseFailedToExportToColleague + "<br/>Course: " + course.Course.AExtlTitle + " (" + course.Course.AExtlCourse + ")" + " - sNumber: " + course.sNumber + " - Institution: " + institution;
                                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "ExportCourseToColleague", "Course failed to be exported to Colleague!", "CourseId: " + course.CourseId + " - Course Title: " + course.Course.AExtlTitle + " (" + course.Course.AExtlCourse + ")" + " - sNumber: " + course.sNumber + " - Institution: " + institution + " - RequestTrackingID: " + course.RequestTrackingID);
                            }
                        }
                        else
                        {
                            // Update Transmission Data Exported DateTime to match Colleague if it's still empty
                            if (!alreadyMarkedExported) lcapasLogic.SaveTransmissionDataExportedDateTime(course.RequestTrackingID, transDataId, whenExportedColleague);
                            errorMessage = Structs.Export.CourseAlreadyInColleague + "<br/>Course: " + course.Course.AExtlTitle + " (" + course.Course.AExtlCourse + ")" + " - sNumber: " + course.sNumber + " - Institution: " + institution;
                            lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "ExportCourseToColleague", "Course is already in Colleague!", "CourseId: " + course.CourseId + " - Course Title: " + course.Course.AExtlTitle + " (" + course.Course.AExtlCourse + ")" + " - sNumber: " + course.sNumber + " - Institution: " + institution+ " - RequestTrackingID: " + course.RequestTrackingID);
                        }
                    } else
                    {
                        if (course != null && string.IsNullOrWhiteSpace(course.Course.AExtlInstitution))
                        {
                            errorMessage = Structs.Export.CourseInstitutionNotFound + "<br/>Apas Institution: " + institution;
                            lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "ExportCourseToColleague", "Institution Not Found in Colleague", "CourseId: " + course.CourseId + " - Course Title: " + course.Course.AExtlTitle + " (" + course.Course.AExtlCourse + ")" + " - sNumber: " + course.sNumber + " - RequestTrackingID: " + course.RequestTrackingID);
                        } else
                        {
                            errorMessage = Structs.Export.EmptyCourse + "<br/>CourseId: " + courseIdstr;
                            lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "ExportCourseToColleague", "Course Not Found!", "CourseId: " + courseIdstr + " - TransmissionDataId: " + transDataId);
                        }
                    }
                } else
                {
                    errorMessage = Structs.Export.CourseidFailedToBeParsed + " - CourseId: " + courseIdstr;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "ExportCourseToColleague", "Error", ex.ToString());
            }
            return success;
        }

        public bool SaveColleagueRequest(string jobKey)
        {
            bool success = false;
            string trangroup = "PS";

            try
            {
                if (jobKey != null)
                {
                    // Check if a TransactionRequestLogs exists first to be able to read and send it to Colleague
                    Models.Lcappsdb.TransactionRequestLog _TransactionRequestLog = lcapasLogic.GetTransactionRequestLogByUuid(jobKey);
                    if (_TransactionRequestLog == null && lcapasLogic.CreateRequestLog(trangroup, jobKey))
                    {
                        _TransactionRequestLog = lcapasLogic.GetTransactionRequestLogByUuid(jobKey);
                    }

                    if (_TransactionRequestLog != null)
                    {
                        // Save Request Log into Colleague
                        CollWebApi.MainDriver mainDriver = new CollWebApi.MainDriver();
                        success = mainDriver.SaveRequestLog(_TransactionRequestLog);
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "SaveColleagueRequest", "Error", ex.ToString());
            }

            return success;
        }

        public bool GetColleagueTranscript(string jobKey)
        {
            bool success = false;
            string trangroup = "PS"; // Can also take NCR (non-credit) as a parameter

            try
            {
                if (jobKey != null) {
                    CollWebApi.MainDriver mainDriver = new CollWebApi.MainDriver();
                    success = mainDriver.GetXmlTranscripts(trangroup, jobKey);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueTranscript", "Error", ex.ToString());
            }

            return success;
        }

        public bool GetStudentRestriction(string sNum)
        {
            bool success = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(sNum))
                {
                    string sNumber = Functions.CleanSnumber(sNum);
                    List<string> _StudentRestrictionCodes = new List<string>();

                    // Get Transcript Restriction Rules/Groups
                    List<string> _RestrictionGroupCodes = lcapasLogic.GetTranscriptRestrictionCodes(true);

                    // Get final transcript restriction codes based on previous rules/groups
                    List<string> _Rules = ctx.RULES_CHECK.Where(x => _RestrictionGroupCodes.Contains(x.RULES_ID)).Select(y => y.RL_CHECK_VALUES).ToList();
                    foreach (string rule in _Rules)
                    {
                        rule.Split(',').ToList().ForEach(x => _StudentRestrictionCodes.Add(x.Replace('"', ' ').Trim()));
                    }

                    // Include Single Transcript Restrictions
                    lcapasLogic.GetTranscriptRestrictionCodes(false).ForEach(x => _StudentRestrictionCodes.Add(x.Replace('"', ' ').Trim()));

                    // Check if Student has any Transcript Restriction
                    var restrictions = ctx.STUDENT_RESTRICTIONS.Where(x => x.STR_STUDENT == sNumber &&
                                                             _StudentRestrictionCodes.Contains(x.STR_RESTRICTION) &&
                                                            ((x.STR_END_DATE == null) || (x.STR_END_DATE != null && x.STR_END_DATE >= DateTime.Now)));

                    if (restrictions.Any())
                    {
                        success = true;
                        string restrComments = string.Empty;
                        foreach (var restric in restrictions)
                        {
                            if (!string.IsNullOrWhiteSpace(restric.STR_RESTRICTION)) { restrComments += restric.STR_RESTRICTION; }
                            if (!string.IsNullOrWhiteSpace(restric.STR_COMMENTS)) { restrComments += " - " + restric.STR_COMMENTS.Replace('ý', ' '); }
                            restrComments += ", ";
                        }
                        lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetStudentRestriction", "Student has an Restriction!", "sNumber: " + sNum + " - Restrictions: " + restrComments);
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetStudentRestriction", "Error", ex.ToString());
            }
            return success;
        }

        public List<string> GetRestrictionList()
        {
            List<string> _StudentRestrictionCodes = new List<string>();

            try
            {
                // Get Transcript Restriction Rules/Groups
                List<string> _RestrictionGroupCodes = lcapasLogic.GetTranscriptRestrictionCodes(true);

                // Get final transcript restriction codes based on previous rules/groups
                List<string> _Rules = ctx.RULES_CHECK.Where(x => _RestrictionGroupCodes.Contains(x.RULES_ID)).Select(y => y.RL_CHECK_VALUES).ToList();
                foreach (string rule in _Rules)
                {
                    rule.Split(',').ToList().ForEach(x => _StudentRestrictionCodes.Add(x.Replace('"', ' ').Trim()));
                }

                // Include Single Transcript Restrictions
                lcapasLogic.GetTranscriptRestrictionCodes(false).ForEach(x => _StudentRestrictionCodes.Add(x.Replace('"', ' ').Trim()));
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetRestrictionList", "Error", ex.ToString());
            }
            return _StudentRestrictionCodes;
        }

        public string GetColleagueInstitutionId(string otherId)
        {
            string result = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(otherId))
                {
                    // Get Colleague Institution Id
                    result = ctx.INSTITUTIONS.Where(x => x.INST_OTHER_ID.Trim().ToUpper() == otherId.Trim().ToUpper()).OrderByDescending(y => y.INSTITUTIONS_ADD_DATE).Select(z => z.INSTITUTIONS_ID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueInstitutionId", "Error", ex.ToString());
            }
            return result;
        }

        public ADDRESSES GetColleagueStudentAddress(string sNumber)
        {
            ADDRESSES result = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(sNumber))
                {
                    // Get student address
                    result = (from ADR in ctx.ADDRESS
                              join PER in ctx.PERSON on ADR.ADDRESS_ID equals PER.PREFERRED_ADDRESS
                             where PER.ID == sNumber.Trim().ToUpper().Replace("S", "")
                            select ADR).OrderByDescending(o => o.ADDRESS_ADD_DATE).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueStudentAddress", "Error", ex.ToString());
            }
            return result;
        }

        public string GetColleagueExternalGradeId(string gradeCode)
        {
            string result = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(gradeCode))
                {
                    // Get Colleague Institution Id
                    result = ctx.GRADES.Where(x => x.GRD_GRADE.Trim().ToUpper() == gradeCode.Trim().ToUpper() &&
                                                   x.GRD_GRADE_SCHEME.Trim().ToUpper() == Structs.Courses.GradeScheme.Trim().ToUpper())
                                        .OrderByDescending(y => y.GRADES_CHGDATE).Select(z => z.GRADES_ID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueExternalGradeId", "Error", ex.ToString());
            }
            return result;
        }

        public List<string> GetMyCredsPendingTRRQRequests(int maxSizeList = 100)
        {
            List<string> _RequestIdList = null;

            try
            {
                int processLastMonths = lcapasLogic.GetSetting(Structs.SettingTypes.Integer, Structs.Settings.MyCredsProcessLastMonthRequests);

                TRRQRequestListViewObj _TRRQRequestList = new TRRQRequestListViewObj()
                {
                    SearchFilter = new TRRQRequestSearchObj()
                    {
                        FromRequestDate = DateTime.Now.AddMonths(processLastMonths),
                        ToRequestDate = DateTime.Now,
                    }
                };

                List<TRRQRequestObj> _RequestQuery = MyCredsRequestQuery(_TRRQRequestList);
                
                if (_RequestQuery != null && _RequestQuery.Any())
                {
                    // Get only pending requests, where date produced is null and it has academic courses in the transcript, it's not empty
                    // Don't include if Student has restrictions (Financial Hold) or if the personal email is invalid and not a Lethbridge College email
                    _RequestIdList = _RequestQuery.Where(x => x.DateProduced == null && x.AcadCoursesCount > 0 && string.IsNullOrEmpty(x.Restriction) && !x.InvalidEmail)
                                                  .OrderByDescending(o => o.RequestDate)
                                                  .Select(s => s.RequestID)
                                                  .Take(maxSizeList).ToList();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetMyCredsPendingTRRQRequests", "Error", ex.ToString());
            }
            return _RequestIdList;
        }



        public Dictionary<string, List<string>> GetColleagueSNumberByRequestIDs(List<string> requestIdList, bool allSelected = false, string filterFields = null)
        {
            Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            List<string> requestDupList = new List<string>();

            try
            {
                if (requestIdList != null && requestIdList.Any())
                {
                    // Deserialize filter fields
                    TRRQRequestListViewObj _TRRQRequestListViewObj = new TRRQRequestListViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _TRRQRequestListViewObj.SearchFilter = JsonConvert.DeserializeObject<TRRQRequestSearchObj>(filterFields);
                    };

                    var query = MyCredsRequestQuery(_TRRQRequestListViewObj);

                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => requestIdList.Contains(a.RequestID.ToUpper().Trim())).ToList();
                    }

                    query.Where(x => x.AcadCoursesCount == null || x.AcadCoursesCount > 0)
                         .OrderByDescending(x => x.RequestDate)
                         .ThenBy(x => x.FullName)
                         .Select(s => new { s.sNumber, s.RequestID })
                         .ToList();

                    foreach (var item in query)
                    {
                        if (!results.TryGetValue(item.sNumber, out requestDupList))
                        {
                            // New entry, create a new list
                            requestDupList = new List<string>();
                            requestDupList.Add(item.RequestID);
                            results.Add(item.sNumber, requestDupList);
                        } else
                        {
                            // Add to existing list
                            results[item.sNumber].Add(item.RequestID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueSNumberByRequestIDs", "Error", ex.ToString());
            }
            return results;
        }

        public List<string> GetColleagueSNumberByAcadCredIDs(List<string> acadCredIdList, bool allSelected = false, string filterFields = null)
        {
            List<string> sNumberList = new List<string>();

            try
            {
                if (acadCredIdList != null && acadCredIdList.Any())
                {
                    // Deserialize filter fields
                    BulkSendListViewObj _BulkSendListViewObj = new BulkSendListViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _BulkSendListViewObj.SearchFilter = JsonConvert.DeserializeObject<SendBulkSearchObj>(filterFields);
                    };

                    var query = MyCredsBulkSendStudentQuery(_BulkSendListViewObj);

                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => acadCredIdList.Contains(a.AcadCredentialsID.ToUpper().Trim())).ToList();
                    }

                    sNumberList = query.Where(x => x.AcadCoursesCount == null | x.AcadCoursesCount > 0).OrderBy(x => x.FullName).ThenBy(x => x.AcadCCDDate).Select(s => s.sNumber).ToList();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueSNumberByAcadCredIDs", "Error", ex.ToString());
            }
            return sNumberList;
        }

        public bool CheckRequestAlreadyExists(string sNumber, string recipientId, DateTime? requestDate, DateTime? dateProduced)
        {
            bool exists = false;
            DateTime? dateChecked = Functions.CheckForNullDate(requestDate.Value.Date);

            try
            {
                // Check if Request already exists in Colleague TRRQ
                // Use: Student (sNumber), Institution (Recipient Id), Request Date (STRL_DATE) and Date Produced (STRL_PRINT_DATE)
                if (!string.IsNullOrWhiteSpace(sNumber) && !string.IsNullOrWhiteSpace(recipientId) && dateChecked != null)
                {
                    exists = ctx.STUDENT_REQUEST_LOGS.Where(x => x.STRL_STUDENT == sNumber && x.STRL_RECIPIENT == recipientId && x.STRL_DATE == dateChecked &&
                                                             (dateProduced == null ? x.STRL_PRINT_DATE == null : x.STRL_PRINT_DATE == dateProduced)).Any();
                    if (exists)
                    {
                        lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "CheckRequestAlreadyExists", "Request Already Exists in Colleague TRRQ", "sNumber: " + sNumber + ", Recipient Id: " + recipientId + ", Request Date: " + dateChecked + ", Date Produced: " + dateProduced);
                    }
                }
                else
                {
                    lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "CheckRequestAlreadyExists", "Unable to check if TRRQ already exists", "sNumber: " + sNumber + ", Recipient Id: " + recipientId + ", Request Date: " + dateChecked + ", Date Produced: " + dateProduced);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "CheckRequestAlreadyExists", "Error", ex.ToString());
            }
            return exists;
        }

        public bool CheckCourseAlreadyExists(CourseExportObj course, ref DateTime? whenExportedColleague)
        {
            bool exist = true;
            DateTime? StarDate = null, EndDate = null;

            try
            {
                if (course.Course != null)
                {
                    // Start Date
                    if (!string.IsNullOrWhiteSpace(course.Course.AExtlStartDate))
                    {
                        if (DateTime.TryParse(course.Course.AExtlStartDate.ConvertColleagueDaysToDate().ToString(), out DateTime dte)) { StarDate = dte; }
                    }

                    // End Date
                    if (!string.IsNullOrWhiteSpace(course.Course.AExtlEndDate))
                    {
                        if (DateTime.TryParse(course.Course.AExtlEndDate.ConvertColleagueDaysToDate().ToString(), out DateTime dte)) { EndDate = dte; }
                    }
                }

                // Check if Course already exists in Colleague
                var query = ctx.EXTERNAL_TRANSCRIPTS.Where(x => x.EXTL_PERSON_ID.Trim().ToUpper() == course.Course.AExtlPersonId.Trim().ToUpper() &&
                                                            x.EXTL_INSTITUTION.Trim().ToUpper() == course.Course.AExtlInstitution.Trim().ToUpper() &&
                                                            x.EXTL_COURSE.Trim().ToUpper() == course.Course.AExtlCourse.Trim().ToUpper() &&
                                                            x.EXTL_START_DATE == StarDate &&
                                                            x.EXTL_END_DATE == EndDate);
                exist = query.Any();

                // Return exported date if transcript is alredy in Colleague
                if (exist)
                {
                    whenExportedColleague = query.OrderByDescending(o => o.EXTERNAL_TRANSCRIPTS_ADDDATE).Select(s => s.EXTERNAL_TRANSCRIPTS_ADDDATE).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "CheckCourseAlreadyExists", "Error", ex.ToString());
            }
            return exist;
        }

        public void Dispose()
        {
            lcapasLogic.Dispose();
            ctx.Dispose();
        }

        public List<SelectListItem> GetColleaguePrograms(bool removeNulls = false)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            try
            {
                var query = ctx.APPLICATIONS.Where(x => x.APPL_ACTIVE_STU_PROGRAM_FLAG == "Y").Select(z => z.APPL_ACAD_PROGRAM).Distinct().OrderBy(o => o);

                result = query.Select(z => new SelectListItem() { Text = z, Value = z }).ToList();

                // Remove null values
                if (removeNulls) { result.RemoveAll(r => string.IsNullOrWhiteSpace(r.Text)); }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleaguePrograms", "Error", ex.ToString());
            }
            return result;
        }

        public List<SelectListItem> GetColleagueProgramsWithTitle()
        {
            List<SelectListItem> result = new List<SelectListItem>();

            try
            {
                var query = (from APP in ctx.APPLICATIONS
                            join AC in ctx.ACAD_PROGRAMS on APP.APPL_ACAD_PROGRAM equals AC.ACAD_PROGRAMS_ID into ACS
                            from AC in ACS.DefaultIfEmpty()

                            where APP.APPL_ACTIVE_STU_PROGRAM_FLAG == "Y"
                            select new { APP.APPL_ACAD_PROGRAM, AC.ACPG_TITLE }
                            ).Distinct().OrderBy(o => o.APPL_ACAD_PROGRAM);

                result = query.Select(z => new SelectListItem() { Text = z.APPL_ACAD_PROGRAM + " - " + z.ACPG_TITLE, Value = z.APPL_ACAD_PROGRAM }).ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueProgramsWithTitle", "Error", ex.ToString());
            }
            return result;
        }

        public List<SelectListItem> GetColleagueDepartments()
        {
            List<SelectListItem> result = new List<SelectListItem>();

            try
            {
                var query = ctx.DEPTS.Where(x => x.DEPTS_ACTIVE_FLAG == "A").Select(s => new { s.DEPTS_ID, s.DEPTS_DESC }).Distinct().OrderBy(o => o.DEPTS_ID);

                result = query.Select(z => new SelectListItem() { Text = z.DEPTS_ID + " - " + z.DEPTS_DESC, Value = z.DEPTS_ID }).ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueDepartments", "Error", ex.ToString());
            }
            return result;
        }

        public List<SelectListItem> GetColleagueTerms(bool removeNulls = false, bool skipDateConversion = false)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            try
            {
                var query = ctx.APPLICATIONS.Select(z => z.APPL_START_TERM).Distinct().OrderByDescending(o => o);

                result = query.Select(z => new SelectListItem() { Text = z, Value = z }).ToList();

                // Remove null values
                if (removeNulls) { result.RemoveAll(r => string.IsNullOrWhiteSpace(r.Text)); }

                if (!skipDateConversion)
                {
                    result.ForEach(x => x.Value = Functions.GetLcSessionDesignator(x.Value) ?? "null");
                }

                result = result.OrderByDescending(o => o.Value).ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueTerms", "Error", ex.ToString());
            }
            return result;
        }

        public List<SelectListItem> GetColleagueApplicationLocations(bool removeNulls = false)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            try
            {
                var query = ctx.APPL_LOCATION_INFO.Select(z => z.APPL_LOCATIONS).Distinct().OrderBy(o => o);

                result = query.Select(z => new SelectListItem() { Text = z, Value = z }).ToList();

                // Remove null values
                if (removeNulls) { result.RemoveAll(r => string.IsNullOrWhiteSpace(r.Text)); }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueApplicationLocations", "Error", ex.ToString());
            }
            return result;
        }

        public List<SelectListItem> GetColleagueAcadCredLocations(bool removeNulls = false)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            try
            {
                var query = ctx.ACAD_CREDENTIALS.Select(z => z.ACAD_LOCATION).Distinct().OrderBy(o => o);

                result = query.Select(z => new SelectListItem() { Text = z, Value = z }).ToList();

                // Remove null values
                if (removeNulls) { result.RemoveAll(r => string.IsNullOrWhiteSpace(r.Text)); }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueAcadCredLocations", "Error", ex.ToString());
            }
            return result;
        }

        public List<SelectListItem> GetColleagueAcadCredCDDType(bool removeNulls = false)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            try
            {
                var query = ctx.ACAD_CREDENTIALS_LS.Where(x => x.ACAD_CREDENTIALS.ACAD_CREDENTIALS_ID == x.ACAD_CREDENTIALS_ID).Select(z => z.ACAD_CCD).Distinct().OrderBy(o => o);

                result = query.Select(z => new SelectListItem() { Text = z, Value = z }).ToList();

                // Remove null values
                if (removeNulls) { result.RemoveAll(r => string.IsNullOrWhiteSpace(r.Text)); }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueAcadCredCDDType", "Error", ex.ToString());
            }
            return result;
        }

        public List<SelectListItem> GetColleagueAcadCredHonors(bool removeNulls = false)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            try
            {
                var query = ctx.ACAD_CREDENTIALS_LS.Where(x => x.ACAD_CREDENTIALS.ACAD_CREDENTIALS_ID == x.ACAD_CREDENTIALS_ID).Select(z => z.ACAD_HONORS).Distinct().OrderBy(o => o);

                result = query.Select(z => new SelectListItem() { Text = z, Value = z }).ToList();

                // Remove null values
                if (removeNulls) { result.RemoveAll(r => string.IsNullOrWhiteSpace(r.Text)); }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueAcadCredHonors", "Error", ex.ToString());
            }
            return result;
        }

        public List<SelectListItem> GetColleagueAdmissionStatus()
        {
            List<SelectListItem> result = new List<SelectListItem>();

            try
            {
                var query = ctx.APPLICATIONS.Where(x => x.APPL_ADMIT_STATUS != null).Select(s => s.APPL_ADMIT_STATUS.Trim().ToUpper()).Distinct().OrderBy(o => o);

                result = query.Select(z => new SelectListItem() { Text = z, Value = z }).ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueAdmissionStatus", "Error", ex.ToString());
            }
            return result;
        }

        public List<SelectListItem> GetColleagueAlienStatus()
        {
            List<SelectListItem> result = new List<SelectListItem>();

            try
            {
                var query = ctx.FOREIGN_PERSON.Where(x => x.FPER_ALIEN_STATUS != null).Select(s => s.FPER_ALIEN_STATUS.Trim().ToUpper()).Distinct().OrderBy(o => o);

                result = query.Select(z => new SelectListItem() { Text = z, Value = z }).ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueAlienStatus", "Error", ex.ToString());
            }
            return result;
        }

        public List<SelectListItem> GetColleagueApplicationStatus()
        {
            List<SelectListItem> result = new List<SelectListItem>();

            try
            {
                var query = ctx.APPLICATION_STATUSES.Where(x => x.APPLICATION_STATUSES_ID != null).Select(z => z.APPLICATION_STATUSES_ID.Trim().ToUpper()).Distinct().OrderBy(o => o);

                result = query.Select(y => new SelectListItem() { Text = y.ToString(), Value = y.ToString() }).ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueApplicationStatus", "Error", ex.ToString());
            }
            return result;
        }

        public List<SelectListItem> GetColleagueResidenceCountry()
        {
            List<SelectListItem> result = new List<SelectListItem>();

            try
            {
                var query = ctx.PERSON.Where(x => x.RESIDENCE_COUNTRY != null).Select(z => z.RESIDENCE_COUNTRY.Trim().ToUpper()).Distinct().OrderBy(o => o);

                result = query.Select(y => new SelectListItem() { Text = y.ToString(), Value = y.ToString() }).ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueResidenceCountry", "Error", ex.ToString());
            }
            return result;
        }

        public List<SelectListItem> GetColleagueCitizenshipCountry()
        {
            List<SelectListItem> result = new List<SelectListItem>();

            try
            {
                var query = ctx.PERSON.Where(x => x.CITIZENSHIP != null).Select(z => z.CITIZENSHIP.Trim().ToUpper()).Distinct().OrderBy(o => o);

                result = query.Select(y => new SelectListItem() { Text = y.ToString(), Value = y.ToString() }).ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueCitizenshipCountry", "Error", ex.ToString());
            }
            return result;
        }

        public List<SelectListItem> GetColleagueEthnic()
        {
            List<SelectListItem> result = new List<SelectListItem>();

            try
            {
                var query = ctx.PERSON.Where(x => x.ETHNIC != null && x.ETHNIC != "NSI").Select(s => s.ETHNIC.Trim().ToUpper()).Distinct().OrderBy(o => o);  // NSI => Do Not Use code

                result = query.Select(z => new SelectListItem() { Text = z, Value = z }).ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueEthnic", "Error", ex.ToString());
            }
            return result;
        }

        public List<SelectListItem> GetColleagueHoldTypes()
        {
            List<SelectListItem> result = new List<SelectListItem>();

            try
            {
                var query = ctx.VALS.Where(x => x.VALCODE_ID == Structs.ColleagueRequestHolds.STU_REQUEST_LOG_HOLDS).Select(s => new { Code = s.VAL_INTERNAL_CODE.Trim().ToUpper(), Desc = s.VAL_EXTERNAL_REPRESENTATION }).Distinct().OrderBy(o => o);

                result = query.Select(z => new SelectListItem() { Text = z.Code +" - "+ z.Desc, Value = z.Code }).ToList();

                result.Add(new SelectListItem() { Text = Structs.MyCredsHoldTypes.NoHold +" - "+ Structs.MyCredsHoldTypes.NoHoldDesc, Value = Structs.MyCredsHoldTypes.NoHold });
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetColleagueHoldTypes", "Error", ex.ToString());
            }
            return result;
        }


        #region Reports

        public bool DailyRequestReports(DailyRequestReportViewObj _DailyReportViewObj)
        {
            bool success = false;
            try
            {
                var _ReportMessages = DailyRequestQuery(_DailyReportViewObj);

                _DailyReportViewObj.Pagination.RecCount = _ReportMessages.Count();
                _DailyReportViewObj.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    _DailyReportViewObj.ReportSearchResultsFilter = _ReportMessages.OrderByDescending(x => x.Fromdate).ToList().Skip((_DailyReportViewObj.Pagination.PageIndex - 1) * _DailyReportViewObj.Pagination.PageSize).Take(_DailyReportViewObj.Pagination.PageSize).ToList();
                    _DailyReportViewObj.Pagination.PageCount = (_ReportMessages.Count() + _DailyReportViewObj.Pagination.PageSize - 1) / _DailyReportViewObj.Pagination.PageSize;
                }
                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "DailyRequestReports", "Error", ex.ToString());
            }

            return success;
        }

        public string GetDailyRequestReportXmlString(string logId)
        {
            string xmlString = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(logId))
                {
                    var result =
                             (from STRL in ctx.STUDENT_REQUEST_LOGS
                              join LS in ctx.STUDENT_REQUEST_LOGS_LS on STRL.STUDENT_REQUEST_LOGS_ID equals LS.STUDENT_REQUEST_LOGS_ID
                              join NAM in ctx.PERSON on STRL.STRL_RECIPIENT equals NAM.ID
                              join NAME in ctx.PERSON on STRL.STRL_STUDENT equals NAME.ID
                              where STRL.STRL_TYPE == "T"
                                && STRL.STUDENT_REQUEST_LOGS_ID == logId
                                && LS.STRL_TRANSCRIPT_GROUPINGS != null

                              select new DailyRequestReportObj()
                              {
                                  LogID = STRL.STUDENT_REQUEST_LOGS_ID,
                                  StuID = STRL.STRL_STUDENT,
                                  FullName = NAME.LAST_NAME + ", " + NAME.FIRST_NAME,
                                  Type = LS.STRL_TRANSCRIPT_GROUPINGS,
                                  FromDate = STRL.STRL_DATE,
                                  ToDate = STRL.STRL_DATE,
                                  Del = STRL.STRL_DELIVERY_METHOD,
                                  Comments = STRL.STRL_COMMENTS,
                                  RecpID = STRL.STRL_RECIPIENT,
                                  RecpFullName = NAM.LAST_NAME + ", " + NAM.FIRST_NAME + ", " + NAM.MIDDLE_NAME,
                                  AddMod = STRL.STRL_ADDRESS_MODIFIER,
                                  Street1 = LS.STRL_ADDRESS,
                                  City = STRL.STRL_CITY,
                                  Prv = STRL.STRL_STATE,
                                  PCode = STRL.STRL_ZIP,
                                  Ctry = STRL.STRL_COUNTRY,
                                  AddOpr = STRL.STUDENT_REQUEST_LOGS_ADDOPR,
                                  AddDt = STRL.STUDENT_REQUEST_LOGS_ADDDATE,
                              });

                    if (result.Any())
                    {
                        xmlString = result.FirstOrDefault().Serialize();
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetDailyRequestReportXmlString", "Error", ex.ToString());
            }
            return xmlString;
        }

        public List<DailyRequestReportSearchResultsFilter> DailyRequestQuery(DailyRequestReportViewObj _DailyRequestReportViewObj)
        {
            List<DailyRequestReportSearchResultsFilter> _ListDailyRequestReportSearchResultsFilter = null;

            DateTime _fromdate = _DailyRequestReportViewObj.ReportSearchFilter.Fromdate ?? DateTime.Now;
            DateTime _todate = _DailyRequestReportViewObj.ReportSearchFilter.Todate ?? DateTime.Now;

            _DailyRequestReportViewObj.ReportSearchFilter.Fromdate = _fromdate.Date;
            _DailyRequestReportViewObj.ReportSearchFilter.Todate = _todate.Date;
            try
            {
                var _ReportMessages =
                            (from STRL in ctx.STUDENT_REQUEST_LOGS
                             join LS in ctx.STUDENT_REQUEST_LOGS_LS on STRL.STUDENT_REQUEST_LOGS_ID equals LS.STUDENT_REQUEST_LOGS_ID
                             join NAM in ctx.PERSON on STRL.STRL_RECIPIENT equals NAM.ID
                             join NAME in ctx.PERSON on STRL.STRL_STUDENT equals NAME.ID
                             where
                            (STRL.STRL_DATE >= _fromdate
                            && STRL.STRL_DATE <= _todate
                            && STRL.STRL_TYPE == "T"
                            && LS.STRL_TRANSCRIPT_GROUPINGS != null)

                             //&& LS.POS == 1)

                             select new DailyRequestReportSearchResultsFilter()
                             {
                                 LogID = STRL.STUDENT_REQUEST_LOGS_ID,
                                 StuID = STRL.STRL_STUDENT,
                                 Qty = STRL.STRL_COPIES.ToString(),
                                 Fullname = NAME.LAST_NAME + ", " + NAME.FIRST_NAME,
                                 Type = LS.STRL_TRANSCRIPT_GROUPINGS,
                                 Fromdate = STRL.STRL_DATE,
                                 Todate = STRL.STRL_DATE,
                                 Del = STRL.STRL_DELIVERY_METHOD,
                                 Comments = STRL.STRL_COMMENTS.Substring(0, 255),
                                 RecpID = STRL.STRL_RECIPIENT,
                                 RecpFullName = NAM.LAST_NAME + ", " + NAM.FIRST_NAME + ", " + NAM.MIDDLE_NAME,
                                 AddMod = STRL.STRL_ADDRESS_MODIFIER,
                                 Street = LS.STRL_ADDRESS,
                                 City = STRL.STRL_CITY,
                                 Prv = STRL.STRL_STATE,
                                 PCode = STRL.STRL_ZIP,
                                 Ctry = STRL.STRL_COUNTRY,
                                 AddOpr = (STRL.STRL_STUDENT == STRL.STUDENT_REQUEST_LOGS_ADDOPR) ? "ST" : STRL.STUDENT_REQUEST_LOGS_ADDOPR,
                                 AddDt = STRL.STUDENT_REQUEST_LOGS_ADDDATE,
                             });

                // Filter by student ID
                if (!string.IsNullOrWhiteSpace(_DailyRequestReportViewObj.ReportSearchFilter.StuID))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.StuID.Trim().Contains(_DailyRequestReportViewObj.ReportSearchFilter.StuID));
                }

                // Filter by full name
                if (!string.IsNullOrWhiteSpace(_DailyRequestReportViewObj.ReportSearchFilter.Fullname))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Fullname.ToUpper().Trim().Contains(_DailyRequestReportViewObj.ReportSearchFilter.Fullname.ToUpper().Trim()));
                }

                // Filter by qty
                if (!string.IsNullOrWhiteSpace(_DailyRequestReportViewObj.ReportSearchFilter.Qty))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Qty.ToString() == _DailyRequestReportViewObj.ReportSearchFilter.Qty.ToString());
                }

                // Filter by type
                if (!string.IsNullOrWhiteSpace(_DailyRequestReportViewObj.ReportSearchFilter.Type))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Type.ToUpper().Trim().Contains(_DailyRequestReportViewObj.ReportSearchFilter.Type.ToUpper().Trim()));
                }

                // Filter by del
                if (!string.IsNullOrWhiteSpace(_DailyRequestReportViewObj.ReportSearchFilter.Del))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Del.ToUpper().Trim().Contains(_DailyRequestReportViewObj.ReportSearchFilter.Del.ToUpper().Trim()));
                }

                // Filter by Comments
                if (!string.IsNullOrWhiteSpace(_DailyRequestReportViewObj.ReportSearchFilter.Comments))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Comments.ToUpper().Trim().Contains(_DailyRequestReportViewObj.ReportSearchFilter.Comments.ToUpper().Trim()));
                }

                // Filter by RecpID
                if (!string.IsNullOrWhiteSpace(_DailyRequestReportViewObj.ReportSearchFilter.RecpID))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.RecpID.Trim().Contains(_DailyRequestReportViewObj.ReportSearchFilter.RecpID));
                }

                // Filter by recpFullName
                if (!string.IsNullOrWhiteSpace(_DailyRequestReportViewObj.ReportSearchFilter.RecpFullName))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.RecpFullName.ToUpper().Trim().Contains(_DailyRequestReportViewObj.ReportSearchFilter.RecpFullName.ToUpper().Trim()));
                }

                // Filter by street
                if (!string.IsNullOrWhiteSpace(_DailyRequestReportViewObj.ReportSearchFilter.Street))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Street.ToUpper().Trim().Contains(_DailyRequestReportViewObj.ReportSearchFilter.Street.ToUpper().Trim()));
                }

                // Filter by city
                if (!string.IsNullOrWhiteSpace(_DailyRequestReportViewObj.ReportSearchFilter.City))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.City.ToUpper().Trim().Contains(_DailyRequestReportViewObj.ReportSearchFilter.City.ToUpper().Trim()));
                }

                // Filter by Province
                if (!string.IsNullOrWhiteSpace(_DailyRequestReportViewObj.ReportSearchFilter.Prv))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Prv.ToUpper().Trim().Contains(_DailyRequestReportViewObj.ReportSearchFilter.Prv.ToUpper().Trim()));
                }

                // Filter by postal code
                if (!string.IsNullOrWhiteSpace(_DailyRequestReportViewObj.ReportSearchFilter.PCode))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.PCode.Trim() == _DailyRequestReportViewObj.ReportSearchFilter.PCode.Trim());
                }

                // Filter by AddOpr
                if (!string.IsNullOrWhiteSpace(_DailyRequestReportViewObj.ReportSearchFilter.AddOpr))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.AddOpr.Trim().Contains(_DailyRequestReportViewObj.ReportSearchFilter.AddOpr));
                }

                // Filter by AddDt
                if (_DailyRequestReportViewObj.ReportSearchFilter.AddDt != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => a.AddDt == _DailyRequestReportViewObj.ReportSearchFilter.AddDt);
                }

                _ListDailyRequestReportSearchResultsFilter = _ReportMessages.ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "DailyRequestQuery", "Error", ex.ToString());
            }

            return _ListDailyRequestReportSearchResultsFilter;
        }

        public List<DailyRequestReportSearchResultsFilter> GetDailyRequestReportByIds(string[] Id, bool allSelected = false, string filterFields = null)
        {
            List<DailyRequestReportSearchResultsFilter> _DailyRequests = new List<DailyRequestReportSearchResultsFilter>();

            try
            {
                if (Id != null && Id.Count() > 0)
                {
                    // Deserialize filter fields
                    DailyRequestReportViewObj DailyRequestReportViewObj = new DailyRequestReportViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        DailyRequestReportViewObj.ReportSearchFilter = JsonConvert.DeserializeObject<DailyRequestReportSearchFilter>(filterFields);
                    };

                    var query = DailyRequestQuery(DailyRequestReportViewObj);

                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => Id.Contains(a.LogID.ToUpper().Trim())).ToList();
                    }

                    _DailyRequests = query.OrderByDescending(x => x.Fromdate).ToList();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetDailyRequestReportByIds", "Error", ex.ToString());
            }
            return _DailyRequests;
        }

        public List<CollProgramApplicationReportSearchResultsFilter> CollProgramApplicationQuery(CollProgramApplicationReportViewObj _CollProgramApplicationReportViewObj)
        {
            List<CollProgramApplicationReportSearchResultsFilter> _ListCollProgramApplicationReportSearchResultsFilter = null;
            try
            {
                var _ReportMessages =
                (from APPL in ctx.APPLICATIONS
                 join PER in ctx.PERSON on APPL.APPL_APPLICANT equals PER.ID into PER1
                 from PER in PER1.DefaultIfEmpty()

                 select new CollProgramApplicationReportSearchResultsFilter()
                 {
                     ID = APPL.APPLICATIONS_ID,
                     PID = APPL.APPL_APPLICANT,
                     FullName = PER.LAST_NAME + ", " + PER.FIRST_NAME + ", " + PER.MIDDLE_NAME,
                     EmailAddress = ctx.PEOPLE_EMAIL.Where(x => x.ID == APPL.APPL_APPLICANT && x.POS == 1).Select(s => s.PERSON_EMAIL_ADDRESSES).FirstOrDefault(),
                     ASN = ctx.PERSON_ALT.Where(x => x.ID == APPL.APPL_APPLICANT && x.PERSON_ALT_ID_TYPES == "ALTA").Max(m => m.PERSON_ALT_IDS),
                     Program = APPL.APPL_ACAD_PROGRAM,
                     StudyLoad = APPL.APPL_STUDENT_LOAD_INTENT,
                     Term = APPL.APPL_START_TERM,
                     Location = ctx.APPL_LOCATION_INFO.Where(x => x.APPLICATIONS_ID == APPL.APPLICATIONS_ID && x.POS == 1).Select(s => s.APPL_LOCATIONS).FirstOrDefault(),
                     AdmitStatus = APPL.APPL_ADMIT_STATUS,
                     ApplicationStatus = ctx.APPL_STATUSES.Where(x => x.APPLICATIONS_ID == APPL.APPLICATIONS_ID && x.POS == 1).Select(s => s.APPL_STATUS).FirstOrDefault(),
                     VisaStatus = PER.IMMIGRATION_STATUS,
                     Citizenship = PER.CITIZENSHIP,
                     AlienStatus = ctx.FOREIGN_PERSON.Where(x => x.FOREIGN_PERSON_ID == APPL.APPL_APPLICANT).Select(s => s.FPER_ALIEN_STATUS).FirstOrDefault(),
                     OrgCountry = PER.RESIDENCE_COUNTRY,
                     Ethnic = PER.ETHNIC
                 }).Distinct();


                // Filter by Application ID
                if (!string.IsNullOrWhiteSpace(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.ID))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ID.Trim().Contains(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.ID.Trim()));
                }

                // Filter by Person PID
                if (!string.IsNullOrWhiteSpace(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.PID))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.PID.Trim().Contains(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.PID.Trim()));
                }

                // Filter by FullName
                if (!string.IsNullOrWhiteSpace(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.FullName))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.FullName.ToUpper().Trim().Contains(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.FullName.ToUpper().Trim()));
                }

                // Filter by Program
                if (!string.IsNullOrWhiteSpace(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.Program))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Program.ToUpper().Trim().Contains(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.Program.ToUpper().Trim()));
                }

                // Filter by Email Address
                if (!string.IsNullOrWhiteSpace(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.EmailAddress))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.EmailAddress.ToUpper().Trim().Contains(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.EmailAddress.ToUpper().Trim()));
                }

                // Filter by ASN
                if (!string.IsNullOrWhiteSpace(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.ASN))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ASN.ToUpper().Trim().Contains(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.ASN.ToUpper().Trim()));
                }

                // Filter by Study Load
                if (!string.IsNullOrWhiteSpace(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.StudyLoad))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.StudyLoad.ToUpper().Trim().Contains(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.StudyLoad.ToUpper().Trim()));
                }

                // Filter by Term
                if (_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.Term != null && _CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.Term.Any())
                {
                    List<string> termList = new List<string>();
                    foreach (string term in _CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.Term)
                    {
                        termList.Add(Functions.GetLcTerm(term.Trim().ToLower().Replace("null", "")));
                    }
                    if (termList.Any())
                    {
                        _ReportMessages = _ReportMessages.Where(a => termList.Contains(a.Term.ToUpper().Trim()));
                    }
                }

                // Filter by Location
                if (!string.IsNullOrWhiteSpace(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.Location))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Location.ToUpper().Trim().Contains(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.Location.ToUpper().Trim()));
                }

                // Filter by admintStatus
                if (_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.AdmitStatus != null && _CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.AdmitStatus.Any())
                {
                    _ReportMessages = _ReportMessages.Where(a => _CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.AdmitStatus.Contains(a.AdmitStatus.ToUpper().Trim()));
                }

                // Filter by ApplStatus
                if (_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.ApplicationStatus != null && _CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.ApplicationStatus.Any())
                {
                    _ReportMessages = _ReportMessages.Where(a => _CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.ApplicationStatus.Contains(a.ApplicationStatus.ToUpper().Trim()));
                }

                // Filter by VisaStatus
                if (!string.IsNullOrWhiteSpace(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.VisaStatus))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.VisaStatus.ToUpper().Trim().Contains(_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.VisaStatus.ToUpper().Trim()));
                }

                // Filter by Citizenship
                if (_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.Citizenship != null && _CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.Citizenship.Any())
                {
                    _ReportMessages = _ReportMessages.Where(a => _CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.Citizenship.Contains(a.Citizenship.ToUpper().Trim()));
                }

                // Filter by AlienStatus
                if (_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.AlienStatus != null && _CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.AlienStatus.Any())
                {
                    _ReportMessages = _ReportMessages.Where(a => _CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.AlienStatus.Contains(a.AlienStatus.ToUpper().Trim()));
                }

                // Filter by residence country
                if (_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.OrgCountry != null && _CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.OrgCountry.Any())
                {
                    _ReportMessages = _ReportMessages.Where(a => _CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.OrgCountry.Contains(a.OrgCountry.ToUpper().Trim()));
                }

                // Filter by Ethnic
                if (_CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.Ethnic != null && _CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.Ethnic.Any())
                {
                    _ReportMessages = _ReportMessages.Where(a => _CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter.Ethnic.Contains(a.Ethnic.ToUpper().Trim()));
                }

                _ListCollProgramApplicationReportSearchResultsFilter = _ReportMessages.ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "CollProgramApplicationQuery", "Error", ex.ToString());
            }
            return _ListCollProgramApplicationReportSearchResultsFilter;
        }

        public bool CollProgramApplicationReports(CollProgramApplicationReportViewObj _CollProgramApplicationReportViewObj)
        {
            bool success = false;
            try
            {
                var _ReportMessages = CollProgramApplicationQuery(_CollProgramApplicationReportViewObj);

                _CollProgramApplicationReportViewObj.Pagination.RecCount = _ReportMessages.Count();
                _CollProgramApplicationReportViewObj.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    _CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchResultsFilter = _ReportMessages.OrderByDescending(x => x.PID).ThenByDescending(x => x.Term).ThenByDescending(x => x.Program).ToList().Skip((_CollProgramApplicationReportViewObj.Pagination.PageIndex - 1) * _CollProgramApplicationReportViewObj.Pagination.PageSize).Take(_CollProgramApplicationReportViewObj.Pagination.PageSize).ToList();
                    _CollProgramApplicationReportViewObj.Pagination.PageCount = (_ReportMessages.Count() + _CollProgramApplicationReportViewObj.Pagination.PageSize - 1) / _CollProgramApplicationReportViewObj.Pagination.PageSize;
                }

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "CollProgramApplicationReports", "Error", ex.ToString());
            }

            return success;
        }

        public List<CollWaitListReportSearchResultsFilter> CollWaitListQuery(CollWaitListReportViewObj _CollWaitListReportViewObj)
        {
            List<CollWaitListReportSearchResultsFilter> _ListCollWaitListReportSearchResultsFilter = null;

            try
            {
                var _ReportMessages =
                            (from APPL in ctx.APPLICATIONS
                             from STAT in ctx.APPL_STATUSES.Where(x => x.APPLICATIONS_ID == APPL.APPLICATIONS_ID)
                             from NAE in ctx.PERSON.Where(y => y.ID == APPL.APPL_APPLICANT)
                             where (
                              STAT.POS == 1
                              && STAT.APPL_STATUS == "WTL"
                              )
                             select new CollWaitListReportSearchResultsFilter()
                             {
                                 APPL_APPLICATION_ID = APPL.APPLICATIONS_ID,
                                 APPL_START_TERM = APPL.APPL_START_TERM,
                                 FullName = NAE.LAST_NAME + ",  " + NAE.FIRST_NAME + ", " + NAE.MIDDLE_NAME,
                                 APPL_APPLICANT = APPL.APPL_APPLICANT,
                                 APPL_VISA_STATUS = APPL.APPL_RESIDENCY_STATUS,
                                 APPL_CITIZENSHIP = NAE.CITIZENSHIP,
                                 APPL_STATUS = STAT.APPL_STATUS,
                                 APPL_STATUS_DATE = STAT.APPL_STATUS_DATE,
                                 APPL_STATUS_TIME = STAT.APPL_STATUS_TIME,
                                 APPL_ACAD_PROGRAM = APPL.APPL_ACAD_PROGRAM,
                                 APPL_RESIDENCE = NAE.RESIDENCE_COUNTRY,

                             });

                // Filter by Status FullName
                if (!string.IsNullOrWhiteSpace(_CollWaitListReportViewObj.CollWaitListReportSearchFilter.FullName))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.FullName.ToUpper().Trim().Contains(_CollWaitListReportViewObj.CollWaitListReportSearchFilter.FullName.ToUpper().Trim()));
                }

                // Filter by APPL_START_TERM
                if (!string.IsNullOrWhiteSpace(_CollWaitListReportViewObj.CollWaitListReportSearchFilter.APPL_START_TERM))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.APPL_START_TERM.ToUpper().Trim().Contains(_CollWaitListReportViewObj.CollWaitListReportSearchFilter.APPL_START_TERM.ToUpper().Trim()));
                }

                // Filter by APPL_APPLICANT
                if (!string.IsNullOrWhiteSpace(_CollWaitListReportViewObj.CollWaitListReportSearchFilter.APPL_APPLICANT))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.APPL_APPLICANT.ToUpper().Trim().Contains(_CollWaitListReportViewObj.CollWaitListReportSearchFilter.APPL_APPLICANT.ToUpper().Trim()));
                }

                // Filter by APPL_STATUS
                if (!string.IsNullOrWhiteSpace(_CollWaitListReportViewObj.CollWaitListReportSearchFilter.APPL_STATUS))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.APPL_STATUS.ToUpper().Trim().Contains(_CollWaitListReportViewObj.CollWaitListReportSearchFilter.APPL_STATUS.ToUpper().Trim()));
                }

                // Filter by APPL_STATUS_DATE
                if (_CollWaitListReportViewObj.CollWaitListReportSearchFilter.APPL_STATUS_DATE != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.APPL_STATUS_DATE) == DbFunctions.TruncateTime(_CollWaitListReportViewObj.CollWaitListReportSearchFilter.APPL_STATUS_DATE));
                }

                // Filter by APPL_ACAD_PROGRAM
                if (!string.IsNullOrWhiteSpace(_CollWaitListReportViewObj.CollWaitListReportSearchFilter.APPL_ACAD_PROGRAM))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.APPL_ACAD_PROGRAM.ToUpper().Trim().Contains(_CollWaitListReportViewObj.CollWaitListReportSearchFilter.APPL_ACAD_PROGRAM.ToUpper().Trim()));
                }

                // Filter by APPL_VISA_STATUS
                if (!string.IsNullOrWhiteSpace(_CollWaitListReportViewObj.CollWaitListReportSearchFilter.APPL_VISA_STATUS))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.APPL_VISA_STATUS.ToUpper().Trim().Contains(_CollWaitListReportViewObj.CollWaitListReportSearchFilter.APPL_VISA_STATUS.ToUpper().Trim()));
                }

                // Filter by APPL_CITIZENSHIP
                if (!string.IsNullOrWhiteSpace(_CollWaitListReportViewObj.CollWaitListReportSearchFilter.APPL_CITIZENSHIP))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.APPL_CITIZENSHIP.ToUpper().Trim().Contains(_CollWaitListReportViewObj.CollWaitListReportSearchFilter.APPL_CITIZENSHIP.ToUpper().Trim()));
                }

                // Filter by APPL_RESIDENCE
                if (!string.IsNullOrWhiteSpace(_CollWaitListReportViewObj.CollWaitListReportSearchFilter.APPL_RESIDENCE))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.APPL_RESIDENCE.ToUpper().Trim().Contains(_CollWaitListReportViewObj.CollWaitListReportSearchFilter.APPL_RESIDENCE.ToUpper().Trim()));
                }

                _ListCollWaitListReportSearchResultsFilter = _ReportMessages.ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "CollWaitListQuery", "Error", ex.ToString());
            }

            return _ListCollWaitListReportSearchResultsFilter;
        }

        public bool CollWaitListReports(CollWaitListReportViewObj _CollWaitListReportViewObj)
        {
            bool success = false;
            try
            {
                var _ReportMessages = CollWaitListQuery(_CollWaitListReportViewObj);

                _CollWaitListReportViewObj.Pagination.RecCount = _ReportMessages.Count();
                _CollWaitListReportViewObj.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    _CollWaitListReportViewObj.CollWaitListReportSearchResultsFilter = _ReportMessages.OrderBy(x => x.APPL_ACAD_PROGRAM).ToList().Skip((_CollWaitListReportViewObj.Pagination.PageIndex - 1) * _CollWaitListReportViewObj.Pagination.PageSize).Take(_CollWaitListReportViewObj.Pagination.PageSize).ToList();
                    _CollWaitListReportViewObj.Pagination.PageCount = (_ReportMessages.Count() + _CollWaitListReportViewObj.Pagination.PageSize - 1) / _CollWaitListReportViewObj.Pagination.PageSize;
                }
                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "CollWaitListReports", "Error", ex.ToString());
            }

            return success;
        }

        public List<CollAdmissionConditionsReportSearchResultsFilter> CollAdmissionConditionsQuery(CollAdmissionConditionsReportViewObj _CollAdmissionConditionsReportViewObj)
        {
            List<CollAdmissionConditionsReportSearchResultsFilter> _ListCollAdmissionConditionsReportSearchResultsFilters = null;

            // Filter by Term
            List<string> startTerm = new List<string>();
            startTerm.Add(Functions.GetLcCurrentTerm());

            if (_CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.Term != null &&
                _CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.Term.Any())
            {
                startTerm = new List<string>();
                foreach (string term in _CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.Term)
                {
                    startTerm.Add(Functions.GetLcTerm(term.Trim().ToLower().Replace("null", "")));
                }
            }

            // Application Status
            List<string> appStatus = new List<string>();
            appStatus.Add("MS");
            appStatus.Add("OFC");
            appStatus.Add("CON");
            appStatus.Add("OFI");

            // Mailling Codes
            string termCode;
            List<string> mailingTermCodes = new List<string>();
            foreach (var term in startTerm)
            {
                termCode = "ADX" + term.Substring(0, 3);
                mailingTermCodes.Add(termCode + "OC" + term);
                mailingTermCodes.Add(termCode + "OI" + term);
                mailingTermCodes.Add(termCode + "IO" + term);
                mailingTermCodes.Add(termCode + "IC" + term);
                mailingTermCodes.Add(termCode + "SO" + term);
                mailingTermCodes.Add(termCode + "SI" + term);
            }

            try
            {
                var _ReportMessages = (
                                from APL in ctx.APPLICATIONS
                                from STA in ctx.APPL_STATUSES
                                from ML in ctx.MAILING
                                from NAME in ctx.PERSON
                                from CH in ctx.CH_CORR
                                from COMM in ctx.CC_COMMENTS
                                from FP in ctx.FOREIGN_PERSON

                                where (
                                    startTerm.Contains(APL.APPL_START_TERM)
                                    && STA.APPLICATIONS_ID == APL.APPLICATIONS_ID
                                    && STA.POS == 1
                                    && appStatus.Contains(STA.APPL_STATUS)
                                    && ML.MAILING_ID == APL.APPL_APPLICANT
                                    && NAME.ID == ML.MAILING_ID
                                    && CH.MAILING_ID == ML.MAILING_ID
                                    && CH.MAILING_CORR_RECVD_COMMENT != null
                                    && mailingTermCodes.Contains(CH.MAILING_CORR_RECEIVED + APL.APPL_START_TERM)
                                    && COMM.CC_COMMENTS_ID == CH.MAILING_CORR_RECVD_COMMENT
                                    && COMM.CC_COMMENTS_DESC != null
                                    && FP.FOREIGN_PERSON_ID == APL.APPL_APPLICANT
                                )
                                select new CollAdmissionConditionsReportSearchResultsFilter()
                                {
                                    ApplicationID = APL.APPLICATIONS_ID.ToUpper().Trim() + CH.MAILING_CORR_RECVD_INSTANCE.ToUpper().Trim(),
                                    //ApplicationID = APL.APPLICATIONS_ID,
                                    PersonID = ML.MAILING_ID,
                                    ApplicationProgram = APL.APPL_ACAD_PROGRAM,
                                    FullName = NAME.LAST_NAME + ", " + NAME.FIRST_NAME + ", " + NAME.MIDDLE_NAME,
                                    ApplicationStatus = STA.APPL_STATUS,
                                    Term = APL.APPL_START_TERM,
                                    AlienStatus = FP.FPER_ALIEN_STATUS,
                                    ConditionProgram = CH.MAILING_CORR_RECVD_INSTANCE,
                                    Condition = COMM.CC_COMMENTS_DESC

                                });

                // Filter by Person ID
                if (!string.IsNullOrWhiteSpace(_CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.PersonID))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.PersonID.ToUpper().Trim().Contains(_CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.PersonID.ToUpper().Trim()));
                }

                // Filter by Application ID
                if (!string.IsNullOrWhiteSpace(_CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.ApplicationID))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ApplicationID.ToUpper().Trim().Contains(_CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.ApplicationID.ToUpper().Trim()));
                }

                // Filter by Application Program
                if (_CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.ApplicationProgram != null && _CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.ApplicationProgram.Any())
                {
                    _ReportMessages = _ReportMessages.Where(a => _CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.ApplicationProgram.Contains(a.ApplicationProgram.ToUpper().Trim()));
                }

                // Filter by Application Status
                if (_CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.ApplicationStatus != null && _CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.ApplicationStatus.Any())
                {
                    _ReportMessages = _ReportMessages.Where(a => _CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.ApplicationStatus.Contains(a.ApplicationStatus.ToUpper().Trim()));
                }

                // Filter by Full Name
                if (!string.IsNullOrWhiteSpace(_CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.FullName))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.FullName.ToUpper().Trim().Contains(_CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.FullName.ToUpper().Trim()));
                }

                // Filter by Term
                //if (!string.IsNullOrWhiteSpace(_CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.Term))
                //{
                //    _ReportMessages = _ReportMessages.Where(a => a.Term.ToUpper().Trim().Contains(_CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.Term.ToUpper().Trim()));
                //}

                // Filter by Alien Status
                if (_CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.AlienStatus != null && _CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.AlienStatus.Any())
                {
                    _ReportMessages = _ReportMessages.Where(a => _CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.AlienStatus.Contains(a.AlienStatus.ToUpper().Trim()));
                }

                // Filter by Condition Program
                if (_CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.ConditionProgram != null && _CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.ConditionProgram.Any())
                {
                    _ReportMessages = _ReportMessages.Where(a => _CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.ConditionProgram.Contains(a.ConditionProgram.ToUpper().Trim()));
                }

                // Filter by Condition
                if (!string.IsNullOrWhiteSpace(_CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.Condition))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Condition.ToUpper().Trim().Contains(_CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter.Condition.ToUpper().Trim()));
                }

                _ListCollAdmissionConditionsReportSearchResultsFilters = _ReportMessages.ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "CollAdmissionConditionsQuery", "Error", ex.ToString());
            }

            return _ListCollAdmissionConditionsReportSearchResultsFilters;
        }

        public bool CollAdmissionConditionsReports(CollAdmissionConditionsReportViewObj _CollAdmissionConditionsReportViewObj)
        {
            bool success = false;
            try
            {
                var _ReportMessages = CollAdmissionConditionsQuery(_CollAdmissionConditionsReportViewObj);

                _CollAdmissionConditionsReportViewObj.Pagination.RecCount = _ReportMessages.Count();
                _CollAdmissionConditionsReportViewObj.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    _CollAdmissionConditionsReportViewObj.CollAdmissionConditionReportSearchResultsFilter = _ReportMessages.OrderBy(o => o.ApplicationProgram).ThenBy(t => t.FullName).ToList().Skip((_CollAdmissionConditionsReportViewObj.Pagination.PageIndex - 1) * _CollAdmissionConditionsReportViewObj.Pagination.PageSize).Take(_CollAdmissionConditionsReportViewObj.Pagination.PageSize).ToList();
                    _CollAdmissionConditionsReportViewObj.Pagination.PageCount = (_ReportMessages.Count() + _CollAdmissionConditionsReportViewObj.Pagination.PageSize - 1) / _CollAdmissionConditionsReportViewObj.Pagination.PageSize;
                }

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "CollAdmissionConditionsReports", "Error", ex.ToString());
            }

            return success;
        }

        public List<CollOverduesReportSearchResultsFilter> CollOverduesQuery(CollOverduesReportViewObj _CollOverduesReportViewObj)
        {
            List<CollOverduesReportSearchResultsFilter> _ListCollOverduesReportSearchResultsFilter = null;

            DateTime comparisondate = !_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Deadline.HasValue ? DateTime.Now : _CollOverduesReportViewObj.CollOverduesReportSearchFilter.Deadline.Value;

            // Filter by Term
            List<string> startTerm = new List<string>();
            startTerm.Add(Functions.GetLcCurrentTerm());

            if (_CollOverduesReportViewObj.CollOverduesReportSearchFilter.StartTerm != null && _CollOverduesReportViewObj.CollOverduesReportSearchFilter.StartTerm.Any())
            {
                startTerm = new List<string>();
                foreach (string term in _CollOverduesReportViewObj.CollOverduesReportSearchFilter.StartTerm)
                {
                    startTerm.Add(Functions.GetLcTerm(term.Trim().ToLower().Replace("null", "")));
                }
            }

            // Application Status
            List<string> appStatus = new List<string>();
            appStatus.Add("OFI");
            appStatus.Add("OFC");

            // Mailling Codes
            string termCode;
            List<string> mailingCodes = new List<string>();
            foreach (var term in startTerm)
            {
                termCode = "ADX" + term.Substring(0, 3);
                mailingCodes.Add(termCode + "OC");
                mailingCodes.Add(termCode + "OI");
                mailingCodes.Add(termCode + "IO");
                mailingCodes.Add(termCode + "IC");
            }

            try
            {
                var _ReportMessages =
                            (from APPL in ctx.APPLICATIONS

                             join CH in ctx.CH_CORR on APPL.APPL_APPLICANT equals CH.MAILING_ID into CH1
                             from CH in CH1.Where(c => c.MAILING_CORR_RECVD_INSTANCE.Trim().ToUpper().Contains(APPL.APPL_ACAD_PROGRAM.Trim().ToUpper())).DefaultIfEmpty()

                             join APS in ctx.APPL_STATUSES on APPL.APPLICATIONS_ID equals APS.APPLICATIONS_ID into APS1
                             from APS in APS1.Where(s => s.POS == 1).DefaultIfEmpty()

                             join PER in ctx.PERSON on APPL.APPL_APPLICANT equals PER.ID into PER1
                             from PER in PER1.DefaultIfEmpty()

                             join APH in ctx.ADR_PHONES on PER.PREFERRED_ADDRESS equals APH.ADDRESS_ID into APH1
                             from APH in APH1.Where(a => a.POS == 1 && (a.ADDRESS_PHONE_TYPE == "HO" || a.ADDRESS_PHONE_TYPE == null)).DefaultIfEmpty()

                             join PPH in ctx.PERPHONE on APPL.APPL_APPLICANT equals PPH.ID into PPH1
                             from PPH in PPH1.Where(p => p.POS == 1 && (p.PERSONAL_PHONE_TYPE == "CP" || p.PERSONAL_PHONE_TYPE == null)).DefaultIfEmpty()

                                 //where APPL.APPL_START_TERM == startTerm
                             where startTerm.Contains(APPL.APPL_START_TERM)
                             && appStatus.Contains(APS.APPL_STATUS)
                             && CH.MAILING_CORR_RECVD_ACT_DT < comparisondate
                             && mailingCodes.Contains(CH.MAILING_CORR_RECEIVED)

                             select new CollOverduesReportSearchResultsFilter()
                             {
                                 ApplicationID = APPL.APPLICATIONS_ID,
                                 ID = APPL.APPL_APPLICANT,
                                 ApplProgram = APPL.APPL_ACAD_PROGRAM,
                                 Status = APS.APPL_STATUS,
                                 FullName = PER.LAST_NAME + ",  " + PER.FIRST_NAME + ",  " + PER.MIDDLE_NAME,
                                 AlienStatus = ctx.FOREIGN_PERSON.Where(x => x.FOREIGN_PERSON_ID == APPL.APPL_APPLICANT).Select(s => s.FPER_ALIEN_STATUS).FirstOrDefault(),
                                 CondProgram = CH.MAILING_CORR_RECVD_INSTANCE,
                                 CondStatus = ctx.APPL_STATUSES.Where(x => x.POS == 1 && x.APPLICATIONS_ID == x.APPLICATION.APPLICATIONS_ID &&
                                                CH.MAILING_CORR_RECVD_INSTANCE.Trim().ToUpper().Contains(x.APPLICATION.APPL_ACAD_PROGRAM.Trim().ToUpper()) &&
                                                x.APPLICATION.APPL_APPLICANT == CH.MAILING_ID && x.APPLICATION.APPL_START_TERM == APPL.APPL_START_TERM).Select(s => s.APPL_STATUS).FirstOrDefault(),
                                 Deadline = CH.MAILING_CORR_RECVD_ACT_DT,
                                 Phone1 = APH.ADDRESS_PHONES,
                                 Type1 = APH.ADDRESS_PHONE_TYPE,
                                 Phone2 = PPH.PERSONAL_PHONE_NUMBER,
                                 Type2 = PPH.PERSONAL_PHONE_TYPE,
                                 StartTerm = APPL.APPL_START_TERM,
                                 Location = ctx.APPL_LOCATION_INFO.Where(x => x.APPLICATIONS_ID == APPL.APPLICATIONS_ID).Select(s => s.APPL_LOCATIONS).FirstOrDefault(),
                                 Comments = ctx.CC_COMMENTS.Where(x => x.CC_COMMENTS_ID == CH.MAILING_CORR_RECVD_COMMENT).Select(s => s.CC_COMMENTS_TEXT).FirstOrDefault(),
                             });  //.Distinct();

                // Filter by ID
                if (!string.IsNullOrWhiteSpace(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.ID))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ID.ToUpper().Trim().Contains(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.ID.ToUpper().Trim()));
                }

                // Filter by ApplProgram
                if (!string.IsNullOrWhiteSpace(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.ApplProgram))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ApplProgram.ToUpper().Trim().Contains(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.ApplProgram.ToUpper().Trim()));
                }

                // Filter by Status
                if (!string.IsNullOrWhiteSpace(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Status))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Status.ToUpper().Trim().Contains(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Status.ToUpper().Trim()));
                }

                // Filter by FullName
                if (!string.IsNullOrWhiteSpace(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.FullName))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.FullName.ToUpper().Trim().Contains(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.FullName.ToUpper().Trim()));
                }

                // Filter by AlineStatus
                if (_CollOverduesReportViewObj.CollOverduesReportSearchFilter.AlienStatus != null && _CollOverduesReportViewObj.CollOverduesReportSearchFilter.AlienStatus.Any())
                {
                    //_ReportMessages = _ReportMessages.Where(a => a.AlienStatus.ToUpper().Trim().Contains(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.AlienStatus.ToUpper().Trim()));
                    _ReportMessages = _ReportMessages.Where(a => _CollOverduesReportViewObj.CollOverduesReportSearchFilter.AlienStatus.Contains(a.AlienStatus.ToUpper().Trim()));
                }

                // Filter by CondProgram
                if (!string.IsNullOrWhiteSpace(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.CondProgram))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.CondProgram.ToUpper().Trim().Contains(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.CondProgram.ToUpper().Trim()));
                }

                // Filter by Phone1
                if (!string.IsNullOrWhiteSpace(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Phone1))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Phone1.ToUpper().Trim().Contains(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Phone1.ToUpper().Trim()));
                }
                // Filter by Type1
                if (!string.IsNullOrWhiteSpace(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Type1))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Type1.ToUpper().Trim().Contains(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Type1.ToUpper().Trim()));
                }
                // Filter by Phone2
                if (!string.IsNullOrWhiteSpace(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Phone2))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Phone2.ToUpper().Trim().Contains(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Phone2.ToUpper().Trim()));
                }
                // Filter by Type2
                if (!string.IsNullOrWhiteSpace(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Type2))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Type2.ToUpper().Trim().Contains(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Type2.ToUpper().Trim()));
                }
                // Filter by DEADLINE
                //if (_CollOverduesReportViewObj.CollOverduesReportSearchFilter.DEADLINE != null)
                //{
                //    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.DEADLINE) == DbFunctions.TruncateTime(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.DEADLINE));
                //}
                // Filter by StartTerm
                //if (!string.IsNullOrWhiteSpace(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Start_term))
                //{
                //    _ReportMessages = _ReportMessages.Where(a => a.Type2.ToUpper().Trim().Contains(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Start_term.ToUpper().Trim()));
                //}

                // Filter by Location
                if (_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Location != null && _CollOverduesReportViewObj.CollOverduesReportSearchFilter.Location.Any())
                {
                    _ReportMessages = _ReportMessages.Where(a => _CollOverduesReportViewObj.CollOverduesReportSearchFilter.Location.Contains(a.Location.ToUpper().Trim()));
                }

                // Filter by Comments
                if (!string.IsNullOrWhiteSpace(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Comments))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Comments.Contains(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Comments.Trim()));
                }

                _ListCollOverduesReportSearchResultsFilter = _ReportMessages.ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "CollOverduesQuery", "Error", ex.ToString());
            }

            return _ListCollOverduesReportSearchResultsFilter;
        }

        public bool CollOverduesReports(CollOverduesReportViewObj _CollOverduesReportViewObj)
        {
            bool success = false;
            try
            {
                var _ReportMessages = CollOverduesQuery(_CollOverduesReportViewObj);

                _CollOverduesReportViewObj.Pagination.RecCount = _ReportMessages.Count();
                _CollOverduesReportViewObj.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    _CollOverduesReportViewObj.CollOverduesReportSearchResultsFilter = _ReportMessages.OrderBy(x => x.FullName).ThenBy(o => o.CondProgram).ToList().Skip((_CollOverduesReportViewObj.Pagination.PageIndex - 1) * _CollOverduesReportViewObj.Pagination.PageSize).Take(_CollOverduesReportViewObj.Pagination.PageSize).ToList();
                    _CollOverduesReportViewObj.Pagination.PageCount = (_ReportMessages.Count() + _CollOverduesReportViewObj.Pagination.PageSize - 1) / _CollOverduesReportViewObj.Pagination.PageSize;
                }

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "CollAdmissionConditionsReports", "Error", ex.ToString());
            }

            return success;
        }

        public List<CollMissingGradeReportSearchResultsFilter> CollMissingGradeQuery(CollMissingGradeReportViewObj _CollMissingGradeReportViewObj)
        {
            List<CollMissingGradeReportSearchResultsFilter> _ListCollMissingGradeReportSearchResultsFilter = null;

            try
            {
                var _ReportMessages =
                            (
                            from STA in ctx.STUDENT_ACAD_CRED
                            from STAL in ctx.STUDENT_ACAD_CRED_LS
                            from PNAME in ctx.PERSON
                            from STAT in ctx.STC_STATUSES
                            from SEC in ctx.COURSE_SECTIONS
                            from CSL in ctx.COURSE_SECTIONS_LS
                            from SCS in ctx.STUDENT_COURSE_SEC
                            from CSF in ctx.COURSE_SEC_FACULTY
                            from FNAME in ctx.PERSON

                            where
                            STAT.POS == 1
                            && STA.STC_STUDENT_COURSE_SEC == SCS.STUDENT_COURSE_SEC_ID

                            && SCS.SCS_COURSE_SECTION == SEC.COURSE_SECTIONS_ID

                            && SEC.COURSE_SECTIONS_ID == CSL.COURSE_SECTIONS_ID

                            && CSL.SEC_FACULTY == CSF.COURSE_SEC_FACULTY_ID

                            && CSF.CSF_FACULTY == FNAME.ID

                            && STA.STUDENT_ACAD_CRED_ID == STAT.STUDENT_ACAD_CRED_ID

                            && STA.STUDENT_ACAD_CRED_ID == STAL.STUDENT_ACAD_CRED_ID
                            && STAL.POS == 1

                            && STA.STC_PERSON_ID == PNAME.ID

                            && STA.STC_END_DATE <= DateTime.Now
                            //DateTime("2020/08/30")

                            && (STAT.STC_STATUS == "N"

                            || STAT.STC_STATUS == "A")

                            && STA.STC_ACAD_LEVEL == "PS"
                            && STA.STC_FINAL_GRADE == null
                             && STA.STC_VERIFIED_GRADE == null
                             && SEC.SEC_GRADE_SCHEME == null

                            select new CollMissingGradeReportSearchResultsFilter()
                            {
                                Sec = STA.STC_SECTION_NO,
                                StuID = STA.STC_PERSON_ID,
                                StuLastName = PNAME.LAST_NAME,
                                StuFirstName = PNAME.FIRST_NAME,
                                InstrID = FNAME.ID,
                                InstrLastName = FNAME.LAST_NAME,
                                InstrFirstName = FNAME.FIRST_NAME,
                                FinGrade = STA.STC_FINAL_GRADE,
                                VerGrade = STA.STC_VERIFIED_GRADE,
                                MidTerm = SCS.SCS_MID_TERM_GRADE1,
                                LastDateAttend = SCS.SCS_LAST_ATTEND_DATE,
                                NvrAttend = SCS.SCS_NEVER_ATTENDED_FLAG,
                                Dept = STAL.STC_DEPTS,
                                SecStarts = SEC.SEC_START_DATE,
                                SecEnds = SEC.SEC_END_DATE,
                                AcdLv = STA.STC_ACAD_LEVEL,
                                Term = STA.STC_TERM
                            }).Distinct();


                // Filter by Course
                if (!string.IsNullOrWhiteSpace(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.Course))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Course.ToUpper().Trim().Contains(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.Course.ToUpper().Trim()));
                }

                // Filter by Sec
                if (!string.IsNullOrWhiteSpace(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.Sec))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Sec.ToUpper().Trim().Contains(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.Sec.ToUpper().Trim()));
                }

                // Filter by StuID
                if (!string.IsNullOrWhiteSpace(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.StuID))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.StuID.ToUpper().Trim().Contains(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.StuID.ToUpper().Trim()));
                }

                // Filter by InstrID
                if (!string.IsNullOrWhiteSpace(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.InstrID))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.InstrID.ToUpper().Trim().Contains(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.InstrID.ToUpper().Trim()));
                }

                // Filter by StuLastName
                if (_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.StuLastName != null && _CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.StuLastName.Any())
                {
                    _ReportMessages = _ReportMessages.Where(a => _CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.StuLastName.Contains(a.StuLastName.ToUpper().Trim()));
                }

                // Filter by StuFirstName
                if (!string.IsNullOrWhiteSpace(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.StuFirstName))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.StuFirstName.ToUpper().Trim().Contains(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.StuFirstName.ToUpper().Trim()));
                }

                // Filter by FinGrade
                if (!string.IsNullOrWhiteSpace(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.FinGrade))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.FinGrade.ToUpper().Trim().Contains(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.FinGrade.ToUpper().Trim()));
                }
                // Filter by VerGrade
                if (!string.IsNullOrWhiteSpace(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.VerGrade))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.VerGrade.ToUpper().Trim().Contains(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.VerGrade.ToUpper().Trim()));
                }
                // Filter by MidTerm
                if (!string.IsNullOrWhiteSpace(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.MidTerm))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.MidTerm.ToUpper().Trim().Contains(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.MidTerm.ToUpper().Trim()));
                }
                // Filter by LastDateAttend
                if (_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.LastDateAttend != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.LastDateAttend) == DbFunctions.TruncateTime(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.LastDateAttend));
                }
                // Filter by DEADLINE
                //if (_CollOverduesReportViewObj.CollOverduesReportSearchFilter.DEADLINE != null)
                //{
                //    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.DEADLINE) == DbFunctions.TruncateTime(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.DEADLINE));
                //}
                // Filter by StartTerm
                //if (!string.IsNullOrWhiteSpace(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Start_term))
                //{
                //    _ReportMessages = _ReportMessages.Where(a => a.Type2.ToUpper().Trim().Contains(_CollOverduesReportViewObj.CollOverduesReportSearchFilter.Start_term.ToUpper().Trim()));
                //}

                // Filter by NvrAttend
                if (_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.NvrAttend != null && _CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.NvrAttend.Any())
                {
                    _ReportMessages = _ReportMessages.Where(a => _CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.NvrAttend.Contains(a.NvrAttend.ToUpper().Trim()));
                }

                // Filter by Dept
                if (!string.IsNullOrWhiteSpace(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.Dept))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Dept.Contains(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.Dept.Trim()));
                }

                // Filter by SecStarts
                if (_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.SecStarts != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.SecStarts) == DbFunctions.TruncateTime(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.SecStarts));
                }

                // Filter by SecEnds
                if (_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.SecEnds != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.SecEnds) == DbFunctions.TruncateTime(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.SecEnds));
                }

                // Filter by AcdLv
                if (!string.IsNullOrWhiteSpace(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.AcdLv))
                {
                    _ReportMessages = _ReportMessages.Where(a => _CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.AcdLv.Contains(a.AcdLv.ToUpper().Trim()));
                }

                // Filter by Term
                if (!string.IsNullOrWhiteSpace(_CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.Term))
                {
                    _ReportMessages = _ReportMessages.Where(a => _CollMissingGradeReportViewObj.CollMissingGradeReportSearchFilter.Term.Contains(a.Term.ToUpper().Trim()));
                }

                _ListCollMissingGradeReportSearchResultsFilter = _ReportMessages.ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "CollMissingGradeQuery", "Error", ex.ToString());
            }

            return _ListCollMissingGradeReportSearchResultsFilter;
        }

        public bool CollMissingGradeReports(CollMissingGradeReportViewObj _CollMissingGradeReportViewObj)
        {
            bool success = false;
            try
            {
                var _ReportMessages = CollMissingGradeQuery(_CollMissingGradeReportViewObj);

                _CollMissingGradeReportViewObj.Pagination.RecCount = _ReportMessages.Count();
                _CollMissingGradeReportViewObj.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    _CollMissingGradeReportViewObj.CollMissingGradeReportSearchResultsFilter = _ReportMessages.OrderBy(o => o.Course).ToList().Skip((_CollMissingGradeReportViewObj.Pagination.PageIndex - 1) * _CollMissingGradeReportViewObj.Pagination.PageSize).Take(_CollMissingGradeReportViewObj.Pagination.PageSize).ToList();
                    _CollMissingGradeReportViewObj.Pagination.PageCount = (_ReportMessages.Count() + _CollMissingGradeReportViewObj.Pagination.PageSize - 1) / _CollMissingGradeReportViewObj.Pagination.PageSize;
                }

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "CollMissingGradeReports", "Error", ex.ToString());
            }

            return success;
        }

        public List<CollTestingResultsReportSearchResultsFilter> CollTestingResultsQuery(CollTestingResultsReportViewObj _CollTestingResultsReportViewObj)
        {
            List<CollTestingResultsReportSearchResultsFilter> _ListCollTestingResultsReportSearchResultsFilter = null;

            DateTime today = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            DateTime comparisonDate = !_CollTestingResultsReportViewObj.CollTestingResultsReportSearchFilter.STATUS_DATE.HasValue ? today : new DateTime(_CollTestingResultsReportViewObj.CollTestingResultsReportSearchFilter.STATUS_DATE.Value.Year, _CollTestingResultsReportViewObj.CollTestingResultsReportSearchFilter.STATUS_DATE.Value.Month, _CollTestingResultsReportViewObj.CollTestingResultsReportSearchFilter.STATUS_DATE.Value.Day);
            try
            {
                var _ReportMessages =
                            (from NC in ctx.STUDENT_NON_COURSES
                             from PE in ctx.PERSON
                             where (
                               NC.STNC_PERSON_ID == PE.ID
                               && NC.STNC_STATUS_DATE == comparisonDate
                              )
                             select new CollTestingResultsReportSearchResultsFilter()
                             {
                                 PERSON_ID = NC.STNC_PERSON_ID,
                                 LAST_NAME = PE.LAST_NAME,
                                 FIRST_NAME = PE.FIRST_NAME,
                                 MIDDLE_NAME = PE.MIDDLE_NAME,
                                 STATUS_DATE = NC.STNC_STATUS_DATE

                             }).Distinct();

                // Filter by Person ID
                if (!string.IsNullOrWhiteSpace(_CollTestingResultsReportViewObj.CollTestingResultsReportSearchFilter.PERSON_ID))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.PERSON_ID.ToUpper().Trim().Contains(_CollTestingResultsReportViewObj.CollTestingResultsReportSearchFilter.PERSON_ID.ToUpper().Trim()));
                }

                // Filter by Last Name
                if (!string.IsNullOrWhiteSpace(_CollTestingResultsReportViewObj.CollTestingResultsReportSearchFilter.LAST_NAME))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.LAST_NAME.ToUpper().Trim().Contains(_CollTestingResultsReportViewObj.CollTestingResultsReportSearchFilter.LAST_NAME.ToUpper().Trim()));
                }

                // Filter by First Name
                if (!string.IsNullOrWhiteSpace(_CollTestingResultsReportViewObj.CollTestingResultsReportSearchFilter.FIRST_NAME))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.FIRST_NAME.ToUpper().Trim().Contains(_CollTestingResultsReportViewObj.CollTestingResultsReportSearchFilter.FIRST_NAME.ToUpper().Trim()));
                }

                // Filter by Middle Name
                if (!string.IsNullOrWhiteSpace(_CollTestingResultsReportViewObj.CollTestingResultsReportSearchFilter.MIDDLE_NAME))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.MIDDLE_NAME.ToUpper().Trim().Contains(_CollTestingResultsReportViewObj.CollTestingResultsReportSearchFilter.MIDDLE_NAME.ToUpper().Trim()));
                }

                // Filter by APPL_STATUS_DATE
                //if (_CollTestingResultsReportViewObj.CollTestingResultsReportSearchFilter.STATUS_DATE != null)
                //{
                //    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.STATUS_DATE) == DbFunctions.TruncateTime(_CollTestingResultsReportViewObj.CollTestingResultsReportSearchFilter.STATUS_DATE));
                //}

                _ListCollTestingResultsReportSearchResultsFilter = _ReportMessages.ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "CollTestingResultsQuery", "Error", ex.ToString());
            }

            return _ListCollTestingResultsReportSearchResultsFilter;
        }

        public bool CollTestingResultsReports(CollTestingResultsReportViewObj _CollTestingResultsReportViewObj)
        {
            bool success = false;
            try
            {
                var _ReportMessages = CollTestingResultsQuery(_CollTestingResultsReportViewObj);

                _CollTestingResultsReportViewObj.Pagination.RecCount = _ReportMessages.Count();
                _CollTestingResultsReportViewObj.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    _CollTestingResultsReportViewObj.CollTestingResultsReportSearchResultsFilter = _ReportMessages.OrderBy(x => x.LAST_NAME).ThenBy(x => x.FIRST_NAME).ThenBy(x => x.MIDDLE_NAME).ToList().Skip((_CollTestingResultsReportViewObj.Pagination.PageIndex - 1) * _CollTestingResultsReportViewObj.Pagination.PageSize).Take(_CollTestingResultsReportViewObj.Pagination.PageSize).ToList();
                    _CollTestingResultsReportViewObj.Pagination.PageCount = (_ReportMessages.Count() + _CollTestingResultsReportViewObj.Pagination.PageSize - 1) / _CollTestingResultsReportViewObj.Pagination.PageSize;
                }

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "CollTestingResultsReports", "Error", ex.ToString());
            }

            return success;
        }

        public List<CollProgramApplicationReportSearchResultsFilter> GetProgramApplicationReportByIds(string[] Id, bool allSelected = false, string filterFields = null)
        {
            List<CollProgramApplicationReportSearchResultsFilter> _ProgramApplications = new List<CollProgramApplicationReportSearchResultsFilter>();

            try
            {
                if (Id != null && Id.Count() > 0)
                {
                    // Deserialize filter fields
                    CollProgramApplicationReportViewObj _CollProgramApplicationReportViewObj = new CollProgramApplicationReportViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _CollProgramApplicationReportViewObj.CollProgramApplicationReportSearchFilter = JsonConvert.DeserializeObject<CollProgramApplicationReportSearchFilter>(filterFields);
                    };

                    var query = CollProgramApplicationQuery(_CollProgramApplicationReportViewObj);

                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => Id.Contains(a.ID.ToUpper().Trim())).ToList();
                    }

                    _ProgramApplications = query.OrderByDescending(x => x.ID).ThenByDescending(x => x.Term).ThenByDescending(x => x.Program).ToList();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetCollProgramApplicationReportByIds", "Error", ex.ToString());
            }
            return _ProgramApplications;
        }

        public List<CollAdmissionConditionsReportSearchResultsFilter> GetCollAdmissionConditionsReportByIds(string[] Id, bool allSelected = false, string filterFields = null)
        {
            List<CollAdmissionConditionsReportSearchResultsFilter> _AdmissionConditions = new List<CollAdmissionConditionsReportSearchResultsFilter>();
            try
            {
                if (Id != null && Id.Count() > 0)
                {
                    // Deserialize filter fields
                    CollAdmissionConditionsReportViewObj _CollAdmissionConditionsReportViewObj = new CollAdmissionConditionsReportViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _CollAdmissionConditionsReportViewObj.CollAdmissionConditionsReportSearchFilter = JsonConvert.DeserializeObject<CollAdmissionConditionsReportSearchFilter>(filterFields);
                    };
                        
                    var query = CollAdmissionConditionsQuery(_CollAdmissionConditionsReportViewObj);
                    
                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => Id.Contains(a.ApplicationID.ToUpper().Trim())).ToList();
                    }

                    _AdmissionConditions = query.OrderBy(o => o.ApplicationProgram).ThenBy(t => t.FullName).ToList();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetCollAdmissionConditionsReportByIds", "Error", ex.ToString());
            }
            return _AdmissionConditions;
        }

        public List<CollMissingGradeReportSearchResultsFilter> GetCollMissingGradeReportByIds(string id, DateTime date, string termStartDate, string stat)
        {
            List<CollMissingGradeReportSearchResultsFilter> _MissingGrade = new List<CollMissingGradeReportSearchResultsFilter>();
            try
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    var query = (
                            from STA in ctx.STUDENT_ACAD_CRED
                            from STAL in ctx.STUDENT_ACAD_CRED_LS
                            from PNAME in ctx.PERSON
                            from STAT in ctx.STC_STATUSES
                            from SEC in ctx.COURSE_SECTIONS
                            from CSL in ctx.COURSE_SECTIONS_LS
                            from SCS in ctx.STUDENT_COURSE_SEC
                            from CSF in ctx.COURSE_SEC_FACULTY
                            from FNAME in ctx.PERSON

                            where
                            STAT.POS == 1
                            && STA.STC_STUDENT_COURSE_SEC == SCS.STUDENT_COURSE_SEC_ID

                            && SCS.SCS_COURSE_SECTION == SEC.COURSE_SECTIONS_ID

                            && SEC.COURSE_SECTIONS_ID == CSL.COURSE_SECTIONS_ID

                            && CSL.SEC_FACULTY == CSF.COURSE_SEC_FACULTY_ID

                            && CSF.CSF_FACULTY == FNAME.ID

                            && STA.STUDENT_ACAD_CRED_ID == STAT.STUDENT_ACAD_CRED_ID

                            && STA.STUDENT_ACAD_CRED_ID == STAL.STUDENT_ACAD_CRED_ID
                            && STAL.POS == 1

                            && STA.STC_PERSON_ID == PNAME.ID

                            && STA.STC_END_DATE <= DateTime.Now
                            //DateTime("2020/08/30")

                            && (STAT.STC_STATUS == "N"

                            || STAT.STC_STATUS == "A")

                            && STA.STC_ACAD_LEVEL == "PS"
                            && STA.STC_FINAL_GRADE == null
                             && STA.STC_VERIFIED_GRADE == null
                             && SEC.SEC_GRADE_SCHEME == null

                            select new CollMissingGradeReportSearchResultsFilter()
                            {
                                Sec = STA.STC_SECTION_NO,
                                StuID = STA.STC_PERSON_ID,
                                StuLastName = PNAME.LAST_NAME,
                                StuFirstName = PNAME.FIRST_NAME,
                                InstrID = FNAME.ID,
                                InstrLastName = FNAME.LAST_NAME,
                                InstrFirstName = FNAME.FIRST_NAME,
                                FinGrade = STA.STC_FINAL_GRADE,
                                VerGrade = STA.STC_VERIFIED_GRADE,
                                MidTerm = SCS.SCS_MID_TERM_GRADE1,
                                LastDateAttend = SCS.SCS_LAST_ATTEND_DATE,
                                NvrAttend = SCS.SCS_NEVER_ATTENDED_FLAG,
                                Dept = STAL.STC_DEPTS,
                                SecStarts = SEC.SEC_START_DATE,
                                SecEnds = SEC.SEC_END_DATE,
                                AcdLv = STA.STC_ACAD_LEVEL,
                                Term = STA.STC_TERM
                            }).Distinct();

                    _MissingGrade = query.OrderBy(x => x.Course).ToList();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetLanguages", "Error", ex.ToString());
            }
            return _MissingGrade;
        }

        public List<CollOverduesReportSearchResultsFilter> GetCollOverduesReportByIds(string[] Id, bool allSelected = false, string filterFields = null)
        {
            List<CollOverduesReportSearchResultsFilter> _Overdues = new List<CollOverduesReportSearchResultsFilter>();
            try
            {
                if (Id != null && Id.Count() > 0)
                {
                    // Deserialize filter fields
                    CollOverduesReportViewObj _CollOverduesReportViewObj = new CollOverduesReportViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _CollOverduesReportViewObj.CollOverduesReportSearchFilter = JsonConvert.DeserializeObject<CollOverduesReportSearchFilter>(filterFields);
                    };

                    var query = CollOverduesQuery(_CollOverduesReportViewObj);

                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => Id.Contains((a.ApplicationID ?? "") + ";" + (a.Deadline == null ? "" : a.Deadline.Value.ToString("yyyy/M/d")) + ";" + (a.StartTerm ?? "") + ";" + (a.Status ?? ""))).ToList();
                    }

                    _Overdues = query.OrderBy(o => o.ApplProgram).ThenBy(t => t.FullName).ToList();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetCollOverduesReportByIds", "Error", ex.ToString());
            }
            return _Overdues;
        }

        public List<CollWaitListReportSearchResultsFilter> GetWaitListReportByIds(string[] Id, bool allSelected = false, string filterFields = null)
        {
            List<CollWaitListReportSearchResultsFilter> _WaitLists = new List<CollWaitListReportSearchResultsFilter>();
            try
            {
                if (Id != null && Id.Count() > 0)
                {
                    // Deserialize filter fields
                    CollWaitListReportViewObj _CollWaitListReportViewObj = new CollWaitListReportViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _CollWaitListReportViewObj.CollWaitListReportSearchFilter = JsonConvert.DeserializeObject<CollWaitListReportSearchFilter>(filterFields);
                    };

                    var query = CollWaitListQuery(_CollWaitListReportViewObj);

                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => Id.Contains(a.APPL_APPLICATION_ID.ToUpper().Trim())).ToList();
                    }

                    _WaitLists = query.OrderBy(o => o.APPL_ACAD_PROGRAM).ThenBy(t => t.FullName).ToList();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetWaitListReportByIds", "Error", ex.ToString());
            }
            return _WaitLists;
        }

        public List<CollTestingResultsReportSearchResultsFilter> GetCollTestingResultsReportByIds(string[] Id, bool allSelected = false, string filterFields = null)
        {
            List<CollTestingResultsReportSearchResultsFilter> _TestingResults = new List<CollTestingResultsReportSearchResultsFilter>();
            try
            {
                if (Id != null && Id.Count() > 0)
                {
                    // Deserialize filter fields
                    CollTestingResultsReportViewObj _CollTestingResultsReportViewObj = new CollTestingResultsReportViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _CollTestingResultsReportViewObj.CollTestingResultsReportSearchFilter = JsonConvert.DeserializeObject<CollTestingResultsReportSearchFilter>(filterFields);
                    };

                    var query = CollTestingResultsQuery(_CollTestingResultsReportViewObj);

                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => Id.Contains((a.PERSON_ID ?? "") + ";" + (a.STATUS_DATE == null ? "" : a.STATUS_DATE.Value.ToString("yyyy/MM/dd")))).ToList();
                    }

                    _TestingResults = query.OrderBy(x => x.LAST_NAME).ThenBy(x => x.FIRST_NAME).ThenBy(x => x.MIDDLE_NAME).ToList();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetCollTestingResultsReportByIds", "Error", ex.ToString());
            }
            return _TestingResults;
        }

        public List<UnsolicitedBatchObj> GetUnsolicitedBatchTranscriptReportByIds(string[] Id, bool allSelected = false, string filterFields = null)
        {
            List<UnsolicitedBatchObj> _UnsolicitedBatchObj = new List<UnsolicitedBatchObj>();

            try
            {
                if (Id != null && Id.Count() > 0)
                {
                    // Deserialize filter fields
                    UnsolicitedBatchListViewObj _UnsolicitedBatchListViewObj = new UnsolicitedBatchListViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _UnsolicitedBatchListViewObj.SearchFilter = JsonConvert.DeserializeObject<UnsolicitedBatchSearchObj>(filterFields);
                    };

                    var query = UnsolicitedBatchTranscriptQuery(_UnsolicitedBatchListViewObj);

                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => Id.Contains(a.sNumber.ToUpper().Trim())).ToList();
                    }

                    _UnsolicitedBatchObj = query.OrderBy(x => x.FullName).ToList();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetUnsolicitedBatchTranscriptReportByIds", "Error", ex.ToString());
            }
            return _UnsolicitedBatchObj;
        }

        public List<TRRQRequestObj> GetMyCredsRequestReportByIds(string[] Id, bool allSelected = false, string filterFields = null)
        {
            List<TRRQRequestObj> _TRRQRequestObj = new List<TRRQRequestObj>();

            try
            {
                if (Id != null && Id.Count() > 0)
                {
                    // Deserialize filter fields
                    TRRQRequestListViewObj _TRRQRequestListViewObj = new TRRQRequestListViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _TRRQRequestListViewObj.SearchFilter = JsonConvert.DeserializeObject<TRRQRequestSearchObj>(filterFields);
                    };

                    var query = MyCredsRequestQuery(_TRRQRequestListViewObj);

                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => Id.Contains(a.RequestID.ToUpper().Trim())).ToList();
                    }

                    _TRRQRequestObj = query.OrderByDescending(x => x.RequestDate).ThenBy(x => x.FullName).ToList();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetMyCredsRequestReportByIds", "Error", ex.ToString());
            }
            return _TRRQRequestObj;
        }

        public List<StudentObj> GetMyCredsBulkSendReportByIds(string[] Id, bool allSelected = false, string filterFields = null)
        {
            List<StudentObj> _StudentObj = new List<StudentObj>();

            try
            {
                if (Id != null && Id.Count() > 0)
                {
                    // Deserialize filter fields
                    BulkSendListViewObj _BulkSendListViewObj = new BulkSendListViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _BulkSendListViewObj.SearchFilter = JsonConvert.DeserializeObject<SendBulkSearchObj>(filterFields);
                    };

                    var query = MyCredsBulkSendStudentQuery(_BulkSendListViewObj);

                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => Id.Contains(a.AcadCredentialsID.ToUpper().Trim())).ToList();
                    }

                    _StudentObj = query.OrderBy(x => x.FullName).ThenBy(x => x.AcadCCDDate).ToList();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetMyCredsBulkSendReportByIds", "Error", ex.ToString());
            }
            return _StudentObj;
        }

        public string GetCollWaitListReportXmlString(string item)
        {
            string xmlString = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    var result = (from APPL in ctx.APPLICATIONS.Where(y => y.APPLICATIONS_ID == item)
                                  from STAT in ctx.APPL_STATUSES.Where(x => x.APPLICATIONS_ID == APPL.APPLICATIONS_ID)
                                  from NAE in ctx.PERSON.Where(y => y.ID == APPL.APPL_APPLICANT)
                                  where (
                                   STAT.POS == 1
                                   && STAT.APPL_STATUS == "WTL"
                                   )
                                  select new CollWaitListReportSearchResultsFilter()
                                  {
                                      APPL_APPLICATION_ID = APPL.APPLICATIONS_ID,
                                      APPL_START_TERM = APPL.APPL_START_TERM,
                                      FullName = NAE.LAST_NAME + ", " + NAE.FIRST_NAME + ", " + NAE.MIDDLE_NAME,
                                      APPL_APPLICANT = APPL.APPL_APPLICANT,
                                      APPL_STATUS = STAT.APPL_STATUS,
                                      APPL_STATUS_DATE = STAT.APPL_STATUS_DATE,
                                      APPL_ACAD_PROGRAM = APPL.APPL_ACAD_PROGRAM,

                                  });

                    if (result.Any())
                    {
                        xmlString = result.FirstOrDefault().Serialize();
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetCollWaitListReportXmlString", "Error", ex.ToString());
            }
            return xmlString;
        }

        #endregion


        #region Transfer Credit

        public List<TransferCreditSearchResultsFilter> TransferCreditQuery(TransferCreditViewObj _TransferCreditViewObj)
        {
            List<TransferCreditSearchResultsFilter> _ListTransferCreditSearchResultsFilter = null;

            // Load last 5 years
            List<int> years = new List<int>();
            int currentYear = DateTime.Now.Year;
            for (int i = 0; i <= 4; i++)
            {
                years.Add(currentYear - i);
            }

            try
            {
                var _ReportMessages = (
                                     from SC in ctx.STUDENT_ACAD_CRED
                                     join TER in ctx.TERMS on SC.STC_TERM equals TER.TERMS_ID into TERS
                                     from TER in TERS.DefaultIfEmpty()
                                     join STPR in ctx.STPR_DATES on SC.STC_PERSON_ID equals STPR.STUDENT_PROGRAMS_ID.Substring(0, 7) into STPRS
                                     from STPR in STPRS.Where(x => (TER.TERM_START_DATE >= x.STPR_START_DATE) &&
                                                                     ((x.STPR_END_DATE == null) || TER.TERM_START_DATE <= x.STPR_END_DATE) &&
                                                                    x.STUDENT_PROGRAMS_ID.Substring(8, 13) != "NCR").DefaultIfEmpty()
                                     join NAME in ctx.PERSON on SC.STC_PERSON_ID equals NAME.ID into NAMES
                                     from NAME in NAMES.DefaultIfEmpty()
                                     join ASN in ctx.PERSON_ALT on NAME.ID equals ASN.ID into ASNS
                                     from ASN in ASNS.Where(x => x.PERSON_ALT_ID_TYPES == "ALTA").DefaultIfEmpty()
                                     join AC in ctx.ACAD_PROGRAMS on STPR.STUDENT_PROGRAMS_ID.Substring(8, 13) equals AC.ACAD_PROGRAMS_ID into ACS
                                     from AC in ACS.Where(x => x.ACPG_ACAD_LEVEL != "NCR").DefaultIfEmpty()
                                     join ACLS in ctx.ACAD_PROGRAMS_LS on AC.ACAD_PROGRAMS_ID equals ACLS.ACAD_PROGRAMS_ID into ACLSS
                                     from ACLS in ACLSS.Where(x => x.POS == 1).DefaultIfEmpty()
                                     where (
                                         !SC.STC_COURSE_NAME.Contains("ICP")
                                        && SC.STC_VERIFIED_GRADE == "125"
                                        && years.Contains((int)TER.TERM_REPORTING_YEAR)
                                       )

                                     select new TransferCreditSearchResultsFilter()
                                     {
                                         sNumber = SC.STC_PERSON_ID,
                                         ASN = ASN.PERSON_ALT_IDS,
                                         BirthDate = NAME.BIRTH_DATE,
                                         Provider = "Lethbridge College",
                                         AcademicYear = ((int)TER.TERM_REPORTING_YEAR).ToString(),
                                         StartTermDate = TER.TERM_START_DATE,
                                         ProgramID = "REPLACE PROGRAM ID",
                                         SpecializationID = ACLS.ACPG_LOCAL_GOVT_CODES.ToString(),
                                         FromInstitution = "Prior Learning",
                                         FromInstitutionLoc = "",
                                         FromInstitutionCourseCode = "Prior Learning",
                                         FromInstitutionAcademicYearCourseTaken = "",
                                         TCACourseCode = SC.STC_COURSE_NAME.ToString(),
                                         TCAForCourseTransfer = "",
                                         TCAAcadmicYear = "",
                                         TCTBYCourse = "",
                                         TCTForPLAR = SC.STC_CRED.ToString(),
                                     }
                                    )
                                      .Union(

                                          from SC in ctx.STUDENT_ACAD_CRED
                                          join TER in ctx.TERMS on SC.STC_TERM equals TER.TERMS_ID into TERS
                                          from TER in TERS.DefaultIfEmpty()
                                          join SELS in ctx.STUDENT_EQUIV_EVALS_LS on SC.STUDENT_ACAD_CRED_ID equals SELS.STE_STUDENT_ACAD_CRED into SELSC
                                          from SELS in SELSC.Where(x => x.POS == 1).DefaultIfEmpty()
                                          join ET in ctx.EXTERNAL_TRANSCRIPTS on SELS.STE_EXTERNAL_TRANSCRIPTS equals ET.EXTERNAL_TRANSCRIPTS_ID into ETS
                                          from ET in ETS.Where(x => x.EXTL_STUDENT_EQUIV_EVAL != null).DefaultIfEmpty()
                                          join NAME in ctx.PERSON on SC.STC_PERSON_ID equals NAME.ID into NAMES
                                          from NAME in NAMES.DefaultIfEmpty()
                                          join ASN in ctx.PERSON_ALT on NAME.ID equals ASN.ID into ASNS
                                          from ASN in ASNS.Where(x => x.PERSON_ALT_ID_TYPES.Equals("ALTA")).DefaultIfEmpty()
                                          join NAME2 in ctx.PERSON on ET.EXTL_INSTITUTION equals NAME2.ID into NAME2S
                                          from NAME2 in NAME2S.DefaultIfEmpty()
                                          join AD in ctx.ADDRESS on NAME2.PREFERRED_ADDRESS equals AD.ADDRESS_ID into ADS
                                          from AD in ADS.DefaultIfEmpty()
                                          join AC in ctx.ACAD_PROGRAMS on SELS.STE_ACAD_PROGRAMS equals AC.ACAD_PROGRAMS_ID into ACS
                                          from AC in ACS.DefaultIfEmpty()
                                          join ACLS in ctx.ACAD_PROGRAMS_LS on AC.ACAD_PROGRAMS_ID equals ACLS.ACAD_PROGRAMS_ID into ACLSS
                                          from ACLS in ACLSS.Where(x => x.POS == 1).DefaultIfEmpty()
                                          join ST in ctx.STC_STATUSES on SELS.STE_STUDENT_ACAD_CRED equals ST.STUDENT_ACAD_CRED_ID into STS
                                          from ST in STS.Where(x => x.STC_STATUS == "TR" && x.POS == 1 && x.STC_STATUS_DATE.Value.Year > 2019).DefaultIfEmpty()
                                          join STPR in ctx.STPR_DATES on SC.STC_PERSON_ID equals STPR.STUDENT_PROGRAMS_ID.Substring(0, 7) into STPRS
                                          from STPR in STPRS.Where(x => ST.STC_STATUS_DATE >= x.STPR_START_DATE &&
                                                      (x.STPR_END_DATE == null || ST.STC_STATUS_DATE <= x.STPR_END_DATE)).DefaultIfEmpty()

                                          where
                                            (
                                            !SC.STC_COURSE_NAME.Contains("ICP")
                                            && SC.STC_VERIFIED_GRADE == "270"
                                            && years.Contains((ST.STC_STATUS_DATE.Value.Month >= 1 && ST.STC_STATUS_DATE.Value.Month <= 6 ? ST.STC_STATUS_DATE.Value.Year - 1 : ST.STC_STATUS_DATE.Value.Year))
                                           )
                                          select new TransferCreditSearchResultsFilter()
                                          {
                                              sNumber = SC.STC_PERSON_ID,
                                              ASN = ASN.PERSON_ALT_IDS,
                                              BirthDate = NAME.BIRTH_DATE,
                                              Provider = "Lethbridge College",
                                              AcademicYear = (ST.STC_STATUS_DATE.Value.Month >= 1 && ST.STC_STATUS_DATE.Value.Month <= 6 ? (ST.STC_STATUS_DATE.Value.Year - 1).ToString() : ST.STC_STATUS_DATE.Value.Year.ToString()),
                                              StartTermDate = TER.TERM_START_DATE,
                                              ProgramID = AC.ACPG_CIP.Replace(".", ""),
                                              SpecializationID = ACLS.ACPG_LOCAL_GOVT_CODES,
                                              FromInstitution = ET.EXTL_INSTITUTION,
                                              FromInstitutionLoc =
                                                    (
                                                        AD.STATE == "AB" ? "Alberta" :
                                                        AD.STATE != "AB" ? "Outside of Alberta" :
                                                        AD.STATE == null ? "Unkonwn" : "Outside of Alberta"
                                                    ).ToString(),
                                              FromInstitutionCourseCode = ET.EXTL_COURSE,
                                              FromInstitutionAcademicYearCourseTaken = ET.EXTL_START_DATE.Value.Year.ToString(),
                                              TCACourseCode = SC.STC_COURSE_NAME,
                                              TCAForCourseTransfer = SC.STC_CRED == null ? "" : SC.STC_CRED.ToString(),
                                              TCAAcadmicYear = ET.EXTERNAL_TRANSCRIPTS_ADDDATE.Value.Year.ToString(),
                                              TCTBYCourse = SC.STC_CRED.ToString(),
                                              TCTForPLAR = "",
                                          }

                                        )
                                      .Distinct().ToList();

                // Filter by ASN
                if (!string.IsNullOrWhiteSpace(_TransferCreditViewObj.ReportSearchFilter.ASN))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ASN.Trim().Contains(_TransferCreditViewObj.ReportSearchFilter.ASN)).ToList();
                }

                // Filter by BirthDate
                if (_TransferCreditViewObj.ReportSearchFilter.BirthDate != null)
                {
                    _ReportMessages = _ReportMessages.Where(a => DbFunctions.TruncateTime(a.BirthDate) == DbFunctions.TruncateTime(_TransferCreditViewObj.ReportSearchFilter.BirthDate)).ToList();
                }

                // Filter by Provider
                if (!string.IsNullOrWhiteSpace(_TransferCreditViewObj.ReportSearchFilter.Provider))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.Provider.Trim().Contains(_TransferCreditViewObj.ReportSearchFilter.Provider.Trim())).ToList();
                }

                // Filter by From Academic Year
                if (!string.IsNullOrWhiteSpace(_TransferCreditViewObj.ReportSearchFilter.AcademicYear))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.AcademicYear.Trim().Contains(_TransferCreditViewObj.ReportSearchFilter.AcademicYear.Trim())).ToList();
                }

                // Filter by SpecializationID
                if (!string.IsNullOrWhiteSpace(_TransferCreditViewObj.ReportSearchFilter.SpecializationID))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.SpecializationID == _TransferCreditViewObj.ReportSearchFilter.SpecializationID).ToList();
                }

                // Filter by FromInstitution
                if (!string.IsNullOrWhiteSpace(_TransferCreditViewObj.ReportSearchFilter.FromInstitution))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.FromInstitution.Trim().Contains(_TransferCreditViewObj.ReportSearchFilter.FromInstitution.Trim())).ToList();
                }

                // Filter by FromInstitutionLoc
                if (!string.IsNullOrWhiteSpace(_TransferCreditViewObj.ReportSearchFilter.FromInstitutionLoc))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.FromInstitutionLoc.Trim().Contains(_TransferCreditViewObj.ReportSearchFilter.FromInstitutionLoc.Trim())).ToList();
                }

                // Filter by FromInstitutionCourseCode
                if (!string.IsNullOrWhiteSpace(_TransferCreditViewObj.ReportSearchFilter.FromInstitutionCourseCode))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.FromInstitutionCourseCode.Trim().Contains(_TransferCreditViewObj.ReportSearchFilter.FromInstitutionCourseCode.Trim())).ToList();
                }

                // Filter by FromInstitutionAcademicYearCourseTaken
                if (!string.IsNullOrWhiteSpace(_TransferCreditViewObj.ReportSearchFilter.FromInstitutionAcademicYearCourseTaken))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.FromInstitutionAcademicYearCourseTaken.Trim().Contains(_TransferCreditViewObj.ReportSearchFilter.FromInstitutionAcademicYearCourseTaken.Trim())).ToList();
                }

                // Filter by TCACourseCode
                if (!string.IsNullOrWhiteSpace(_TransferCreditViewObj.ReportSearchFilter.TCACourseCode))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.TCACourseCode.Trim().Contains(_TransferCreditViewObj.ReportSearchFilter.TCACourseCode.Trim())).ToList();
                }
                //Filter by TCAForCourseTransfer
                if (!string.IsNullOrWhiteSpace(_TransferCreditViewObj.ReportSearchFilter.TCAForCourseTransfer))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.TCAForCourseTransfer.Trim().Contains(_TransferCreditViewObj.ReportSearchFilter.TCAForCourseTransfer.Trim())).ToList();
                }
                //Filter by TCAAcadmicYear
                if (!string.IsNullOrWhiteSpace(_TransferCreditViewObj.ReportSearchFilter.TCAAcadmicYear))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.TCAAcadmicYear.Trim().Contains(_TransferCreditViewObj.ReportSearchFilter.TCAAcadmicYear.Trim())).ToList();
                }
                //Filter by TCTBYCourse
                if (!string.IsNullOrWhiteSpace(_TransferCreditViewObj.ReportSearchFilter.TCTBYCourse))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.TCTBYCourse.Trim().Contains(_TransferCreditViewObj.ReportSearchFilter.TCACourseCode.Trim())).ToList();
                }
                //Filter by TCTForPLAR
                if (!string.IsNullOrWhiteSpace(_TransferCreditViewObj.ReportSearchFilter.TCTForPLAR))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.TCTForPLAR.Trim().Contains(_TransferCreditViewObj.ReportSearchFilter.TCACourseCode.Trim())).ToList();
                }

                // Load ProgramID
                _ReportMessages.ForEach(x => x.ProgramID = x.ProgramID == "REPLACE PROGRAM ID" ? ReturnCIP(x.sNumber, x.StartTermDate) : x.ProgramID);

                // Filter by From Program ID
                if (!string.IsNullOrWhiteSpace(_TransferCreditViewObj.ReportSearchFilter.ProgramID))
                {
                    _ReportMessages = _ReportMessages.Where(a => a.ProgramID.Trim().Contains(_TransferCreditViewObj.ReportSearchFilter.ProgramID)).ToList();
                }

                _ListTransferCreditSearchResultsFilter = _ReportMessages.ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "TransferCreditQuery", "Error", ex.ToString());
            }

            return _ListTransferCreditSearchResultsFilter;
        }

        public bool LoadTransferCredits(TransferCreditViewObj _TransferCreditViewObj)
        {
            bool success = false;
            try
            {
                var _ReportMessages = TransferCreditQuery(_TransferCreditViewObj);

                _TransferCreditViewObj.Pagination.RecCount = _ReportMessages.Count();
                _TransferCreditViewObj.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    _TransferCreditViewObj.ReportSearchResultsFilter = _ReportMessages.OrderBy(x => x.ASN).ToList().Skip((_TransferCreditViewObj.Pagination.PageIndex - 1) * _TransferCreditViewObj.Pagination.PageSize).Take(_TransferCreditViewObj.Pagination.PageSize).ToList();
                    _TransferCreditViewObj.Pagination.PageCount = (_ReportMessages.Count() + _TransferCreditViewObj.Pagination.PageSize - 1) / _TransferCreditViewObj.Pagination.PageSize;
                }

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "LoadTransferCredits", "Error", ex.ToString());
            }

            return success;
        }

        public string ReturnCIP(string sNumber, DateTime? TermDate)
        {
            string result = string.Empty;
            try
            {
                var query = ctx.ACAD_PROGRAMS
                            .Where(a => a.ACPG_ACAD_LEVEL != "NCR" &&
                                        ctx.STPR_DATES
                                        .Where(d => d.STUDENT_PROGRAMS_ID.Substring(0, 7) == sNumber
                                            && TermDate >= d.STPR_START_DATE
                                            && ((d.STPR_END_DATE == null) || TermDate <= d.STPR_END_DATE)
                                        )
                                        .Select(s => s.STUDENT_PROGRAMS_ID.Substring(8, 13))
                                        .Contains(a.ACAD_PROGRAMS_ID)
                                    )
                            .Select(a => a.ACPG_CIP.Replace(".", ""));

                result = query.ToList().Aggregate(
                           "DELETE", // start with empty string to handle empty list case.
                           (current, next) => current + ", " + next).Replace("DELETE, ", "");

            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "ReturnCIP", "Error", ex.ToString());
            }

            return result;
        }

        public List<TransferCreditSearchResultsFilter> GetTransferCreditsReportByIds(string[] Id, bool allSelected = false, string filterFields = null)
        {
            List<TransferCreditSearchResultsFilter> _TransferCredit = new List<TransferCreditSearchResultsFilter>();
            try
            {
                if (Id != null && Id.Count() > 0)
                {
                    // Deserialize filter fields
                    TransferCreditViewObj _TransferCreditViewObj = new TransferCreditViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _TransferCreditViewObj.ReportSearchFilter = JsonConvert.DeserializeObject<TransferCreditSearchFilter>(filterFields);
                    };

                    var query = TransferCreditQuery(_TransferCreditViewObj);

                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => Id.Contains((a.ASN ?? "") + ";" + (a.TCACourseCode ?? ""))).ToList();
                    }

                    _TransferCredit = query.OrderBy(o => o.ASN).ToList();
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetTransferCreditsReportByIds", "Error", ex.ToString());
            }
            return _TransferCredit;
        }

        public TCA GetXMLTransferCreditsReportByIds(string[] Id, bool allSelected = false, string filterFields = null)
        {
            List<TransferCreditSearchResultsFilter> _TransferCredit = new List<TransferCreditSearchResultsFilter>();
            List<TCALearner> _TCALearner = new List<TCALearner>();
            TCA _TCA = new TCA();
            try
            {
                if (Id != null && Id.Count() > 0)
                {
                    // Deserialize filter fields
                    TransferCreditViewObj _TransferCreditViewObj = new TransferCreditViewObj();
                    if (filterFields != null && !string.IsNullOrWhiteSpace(filterFields.Replace('[', ' ').Replace(']', ' ').Trim()))
                    {
                        _TransferCreditViewObj.ReportSearchFilter = JsonConvert.DeserializeObject<TransferCreditSearchFilter>(filterFields);
                    };

                    var query = TransferCreditQuery(_TransferCreditViewObj);

                    if (!allSelected)
                    {
                        // filter by Selected Ids
                        query = query.Where(a => Id.Contains((a.ASN ?? "") + ";" + (a.TCACourseCode ?? ""))).ToList();
                    }

                    _TransferCredit = query.OrderBy(o => o.ASN).ToList();

                    foreach (var item in _TransferCredit)
                    {
                        TCALearner tcaLearner = new TCALearner()
                        {
                            TransferCredit = new TransferCreditObj()
                            {
                                FromInstitution = "Prior Learning",//FromInstitution
                                FILocation = "",//FromInstitutionLoc
                                FICourseCode = "Prior Learning",//FromInstitutionCourseCode
                                FIAcademicYearCourseTaken = "",//FromInstitutionAcademicYearCourseTaken
                                TCACourseCode = item.TCACourseCode,//TCACourseCode
                                TCAForCourseTransfer = "",//TCAForCourseTransfer
                                TCAAcademicYear = "",//TCAAcadmicYear
                                TCTByCourse = "",//TCTBYCourse
                                TCTForPLAR = item.TCTForPLAR,//TCTForPLAR
                                Notes = ""
                            },

                            SNumber = item.sNumber,
                            StartTermDate = item.StartTermDate,
                            ASN = item.ASN,
                            Birthdate = item.BirthDate.ToString(),
                            Provider = item.Provider,
                            AcademicYear = item.AcademicYear,
                            ProgramID = item.ProgramID,
                            SpecializationID = item.SpecializationID
                        };

                        _TCALearner.Add(tcaLearner);
                    }

                    _TCA.TCALearner = _TCALearner;
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetXMLTransferCreditsReportByIds", "Error", ex.ToString());
            }
            return _TCA;
        }

        #endregion

        #region Batch Transcript

        public List<UnsolicitedBatchObj> UnsolicitedBatchTranscriptQuery(UnsolicitedBatchListViewObj _UnsolicitedBatchListViewObj)
        {
            List<UnsolicitedBatchObj> _ListUnsolicitedBatchObj = null;
            try
            {
                // Term
                string _Term = string.Empty;
                if (!string.IsNullOrWhiteSpace(_UnsolicitedBatchListViewObj.SearchFilter.Term))
                {
                    _Term = _UnsolicitedBatchListViewObj.SearchFilter.Term;
                }
                else
                {
                    _Term = Functions.GetLcCurrentTerm();
                }

                // Department
                List<string> _Departments = new List<string>() { };
                if (!string.IsNullOrWhiteSpace(_UnsolicitedBatchListViewObj.SearchFilter.DeptCode))
                {
                    _Departments.Add(_UnsolicitedBatchListViewObj.SearchFilter.DeptCode);
                }
                else
                {
                    _Departments.Add("NSG");
                }

                // Status
                List<string> _Statuses = new List<string>() { };
                _Statuses.Add("A");
                _Statuses.Add("N");
                _Statuses.Add("W");

                var results = (from AC in ctx.STUDENT_ACAD_CRED
                               join ACLS in ctx.STUDENT_ACAD_CRED_LS on AC.STUDENT_ACAD_CRED_ID equals ACLS.STUDENT_ACAD_CRED_ID
                               join STAT in ctx.STC_STATUSES on AC.STUDENT_ACAD_CRED_ID equals STAT.STUDENT_ACAD_CRED_ID
                               join ST in ctx.STUDENT_TERMS on (AC.STC_PERSON_ID + "*" + _Term + "*PS") equals ST.STUDENT_TERMS_ID
                               join TER in ctx.TERMS on AC.STC_TERM equals TER.TERMS_ID into TERS
                               from TER in TERS.DefaultIfEmpty()
                               join STPR in ctx.STPR_DATES on AC.STC_PERSON_ID equals STPR.STUDENT_PROGRAMS_ID.Substring(0, 7) into STPRS
                               from STPR in STPRS.Where(x => (TER.TERM_START_DATE >= x.STPR_START_DATE) &&
                                                               ((x.STPR_END_DATE == null) || TER.TERM_START_DATE <= x.STPR_END_DATE) &&
                                                               (!x.STUDENT_PROGRAMS_ID.Contains("NCR"))
                                                               ).DefaultIfEmpty()
                               join ACP in ctx.ACAD_PROGRAMS on STPR.STUDENT_PROGRAMS_ID.Substring(8, 13) equals ACP.ACAD_PROGRAMS_ID into ACPS
                               from ACP in ACPS.DefaultIfEmpty()
                               join SP in ctx.STUDENT_PROGRAMS on STPR.STUDENT_PROGRAMS_ID equals SP.STUDENT_PROGRAMS_ID into SPS
                               from SP in SPS.Where(x => x.STPR_DEPT == ACLS.STC_DEPTS).DefaultIfEmpty()
                               join NAME in ctx.PERSON on AC.STC_PERSON_ID equals NAME.ID into NAMES
                               from NAME in NAMES.DefaultIfEmpty()
                               join ASN in ctx.PERSON_ALT on NAME.ID equals ASN.ID into ASNS
                               from ASN in ASNS.Where(x => x.PERSON_ALT_ID_TYPES == "ALTA").DefaultIfEmpty()
                               join DPT in ctx.DEPTS on ACLS.STC_DEPTS equals DPT.DEPTS_ID into DPTS
                               from DPT in DPTS.Where(x => x.DEPTS_ACTIVE_FLAG == "A").DefaultIfEmpty()

                               where AC.STC_TERM == _Term
                                  && ACLS.STC_DEPTS != null
                                  && _Departments.Contains(ACLS.STC_DEPTS)
                                  && STAT.POS == 1
                                  && _Statuses.Contains(STAT.STC_STATUS)

                               select new UnsolicitedBatchObj()
                               {
                                   sNumber = AC.STC_PERSON_ID,
                                   FirstName = NAME.FIRST_NAME,
                                   LastName = NAME.LAST_NAME,
                                   FullName = NAME.FIRST_NAME + " " + NAME.LAST_NAME,
                                   Asn = ASN.PERSON_ALT_IDS,
                                   ProgramCode = ACP.ACAD_PROGRAMS_ID,
                                   ProgramTitle = SP.STPR_TITLE ?? ACP.ACPG_TITLE,
                                   DeptCode = DPT.DEPTS_ID,
                                   DeptTitle = DPT.DEPTS_DESC,
                                   Term = AC.STC_TERM,
                               }
                            ).AsQueryable();

                if (!string.IsNullOrWhiteSpace(_UnsolicitedBatchListViewObj.SearchFilter.StudentRecord.Snumber))
                {
                    results = results.Where(x => x.sNumber.Trim().ToUpper().Contains(_UnsolicitedBatchListViewObj.SearchFilter.StudentRecord.Snumber.Trim().ToUpper()));
                }

                if (!string.IsNullOrWhiteSpace(_UnsolicitedBatchListViewObj.SearchFilter.StudentRecord.ASN))
                {
                    results = results.Where(x => x.Asn.Trim().ToUpper().Contains(_UnsolicitedBatchListViewObj.SearchFilter.StudentRecord.ASN.Trim().ToUpper()));
                }

                if (!string.IsNullOrWhiteSpace(_UnsolicitedBatchListViewObj.SearchFilter.ProgramCode))
                {
                    results = results.Where(x => x.ProgramCode.Trim().ToUpper().Contains(_UnsolicitedBatchListViewObj.SearchFilter.ProgramCode.Trim().ToUpper()));
                }

                if (_UnsolicitedBatchListViewObj.SearchFilter.StudentRecord != null && !string.IsNullOrWhiteSpace(_UnsolicitedBatchListViewObj.SearchFilter.StudentRecord.FullName))
                {
                    string[] _StudentNames = Functions.SeparateNames(_UnsolicitedBatchListViewObj.SearchFilter.StudentRecord.FullName);
                    string firstName = string.Empty; string middleName = string.Empty; string lastName = string.Empty;

                    int cnt = 0;
                    if (!string.IsNullOrWhiteSpace(_StudentNames[0])) { firstName = _StudentNames[0].Trim().ToUpper(); cnt++; }
                    if (!string.IsNullOrWhiteSpace(_StudentNames[1])) { middleName = _StudentNames[1].Trim().ToUpper(); cnt++; }
                    if (!string.IsNullOrWhiteSpace(_StudentNames[2])) { lastName = _StudentNames[2].Trim().ToUpper(); cnt++; }

                    if (!string.IsNullOrWhiteSpace(firstName))
                    {
                        if (cnt == 1)
                        {
                            results = results.Where(x => x.LastName.Trim().ToUpper().Contains(firstName) || x.FirstName.Trim().ToUpper().Contains(firstName));
                        }
                        if (!string.IsNullOrWhiteSpace(lastName))
                        {
                            if (cnt == 2)
                            {
                                results = results.Where(x => x.FirstName.Trim().ToUpper().Contains(firstName) && x.LastName.Trim().ToUpper().Contains(lastName));
                            }
                            if (!string.IsNullOrWhiteSpace(middleName))
                            {
                                if (cnt == 3)
                                {
                                    results = results.Where(x => x.LastName.Trim().ToUpper().Contains(lastName) && x.FirstName.Trim().ToUpper().Contains(firstName));
                                }
                            }
                        }
                    }
                }

                _ListUnsolicitedBatchObj = results.Distinct().ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "UnsolicitedBatchTranscriptQuery", "Error", ex.ToString());
            }
            return _ListUnsolicitedBatchObj;
        }

        public bool UnsolicitedBatchTranscriptReports(UnsolicitedBatchListViewObj _UnsolicitedBatchListViewObj)
        {
            bool success = false;
            try
            {
                var _ReportMessages = UnsolicitedBatchTranscriptQuery(_UnsolicitedBatchListViewObj);

                _UnsolicitedBatchListViewObj.Pagination.RecCount = _ReportMessages.Count();
                _UnsolicitedBatchListViewObj.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    _UnsolicitedBatchListViewObj.Students = _ReportMessages.OrderBy(x => x.FullName).ToList().Skip((_UnsolicitedBatchListViewObj.Pagination.PageIndex - 1) * _UnsolicitedBatchListViewObj.Pagination.PageSize).Take(_UnsolicitedBatchListViewObj.Pagination.PageSize).ToList();
                    _UnsolicitedBatchListViewObj.Pagination.PageCount = (_ReportMessages.Count() + _UnsolicitedBatchListViewObj.Pagination.PageSize - 1) / _UnsolicitedBatchListViewObj.Pagination.PageSize;
                }

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "UnsolicitedBatchTranscriptReports", "Error", ex.ToString());
            }

            return success;
        }

        public List<TRRQRequestObj> MyCredsRequestQuery(TRRQRequestListViewObj batchList)
        {
            List<TRRQRequestObj> _ListTRRQRequestObj = null;
            try
            {
                List<string> RestrictionList = GetRestrictionList();

                var results = (from STRL in ctx.STUDENT_REQUEST_LOGS
                               join LS in ctx.STUDENT_REQUEST_LOGS_LS on STRL.STUDENT_REQUEST_LOGS_ID equals LS.STUDENT_REQUEST_LOGS_ID
                               join NAME in ctx.PERSON on STRL.STRL_STUDENT equals NAME.ID into NAMES
                               from NAME in NAMES.DefaultIfEmpty()
                               where
                                  (STRL.STRL_DATE >= batchList.SearchFilter.FromRequestDate
                                  && STRL.STRL_DATE <= batchList.SearchFilter.ToRequestDate
                                  && STRL.STRL_TYPE == "T"
                                  && LS.POS == 1
                                  && LS.STRL_TRANSCRIPT_GROUPINGS != null
                                  && (STRL.STRL_COMMENTS == null || !STRL.STRL_COMMENTS.Contains("APAS")))

                               select new TRRQRequestObj()
                               {
                                   RequestID = STRL.STUDENT_REQUEST_LOGS_ID,
                                   sNumber = STRL.STRL_STUDENT,
                                   FirstName = NAME.FIRST_NAME,
                                   LastName = NAME.LAST_NAME,
                                   FullName = NAME.FIRST_NAME + " " + NAME.LAST_NAME,
                                   Email = STRL.STRL_RECIPIENT_NAME != null && !STRL.STRL_RECIPIENT_NAME.Trim().ToUpper().Contains(Structs.Email.LethbridgeCollegeEmailType.ToUpper()) ? 
                                            STRL.STRL_RECIPIENT_NAME : 
                                            ctx.PEOPLE_EMAIL.Where(y => y.PERSON_EMAIL_TYPES.Trim().ToUpper() == "OFF" && y.ID == STRL.STRL_STUDENT).Select(s => s.PERSON_EMAIL_ADDRESSES).FirstOrDefault(),
                                   HoldType = STRL.STRL_STU_REQUEST_LOG_HOLDS,
                                   RequestDate = STRL.STRL_DATE,
                                   DateProduced = STRL.STRL_PRINT_DATE,
                                   Operator = STRL.STUDENT_REQUEST_LOGS_ADDOPR.ToUpper().Contains("S") ? STRL.STUDENT_REQUEST_LOGS_ADDOPR.ToLower() : "Self-Service",
                                   Comments = STRL.STRL_COMMENTS,
                                   Restriction = ctx.STUDENT_RESTRICTIONS.Where(x => x.STR_STUDENT == STRL.STRL_STUDENT &&
                                                                                RestrictionList.Contains(x.STR_RESTRICTION) &&
                                                                                ((x.STR_END_DATE == null) || (x.STR_END_DATE != null && x.STR_END_DATE >= DateTime.Now)))
                                                                         .OrderByDescending(o => o.STR_START_DATE)
                                                                         .Select(s => s.STR_RESTRICTION)
                                                                         .FirstOrDefault(),
                                   AcadCoursesCount = ctx.STUDENT_ACAD_CRED.Where(x => x.STC_PERSON_ID == STRL.STRL_STUDENT && x.STC_ACAD_LEVEL == "PS").Count()
                               }
                               ).AsQueryable();

                if (!string.IsNullOrWhiteSpace(batchList.SearchFilter.RequestID))
                {
                    results = results.Where(x => x.RequestID.Trim().ToUpper().Contains(batchList.SearchFilter.RequestID.Trim().ToUpper()));
                }

                if (!string.IsNullOrWhiteSpace(batchList.SearchFilter.StudentRecord.Snumber))
                {
                    results = results.Where(x => x.sNumber.Trim().ToUpper().Contains(batchList.SearchFilter.StudentRecord.Snumber.Trim().ToUpper()));
                }

                if (!string.IsNullOrWhiteSpace(batchList.SearchFilter.StudentRecord.Email))
                {
                    results = results.Where(x => x.Email.Trim().ToUpper().Contains(batchList.SearchFilter.StudentRecord.Email.Trim().ToUpper()));
                }

                //if (!string.IsNullOrWhiteSpace(batchList.SearchFilter.TRRQRequestingInstitution))
                //{
                //    results = results.Where(x => x.InstitutionName.Trim().ToUpper().Contains(batchList.SearchFilter.TRRQRequestingInstitution.Trim().ToUpper()));
                //}

                if (!string.IsNullOrWhiteSpace(batchList.SearchFilter.HoldType))
                {
                    if (batchList.SearchFilter.HoldType == Structs.MyCredsHoldTypes.NoHold)
                    {
                        results = results.Where(x => x.HoldType == null || x.HoldType.Trim() == "");
                    }
                    else
                    {
                        results = results.Where(x => x.HoldType.Trim().ToUpper().Contains(batchList.SearchFilter.HoldType.Trim().ToUpper()));
                    }
                }

                if (batchList.SearchFilter.FromDateProduced != null)
                {
                    results = results.Where(x => DbFunctions.TruncateTime(x.DateProduced) >= DbFunctions.TruncateTime(batchList.SearchFilter.FromDateProduced));
                }

                if (batchList.SearchFilter.ToDateProduced != null)
                {
                    results = results.Where(x => DbFunctions.TruncateTime(x.DateProduced) <= DbFunctions.TruncateTime(batchList.SearchFilter.ToDateProduced));
                }

                // If date produced was not filter, bring only pending requests where data produced is null
                //if (batchList.SearchFilter.FromDateProduced == null && batchList.SearchFilter.ToDateProduced == null)
                //{
                //    results = results.Where(x => x.DateProduced == null);
                //}

                if (!string.IsNullOrWhiteSpace(batchList.SearchFilter.Operator))
                {
                    results = results.Where(x => x.Operator.Trim().ToUpper().Contains(batchList.SearchFilter.Operator.Trim().ToUpper()));
                }

                if (!string.IsNullOrWhiteSpace(batchList.SearchFilter.Comments))
                {
                    results = results.Where(x => x.Comments.Trim().ToUpper().Contains(batchList.SearchFilter.Comments.Trim().ToUpper()));
                }

                if (batchList.SearchFilter.StudentRecord != null && !string.IsNullOrWhiteSpace(batchList.SearchFilter.StudentRecord.FullName))
                {
                    string[] _StudentNames = Functions.SeparateNames(batchList.SearchFilter.StudentRecord.FullName);
                    string firstName = string.Empty; string middleName = string.Empty; string lastName = string.Empty;

                    int cnt = 0;
                    if (!string.IsNullOrWhiteSpace(_StudentNames[0])) { firstName = _StudentNames[0].Trim().ToUpper(); cnt++; }
                    if (!string.IsNullOrWhiteSpace(_StudentNames[1])) { middleName = _StudentNames[1].Trim().ToUpper(); cnt++; }
                    if (!string.IsNullOrWhiteSpace(_StudentNames[2])) { lastName = _StudentNames[2].Trim().ToUpper(); cnt++; }

                    if (!string.IsNullOrWhiteSpace(firstName))
                    {
                        if (cnt == 1)
                        {
                            results = results.Where(x => x.LastName.Trim().ToUpper().Contains(firstName) || x.FirstName.Trim().ToUpper().Contains(firstName));
                        }
                        if (!string.IsNullOrWhiteSpace(lastName))
                        {
                            if (cnt == 2)
                            {
                                results = results.Where(x => x.FirstName.Trim().ToUpper().Contains(firstName) && x.LastName.Trim().ToUpper().Contains(lastName));
                            }
                            if (!string.IsNullOrWhiteSpace(middleName))
                            {
                                if (cnt == 3)
                                {
                                    results = results.Where(x => x.LastName.Trim().ToUpper().Contains(lastName) && x.FirstName.Trim().ToUpper().Contains(firstName));
                                }
                            }
                        }
                    }
                }

                _ListTRRQRequestObj = results.ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "MyCredsRequestQuery", "Error", ex.ToString());
            }
            return _ListTRRQRequestObj;
        }

        public bool LoadTRRQRequests(TRRQRequestListViewObj batchList)
        {
            bool success = false;

            try
            {
                var _ReportMessages = MyCredsRequestQuery(batchList);

                batchList.Pagination.RecCount = _ReportMessages.Count();
                batchList.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    batchList.TranscriptRequests = _ReportMessages.OrderByDescending(x => x.DateProduced ?? DateTime.Now).ThenByDescending(x => x.RequestDate).ThenBy(x => x.FullName).ToList().Skip((batchList.Pagination.PageIndex - 1) * batchList.Pagination.PageSize).Take(batchList.Pagination.PageSize).ToList();
                    batchList.Pagination.PageCount = (_ReportMessages.Count() + batchList.Pagination.PageSize - 1) / batchList.Pagination.PageSize;
                }
                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "LoadTRRQRequests", "Error", ex.ToString());
            }
            return success;
        }

        public bool SaveTRRQRequestDateProduced(MyCredsTransactionObj myCredsTransaction)
        {
            bool success = false;
            List<STUDENT_REQUEST_LOGS> _RequestList = null;
            Models.Lcappsdb.TransactionRequestLog _TransactionRequestLog = null;

            try
            {
                // Find TRRQ request in Colleague
                if (myCredsTransaction.RequestIdList != null && myCredsTransaction.RequestIdList.Any())
                {
                    _RequestList = ctx.STUDENT_REQUEST_LOGS.Where(x => myCredsTransaction.RequestIdList.Contains(x.STUDENT_REQUEST_LOGS_ID)).OrderByDescending(o => o.STUDENT_REQUEST_LOGS_ADDDATE).ToList();
                }
                
                // If TRRQ request not found, create a new one
                if (_RequestList == null || !_RequestList.Any())
                {
                    if (string.IsNullOrWhiteSpace(myCredsTransaction.UUID))
                    {
                        myCredsTransaction.UUID = Guid.NewGuid().ToString();
                    }

                    // Get student address
                    ADDRESSES _StudentAddress = GetColleagueStudentAddress(myCredsTransaction.sNumber);
                    if (_StudentAddress != null)
                    {
                        myCredsTransaction.AddressLine = _StudentAddress.ADDRESS_LS.Where(x => x.POS == 1).Select(s => s.ADDRESS_LINES).FirstOrDefault();
                        myCredsTransaction.City = _StudentAddress.CITY;
                        myCredsTransaction.State = _StudentAddress.STATE;
                        myCredsTransaction.PostalCode = _StudentAddress.ZIP;
                        myCredsTransaction.Country = _StudentAddress.COUNTRY;
                    }

                    // Create a RequestLog in the Toolkit database
                    _TransactionRequestLog = lcapasLogic.CreateMyCredsRequestLog(myCredsTransaction);
                    if (_TransactionRequestLog != null)
                    {
                        // Save Request Log into Colleague
                        CollWebApi.MainDriver mainDriver = new CollWebApi.MainDriver();
                        success = mainDriver.SaveMyCredsRequestLog(_TransactionRequestLog, myCredsTransaction.RequestedDate, myCredsTransaction.ProducedDate);
                    }
                }

                // Save Date Produced and MyCreds Comments
                if (_RequestList != null && _RequestList.Any())
                {
                    foreach (var _Request in _RequestList)
                    {
                        _Request.STRL_PRINT_DATE = myCredsTransaction.ProducedDate ?? DateTime.Now;
                        _Request.STRL_COMMENTS = lcapasLogic.MyCredsRequestComments(_Request.STRL_COMMENTS, myCredsTransaction.BatchCode);
                        _Request.STRL_DELIVERY_METHOD = "E";  // EDI Delivery MEthod
                    }
                    ctx.SaveChanges();
                }

                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "SaveTRRQRequestDateProduced", "Error", ex.ToString());
            }
            return success;
        }

        public Dictionary<string, List<string>> GetMyCredsTRRQRequest(string requestId)
        {
            STUDENT_REQUEST_LOGS _Request = null;
            Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            List<string> requestDupList = new List<string>();

            try
            {
                // Find TRRQ request in Colleague
                if (string.IsNullOrWhiteSpace(requestId))
                {
                    _Request = ctx.STUDENT_REQUEST_LOGS.Where(x => x.STUDENT_REQUEST_LOGS_ID.Trim().ToUpper() == requestId.Trim().ToUpper())
                                                           .OrderByDescending(o => o.STUDENT_REQUEST_LOGS_ADDDATE).FirstOrDefault();
                }

                // If TRRQ request not found, create a new one
                if (_Request == null && !string.IsNullOrWhiteSpace(_Request.STUDENT_REQUEST_LOGS_ID) && !string.IsNullOrWhiteSpace(_Request.STRL_STUDENT))
                {
                    // New entry, create a new list
                    requestDupList = new List<string>();
                    requestDupList.Add(_Request.STUDENT_REQUEST_LOGS_ID);
                    results.Add(_Request.STRL_STUDENT, requestDupList);
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetMyCredsTRRQRequest", "Error", ex.ToString());
            }
            return results;
        }

        public List<StudentObj> MyCredsBulkSendStudentQuery(BulkSendListViewObj batchList)
        {
            List<StudentObj> _ListStudentsObj = null;
            try
            {
                List<string> RestrictionList = GetRestrictionList();

                var results = (from ACD in ctx.ACAD_CREDENTIALS
                               join ALS in ctx.ACAD_CREDENTIALS_LS on ACD.ACAD_CREDENTIALS_ID equals ALS.ACAD_CREDENTIALS_ID
                               join PRG in ctx.ACAD_PROGRAMS on ACD.ACAD_ACAD_PROGRAM equals PRG.ACAD_PROGRAMS_ID into PRGS
                               from PRG in PRGS.DefaultIfEmpty()
                               join NAME in ctx.PERSON on ACD.ACAD_PERSON_ID equals NAME.ID into NAMES
                               from NAME in NAMES.DefaultIfEmpty()
                               join FPER in ctx.FOREIGN_PERSON on NAME.ID equals FPER.FOREIGN_PERSON_ID into FPERS
                               from FPER in FPERS.DefaultIfEmpty()
                               where
                                  (ACD.ACAD_INSTITUTIONS_ID == Structs.Institution.LethbridgeCollegeID &&  // Lethbridge College
                                   ALS.ACAD_CCD_DATE != null)

                               select new StudentObj()
                               {
                                   AcadCredentialsID = ACD.ACAD_CREDENTIALS_ID,
                                   sNumber = ACD.ACAD_PERSON_ID,
                                   FirstName = NAME.FIRST_NAME,
                                   LastName = NAME.LAST_NAME,
                                   FullName = NAME.FIRST_NAME + " " + NAME.LAST_NAME,
                                   Email = ctx.PEOPLE_EMAIL.Where(y => y.PERSON_EMAIL_TYPES.Trim().ToUpper() == "OFF" && y.ID == ACD.ACAD_PERSON_ID).Select(s => s.PERSON_EMAIL_ADDRESSES).FirstOrDefault(),
                                   Ethnic = NAME.ETHNIC,
                                   AlienStatus = FPER.FPER_ALIEN_STATUS,
                                   ProgramCode = ACD.ACAD_ACAD_PROGRAM,
                                   ProgramDesc = PRG.ACPG_TITLE,
                                   Campus = ACD.ACAD_LOCATION,
                                   AcadCredAddDate = ACD.ACAD_CREDENTIALS_ADDDATE,
                                   AcadCCDDate = ALS.ACAD_CCD_DATE,
                                   CCDType = ALS.ACAD_CCD,
                                   AcadHonors = ALS.ACAD_HONORS,
                                   AcadGPA = ACD.ACAD_GPA,
                                   Restriction = ctx.STUDENT_RESTRICTIONS.Where(x => x.STR_STUDENT == ACD.ACAD_PERSON_ID &&
                                                                                RestrictionList.Contains(x.STR_RESTRICTION) &&
                                                                                ((x.STR_END_DATE == null) || (x.STR_END_DATE != null && x.STR_END_DATE >= DateTime.Now)))
                                                                         .OrderByDescending(o => o.STR_START_DATE)
                                                                         .Select(s => s.STR_RESTRICTION)
                                                                         .FirstOrDefault(),
                                   AcadCoursesCount = ctx.STUDENT_ACAD_CRED.Where(x => x.STC_PERSON_ID == ACD.ACAD_PERSON_ID && x.STC_ACAD_LEVEL == "PS").Count(),
                               }
                               ).AsQueryable();

                if (!string.IsNullOrWhiteSpace(batchList.SearchFilter.AcadCredentialsID))
                {
                    results = results.Where(x => x.AcadCredentialsID.Trim().ToUpper().Contains(batchList.SearchFilter.AcadCredentialsID.Trim().ToUpper()));
                }

                if (!string.IsNullOrWhiteSpace(batchList.SearchFilter.StudentRecord.Snumber))
                {
                    results = results.Where(x => x.sNumber.Trim().ToUpper().Contains(batchList.SearchFilter.StudentRecord.Snumber.Trim().ToUpper()));
                }

                if (!string.IsNullOrWhiteSpace(batchList.SearchFilter.StudentRecord.Email))
                {
                    results = results.Where(x => x.Email.Trim().ToUpper().Contains(batchList.SearchFilter.StudentRecord.Email.Trim().ToUpper()));
                }

                if (batchList.SearchFilter.Ethnic != null && batchList.SearchFilter.Ethnic.Any())
                {
                    results = results.Where(a => batchList.SearchFilter.Ethnic.Contains(a.Ethnic.Trim().ToUpper()));
                }

                if (batchList.SearchFilter.AlienStatus != null && batchList.SearchFilter.AlienStatus.Any())
                {
                    results = results.Where(a => batchList.SearchFilter.AlienStatus.Contains(a.AlienStatus.Trim().ToUpper()));
                }

                if (batchList.SearchFilter.ProgramCode != null && batchList.SearchFilter.ProgramCode.Any())
                {
                    results = results.Where(a => batchList.SearchFilter.ProgramCode.Contains(a.ProgramCode.Trim().ToUpper()));
                }

                if (!string.IsNullOrWhiteSpace(batchList.SearchFilter.ProgramDesc))
                {
                    results = results.Where(x => x.ProgramDesc.Trim().ToUpper().Contains(batchList.SearchFilter.ProgramDesc.Trim().ToUpper()));
                }

                if (batchList.SearchFilter.Campus != null && batchList.SearchFilter.Campus.Any())
                {
                    results = results.Where(a => batchList.SearchFilter.Campus.Contains(a.Campus.Trim().ToUpper()));
                }

                if (batchList.SearchFilter.CCDType != null && batchList.SearchFilter.CCDType.Any())
                {
                    results = results.Where(a => batchList.SearchFilter.CCDType.Contains(a.CCDType.Trim().ToUpper()));
                }

                if (batchList.SearchFilter.AcadHonors != null && batchList.SearchFilter.AcadHonors.Any())
                {
                    results = results.Where(a => batchList.SearchFilter.AcadHonors.Contains(a.AcadHonors.Trim().ToUpper()));
                }

                if (!string.IsNullOrWhiteSpace(batchList.SearchFilter.AcadGPA))
                {
                    results = results.Where(x => (x.AcadGPA != null ? x.AcadGPA.ToString() : " ").Trim().ToUpper().Contains(batchList.SearchFilter.AcadGPA.Trim().ToUpper()));
                }

                if (batchList.SearchFilter.FromAcadCCDDate != null)
                {
                    results = results.Where(x => DbFunctions.TruncateTime(x.AcadCCDDate) >= DbFunctions.TruncateTime(batchList.SearchFilter.FromAcadCCDDate));
                }

                if (batchList.SearchFilter.ToAcadCCDDate != null)
                {
                    results = results.Where(x => DbFunctions.TruncateTime(x.AcadCCDDate) <= DbFunctions.TruncateTime(batchList.SearchFilter.ToAcadCCDDate));
                }

                if (batchList.SearchFilter.FromAcadCredAddDate != null)
                {
                    results = results.Where(x => DbFunctions.TruncateTime(x.AcadCredAddDate) >= DbFunctions.TruncateTime(batchList.SearchFilter.FromAcadCredAddDate));
                }

                if (batchList.SearchFilter.ToAcadCredAddDate != null)
                {
                    results = results.Where(x => DbFunctions.TruncateTime(x.AcadCredAddDate) <= DbFunctions.TruncateTime(batchList.SearchFilter.ToAcadCredAddDate));
                }

                if (batchList.SearchFilter.StudentRecord != null && !string.IsNullOrWhiteSpace(batchList.SearchFilter.StudentRecord.FullName))
                {
                    string[] _StudentNames = Functions.SeparateNames(batchList.SearchFilter.StudentRecord.FullName);
                    string firstName = string.Empty; string middleName = string.Empty; string lastName = string.Empty;

                    int cnt = 0;
                    if (!string.IsNullOrWhiteSpace(_StudentNames[0])) { firstName = _StudentNames[0].Trim().ToUpper(); cnt++; }
                    if (!string.IsNullOrWhiteSpace(_StudentNames[1])) { middleName = _StudentNames[1].Trim().ToUpper(); cnt++; }
                    if (!string.IsNullOrWhiteSpace(_StudentNames[2])) { lastName = _StudentNames[2].Trim().ToUpper(); cnt++; }

                    if (!string.IsNullOrWhiteSpace(firstName))
                    {
                        if (cnt == 1)
                        {
                            results = results.Where(x => x.LastName.Trim().ToUpper().Contains(firstName) || x.FirstName.Trim().ToUpper().Contains(firstName));
                        }
                        if (!string.IsNullOrWhiteSpace(lastName))
                        {
                            if (cnt == 2)
                            {
                                results = results.Where(x => x.FirstName.Trim().ToUpper().Contains(firstName) && x.LastName.Trim().ToUpper().Contains(lastName));
                            }
                            if (!string.IsNullOrWhiteSpace(middleName))
                            {
                                if (cnt == 3)
                                {
                                    results = results.Where(x => x.LastName.Trim().ToUpper().Contains(lastName) && x.FirstName.Trim().ToUpper().Contains(firstName));
                                }
                            }
                        }
                    }
                }

                _ListStudentsObj = results.ToList();
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "MyCredsBulkSendStudentQuery", "Error", ex.ToString());
            }
            return _ListStudentsObj;
        }

        public bool LoadBulkSendStudents(BulkSendListViewObj batchList)
        {
            bool success = false;

            try
            {
                var _ReportMessages = MyCredsBulkSendStudentQuery(batchList);

                batchList.Pagination.RecCount = _ReportMessages.Count();
                batchList.Pagination.PageCount = 1;
                if (_ReportMessages.Count() > 0)
                {
                    batchList.Students = _ReportMessages.OrderBy(x => x.FullName).ThenBy(x => x.AcadCCDDate).ToList().Skip((batchList.Pagination.PageIndex - 1) * batchList.Pagination.PageSize).Take(batchList.Pagination.PageSize).ToList();
                    batchList.Pagination.PageCount = (_ReportMessages.Count() + batchList.Pagination.PageSize - 1) / batchList.Pagination.PageSize;
                }
                success = true;
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "LoadBulkSendStudents", "Error", ex.ToString());
            }
            return success;
        }


        public string GetStudentPersonalEmail(string sNumber)
        {
            string result = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(sNumber))
                {
                    var _Email = ctx.PEOPLE_EMAIL.Where(y => y.PERSON_EMAIL_TYPES.Trim().ToUpper() == "OFF" && y.ID.Trim().ToUpper() == sNumber.Trim().ToUpper()).Select(s => s.PERSON_EMAIL_ADDRESSES).FirstOrDefault();

                    if (_Email != null && !string.IsNullOrWhiteSpace(_Email))
                    {
                        result = _Email;
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetStudentPersonalEmail", "Error", ex.ToString());
            }
            return result;
        }

        public string GetStudentRequestLogEmail(string sNumber, List<string> requestIdList = null)
        {
            string result = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(sNumber))
                {
                    // Filter by sNumber and existing email in the Recipient_Name field
                    var query = ctx.STUDENT_REQUEST_LOGS.Where(y => y.STRL_STUDENT.Trim().ToUpper() == sNumber.Trim().ToUpper() && !string.IsNullOrEmpty(y.STRL_RECIPIENT_NAME.Trim()));

                    // Filter by Request ID, if provided
                    if (requestIdList != null && requestIdList.Any())
                    {
                        query = query.Where(x => requestIdList.Contains(x.STUDENT_REQUEST_LOGS_ID));
                    }

                    var _Email = query.OrderByDescending(o => o.STRL_DATE).Select(s => s.STRL_RECIPIENT_NAME).FirstOrDefault();

                    if (_Email != null && !string.IsNullOrWhiteSpace(_Email))
                    {
                        result = _Email;
                    }
                }
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasCore, Structs.Class.ColleagueLogic, "GetStudentRequestLogEmail", "Error", ex.ToString());
            }
            return result;
        }

        #endregion

    }
}

