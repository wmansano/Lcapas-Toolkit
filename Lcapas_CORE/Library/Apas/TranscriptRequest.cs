using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Lcapas.Core.Library.Apas.TranscriptRequest
{
    #region Classes
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:message:TranscriptRequest:v1.4.0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:org:pesc:message:TranscriptRequest:v1.4.0", IsNullable = false)]
    public partial class TranscriptRequest
    {

        private AcademicRecord.TransmissionDataType transmissionDataField;
        private AcademicRecord.RequestType[] requestField;
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
        [System.Xml.Serialization.XmlElementAttribute("Request")]
        public AcademicRecord.RequestType[] Request
        {
            get
            {
                return this.requestField;
            }
            set
            {
                this.requestField = value;
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

    #endregion

    #region Types

    #endregion

}