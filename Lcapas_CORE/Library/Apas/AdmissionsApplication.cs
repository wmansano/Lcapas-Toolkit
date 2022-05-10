using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Lcapas.Core.Library.Apas.AdmissionsApplication
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:org:pesc:message:AdmissionsApplication:v1.3.0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:org:pesc:message:AdmissionsApplication:v1.3.0", IsNullable = false)]
    public partial class AdmissionsApplication
    {

        private AdmissionsRecord.TransmissionDataType transmissionDataField;
        private AdmissionsRecord.ApplicantType applicantField;
        private List<string> noteMessageField;
        private System.Xml.XmlElement userDefinedExtensionsField;

        /// <remarks/>
        public AdmissionsRecord.TransmissionDataType TransmissionData
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
        public AdmissionsRecord.ApplicantType Applicant
        {
            get
            {
                return this.applicantField;
            }
            set
            {
                this.applicantField = value;
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