using System.Collections.Generic;

namespace Lcapas.Core.Library.Apas.HighSchoolTranscript
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:message:HighSchoolTranscript:v1.5.0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:org:pesc:message:HighSchoolTranscript:v1.5.0", IsNullable = false)]
    public partial class HighSchoolTranscript
    {
        private AcademicRecord.TransmissionDataType transmissionDataField;
        private AcademicRecord.K12StudentType studentField;
        private List<string> noteMessageField;
        private System.Xml.XmlElement userDefinedExtensionsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AcademicRecord.TransmissionDataType TransmissionData
        {
            get
            {
                return this.transmissionDataField;
            }
            set
            {
                this.transmissionDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AcademicRecord.K12StudentType Student
        {
            get
            {
                return this.studentField;
            }
            set
            {
                this.studentField = value;
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
}