//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lcapas.Core.Models.Colldb
{
    using System;
    using System.Collections.Generic;
    
    public partial class PEOPLE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PEOPLE()
        {
            this.ADDRESS_LS = new HashSet<ADDRESS_LS>();
            this.ADDRESS_LS1 = new HashSet<ADDRESS_LS>();
            this.APPLICATIONS = new HashSet<APPLICATION>();
            this.APPLICATIONS1 = new HashSet<APPLICATION>();
            this.APPLICATIONS2 = new HashSet<APPLICATION>();
            this.NAMEHISTs = new HashSet<NAMEHISTS>();
            this.PEOPLE_EMAIL = new HashSet<PEOPLE_EMAIL>();
            this.PERSON1 = new HashSet<PEOPLE>();
            this.PERSON_ALT = new HashSet<PERSON_ALT>();
            this.PERSON11 = new HashSet<PEOPLE>();
            this.STUDENT_ACAD_CRED = new HashSet<STUDENT_ACAD_CRED>();
            this.STUDENT_COURSE_SEC = new HashSet<STUDENT_COURSE_SEC>();
            this.STUDENT_STANDINGS = new HashSet<STUDENT_STANDINGS>();
            this.PERSON_ST_LS = new HashSet<PERSON_ST_LS>();
            this.STUDENT_EQUIV_EVALS = new HashSet<STUDENT_EQUIV_EVALS>();
            this.STUDENT_EQUIV_EVALS1 = new HashSet<STUDENT_EQUIV_EVALS>();
            this.INSTITUTIONS = new HashSet<INSTITUTION>();
            this.STUDENT_REQUEST_LOGS = new HashSet<STUDENT_REQUEST_LOGS>();
            this.CH_CORR = new HashSet<CH_CORR>();
            this.STUDENT_NON_COURSES = new HashSet<STUDENT_NON_COURSES>();
            this.PERPHONEs = new HashSet<PERPHONE>();
            this.ACAD_CREDENTIALS = new HashSet<ACAD_CREDENTIALS>();
        }
    
        public string ID { get; set; }
        public string LAST_NAME { get; set; }
        public string SOURCE { get; set; }
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string PREFIX { get; set; }
        public string PREFERRED_ADDRESS { get; set; }
        public string JOINT_PERSON { get; set; }
        public string SSN { get; set; }
        public string PERSON_STATUS { get; set; }
        public string GENDER { get; set; }
        public string MARITAL_STATUS { get; set; }
        public Nullable<System.DateTime> BIRTH_DATE { get; set; }
        public string PREFERRED_NAME { get; set; }
        public string PERSON_ADD_OPERATOR { get; set; }
        public string SPOUSE { get; set; }
        public string NICKNAME { get; set; }
        public string ETHNIC { get; set; }
        public string PERSON_CHANGE_OPERATOR { get; set; }
        public Nullable<System.DateTime> PERSON_CHANGE_DATE { get; set; }
        public Nullable<System.DateTime> PERSON_ADD_DATE { get; set; }
        public string SUFFIX { get; set; }
        public string PERSON_NATIVE_LANGUAGE { get; set; }
        public string ANONYMOUS { get; set; }
        public Nullable<System.DateTime> DECEASED_DATE { get; set; }
        public string BIRTH_NAME_LAST { get; set; }
        public string BIRTH_NAME_FIRST { get; set; }
        public string BIRTH_NAME_MIDDLE { get; set; }
        public string PREFERRED_RESIDENCE { get; set; }
        public string PERSON_BEN_ID { get; set; }
        public string INCOME_LEVEL { get; set; }
        public string PREF_BUS_ADDRESS { get; set; }
        public Nullable<System.DateTime> PERSON_COUNTRY_ENTRY_DATE { get; set; }
        public string PREMIUM_PREFERENCE { get; set; }
        public string PERSON_CORP_INDICATOR { get; set; }
        public string RESIDENCE_COUNTRY { get; set; }
        public string BOX { get; set; }
        public string PERSON_DONOR_TYPE { get; set; }
        public string PERSON_GROUP_ID { get; set; }
        public string PREFERRED_LISTING { get; set; }
        public string OBITUARY { get; set; }
        public string MEMORIAL_TYPE { get; set; }
        public Nullable<System.DateTime> PERSON_ORIGIN_DATE { get; set; }
        public string PERSON_ORIGIN_CODE { get; set; }
        public string PERSON_USER1 { get; set; }
        public string PERSON_USER2 { get; set; }
        public string PERSON_USER3 { get; set; }
        public string PERSON_USER4 { get; set; }
        public string PERSON_USER5 { get; set; }
        public string PERSON_USER_CHANGED_BY { get; set; }
        public Nullable<System.DateTime> PERSON_USER_CHANGED_DATE { get; set; }
        public string PERSON_USER6 { get; set; }
        public string PERSON_USER7 { get; set; }
        public string PERSON_USER8 { get; set; }
        public string PERSON_USER9 { get; set; }
        public string PERSON_USER10 { get; set; }
        public string PERSON_MERGED_TO_ID { get; set; }
        public string ANNUITY_ADDRESS { get; set; }
        public string PERSON_BIRTH_PLACE { get; set; }
        public string DENOMINATION { get; set; }
        public string RFA_SEGMENT { get; set; }
        public string PERSON_VIP { get; set; }
        public string POLITICAL_PARTY { get; set; }
        public Nullable<System.DateTime> VISA_ISSUED_DATE { get; set; }
        public string OCCUPATION { get; set; }
        public string PERSON_OVERRIDE_SALUTATION { get; set; }
        public string PERSON_FAMILY_SIZE { get; set; }
        public string GUARDIANS { get; set; }
        public string PERSON_OVRL_EMP_STAT { get; set; }
        public string PARTICIPANT_TYPE { get; set; }
        public string RESIDENCE_COUNTY { get; set; }
        public string RESIDENCE_STATE { get; set; }
        public string VISA_TYPE { get; set; }
        public Nullable<System.DateTime> VISA_EXP_DATE { get; set; }
        public string ALIEN_ID { get; set; }
        public string ALIEN_FLAG { get; set; }
        public string SELECTIVE_SERVICE_FLAG { get; set; }
        public string SELECTIVE_SERVICE_NUMBER { get; set; }
        public string CITIZENSHIP { get; set; }
        public string EMER_CONTACT_NAME { get; set; }
        public string EMER_CONTACT_PHONE { get; set; }
        public string DIRECTORY_FLAG { get; set; }
        public string PRIVACY_FLAG { get; set; }
        public string IMMIGRATION_STATUS { get; set; }
        public string PREF_EMPLOYMENT { get; set; }
        public string PERSON_PRIMARY_LANGUAGE { get; set; }
        public string AARS { get; set; }
        public string DRIVER_LICENSE_NO { get; set; }
        public string DRIVER_LICENSE_STATE { get; set; }
        public Nullable<decimal> PERSON_HOME_LANG_SCH_NO_YRS { get; set; }
        public string PERSON_HOME_LANG_SCH_COUNTRY { get; set; }
        public string PERSON_ACHIEVEMENTS { get; set; }
        public string PERSON_WEBSITE_ADDRESS { get; set; }
        public string PERSON_CAMPUS_ORGS_ID { get; set; }
        public Nullable<decimal> PERSON_ADDR_CHG_CTR { get; set; }
        public string SORT_NAME_IDX { get; set; }
        public Nullable<System.DateTime> PERSON_ADDTIME { get; set; }
        public Nullable<System.DateTime> PERSON_CHGTIME { get; set; }
        public string GENDER_IDENTITY { get; set; }
        public string PERSONAL_PRONOUN { get; set; }
        public string PERSON_CHOSEN_FIRST_NAME { get; set; }
        public string PERSON_CHOSEN_LAST_NAME { get; set; }
        public string PERSON_CHOSEN_MIDDLE_NAME { get; set; }
        public string PERSON_JATI_ID { get; set; }
        public string PERSON_SEXUAL_ORIENTATION { get; set; }
    
        public virtual ADDRESSES ADDRESS { get; set; }
        public virtual ADDRESSES ADDRESS1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADDRESS_LS> ADDRESS_LS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADDRESS_LS> ADDRESS_LS1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPLICATION> APPLICATIONS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPLICATION> APPLICATIONS1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPLICATION> APPLICATIONS2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NAMEHISTS> NAMEHISTs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEOPLE_EMAIL> PEOPLE_EMAIL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEOPLE> PERSON1 { get; set; }
        public virtual PEOPLE PERSON2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERSON_ALT> PERSON_ALT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEOPLE> PERSON11 { get; set; }
        public virtual PEOPLE PERSON3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENT_ACAD_CRED> STUDENT_ACAD_CRED { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENT_COURSE_SEC> STUDENT_COURSE_SEC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENT_STANDINGS> STUDENT_STANDINGS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERSON_ST_LS> PERSON_ST_LS { get; set; }
        public virtual PERSON_ST PERSON_ST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENT_EQUIV_EVALS> STUDENT_EQUIV_EVALS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENT_EQUIV_EVALS> STUDENT_EQUIV_EVALS1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INSTITUTION> INSTITUTIONS { get; set; }
        public virtual INSTITUTION INSTITUTION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENT_REQUEST_LOGS> STUDENT_REQUEST_LOGS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CH_CORR> CH_CORR { get; set; }
        public virtual FOREIGN_PERSON FOREIGN_PERSON { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENT_NON_COURSES> STUDENT_NON_COURSES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERPHONE> PERPHONEs { get; set; }
        public virtual MAILING MAILING { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACAD_CREDENTIALS> ACAD_CREDENTIALS { get; set; }
    }
}