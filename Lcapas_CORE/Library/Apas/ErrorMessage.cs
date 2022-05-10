using System.Collections.Generic;

namespace Lcapas.Core.Library.Apas.ErrorMessage
{
    #region Classes

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ca:applyalberta:message:ErrorMessage:v1.0.1")]
    [System.Xml.Serialization.XmlRootAttribute("ErrorMessage", Namespace = "urn:ca:applyalberta:message:ErrorMessage:v1.0.1", IsNullable = false)]
    public partial class ErrorMessageType
    {

        private string requestTrackingIDField;

        private ValidationErrorType[] validationErrorsField;

        private string originalMessageField;

        private string originalMessageUUIDField;

        private ErrorSourceType errorSourceField;

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
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("ValidationError", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public ValidationErrorType[] ValidationErrors
        {
            get
            {
                return this.validationErrorsField;
            }
            set
            {
                this.validationErrorsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OriginalMessage
        {
            get
            {
                return this.originalMessageField;
            }
            set
            {
                this.originalMessageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OriginalMessageUUID
        {
            get
            {
                return this.originalMessageUUIDField;
            }
            set
            {
                this.originalMessageUUIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ErrorSourceType ErrorSource
        {
            get
            {
                return this.errorSourceField;
            }
            set
            {
                this.errorSourceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:ca:applyalberta:message:ErrorMessage:v1.0.1")]
    public partial class ValidationErrorType
    {

        private string errorCodeField;

        private string errorTextField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ErrorCode
        {
            get
            {
                return this.errorCodeField;
            }
            set
            {
                this.errorCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ErrorText
        {
            get
            {
                return this.errorTextField;
            }
            set
            {
                this.errorTextField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:ca:applyalberta:message:ErrorMessage:v1.0.1")]
    public partial class ErrorSourceType
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

    #endregion

    #region Types

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:ca:applyalberta:message:ErrorMessage:v1.0.1")]
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

    #endregion

}