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
    
    public partial class DEPT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DEPT()
        {
            this.ACAD_PROGRAMS_LS = new HashSet<ACAD_PROGRAMS_LS>();
            this.STUDENT_ACAD_CRED_LS = new HashSet<STUDENT_ACAD_CRED_LS>();
            this.STUDENT_PROGRAMS = new HashSet<STUDENT_PROGRAMS>();
        }
    
        public string DEPTS_ID { get; set; }
        public string DEPTS_DESC { get; set; }
        public string DEPTS_DIVISION { get; set; }
        public string DEPTS_SCHOOL { get; set; }
        public string DEPTS_ACTIVE_FLAG { get; set; }
        public string DEPTS_ADD_OPERATOR { get; set; }
        public Nullable<System.DateTime> DEPTS_ADD_DATE { get; set; }
        public string DEPTS_CHANGE_OPERATOR { get; set; }
        public Nullable<System.DateTime> DEPTS_CHANGE_DATE { get; set; }
        public string DEPTS_USER1 { get; set; }
        public string DEPTS_USER2 { get; set; }
        public string DEPTS_USER3 { get; set; }
        public string DEPTS_USER4 { get; set; }
        public string DEPTS_USER5 { get; set; }
        public string DEPTS_USER6 { get; set; }
        public string DEPTS_USER7 { get; set; }
        public string DEPTS_USER8 { get; set; }
        public string DEPTS_USER9 { get; set; }
        public string DEPTS_USER10 { get; set; }
        public string DEPTS_ALT_ID { get; set; }
        public string DEPTS_USER11 { get; set; }
        public string DEPTS_USER12 { get; set; }
        public string DEPTS_USER13 { get; set; }
        public string DEPTS_USER14 { get; set; }
        public string DEPTS_USER15 { get; set; }
        public string DEPTS_USER16 { get; set; }
        public string DEPTS_TERMINAL_DEGREE { get; set; }
        public string DEPTS_ACAD_LEVEL { get; set; }
        public string DEPTS_GRADE_SCHEME { get; set; }
        public string DEPTS_HEAD_ID { get; set; }
        public string DEPTS_FAC_DESIRED_DEGREE { get; set; }
        public string DEPTS_REG_RETAKE_POLICY { get; set; }
        public string DEPTS_CIP { get; set; }
        public string DEPTS_TYPE { get; set; }
        public string DEPTS_PAYROLL_SORT_CODE { get; set; }
        public string DEPTS_INSTITUTIONS_ID { get; set; }
        public Nullable<decimal> DEPTS_CONFLICT_DECIDER { get; set; }
        public string DEPT_PRTL_FAC_PERMISSION { get; set; }
        public string DEPT_PRTL_STU_PERMISSION { get; set; }
        public string DEPT_INTG_KEY_IDX { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACAD_PROGRAMS_LS> ACAD_PROGRAMS_LS { get; set; }
        public virtual DEPT DEPTS1 { get; set; }
        public virtual DEPT DEPT1 { get; set; }
        public virtual INSTITUTION INSTITUTION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENT_ACAD_CRED_LS> STUDENT_ACAD_CRED_LS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENT_PROGRAMS> STUDENT_PROGRAMS { get; set; }
    }
}