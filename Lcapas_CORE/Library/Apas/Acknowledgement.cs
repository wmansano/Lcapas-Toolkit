using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Lcapas.Core.Library.Apas.Acknowledgment
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ca:applyalberta:message:Acknowledgment:v1.0.1")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:ca:applyalberta:message:Acknowledgment:v1.0.1", IsNullable = false)]
    public partial class Acknowledgment : AcknowledgmentType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:ca:applyalberta:message:Acknowledgment:v1.0.1")]
    public partial class AcknowledgmentType
    {
        private AcknowledgmentSourceType acknowledgmentSourceField;
        private AcknowledgmentMessageType acknowledgmentMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AcknowledgmentSourceType AcknowledgmentSource
        {
            get
            {
                return this.acknowledgmentSourceField;
            }
            set
            {
                this.acknowledgmentSourceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AcknowledgmentMessageType AcknowledgmentMessage
        {
            get
            {
                return this.acknowledgmentMessageField;
            }
            set
            {
                this.acknowledgmentMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:ca:applyalberta:message:Acknowledgment:v1.0.1")]
    public partial class AcknowledgmentSourceType
    {
        private OriginatorType originatorField;
        private string organizationAPASCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public OriginatorType Originator
        {
            get
            {
                return this.originatorField;
            }
            set
            {
                this.originatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OrganizationAPASCode
        {
            get
            {
                return this.organizationAPASCodeField;
            }
            set
            {
                this.organizationAPASCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:ca:applyalberta:message:Acknowledgment:v1.0.1")]
    public enum OriginatorType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("APAS Core Production")]
        APASCoreProduction,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("APAS Core Support")]
        APASCoreSupport,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("APAS Proxy")]
        APASProxy,

        /// <remarks/>
        Other,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:ca:applyalberta:message:Acknowledgment:v1.0.1")]
    public partial class AcknowledgmentMessageType
    {
        private string acknowledgedMessageUUIDField;
        private string messageDetailsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AcknowledgedMessageUUID
        {
            get
            {
                return this.acknowledgedMessageUUIDField;
            }
            set
            {
                this.acknowledgedMessageUUIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MessageDetails
        {
            get
            {
                return this.messageDetailsField;
            }
            set
            {
                this.messageDetailsField = value;
            }
        }
    }
}