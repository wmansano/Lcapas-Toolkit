using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lcapas.Core.Library.Apas.AcademicRecord
{

    #region Classes

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class TransmissionDataType
    {

        private string documentIDField;
        private System.DateTime createdDateTimeField;
        private CoreMain.DocumentTypeCodeType documentTypeCodeField;
        private CoreMain.TransmissionTypeType transmissionTypeField;
        private CoreMain.DocumentProcessCodeType documentProcessCodeField;
        private CoreMain.DocumentOfficialCodeType documentOfficialCodeField;
        private CoreMain.DocumentCompleteCodeType documentCompleteCodeField;
        private SourceDestinationType sourceField;
        private SourceDestinationType destinationField;
        private bool documentProcessCodeFieldSpecified;
        private bool documentOfficialCodeFieldSpecified;
        private bool documentCompleteCodeFieldSpecified;
        private string requestTrackingIDField;
        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DocumentID
        {
            get
            {
                return this.documentIDField;
            }
            set
            {
                this.documentIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime CreatedDateTime
        {
            get
            {
                return this.createdDateTimeField;
            }
            set
            {
                this.createdDateTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.DocumentTypeCodeType DocumentTypeCode
        {
            get
            {
                return this.documentTypeCodeField;
            }
            set
            {
                this.documentTypeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.TransmissionTypeType TransmissionType
        {
            get
            {
                return this.transmissionTypeField;
            }
            set
            {
                this.transmissionTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public SourceDestinationType Source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public SourceDestinationType Destination
        {
            get
            {
                return this.destinationField;
            }
            set
            {
                this.destinationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.DocumentProcessCodeType DocumentProcessCode
        {
            get
            {
                return this.documentProcessCodeField;
            }
            set
            {
                this.documentProcessCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DocumentProcessCodeSpecified
        {
            get
            {
                return this.documentProcessCodeFieldSpecified;
            }
            set
            {
                this.documentProcessCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.DocumentOfficialCodeType DocumentOfficialCode
        {
            get
            {
                return this.documentOfficialCodeField;
            }
            set
            {
                this.documentOfficialCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DocumentOfficialCodeSpecified
        {
            get
            {
                return this.documentOfficialCodeFieldSpecified;
            }
            set
            {
                this.documentOfficialCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.DocumentCompleteCodeType DocumentCompleteCode
        {
            get
            {
                return this.documentCompleteCodeField;
            }
            set
            {
                this.documentCompleteCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DocumentCompleteCodeSpecified
        {
            get
            {
                return this.documentCompleteCodeFieldSpecified;
            }
            set
            {
                this.documentCompleteCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RequestTrackingID
        {
            get
            {
                return this.requestTrackingIDField;
            }
            set
            {
                this.requestTrackingIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class SourceDestinationType
    {

        private OrganizationType organizationField;
        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public OrganizationType Organization
        {
            get
            {
                return this.organizationField;
            }
            set
            {
                this.organizationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class OrganizationType
    {

        private string oPEIDField;
        private string nCHELPIDField;
        private string iPEDSField;
        private string aTPField;
        private string fICEField;
        private string aCTField;
        private string cCDField;
        private string cEEBACTField;
        private string cSISField;
        private string uSISField;
        private string eSISField;
        private string aPASField;
        private CoreMain.LocalOrganizationIDType localOrganizationIDField;
        private List<string> organizationNameField;
        private List<ContactsType> contactsField;
        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OPEID
        {
            get
            {
                return this.oPEIDField;
            }
            set
            {
                this.oPEIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NCHELPID
        {
            get
            {
                return this.nCHELPIDField;
            }
            set
            {
                this.nCHELPIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string IPEDS
        {
            get
            {
                return this.iPEDSField;
            }
            set
            {
                this.iPEDSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ATP
        {
            get
            {
                return this.aTPField;
            }
            set
            {
                this.aTPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FICE
        {
            get
            {
                return this.fICEField;
            }
            set
            {
                this.fICEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ACT
        {
            get
            {
                return this.aCTField;
            }
            set
            {
                this.aCTField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CCD
        {
            get
            {
                return this.cCDField;
            }
            set
            {
                this.cCDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CEEBACT
        {
            get
            {
                return this.cEEBACTField;
            }
            set
            {
                this.cEEBACTField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CSIS
        {
            get
            {
                return this.cSISField;
            }
            set
            {
                this.cSISField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string USIS
        {
            get
            {
                return this.uSISField;
            }
            set
            {
                this.uSISField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ESIS
        {
            get
            {
                return this.eSISField;
            }
            set
            {
                this.eSISField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string APAS
        {
            get
            {
                return this.aPASField;
            }
            set
            {
                this.aPASField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.LocalOrganizationIDType LocalOrganizationID
        {
            get
            {
                return this.localOrganizationIDField;
            }
            set
            {
                this.localOrganizationIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> OrganizationName
        {
            get
            {
                return this.organizationNameField;
            }
            set
            {
                this.organizationNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<ContactsType> Contacts
        {
            get
            {
                return this.contactsField;
            }
            set
            {
                this.contactsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

///// <remarks/>
//[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
//[System.SerializableAttribute()]
//[System.Diagnostics.DebuggerStepThroughAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(TypeName = "CitizenshipType", Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
//public partial class CitizenshipType1 : CoreMain.CitizenshipType
//{

//    private CoreMain.CountryCodeType citizenshipCountryCodeField;
//    private bool citizenshipCountryCodeFieldSpecified;
//    private List<string> noteMessageField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
//    public CoreMain.CountryCodeType CitizenshipCountryCode
//    {
//        get
//        {
//            return this.citizenshipCountryCodeField;
//        }
//        set
//        {
//            this.citizenshipCountryCodeField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlIgnoreAttribute()]
//    public bool CitizenshipCountryCodeSpecified
//    {
//        get
//        {
//            return this.citizenshipCountryCodeFieldSpecified;
//        }
//        set
//        {
//            this.citizenshipCountryCodeFieldSpecified = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
//    public List<string> NoteMessage
//    {
//        get
//        {
//            return this.noteMessageField;
//        }
//        set
//        {
//            this.noteMessageField = value;
//        }
//    }
//}

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class CourseType
    {

        private CoreMain.CourseCreditBasisType courseCreditBasisField;
        private CoreMain.CourseCreditUnitsType courseCreditUnitsField;
        private bool courseCreditUnitsFieldSpecified;
        private CoreMain.CourseCreditLevelType courseCreditLevelField;
        private bool courseCreditLevelFieldSpecified;
        private decimal courseCreditValueField;
        private bool courseCreditValueFieldSpecified;
        private decimal courseCreditEarnedField;
        private bool courseCreditEarnedFieldSpecified;
        private string courseAcademicGradeScaleCodeField;
        private string courseAcademicGradeField;
        private CoreMain.CourseAcademicGradeStatusCodeType courseAcademicGradeStatusCodeField;
        private bool courseAcademicGradeStatusCodeFieldSpecified;
        private string courseNarrativeExplanationGradeField;
        private CoreMain.CourseRepeatCodeType courseRepeatCodeField;
        private bool courseRepeatCodeFieldSpecified;
        private string itemField;
        private ItemChoiceType itemElementNameField;
        private decimal courseQualityPointsEarnedField;
        private bool courseQualityPointsEarnedFieldSpecified;
        private CoreMain.CourseLevelType courseLevelField;
        private bool courseLevelFieldSpecified;
        private CoreMain.CourseGPAApplicabilityCodeType courseGPAApplicabilityCodeField;
        private bool courseGPAApplicabilityCodeFieldSpecified;
        private string courseSubjectAbbreviationField;
        private string courseNumberField;
        private string courseSectionNumberField;
        private string originalCourseIDField;
        private string agencyCourseIDField;
        private string courseTitleField;
        private System.DateTime courseAddDateField;
        private bool courseAddDateFieldSpecified;
        private System.DateTime courseDropDateField;
        private bool courseDropDateFieldSpecified;
        private AcademicRecord.SchoolType courseOverrideSchoolField;
        private string overrideSchoolCourseNumberField;
        private CoreMain.CourseApplicabilityType courseApplicabilityField;
        private bool courseApplicabilityFieldSpecified;
        private System.DateTime courseBeginDateField;
        private bool courseBeginDateFieldSpecified;
        private System.DateTime courseEndDateField;
        private bool courseEndDateFieldSpecified;
        private CoreMain.CourseInstructionSiteType courseInstructionSiteField;
        private bool courseInstructionSiteFieldSpecified;
        private string courseInstructionSiteNameField;
        private List<CoreMain.RAPType> requirementField;
        private List<CoreMain.RAPType> attributeField;
        private List<CoreMain.RAPType> proficiencyField;
        private List<CoreMain.LicensureType> licensureField;
        private List<CoreMain.LanguageOfInstructionType> languageOfInstructionField;
        private List<string> noteMessageField;
        private System.Xml.XmlElement userDefinedExtensionsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.CourseCreditBasisType CourseCreditBasis
        {
            get
            {
                return this.courseCreditBasisField;
            }
            set
            {
                this.courseCreditBasisField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.CourseCreditUnitsType CourseCreditUnits
        {
            get
            {
                return this.courseCreditUnitsField;
            }
            set
            {
                this.courseCreditUnitsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CourseCreditUnitsSpecified
        {
            get
            {
                return this.courseCreditUnitsFieldSpecified;
            }
            set
            {
                this.courseCreditUnitsFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.CourseCreditLevelType CourseCreditLevel
        {
            get
            {
                return this.courseCreditLevelField;
            }
            set
            {
                this.courseCreditLevelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CourseCreditLevelSpecified
        {
            get
            {
                return this.courseCreditLevelFieldSpecified;
            }
            set
            {
                this.courseCreditLevelFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal CourseCreditValue
        {
            get
            {
                return this.courseCreditValueField;
            }
            set
            {
                this.courseCreditValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CourseCreditValueSpecified
        {
            get
            {
                return this.courseCreditValueFieldSpecified;
            }
            set
            {
                this.courseCreditValueFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal CourseCreditEarned
        {
            get
            {
                return this.courseCreditEarnedField;
            }
            set
            {
                this.courseCreditEarnedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CourseCreditEarnedSpecified
        {
            get
            {
                return this.courseCreditEarnedFieldSpecified;
            }
            set
            {
                this.courseCreditEarnedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CourseAcademicGradeScaleCode
        {
            get
            {
                return this.courseAcademicGradeScaleCodeField;
            }
            set
            {
                this.courseAcademicGradeScaleCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CourseAcademicGrade
        {
            get
            {
                return this.courseAcademicGradeField;
            }
            set
            {
                this.courseAcademicGradeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.CourseAcademicGradeStatusCodeType CourseAcademicGradeStatusCode
        {
            get
            {
                return this.courseAcademicGradeStatusCodeField;
            }
            set
            {
                this.courseAcademicGradeStatusCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CourseAcademicGradeStatusCodeSpecified
        {
            get
            {
                return this.courseAcademicGradeStatusCodeFieldSpecified;
            }
            set
            {
                this.courseAcademicGradeStatusCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CourseNarrativeExplanationGrade
        {
            get
            {
                return this.courseNarrativeExplanationGradeField;
            }
            set
            {
                this.courseNarrativeExplanationGradeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.CourseRepeatCodeType CourseRepeatCode
        {
            get
            {
                return this.courseRepeatCodeField;
            }
            set
            {
                this.courseRepeatCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CourseRepeatCodeSpecified
        {
            get
            {
                return this.courseRepeatCodeFieldSpecified;
            }
            set
            {
                this.courseRepeatCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CourseCIPCode", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("CourseCSISCode", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("CourseNCESCode", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("CourseSCEDCode", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("CourseUSISCode", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal CourseQualityPointsEarned
        {
            get
            {
                return this.courseQualityPointsEarnedField;
            }
            set
            {
                this.courseQualityPointsEarnedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CourseQualityPointsEarnedSpecified
        {
            get
            {
                return this.courseQualityPointsEarnedFieldSpecified;
            }
            set
            {
                this.courseQualityPointsEarnedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.CourseLevelType CourseLevel
        {
            get
            {
                return this.courseLevelField;
            }
            set
            {
                this.courseLevelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CourseLevelSpecified
        {
            get
            {
                return this.courseLevelFieldSpecified;
            }
            set
            {
                this.courseLevelFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.CourseGPAApplicabilityCodeType CourseGPAApplicabilityCode
        {
            get
            {
                return this.courseGPAApplicabilityCodeField;
            }
            set
            {
                this.courseGPAApplicabilityCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CourseGPAApplicabilityCodeSpecified
        {
            get
            {
                return this.courseGPAApplicabilityCodeFieldSpecified;
            }
            set
            {
                this.courseGPAApplicabilityCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CourseSubjectAbbreviation
        {
            get
            {
                return this.courseSubjectAbbreviationField;
            }
            set
            {
                this.courseSubjectAbbreviationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CourseNumber
        {
            get
            {
                return this.courseNumberField;
            }
            set
            {
                this.courseNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CourseSectionNumber
        {
            get
            {
                return this.courseSectionNumberField;
            }
            set
            {
                this.courseSectionNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OriginalCourseID
        {
            get
            {
                return this.originalCourseIDField;
            }
            set
            {
                this.originalCourseIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AgencyCourseID
        {
            get
            {
                return this.agencyCourseIDField;
            }
            set
            {
                this.agencyCourseIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CourseTitle
        {
            get
            {
                return this.courseTitleField;
            }
            set
            {
                this.courseTitleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime CourseAddDate
        {
            get
            {
                return this.courseAddDateField;
            }
            set
            {
                this.courseAddDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CourseAddDateSpecified
        {
            get
            {
                return this.courseAddDateFieldSpecified;
            }
            set
            {
                this.courseAddDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime CourseDropDate
        {
            get
            {
                return this.courseDropDateField;
            }
            set
            {
                this.courseDropDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CourseDropDateSpecified
        {
            get
            {
                return this.courseDropDateFieldSpecified;
            }
            set
            {
                this.courseDropDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AcademicRecord.SchoolType CourseOverrideSchool
        {
            get
            {
                return this.courseOverrideSchoolField;
            }
            set
            {
                this.courseOverrideSchoolField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OverrideSchoolCourseNumber
        {
            get
            {
                return this.overrideSchoolCourseNumberField;
            }
            set
            {
                this.overrideSchoolCourseNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.CourseApplicabilityType CourseApplicability
        {
            get
            {
                return this.courseApplicabilityField;
            }
            set
            {
                this.courseApplicabilityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CourseApplicabilitySpecified
        {
            get
            {
                return this.courseApplicabilityFieldSpecified;
            }
            set
            {
                this.courseApplicabilityFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime CourseBeginDate
        {
            get
            {
                return this.courseBeginDateField;
            }
            set
            {
                this.courseBeginDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CourseBeginDateSpecified
        {
            get
            {
                return this.courseBeginDateFieldSpecified;
            }
            set
            {
                this.courseBeginDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime CourseEndDate
        {
            get
            {
                return this.courseEndDateField;
            }
            set
            {
                this.courseEndDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CourseEndDateSpecified
        {
            get
            {
                return this.courseEndDateFieldSpecified;
            }
            set
            {
                this.courseEndDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.CourseInstructionSiteType CourseInstructionSite
        {
            get
            {
                return this.courseInstructionSiteField;
            }
            set
            {
                this.courseInstructionSiteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CourseInstructionSiteSpecified
        {
            get
            {
                return this.courseInstructionSiteFieldSpecified;
            }
            set
            {
                this.courseInstructionSiteFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CourseInstructionSiteName
        {
            get
            {
                return this.courseInstructionSiteNameField;
            }
            set
            {
                this.courseInstructionSiteNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Requirement", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.RAPType> Requirement
        {
            get
            {
                return this.requirementField;
            }
            set
            {
                this.requirementField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Attribute", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.RAPType> Attribute
        {
            get
            {
                return this.attributeField;
            }
            set
            {
                this.attributeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Proficiency", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.RAPType> Proficiency
        {
            get
            {
                return this.proficiencyField;
            }
            set
            {
                this.proficiencyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Licensure", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.LicensureType> Licensure
        {
            get
            {
                return this.licensureField;
            }
            set
            {
                this.licensureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("LanguageOfInstruction", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.LanguageOfInstructionType> LanguageOfInstruction
        {
            get
            {
                return this.languageOfInstructionField;
            }
            set
            {
                this.languageOfInstructionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Xml.XmlElement UserDefinedExtensions
        {
            get
            {
                return this.userDefinedExtensionsField;
            }
            set
            {
                this.userDefinedExtensionsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class SchoolType
    {

        private string organizationNameField;
        private string oPEIDField;
        private string nCHELPIDField;
        private string iPEDSField;
        private string aTPField;
        private string fICEField;
        private string aCTField;
        private string cCDField;
        private string cEEBACTField;
        private string cSISField;
        private string uSISField;
        private string eSISField;
        private string aPASField;
        private CoreMain.LocalOrganizationIDType localOrganizationIDField;
        private CoreMain.SchoolOverrideCodeType schoolOverrideCodeField;
        private bool schoolOverrideCodeFieldSpecified;
        private CoreMain.SchoolLevelType schoolLevelField;
        private bool schoolLevelFieldSpecified;
        private List<ContactsType> contactsField;
        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OrganizationName
        {
            get
            {
                return organizationNameField;
            }
            set
            {
                organizationNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OPEID
        {
            get
            {
                return this.oPEIDField;
            }
            set
            {
                this.oPEIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NCHELPID
        {
            get
            {
                return this.nCHELPIDField;
            }
            set
            {
                this.nCHELPIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string IPEDS
        {
            get
            {
                return this.iPEDSField;
            }
            set
            {
                this.iPEDSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ATP
        {
            get
            {
                return this.aTPField;
            }
            set
            {
                this.aTPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FICE
        {
            get
            {
                return this.fICEField;
            }
            set
            {
                this.fICEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ACT
        {
            get
            {
                return this.aCTField;
            }
            set
            {
                this.aCTField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CCD
        {
            get
            {
                return this.cCDField;
            }
            set
            {
                this.cCDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CEEBACT
        {
            get
            {
                return this.cEEBACTField;
            }
            set
            {
                this.cEEBACTField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CSIS
        {
            get
            {
                return this.cSISField;
            }
            set
            {
                this.cSISField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string USIS
        {
            get
            {
                return this.uSISField;
            }
            set
            {
                this.uSISField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ESIS
        {
            get
            {
                return this.eSISField;
            }
            set
            {
                this.eSISField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string APAS
        {
            get
            {
                return this.aPASField;
            }
            set
            {
                this.aPASField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.LocalOrganizationIDType LocalOrganizationID
        {
            get
            {
                return this.localOrganizationIDField;
            }
            set
            {
                this.localOrganizationIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.SchoolOverrideCodeType SchoolOverrideCode
        {
            get
            {
                return this.schoolOverrideCodeField;
            }
            set
            {
                this.schoolOverrideCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SchoolOverrideCodeSpecified
        {
            get
            {
                return this.schoolOverrideCodeFieldSpecified;
            }
            set
            {
                this.schoolOverrideCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.SchoolLevelType SchoolLevel
        {
            get
            {
                return this.schoolLevelField;
            }
            set
            {
                this.schoolLevelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SchoolLevelSpecified
        {
            get
            {
                return this.schoolLevelFieldSpecified;
            }
            set
            {
                this.schoolLevelFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<ContactsType> Contacts
        {
            get
            {
                return this.contactsField;
            }
            set
            {
                this.contactsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class ContactsType
    {
        private List<AddressType1> addressField;
        private List<CoreMain.PhoneType> phoneField;
        private List<CoreMain.PhoneType> faxPhoneField;
        private List<EmailType1> emailField;
        private List<CoreMain.URLType> uRLField;
        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Address", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AddressType1> Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Phone", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.PhoneType> Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FaxPhone", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.PhoneType> FaxPhone
        {
            get
            {
                return this.faxPhoneField;
            }
            set
            {
                this.faxPhoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Email", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<EmailType1> Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("URL", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.URLType> URL
        {
            get
            {
                return this.uRLField;
            }
            set
            {
                this.uRLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AddressType", Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class AddressType1 : CoreMain.AddressType
    {

        private List<string> attentionLineField;
        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AttentionLine", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> AttentionLine
        {
            get
            {
                return this.attentionLineField;
            }
            set
            {
                this.attentionLineField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "EmailType", Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class EmailType1 : CoreMain.EmailType
    {

        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class AcademicSessionType
    {

        private CoreMain.AcademicSessionDetailType academicSessionDetailField;
        private AcademicRecord.SchoolType schoolField;
        private CoreMain.StudentLevelType studentLevelField;
        private List<AcademicRecord.AcademicProgramType> academicProgramField;
        private List<AcademicAwardType> academicAwardField;
        private List<CourseType> courseField;
        private List<AcademicSummaryFType> academicSummaryField;
        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.AcademicSessionDetailType AcademicSessionDetail
        {
            get
            {
                return this.academicSessionDetailField;
            }
            set
            {
                this.academicSessionDetailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AcademicRecord.SchoolType School
        {
            get
            {
                return this.schoolField;
            }
            set
            {
                this.schoolField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.StudentLevelType StudentLevel
        {
            get
            {
                return this.studentLevelField;
            }
            set
            {
                this.studentLevelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AcademicProgram", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AcademicRecord.AcademicProgramType> AcademicProgram
        {
            get
            {
                return this.academicProgramField;
            }
            set
            {
                this.academicProgramField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AcademicAward", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AcademicAwardType> AcademicAward
        {
            get
            {
                return this.academicAwardField;
            }
            set
            {
                this.academicAwardField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Course", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CourseType> Course
        {
            get
            {
                return this.courseField;
            }
            set
            {
                this.courseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AcademicSummary", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AcademicSummaryFType> AcademicSummary
        {
            get
            {
                return this.academicSummaryField;
            }
            set
            {
                this.academicSummaryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AcademicProgramType", Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class AcademicProgramType : CoreMain.AcademicProgramType
    {

        private AcademicSummaryE2Type programSummaryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AcademicSummaryE2Type ProgramSummary
        {
            get
            {
                return this.programSummaryField;
            }
            set
            {
                this.programSummaryField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class AcademicSummaryE2Type : AcademicSummaryBaseType
    {

        private List<CoreMain.DelinquenciesType> delinquenciesField;

        private System.DateTime exitDateField;

        private bool exitDateFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Delinquencies", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.DelinquenciesType> Delinquencies
        {
            get
            {
                return this.delinquenciesField;
            }
            set
            {
                this.delinquenciesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime ExitDate
        {
            get
            {
                return this.exitDateField;
            }
            set
            {
                this.exitDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ExitDateSpecified
        {
            get
            {
                return this.exitDateFieldSpecified;
            }
            set
            {
                this.exitDateFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AcademicSummaryFType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AcademicSummaryE2Type))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AcademicSummaryE1Type))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class AcademicSummaryBaseType
    {

        private CoreMain.AcademicSummaryTypeType academicSummaryTypeField;

        private bool academicSummaryTypeFieldSpecified;

        private CoreMain.AcademicSummaryLevelType academicSummaryLevelField;

        private bool academicSummaryLevelFieldSpecified;

        private CoreMain.GPAType gPAField;

        private CoreMain.AcademicHonorsType academicHonorsField;

        private string classRankField;

        private string classSizeField;

        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.AcademicSummaryTypeType AcademicSummaryType
        {
            get
            {
                return this.academicSummaryTypeField;
            }
            set
            {
                this.academicSummaryTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AcademicSummaryTypeSpecified
        {
            get
            {
                return this.academicSummaryTypeFieldSpecified;
            }
            set
            {
                this.academicSummaryTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.AcademicSummaryLevelType AcademicSummaryLevel
        {
            get
            {
                return this.academicSummaryLevelField;
            }
            set
            {
                this.academicSummaryLevelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AcademicSummaryLevelSpecified
        {
            get
            {
                return this.academicSummaryLevelFieldSpecified;
            }
            set
            {
                this.academicSummaryLevelFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.GPAType GPA
        {
            get
            {
                return this.gPAField;
            }
            set
            {
                this.gPAField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.AcademicHonorsType AcademicHonors
        {
            get
            {
                return this.academicHonorsField;
            }
            set
            {
                this.academicHonorsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
        public string ClassRank
        {
            get
            {
                return this.classRankField;
            }
            set
            {
                this.classRankField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
        public string ClassSize
        {
            get
            {
                return this.classSizeField;
            }
            set
            {
                this.classSizeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class AcademicSummaryFType : AcademicSummaryBaseType
    {

        private CoreMain.AcademicProgramType academicProgramField;
        private List<CoreMain.DelinquenciesType> delinquenciesField;
        private System.DateTime exitDateField;
        private bool exitDateFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.AcademicProgramType AcademicProgram
        {
            get
            {
                return this.academicProgramField;
            }
            set
            {
                this.academicProgramField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Delinquencies", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.DelinquenciesType> Delinquencies
        {
            get
            {
                return this.delinquenciesField;
            }
            set
            {
                this.delinquenciesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime ExitDate
        {
            get
            {
                return this.exitDateField;
            }
            set
            {
                this.exitDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ExitDateSpecified
        {
            get
            {
                return this.exitDateFieldSpecified;
            }
            set
            {
                this.exitDateFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class AcademicSummaryE1Type : AcademicSummaryBaseType
    {

        private CoreMain.AcademicProgramType academicProgramField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.AcademicProgramType AcademicProgram
        {
            get
            {
                return this.academicProgramField;
            }
            set
            {
                this.academicProgramField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class AcademicAwardType
    {

        private CoreMain.AcademicAwardLevelType? academicAwardLevelField;
        private bool academicAwardLevelFieldSpecified;
        private System.DateTime academicAwardDateField;
        private bool academicAwardDateFieldSpecified;
        private string academicAwardTitleField;
        private List<CoreMain.AcademicHonorsType> academicHonorsField;
        private bool academicCompletionIndicatorField;
        private bool academicCompletionIndicatorFieldSpecified;
        private System.DateTime academicCompletionDateField;
        private bool academicCompletionDateFieldSpecified;
        private List<AcademicRecord.AcademicProgramType> academicAwardProgramField;
        private List<CoreMain.AcademicDegreeRequirementType> academicDegreeRequirementField;
        private List<AcademicSummaryE1Type> academicSummaryField;
        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.AcademicAwardLevelType? AcademicAwardLevel
        {
            get
            {
                return this.academicAwardLevelField;
            }
            set
            {
                this.academicAwardLevelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AcademicAwardLevelSpecified
        {
            get
            {
                return this.academicAwardLevelFieldSpecified;
            }
            set
            {
                this.academicAwardLevelFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime AcademicAwardDate
        {
            get
            {
                return this.academicAwardDateField;
            }
            set
            {
                this.academicAwardDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AcademicAwardDateSpecified
        {
            get
            {
                return this.academicAwardDateFieldSpecified;
            }
            set
            {
                this.academicAwardDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AcademicAwardTitle
        {
            get
            {
                return this.academicAwardTitleField;
            }
            set
            {
                this.academicAwardTitleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AcademicHonors", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.AcademicHonorsType> AcademicHonors
        {
            get
            {
                return this.academicHonorsField;
            }
            set
            {
                this.academicHonorsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool AcademicCompletionIndicator
        {
            get
            {
                return this.academicCompletionIndicatorField;
            }
            set
            {
                this.academicCompletionIndicatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AcademicCompletionIndicatorSpecified
        {
            get
            {
                return this.academicCompletionIndicatorFieldSpecified;
            }
            set
            {
                this.academicCompletionIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime AcademicCompletionDate
        {
            get
            {
                return this.academicCompletionDateField;
            }
            set
            {
                this.academicCompletionDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AcademicCompletionDateSpecified
        {
            get
            {
                return this.academicCompletionDateFieldSpecified;
            }
            set
            {
                this.academicCompletionDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AcademicAwardProgram", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AcademicRecord.AcademicProgramType> AcademicAwardProgram
        {
            get
            {
                return this.academicAwardProgramField;
            }
            set
            {
                this.academicAwardProgramField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AcademicDegreeRequirement", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.AcademicDegreeRequirementType> AcademicDegreeRequirement
        {
            get
            {
                return this.academicDegreeRequirementField;
            }
            set
            {
                this.academicDegreeRequirementField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AcademicSummary", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AcademicSummaryE1Type> AcademicSummary
        {
            get
            {
                return this.academicSummaryField;
            }
            set
            {
                this.academicSummaryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class AcademicRecordType
    {

        private AcademicRecord.SchoolType schoolField;

        private CoreMain.StudentLevelType studentLevelField;

        private List<AcademicAwardType> academicAwardField;

        private List<AcademicSummaryFType> academicSummaryField;

        private List<AcademicSessionType> academicSessionField;

        private List<CourseType> courseField;

        private List<CoreMain.AdditionalStudentAchievementsType> additionalStudentAchievementsField;

        private List<string> noteMessageField;

        private System.Xml.XmlElement userDefinedExtensionsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AcademicRecord.SchoolType School
        {
            get
            {
                return this.schoolField;
            }
            set
            {
                this.schoolField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.StudentLevelType StudentLevel
        {
            get
            {
                return this.studentLevelField;
            }
            set
            {
                this.studentLevelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AcademicAward", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AcademicAwardType> AcademicAward
        {
            get
            {
                return this.academicAwardField;
            }
            set
            {
                this.academicAwardField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AcademicSummary", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AcademicSummaryFType> AcademicSummary
        {
            get
            {
                return this.academicSummaryField;
            }
            set
            {
                this.academicSummaryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AcademicSession", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AcademicSessionType> AcademicSession
        {
            get
            {
                return this.academicSessionField;
            }
            set
            {
                this.academicSessionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Course", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CourseType> Course
        {
            get
            {
                return this.courseField;
            }
            set
            {
                this.courseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AdditionalStudentAchievements", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.AdditionalStudentAchievementsType> AdditionalStudentAchievements
        {
            get
            {
                return this.additionalStudentAchievementsField;
            }
            set
            {
                this.additionalStudentAchievementsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Xml.XmlElement UserDefinedExtensions
        {
            get
            {
                return this.userDefinedExtensionsField;
            }
            set
            {
                this.userDefinedExtensionsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ResidencyType", Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class ResidencyType1 : CoreMain.ResidencyType
    {

        private string stateProvinceField;
        private string countyCodeField;
        private string countyField;
        private CoreMain.CountryCodeType countryCodeField;
        private bool countryCodeFieldSpecified;
        private string countryField;
        private CoreMain.ResidencyStatusCodeType residencyStatusCodeField;
        private bool residencyStatusCodeFieldSpecified;
        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string StateProvince
        {
            get
            {
                return this.stateProvinceField;
            }
            set
            {
                this.stateProvinceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CountyCode
        {
            get
            {
                return this.countyCodeField;
            }
            set
            {
                this.countyCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string County
        {
            get
            {
                return this.countyField;
            }
            set
            {
                this.countyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.CountryCodeType CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CountryCodeSpecified
        {
            get
            {
                return this.countryCodeFieldSpecified;
            }
            set
            {
                this.countryCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.ResidencyStatusCodeType ResidencyStatusCode
        {
            get
            {
                return this.residencyStatusCodeField;
            }
            set
            {
                this.residencyStatusCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ResidencyStatusCodeSpecified
        {
            get
            {
                return this.residencyStatusCodeFieldSpecified;
            }
            set
            {
                this.residencyStatusCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class HighSchoolType
    {

        private string organizationNameField;
        private string oPEIDField;
        private string nCHELPIDField;
        private string iPEDSField;
        private string aTPField;
        private string fICEField;
        private string aCTField;
        private string cCDField;
        private string cEEBACTField;
        private string cSISField;
        private string uSISField;
        private string eSISField;
        private string aPASField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OrganizationName
        {
            get
            {
                return this.organizationNameField;
            }
            set
            {
                this.organizationNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OPEID
        {
            get
            {
                return this.oPEIDField;
            }
            set
            {
                this.oPEIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NCHELPID
        {
            get
            {
                return this.nCHELPIDField;
            }
            set
            {
                this.nCHELPIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string IPEDS
        {
            get
            {
                return this.iPEDSField;
            }
            set
            {
                this.iPEDSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ATP
        {
            get
            {
                return this.aTPField;
            }
            set
            {
                this.aTPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FICE
        {
            get
            {
                return this.fICEField;
            }
            set
            {
                this.fICEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ACT
        {
            get
            {
                return this.aCTField;
            }
            set
            {
                this.aCTField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CCD
        {
            get
            {
                return this.cCDField;
            }
            set
            {
                this.cCDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CEEBACT
        {
            get
            {
                return this.cEEBACTField;
            }
            set
            {
                this.cEEBACTField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CSIS
        {
            get
            {
                return this.cSISField;
            }
            set
            {
                this.cSISField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string USIS
        {
            get
            {
                return this.uSISField;
            }
            set
            {
                this.uSISField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ESIS
        {
            get
            {
                return this.eSISField;
            }
            set
            {
                this.eSISField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string APAS
        {
            get
            {
                return this.aPASField;
            }
            set
            {
                this.aPASField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class PersonType
    {

        private string schoolAssignedPersonIDField;
        private string sINField;
        private string nSNField;
        private List<CoreMain.AgencyIdentifierType> agencyIdentifierField;
        private string recipientAssignedIDField;
        private string sSNField;
        private CoreMain.BirthType birthField;
        private CoreMain.NameType nameField;
        private List<CoreMain.NameType> alternateNameField;
        private HighSchoolType highSchoolField;
        private List<ContactsType> contactsField;
        private CoreMain.GenderType genderField;
        private ResidencyType1 residencyField;
        private CoreMain.DeceasedType deceasedField;
        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SchoolAssignedPersonID
        {
            get
            {
                return this.schoolAssignedPersonIDField;
            }
            set
            {
                this.schoolAssignedPersonIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SIN
        {
            get
            {
                return this.sINField;
            }
            set
            {
                this.sINField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "token")]
        public string NSN
        {
            get
            {
                return this.nSNField;
            }
            set
            {
                this.nSNField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.AgencyIdentifierType> AgencyIdentifier
            {
            get
            {
                return this.agencyIdentifierField;
            }
            set
            {
                this.agencyIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RecipientAssignedID
        {
            get
            {
                return this.recipientAssignedIDField;
            }
            set
            {
                this.recipientAssignedIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SSN
        {
            get
            {
                return this.sSNField;
            }
            set
            {
                this.sSNField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.BirthType Birth
        {
            get
            {
                return this.birthField;
            }
            set
            {
                this.birthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.NameType Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AlternateName", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.NameType> AlternateName
        {
            get
            {
                return this.alternateNameField;
            }
            set
            {
                this.alternateNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HighSchoolType HighSchool
        {
            get
            {
                return this.highSchoolField;
            }
            set
            {
                this.highSchoolField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Contacts", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<ContactsType> Contacts
        {
            get
            {
                return this.contactsField;
            }
            set
            {
                this.contactsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.GenderType Gender
        {
            get
            {
                return this.genderField;
            }
            set
            {
                this.genderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ResidencyType1 Residency
        {
            get
            {
                return this.residencyField;
            }
            set
            {
                this.residencyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.DeceasedType Deceased
        {
            get
            {
                return this.deceasedField;
            }
            set
            {
                this.deceasedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class BirthType
    {

        private System.DateTime birthDateField;
        private bool birthDateFieldSpecified;
        private string birthdayField;
        private string birthCityField;
        private CoreMain.StateProvinceCodeType birthStateField;
        private bool birthStateFieldSpecified;
        private CoreMain.CountryCodeType birthCountryField;
        private bool birthCountryFieldSpecified;
        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime BirthDate
        {
            get
            {
                return this.birthDateField;
            }
            set
            {
                this.birthDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BirthDateSpecified
        {
            get
            {
                return this.birthDateFieldSpecified;
            }
            set
            {
                this.birthDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "gMonthDay")]
        public string Birthday
        {
            get
            {
                return this.birthdayField;
            }
            set
            {
                this.birthdayField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BirthCity
        {
            get
            {
                return this.birthCityField;
            }
            set
            {
                this.birthCityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.StateProvinceCodeType BirthState
        {
            get
            {
                return this.birthStateField;
            }
            set
            {
                this.birthStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BirthStateSpecified
        {
            get
            {
                return this.birthStateFieldSpecified;
            }
            set
            {
                this.birthStateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.CountryCodeType BirthCountry
        {
            get
            {
                return this.birthCountryField;
            }
            set
            {
                this.birthCountryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BirthCountrySpecified
        {
            get
            {
                return this.birthCountryFieldSpecified;
            }
            set
            {
                this.birthCountryFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class K12PersonType
    {
        private string schoolAssignedPersonIDField;
        private string sINField;
        private string nSNField;
        private List<CoreMain.AgencyIdentifierType> agencyIdentifierField;
        private string recipientAssignedIDField;
        private string sSNField;
        private BirthType birthField;
        private CoreMain.NameType nameField;
        private List<CoreMain.NameType> alternateNameField;
        private CoreMain.NameType parentGuardianNameField;
        private List<ContactsType> contactsField;
        private CoreMain.GenderType genderField;
        private CoreMain.RaceEthnicityType raceEthnicityField;
        private CoreMain.DeceasedType deceasedField;
        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SchoolAssignedPersonID
        {
            get
            {
                return this.schoolAssignedPersonIDField;
            }
            set
            {
                this.schoolAssignedPersonIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SIN
        {
            get
            {
                return this.sINField;
            }
            set
            {
                this.sINField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "token")]
        public string NSN
        {
            get
            {
                return this.nSNField;
            }
            set
            {
                this.nSNField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.AgencyIdentifierType> AgencyIdentifier
        {
            get
            {
                return this.agencyIdentifierField;
            }
            set
            {
                this.agencyIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RecipientAssignedID
        {
            get
            {
                return this.recipientAssignedIDField;
            }
            set
            {
                this.recipientAssignedIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SSN
        {
            get
            {
                return this.sSNField;
            }
            set
            {
                this.sSNField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public BirthType Birth
        {
            get
            {
                return this.birthField;
            }
            set
            {
                this.birthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.NameType Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AlternateName", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.NameType> AlternateName
        {
            get
            {
                return this.alternateNameField;
            }
            set
            {
                this.alternateNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.NameType ParentGuardianName
        {
            get
            {
                return this.parentGuardianNameField;
            }
            set
            {
                this.parentGuardianNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Contacts", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<ContactsType> Contacts
        {
            get
            {
                return this.contactsField;
            }
            set
            {
                this.contactsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.GenderType Gender
        {
            get
            {
                return this.genderField;
            }
            set
            {
                this.genderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.RaceEthnicityType RaceEthnicity
        {
            get
            {
                return this.raceEthnicityField;
            }
            set
            {
                this.raceEthnicityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.DeceasedType Deceased
        {
            get
            {
                return this.deceasedField;
            }
            set
            {
                this.deceasedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class StudentType
    {

        private PersonType personField;
        private List<AcademicRecordType> academicRecordField;
        private CoreMain.HealthType healthField;
        private List<CoreMain.TestsType> testsField;
        private List<string> noteMessageField;
        private System.Xml.XmlElement userDefinedExtensionsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PersonType Person
        {
            get
            {
                return this.personField;
            }
            set
            {
                this.personField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AcademicRecord", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AcademicRecordType> AcademicRecord
        {
            get
            {
                return this.academicRecordField;
            }
            set
            {
                this.academicRecordField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.HealthType Health
        {
            get
            {
                return this.healthField;
            }
            set
            {
                this.healthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Tests", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.TestsType> Tests
        {
            get
            {
                return this.testsField;
            }
            set
            {
                this.testsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Xml.XmlElement UserDefinedExtensions
        {
            get
            {
                return this.userDefinedExtensionsField;
            }
            set
            {
                this.userDefinedExtensionsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class K12StudentType
    {
        private K12PersonType personField;
        private List<AcademicRecordType> academicRecordField;
        private CoreMain.HealthType healthField;
        private List<CoreMain.TestsType> testsField;
        private List<string> noteMessageField;
        private System.Xml.XmlElement userDefinedExtensionsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public K12PersonType Person
        {
            get
            {
                return this.personField;
            }
            set
            {
                this.personField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AcademicRecord", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AcademicRecordType> AcademicRecord
        {
            get
            {
                return this.academicRecordField;
            }
            set
            {
                this.academicRecordField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.HealthType Health
        {
            get
            {
                return this.healthField;
            }
            set
            {
                this.healthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Tests", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.TestsType> Tests
        {
            get
            {
                return this.testsField;
            }
            set
            {
                this.testsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Xml.XmlElement UserDefinedExtensions
        {
            get
            {
                return this.userDefinedExtensionsField;
            }
            set
            {
                this.userDefinedExtensionsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class ResponseType
    {

        private System.DateTime createdDateTimeField;
        private string requestTrackingIDField;
        private string recipientTrackingIDField;
        private ResponseStatusType responseStatusField;
        private List<ResponseHoldType> responseHoldField;
        private OrderFeeType orderFeeField;
        private RequestedStudentType requestedStudentField;
        private RequestorReceiverType receiverField;
        private DeliveryMethodType deliveryMethodField;
        private bool deliveryMethodFieldSpecified;
        private ElectronicDeliveryType electronicDeliveryField;
        private string carrierNameField;
        private string carrierTrackingIDField;
        private List<string> noteMessageField;
        private System.Xml.XmlElement userDefinedExtensionsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime CreatedDateTime
        {
            get
            {
                return this.createdDateTimeField;
            }
            set
            {
                this.createdDateTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RequestTrackingID
        {
            get
            {
                return this.requestTrackingIDField;
            }
            set
            {
                this.requestTrackingIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RecipientTrackingID
        {
            get
            {
                return this.recipientTrackingIDField;
            }
            set
            {
                this.recipientTrackingIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ResponseStatusType ResponseStatus
        {
            get
            {
                return this.responseStatusField;
            }
            set
            {
                this.responseStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ResponseHold", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<ResponseHoldType> ResponseHold
        {
            get
            {
                return this.responseHoldField;
            }
            set
            {
                this.responseHoldField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public OrderFeeType OrderFee
        {
            get
            {
                return this.orderFeeField;
            }
            set
            {
                this.orderFeeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RequestedStudentType RequestedStudent
        {
            get
            {
                return this.requestedStudentField;
            }
            set
            {
                this.requestedStudentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RequestorReceiverType Receiver
        {
            get
            {
                return this.receiverField;
            }
            set
            {
                this.receiverField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DeliveryMethodType DeliveryMethod
        {
            get
            {
                return this.deliveryMethodField;
            }
            set
            {
                this.deliveryMethodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DeliveryMethodSpecified
        {
            get
            {
                return this.deliveryMethodFieldSpecified;
            }
            set
            {
                this.deliveryMethodFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ElectronicDeliveryType ElectronicDelivery
        {
            get
            {
                return this.electronicDeliveryField;
            }
            set
            {
                this.electronicDeliveryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CarrierName
        {
            get
            {
                return this.carrierNameField;
            }
            set
            {
                this.carrierNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CarrierTrackingID
        {
            get
            {
                return this.carrierTrackingIDField;
            }
            set
            {
                this.carrierTrackingIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Xml.XmlElement UserDefinedExtensions
        {
            get
            {
                return this.userDefinedExtensionsField;
            }
            set
            {
                this.userDefinedExtensionsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public enum ResponseStatusType
    {
        TranscriptSent = 0,
        TranscriptRequestReceived = 1,
        Hold = 2,
        NoRecord = 3,
        Canceled = 4,
        OfflineRecordSearch = 5,
        OfflineRecordSent = 6,
        MultipleMatch = 7,
        Deceased = 8,
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class ResponseHoldType
    {

        private HoldReasonType holdReasonField;
        private System.DateTime plannedReleaseDateField;
        private bool plannedReleaseDateFieldSpecified;
        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HoldReasonType HoldReason
        {
            get
            {
                return this.holdReasonField;
            }
            set
            {
                this.holdReasonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime PlannedReleaseDate
        {
            get
            {
                return this.plannedReleaseDateField;
            }
            set
            {
                this.plannedReleaseDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PlannedReleaseDateSpecified
        {
            get
            {
                return this.plannedReleaseDateFieldSpecified;
            }
            set
            {
                this.plannedReleaseDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class OrderFeeType
    {
        private FeeStatusCodeType feeStatusCodeField;
        private bool feeStatusCodeFieldSpecified;
        private decimal feeAmountField;
        private bool feeAmountFieldSpecified;
        private string feeStatusReasonField;
        private string noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public FeeStatusCodeType FeeStatusCode
        {
            get
            {
                return this.feeStatusCodeField;
            }
            set
            {
                this.feeStatusCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FeeStatusCodeSpecified
        {
            get
            {
                return this.feeStatusCodeFieldSpecified;
            }
            set
            {
                this.feeStatusCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal FeeAmount
        {
            get
            {
                return this.feeAmountField;
            }
            set
            {
                this.feeAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FeeAmountSpecified
        {
            get
            {
                return this.feeAmountFieldSpecified;
            }
            set
            {
                this.feeAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FeeStatusReason
        {
            get
            {
                return this.feeStatusReasonField;
            }
            set
            {
                this.feeStatusReasonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class RequestedStudentType
    {
        private PersonType personField;
        private List<AttendanceType> attendanceField;
        private UpdateContactsInformationType updateContactsInformationField;
        private bool updateContactsInformationFieldSpecified;
        private FeeDiscountRequestCodeType feeDiscountRequestCodeField;
        private bool feeDiscountRequestCodeFieldSpecified;
        private bool releaseAuthorizedIndicatorField;
        //private ReleaseAuthorizedMethodType releaseAuthorizedMethodField;
        //private bool releaseAuthorizedMethodFieldSpecified;
        private List<string> noteMessageField;
        private System.Xml.XmlElement userDefinedExtensionsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PersonType Person
        {
            get
            {
                return this.personField;
            }
            set
            {
                this.personField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Attendance", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AttendanceType> Attendance
        {
            get
            {
                return this.attendanceField;
            }
            set
            {
                this.attendanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public UpdateContactsInformationType UpdateContactsInformation
        {
            get
            {
                return this.updateContactsInformationField;
            }
            set
            {
                this.updateContactsInformationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UpdateContactsInformationSpecified
        {
            get
            {
                return this.updateContactsInformationFieldSpecified;
            }
            set
            {
                this.updateContactsInformationFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public FeeDiscountRequestCodeType FeeDiscountRequestCode
        {
            get
            {
                return this.feeDiscountRequestCodeField;
            }
            set
            {
                this.feeDiscountRequestCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FeeDiscountRequestCodeSpecified
        {
            get
            {
                return this.feeDiscountRequestCodeFieldSpecified;
            }
            set
            {
                this.feeDiscountRequestCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool ReleaseAuthorizedIndicator
        {
            get
            {
                return this.releaseAuthorizedIndicatorField;
            }
            set
            {
                this.releaseAuthorizedIndicatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        //public ReleaseAuthorizedMethodType ReleaseAuthorizedMethod
        //{
        //    get
        //    {
        //        return this.releaseAuthorizedMethodField;
        //    }
        //    set
        //    {
        //        this.releaseAuthorizedMethodField = value;
        //    }
        //}

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool ReleaseAuthorizedMethodSpecified
        //{
        //    get
        //    {
        //        return this.releaseAuthorizedMethodFieldSpecified;
        //    }
        //    set
        //    {
        //        this.releaseAuthorizedMethodFieldSpecified = value;
        //    }
        //}

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Xml.XmlElement UserDefinedExtensions
        {
            get
            {
                return this.userDefinedExtensionsField;
            }
            set
            {
                this.userDefinedExtensionsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class RequestorReceiverType
    {
        private object itemField;
        private List<string> noteMessageField;
        private System.Xml.XmlElement userDefinedExtensionsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Person", typeof(PersonType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("RequestorReceiverOrganization", typeof(RequestorReceiverOrganizationType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Xml.XmlElement UserDefinedExtensions
        {
            get
            {
                return this.userDefinedExtensionsField;
            }
            set
            {
                this.userDefinedExtensionsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class ElectronicDeliveryType
    {
        private ElectronicFormatType electronicFormatField;
        private ElectronicMethodType electronicMethodField;
        private string serviceProviderField;
        private string noteMessageField;
        private System.Xml.XmlElement userDefinedExtensionsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ElectronicFormatType ElectronicFormat
        {
            get
            {
                return this.electronicFormatField;
            }
            set
            {
                this.electronicFormatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ElectronicMethodType ElectronicMethod
        {
            get
            {
                return this.electronicMethodField;
            }
            set
            {
                this.electronicMethodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ServiceProvider
        {
            get
            {
                return this.serviceProviderField;
            }
            set
            {
                this.serviceProviderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Xml.XmlElement UserDefinedExtensions
        {
            get
            {
                return this.userDefinedExtensionsField;
            }
            set
            {
                this.userDefinedExtensionsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class AttendanceType
    {
        private AcademicRecord.SchoolType schoolField;
        private System.DateTime enrollDateField;
        private bool enrollDateFieldSpecified;
        private System.DateTime exitDateField;
        private bool exitDateFieldSpecified;
        private bool currentEnrollmentIndicatorField;
        private bool currentEnrollmentIndicatorFieldSpecified;
        private List<AcademicAwardsReportedType> academicAwardsReportedField;
        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AcademicRecord.SchoolType School
        {
            get
            {
                return this.schoolField;
            }
            set
            {
                this.schoolField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime EnrollDate
        {
            get
            {
                return this.enrollDateField;
            }
            set
            {
                this.enrollDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EnrollDateSpecified
        {
            get
            {
                return this.enrollDateFieldSpecified;
            }
            set
            {
                this.enrollDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime ExitDate
        {
            get
            {
                return this.exitDateField;
            }
            set
            {
                this.exitDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ExitDateSpecified
        {
            get
            {
                return this.exitDateFieldSpecified;
            }
            set
            {
                this.exitDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool CurrentEnrollmentIndicator
        {
            get
            {
                return this.currentEnrollmentIndicatorField;
            }
            set
            {
                this.currentEnrollmentIndicatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CurrentEnrollmentIndicatorSpecified
        {
            get
            {
                return this.currentEnrollmentIndicatorFieldSpecified;
            }
            set
            {
                this.currentEnrollmentIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AcademicAwardsReported", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AcademicAwardsReportedType> AcademicAwardsReported
        {
            get
            {
                return this.academicAwardsReportedField;
            }
            set
            {
                this.academicAwardsReportedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class RequestorReceiverOrganizationType
    {
        private List<string> organizationNameField;
        private string oPEIDField;
        private string nCHELPIDField;
        private string iPEDSField;
        private string aTPField;
        private string fICEField;
        private string aCTField;
        private string cCDField;
        private string cEEBACTField;
        private string cSISField;
        private string uSISField;
        private string eSISField;
        private string aPASField;
        private List<ContactsType> contactsField;
        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("OrganizationName", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> OrganizationName
        {
            get
            {
                return this.organizationNameField;
            }
            set
            {
                this.organizationNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OPEID
        {
            get
            {
                return this.oPEIDField;
            }
            set
            {
                this.oPEIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NCHELPID
        {
            get
            {
                return this.nCHELPIDField;
            }
            set
            {
                this.nCHELPIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string IPEDS
        {
            get
            {
                return this.iPEDSField;
            }
            set
            {
                this.iPEDSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ATP
        {
            get
            {
                return this.aTPField;
            }
            set
            {
                this.aTPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FICE
        {
            get
            {
                return this.fICEField;
            }
            set
            {
                this.fICEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ACT
        {
            get
            {
                return this.aCTField;
            }
            set
            {
                this.aCTField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CCD
        {
            get
            {
                return this.cCDField;
            }
            set
            {
                this.cCDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CEEBACT
        {
            get
            {
                return this.cEEBACTField;
            }
            set
            {
                this.cEEBACTField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CSIS
        {
            get
            {
                return this.cSISField;
            }
            set
            {
                this.cSISField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string USIS
        {
            get
            {
                return this.uSISField;
            }
            set
            {
                this.uSISField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ESIS
        {
            get
            {
                return this.eSISField;
            }
            set
            {
                this.eSISField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string APAS
        {
            get
            {
                return this.aPASField;
            }
            set
            {
                this.aPASField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Contacts", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<ContactsType> Contacts
        {
            get
            {
                return this.contactsField;
            }
            set
            {
                this.contactsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class AcademicAwardsReportedType
    {
        private string academicAwardTitleField;
        private System.DateTime academicAwardDateField;
        private bool academicAwardDateFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AcademicAwardTitle
        {
            get
            {
                return this.academicAwardTitleField;
            }
            set
            {
                this.academicAwardTitleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime AcademicAwardDate
        {
            get
            {
                return this.academicAwardDateField;
            }
            set
            {
                this.academicAwardDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AcademicAwardDateSpecified
        {
            get
            {
                return this.academicAwardDateFieldSpecified;
            }
            set
            {
                this.academicAwardDateFieldSpecified = value;
            }
        }
    }

///// <remarks/>
//[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
//[System.SerializableAttribute()]
//[System.Diagnostics.DebuggerStepThroughAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(TypeName = "SchoolType", Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
//public partial class SchoolType1
//{
//    private string organizationNameField;
//    private string oPEIDField;
//    private string nCHELPIDField;
//    private string iPEDSField;
//    private string aTPField;
//    private string fICEField;
//    private string aCTField;
//    private string cCDField;
//    private string cEEBACTField;
//    private string cSISField;
//    private string uSISField;
//    private string eSISField;
//    private string dUNSField;
//    private CoreMain.LocalOrganizationIDType localOrganizationIDField;
//    private CoreMain.SchoolOverrideCodeType schoolOverrideCodeField;
//    private bool schoolOverrideCodeFieldSpecified;
//    private CoreMain.SchoolLevelType schoolLevelField;
//    private bool schoolLevelFieldSpecified;
//    private List<ContactsType2> contactsField;
//    private List<string> noteMessageField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
//    public string OrganizationName
//    {
//        get
//        {
//            return this.organizationNameField;
//        }
//        set
//        {
//            this.organizationNameField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.3.0")]
//    public string OPEID
//    {
//        get
//        {
//            return this.oPEIDField;
//        }
//        set
//        {
//            this.oPEIDField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.3.0")]
//    public string NCHELPID
//    {
//        get
//        {
//            return this.nCHELPIDField;
//        }
//        set
//        {
//            this.nCHELPIDField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.3.0")]
//    public string IPEDS
//    {
//        get
//        {
//            return this.iPEDSField;
//        }
//        set
//        {
//            this.iPEDSField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.3.0")]
//    public string ATP
//    {
//        get
//        {
//            return this.aTPField;
//        }
//        set
//        {
//            this.aTPField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.3.0")]
//    public string FICE
//    {
//        get
//        {
//            return this.fICEField;
//        }
//        set
//        {
//            this.fICEField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.3.0")]
//    public string ACT
//    {
//        get
//        {
//            return this.aCTField;
//        }
//        set
//        {
//            this.aCTField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.3.0")]
//    public string CCD
//    {
//        get
//        {
//            return this.cCDField;
//        }
//        set
//        {
//            this.cCDField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.3.0")]
//    public string CEEBACT
//    {
//        get
//        {
//            return this.cEEBACTField;
//        }
//        set
//        {
//            this.cEEBACTField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.3.0")]
//    public string CSIS
//    {
//        get
//        {
//            return this.cSISField;
//        }
//        set
//        {
//            this.cSISField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.3.0")]
//    public string USIS
//    {
//        get
//        {
//            return this.uSISField;
//        }
//        set
//        {
//            this.uSISField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.3.0")]
//    public string ESIS
//    {
//        get
//        {
//            return this.eSISField;
//        }
//        set
//        {
//            this.eSISField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.3.0")]
//    public string DUNS
//    {
//        get
//        {
//            return this.dUNSField;
//        }
//        set
//        {
//            this.dUNSField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
//    public CoreMain.LocalOrganizationIDType LocalOrganizationID
//    {
//        get
//        {
//            return this.localOrganizationIDField;
//        }
//        set
//        {
//            this.localOrganizationIDField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
//    public CoreMain.SchoolOverrideCodeType SchoolOverrideCode
//    {
//        get
//        {
//            return this.schoolOverrideCodeField;
//        }
//        set
//        {
//            this.schoolOverrideCodeField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlIgnoreAttribute()]
//    public bool SchoolOverrideCodeSpecified
//    {
//        get
//        {
//            return this.schoolOverrideCodeFieldSpecified;
//        }
//        set
//        {
//            this.schoolOverrideCodeFieldSpecified = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
//    public CoreMain.SchoolLevelType SchoolLevel
//    {
//        get
//        {
//            return this.schoolLevelField;
//        }
//        set
//        {
//            this.schoolLevelField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlIgnoreAttribute()]
//    public bool SchoolLevelSpecified
//    {
//        get
//        {
//            return this.schoolLevelFieldSpecified;
//        }
//        set
//        {
//            this.schoolLevelFieldSpecified = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute("Contacts", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
//    public List<ContactsType2> Contacts
//    {
//        get
//        {
//            return this.contactsField;
//        }
//        set
//        {
//            this.contactsField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
//    public List<string> NoteMessage
//    {
//        get
//        {
//            return this.noteMessageField;
//        }
//        set
//        {
//            this.noteMessageField = value;
//        }
//    }

//}

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ContactsType", Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class ContactsType2
    {

        private List<AddressType2> addressField;
        private List<PhoneType2> phoneField;
        private List<PhoneType2> faxPhoneField;
        private List<EmailType2> emailField;
        private List<CoreMain.URLType> uRLField;
        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Address", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AddressType2> Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Phone", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<PhoneType2> Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FaxPhone", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<PhoneType2> FaxPhone
        {
            get
            {
                return this.faxPhoneField;
            }
            set
            {
                this.faxPhoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Email", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<EmailType2> Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("URL", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.URLType> URL
        {
            get
            {
                return this.uRLField;
            }
            set
            {
                this.uRLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AddressType", Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class AddressType2 : CoreMain.AddressType
    {

        private List<string> attentionLineField;
        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AttentionLine", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> AttentionLine
        {
            get
            {
                return this.attentionLineField;
            }
            set
            {
                this.attentionLineField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "PhoneType", Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class PhoneType2 : CoreMain.PhoneType
    {

        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public new List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "EmailType", Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class EmailType2 : CoreMain.EmailType
    {

        private List<string> noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class RequestType
    {

        private System.DateTime createdDateTimeField;
        private RequestorReceiverType requestorField;
        private RequestedStudentType requestedStudentField;
        private List<RecipientType> recipientField;
        private List<string> noteMessageField;
        private System.Xml.XmlElement userDefinedExtensionsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime CreatedDateTime
        {
            get
            {
                return this.createdDateTimeField;
            }
            set
            {
                this.createdDateTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RequestorReceiverType Requestor
        {
            get
            {
                return this.requestorField;
            }
            set
            {
                this.requestorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RequestedStudentType RequestedStudent
        {
            get
            {
                return this.requestedStudentField;
            }
            set
            {
                this.requestedStudentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Recipient", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<RecipientType> Recipient
        {
            get
            {
                return this.recipientField;
            }
            set
            {
                this.recipientField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Xml.XmlElement UserDefinedExtensions
        {
            get
            {
                return this.userDefinedExtensionsField;
            }
            set
            {
                this.userDefinedExtensionsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class RecipientType
    {

        private string requestTrackingIDField;
        private string recipientTrackingIDField;
        private RequestorReceiverType receiverField;
        private AcademicRecord.TranscriptHoldType transcriptHoldField;
        private TranscriptTypeType transcriptTypeField;
        private bool transcriptTypeFieldSpecified;
        private TranscriptPurposeType transcriptPurposeField;
        private bool transcriptPurposeFieldSpecified;
        private string certificationRequestedField;
        private DeliveryMethodType deliveryMethodField;
        private bool deliveryMethodFieldSpecified;
        private ElectronicDeliveryType electronicDeliveryField;
        private bool rushProcessingRequestedField;
        private bool rushProcessingRequestedFieldSpecified;
        private string deliveryInstructionField;
        private string transcriptCopiesField;
        private bool stampSealEnvelopeIndicatorField;
        private bool stampSealEnvelopeIndicatorFieldSpecified;
        private CoreMain.DocumentOfficialCodeType documentOfficialCodeField;
        private bool documentOfficialCodeFieldSpecified;
        private UpdateReasonType updateReasonField;
        private bool updateReasonFieldSpecified;
        private List<string> noteMessageField;
        private System.Xml.XmlElement userDefinedExtensionsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RequestTrackingID
        {
            get
            {
                return this.requestTrackingIDField;
            }
            set
            {
                this.requestTrackingIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RecipientTrackingID
        {
            get
            {
                return this.recipientTrackingIDField;
            }
            set
            {
                this.recipientTrackingIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RequestorReceiverType Receiver
        {
            get
            {
                return this.receiverField;
            }
            set
            {
                this.receiverField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TranscriptHoldType TranscriptHold
        {
            get
            {
                return this.transcriptHoldField;
            }
            set
            {
                this.transcriptHoldField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TranscriptTypeType TranscriptType
        {
            get
            {
                return this.transcriptTypeField;
            }
            set
            {
                this.transcriptTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TranscriptTypeSpecified
        {
            get
            {
                return this.transcriptTypeFieldSpecified;
            }
            set
            {
                this.transcriptTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TranscriptPurposeType TranscriptPurpose
        {
            get
            {
                return this.transcriptPurposeField;
            }
            set
            {
                this.transcriptPurposeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TranscriptPurposeSpecified
        {
            get
            {
                return this.transcriptPurposeFieldSpecified;
            }
            set
            {
                this.transcriptPurposeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CertificationRequested
        {
            get
            {
                return this.certificationRequestedField;
            }
            set
            {
                this.certificationRequestedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DeliveryMethodType DeliveryMethod
        {
            get
            {
                return this.deliveryMethodField;
            }
            set
            {
                this.deliveryMethodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DeliveryMethodSpecified
        {
            get
            {
                return this.deliveryMethodFieldSpecified;
            }
            set
            {
                this.deliveryMethodFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ElectronicDeliveryType ElectronicDelivery
        {
            get
            {
                return this.electronicDeliveryField;
            }
            set
            {
                this.electronicDeliveryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool RushProcessingRequested
        {
            get
            {
                return this.rushProcessingRequestedField;
            }
            set
            {
                this.rushProcessingRequestedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RushProcessingRequestedSpecified
        {
            get
            {
                return this.rushProcessingRequestedFieldSpecified;
            }
            set
            {
                this.rushProcessingRequestedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DeliveryInstruction
        {
            get
            {
                return this.deliveryInstructionField;
            }
            set
            {
                this.deliveryInstructionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
        public string TranscriptCopies
        {
            get
            {
                return this.transcriptCopiesField;
            }
            set
            {
                this.transcriptCopiesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool StampSealEnvelopeIndicator
        {
            get
            {
                return this.stampSealEnvelopeIndicatorField;
            }
            set
            {
                this.stampSealEnvelopeIndicatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool StampSealEnvelopeIndicatorSpecified
        {
            get
            {
                return this.stampSealEnvelopeIndicatorFieldSpecified;
            }
            set
            {
                this.stampSealEnvelopeIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.DocumentOfficialCodeType DocumentOfficialCode
        {
            get
            {
                return this.documentOfficialCodeField;
            }
            set
            {
                this.documentOfficialCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DocumentOfficialCodeSpecified
        {
            get
            {
                return this.documentOfficialCodeFieldSpecified;
            }
            set
            {
                this.documentOfficialCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public UpdateReasonType UpdateReason
        {
            get
            {
                return this.updateReasonField;
            }
            set
            {
                this.updateReasonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UpdateReasonSpecified
        {
            get
            {
                return this.updateReasonFieldSpecified;
            }
            set
            {
                this.updateReasonFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Xml.XmlElement UserDefinedExtensions
        {
            get
            {
                return this.userDefinedExtensionsField;
            }
            set
            {
                this.userDefinedExtensionsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public partial class TranscriptHoldType
    {

        private HoldTypeType holdTypeField;
        private string sessionDesignatorField;
        private string sessionNameField;
        private List<string> courseTitleField;
        private string academicAwardTitleField;
        private System.DateTime releaseDateField;
        private bool releaseDateFieldSpecified;
        private string noteMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HoldTypeType HoldType
        {
            get
            {
                return this.holdTypeField;
            }
            set
            {
                this.holdTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "gYearMonth")]
        public string SessionDesignator
        {
            get
            {
                return this.sessionDesignatorField;
            }
            set
            {
                this.sessionDesignatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SessionName
        {
            get
            {
                return this.sessionNameField;
            }
            set
            {
                this.sessionNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CourseTitle", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> CourseTitle
        {
            get
            {
                return this.courseTitleField;
            }
            set
            {
                this.courseTitleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AcademicAwardTitle
        {
            get
            {
                return this.academicAwardTitleField;
            }
            set
            {
                this.academicAwardTitleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime ReleaseDate
        {
            get
            {
                return this.releaseDateField;
            }
            set
            {
                this.releaseDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReleaseDateSpecified
        {
            get
            {
                return this.releaseDateFieldSpecified;
            }
            set
            {
                this.releaseDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NoteMessage
        {
            get
            {
                return this.noteMessageField;
            }
            set
            {
                this.noteMessageField = value;
            }
        }
    }


#endregion

    #region Types

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0", IncludeInSchema = false)]
    public enum ItemChoiceType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":CourseCIPCode")]
        CourseCIPCode,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":CourseCSISCode")]
        CourseCSISCode,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":CourseNCESCode")]
        CourseNCESCode,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":CourseSCEDCode")]
        CourseSCEDCode,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":CourseUSISCode")]
        CourseUSISCode,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public enum HoldReasonType
    {

            [Display(Name = "Financial")]
            Financial = 1,

            //[Display(Name = "Transcript Fee")]
            //TranscriptFee = 2,

            [Display(Name = "RequestedAction")]
            RequestedAction = 3,

            [Display(Name = "Other")]
            Other = 4,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public enum FeeStatusCodeType
    {

        /// <remarks/>
        Due,

        /// <remarks/>
        Paid,

        /// <remarks/>
        Waived,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public enum UpdateContactsInformationType
    {

        /// <remarks/>
        UpdateContacts,

        /// <remarks/>
        NoUpdate,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public enum FeeDiscountRequestCodeType
    {

        /// <remarks/>
        FirstTranscriptRequested,

        /// <remarks/>
        PrepaidFee,

        /// <remarks/>
        FinancialHardship,
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public enum ReleaseAuthorizedMethodType
    {

        /// <remarks/>
        Signature,

        /// <remarks/>
        ElectronicSignature,

        /// <remarks/>
        LegitimateEducationalInterest,

        /// <remarks/>
        Other,
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public enum ElectronicFormatType
    {

        /// <remarks/>
        XML,

        /// <remarks/>
        EDI,

        /// <remarks/>
        PDF,

        /// <remarks/>
        Other,
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public enum ElectronicMethodType
    {

        /// <remarks/>
        Email,

        /// <remarks/>
        ServiceProvider,

        /// <remarks/>
        DirectFileTransfer,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("")]
        Item,
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public enum DeliveryMethodType
    {

        /// <remarks/>
        HoldForPickup,

        /// <remarks/>
        Mail,

        /// <remarks/>
        Express,

        /// <remarks/>
        Overnight,

        /// <remarks/>
        Fax,

        /// <remarks/>
        FaxMail,

        /// <remarks/>
        FaxExpress,

        /// <remarks/>
        FaxOvernight,

        /// <remarks/>
        Electronic,

        /// <remarks/>
        ExpressInternational,

        /// <remarks/>
        ExpressCanadaMexico,

        /// <remarks/>
        ExpressUnitedStates,
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public enum TranscriptTypeType
    {

        /// <remarks/>
        Undergraduate,

        /// <remarks/>
        Law,

        /// <remarks/>
        Dental,

        /// <remarks/>
        Pharmacy,

        /// <remarks/>
        Medical,

        /// <remarks/>
        Management,

        /// <remarks/>
        Health,

        /// <remarks/>
        Graduate,

        /// <remarks/>
        Complete,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public enum TranscriptPurposeType
    {

        /// <remarks/>
        Admission,

        /// <remarks/>
        Registrar,

        /// <remarks/>
        AdmissionRegistrar,

        /// <remarks/>
        CertificationLicensure,

        /// <remarks/>
        Employment,

        /// <remarks/>
        Scholarship,

        /// <remarks/>
        SelfManagedPackage,

        /// <remarks/>
        Self,

        /// <remarks/>
        AdmissionService,

        /// <remarks/>
        GraduateAdmissions,

        /// <remarks/>
        LawSchoolAdmissions,

        /// <remarks/>
        MedicalSchoolAdmissions,

        /// <remarks/>
        ScholarshipGrantFellowship,

        /// <remarks/>
        Transfer,

        /// <remarks/>
        UndergraduateAdmissions,

        /// <remarks/>
        Other,
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public enum UpdateReasonType
    {

        /// <remarks/>
        Cancelled,

        /// <remarks/>
        Revised,

        /// <remarks/>
        Duplicate,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:org:pesc:sector:AcademicRecord:v1.9.0")]
    public enum HoldTypeType
    {
        [Display(Name = "Now")]
        [DataType(DataType.Text)]
        Now = 0,

        [Display(Name = "After Degree Awarded")]
        [DataType(DataType.Date)]
        AfterDegreeAwarded = 1,

        [Display(Name = "After Grades Posted")]
        [DataType(DataType.Date)]
        AfterGradesPosted = 2,

        [Display(Name = "After Specified Date")]
        [DataType(DataType.Date)]
        AfterSpecifiedDate = 3,

        [Display(Name = "After Specified Term")]
        [DataType("Term")]
        [RegularExpression(@"[0-9]{2} ?[FfLlSsMmNnWw]{2}")]  // Ex: 18FL, 19SM, 19 WN
        AfterSpecifiedTerm = 4,

        /// <remarks/>
        //AfterCertificateAwarded,

        /// <remarks/>
        //AfterDegreeCompletionStatement,

        /// <remarks/>
        //AfterHonorsStatement,

        /// <remarks/>
        //AfterCurrentTermEnrollment,

        [Display(Name = "After Specified Course Grade")]
        [DataType(DataType.Text)]
        AfterSpecifiedCourseGrade = 9,

        /// <remarks/>
        //AfterGradesChanged,

        /// <remarks/>
        //AfterCorrespondenceCourseCompleted,

        /// <remarks/>
        //Other,
    }


    #endregion

}