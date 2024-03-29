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
    
    public partial class ACAD_PROGRAMS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACAD_PROGRAMS()
        {
            this.ACAD_PROGRAMS_LS = new HashSet<ACAD_PROGRAMS_LS>();
            this.ACAD_PROGRAMS_LS1 = new HashSet<ACAD_PROGRAMS_LS>();
            this.APPLICATIONS = new HashSet<APPLICATION>();
        }
    
        public string ACAD_PROGRAMS_ID { get; set; }
        public string ACPG_DESC { get; set; }
        public string ACPG_TITLE { get; set; }
        public string ACPG_DEGREE { get; set; }
        public string ACPG_ACAD_LEVEL { get; set; }
        public Nullable<System.DateTime> ACPG_ACCRED_EXPIRE_DATE { get; set; }
        public string ACPG_STUDENT_SELECT_FLAG { get; set; }
        public Nullable<System.DateTime> ACPG_START_DATE { get; set; }
        public Nullable<System.DateTime> ACPG_END_DATE { get; set; }
        public string ACPG_COMMENTS { get; set; }
        public string ACAD_PROGRAMS_ADDOPR { get; set; }
        public Nullable<System.DateTime> ACAD_PROGRAMS_ADDDATE { get; set; }
        public string ACAD_PROGRAMS_CHGOPR { get; set; }
        public Nullable<System.DateTime> ACAD_PROGRAMS_CHGDATE { get; set; }
        public string ACPG_ALLOW_GRADUATION_FLAG { get; set; }
        public string ACPG_GRADE_SCHEME { get; set; }
        public string ACPG_CIP { get; set; }
        public string ACPG_TRANSCRIPT_GROUPING { get; set; }
        public Nullable<decimal> ACPG_CMPL_MONTHS { get; set; }
        public string ACPG_HONORS_CODE { get; set; }
        public string ACPG_SESSION_TYPE { get; set; }
        public Nullable<decimal> ACPG_CMPL_SESSIONS { get; set; }
        public string ACPG_CREATE_APPLICATION_FLAG { get; set; }
        public string ACPG_USER1 { get; set; }
        public string ACPG_USER2 { get; set; }
        public string ACPG_USER3 { get; set; }
        public string ACPG_USER4 { get; set; }
        public string ACPG_USER5 { get; set; }
        public string ACPG_USER6 { get; set; }
        public string ACPG_USER7 { get; set; }
        public string ACPG_USER8 { get; set; }
        public string ACPG_USER9 { get; set; }
        public string ACPG_USER10 { get; set; }
        public Nullable<decimal> ACPG_ADMIT_CAPACITY { get; set; }
        public Nullable<decimal> ACPG_TERM_LENGTH { get; set; }
        public string ACPG_HOME_LANG_NOT_REQD_RSN { get; set; }
        public string ACPG_ARMY_CAREER_DEG_FLAG { get; set; }
        public Nullable<decimal> ACPG_FA_PUB_MONTHS { get; set; }
        public Nullable<decimal> ACPG_FA_PUB_WEEKS { get; set; }
        public Nullable<decimal> ACPG_FA_PUB_YEARS { get; set; }
        public string ACPG_UNOFF_TRANS_GROUPING { get; set; }
        public string ACPG_OFFICIAL_TRANS_GROUPING { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACAD_PROGRAMS_LS> ACAD_PROGRAMS_LS { get; set; }
        public virtual GRADE_SCHEMES GRADE_SCHEMES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACAD_PROGRAMS_LS> ACAD_PROGRAMS_LS1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPLICATION> APPLICATIONS { get; set; }
    }
}
