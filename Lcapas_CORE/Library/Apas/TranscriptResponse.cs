using System.Collections.Generic;

namespace Lcapas.Core.Library.Apas.TranscriptResponse
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:message:TranscriptResponse:v1.4.0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:org:pesc:message:TranscriptResponse:v1.4.0", IsNullable = false)]
    public partial class TranscriptResponse
    {

        private AcademicRecord.TransmissionDataType transmissionDataField;
        private AcademicRecord.ResponseType[] responseField;
        private List<string> noteMessageField;

        private System.Xml.XmlElement userDefinedExtensionsField;

        /// <remarks/>
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
        [System.Xml.Serialization.XmlElementAttribute("Response")]
        public AcademicRecord.ResponseType[] Response
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
        [System.Xml.Serialization.XmlElementAttribute("NoteMessage")]
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