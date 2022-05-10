using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Lcapas.Core.Library.Apas.StatisticalData
{

    #region Classes

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ca:applyalberta:message:StatisticalData:v1.0.1")]
    [System.Xml.Serialization.XmlRootAttribute("StatisticalData", Namespace = "urn:ca:applyalberta:message:StatisticalData:v1.0.1", IsNullable = false)]

    public partial class StatisticalDataType
    {
        private string createDateTimeField;

        private int applicationIDField;

        private string applicantASNField;

        private string applicationType1Field;

        private string applicationType2Field;

        private CoreMain.LocalOrganizationIDType institutionIDField;

        private ApplicationStageType applicationStageField;

        private CoreMain.AcademicSessionDetailType academicSessionField;

        private AcademicProgramTypeType applicationDegreeProgramField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CreateDateTime
        {
            get
            {
                return this.createDateTimeField;
            }
            set
            {
                this.createDateTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int ApplicationID
        {
            get
            {
                return this.applicationIDField;
            }
            set
            {
                this.applicationIDField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ApplicantASN
        {
            get
            {
                return this.applicantASNField;
            }
            set
            {
                this.applicantASNField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ApplicationType1
        {
            get
            {
                return this.applicationType1Field;
            }
            set
            {
                this.applicationType1Field = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ApplicationType2
        {
            get
            {
                return this.applicationType2Field;
            }
            set
            {
                this.applicationType2Field = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.LocalOrganizationIDType InstitutionID
        {
            get
            {
                return this.institutionIDField;
            }
            set
            {
                this.institutionIDField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ApplicationStageType ApplicationStage
        {
            get
            {
                return this.applicationStageField;
            }
            set
            {
                this.applicationStageField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.AcademicSessionDetailType AcademicSession
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

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AcademicProgramTypeType ApplicationDegreeProgram
        {
            get
            {
                return this.applicationDegreeProgramField;
            }
            set
            {
                this.applicationDegreeProgramField = value;
            }
        }

    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:ca:applyalberta:message:StatData:v1.0.1")]

    public partial class AcademicProgramTypeType
    {
        private CoreMain.AcademicProgramTypeType academicProgramTypeField;
        private string academicProgramNameField;
        private string academicProgramCodeField;
        private string academicProgramFacultyField;
        private AcademicProgramDegreeLevelType academicProgramDegreeLevelField;
        private string academicProgramDegreeTitleField;
        private string academicProgramCampusField;
        private int academicProgramPriorityField;

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.AcademicProgramTypeType AcademicProgramType
        {
            get
            {
                return this.academicProgramTypeField;
            }
            set
            {
                this.academicProgramTypeField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AcademicProgramCode
        {
            get
            {
                return this.academicProgramCodeField;
            }
            set
            {
                this.academicProgramCodeField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AcademicProgramFaculty
        {
            get
            {
                return this.academicProgramFacultyField;
            }
            set
            {
                this.academicProgramFacultyField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AcademicProgramDegreeLevelType AcademicProgramDegreeLevel
        {
            get
            {
                return this.academicProgramDegreeLevelField;
            }
            set
            {
                this.academicProgramDegreeLevelField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AcademicProgramDegreeTitle
        {
            get
            {
                return this.academicProgramDegreeTitleField;
            }
            set
            {
                this.academicProgramDegreeTitleField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AcademicProgramCampus
        {
            get
            {
                return this.academicProgramCampusField;
            }
            set
            {
                this.academicProgramCampusField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int AcademicProgramPriority
        {
            get
            {
                return this.academicProgramPriorityField;
            }
            set
            {
                this.academicProgramPriorityField = value;
            }
        }
    }

    #endregion


    #region Types

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:ca:applyalberta:message:StatData:v1.0.1")]
    public enum AcademicProgramDegreeLevelType
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("AssociateDegree")]
        AssociateDegree,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Bachelors")]
        Bachelors,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Certificate")]
        Certificate,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Masters")]
        Masters,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Doctorate")]
        Doctorate,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("NonDegree")]
        NonDegree,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Professional")]
        Professional,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:ca:applyalberta:message:StatData:v1.0.1")]
    public enum ApplicationStageType
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Stopped")]
        Stopped,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Submitted")]
        Submitted,
    }


    #endregion

}