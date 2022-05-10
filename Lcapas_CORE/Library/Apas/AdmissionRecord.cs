using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Lcapas.Core.Library.Apas.AdmissionsRecord
{

    #region Classes
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class TransmissionDataType
    {

        private string documentIDField;
        private System.DateTime createdDateTimeField;
        private CoreMain.DocumentTypeCodeType documentTypeCodeField;
        private CoreMain.TransmissionTypeType transmissionTypeField;
        private CoreMain.DocumentProcessCodeType documentProcessCodeField;
        private CoreMain.DocumentOfficialCodeType documentOfficialCodeField;
        private CoreMain.DocumentCompleteCodeType documentCompleteCodeField;
        private AcademicRecord.SourceDestinationType sourceField;
        private AcademicRecord.SourceDestinationType destinationField;
        private bool documentProcessCodeFieldSpecified;
        private bool documentOfficialCodeFieldSpecified;
        private bool documentCompleteCodeFieldSpecified;
        private string requestTrackingIDField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AcademicRecord.SourceDestinationType Source
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AcademicRecord.SourceDestinationType Destination
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class ApplicantType
    {

        private ApplicantPersonType personField;
        private FamilyType familyField;
        private List<ApplicationType> applicationField;
        private List<AcademicRecordType> selfReportedAcademicRecordField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ApplicantPersonType Person
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public FamilyType Family
        {
            get
            {
                return this.familyField;
            }
            set
            {
                this.familyField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<ApplicationType> Application
        {
            get
            {
                return this.applicationField;
            }
            set
            {
                this.applicationField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AcademicRecordType> SelfReportedAcademicRecord
        {
            get
            {
                return this.selfReportedAcademicRecordField;
            }
            set
            {
                this.selfReportedAcademicRecordField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class ApplicantPersonType
    {
        private string schoolAssignedPersonIDField;
        private string sINField;
        private string nSNField;
        private string recipientAssignedIDField;
        private string sSNField;
        private List<CoreMain.AgencyIdentifierType> agencyIdentifierField;
        private CoreMain.BirthType birthField;
        private CoreMain.NameType nameField;
        private List<CoreMain.NameType> alternateNameField;
        private CoreMain.NameType preferredNameField;
        private List<CitizenshipType1> citizenshipField;
        private AdmissionsRecord.ContactsType1 contactsField;
        private CoreMain.GenderType genderField;
        private AcademicRecord.ResidencyType1 residencyField;
        private CanadianEthnicityRaceType canadianEthnicityRaceField;
        private ImmigrationType immigrationField;
        private List<string> noteMessageField;
        private CoreMain.LanguageType languageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "token")]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute("AlternateName", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.NameType PreferredName
        {
            get
            {
                return this.preferredNameField;
            }
            set
            {
                this.preferredNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Citizenship", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CitizenshipType1> Citizenship
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
        public AdmissionsRecord.ContactsType1 Contacts
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AcademicRecord.ResidencyType1 Residency
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
        public CanadianEthnicityRaceType CanadianEthnicityRace
        {
            get
            {
                return this.canadianEthnicityRaceField;
            }
            set
            {
                this.canadianEthnicityRaceField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ImmigrationType Immigration
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

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.LanguageType Language
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class ApplicationType
    {

        private ApplicationSourceType applicationSourceField;
        private string applicationIDField;
        private List<ApplicationDetailType> applicationDetailField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ApplicationSourceType ApplicationSource
        {
            get
            {
                return this.applicationSourceField;
            }
            set
            {
                this.applicationSourceField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ApplicationID
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

        /// <remarks/>
        [XmlElementAttribute("ApplicationDetail", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<ApplicationDetailType> ApplicationDetail
        {
            get
            {
                return this.applicationDetailField;
            }
            set
            {
                this.applicationDetailField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class FamilyType
    {
        private AdmissionsPersonType familyMemberField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AdmissionsPersonType FamilyMember
        {
            get
            {
                return this.familyMemberField;
            }
            set
            {
                this.familyMemberField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class AdmissionsPersonType
    {
        private string nameField;
        private CoreMain.AttendedCollegeCodeType? attendedCollegeCodefield;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Name
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.AttendedCollegeCodeType? AttendedCollegeCode
        {
            get
            {
                return this.attendedCollegeCodefield;
            }
            set
            {
                this.attendedCollegeCodefield = value;
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class CanadianEthnicityRaceType
    {

        private CoreMain.CanadianEthnicityCodeType canadianEthnicityCodeField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.CanadianEthnicityCodeType CanadianEthnicityCode
        {
            get
            {
                return this.canadianEthnicityCodeField;
            }
            set
            {
                this.canadianEthnicityCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class ImmigrationType
    {

        private System.DateTime firstEntryIntoHostCountryDateField;
        private VisaDetailType visaDetailField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime FirstEntryIntoHostCountryDate
        {
            get
            {
                return this.firstEntryIntoHostCountryDateField;
            }
            set
            {
                this.firstEntryIntoHostCountryDateField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public VisaDetailType VisaDetail
        {
            get
            {
                return this.visaDetailField;
            }
            set
            {
                this.visaDetailField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class VisaDetailType
    {

        private string nonImmigrantVisaNumberField;
        private System.DateTime? visaExpirationDateField;

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
        public System.DateTime? VisaExpirationDate
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(TypeName = "ContactsType", Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class ContactsType1
    {

        private AddressType1 permanentAddressField;
        private AddressType1 currentAddressField;
        private CoreMain.PhoneType permanentPhoneField;
        private CoreMain.PhoneType dayPhoneField;
        private CoreMain.PhoneType mobilePhoneField;
        private CoreMain.PhoneType preferredPhoneField;
        private EmailType1 primaryEmailField;

        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AddressType1 PermanentAddress
        {
            get
            {
                return this.permanentAddressField;
            }
            set
            {
                this.permanentAddressField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AddressType1 CurrentAddress
        {
            get
            {
                return currentAddressField;
            }
            set
            {
                currentAddressField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.PhoneType PermanentPhone
        {
            get
            {
                return this.permanentPhoneField;
            }
            set
            {
                this.permanentPhoneField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.PhoneType DayPhone
        {
            get
            {
                return this.dayPhoneField;
            }
            set
            {
                this.dayPhoneField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.PhoneType MobilePhone
        {
            get
            {
                return this.mobilePhoneField;
            }
            set
            {
                this.mobilePhoneField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.PhoneType PreferredPhone
        {
            get
            {
                return this.preferredPhoneField;
            }
            set
            {
                this.preferredPhoneField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public EmailType1 PrimaryEmail
        {
            get
            {
                return this.primaryEmailField;
            }
            set
            {
                this.primaryEmailField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return noteMessageField;
            }
            set
            {
                noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(TypeName = "EmailType", Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class EmailType1 : CoreMain.EmailType
    {

        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return noteMessageField;
            }
            set
            {
                noteMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(TypeName = "AddressType", Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class AddressType1 : CoreMain.AddressType
    {

        private List<string> attentionLineField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute("AttentionLine", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> AttentionLine
        {
            get
            {
                return attentionLineField;
            }
            set
            {
                attentionLineField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("NoteMessage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<string> NoteMessage
        {
            get
            {
                return noteMessageField;
            }
            set
            {
                noteMessageField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(TypeName = "CitizenshipType", Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class CitizenshipType1 : CoreMain.CitizenshipType
    {

        private CoreMain.CountryCodeType citizenshipCountryCodeField;
        private bool citizenshipCountryCodeFieldSpecified;
        //private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.CountryCodeType CitizenshipCountryCode
        {
            get
            {
                return this.citizenshipCountryCodeField;
            }
            set
            {
                this.citizenshipCountryCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool CitizenshipCountryCodeSpecified
        {
            get
            {
                return this.citizenshipCountryCodeFieldSpecified;
            }
            set
            {
                this.citizenshipCountryCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        // Note Message is inherited from CoreMain.CitizenshipType
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(TypeName = "CitizenshipType", Namespace = "urn:org:pesc:sector:AcademicRecord:v1.2.0")]
    public partial class CitizenshipType2 : CoreMain.CitizenshipType
    {

        private CoreMain.CountryCodeType citizenshipCountryCodeField;
        private bool citizenshipCountryCodeFieldSpecified;
        //private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.CountryCodeType CitizenshipCountryCode
        {
            get
            {
                return this.citizenshipCountryCodeField;
            }
            set
            {
                this.citizenshipCountryCodeField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool CitizenshipCountryCodeSpecified
        {
            get
            {
                return this.citizenshipCountryCodeFieldSpecified;
            }
            set
            {
                this.citizenshipCountryCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        //NoteMessage is inherited from CoreMain.CitizenshipType
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class AcademicRecordType
    {

        private SchoolType schoolField;
        private System.DateTime firstDateAttendedField;
        private System.DateTime lastDateAttendedField;
        private object schoolAssignedPersonIDField;
        private CoreMain.StudentLevelType studentLevelField;
        private List<AcademicAwardType> academicAwardField;
        private List<AcademicSummaryE1Type1> academicSummaryField;
        private List<AcademicSessionType> academicSessionField;
        private List<CourseType1> courseField;
        private List<CoreMain.AdditionalStudentAchievementsType> additionalStudentAchievementsField;
        private List<string> noteMessageField;
        private System.Xml.XmlElement userDefinedExtensionsField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public SchoolType School
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime FirstDateAttended
        {
            get
            {
                return this.firstDateAttendedField;
            }
            set
            {
                this.firstDateAttendedField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime LastDateAttended
        {
            get
            {
                return this.lastDateAttendedField;
            }
            set
            {
                this.lastDateAttendedField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object SchoolAssignedPersonID
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute("AcademicAward", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute("AcademicSummary", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AcademicSummaryE1Type1> AcademicSummary
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
        [XmlElementAttribute("AcademicSession", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute("Course", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CourseType1> Course
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
        [XmlElementAttribute("AdditionalStudentAchievements", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class ApplicationDetailType
    {

        private CoreMain.AcademicSessionDetailType applicationSessionField;
        private List<CoreMain.AcademicProgramType> applicationDegreeProgramField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.AcademicSessionDetailType ApplicationSession
        {
            get
            {
                return this.applicationSessionField;
            }
            set
            {
                this.applicationSessionField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("ApplicationDegreeProgram", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.AcademicProgramType> ApplicationDegreeProgram
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
    [XmlTypeAttribute(Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
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
        private List<CoreMain.ContactsType> contactsField;
        private List<string> noteMessageField;

        /// <remarks/>
        //[XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        /// <remarks/>
        [XmlElementAttribute("OrganizationName", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        //[XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.2.0")]
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
        //[XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.2.0")]
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
        //[XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.2.0")]
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
        //[XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.2.0")]
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
        //[XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.2.0")]
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
        //[XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.2.0")]
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
        //[XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.2.0")]
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
        //[XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.2.0")]
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
        //[XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.2.0")]
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
        //[XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.2.0")]
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
        //[XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.2.0")]
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
        //[XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Namespace = "urn:org:pesc:core:CoreMain:v1.2.0")]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute("Contacts", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.ContactsType> Contacts
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
    [XmlIncludeAttribute(typeof(AcademicSummaryFType1))]
    [XmlIncludeAttribute(typeof(AcademicSummaryE2Type1))]
    [XmlIncludeAttribute(typeof(AcademicSummaryE1Type1))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(TypeName = "AcademicSummaryBaseType", Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class AcademicSummaryBaseType1
    {

        private CoreMain.AcademicSummaryTypeType academicSummaryTypeField;
        private bool academicSummaryTypeFieldSpecified;
        private CoreMain.CourseCreditLevelType academicSummaryLevelField;
        private bool academicSummaryLevelFieldSpecified;
        private CoreMain.GPAType gPAField;
        private CoreMain.AcademicHonorsType academicHonorsField;
        private string classRankField;
        private string classSizeField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CoreMain.CourseCreditLevelType AcademicSummaryLevel
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
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
    [XmlTypeAttribute(TypeName = "AcademicSummaryFType", Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class AcademicSummaryFType1 : AcademicSummaryBaseType1
    {

        private CoreMain.AcademicProgramType academicProgramField;

        private List<CoreMain.DelinquenciesType> delinquenciesField;

        private System.DateTime exitDateField;

        private bool exitDateFieldSpecified;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute("Delinquencies", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
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
        [XmlIgnoreAttribute()]
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
    [XmlTypeAttribute(TypeName = "AcademicSummaryE2Type", Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class AcademicSummaryE2Type1 : AcademicSummaryBaseType1
    {
        private List<CoreMain.DelinquenciesType> delinquenciesField;
        private System.DateTime exitDateField;
        private bool exitDateFieldSpecified;

        /// <remarks/>
        [XmlElementAttribute("Delinquencies", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
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
        [XmlIgnoreAttribute()]
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
    [XmlTypeAttribute(TypeName = "AcademicSummaryE1Type", Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class AcademicSummaryE1Type1 : AcademicSummaryBaseType1
    {

        private CoreMain.AcademicProgramType academicProgramField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class AcademicSessionType
    {
        private CoreMain.AcademicSessionDetailType academicSessionDetailField;
        private AdmissionsRecord.SchoolType schoolField;
        private CoreMain.StudentLevelType studentLevelField;
        private List<CoreMain.AcademicProgramType> academicProgramField;
        private List<AcademicAwardType> academicAwardField;
        private List<AcademicRecord.CourseType> courseField;
        private List<AcademicRecord.AcademicSummaryFType> academicSummaryField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AdmissionsRecord.SchoolType School
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute("AcademicProgram", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.AcademicProgramType> AcademicProgram
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
        [XmlElementAttribute("AcademicAward", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute("Course", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AcademicRecord.CourseType> Course
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
        [XmlElementAttribute("AcademicSummary", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AcademicRecord.AcademicSummaryFType> AcademicSummary
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
    [XmlTypeAttribute(TypeName = "CourseType", Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public partial class CourseType1
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
        private ItemChoiceType1 itemElementNameField;
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute("CourseCIPCode", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("CourseCSISCode", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("CourseNCESCode", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("CourseSCEDCode", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [XmlElementAttribute("CourseUSISCode", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item
        {
            get
            {
                return itemField;
            }
            set
            {
                itemField = value;
            }
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public ItemChoiceType1 ItemElementName
        {
            get
            {
                return itemElementNameField;
            }
            set
            {
                itemElementNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute("Requirement", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute("Attribute", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute("Proficiency", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute("Licensure", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute("LanguageOfInstruction", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlTypeAttribute(Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
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
        private List<CoreMain.AcademicProgramType> academicAwardProgramField;
        private List<CoreMain.AcademicDegreeRequirementType> academicDegreeRequirementField;
        private List<AcademicSummaryE1Type1> academicSummaryField;
        private List<string> noteMessageField;

        /// <remarks/>
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute("AcademicHonors", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
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
        [XmlIgnoreAttribute()]
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
        [XmlElementAttribute("AcademicAwardProgram", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<CoreMain.AcademicProgramType> AcademicAwardProgram
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
        [XmlElementAttribute("AcademicDegreeRequirement", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [XmlElementAttribute("AcademicSummary", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<AcademicSummaryE1Type1> AcademicSummary
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

    #region types

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0")]
    public enum ApplicationSourceType
    {

        /// <remarks/>
        Institution,

        /// <remarks/>
        Applicant,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "urn:ca:applyalberta:sector:AdmissionsRecord:v1.3.0", IncludeInSchema = false)]
    public enum ItemChoiceType1
    {

        /// <remarks/>
        [XmlEnumAttribute(":CourseCIPCode")]
        CourseCIPCode,

        /// <remarks/>
        [XmlEnumAttribute(":CourseCSISCode")]
        CourseCSISCode,

        /// <remarks/>
        [XmlEnumAttribute(":CourseNCESCode")]
        CourseNCESCode,

        /// <remarks/>
        [XmlEnumAttribute(":CourseSCEDCode")]
        CourseSCEDCode,

        /// <remarks/>
        [XmlEnumAttribute(":CourseUSISCode")]
        CourseUSISCode,
    }


    #endregion
}