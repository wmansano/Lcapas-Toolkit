using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Ellucian.Data.Colleague;
using Ellucian.Dmi.Runtime;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Lcapas.CollWebApi.DataContracts
{

    #region Data Contracts

    [GeneratedCodeAttribute("Colleague Data Contract Generator", "1.1")]
    [DataContract]
    [ColleagueDataContract(ColleagueId = "X.L16.INSERT.STU.REQ", GeneratedDateTime = "", User = "")]
    [SctrqDataContract(Application = "ST", DataContractVersion = 1)]
    public class ContractRequest_InsertReqLog
    {
        /// <summary>
        /// Version
        /// </summary>
        [DataMember]
        public int _AppServerVersion { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.STUDENT.ID", InBoundData = true)]
        public string StudentId { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.REQUEST.TYPE", InBoundData = true)]
        public string RequestType { get; set; }

        [DataMember(IsRequired = false)]
        [SctrqDataMember(AppServerName = "A.REQUEST.DATE", InBoundData = true)]
        public string RequestDate { get; set; }

        [DataMember(IsRequired = false)]
        [SctrqDataMember(AppServerName = "A.RECIPIENT.ID", InBoundData = true)]
        public string RecipientId { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.RECIPIENT.NAME", InBoundData = true)]
        public string RecipientName { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "AL.RECIPIENT.ADDRESS.LINES", InBoundData = true)]
        public string RecipientAddressLines { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.RECIPIENT.CITY", InBoundData = true)]
        public string RecipientCity { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.RECIPIENT.STATE", InBoundData = true)]
        public string RecipientState { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.RECIPIENT.POSTAL.CODE", InBoundData = true)]
        public string RecipientPostalCode { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.RECIPIENT.COUNTRY.CODE", InBoundData = true)]
        public string RecipientCountryCode { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.NUMBER.OF.COPIES", InBoundData = true)]
        public string NumberOfCopies { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "AL.COMMENTS", InBoundData = true)]
        public string Comments { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.REQUEST.HOLD.CODE", InBoundData = true)]
        public string RequestHoldCode { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.TRANSCRIPT.GROUPING", InBoundData = true)]
        public string TranscriptGrouping { get; set; }

        [DataMember(IsRequired = false)]
        [SctrqDataMember(AppServerName = "A.DATE.PRODUCED", InBoundData = true)]
        public string DateProduced { get; set; }



        public ContractRequest_InsertReqLog()
        {
        }
    }

    [GeneratedCodeAttribute("Colleague Data Contract Generator", "1.1")]
    [DataContract]
    [ColleagueDataContract(ColleagueId = "X.L16.INSERT.STU.REQ", GeneratedDateTime = "", User = "")]
    [SctrqDataContract(Application = "ST", DataContractVersion = 1)]
    public class InsertStuReqLogContractResponse
    {
        /// <summary>
        /// Version
        /// </summary>
        [DataMember]
        public int _AppServerVersion { get; set; }

        [DataMember]
        [SctrqDataMember(AppServerName = "A.ERROR.OCCURED", OutBoundData = true)]
        public string ErrorOccured { get; set; }

        [DataMember]
        [SctrqDataMember(AppServerName = "A.ERROR.MESSAGE", OutBoundData = true)]
        public string ErrorMessage { get; set; }

        [DataMember]
        [SctrqDataMember(AppServerName = "A.STUDENT.REQUEST.LOGS.ID", OutBoundData = true)]
        public string StudentRequestLogsId { get; set; }

        public InsertStuReqLogContractResponse()
        {
            ErrorOccured = string.Empty;
            ErrorMessage = string.Empty;
            StudentRequestLogsId = string.Empty;
        }
    }

    [GeneratedCodeAttribute("Colleague Data Contract Generator", "1.1")]
    [DataContract]
    [ColleagueDataContract(ColleagueId = "X.L16.INSERT.EXT.CRS", GeneratedDateTime = "", User = "")]
    [SctrqDataContract(Application = "ST", DataContractVersion = 1)]
    public class ContractRequest_InsertExtCrs
    {
        /// <summary>
        /// Version
        /// </summary>
        [DataMember]
        public int _AppServerVersion { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.EXTERNAL.TRANSCRIPTS.ID", InBoundData = true)]
        public string AExternalTranscriptsId { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.EXTL.PERSON.ID", InBoundData = true)]
        public string AExtlPersonId { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.EXTL.INSTITUTION", InBoundData = true)]
        public string AExtlInstitution { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.EXTL.COURSE", InBoundData = true)]
        public string AExtlCourse { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.EXTL.TITLE", InBoundData = true)]
        public string AExtlTitle { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.EXTL.CRED", InBoundData = true)]
        public string AExtlCred { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.EXTL.GRADE", InBoundData = true)]
        public string AExtlGrade { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.EXTL.GRADE.SCHEME", InBoundData = true)]
        public string AExtlGradeScheme { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.EXTL.START.DATE", InBoundData = true)]
        public string AExtlStartDate { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.EXTL.END.DATE", InBoundData = true)]
        public string AExtlEndDate { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.EXTL.OPR", InBoundData = true)]
        public string AExtlOpr { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.EXTL.PERS.INST.IDX", InBoundData = true)]
        public string AExtlPersInstIdx { get; set; }


        public ContractRequest_InsertExtCrs()
        {
        }
    }

    [GeneratedCodeAttribute("Colleague Data Contract Generator", "1.1")]
    [DataContract]
    [ColleagueDataContract(ColleagueId = "X.L16.INSERT.EXT.CRS", GeneratedDateTime = "", User = "")]
    [SctrqDataContract(Application = "ST", DataContractVersion = 1)]
    public class InsertCourseContractResponse
    {
        /// <summary>
        /// Version
        /// </summary>
        [DataMember]
        public int _AppServerVersion { get; set; }

        [DataMember]
        [SctrqDataMember(AppServerName = "A.RESULT", OutBoundData = true)]
        public string Result { get; set; }

        public InsertCourseContractResponse()
        {
            Result = string.Empty;
        }
    }

    [GeneratedCodeAttribute("Colleague Data Contract Generator", "1.1")]
    [DataContract]
    [ColleagueDataContract(ColleagueId = "X.L16.GET.XML.TRAN", GeneratedDateTime = "", User = "")]
    [SctrqDataContract(Application = "ST", DataContractVersion = 1)]
    public class ContractRequest_GetXmlTran
    {
        /// <summary>
        /// Version
        /// </summary>
        [DataMember]
        public int _AppServerVersion { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.ID", InBoundData = true)]
        public string StudentId { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.TRGR", InBoundData = true)]
        public string TranscriptGrouping { get; set; }

        public ContractRequest_GetXmlTran()
        {

        }
    }

    [GeneratedCodeAttribute("Colleague Data Contract Generator", "1.1")]
    [DataContract]
    [ColleagueDataContract(ColleagueId = "X.L16.GET.XML.TRAN", GeneratedDateTime = "", User = "")]
    [SctrqDataContract(Application = "ST", DataContractVersion = 1)]
    public class GetXmlTranscriptResponse
    {
        /// <summary>
        /// Version
        /// </summary>
        [DataMember]
        public int _AppServerVersion { get; set; }

        [DataMember]
        [SctrqDataMember(AppServerName = "A.TEXT", OutBoundData = true)]
        public string TranscriptText { get; set; }

        public GetXmlTranscriptResponse()
        {
            TranscriptText = string.Empty;
        }
    }

    [GeneratedCodeAttribute("Colleague Data Contract Generator", "1.1")]
    [DataContract]
    [ColleagueDataContract(ColleagueId = "CREATE.PLAIN.TEXT.TRANSCRIPT", GeneratedDateTime = "", User = "")]
    [SctrqDataContract(Application = "ST", DataContractVersion = 1)]
    public class ContractRequest_GetTextTran
    {
        /// <summary>
        /// Version
        /// </summary>
        [DataMember]
        public int _AppServerVersion { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.ID", InBoundData = true)]
        public string StudentId { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "A.TRGR", InBoundData = true)]
        public string TranscriptGrouping { get; set; }

        public ContractRequest_GetTextTran()
        {
        }
    }

    [GeneratedCodeAttribute("Colleague Data Contract Generator", "1.1")]
    [DataContract]
    [ColleagueDataContract(ColleagueId = "CREATE.PLAIN.TEXT.TRANSCRIPT", GeneratedDateTime = "", User = "")]
    [SctrqDataContract(Application = "ST", DataContractVersion = 1)]
    public class GetPlainTextTranscriptResponse
    {
        /// <summary>
        /// Version
        /// </summary>
        [DataMember]
        public int _AppServerVersion { get; set; }

        [DataMember]
        [SctrqDataMember(AppServerName = "A.TEXT", OutBoundData = true)]
        public string TranscriptText { get; set; }

        public GetPlainTextTranscriptResponse()
        {
            TranscriptText = string.Empty;
        }
    }

    [GeneratedCodeAttribute("Colleague Data Contract Generator", "1.1")]
    [DataContract]
    [ColleagueDataContract(ColleagueId = "GET.APPL.ENTITIES", GeneratedDateTime = "", User = "")]
    [SctrqDataContract(Application = "UT", DataContractVersion = 1)]
    public class GetApplEntitiesRequest
    {
        /// <summary>
        /// Version
        /// </summary>
        [DataMember]
        public int _AppServerVersion { get; set; }

        [DataMember(IsRequired = true)]
        [SctrqDataMember(AppServerName = "TV.APPLICATION", InBoundData = true)]
        public string TvApplication { get; set; }

        public GetApplEntitiesRequest()
        {
        }
    }

    [GeneratedCodeAttribute("Colleague Data Contract Generator", "1.1")]
    [DataContract]
    [ColleagueDataContract(ColleagueId = "GET.APPL.ENTITIES", GeneratedDateTime = "", User = "")]
    [SctrqDataContract(Application = "UT", DataContractVersion = 1)]
    public class GetApplEntitiesResponse
    {
        /// <summary>
        /// Version
        /// </summary>
        [DataMember]
        public int _AppServerVersion { get; set; }

        [DataMember]
        [SctrqDataMember(AppServerName = "TV.ENTITIES", OutBoundData = true)]
        public List<string> TvEntities { get; set; }

        public GetApplEntitiesResponse()
        {
            TvEntities = new List<string>();
        }
    }

    #endregion Data Contracts

    #region Entity Contracts

    [GeneratedCodeAttribute("Colleague Data Contract Generator", "1.1")]
    [DataContract(Name = "CourseSectionsInfo")]
    [ColleagueDataContract(GeneratedDateTime = "", User = "")]
    [EntityDataContract(EntityName = "COURSE.SECTIONS", EntityType = "PHYS")]
    public class CourseSectionsInfo : IColleagueEntity
    {
        /// <summary>
        /// Version
        /// </summary>
        [DataMember]
        public int _AppServerVersion { get; set; }

        /// <summary>
        /// Record Key
        /// </summary>
        [DataMember]
        public string Recordkey { get; set; }

        public void setKey(string key)
        {
            Recordkey = key;
        }

        /// <summary>
        /// CDD Name: SEC.SHORT.TITLE
        /// </summary>
        [DataMember(Order = 0, Name = "SEC.SHORT.TITLE")]
        public string SecShortTitle { get; set; }

        /// <summary>
        /// CDD Name: SEC.SUBJECT
        /// </summary>
        [DataMember(Order = 5, Name = "SEC.SUBJECT")]
        public string SecSubject { get; set; }

        /// <summary>
        /// CDD Name: SEC.NO
        /// </summary>
        [DataMember(Order = 17, Name = "SEC.NO")]
        public string SecNo { get; set; }


        // build up all the Associated objects and add them to the properties
        public void buildAssociations()
        {

        }
    }

    [GeneratedCodeAttribute("Colleague Data Contract Generator", "1.1")]
    [DataContract(Name = "PersonStCourseInfo")]
    [ColleagueDataContract(GeneratedDateTime = "", User = "")]
    [EntityDataContract(EntityName = "PERSON.ST", EntityType = "PHYS")]
    public class PersonStCourseInfo : IColleagueEntity
    {
        /// <summary>
        /// Version
        /// </summary>
        [DataMember]
        public int _AppServerVersion { get; set; }

        /// <summary>
        /// Record Key
        /// </summary>
        [DataMember]
        public string Recordkey { get; set; }

        public void setKey(string key)
        {
            Recordkey = key;
        }

        /// <summary>
        /// CDD Name: PST.STUDENT.COURSE.SEC
        /// </summary>
        [DataMember(Order = 14, Name = "PST.STUDENT.COURSE.SEC")]
        public List<string> PstStudentCourseSec { get; set; }

        /// <summary>
        /// CDD Name: PST.ADVISEMENT
        /// </summary>
        [DataMember(Order = 20, Name = "PST.ADVISEMENT")]
        public List<string> PstAdvisement { get; set; }


        // build up all the Associated objects and add them to the properties
        public void buildAssociations()
        {

        }
    }

    [GeneratedCodeAttribute("Colleague Data Contract Generator", "1.1")]
    [DataContract(Name = "PersonLFM")]
    [ColleagueDataContract(GeneratedDateTime = "", User = "")]
    [EntityDataContract(EntityName = "PERSON", EntityType = "PHYS")]
    public class PersonLFM : IColleagueEntity
    {
        /// <summary>
        /// Version
        /// </summary>
        [DataMember]
        public int _AppServerVersion { get; set; }

        /// <summary>
        /// Record Key
        /// </summary>
        [DataMember]
        public string Recordkey { get; set; }

        public void setKey(string key)
        {
            Recordkey = key;
        }

        /// <summary>
        /// CDD Name: LAST.NAME
        /// </summary>
        [DataMember(Order = 0, Name = "LAST.NAME")]
        public string LastName { get; set; }

        /// <summary>
        /// CDD Name: FIRST.NAME
        /// </summary>
        [DataMember(Order = 2, Name = "FIRST.NAME")]
        public string FirstName { get; set; }

        /// <summary>
        /// CDD Name: MIDDLE.NAME
        /// </summary>
        [DataMember(Order = 3, Name = "MIDDLE.NAME")]
        public string MiddleName { get; set; }


        // build up all the Associated objects and add them to the properties
        public void buildAssociations()
        {

        }
    }

    [GeneratedCodeAttribute("Colleague Data Contract Generator", "1.1")]
    [DataContract(Name = "StudentAdvisementInfo")]
    [ColleagueDataContract(GeneratedDateTime = "", User = "")]
    [EntityDataContract(EntityName = "STUDENT.ADVISEMENT", EntityType = "PHYS")]
    public class StudentAdvisementInfo : IColleagueEntity
    {
        /// <summary>
        /// Version
        /// </summary>
        [DataMember]
        public int _AppServerVersion { get; set; }

        /// <summary>
        /// Record Key
        /// </summary>
        [DataMember]
        public string Recordkey { get; set; }

        public void setKey(string key)
        {
            Recordkey = key;
        }

        /// <summary>
        /// CDD Name: STAD.STUDENT
        /// </summary>
        [DataMember(Order = 0, Name = "STAD.STUDENT")]
        public string StadStudent { get; set; }

        /// <summary>
        /// CDD Name: STAD.FACULTY
        /// </summary>
        [DataMember(Order = 1, Name = "STAD.FACULTY")]
        public string StadFaculty { get; set; }

        /// <summary>
        /// CDD Name: STAD.START.DATE
        /// </summary>
        [DataMember(Order = 2, Name = "STAD.START.DATE")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [ColleagueDataMember(UseEnvisionInternalFormat = true)]
        public DateTime? StadStartDate { get; set; }

        /// <summary>
        /// CDD Name: STAD.END.DATE
        /// </summary>
        [DataMember(Order = 3, Name = "STAD.END.DATE")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [ColleagueDataMember(UseEnvisionInternalFormat = true)]
        public DateTime? StadEndDate { get; set; }


        // build up all the Associated objects and add them to the properties
        public void buildAssociations()
        {

        }
    }

    [GeneratedCodeAttribute("Colleague Data Contract Generator", "1.1")]
    [DataContract(Name = "StudentCourseSecInfo")]
    [ColleagueDataContract(GeneratedDateTime = "", User = "")]
    [EntityDataContract(EntityName = "STUDENT.COURSE.SEC", EntityType = "PHYS")]
    public class StudentCourseSecInfo : IColleagueEntity
    {
        /// <summary>
        /// Version
        /// </summary>
        [DataMember]
        public int _AppServerVersion { get; set; }

        /// <summary>
        /// Record Key
        /// </summary>
        [DataMember]
        public string Recordkey { get; set; }

        public void setKey(string key)
        {
            Recordkey = key;
        }

        /// <summary>
        /// CDD Name: SCS.COURSE.SECTION
        /// </summary>
        [DataMember(Order = 0, Name = "SCS.COURSE.SECTION")]
        public string ScsCourseSection { get; set; }


        // build up all the Associated objects and add them to the properties
        public void buildAssociations()
        {

        }
    }

    #endregion Entity Contracts
}
