using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Lcapas.Core.Library.Apas.CoreMain {

    #region Classes

    ///// <remarks/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    //public partial class OrganizationNameType
    //{

    //    private string organizationNameTypeField;

    //    public string OrganizationName
    //    {
    //        get
    //        {
    //            return this.organizationNameTypeField;
    //        }
    //        set
    //        {
    //            this.organizationNameTypeField = value;
    //        }
    //    }
    //}

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class LocalOrganizationIDType
    {
        private string localOrganizationIDCodeField;
        private LocalOrganizationIDCodeQualifierType localOrganizationIDQualifierField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LocalOrganizationIDCode
        {
            get
            {
                return this.localOrganizationIDCodeField;
            }
            set
            {
                this.localOrganizationIDCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public LocalOrganizationIDCodeQualifierType LocalOrganizationIDQualifier
        {
            get
            {
                return this.localOrganizationIDQualifierField;
            }
            set
            {
                this.localOrganizationIDQualifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class TestScoreType
    {

        private string scoreValueField;

        private TestScoreMethodType testScoreMethodField;

        private bool testScoreMethodFieldSpecified;

        private bool scoreRevisedIndicatorField;

        private bool scoreRevisedIndicatorFieldSpecified;

        private bool scoreInvalidatedIndicatorField;

        private bool scoreInvalidatedIndicatorFieldSpecified;

        private bool scoreSelfreportedIndicatorField;

        private bool scoreSelfreportedIndicatorFieldSpecified;

        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ScoreValue
        {
            get
            {
                return this.scoreValueField;
            }
            set
            {
                this.scoreValueField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TestScoreMethodType TestScoreMethod
        {
            get
            {
                return this.testScoreMethodField;
            }
            set
            {
                this.testScoreMethodField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool TestScoreMethodSpecified
        {
            get
            {
                return this.testScoreMethodFieldSpecified;
            }
            set
            {
                this.testScoreMethodFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool ScoreRevisedIndicator
        {
            get
            {
                return this.scoreRevisedIndicatorField;
            }
            set
            {
                this.scoreRevisedIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool ScoreRevisedIndicatorSpecified
        {
            get
            {
                return this.scoreRevisedIndicatorFieldSpecified;
            }
            set
            {
                this.scoreRevisedIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool ScoreInvalidatedIndicator
        {
            get
            {
                return this.scoreInvalidatedIndicatorField;
            }
            set
            {
                this.scoreInvalidatedIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool ScoreInvalidatedIndicatorSpecified
        {
            get
            {
                return this.scoreInvalidatedIndicatorFieldSpecified;
            }
            set
            {
                this.scoreInvalidatedIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool ScoreSelfreportedIndicator
        {
            get
            {
                return this.scoreSelfreportedIndicatorField;
            }
            set
            {
                this.scoreSelfreportedIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool ScoreSelfreportedIndicatorSpecified
        {
            get
            {
                return this.scoreSelfreportedIndicatorFieldSpecified;
            }
            set
            {
                this.scoreSelfreportedIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class SubtestsType
    {

        private string subtestCodeField;

        private string subtestNameField;

        private List<TestScoreType> testScoresField;

        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SubtestCode
        {
            get
            {
                return this.subtestCodeField;
            }
            set
            {
                this.subtestCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SubtestName
        {
            get
            {
                return this.subtestNameField;
            }
            set
            {
                this.subtestNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("TestScores", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<TestScoreType> TestScores
        {
            get
            {
                return this.testScoresField;
            }
            set
            {
                this.testScoresField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class TestsType
    {
        private string testCodeField;
        private string testNameField;
        private object itemField;
        private ItemChoiceType1 itemElementNameField;
        private StudentLevelType studentLevelField;
        private List<SubtestsType> subtestField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TestCode
        {
            get
            {
                return this.testCodeField;
            }
            set
            {
                this.testCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TestName
        {
            get
            {
                return this.testNameField;
            }
            set
            {
                this.testNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("TestDate", typeof(System.DateTime), Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        [XmlElementAttribute("TestYear", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "gYear")]
        [XmlElementAttribute("TestYearMonth", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "gYearMonth")]
        [XmlChoiceIdentifierAttribute("ItemElementName")]
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
        [XmlIgnoreAttribute()]
        public ItemChoiceType1 ItemElementName
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public StudentLevelType StudentLevel
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
        [XmlElementAttribute("Subtest", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<SubtestsType> Subtest
        {
            get
            {
                return this.subtestField;
            }
            set
            {
                this.subtestField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class StudentLevelType
    {

        private StudentLevelCodeType studentLevelCodeField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public StudentLevelCodeType StudentLevelCode
        {
            get
            {
                return this.studentLevelCodeField;
            }
            set
            {
                this.studentLevelCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class ImmunizationsType
    {

        private System.DateTime immunizationDateField;
        private bool immunizationDateFieldSpecified;
        private string immunizationCodeField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime ImmunizationDate
        {
            get
            {
                return this.immunizationDateField;
            }
            set
            {
                this.immunizationDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool ImmunizationDateSpecified
        {
            get
            {
                return this.immunizationDateFieldSpecified;
            }
            set
            {
                this.immunizationDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ImmunizationCode
        {
            get
            {
                return this.immunizationCodeField;
            }
            set
            {
                this.immunizationCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class HealthType
    {

        private List<ImmunizationsType> immunizationsField;

        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute("Immunizations", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<ImmunizationsType> Immunizations
        {
            get
            {
                return this.immunizationsField;
            }
            set
            {
                this.immunizationsField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class LanguageType
    {

        private string languageCodeField;

        private LanguageProficiencyType languageProficiencyField;

        private bool languageProficiencyFieldSpecified;

        private LanguageUsageType languageUsageField;

        private bool languageUsageFieldSpecified;

        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LanguageCode
        {
            get
            {
                return this.languageCodeField;
            }
            set
            {
                this.languageCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public LanguageProficiencyType LanguageProficiency
        {
            get
            {
                return this.languageProficiencyField;
            }
            set
            {
                this.languageProficiencyField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool LanguageProficiencySpecified
        {
            get
            {
                return this.languageProficiencyFieldSpecified;
            }
            set
            {
                this.languageProficiencyFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public LanguageUsageType LanguageUsage
        {
            get
            {
                return this.languageUsageField;
            }
            set
            {
                this.languageUsageField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool LanguageUsageSpecified
        {
            get
            {
                return this.languageUsageFieldSpecified;
            }
            set
            {
                this.languageUsageFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class AdditionalStudentAchievementsType
    {

        private List<RAPType> requirementField;

        private List<RAPType> attributeField;

        private List<RAPType> proficiencyField;

        private List<LanguageType> languageField;

        private List<LicensureType> licensureField;

        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute("Requirement", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<RAPType> Requirement
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
        [XmlElementAttribute("Attribute", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<RAPType> Attribute
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
        [XmlElementAttribute("Proficiency", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<RAPType> Proficiency
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
        [XmlElementAttribute("Language", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<LanguageType> Language
        {
            get
            {
                return this.languageField;
            }
            set
            {
                this.languageField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Licensure", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<LicensureType> Licensure
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
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class RAPType
    {

        private List<string> rAPCodeField;

        private List<string> rAPNameField;

        private List<string> rAPSubNameField;

        private List<ConditionsMetCodeType> conditionsMetCodeField;

        private List<System.DateTime> conditionsMetDateField;

        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute("RAPCode", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> RAPCode
        {
            get
            {
                return this.rAPCodeField;
            }
            set
            {
                this.rAPCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("RAPName", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> RAPName
        {
            get
            {
                return this.rAPNameField;
            }
            set
            {
                this.rAPNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("RAPSubName", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> RAPSubName
        {
            get
            {
                return this.rAPSubNameField;
            }
            set
            {
                this.rAPSubNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("ConditionsMetCode", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<ConditionsMetCodeType> ConditionsMetCode
        {
            get
            {
                return this.conditionsMetCodeField;
            }
            set
            {
                this.conditionsMetCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("ConditionsMetDate", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public List<System.DateTime> ConditionsMetDate
        {
            get
            {
                return this.conditionsMetDateField;
            }
            set
            {
                this.conditionsMetDateField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class LicensureType
    {

        private List<string> licensureNameField;

        private List<System.DateTime> licensurePassageDateField;

        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute("LicensureName", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> LicensureName
        {
            get
            {
                return this.licensureNameField;
            }
            set
            {
                this.licensureNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("LicensurePassageDate", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public List<System.DateTime> LicensurePassageDate
        {
            get
            {
                return this.licensurePassageDateField;
            }
            set
            {
                this.licensurePassageDateField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class LanguageOfInstructionType
    {

        private string languageCodeField;

        private InstructionUsageType instructionUsageField;

        private bool instructionUsageFieldSpecified;

        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LanguageCode
        {
            get
            {
                return this.languageCodeField;
            }
            set
            {
                this.languageCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public InstructionUsageType InstructionUsage
        {
            get
            {
                return this.instructionUsageField;
            }
            set
            {
                this.instructionUsageField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool InstructionUsageSpecified
        {
            get
            {
                return this.instructionUsageFieldSpecified;
            }
            set
            {
                this.instructionUsageFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlIncludeAttribute(typeof(AcademicRecord.AddressType1))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class AddressType
    {

        private List<string> addressLineField;
        private string cityField;
        private object[] itemsField;
        private ItemsChoiceType[] itemsElementNameField;

        /// <remarks/>
        [XmlElementAttribute("AddressLine", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> AddressLine
        {
            get
            {
                return this.addressLineField;
            }
            set
            {
                this.addressLineField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("CountryCode", typeof(CountryCodeType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("PostalCode", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("StateProvince", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("StateProvinceCode", typeof(StateProvinceCodeType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("ItemsElementName")]
        [XmlIgnoreAttribute()]
        public ItemsChoiceType[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class PhoneType
    {

        private string countryPrefixCodeField;
        private string areaCityCodeField;
        private string phoneNumberField;
        private string phoneNumberExtensionField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CountryPrefixCode
        {
            get
            {
                return this.countryPrefixCodeField;
            }
            set
            {
                this.countryPrefixCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AreaCityCode
        {
            get
            {
                return this.areaCityCodeField;
            }
            set
            {
                this.areaCityCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PhoneNumber
        {
            get
            {
                return this.phoneNumberField;
            }
            set
            {
                this.phoneNumberField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PhoneNumberExtension
        {
            get
            {
                return this.phoneNumberExtensionField;
            }
            set
            {
                this.phoneNumberExtensionField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlIncludeAttribute(typeof(AcademicRecord.EmailType1))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class EmailType
    {

        private string emailAddressField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EmailAddress
        {
            get
            {
                return this.emailAddressField;
            }
            set
            {
                this.emailAddressField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class URLType
    {

        private string uRLAddressField;

        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string URLAddress
        {
            get
            {
                return this.uRLAddressField;
            }
            set
            {
                this.uRLAddressField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class AcademicSessionDetailType
    {

        private string sessionDesignatorField;
        private string sessionDesignatorSuffixField;
        private string sessionNameField;
        private SessionTypeType? sessionTypeField;
        private bool sessionTypeFieldSpecified;
        private string sessionSchoolYearField;
        private System.DateTime sessionBeginDateField;
        private bool sessionBeginDateFieldSpecified;
        private System.DateTime sessionEndDateField;
        private bool sessionEndDateFieldSpecified;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "gYearMonth")]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SessionDesignatorSuffix
        {
            get
            {
                return this.sessionDesignatorSuffixField;
            }
            set
            {
                this.sessionDesignatorSuffixField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public SessionTypeType? SessionType
        {
            get
            {
                return this.sessionTypeField;
            }
            set
            {
                this.sessionTypeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool SessionTypeSpecified
        {
            get
            {
                return this.sessionTypeFieldSpecified;
            }
            set
            {
                this.sessionTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SessionSchoolYear
        {
            get
            {
                return this.sessionSchoolYearField;
            }
            set
            {
                this.sessionSchoolYearField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime SessionBeginDate
        {
            get
            {
                return this.sessionBeginDateField;
            }
            set
            {
                this.sessionBeginDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool SessionBeginDateSpecified
        {
            get
            {
                return this.sessionBeginDateFieldSpecified;
            }
            set
            {
                this.sessionBeginDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime SessionEndDate
        {
            get
            {
                return this.sessionEndDateField;
            }
            set
            {
                this.sessionEndDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool SessionEndDateSpecified
        {
            get
            {
                return this.sessionEndDateFieldSpecified;
            }
            set
            {
                this.sessionEndDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class GPAType
    {

        private decimal creditHoursAttemptedField;
        private bool creditHoursAttemptedFieldSpecified;
        private decimal creditHoursEarnedField;
        private bool creditHoursEarnedFieldSpecified;
        private CourseCreditUnitsType creditUnitField;
        private bool creditUnitFieldSpecified;
        private decimal gradePointAverageField;
        private bool gradePointAverageFieldSpecified;
        private decimal totalQualityPointsField;
        private bool totalQualityPointsFieldSpecified;
        private decimal creditHoursforGPAField;
        private bool creditHoursforGPAFieldSpecified;
        private decimal gPARangeMinimumField;
        private bool gPARangeMinimumFieldSpecified;
        private decimal gPARangeMaximumField;
        private bool gPARangeMaximumFieldSpecified;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal CreditHoursAttempted
        {
            get
            {
                return this.creditHoursAttemptedField;
            }
            set
            {
                this.creditHoursAttemptedField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool CreditHoursAttemptedSpecified
        {
            get
            {
                return this.creditHoursAttemptedFieldSpecified;
            }
            set
            {
                this.creditHoursAttemptedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal CreditHoursEarned
        {
            get
            {
                return this.creditHoursEarnedField;
            }
            set
            {
                this.creditHoursEarnedField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool CreditHoursEarnedSpecified
        {
            get
            {
                return this.creditHoursEarnedFieldSpecified;
            }
            set
            {
                this.creditHoursEarnedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CourseCreditUnitsType CreditUnit
        {
            get
            {
                return this.creditUnitField;
            }
            set
            {
                this.creditUnitField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool CreditUnitSpecified
        {
            get
            {
                return this.creditUnitFieldSpecified;
            }
            set
            {
                this.creditUnitFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal GradePointAverage
        {
            get
            {
                return this.gradePointAverageField;
            }
            set
            {
                this.gradePointAverageField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool GradePointAverageSpecified
        {
            get
            {
                return this.gradePointAverageFieldSpecified;
            }
            set
            {
                this.gradePointAverageFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal TotalQualityPoints
        {
            get
            {
                return this.totalQualityPointsField;
            }
            set
            {
                this.totalQualityPointsField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool TotalQualityPointsSpecified
        {
            get
            {
                return this.totalQualityPointsFieldSpecified;
            }
            set
            {
                this.totalQualityPointsFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal CreditHoursforGPA
        {
            get
            {
                return this.creditHoursforGPAField;
            }
            set
            {
                this.creditHoursforGPAField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool CreditHoursforGPASpecified
        {
            get
            {
                return this.creditHoursforGPAFieldSpecified;
            }
            set
            {
                this.creditHoursforGPAFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal GPARangeMinimum
        {
            get
            {
                return this.gPARangeMinimumField;
            }
            set
            {
                this.gPARangeMinimumField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool GPARangeMinimumSpecified
        {
            get
            {
                return this.gPARangeMinimumFieldSpecified;
            }
            set
            {
                this.gPARangeMinimumFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal GPARangeMaximum
        {
            get
            {
                return this.gPARangeMaximumField;
            }
            set
            {
                this.gPARangeMaximumField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool GPARangeMaximumSpecified
        {
            get
            {
                return this.gPARangeMaximumFieldSpecified;
            }
            set
            {
                this.gPARangeMaximumFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class AcademicHonorsType
    {

        private string honorsTitleField;
        private HonorsRecognitionLevelType honorsLevelField;
        private bool honorsLevelFieldSpecified;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string HonorsTitle
        {
            get
            {
                return this.honorsTitleField;
            }
            set
            {
                this.honorsTitleField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HonorsRecognitionLevelType HonorsLevel
        {
            get
            {
                return this.honorsLevelField;
            }
            set
            {
                this.honorsLevelField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool HonorsLevelSpecified
        {
            get
            {
                return this.honorsLevelFieldSpecified;
            }
            set
            {
                this.honorsLevelFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlIncludeAttribute(typeof(CoreMain.AcademicProgramType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class AcademicProgramType
    {

        private string programCIPCodeField;
        private string programHEGISCodeField;
        private string programCSISCodeField;
        private string programUSISCodeField;
        private string programESISCodeField;
        private CoreMain.AcademicProgramTypeType academicPrgramTypeTypeField;
        private bool academicProgramTypeFieldSpecified;
        private string academicProgramNameField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ProgramCIPCode
        {
            get
            {
                return this.programCIPCodeField;
            }
            set
            {
                this.programCIPCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ProgramHEGISCode
        {
            get
            {
                return this.programHEGISCodeField;
            }
            set
            {
                this.programHEGISCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ProgramCSISCode
        {
            get
            {
                return this.programCSISCodeField;
            }
            set
            {
                this.programCSISCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ProgramUSISCode
        {
            get
            {
                return this.programUSISCodeField;
            }
            set
            {
                this.programUSISCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ProgramESISCode
        {
            get
            {
                return this.programESISCodeField;
            }
            set
            {
                this.programESISCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("AcademicProgramType", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.AcademicProgramTypeType AcademicProgramTypeType
        {
            get
            {
                return this.academicPrgramTypeTypeField;
            }
            set
            {
                this.academicPrgramTypeTypeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool AcademicProgramTypeSpecified
        {
            get
            {
                return this.academicProgramTypeFieldSpecified;
            }
            set
            {
                this.academicProgramTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AcademicProgramName
        {
            get
            {
                return this.academicProgramNameField;
            }
            set
            {
                this.academicProgramNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class AcademicDegreeRequirementType
    {

        private string thesisDissertationTitleField;

        private string thesisDissertationAdvisorField;

        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ThesisDissertationTitle
        {
            get
            {
                return this.thesisDissertationTitleField;
            }
            set
            {
                this.thesisDissertationTitleField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ThesisDissertationAdvisor
        {
            get
            {
                return this.thesisDissertationAdvisorField;
            }
            set
            {
                this.thesisDissertationAdvisorField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class DeceasedType
    {

        private bool deceasedIndicatorField;

        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool DeceasedIndicator
        {
            get
            {
                return this.deceasedIndicatorField;
            }
            set
            {
                this.deceasedIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlIncludeAttribute(typeof(AcademicRecord.ResidencyType1))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class ResidencyType
    {

        private StateProvinceCodeType stateProvinceCodeField;
        private bool stateProvinceCodeFieldSpecified;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public StateProvinceCodeType StateProvinceCode
        {
            get
            {
                return this.stateProvinceCodeField;
            }
            set
            {
                this.stateProvinceCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool StateProvinceCodeSpecified
        {
            get
            {
                return this.stateProvinceCodeFieldSpecified;
            }
            set
            {
                this.stateProvinceCodeFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class GenderType
    {

        private GenderCodeType genderCodeField;

        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GenderCodeType GenderCode
        {
            get
            {
                return this.genderCodeField;
            }
            set
            {
                this.genderCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class NameType
    {

        private string namePrefixField;
        private string firstNameField;
        private List<string> middleNameField;
        private string lastNameField;
        private NameSuffixType nameSuffixField;
        private bool nameSuffixFieldSpecified;
        private string nameTitleField;
        private string compositeNameField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NamePrefix
        {
            get
            {
                return this.namePrefixField;
            }
            set
            {
                this.namePrefixField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FirstName
        {
            get
            {
                return this.firstNameField;
            }
            set
            {
                this.firstNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("MiddleName", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> MiddleName
        {
            get
            {
                return this.middleNameField;
            }
            set
            {
                this.middleNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LastName
        {
            get
            {
                return this.lastNameField;
            }
            set
            {
                this.lastNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public NameSuffixType NameSuffix
        {
            get
            {
                return this.nameSuffixField;
            }
            set
            {
                this.nameSuffixField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool NameSuffixSpecified
        {
            get
            {
                return this.nameSuffixFieldSpecified;
            }
            set
            {
                this.nameSuffixFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NameTitle
        {
            get
            {
                return this.nameTitleField;
            }
            set
            {
                this.nameTitleField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CompositeName
        {
            get
            {
                return this.compositeNameField;
            }
            set
            {
                this.compositeNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class BirthType
    {

        private System.DateTime birthDateField;
        private bool birthDateFieldSpecified;
        private string birthdayField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "gMonthDay")]
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
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    [XmlRootAttribute("DLLoanInformation", Namespace = "urn:org:pesc:core:CoreMain:v1.16.0", IsNullable = false)]
    public partial class DLLoanInformationType : LoanInformationType
    {

        private System.Nullable<decimal> originationFeePercentField;

        private bool originationFeePercentFieldSpecified;

        private System.Nullable<decimal> interestRebatePercentField;

        private bool interestRebatePercentFieldSpecified;

        private System.Nullable<DLLoanInformationTypePromissoryNotePrintCode> promissoryNotePrintCodeField;

        private bool promissoryNotePrintCodeFieldSpecified;

        private System.Nullable<DLLoanInformationTypeDisclosureStatementPrintCode> disclosureStatementPrintCodeField;

        private bool disclosureStatementPrintCodeFieldSpecified;

        private System.Nullable<System.DateTime> academicYearBeginDateField;

        private bool academicYearBeginDateFieldSpecified;

        private System.Nullable<System.DateTime> academicYearEndDateField;

        private bool academicYearEndDateFieldSpecified;

        private ResponseType responseField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> OriginationFeePercent
        {
            get
            {
                return this.originationFeePercentField;
            }
            set
            {
                this.originationFeePercentField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool OriginationFeePercentSpecified
        {
            get
            {
                return this.originationFeePercentFieldSpecified;
            }
            set
            {
                this.originationFeePercentFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> InterestRebatePercent
        {
            get
            {
                return this.interestRebatePercentField;
            }
            set
            {
                this.interestRebatePercentField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool InterestRebatePercentSpecified
        {
            get
            {
                return this.interestRebatePercentFieldSpecified;
            }
            set
            {
                this.interestRebatePercentFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<DLLoanInformationTypePromissoryNotePrintCode> PromissoryNotePrintCode
        {
            get
            {
                return this.promissoryNotePrintCodeField;
            }
            set
            {
                this.promissoryNotePrintCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool PromissoryNotePrintCodeSpecified
        {
            get
            {
                return this.promissoryNotePrintCodeFieldSpecified;
            }
            set
            {
                this.promissoryNotePrintCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<DLLoanInformationTypeDisclosureStatementPrintCode> DisclosureStatementPrintCode
        {
            get
            {
                return this.disclosureStatementPrintCodeField;
            }
            set
            {
                this.disclosureStatementPrintCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisclosureStatementPrintCodeSpecified
        {
            get
            {
                return this.disclosureStatementPrintCodeFieldSpecified;
            }
            set
            {
                this.disclosureStatementPrintCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date", IsNullable = true)]
        public System.Nullable<System.DateTime> AcademicYearBeginDate
        {
            get
            {
                return this.academicYearBeginDateField;
            }
            set
            {
                this.academicYearBeginDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool AcademicYearBeginDateSpecified
        {
            get
            {
                return this.academicYearBeginDateFieldSpecified;
            }
            set
            {
                this.academicYearBeginDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date", IsNullable = true)]
        public System.Nullable<System.DateTime> AcademicYearEndDate
        {
            get
            {
                return this.academicYearEndDateField;
            }
            set
            {
                this.academicYearEndDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool AcademicYearEndDateSpecified
        {
            get
            {
                return this.academicYearEndDateFieldSpecified;
            }
            set
            {
                this.academicYearEndDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ResponseType Response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }
    }

    /// <remarks/>
    [XmlIncludeAttribute(typeof(FFELDocumentResponseType))]
    [XmlIncludeAttribute(typeof(PellAwardResponseType))]
    [XmlIncludeAttribute(typeof(LoanAwardResponseType))]
    [XmlIncludeAttribute(typeof(DLAwardResponseType))]
    [XmlIncludeAttribute(typeof(PLUSAwardResponseType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class ResponseType
    {

        private ResponseCodeType responseCodeField;

        private bool responseCodeFieldSpecified;

        private List<EditProcessResultType> editProcessResultField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ResponseCodeType ResponseCode
        {
            get
            {
                return this.responseCodeField;
            }
            set
            {
                this.responseCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool ResponseCodeSpecified
        {
            get
            {
                return this.responseCodeFieldSpecified;
            }
            set
            {
                this.responseCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("EditProcessResult", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<EditProcessResultType> EditProcessResult
        {
            get
            {
                return this.editProcessResultField;
            }
            set
            {
                this.editProcessResultField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class EditProcessResultType
    {

        private string responseErrorCodeField;

        private string responseMessageField;

        private string responseErrorFieldField;

        private string responseErrorValueField;

        private string reportedValueField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ResponseErrorCode
        {
            get
            {
                return this.responseErrorCodeField;
            }
            set
            {
                this.responseErrorCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ResponseMessage
        {
            get
            {
                return this.responseMessageField;
            }
            set
            {
                this.responseMessageField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ResponseErrorField
        {
            get
            {
                return this.responseErrorFieldField;
            }
            set
            {
                this.responseErrorFieldField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public string ResponseErrorValue
        {
            get
            {
                return this.responseErrorValueField;
            }
            set
            {
                this.responseErrorValueField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ReportedValue
        {
            get
            {
                return this.reportedValueField;
            }
            set
            {
                this.reportedValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class FFELDocumentResponseType : ResponseType
    {

        private DocumentTypeCodeType documentTypeCodeField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DocumentTypeCodeType DocumentTypeCode
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class PellAwardResponseType : ResponseType
    {

        private decimal yTDDisbursementAmountField;

        private bool yTDDisbursementAmountFieldSpecified;

        private decimal totalEligibilityUsedField;

        private bool totalEligibilityUsedFieldSpecified;

        private System.Nullable<decimal> scheduledPellGrantField;

        private bool scheduledPellGrantFieldSpecified;

        private decimal negativePendingAmountField;

        private bool negativePendingAmountFieldSpecified;

        private List<PellAwardResponseTypeFSACode> fSACodeField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal YTDDisbursementAmount
        {
            get
            {
                return this.yTDDisbursementAmountField;
            }
            set
            {
                this.yTDDisbursementAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool YTDDisbursementAmountSpecified
        {
            get
            {
                return this.yTDDisbursementAmountFieldSpecified;
            }
            set
            {
                this.yTDDisbursementAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal TotalEligibilityUsed
        {
            get
            {
                return this.totalEligibilityUsedField;
            }
            set
            {
                this.totalEligibilityUsedField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool TotalEligibilityUsedSpecified
        {
            get
            {
                return this.totalEligibilityUsedFieldSpecified;
            }
            set
            {
                this.totalEligibilityUsedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> ScheduledPellGrant
        {
            get
            {
                return this.scheduledPellGrantField;
            }
            set
            {
                this.scheduledPellGrantField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool ScheduledPellGrantSpecified
        {
            get
            {
                return this.scheduledPellGrantFieldSpecified;
            }
            set
            {
                this.scheduledPellGrantFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal NegativePendingAmount
        {
            get
            {
                return this.negativePendingAmountField;
            }
            set
            {
                this.negativePendingAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool NegativePendingAmountSpecified
        {
            get
            {
                return this.negativePendingAmountFieldSpecified;
            }
            set
            {
                this.negativePendingAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("FSACode", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<PellAwardResponseTypeFSACode> FSACode
        {
            get
            {
                return this.fSACodeField;
            }
            set
            {
                this.fSACodeField = value;
            }
        }
    }

    /// <remarks/>
    [XmlIncludeAttribute(typeof(DLAwardResponseType))]
    [XmlIncludeAttribute(typeof(PLUSAwardResponseType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class LoanAwardResponseType : ResponseType
    {

        private bool eMPNIndicatorField;

        private bool eMPNIndicatorFieldSpecified;

        private string mPNIDField;

        private LoanAwardResponseTypeMPNStatusCode mPNStatusCodeField;

        private bool mPNStatusCodeFieldSpecified;

        private bool mPNLinkIndicatorField;

        private bool mPNLinkIndicatorFieldSpecified;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool EMPNIndicator
        {
            get
            {
                return this.eMPNIndicatorField;
            }
            set
            {
                this.eMPNIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool EMPNIndicatorSpecified
        {
            get
            {
                return this.eMPNIndicatorFieldSpecified;
            }
            set
            {
                this.eMPNIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MPNID
        {
            get
            {
                return this.mPNIDField;
            }
            set
            {
                this.mPNIDField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public LoanAwardResponseTypeMPNStatusCode MPNStatusCode
        {
            get
            {
                return this.mPNStatusCodeField;
            }
            set
            {
                this.mPNStatusCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool MPNStatusCodeSpecified
        {
            get
            {
                return this.mPNStatusCodeFieldSpecified;
            }
            set
            {
                this.mPNStatusCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool MPNLinkIndicator
        {
            get
            {
                return this.mPNLinkIndicatorField;
            }
            set
            {
                this.mPNLinkIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool MPNLinkIndicatorSpecified
        {
            get
            {
                return this.mPNLinkIndicatorFieldSpecified;
            }
            set
            {
                this.mPNLinkIndicatorFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [XmlIncludeAttribute(typeof(PLUSAwardResponseType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class DLAwardResponseType : LoanAwardResponseType
    {

        private System.Nullable<decimal> paymentToServicerAmountField;

        private bool paymentToServicerAmountFieldSpecified;

        private System.DateTime paymentToServicerDateField;

        private bool paymentToServicerDateFieldSpecified;

        private decimal bookedLoanAmountField;

        private bool bookedLoanAmountFieldSpecified;

        private System.DateTime bookedLoanAmountDateField;

        private bool bookedLoanAmountDateFieldSpecified;

        private decimal endorserAmountField;

        private bool endorserAmountFieldSpecified;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> PaymentToServicerAmount
        {
            get
            {
                return this.paymentToServicerAmountField;
            }
            set
            {
                this.paymentToServicerAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool PaymentToServicerAmountSpecified
        {
            get
            {
                return this.paymentToServicerAmountFieldSpecified;
            }
            set
            {
                this.paymentToServicerAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime PaymentToServicerDate
        {
            get
            {
                return this.paymentToServicerDateField;
            }
            set
            {
                this.paymentToServicerDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool PaymentToServicerDateSpecified
        {
            get
            {
                return this.paymentToServicerDateFieldSpecified;
            }
            set
            {
                this.paymentToServicerDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal BookedLoanAmount
        {
            get
            {
                return this.bookedLoanAmountField;
            }
            set
            {
                this.bookedLoanAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool BookedLoanAmountSpecified
        {
            get
            {
                return this.bookedLoanAmountFieldSpecified;
            }
            set
            {
                this.bookedLoanAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime BookedLoanAmountDate
        {
            get
            {
                return this.bookedLoanAmountDateField;
            }
            set
            {
                this.bookedLoanAmountDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool BookedLoanAmountDateSpecified
        {
            get
            {
                return this.bookedLoanAmountDateFieldSpecified;
            }
            set
            {
                this.bookedLoanAmountDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal EndorserAmount
        {
            get
            {
                return this.endorserAmountField;
            }
            set
            {
                this.endorserAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool EndorserAmountSpecified
        {
            get
            {
                return this.endorserAmountFieldSpecified;
            }
            set
            {
                this.endorserAmountFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class PLUSAwardResponseType : DLAwardResponseType
    {

        private PLUSAwardResponseTypeCreditDecisionStatus creditDecisionStatusField;

        private bool creditDecisionStatusFieldSpecified;

        private System.DateTime creditDecisionDateField;

        private bool creditDecisionDateFieldSpecified;

        private PLUSAwardResponseTypeCreditOverrideCode creditOverrideCodeField;

        private bool creditOverrideCodeFieldSpecified;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PLUSAwardResponseTypeCreditDecisionStatus CreditDecisionStatus
        {
            get
            {
                return this.creditDecisionStatusField;
            }
            set
            {
                this.creditDecisionStatusField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool CreditDecisionStatusSpecified
        {
            get
            {
                return this.creditDecisionStatusFieldSpecified;
            }
            set
            {
                this.creditDecisionStatusFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime CreditDecisionDate
        {
            get
            {
                return this.creditDecisionDateField;
            }
            set
            {
                this.creditDecisionDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool CreditDecisionDateSpecified
        {
            get
            {
                return this.creditDecisionDateFieldSpecified;
            }
            set
            {
                this.creditDecisionDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PLUSAwardResponseTypeCreditOverrideCode CreditOverrideCode
        {
            get
            {
                return this.creditOverrideCodeField;
            }
            set
            {
                this.creditOverrideCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool CreditOverrideCodeSpecified
        {
            get
            {
                return this.creditOverrideCodeFieldSpecified;
            }
            set
            {
                this.creditOverrideCodeFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [XmlIncludeAttribute(typeof(DLLoanInformationType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class LoanInformationType
    {

        private StudentLevelCodeType studentLevelCodeField;

        private bool studentLevelCodeFieldSpecified;

        private System.Nullable<System.DateTime> financialAwardBeginDateField;

        private bool financialAwardBeginDateFieldSpecified;

        private System.Nullable<System.DateTime> financialAwardEndDateField;

        private bool financialAwardEndDateFieldSpecified;

        private string loanKeyField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public StudentLevelCodeType StudentLevelCode
        {
            get
            {
                return this.studentLevelCodeField;
            }
            set
            {
                this.studentLevelCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool StudentLevelCodeSpecified
        {
            get
            {
                return this.studentLevelCodeFieldSpecified;
            }
            set
            {
                this.studentLevelCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date", IsNullable = true)]
        public System.Nullable<System.DateTime> FinancialAwardBeginDate
        {
            get
            {
                return this.financialAwardBeginDateField;
            }
            set
            {
                this.financialAwardBeginDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool FinancialAwardBeginDateSpecified
        {
            get
            {
                return this.financialAwardBeginDateFieldSpecified;
            }
            set
            {
                this.financialAwardBeginDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date", IsNullable = true)]
        public System.Nullable<System.DateTime> FinancialAwardEndDate
        {
            get
            {
                return this.financialAwardEndDateField;
            }
            set
            {
                this.financialAwardEndDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool FinancialAwardEndDateSpecified
        {
            get
            {
                return this.financialAwardEndDateFieldSpecified;
            }
            set
            {
                this.financialAwardEndDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "integer")]
        public string LoanKey
        {
            get
            {
                return this.loanKeyField;
            }
            set
            {
                this.loanKeyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    [XmlRootAttribute("Pell", Namespace = "urn:org:pesc:core:CoreMain:v1.16.0", IsNullable = false)]
    public partial class PellType : FinancialAwardType
    {

        private System.Nullable<decimal> attendanceCostField;

        private bool attendanceCostFieldSpecified;

        private System.Nullable<PellTypeAcademicCalendarCode> academicCalendarCodeField;

        private bool academicCalendarCodeFieldSpecified;

        private System.Nullable<PellTypePaymentMethodologyCode> paymentMethodologyCodeField;

        private bool paymentMethodologyCodeFieldSpecified;

        private string weeksUsedCalculateField;

        private string weeksProgramsAcademicYearField;

        private string hoursAwardYearField;

        private string hoursProgramsAcademicYearField;

        private System.Nullable<PellTypeLowTuitionFeesCode> lowTuitionFeesCodeField;

        private bool lowTuitionFeesCodeFieldSpecified;

        private System.Nullable<bool> incarceratedIndicatorField;

        private bool incarceratedIndicatorFieldSpecified;

        private System.Nullable<PellTypeVerificationStatusCode> verificationStatusCodeField;

        private bool verificationStatusCodeFieldSpecified;

        private System.Nullable<System.DateTime> enrollmentDateField;

        private bool enrollmentDateFieldSpecified;

        private System.Nullable<PellTypeSecondaryEFCCode> secondaryEFCCodeField;

        private bool secondaryEFCCodeFieldSpecified;

        private PellAwardResponseType responseField;

        private List<PellTypeDisbursement> disbursementField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> AttendanceCost
        {
            get
            {
                return this.attendanceCostField;
            }
            set
            {
                this.attendanceCostField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool AttendanceCostSpecified
        {
            get
            {
                return this.attendanceCostFieldSpecified;
            }
            set
            {
                this.attendanceCostFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<PellTypeAcademicCalendarCode> AcademicCalendarCode
        {
            get
            {
                return this.academicCalendarCodeField;
            }
            set
            {
                this.academicCalendarCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool AcademicCalendarCodeSpecified
        {
            get
            {
                return this.academicCalendarCodeFieldSpecified;
            }
            set
            {
                this.academicCalendarCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<PellTypePaymentMethodologyCode> PaymentMethodologyCode
        {
            get
            {
                return this.paymentMethodologyCodeField;
            }
            set
            {
                this.paymentMethodologyCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool PaymentMethodologyCodeSpecified
        {
            get
            {
                return this.paymentMethodologyCodeFieldSpecified;
            }
            set
            {
                this.paymentMethodologyCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer", IsNullable = true)]
        public string WeeksUsedCalculate
        {
            get
            {
                return this.weeksUsedCalculateField;
            }
            set
            {
                this.weeksUsedCalculateField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer", IsNullable = true)]
        public string WeeksProgramsAcademicYear
        {
            get
            {
                return this.weeksProgramsAcademicYearField;
            }
            set
            {
                this.weeksProgramsAcademicYearField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer", IsNullable = true)]
        public string HoursAwardYear
        {
            get
            {
                return this.hoursAwardYearField;
            }
            set
            {
                this.hoursAwardYearField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer", IsNullable = true)]
        public string HoursProgramsAcademicYear
        {
            get
            {
                return this.hoursProgramsAcademicYearField;
            }
            set
            {
                this.hoursProgramsAcademicYearField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<PellTypeLowTuitionFeesCode> LowTuitionFeesCode
        {
            get
            {
                return this.lowTuitionFeesCodeField;
            }
            set
            {
                this.lowTuitionFeesCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool LowTuitionFeesCodeSpecified
        {
            get
            {
                return this.lowTuitionFeesCodeFieldSpecified;
            }
            set
            {
                this.lowTuitionFeesCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<bool> IncarceratedIndicator
        {
            get
            {
                return this.incarceratedIndicatorField;
            }
            set
            {
                this.incarceratedIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool IncarceratedIndicatorSpecified
        {
            get
            {
                return this.incarceratedIndicatorFieldSpecified;
            }
            set
            {
                this.incarceratedIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<PellTypeVerificationStatusCode> VerificationStatusCode
        {
            get
            {
                return this.verificationStatusCodeField;
            }
            set
            {
                this.verificationStatusCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool VerificationStatusCodeSpecified
        {
            get
            {
                return this.verificationStatusCodeFieldSpecified;
            }
            set
            {
                this.verificationStatusCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date", IsNullable = true)]
        public System.Nullable<System.DateTime> EnrollmentDate
        {
            get
            {
                return this.enrollmentDateField;
            }
            set
            {
                this.enrollmentDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool EnrollmentDateSpecified
        {
            get
            {
                return this.enrollmentDateFieldSpecified;
            }
            set
            {
                this.enrollmentDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<PellTypeSecondaryEFCCode> SecondaryEFCCode
        {
            get
            {
                return this.secondaryEFCCodeField;
            }
            set
            {
                this.secondaryEFCCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool SecondaryEFCCodeSpecified
        {
            get
            {
                return this.secondaryEFCCodeFieldSpecified;
            }
            set
            {
                this.secondaryEFCCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PellAwardResponseType Response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Disbursement", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<PellTypeDisbursement> Disbursement
        {
            get
            {
                return this.disbursementField;
            }
            set
            {
                this.disbursementField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class PellTypeDisbursement : PellDisbursementType
    {

        private ResponseType responseField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ResponseType Response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class PellDisbursementType : CODDisbursementType
    {

        private System.Nullable<System.DateTime> paymentPeriodStartDateField;

        private bool paymentPeriodStartDateFieldSpecified;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date", IsNullable = true)]
        public System.Nullable<System.DateTime> PaymentPeriodStartDate
        {
            get
            {
                return this.paymentPeriodStartDateField;
            }
            set
            {
                this.paymentPeriodStartDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool PaymentPeriodStartDateSpecified
        {
            get
            {
                return this.paymentPeriodStartDateFieldSpecified;
            }
            set
            {
                this.paymentPeriodStartDateFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [XmlIncludeAttribute(typeof(AlternativeLoanDisbursementType))]
    [XmlIncludeAttribute(typeof(PellDisbursementType))]
    [XmlIncludeAttribute(typeof(DLDisbursementType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class CODDisbursementType : DisbursementType
    {

        private System.Nullable<bool> disbursementReleaseIndicatorField;

        private bool disbursementReleaseIndicatorFieldSpecified;

        private string disbursementSequenceNumberField;

        private string noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<bool> DisbursementReleaseIndicator
        {
            get
            {
                return this.disbursementReleaseIndicatorField;
            }
            set
            {
                this.disbursementReleaseIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementReleaseIndicatorSpecified
        {
            get
            {
                return this.disbursementReleaseIndicatorFieldSpecified;
            }
            set
            {
                this.disbursementReleaseIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer", IsNullable = true)]
        public string DisbursementSequenceNumber
        {
            get
            {
                return this.disbursementSequenceNumberField;
            }
            set
            {
                this.disbursementSequenceNumberField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlIncludeAttribute(typeof(FFELDisbursementType))]
    [XmlIncludeAttribute(typeof(FFELDisbursementResponseType))]
    [XmlIncludeAttribute(typeof(CODDisbursementType))]
    [XmlIncludeAttribute(typeof(AlternativeLoanDisbursementType))]
    [XmlIncludeAttribute(typeof(PellDisbursementType))]
    [XmlIncludeAttribute(typeof(DLDisbursementType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class DisbursementType
    {

        private System.Nullable<decimal> disbursementAmountField;

        private bool disbursementAmountFieldSpecified;

        private System.Nullable<System.DateTime> disbursementDateField;

        private bool disbursementDateFieldSpecified;

        private string numberField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> DisbursementAmount
        {
            get
            {
                return this.disbursementAmountField;
            }
            set
            {
                this.disbursementAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementAmountSpecified
        {
            get
            {
                return this.disbursementAmountFieldSpecified;
            }
            set
            {
                this.disbursementAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date", IsNullable = true)]
        public System.Nullable<System.DateTime> DisbursementDate
        {
            get
            {
                return this.disbursementDateField;
            }
            set
            {
                this.disbursementDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementDateSpecified
        {
            get
            {
                return this.disbursementDateFieldSpecified;
            }
            set
            {
                this.disbursementDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute(DataType = "integer")]
        public string Number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }
    }

    /// <remarks/>
    [XmlIncludeAttribute(typeof(FFELDisbursementResponseType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class FFELDisbursementType : DisbursementType
    {

        private bool disbursementReleaseIndicatorField;

        private bool disbursementReleaseIndicatorFieldSpecified;

        private bool disbursementDayOverrideIndicatorField;

        private bool disbursementDayOverrideIndicatorFieldSpecified;

        private bool disbursementConsummationIndicatorField;

        private bool disbursementConsummationIndicatorFieldSpecified;

        private System.DateTime cancellationDateField;

        private bool cancellationDateFieldSpecified;

        private decimal disbursementReturnedAmountField;

        private bool disbursementReturnedAmountFieldSpecified;

        private FundsDistributionMethodCodeType fundsDistributionMethodCodeField;

        private bool fundsDistributionMethodCodeFieldSpecified;

        private System.DateTime postwithdrawalReturnDateField;

        private bool postwithdrawalReturnDateFieldSpecified;

        private bool postwithdrawalReturnCorrectionIndicatorField;

        private bool postwithdrawalReturnCorrectionIndicatorFieldSpecified;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool DisbursementReleaseIndicator
        {
            get
            {
                return this.disbursementReleaseIndicatorField;
            }
            set
            {
                this.disbursementReleaseIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementReleaseIndicatorSpecified
        {
            get
            {
                return this.disbursementReleaseIndicatorFieldSpecified;
            }
            set
            {
                this.disbursementReleaseIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool DisbursementDayOverrideIndicator
        {
            get
            {
                return this.disbursementDayOverrideIndicatorField;
            }
            set
            {
                this.disbursementDayOverrideIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementDayOverrideIndicatorSpecified
        {
            get
            {
                return this.disbursementDayOverrideIndicatorFieldSpecified;
            }
            set
            {
                this.disbursementDayOverrideIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool DisbursementConsummationIndicator
        {
            get
            {
                return this.disbursementConsummationIndicatorField;
            }
            set
            {
                this.disbursementConsummationIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementConsummationIndicatorSpecified
        {
            get
            {
                return this.disbursementConsummationIndicatorFieldSpecified;
            }
            set
            {
                this.disbursementConsummationIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime CancellationDate
        {
            get
            {
                return this.cancellationDateField;
            }
            set
            {
                this.cancellationDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool CancellationDateSpecified
        {
            get
            {
                return this.cancellationDateFieldSpecified;
            }
            set
            {
                this.cancellationDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal DisbursementReturnedAmount
        {
            get
            {
                return this.disbursementReturnedAmountField;
            }
            set
            {
                this.disbursementReturnedAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementReturnedAmountSpecified
        {
            get
            {
                return this.disbursementReturnedAmountFieldSpecified;
            }
            set
            {
                this.disbursementReturnedAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public FundsDistributionMethodCodeType FundsDistributionMethodCode
        {
            get
            {
                return this.fundsDistributionMethodCodeField;
            }
            set
            {
                this.fundsDistributionMethodCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool FundsDistributionMethodCodeSpecified
        {
            get
            {
                return this.fundsDistributionMethodCodeFieldSpecified;
            }
            set
            {
                this.fundsDistributionMethodCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime PostwithdrawalReturnDate
        {
            get
            {
                return this.postwithdrawalReturnDateField;
            }
            set
            {
                this.postwithdrawalReturnDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool PostwithdrawalReturnDateSpecified
        {
            get
            {
                return this.postwithdrawalReturnDateFieldSpecified;
            }
            set
            {
                this.postwithdrawalReturnDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool PostwithdrawalReturnCorrectionIndicator
        {
            get
            {
                return this.postwithdrawalReturnCorrectionIndicatorField;
            }
            set
            {
                this.postwithdrawalReturnCorrectionIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool PostwithdrawalReturnCorrectionIndicatorSpecified
        {
            get
            {
                return this.postwithdrawalReturnCorrectionIndicatorFieldSpecified;
            }
            set
            {
                this.postwithdrawalReturnCorrectionIndicatorFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class FFELDisbursementResponseType : FFELDisbursementType
    {

        private FFELDisbursementResponseTypeResponse responseField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public FFELDisbursementResponseTypeResponse Response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class FFELDisbursementResponseTypeResponse : ResponseType
    {

        private System.Nullable<decimal> disbursementNetAmountField;

        private bool disbursementNetAmountFieldSpecified;

        private DisbursementStatusCodeType disbursementStatusCodeField;

        private bool disbursementStatusCodeFieldSpecified;

        private System.Nullable<decimal> disbursementFeeAmountField;

        private bool disbursementFeeAmountFieldSpecified;

        private decimal disbursementFeePaidField;

        private bool disbursementFeePaidFieldSpecified;

        private decimal guaranteeFeeAmountField;

        private bool guaranteeFeeAmountFieldSpecified;

        private decimal guaranteeFeePaidField;

        private bool guaranteeFeePaidFieldSpecified;

        private bool disbursementReleaseIndicatorField;

        private bool disbursementReleaseIndicatorFieldSpecified;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> DisbursementNetAmount
        {
            get
            {
                return this.disbursementNetAmountField;
            }
            set
            {
                this.disbursementNetAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementNetAmountSpecified
        {
            get
            {
                return this.disbursementNetAmountFieldSpecified;
            }
            set
            {
                this.disbursementNetAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DisbursementStatusCodeType DisbursementStatusCode
        {
            get
            {
                return this.disbursementStatusCodeField;
            }
            set
            {
                this.disbursementStatusCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementStatusCodeSpecified
        {
            get
            {
                return this.disbursementStatusCodeFieldSpecified;
            }
            set
            {
                this.disbursementStatusCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> DisbursementFeeAmount
        {
            get
            {
                return this.disbursementFeeAmountField;
            }
            set
            {
                this.disbursementFeeAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementFeeAmountSpecified
        {
            get
            {
                return this.disbursementFeeAmountFieldSpecified;
            }
            set
            {
                this.disbursementFeeAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal DisbursementFeePaid
        {
            get
            {
                return this.disbursementFeePaidField;
            }
            set
            {
                this.disbursementFeePaidField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementFeePaidSpecified
        {
            get
            {
                return this.disbursementFeePaidFieldSpecified;
            }
            set
            {
                this.disbursementFeePaidFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal GuaranteeFeeAmount
        {
            get
            {
                return this.guaranteeFeeAmountField;
            }
            set
            {
                this.guaranteeFeeAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool GuaranteeFeeAmountSpecified
        {
            get
            {
                return this.guaranteeFeeAmountFieldSpecified;
            }
            set
            {
                this.guaranteeFeeAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal GuaranteeFeePaid
        {
            get
            {
                return this.guaranteeFeePaidField;
            }
            set
            {
                this.guaranteeFeePaidField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool GuaranteeFeePaidSpecified
        {
            get
            {
                return this.guaranteeFeePaidFieldSpecified;
            }
            set
            {
                this.guaranteeFeePaidFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool DisbursementReleaseIndicator
        {
            get
            {
                return this.disbursementReleaseIndicatorField;
            }
            set
            {
                this.disbursementReleaseIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementReleaseIndicatorSpecified
        {
            get
            {
                return this.disbursementReleaseIndicatorFieldSpecified;
            }
            set
            {
                this.disbursementReleaseIndicatorFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class AlternativeLoanDisbursementType : CODDisbursementType
    {

        private System.Nullable<bool> firstDisbursementIndicatorField;

        private bool firstDisbursementIndicatorFieldSpecified;

        private System.Nullable<decimal> disbursementNetAmountField;

        private bool disbursementNetAmountFieldSpecified;

        private System.Nullable<decimal> disbursementFeeAmountField;

        private bool disbursementFeeAmountFieldSpecified;

        private System.Nullable<decimal> interestRebateAmountField;

        private bool interestRebateAmountFieldSpecified;

        private System.Nullable<bool> disbursementConfirmationIndicatorField;

        private bool disbursementConfirmationIndicatorFieldSpecified;

        private bool disbursementConsummationIndicatorField;

        private bool disbursementConsummationIndicatorFieldSpecified;

        private decimal disbursementReturnedAmountField;

        private bool disbursementReturnedAmountFieldSpecified;

        private decimal guaranteeFeeAmountField;

        private bool guaranteeFeeAmountFieldSpecified;

        private System.DateTime postwithdrawalReturnDateField;

        private bool postwithdrawalReturnDateFieldSpecified;

        private decimal previousPostwithdrawalReturnAmountField;

        private bool previousPostwithdrawalReturnAmountFieldSpecified;

        private bool postwithdrawalReturnCorrectionIndicatorField;

        private bool postwithdrawalReturnCorrectionIndicatorFieldSpecified;

        private FundsDistributionMethodCodeType fundsDistributionMethodCodeField;

        private bool fundsDistributionMethodCodeFieldSpecified;

        private string checkNumberField;

        private bool lateDisbursementindicatorField;

        private bool lateDisbursementindicatorFieldSpecified;

        private decimal disbursementFeePaidField;

        private bool disbursementFeePaidFieldSpecified;

        private bool previouslyReportedIndicatorField;

        private bool previouslyReportedIndicatorFieldSpecified;

        private decimal outstandingCancellationAmountField;

        private bool outstandingCancellationAmountFieldSpecified;

        private System.DateTime cancellationDateField;

        private bool cancellationDateFieldSpecified;

        private bool disbursementDayOverrideIndicatorField;

        private bool disbursementDayOverrideIndicatorFieldSpecified;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<bool> FirstDisbursementIndicator
        {
            get
            {
                return this.firstDisbursementIndicatorField;
            }
            set
            {
                this.firstDisbursementIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool FirstDisbursementIndicatorSpecified
        {
            get
            {
                return this.firstDisbursementIndicatorFieldSpecified;
            }
            set
            {
                this.firstDisbursementIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> DisbursementNetAmount
        {
            get
            {
                return this.disbursementNetAmountField;
            }
            set
            {
                this.disbursementNetAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementNetAmountSpecified
        {
            get
            {
                return this.disbursementNetAmountFieldSpecified;
            }
            set
            {
                this.disbursementNetAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> DisbursementFeeAmount
        {
            get
            {
                return this.disbursementFeeAmountField;
            }
            set
            {
                this.disbursementFeeAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementFeeAmountSpecified
        {
            get
            {
                return this.disbursementFeeAmountFieldSpecified;
            }
            set
            {
                this.disbursementFeeAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> InterestRebateAmount
        {
            get
            {
                return this.interestRebateAmountField;
            }
            set
            {
                this.interestRebateAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool InterestRebateAmountSpecified
        {
            get
            {
                return this.interestRebateAmountFieldSpecified;
            }
            set
            {
                this.interestRebateAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<bool> DisbursementConfirmationIndicator
        {
            get
            {
                return this.disbursementConfirmationIndicatorField;
            }
            set
            {
                this.disbursementConfirmationIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementConfirmationIndicatorSpecified
        {
            get
            {
                return this.disbursementConfirmationIndicatorFieldSpecified;
            }
            set
            {
                this.disbursementConfirmationIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool DisbursementConsummationIndicator
        {
            get
            {
                return this.disbursementConsummationIndicatorField;
            }
            set
            {
                this.disbursementConsummationIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementConsummationIndicatorSpecified
        {
            get
            {
                return this.disbursementConsummationIndicatorFieldSpecified;
            }
            set
            {
                this.disbursementConsummationIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal DisbursementReturnedAmount
        {
            get
            {
                return this.disbursementReturnedAmountField;
            }
            set
            {
                this.disbursementReturnedAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementReturnedAmountSpecified
        {
            get
            {
                return this.disbursementReturnedAmountFieldSpecified;
            }
            set
            {
                this.disbursementReturnedAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal GuaranteeFeeAmount
        {
            get
            {
                return this.guaranteeFeeAmountField;
            }
            set
            {
                this.guaranteeFeeAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool GuaranteeFeeAmountSpecified
        {
            get
            {
                return this.guaranteeFeeAmountFieldSpecified;
            }
            set
            {
                this.guaranteeFeeAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime PostwithdrawalReturnDate
        {
            get
            {
                return this.postwithdrawalReturnDateField;
            }
            set
            {
                this.postwithdrawalReturnDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool PostwithdrawalReturnDateSpecified
        {
            get
            {
                return this.postwithdrawalReturnDateFieldSpecified;
            }
            set
            {
                this.postwithdrawalReturnDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal PreviousPostwithdrawalReturnAmount
        {
            get
            {
                return this.previousPostwithdrawalReturnAmountField;
            }
            set
            {
                this.previousPostwithdrawalReturnAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool PreviousPostwithdrawalReturnAmountSpecified
        {
            get
            {
                return this.previousPostwithdrawalReturnAmountFieldSpecified;
            }
            set
            {
                this.previousPostwithdrawalReturnAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool PostwithdrawalReturnCorrectionIndicator
        {
            get
            {
                return this.postwithdrawalReturnCorrectionIndicatorField;
            }
            set
            {
                this.postwithdrawalReturnCorrectionIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool PostwithdrawalReturnCorrectionIndicatorSpecified
        {
            get
            {
                return this.postwithdrawalReturnCorrectionIndicatorFieldSpecified;
            }
            set
            {
                this.postwithdrawalReturnCorrectionIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public FundsDistributionMethodCodeType FundsDistributionMethodCode
        {
            get
            {
                return this.fundsDistributionMethodCodeField;
            }
            set
            {
                this.fundsDistributionMethodCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool FundsDistributionMethodCodeSpecified
        {
            get
            {
                return this.fundsDistributionMethodCodeFieldSpecified;
            }
            set
            {
                this.fundsDistributionMethodCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CheckNumber
        {
            get
            {
                return this.checkNumberField;
            }
            set
            {
                this.checkNumberField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool LateDisbursementindicator
        {
            get
            {
                return this.lateDisbursementindicatorField;
            }
            set
            {
                this.lateDisbursementindicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool LateDisbursementindicatorSpecified
        {
            get
            {
                return this.lateDisbursementindicatorFieldSpecified;
            }
            set
            {
                this.lateDisbursementindicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal DisbursementFeePaid
        {
            get
            {
                return this.disbursementFeePaidField;
            }
            set
            {
                this.disbursementFeePaidField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementFeePaidSpecified
        {
            get
            {
                return this.disbursementFeePaidFieldSpecified;
            }
            set
            {
                this.disbursementFeePaidFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool PreviouslyReportedIndicator
        {
            get
            {
                return this.previouslyReportedIndicatorField;
            }
            set
            {
                this.previouslyReportedIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool PreviouslyReportedIndicatorSpecified
        {
            get
            {
                return this.previouslyReportedIndicatorFieldSpecified;
            }
            set
            {
                this.previouslyReportedIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal OutstandingCancellationAmount
        {
            get
            {
                return this.outstandingCancellationAmountField;
            }
            set
            {
                this.outstandingCancellationAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool OutstandingCancellationAmountSpecified
        {
            get
            {
                return this.outstandingCancellationAmountFieldSpecified;
            }
            set
            {
                this.outstandingCancellationAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime CancellationDate
        {
            get
            {
                return this.cancellationDateField;
            }
            set
            {
                this.cancellationDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool CancellationDateSpecified
        {
            get
            {
                return this.cancellationDateFieldSpecified;
            }
            set
            {
                this.cancellationDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool DisbursementDayOverrideIndicator
        {
            get
            {
                return this.disbursementDayOverrideIndicatorField;
            }
            set
            {
                this.disbursementDayOverrideIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementDayOverrideIndicatorSpecified
        {
            get
            {
                return this.disbursementDayOverrideIndicatorFieldSpecified;
            }
            set
            {
                this.disbursementDayOverrideIndicatorFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class DLDisbursementType : CODDisbursementType
    {

        private System.Nullable<bool> firstDisbursementIndicatorField;

        private bool firstDisbursementIndicatorFieldSpecified;

        private System.Nullable<decimal> disbursementNetAmountField;

        private bool disbursementNetAmountFieldSpecified;

        private System.Nullable<decimal> disbursementFeeAmountField;

        private bool disbursementFeeAmountFieldSpecified;

        private System.Nullable<decimal> interestRebateAmountField;

        private bool interestRebateAmountFieldSpecified;

        private System.Nullable<bool> disbursementConfirmationIndicatorField;

        private bool disbursementConfirmationIndicatorFieldSpecified;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<bool> FirstDisbursementIndicator
        {
            get
            {
                return this.firstDisbursementIndicatorField;
            }
            set
            {
                this.firstDisbursementIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool FirstDisbursementIndicatorSpecified
        {
            get
            {
                return this.firstDisbursementIndicatorFieldSpecified;
            }
            set
            {
                this.firstDisbursementIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> DisbursementNetAmount
        {
            get
            {
                return this.disbursementNetAmountField;
            }
            set
            {
                this.disbursementNetAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementNetAmountSpecified
        {
            get
            {
                return this.disbursementNetAmountFieldSpecified;
            }
            set
            {
                this.disbursementNetAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> DisbursementFeeAmount
        {
            get
            {
                return this.disbursementFeeAmountField;
            }
            set
            {
                this.disbursementFeeAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementFeeAmountSpecified
        {
            get
            {
                return this.disbursementFeeAmountFieldSpecified;
            }
            set
            {
                this.disbursementFeeAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> InterestRebateAmount
        {
            get
            {
                return this.interestRebateAmountField;
            }
            set
            {
                this.interestRebateAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool InterestRebateAmountSpecified
        {
            get
            {
                return this.interestRebateAmountFieldSpecified;
            }
            set
            {
                this.interestRebateAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<bool> DisbursementConfirmationIndicator
        {
            get
            {
                return this.disbursementConfirmationIndicatorField;
            }
            set
            {
                this.disbursementConfirmationIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DisbursementConfirmationIndicatorSpecified
        {
            get
            {
                return this.disbursementConfirmationIndicatorFieldSpecified;
            }
            set
            {
                this.disbursementConfirmationIndicatorFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [XmlIncludeAttribute(typeof(PellType))]
    [XmlIncludeAttribute(typeof(LoanAwardType))]
    [XmlIncludeAttribute(typeof(StaffordAwardType))]
    [XmlIncludeAttribute(typeof(AlternativeLoanAwardType))]
    [XmlIncludeAttribute(typeof(CODAlternativeLoanType))]
    [XmlIncludeAttribute(typeof(UnsubsidizedAwardType))]
    [XmlIncludeAttribute(typeof(DLUnsubsidizedType))]
    [XmlIncludeAttribute(typeof(SubsidizedAwardType))]
    [XmlIncludeAttribute(typeof(DLSubsidizedType))]
    [XmlIncludeAttribute(typeof(PLUSAwardType))]
    [XmlIncludeAttribute(typeof(DLPLUSType))]
    [XmlIncludeAttribute(typeof(CampusBasedAwardType))]
    [XmlIncludeAttribute(typeof(PerkinsType))]
    [XmlIncludeAttribute(typeof(FWSPType))]
    [XmlIncludeAttribute(typeof(SEOGType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class FinancialAwardType
    {

        private string loanKeyField;

        private string financialAwardYearField;

        private string cPSTransactionNumberField;

        private System.Nullable<decimal> financialAwardAmountField;

        private bool financialAwardAmountFieldSpecified;

        private string noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
        public string LoanKey
        {
            get
            {
                return this.loanKeyField;
            }
            set
            {
                this.loanKeyField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "gYear")]
        public string FinancialAwardYear
        {
            get
            {
                return this.financialAwardYearField;
            }
            set
            {
                this.financialAwardYearField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
        public string CPSTransactionNumber
        {
            get
            {
                return this.cPSTransactionNumberField;
            }
            set
            {
                this.cPSTransactionNumberField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> FinancialAwardAmount
        {
            get
            {
                return this.financialAwardAmountField;
            }
            set
            {
                this.financialAwardAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool FinancialAwardAmountSpecified
        {
            get
            {
                return this.financialAwardAmountFieldSpecified;
            }
            set
            {
                this.financialAwardAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlIncludeAttribute(typeof(StaffordAwardType))]
    [XmlIncludeAttribute(typeof(AlternativeLoanAwardType))]
    [XmlIncludeAttribute(typeof(CODAlternativeLoanType))]
    [XmlIncludeAttribute(typeof(UnsubsidizedAwardType))]
    [XmlIncludeAttribute(typeof(DLUnsubsidizedType))]
    [XmlIncludeAttribute(typeof(SubsidizedAwardType))]
    [XmlIncludeAttribute(typeof(DLSubsidizedType))]
    [XmlIncludeAttribute(typeof(PLUSAwardType))]
    [XmlIncludeAttribute(typeof(DLPLUSType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class LoanAwardType : FinancialAwardType
    {

        private System.Nullable<LoanAwardTypeDependencyStatusCode> dependencyStatusCodeField;

        private bool dependencyStatusCodeFieldSpecified;

        private DefaultOverpayCodeType defaultOverpayCodeField;

        private bool defaultOverpayCodeFieldSpecified;

        private string financialAwardNumberField;

        private string financialAwardIDField;

        private System.Nullable<System.DateTime> financialAwardCreateDateField;

        private bool financialAwardCreateDateFieldSpecified;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<LoanAwardTypeDependencyStatusCode> DependencyStatusCode
        {
            get
            {
                return this.dependencyStatusCodeField;
            }
            set
            {
                this.dependencyStatusCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DependencyStatusCodeSpecified
        {
            get
            {
                return this.dependencyStatusCodeFieldSpecified;
            }
            set
            {
                this.dependencyStatusCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DefaultOverpayCodeType DefaultOverpayCode
        {
            get
            {
                return this.defaultOverpayCodeField;
            }
            set
            {
                this.defaultOverpayCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DefaultOverpayCodeSpecified
        {
            get
            {
                return this.defaultOverpayCodeFieldSpecified;
            }
            set
            {
                this.defaultOverpayCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
        public string FinancialAwardNumber
        {
            get
            {
                return this.financialAwardNumberField;
            }
            set
            {
                this.financialAwardNumberField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FinancialAwardID
        {
            get
            {
                return this.financialAwardIDField;
            }
            set
            {
                this.financialAwardIDField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date", IsNullable = true)]
        public System.Nullable<System.DateTime> FinancialAwardCreateDate
        {
            get
            {
                return this.financialAwardCreateDateField;
            }
            set
            {
                this.financialAwardCreateDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool FinancialAwardCreateDateSpecified
        {
            get
            {
                return this.financialAwardCreateDateFieldSpecified;
            }
            set
            {
                this.financialAwardCreateDateFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [XmlIncludeAttribute(typeof(AlternativeLoanAwardType))]
    [XmlIncludeAttribute(typeof(CODAlternativeLoanType))]
    [XmlIncludeAttribute(typeof(UnsubsidizedAwardType))]
    [XmlIncludeAttribute(typeof(DLUnsubsidizedType))]
    [XmlIncludeAttribute(typeof(SubsidizedAwardType))]
    [XmlIncludeAttribute(typeof(DLSubsidizedType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class StaffordAwardType : LoanAwardType
    {
    }

    /// <remarks/>
    [XmlIncludeAttribute(typeof(CODAlternativeLoanType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class AlternativeLoanAwardType : StaffordAwardType
    {

        private DLAwardResponseType responseField;

        private List<AlternativeLoanDisbursementType> disbursementField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DLAwardResponseType Response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Disbursement", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AlternativeLoanDisbursementType> Disbursement
        {
            get
            {
                return this.disbursementField;
            }
            set
            {
                this.disbursementField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class CODAlternativeLoanType : AlternativeLoanAwardType
    {

        private string alternativeLoanProgramCodeField;

        private InterestRateOptionType interestRateOptionField;

        private bool interestRateOptionFieldSpecified;

        private RepaymentOptionCodeType repaymentOptionCodeField;

        private bool repaymentOptionCodeFieldSpecified;

        private string alternativeLoanApplicationVersionField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AlternativeLoanProgramCode
        {
            get
            {
                return this.alternativeLoanProgramCodeField;
            }
            set
            {
                this.alternativeLoanProgramCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public InterestRateOptionType InterestRateOption
        {
            get
            {
                return this.interestRateOptionField;
            }
            set
            {
                this.interestRateOptionField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool InterestRateOptionSpecified
        {
            get
            {
                return this.interestRateOptionFieldSpecified;
            }
            set
            {
                this.interestRateOptionFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RepaymentOptionCodeType RepaymentOptionCode
        {
            get
            {
                return this.repaymentOptionCodeField;
            }
            set
            {
                this.repaymentOptionCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool RepaymentOptionCodeSpecified
        {
            get
            {
                return this.repaymentOptionCodeFieldSpecified;
            }
            set
            {
                this.repaymentOptionCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AlternativeLoanApplicationVersion
        {
            get
            {
                return this.alternativeLoanApplicationVersionField;
            }
            set
            {
                this.alternativeLoanApplicationVersionField = value;
            }
        }
    }

    /// <remarks/>
    [XmlIncludeAttribute(typeof(DLUnsubsidizedType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class UnsubsidizedAwardType : StaffordAwardType
    {

        private System.Nullable<bool> hPPAIndicatorField;

        private bool hPPAIndicatorFieldSpecified;

        private DLAwardResponseType responseField;

        private List<UnsubsidizedAwardTypeDisbursement> disbursementField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<bool> HPPAIndicator
        {
            get
            {
                return this.hPPAIndicatorField;
            }
            set
            {
                this.hPPAIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool HPPAIndicatorSpecified
        {
            get
            {
                return this.hPPAIndicatorFieldSpecified;
            }
            set
            {
                this.hPPAIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DLAwardResponseType Response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Disbursement", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<UnsubsidizedAwardTypeDisbursement> Disbursement
        {
            get
            {
                return this.disbursementField;
            }
            set
            {
                this.disbursementField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class UnsubsidizedAwardTypeDisbursement : DLDisbursementType
    {

        private ResponseType responseField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ResponseType Response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }
    }

    /// <remarks/>
    [XmlIncludeAttribute(typeof(DLSubsidizedType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class SubsidizedAwardType : StaffordAwardType
    {

        private DLAwardResponseType responseField;

        private List<SubsidizedAwardTypeDisbursement> disbursementField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DLAwardResponseType Response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Disbursement", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<SubsidizedAwardTypeDisbursement> Disbursement
        {
            get
            {
                return this.disbursementField;
            }
            set
            {
                this.disbursementField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class SubsidizedAwardTypeDisbursement : DLDisbursementType
    {

        private ResponseType responseField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ResponseType Response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }
    }

    /// <remarks/>
    [XmlIncludeAttribute(typeof(DLPLUSType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class PLUSAwardType : LoanAwardType
    {
        private System.Nullable<decimal> financialAwardAmountRequestedField;
        private bool financialAwardAmountRequestedFieldSpecified;
        private BorrowerType borrowerField;
        private PLUSAwardResponseType responseField;
        private List<PLUSAwardTypeDisbursement> disbursementField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> FinancialAwardAmountRequested
        {
            get
            {
                return this.financialAwardAmountRequestedField;
            }
            set
            {
                this.financialAwardAmountRequestedField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool FinancialAwardAmountRequestedSpecified
        {
            get
            {
                return this.financialAwardAmountRequestedFieldSpecified;
            }
            set
            {
                this.financialAwardAmountRequestedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public BorrowerType Borrower
        {
            get
            {
                return this.borrowerField;
            }
            set
            {
                this.borrowerField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PLUSAwardResponseType Response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Disbursement", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<PLUSAwardTypeDisbursement> Disbursement
        {
            get
            {
                return this.disbursementField;
            }
            set
            {
                this.disbursementField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class BorrowerType
    {

        private List<PersonType1> personField;

        private List<EmploymentType> employmentField;

        /// <remarks/>
        [XmlElementAttribute("Person", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<PersonType1> Person
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
        [XmlElementAttribute("Employment", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<EmploymentType> Employment
        {
            get
            {
                return this.employmentField;
            }
            set
            {
                this.employmentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(TypeName = "PersonType", Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class PersonType1
    {

        private List<BirthType> birthField;
        private List<NameType> nameField;
        private List<ContactsType> contactsField;
        private GenderType genderField;
        private List<RaceEthnicityType> raceEthnicityField;
        private List<CitizenshipType> citizenshipField;
        private ResidencyType residencyField;
        private DeceasedType deceasedField;
        private List<EducationLevelType> educationLevelField;
        private List<ImmigrationType> immigrationField;
        private List<string> occupationField;
        private List<string> religiousAffiliationField;
        private List<PrivacyRestrictionType> privacyRestrictionField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute("Birth", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<BirthType> Birth
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
        [XmlElementAttribute("Name", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<NameType> Name
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
        [XmlElementAttribute("Contacts", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GenderType Gender
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
        [XmlElementAttribute("RaceEthnicity", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<RaceEthnicityType> RaceEthnicity
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
        [XmlElementAttribute("Citizenship", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CitizenshipType> Citizenship
        {
            get
            {
                return this.citizenshipField;
            }
            set
            {
                this.citizenshipField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ResidencyType Residency
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DeceasedType Deceased
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
        [XmlElementAttribute("EducationLevel", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<EducationLevelType> EducationLevel
        {
            get
            {
                return this.educationLevelField;
            }
            set
            {
                this.educationLevelField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Immigration", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<ImmigrationType> Immigration
        {
            get
            {
                return this.immigrationField;
            }
            set
            {
                this.immigrationField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Occupation", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> Occupation
        {
            get
            {
                return this.occupationField;
            }
            set
            {
                this.occupationField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("ReligiousAffiliation", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> ReligiousAffiliation
        {
            get
            {
                return this.religiousAffiliationField;
            }
            set
            {
                this.religiousAffiliationField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("PrivacyRestriction", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<PrivacyRestrictionType> PrivacyRestriction
        {
            get
            {
                return this.privacyRestrictionField;
            }
            set
            {
                this.privacyRestrictionField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(TypeName = "ContactsType", Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class ContactsType1
    {

        private List<AddressType> addressField;
        private List<EmailType> emailField;
        private List<PhoneType> phoneField;
        private List<URLType> uRLField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute("Address", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AddressType> Address
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
        [XmlElementAttribute("Email", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<EmailType> Email
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
        [XmlElementAttribute("Phone", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<PhoneType> Phone
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
        [XmlElementAttribute("URL", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<URLType> URL
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
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class RaceEthnicityType
    {

        private RaceEthnicityCodeType raceEthnicityCodeField;
        private bool raceEthnicityCodeFieldSpecified;
        private List<string> localRaceEthnicityCodeField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RaceEthnicityCodeType RaceEthnicityCode
        {
            get
            {
                return this.raceEthnicityCodeField;
            }
            set
            {
                this.raceEthnicityCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool RaceEthnicityCodeSpecified
        {
            get
            {
                return this.raceEthnicityCodeFieldSpecified;
            }
            set
            {
                this.raceEthnicityCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("LocalRaceEthnicityCode", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> LocalRaceEthnicityCode
        {
            get
            {
                return this.localRaceEthnicityCodeField;
            }
            set
            {
                this.localRaceEthnicityCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    //[XmlIncludeAttribute(typeof(AcademicRecord.CitizenshipType1))]
    //[XmlIncludeAttribute(typeof(AcademicRecord.CitizenshipType2))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class CitizenshipType
    {

        private CitizenshipStatusCodeType citizenshipStatusCodeField;
        private bool citizenshipStatusCodeFieldSpecified;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CitizenshipStatusCodeType CitizenshipStatusCode
        {
            get
            {
                return this.citizenshipStatusCodeField;
            }
            set
            {
                this.citizenshipStatusCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool CitizenshipStatusCodeSpecified
        {
            get
            {
                return this.citizenshipStatusCodeFieldSpecified;
            }
            set
            {
                this.citizenshipStatusCodeFieldSpecified = value;
            }
        }

        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class ImmigrationType
    {

        private string alienRegistrationNumberField;
        private System.DateTime firstEntryIntoUSDateField;
        private bool firstEntryIntoUSDateFieldSpecified;
        private bool immigrationI20RequestIndicatorField;
        private bool immigrationI20RequestIndicatorFieldSpecified;
        private System.DateTime nonImmigrantVisaIssueDateField;
        private bool nonImmigrantVisaIssueDateFieldSpecified;
        private string nonImmigrantVisaNumberField;
        private NonImmigrantVisaStatusChangeCodeType nonImmigrantVisaStatusChangeCodeField;
        private bool nonImmigrantVisaStatusChangeCodeFieldSpecified;
        private System.DateTime nonImmigrantVisaStatusChangeDateField;
        private bool nonImmigrantVisaStatusChangeDateFieldSpecified;
        private string nonImmigrantVisaTypeField;
        private System.DateTime requiredFormsReceiveDateField;
        private bool requiredFormsReceiveDateFieldSpecified;
        private SponsorType sponsorField;
        private USStudyFormsReceiptType uSStudyFormsReceiptField;
        private bool uSStudyFormsReceiptFieldSpecified;
        private System.DateTime visaExpirationDateField;
        private bool visaExpirationDateFieldSpecified;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AlienRegistrationNumber
        {
            get
            {
                return this.alienRegistrationNumberField;
            }
            set
            {
                this.alienRegistrationNumberField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime FirstEntryIntoUSDate
        {
            get
            {
                return this.firstEntryIntoUSDateField;
            }
            set
            {
                this.firstEntryIntoUSDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool FirstEntryIntoUSDateSpecified
        {
            get
            {
                return this.firstEntryIntoUSDateFieldSpecified;
            }
            set
            {
                this.firstEntryIntoUSDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool ImmigrationI20RequestIndicator
        {
            get
            {
                return this.immigrationI20RequestIndicatorField;
            }
            set
            {
                this.immigrationI20RequestIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool ImmigrationI20RequestIndicatorSpecified
        {
            get
            {
                return this.immigrationI20RequestIndicatorFieldSpecified;
            }
            set
            {
                this.immigrationI20RequestIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime NonImmigrantVisaIssueDate
        {
            get
            {
                return this.nonImmigrantVisaIssueDateField;
            }
            set
            {
                this.nonImmigrantVisaIssueDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool NonImmigrantVisaIssueDateSpecified
        {
            get
            {
                return this.nonImmigrantVisaIssueDateFieldSpecified;
            }
            set
            {
                this.nonImmigrantVisaIssueDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NonImmigrantVisaNumber
        {
            get
            {
                return this.nonImmigrantVisaNumberField;
            }
            set
            {
                this.nonImmigrantVisaNumberField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public NonImmigrantVisaStatusChangeCodeType NonImmigrantVisaStatusChangeCode
        {
            get
            {
                return this.nonImmigrantVisaStatusChangeCodeField;
            }
            set
            {
                this.nonImmigrantVisaStatusChangeCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool NonImmigrantVisaStatusChangeCodeSpecified
        {
            get
            {
                return this.nonImmigrantVisaStatusChangeCodeFieldSpecified;
            }
            set
            {
                this.nonImmigrantVisaStatusChangeCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime NonImmigrantVisaStatusChangeDate
        {
            get
            {
                return this.nonImmigrantVisaStatusChangeDateField;
            }
            set
            {
                this.nonImmigrantVisaStatusChangeDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool NonImmigrantVisaStatusChangeDateSpecified
        {
            get
            {
                return this.nonImmigrantVisaStatusChangeDateFieldSpecified;
            }
            set
            {
                this.nonImmigrantVisaStatusChangeDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NonImmigrantVisaType
        {
            get
            {
                return this.nonImmigrantVisaTypeField;
            }
            set
            {
                this.nonImmigrantVisaTypeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime RequiredFormsReceiveDate
        {
            get
            {
                return this.requiredFormsReceiveDateField;
            }
            set
            {
                this.requiredFormsReceiveDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool RequiredFormsReceiveDateSpecified
        {
            get
            {
                return this.requiredFormsReceiveDateFieldSpecified;
            }
            set
            {
                this.requiredFormsReceiveDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public SponsorType Sponsor
        {
            get
            {
                return this.sponsorField;
            }
            set
            {
                this.sponsorField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public USStudyFormsReceiptType USStudyFormsReceipt
        {
            get
            {
                return this.uSStudyFormsReceiptField;
            }
            set
            {
                this.uSStudyFormsReceiptField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool USStudyFormsReceiptSpecified
        {
            get
            {
                return this.uSStudyFormsReceiptFieldSpecified;
            }
            set
            {
                this.uSStudyFormsReceiptFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime VisaExpirationDate
        {
            get
            {
                return this.visaExpirationDateField;
            }
            set
            {
                this.visaExpirationDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool VisaExpirationDateSpecified
        {
            get
            {
                return this.visaExpirationDateFieldSpecified;
            }
            set
            {
                this.visaExpirationDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class SponsorType
    {

        private OrganizationType1 sponsorOrganizationField;
        private PersonType1 sponsorPersonField;
        private SponsorTypeType sponsorType1Field;
        private bool sponsorType1FieldSpecified;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public OrganizationType1 SponsorOrganization
        {
            get
            {
                return this.sponsorOrganizationField;
            }
            set
            {
                this.sponsorOrganizationField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PersonType1 SponsorPerson
        {
            get
            {
                return this.sponsorPersonField;
            }
            set
            {
                this.sponsorPersonField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("SponsorType", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public SponsorTypeType SponsorType1
        {
            get
            {
                return this.sponsorType1Field;
            }
            set
            {
                this.sponsorType1Field = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool SponsorType1Specified
        {
            get
            {
                return this.sponsorType1FieldSpecified;
            }
            set
            {
                this.sponsorType1FieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlIncludeAttribute(typeof(EmployerType))]
    [XmlIncludeAttribute(typeof(BorrowerpreferredLenderType))]
    [XmlIncludeAttribute(typeof(LenderType))]
    [XmlIncludeAttribute(typeof(GuarantorType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(TypeName = "OrganizationType", Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class OrganizationType1
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
        private List<string> organizationNameField;
        private List<ContactsType> contactsField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute("OrganizationName", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute("Contacts", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class EmployerType : OrganizationType1
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class BorrowerpreferredLenderType : OrganizationType1
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class LenderType : OrganizationType1
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class GuarantorType : OrganizationType1
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class PrivacyRestrictionType
    {

        private System.DateTime privacyRestrictionDateField;
        private bool privacyRestrictionDateFieldSpecified;
        private PrivacyRestrictionLevelType privacyRestrictionLevelField;
        private bool privacyRestrictionLevelFieldSpecified;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime PrivacyRestrictionDate
        {
            get
            {
                return this.privacyRestrictionDateField;
            }
            set
            {
                this.privacyRestrictionDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool PrivacyRestrictionDateSpecified
        {
            get
            {
                return this.privacyRestrictionDateFieldSpecified;
            }
            set
            {
                this.privacyRestrictionDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PrivacyRestrictionLevelType PrivacyRestrictionLevel
        {
            get
            {
                return this.privacyRestrictionLevelField;
            }
            set
            {
                this.privacyRestrictionLevelField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool PrivacyRestrictionLevelSpecified
        {
            get
            {
                return this.privacyRestrictionLevelFieldSpecified;
            }
            set
            {
                this.privacyRestrictionLevelFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class EmploymentType
    {

        private OrganizationType1 employerField;

        private System.DateTime employmentBeginDateField;

        private bool employmentBeginDateFieldSpecified;

        private System.DateTime employmentEndDateField;

        private bool employmentEndDateFieldSpecified;

        private string positionTitleField;

        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public OrganizationType1 Employer
        {
            get
            {
                return this.employerField;
            }
            set
            {
                this.employerField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime EmploymentBeginDate
        {
            get
            {
                return this.employmentBeginDateField;
            }
            set
            {
                this.employmentBeginDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool EmploymentBeginDateSpecified
        {
            get
            {
                return this.employmentBeginDateFieldSpecified;
            }
            set
            {
                this.employmentBeginDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime EmploymentEndDate
        {
            get
            {
                return this.employmentEndDateField;
            }
            set
            {
                this.employmentEndDateField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool EmploymentEndDateSpecified
        {
            get
            {
                return this.employmentEndDateFieldSpecified;
            }
            set
            {
                this.employmentEndDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PositionTitle
        {
            get
            {
                return this.positionTitleField;
            }
            set
            {
                this.positionTitleField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class PLUSAwardTypeDisbursement : DLDisbursementType
    {

        private ResponseType responseField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ResponseType Response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }
    }

    /// <remarks/>
    [XmlIncludeAttribute(typeof(PerkinsType))]
    [XmlIncludeAttribute(typeof(FWSPType))]
    [XmlIncludeAttribute(typeof(SEOGType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class CampusBasedAwardType : FinancialAwardType
    {

        private System.Nullable<CampusBasedAwardTypeDependencyStatusCode> dependencyStatusCodeField;

        private bool dependencyStatusCodeFieldSpecified;

        private System.Nullable<bool> lessThanFullTimeIndicatorField;

        private bool lessThanFullTimeIndicatorFieldSpecified;

        private System.Nullable<decimal> federalShareAmountField;

        private bool federalShareAmountFieldSpecified;

        private StudentLevelCodeType studentLevelCodeField;

        private bool studentLevelCodeFieldSpecified;

        private System.Nullable<decimal> fISAPIncomeAmountField;

        private bool fISAPIncomeAmountFieldSpecified;

        private System.Nullable<CampusBasedAwardTypeSecondaryEFCCode> secondaryEFCCodeField;

        private bool secondaryEFCCodeFieldSpecified;

        private ResponseType responseField;

        private List<CODDisbursementType> disbursementField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<CampusBasedAwardTypeDependencyStatusCode> DependencyStatusCode
        {
            get
            {
                return this.dependencyStatusCodeField;
            }
            set
            {
                this.dependencyStatusCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool DependencyStatusCodeSpecified
        {
            get
            {
                return this.dependencyStatusCodeFieldSpecified;
            }
            set
            {
                this.dependencyStatusCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<bool> LessThanFullTimeIndicator
        {
            get
            {
                return this.lessThanFullTimeIndicatorField;
            }
            set
            {
                this.lessThanFullTimeIndicatorField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool LessThanFullTimeIndicatorSpecified
        {
            get
            {
                return this.lessThanFullTimeIndicatorFieldSpecified;
            }
            set
            {
                this.lessThanFullTimeIndicatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> FederalShareAmount
        {
            get
            {
                return this.federalShareAmountField;
            }
            set
            {
                this.federalShareAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool FederalShareAmountSpecified
        {
            get
            {
                return this.federalShareAmountFieldSpecified;
            }
            set
            {
                this.federalShareAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public StudentLevelCodeType StudentLevelCode
        {
            get
            {
                return this.studentLevelCodeField;
            }
            set
            {
                this.studentLevelCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool StudentLevelCodeSpecified
        {
            get
            {
                return this.studentLevelCodeFieldSpecified;
            }
            set
            {
                this.studentLevelCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<decimal> FISAPIncomeAmount
        {
            get
            {
                return this.fISAPIncomeAmountField;
            }
            set
            {
                this.fISAPIncomeAmountField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool FISAPIncomeAmountSpecified
        {
            get
            {
                return this.fISAPIncomeAmountFieldSpecified;
            }
            set
            {
                this.fISAPIncomeAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public System.Nullable<CampusBasedAwardTypeSecondaryEFCCode> SecondaryEFCCode
        {
            get
            {
                return this.secondaryEFCCodeField;
            }
            set
            {
                this.secondaryEFCCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool SecondaryEFCCodeSpecified
        {
            get
            {
                return this.secondaryEFCCodeFieldSpecified;
            }
            set
            {
                this.secondaryEFCCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ResponseType Response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Disbursement", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CODDisbursementType> Disbursement
        {
            get
            {
                return this.disbursementField;
            }
            set
            {
                this.disbursementField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    [XmlRootAttribute("DLSubsidized", Namespace = "urn:org:pesc:core:CoreMain:v1.16.0", IsNullable = false)]
    public partial class DLSubsidizedType : SubsidizedAwardType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    [XmlRootAttribute("DLUnsubsidized", Namespace = "urn:org:pesc:core:CoreMain:v1.16.0", IsNullable = false)]
    public partial class DLUnsubsidizedType : UnsubsidizedAwardType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    [XmlRootAttribute("DLPLUS", Namespace = "urn:org:pesc:core:CoreMain:v1.16.0", IsNullable = false)]
    public partial class DLPLUSType : PLUSAwardType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    [XmlRootAttribute("Perkins", Namespace = "urn:org:pesc:core:CoreMain:v1.16.0", IsNullable = false)]
    public partial class PerkinsType : CampusBasedAwardType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    [XmlRootAttribute("SEOG", Namespace = "urn:org:pesc:core:CoreMain:v1.16.0", IsNullable = false)]
    public partial class SEOGType : CampusBasedAwardType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    [XmlRootAttribute("FWSP", Namespace = "urn:org:pesc:core:CoreMain:v1.16.0", IsNullable = false)]
    public partial class FWSPType : CampusBasedAwardType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(TypeName = "BirthType", Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class BirthType1
    {

        private System.DateTime birthDateField;
        private bool birthDateFieldSpecified;
        private string birthdayField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "gMonthDay")]
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
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class ContactsType
    {

        private List<AddressType> addressField;
        private List<EmailType> emailField;
        private List<PhoneType> phoneField;
        private List<URLType> uRLField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute("Address", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AddressType> Address
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
        [XmlElementAttribute("Email", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<EmailType> Email
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
        [XmlElementAttribute("Phone", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<PhoneType> Phone
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
        [XmlElementAttribute("URL", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<URLType> URL
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
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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


    #endregion

    #region Types

    public enum InternalTransmissionType { 
        ReceivedApplications,
        SentApplications,
        ReceivedTransferRequests,
        SentTransferRequests,
        ReceivedTranscripts,
        SentTranscripts,
        NotSet,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum DocumentTypeCodeType
    {

        /// <remarks/>
        Request = 0,

        /// <remarks/>
        Response = 1,

        /// <remarks/>
        DisbursementRoster = 2,

        /// <remarks/>
        DisbursementForecast = 3,

        /// <remarks/>
        DisbursementAcknowledgement = 4,

        /// <remarks/>
        Application = 5,

        /// <remarks/>
        Change = 6,

        /// <remarks/>
        CertificationRequest = 7,

        /// <remarks/>
        Receipt = 8,

        /// <remarks/>
        TermEnroll = 9,

        /// <remarks/>
        TermGrade = 10,

        /// <remarks/>
        StudentRequest = 11,

        /// <remarks/>
        RequestedRecord = 12,

        /// <remarks/>
        InstitutionRequest = 13,

        /// <remarks/>
        ThirdPartyRequest = 14,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum TransmissionTypeType
    {

        /// <remarks/>
        Original,

        /// <remarks/>
        Replace,

        /// <remarks/>
        Duplicate,

        /// <remarks/>
        Resubmission,

        /// <remarks/>
        Reissue,

        /// <remarks/>
        MutuallyDefined,
    }

    /// <remarks/>
    /// Codes for US states and Canadian provinces
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum LocalOrganizationIDCodeQualifierType
    {

        /// <remarks/>
        AA,

        /// <remarks/>
        AB,

        /// <remarks/>
        AE,

        /// <remarks/>
        AK,

        /// <remarks/>
        AL,

        /// <remarks/>
        AP,

        /// <remarks/>
        AR,

        /// <remarks/>
        AS,

        /// <remarks/>
        AZ,

        /// <remarks/>
        BC,

        /// <remarks/>
        CA,

        /// <remarks/>
        CO,

        /// <remarks/>
        CT,

        /// <remarks/>
        CZ,

        /// <remarks/>
        DC,

        /// <remarks/>
        DE,

        /// <remarks/>
        FL,

        /// <remarks/>
        FM,

        /// <remarks/>
        GA,

        /// <remarks/>
        GU,

        /// <remarks/>
        HI,

        /// <remarks/>
        IA,

        /// <remarks/>
        ID,

        /// <remarks/>
        IL,

        /// <remarks/>
        IN,

        /// <remarks/>
        KS,

        /// <remarks/>
        KY,

        /// <remarks/>
        LA,

        /// <remarks/>
        MA,

        /// <remarks/>
        MB,

        /// <remarks/>
        MD,

        /// <remarks/>
        ME,

        /// <remarks/>
        MH,

        /// <remarks/>
        MI,

        /// <remarks/>
        MN,

        /// <remarks/>
        MO,

        /// <remarks/>
        MP,

        /// <remarks/>
        MS,

        /// <remarks/>
        MT,

        /// <remarks/>
        NB,

        /// <remarks/>
        NC,

        /// <remarks/>
        ND,

        /// <remarks/>
        NE,

        /// <remarks/>
        NF,

        /// <remarks/>
        NH,

        /// <remarks/>
        NJ,

        /// <remarks/>
        NL,

        /// <remarks/>
        NM,

        /// <remarks/>
        NS,

        /// <remarks/>
        NT,

        /// <remarks/>
        NU,

        /// <remarks/>
        NV,

        /// <remarks/>
        NY,

        /// <remarks/>
        OH,

        /// <remarks/>
        OK,

        /// <remarks/>
        ON,

        /// <remarks/>
        OR,

        /// <remarks/>
        PA,

        /// <remarks/>
        PE,

        /// <remarks/>
        PR,

        /// <remarks/>
        PW,

        /// <remarks/>
        QC,

        /// <remarks/>
        RI,

        /// <remarks/>
        SC,

        /// <remarks/>
        SD,

        /// <remarks/>
        SK,

        /// <remarks/>
        TN,

        /// <remarks/>
        TX,

        /// <remarks/>
        UT,

        /// <remarks/>
        VA,

        /// <remarks/>
        VI,

        /// <remarks/>
        VT,

        /// <remarks/>
        WA,

        /// <remarks/>
        WI,

        /// <remarks/>
        WV,

        /// <remarks/>
        WY,

        /// <remarks/>
        YT,

        /// <remarks/>
        ZZ,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum TestScoreMethodType
    {

        /// <remarks/>
        Scaled,

        /// <remarks/>
        Graded,

        /// <remarks/>
        Standard,

        /// <remarks/>
        Raw,

        /// <remarks/>
        Percent,

        /// <remarks/>
        Mastery,

        /// <remarks/>
        Adjective,

        /// <remarks/>
        Stanine,

        /// <remarks/>
        Percentile,

        /// <remarks/>
        NormalCurve,

        /// <remarks/>
        Equated,

        /// <remarks/>
        Local,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0", IncludeInSchema = false)]
    public enum ItemChoiceType1
    {

        /// <remarks/>
        [XmlEnumAttribute(":TestDate")]
        TestDate,

        /// <remarks/>
        [XmlEnumAttribute(":TestYear")]
        TestYear,

        /// <remarks/>
        [XmlEnumAttribute(":TestYearMonth")]
        TestYearMonth,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum StudentLevelCodeType
    {
        FirstGrade = 0,
        SecondGrade = 1,
        ThirdGrade = 2,
        FourthGrade = 3,
        FifthGrade = 4,
        SixthGrade = 5,
        SeventhGrade = 6,
        EighthGrade = 7,
        NinthGrade = 8,
        TenthGrade = 9,
        EleventhGrade = 10,
        TwelfthGrade = 11,
        Grade13 = 12,
        NoFormalSchool = 13,
        Postsecondary = 14,

        Infant = 15,
        PreKindergarten = 16,
        HalfDayKindergarten = 17,
        Kindergarten = 18,
        PostsecondaryBachelorPreliminaryYear = 19,
        NonDegree = 20,
        CollegeFirstYear = 21,
        CollegeFirstYearAttendedBefore = 22,
        CollegeSophomore = 23,
        CollegeJunior = 24,
        CollegeSenior = 25,
        CollegeFifthYear = 26,
        PostBaccalaureate = 27,
        GraduateNonDegree = 28,
        GraduateFirstYear = 29,
        GraduateSecondYear = 30,
        GraduateThirdYear = 31,
        GraduateBeyondThirdYear = 32,
        Professional = 33,
        ProfessionalFirstYear = 34,
        ProfessionalSecondYear = 35,
        ProfessionalThirdYear = 36,
        ProfessionalBeyondThirdYear = 37,
        MastersQualifying = 38,
        Masters = 39,
        Doctoral = 40,
        Postdoctoral = 41,
        Ungraded = 42,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum LanguageProficiencyType
    {
        EnglishOnly,
        Excellent,
        Fair,
        FullyEnglishProficient,
        Good,
        LimitedEnglishProficiency,
        NonEnglishSpeaking,
        Poor,
        RedesignatedFluentEnglishProficient,
        Unacceptable,
        Unknown,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum LanguageUsageType
    {
        Always,
        FirstSpokenLanguage,
        Native,
        Reading,
        SomeSections,
        Sometimes,
        Speaking,
        SpokenAtHome,
        Writing,
        WrittenCommunication,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum ConditionsMetCodeType
    {

        /// <remarks/>
        Yes,

        /// <remarks/>
        No,

        /// <remarks/>
        Conditional,

        /// <remarks/>
        MutuallyDefined,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum InstructionUsageType
    {

        /// <remarks/>
        Instruction,

        /// <remarks/>
        Examination,

        /// <remarks/>
        WrittenExam,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum CourseCreditBasisType
    {

        /// <remarks/>
        Regular,

        /// <remarks/>
        AcademicRenewal,

        /// <remarks/>
        AdvancedPlacement,

        /// <remarks/>
        AdvancedStanding,

        /// <remarks/>
        ContinuingEducation,

        /// <remarks/>
        Exemption,

        /// <remarks/>
        Equivalence,

        /// <remarks/>
        InternationalBaccalaureate,

        /// <remarks/>
        Military,

        /// <remarks/>
        Remedial,

        /// <remarks/>
        CreditByExam,

        /// <remarks/>
        HighSchoolTransferCredit,

        /// <remarks/>
        HighSchoolCreditOnly,

        /// <remarks/>
        HighSchoolDualCredit,

        /// <remarks/>
        JuniorHighSchoolCredit,

        /// <remarks/>
        Major,

        /// <remarks/>
        AdultBasic,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum CourseCreditUnitsType
    {

        /// <remarks/>
        NoCredit,

        /// <remarks/>
        Quarter,

        /// <remarks/>
        Semester,

        /// <remarks/>
        Units,

        /// <remarks/>
        ClockHours,

        /// <remarks/>
        CarnegieUnits,

        /// <remarks/>
        ContinuingEducationUnits,

        /// <remarks/>
        Unreported,

        /// <remarks/>
        Other,

        /// <remarks/>
        [XmlEnumAttribute("NoCredit ")]
        NoCredit1,

        /// <remarks/>
        [XmlEnumAttribute("Quarter ")]
        Quarter1,

        /// <remarks/>
        [XmlEnumAttribute("Semester  ")]
        Semester1,

        /// <remarks/>
        [XmlEnumAttribute("Units ")]
        Units1,

        /// <remarks/>
        [XmlEnumAttribute("Other ")]
        Other1,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum CourseCreditLevelType
    {
        Undergraduate = 0,
        LowerDivision = 1,
        UpperDivision = 2,
        Vocational = 3,
        TechnicalPreparatory = 4,
        Graduate = 5,
        Professional = 6,
        Dual = 7,
        GraduateProfessional = 8,
        TenthGrade = 9,
        EleventhGrade = 10,
        TwelfthGrade = 11
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum HonorsLevelType
    {
        FirstHighest,
        SecondHighest,
        ThirdHighest,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum CourseAcademicGradeStatusCodeType
    {
        InProgress = 0,
        Completed = 1,
        PlanningToTake = 2,
        AuditedCourse = 3,
        HonorsGrade = 4,
        Incomplete = 5,
        IncompleteNotResolvedFail = 6,
        NotYetReported = 7,
        OtherFail = 8,
        OtherPass = 9,
        PassFailFail = 10,
        PassFailPass = 11,
        TransferNoGrade = 12,
        Withdrew = 13,
        Withdrawn = 14,
        WithdrewFailing = 15,
        WithdrewNoPenalty = 16,
        WithdrewPassing = 17,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum CourseRepeatCodeType
    {

        /// <remarks/>
        RepeatCounted,

        /// <remarks/>
        RepeatNotCounted,

        /// <remarks/>
        ReplacementCounted,

        /// <remarks/>
        ReplacedNotCounted,

        /// <remarks/>
        RepeatOtherInstitution,

        /// <remarks/>
        NotCountedOther,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum CourseLevelType
    {

        /// <remarks/>
        Accelerated,

        /// <remarks/>
        AdultBasic,

        /// <remarks/>
        AdvancedPlacement,

        /// <remarks/>
        Basic,

        /// <remarks/>
        InternationalBaccalaureate,

        /// <remarks/>
        CollegeLevel,

        /// <remarks/>
        CollegePreparatory,

        /// <remarks/>
        GiftedTalented,

        /// <remarks/>
        Honors,

        /// <remarks/>
        NonAcademic,

        /// <remarks/>
        SpecialEducation,

        /// <remarks/>
        TechnicalPreparatory,

        /// <remarks/>
        Vocational,

        /// <remarks/>
        LowerDivision,

        /// <remarks/>
        UpperDivision,

        /// <remarks/>
        Dual,

        /// <remarks/>
        GraduateProfessional,

        /// <remarks/>
        Remedial,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum CourseGPAApplicabilityCodeType
    {

        /// <remarks/>
        Applicable,

        /// <remarks/>
        NotApplicable,

        /// <remarks/>
        Weighted,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum SchoolOverrideCodeType
    {

        /// <remarks/>
        Institutional,

        /// <remarks/>
        Multicampus,

        /// <remarks/>
        Transfer,

        /// <remarks/>
        StudyAbroad,

        /// <remarks/>
        Coop,

        /// <remarks/>
        Reciprocal,

        /// <remarks/>
        Internship,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum SchoolLevelType
    {

        /// <remarks/>
        Elementary,

        /// <remarks/>
        Secondary,

        /// <remarks/>
        Postsecondary,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum CountryCodeType
    {
        /// Manually added Serbia to the list because application from Serbia was received.
        /// Manually adding XCA - canada default for our system - especially for Residency on ApplicationForm

        RS = 0,
        AF = 1,
        AX = 2,
        AL = 3,
        DZ = 4,
        AS = 5,
        AD = 6,
        AO = 7,
        AI = 8,
        AQ = 9,
        AG = 10,
        AR = 11,
        AM = 12,
        AW = 13,
        AU = 14,
        AT = 15,
        AZ = 16,
        BS = 17,
        BH = 18,
        BD = 19,
        BB = 20,
        BY = 21,
        BE = 22,
        BZ = 23,
        BJ = 24,
        BM = 25,
        BT = 26,
        BO = 27,
        BA = 28,
        BW = 29,
        BV = 30,
        BR = 31,
        IO = 32,
        BN = 33,
        BG = 34,
        BF = 35,
        BI = 36,
        KH = 37,
        CM = 38,
        CA = 39,
        CV = 40,
        KY = 41,
        CF = 42,
        TD = 43,
        CL = 44,
        CN = 45,
        CX = 46,
        CC = 47,
        CO = 48,
        KM = 49,
        CG = 50,
        CD = 51,
        CK = 52,
        CR = 53,
        CI = 54,
        HR = 55,
        CU = 56,
        CY = 57,
        CZ = 58,
        DK = 59,
        DJ = 60,
        DM = 61,
        DO = 62,
        EC = 63,
        EG = 64,
        SV = 65,
        GQ = 66,
        ER = 67,
        EE = 68,
        ET = 69,
        FK = 70,
        FO = 71,
        FJ = 72,
        FI = 73,
        FR = 74,
        GF = 75,
        PF = 76,
        TF = 77,
        GA = 78,
        GM = 79,
        GE = 80,
        DE = 81,
        GH = 82,
        GI = 83,
        GR = 84,
        GL = 85,
        GD = 86,
        GP = 87,
        GU = 88,
        GT = 89,
        GN = 90,
        GW = 91,
        GY = 92,
        HT = 93,
        HM = 94,
        VA = 95,
        HN = 96,
        HK = 97,
        HU = 98,
        IS = 99,
        IN = 100,
        ID = 101,
        IR = 102,
        IQ = 103,
        IE = 104,
        IL = 105,
        IT = 106,
        JM = 107,
        JP = 108,
        JO = 109,
        KZ = 110,
        KE = 111,
        KI = 112,
        KP = 113,
        KR = 114,
        KW = 115,
        KG = 116,
        LA = 117,
        LV = 118,
        LB = 119,
        LS = 120,
        LR = 121,
        LY = 122,
        LI = 123,
        LT = 124,
        LU = 125,
        MO = 126,
        MK = 127,
        MG = 128,
        MW = 129,
        MY = 130,
        MV = 131,
        ML = 132,
        MT = 133,
        MH = 134,
        MQ = 135,
        MR = 136,
        MU = 137,
        YT = 138,
        MX = 139,
        FM = 140,
        MD = 141,
        MC = 142,
        MN = 143,
        MS = 144,
        MA = 145,
        MZ = 146,
        MM = 147,
        NA = 148,
        NR = 149,
        NP = 150,
        NL = 151,
        AN = 152,
        NC = 153,
        NZ = 154,
        NI = 155,
        NE = 156,
        NG = 157,
        NU = 158,
        NF = 159,
        MP = 160,
        NO = 161,
        OM = 162,
        PK = 163,
        PW = 164,
        PS = 165,
        PA = 166,
        PG = 167,
        PY = 168,
        PE = 169,
        PH = 170,
        PN = 171,
        PL = 172,
        PT = 173,
        PR = 174,
        QA = 175,
        RE = 176,
        RO = 177,
        RU = 178,
        RW = 179,
        SH = 180,
        KN = 181,
        LC = 182,
        PM = 183,
        VC = 184,
        WS = 185,
        SM = 186,
        ST = 187,
        SA = 188,
        SN = 189,
        CS = 190,
        SC = 191,
        SL = 192,
        SG = 193,
        SK = 194,
        SI = 195,
        SB = 196,
        SO = 197,
        ZA = 198,
        GS = 199,
        ES = 200,
        LK = 201,
        SD = 202,
        SR = 203,
        SJ = 204,
        SZ = 205,
        SE = 206,
        CH = 207,
        SY = 208,
        TW = 209,
        TJ = 210,
        TZ = 211,
        TH = 212,
        TL = 213,
        TG = 214,
        TK = 215,
        TO = 216,
        TT = 217,
        TN = 218,
        TR = 219,
        TM = 220,
        TC = 221,
        TV = 222,
        UG = 223,
        UA = 224,
        AE = 225,
        GB = 226,
        US = 227,
        UM = 228,
        UY = 229,
        UZ = 230,
        VU = 231,
        VE = 232,
        VN = 233,
        VG = 234,
        VI = 235,
        WF = 236,
        EH = 237,
        YE = 238,
        ZM = 239,
        ZW = 240,
        SS = 241,
        BL = 242,
        BQ = 243,
        CW = 244,
        GG = 245,
        GZ = 246,
        IM = 247,
        JE = 248,
        KS = 249,
        ME = 250,
        MF = 251,
        SX = 252,
        WE = 253,

        /// Manually adding XCA for our application Residency 
        /// XCA,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum StateProvinceCodeType
    {
        AA = 0,
        AB = 1,
        AE = 2,
        AK = 3,
        AL = 4,
        AP = 5,
        AR = 6,
        AS = 7,
        AZ = 8,
        BC = 9,
        CA = 10,
        CO = 11,
        CT = 12,
        CZ = 13,
        DC = 14,
        DE = 15,
        FL = 16,
        FM = 17,
        GA = 18,
        GU = 19,
        HI = 20,
        IA = 21,
        ID = 22,
        IL = 23,
        IN = 24,
        KS = 25,
        KY = 26,
        LA = 27,
        MA = 28,
        MB = 29,
        MD = 30,
        ME = 31,
        MH = 32,
        MI = 33,
        MN = 34,
        MO = 35,
        MP = 36,
        MS = 37,
        MT = 38,
        NB = 39,
        NC = 40,
        ND = 41,
        NE = 42,
        NF = 43,
        NH = 44,
        NJ = 45,
        NL = 46,
        NM = 47,
        NS = 48,
        NT = 49,
        NU = 50,
        NV = 51,
        NY = 52,
        OH = 53,
        OK = 54,
        ON = 55,
        OR = 56,
        PA = 57,
        PE = 58,
        PR = 59,
        PW = 60,
        QC = 61,
        RI = 62,
        SC = 63,
        SD = 64,
        SK = 65,
        TN = 66,
        TX = 67,
        UT = 68,
        VA = 69,
        VI = 70,
        VT = 71,
        WA = 72,
        WI = 73,
        WV = 74,
        WY = 75,
        YT = 76,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    //[XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0", IncludeInSchema = false)]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum ItemsChoiceType
    {
        /// <remarks/>
        [XmlEnumAttribute(":CountryCode")]
        CountryCode,

        /// <remarks/>
        [XmlEnumAttribute(":PostalCode")]
        PostalCode,

        /// <remarks/>
        [XmlEnumAttribute(":StateProvince")]
        StateProvince,

        /// <remarks/>
        [XmlEnumAttribute(":StateProvinceCode")]
        StateProvinceCode,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum CourseApplicabilityType
    {
        /// <remarks/>
        NotApplicable,

        /// <remarks/>
        FirstProgram,

        /// <remarks/>
        SecondProgram,

        /// <remarks/>
        BothPrograms,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum CourseInstructionSiteType
    {
        /// <remarks/>
        OnCampus,

        /// <remarks/>
        OffCampus,

        /// <remarks/>
        Extension,

        /// <remarks/>
        StudyAbroad,

        /// <remarks/>
        Correctional,

        /// <remarks/>
        Millitary,

        /// <remarks/>
        Telecommunication,

        /// <remarks/>
        Auxiliary,

        /// <remarks/>
        ClinicHospital,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum SessionTypeType
    {

        /// <remarks/>
        FullYear,

        /// <remarks/>
        Semester,

        /// <remarks/>
        Trimester,

        /// <remarks/>
        Quarter,

        /// <remarks/>
        Quinmester,

        /// <remarks/>
        MiniTerm,

        /// <remarks/>
        SummerSession,

        /// <remarks/>
        Intersession,

        /// <remarks/>
        LongSession,

        /// <remarks/>
        FallSession,

        /// <remarks/>
        FourOneFourPlan,

        /// <remarks/>
        Continuous,

        /// <remarks/>
        DiffersByProgram,

        /// <remarks/>
        Other,

        /// <remarks/>
        TwelveMonth,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum DelinquenciesType
    {
        /// <remarks/>
        GoodStanding,

        /// <remarks/>
        ProbationGPA,

        /// <remarks/>
        ProbationHours,

        /// <remarks/>
        SuspensionGPA,

        /// <remarks/>
        SuspensionHours,

        /// <remarks/>
        ProbationDiscipline,

        /// <remarks/>
        SuspensionDiscipline,

        /// <remarks/>
        Unknown,

        /// <remarks/>
        Other,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum AcademicSummaryLevelType
    {
        /// <remarks/>
        Undergraduate,
        /// <remarks/>
        LowerDivision,
        /// <remarks/>
        UpperDivision,
        /// <remarks/>
        Vocational,
        /// <remarks/>
        TechnicalPreparatory,
        /// <remarks/>
        Graduate,
        /// <remarks/>
        Professional,
        /// <remarks/>
        Dual,
        /// <remarks/>
        GraduateProfessional,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum AcademicSummaryTypeType
    {
        /// <remarks/>
        All,

        /// <remarks/>
        SenderOnly,

        /// <remarks/>
        TransferOnly,

        /// <remarks/>
        AllNotRepeated,

        /// <remarks/>
        SenderNotRepeated,

        /// <remarks/>
        TransferNotRepeated,

        /// <remarks/>
        AcademicRenewal,

        /// <remarks/>
        CarryoverCredit,

        /// <remarks/>
        DegreeApplicable,

        /// <remarks/>
        NonDegreeApplicable,

        /// <remarks/>
        ConvertedFrom,

        /// <remarks/>
        ConvertedTo,

        /// <remarks/>
        Weighted,

        /// <remarks/>
        NonWeighted,

        /// <remarks/>
        UserDefined,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum HonorsRecognitionLevelType
    {
        FirstHighest,
        SecondHighest,
        ThirdHighest,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum AcademicProgramTypeType
    {
        Concentration = 0,
        Major = 1,
        Minor = 2,
        Specialization = 3,
        Focus = 4,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum AcademicAwardLevelType
    {
        B17 = 0,
        B18 = 1,
        B19 = 2,
        B20 = 3,
        B21 = 4,
        B22 = 5,
        B23 = 6,
        B24 = 7,
        B25 = 8,
        B26 = 9,
        B27 = 10,
        B28 = 11,
        B58 = 12,
        B59 = 13,
        B60 = 14,

        [XmlEnumAttribute("0.0")]
        Item00 = 32,

        [XmlEnumAttribute("1.1")]
        Item11 = 15,

        [XmlEnumAttribute("1.2")]
        Item12 = 16,

        [XmlEnumAttribute("1.3")]
        Item13 = 33,

        [XmlEnumAttribute("1.4")]
        Item15 = 34,

        [XmlEnumAttribute("1.5")]
        Item14 = 35,

        [XmlEnumAttribute("2.0")]
        Item20 = 17,

        [XmlEnumAttribute("2.1")]
        Item21 = 18,

        [XmlEnumAttribute("2.2")]
        Item22 = 19,

        [XmlEnumAttribute("2.3")]
        Item23 = 20,

        [XmlEnumAttribute("2.4")]
        Item24 = 21,

        [XmlEnumAttribute("2.5")]
        Item25 = 22,

        [XmlEnumAttribute("2.6")]
        Item26 = 23,

        [XmlEnumAttribute("2.7")]
        Item27 = 24,

        [XmlEnumAttribute("2.8")]
        Item28 = 36,

        [XmlEnumAttribute("3.1")]
        Item31 = 25,

        [XmlEnumAttribute("3.2")]
        Item32 = 26,

        [XmlEnumAttribute("3.3")]
        Item33 = 37,

        [XmlEnumAttribute("4.0")]
        Item40 = 38,

        [XmlEnumAttribute("4.1")]
        Item41 = 27,

        [XmlEnumAttribute("4.2")]
        Item42 = 28,

        [XmlEnumAttribute("4.3")]
        Item43 = 29,

        [XmlEnumAttribute("4.4")]
        Item44 = 30,

        [XmlEnumAttribute("4.5")]
        Item45 = 31,

        FrenchBaccalaureate = 39,
        CEGEP = 40,

        [XmlEnumAttribute("1")]
        L1 = 41,
        [XmlEnumAttribute("2")]
        L2 = 42,
        [XmlEnumAttribute("3")]
        L3 = 43,
        [XmlEnumAttribute("4")]
        L4 = 44,
        [XmlEnumAttribute("5")]
        L5 = 45,
        [XmlEnumAttribute("6")]
        L6 = 46,
        [XmlEnumAttribute("7")]
        L7 = 47,
        [XmlEnumAttribute("8")]
        L8 = 48,
        [XmlEnumAttribute("9")]
        L9 = 49,
        [XmlEnumAttribute("10")]
        L10 = 50,
        [XmlEnumAttribute("11")]
        L11 = 51,
        [XmlEnumAttribute("17")]
        L17 = 52,
        [XmlEnumAttribute("18")]
        L18 = 53,
        [XmlEnumAttribute("19")]
        L19 = 54,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum ResidencyStatusCodeType
    {

        /// <remarks/>
        Resident,

        /// <remarks/>
        NonResident,

        /// <remarks/>
        NotReported,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum GenderCodeType
    {

        [Display(Name = "Female")]
        Female = 0,

        [Display(Name = "Male")]
        Male = 1,

        [Display(Name = "Unreported")]
        Unreported = 2,

        [Display(Name = "Unspecified")]
        Unspecified = 3,
    } 

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum NameSuffixType
    {

        /// <remarks/>
        JR,

        /// <remarks/>
        SR,

        /// <remarks/>
        I,

        /// <remarks/>
        II,

        /// <remarks/>
        III,

        /// <remarks/>
        IV,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum DocumentProcessCodeType
    {

        /// <remarks/>
        TEST,

        /// <remarks/>
        PRODUCTION,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum DocumentOfficialCodeType
    {

        /// <remarks/>
        Official,

        /// <remarks/>
        Unofficial,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum DocumentCompleteCodeType
    {

        /// <remarks/>
        Partial,

        /// <remarks/>
        Complete,

        /// <remarks/>
        [XmlEnumAttribute("")]
        Item,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum DLLoanInformationTypePromissoryNotePrintCode
    {

        /// <remarks/>
        S,

        /// <remarks/>
        R,

        /// <remarks/>
        Z,

        /// <remarks/>
        V,

        /// <remarks/>
        O,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum DLLoanInformationTypeDisclosureStatementPrintCode
    {

        /// <remarks/>
        Y,

        /// <remarks/>
        R,

        /// <remarks/>
        S,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum ResponseCodeType
    {

        /// <remarks/>
        A,

        /// <remarks/>
        R,

        /// <remarks/>
        D,

        /// <remarks/>
        C,

        /// <remarks/>
        X,

        /// <remarks/>
        Accepted,

        /// <remarks/>
        Rejected,

        /// <remarks/>
        Pending,

        /// <remarks/>
        Forwarding,

        /// <remarks/>
        Statusing,

        /// <remarks/>
        Forwarded,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum PellAwardResponseTypeFSACode
    {

        /// <remarks/>
        SA,

        /// <remarks/>
        CE,

        /// <remarks/>
        PO,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum LoanAwardResponseTypeMPNStatusCode
    {

        /// <remarks/>
        A,

        /// <remarks/>
        R,

        /// <remarks/>
        X,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum PLUSAwardResponseTypeCreditDecisionStatus
    {

        /// <remarks/>
        A,

        /// <remarks/>
        D,

        /// <remarks/>
        P,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum PLUSAwardResponseTypeCreditOverrideCode
    {

        /// <remarks/>
        C,

        /// <remarks/>
        D,

        /// <remarks/>
        E,

        /// <remarks/>
        N,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum PellTypeAcademicCalendarCode
    {

        /// <remarks/>
        [XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [XmlEnumAttribute("5")]
        Item5,

        /// <remarks/>
        [XmlEnumAttribute("6")]
        Item6,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum PellTypePaymentMethodologyCode
    {

        /// <remarks/>
        [XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [XmlEnumAttribute("5")]
        Item5,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum PellTypeLowTuitionFeesCode
    {

        /// <remarks/>
        [XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [XmlEnumAttribute(" ")]
        Item,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum PellTypeVerificationStatusCode
    {

        /// <remarks/>
        W,

        /// <remarks/>
        V,

        /// <remarks/>
        S,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum PellTypeSecondaryEFCCode
    {

        /// <remarks/>
        O,

        /// <remarks/>
        S,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum FundsDistributionMethodCodeType
    {

        /// <remarks/>
        EFT,

        /// <remarks/>
        IndividualCheck,

        /// <remarks/>
        MasterCheck,

        /// <remarks/>
        Netting,

        /// <remarks/>
        NoFundsMoved,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum DisbursementStatusCodeType
    {

        /// <remarks/>
        A,

        /// <remarks/>
        B,

        /// <remarks/>
        C,

        /// <remarks/>
        D,

        /// <remarks/>
        F,

        /// <remarks/>
        G,

        /// <remarks/>
        H,

        /// <remarks/>
        L,

        /// <remarks/>
        P,

        /// <remarks/>
        R,

        /// <remarks/>
        S,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum LoanAwardTypeDependencyStatusCode
    {

        /// <remarks/>
        I,

        /// <remarks/>
        D,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum DefaultOverpayCodeType
    {

        /// <remarks/>
        Y,

        /// <remarks/>
        N,

        /// <remarks/>
        Z,

        /// <remarks/>
        Yes,

        /// <remarks/>
        No,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum InterestRateOptionType
    {

        /// <remarks/>
        Fixed,

        /// <remarks/>
        Variable,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum RepaymentOptionCodeType
    {

        /// <remarks/>
        Deferment,

        /// <remarks/>
        InterestOnly,

        /// <remarks/>
        PrincipalAndInterest,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum RaceEthnicityCodeType
    {

        /// <remarks/>
        AmericanIndianAlaskaNative,

        /// <remarks/>
        Asian,

        /// <remarks/>
        BlackNonHispanic,

        /// <remarks/>
        Hispanic,

        /// <remarks/>
        WhiteNonHispanic,

        /// <remarks/>
        NativeHawaiianPacificIslander,

        /// <remarks/>
        [XmlEnumAttribute("Asian ")]
        Asian1,

        /// <remarks/>
        [XmlEnumAttribute("WhiteNonHispanic ")]
        WhiteNonHispanic1,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum CitizenshipStatusCodeType
    {
        Asylee = 0,
        Citizen = 1,
        EligibleNonCitizen = 2,
        IllegalAlien = 3,
        LandedImmigrant = 4,
        MinistersPermit = 5,
        NonCitizen = 6,
        NonPermanentResidentAlien = 7,
        NonResidentAlien = 8,
        NonResidentCitizen = 9,
        NotEligible = 10,
        PermanentVisa = 11,
        Refugee = 12,
        ResidentAlien = 13,
        StudentAuthorization = 14,
        StudentVisa = 15,
        TemporaryVisa = 16,
        UndocumentedAlien = 17,
        VisitorVisa = 18,
        WorkVisa = 19,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum EducationLevelType
    {

        /// <remarks/>
        None,

        /// <remarks/>
        Elementary,

        /// <remarks/>
        MiddleSchool,

        /// <remarks/>
        JuniorHighSchool,

        /// <remarks/>
        SecondarySchool,

        /// <remarks/>
        HighSchoolAttended,

        /// <remarks/>
        HighSchoolGraduate,

        /// <remarks/>
        VocationalSchool,

        /// <remarks/>
        CollegeAttended,

        /// <remarks/>
        CollegeCertificate,

        /// <remarks/>
        CollegeDiploma,

        /// <remarks/>
        AssociateDegree,

        /// <remarks/>
        BaccalaureateDegree,

        /// <remarks/>
        ProfessionalAttended,

        /// <remarks/>
        ProfessionalDegree,

        /// <remarks/>
        ProfessionalCertification,

        /// <remarks/>
        MastersDegree,

        /// <remarks/>
        DoctoralDegree,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum NonImmigrantVisaStatusChangeCodeType
    {

        /// <remarks/>
        NoChange,

        /// <remarks/>
        StatusChange,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum SponsorTypeType
    {

        /// <remarks/>
        Parent,

        /// <remarks/>
        Spouse,

        /// <remarks/>
        Relative,

        /// <remarks/>
        USCompany,

        /// <remarks/>
        ForeignCompany,

        /// <remarks/>
        USGovernment,

        /// <remarks/>
        ForeignGovernment,

        /// <remarks/>
        Organization,

        /// <remarks/>
        Institution,

        /// <remarks/>
        Self,

        /// <remarks/>
        Unknown,

        /// <remarks/>
        Other,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum USStudyFormsReceiptType
    {

        /// <remarks/>
        I20,

        /// <remarks/>
        IAP66,

        /// <remarks/>
        I94,

        /// <remarks/>
        EducationalCosts,

        /// <remarks/>
        I134,

        /// <remarks/>
        I688B,

        /// <remarks/>
        Passport,

        /// <remarks/>
        I551,

        /// <remarks/>
        NotApplicable,

        /// <remarks/>
        Other,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum PrivacyRestrictionLevelType
    {

        /// <remarks/>
        [XmlEnumAttribute("00")]
        Item00,

        /// <remarks/>
        [XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [XmlEnumAttribute("99")]
        Item99,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum CampusBasedAwardTypeDependencyStatusCode
    {

        /// <remarks/>
        I,

        /// <remarks/>
        D,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum CampusBasedAwardTypeSecondaryEFCCode
    {

        /// <remarks/>
        O,

        /// <remarks/>
        S,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public partial class AgencyIdentifierType
    {

        private string agencyAssignedIDField;
        private AgencyCodeType agencyCodeField;
        private string agencyNameField;
        private CountryCodeType countryCodeField;
        private StateProvinceCodeType stateProvinceCodeField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AgencyAssignedID
        {
            get
            {
                return this.agencyAssignedIDField;
            }
            set
            {
                this.agencyAssignedIDField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AgencyCodeType AgencyCode
        {
            get
            {
                return this.agencyCodeField;
            }
            set
            {
                this.agencyCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AgencyName
        {
            get
            {
                return this.agencyNameField;
            }
            set
            {
                this.agencyNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CountryCodeType CountryCode
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public StateProvinceCodeType StateProvinceCode
        {
            get
            {
                return this.stateProvinceCodeField;
            }
            set
            {
                this.stateProvinceCodeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum AgencyCodeType
    {
        District = 0,
        Migrant = 1,
        MutuallyDefined = 2,
        National = 3,
        Province = 4,
        Regional = 5,
        State = 6,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum CanadianEthnicityCodeType
    {
        FirstNationStatus = 0,
        FirstNationsNonStatus = 1,
        Inuit = 2,
        Metis = 3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:core:CoreMain:v1.16.0")]
    public enum AttendedCollegeCodeType
    {
        [Display(Name = "No Family Member Attendance")]
        NoFamilyMemberAttendance = 0,

        [Display(Name = "Either Parent")]
        EitherParent = 1,

        [Display(Name = "Grand Parent")]
        GrandParent = 2,

        [Display(Name = "Siblings")]
        Siblings = 3,

        [Display(Name = "Spouse")]
        Spouse = 4,
    }

    #endregion

}