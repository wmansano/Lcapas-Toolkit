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
    
    public partial class EXTERNAL_TRANSCRIPTS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXTERNAL_TRANSCRIPTS()
        {
            this.APPLICATIONS_LS = new HashSet<APPLICATIONS_LS>();
            this.EXTERNAL_NOTES = new HashSet<EXTERNAL_NOTES>();
            this.EXTERNAL_TRANSCRIPTS_LS = new HashSet<EXTERNAL_TRANSCRIPTS_LS>();
            this.PERSON_ST_LS = new HashSet<PERSON_ST_LS>();
            this.STUDENT_EQUIV_EVALS_LS = new HashSet<STUDENT_EQUIV_EVALS_LS>();
        }
    
        public string EXTERNAL_TRANSCRIPTS_ID { get; set; }
        public string EXTL_PERSON_ID { get; set; }
        public string EXTL_INSTITUTION { get; set; }
        public string EXTL_COURSE { get; set; }
        public string EXTL_TITLE { get; set; }
        public Nullable<decimal> EXTL_CRED { get; set; }
        public string EXTL_GRADE { get; set; }
        public string EXTL_STUDENT_EQUIV_EVAL { get; set; }
        public string EXTL_COMMENTS { get; set; }
        public string EXTL_GRADE_SCHEME { get; set; }
        public string EXTL_TERM { get; set; }
        public Nullable<System.DateTime> EXTL_START_DATE { get; set; }
        public Nullable<System.DateTime> EXTL_END_DATE { get; set; }
        public string EXTL_CATEGORY { get; set; }
        public string EXTERNAL_TRANSCRIPTS_ADDOPR { get; set; }
        public Nullable<System.DateTime> EXTERNAL_TRANSCRIPTS_ADDDATE { get; set; }
        public string EXTERNAL_TRANSCRIPTS_CHGOPR { get; set; }
        public Nullable<System.DateTime> EXTERNAL_TRANSCRIPTS_CHGDATE { get; set; }
        public string EXTL_USER1 { get; set; }
        public string EXTL_USER2 { get; set; }
        public string EXTL_USER3 { get; set; }
        public string EXTL_USER4 { get; set; }
        public string EXTL_USER5 { get; set; }
        public string EXTL_USER6 { get; set; }
        public string EXTL_USER7 { get; set; }
        public string EXTL_USER8 { get; set; }
        public string EXTL_USER9 { get; set; }
        public string EXTL_USER10 { get; set; }
        public string EXTL_INTERIM_GRADE { get; set; }
        public Nullable<System.DateTime> EXTL_INTERIM_GRADE_DATE { get; set; }
        public string EXTL_PERSON_INSTITUTION_IDX { get; set; }
        public string EXTL_STATUS { get; set; }
        public string EXTL_EDI_FLAG { get; set; }
        public string EXTL_SOURCE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPLICATIONS_LS> APPLICATIONS_LS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXTERNAL_NOTES> EXTERNAL_NOTES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXTERNAL_TRANSCRIPTS_LS> EXTERNAL_TRANSCRIPTS_LS { get; set; }
        public virtual GRADE GRADE { get; set; }
        public virtual GRADE_SCHEMES GRADE_SCHEMES { get; set; }
        public virtual GRADE GRADE1 { get; set; }
        public virtual PERSON_ST PERSON_ST { get; set; }
        public virtual STUDENT_EQUIV_EVALS STUDENT_EQUIV_EVALS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERSON_ST_LS> PERSON_ST_LS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENT_EQUIV_EVALS_LS> STUDENT_EQUIV_EVALS_LS { get; set; }
        public virtual INSTITUTION INSTITUTION { get; set; }
    }
}
