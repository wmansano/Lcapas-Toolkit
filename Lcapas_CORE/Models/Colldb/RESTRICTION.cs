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
    
    public partial class RESTRICTION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RESTRICTION()
        {
            this.RESTRICTIONS_LS = new HashSet<RESTRICTIONS_LS>();
            this.RESTRICTIONS_LS1 = new HashSet<RESTRICTIONS_LS>();
            this.STUDENT_RESTRICTIONS = new HashSet<STUDENT_RESTRICTIONS>();
        }
    
        public string RESTRICTIONS_ID { get; set; }
        public string REST_DESC { get; set; }
        public Nullable<decimal> REST_SEVERITY { get; set; }
        public string REST_USER1 { get; set; }
        public string REST_USER2 { get; set; }
        public string REST_USER3 { get; set; }
        public string REST_USER4 { get; set; }
        public string REST_USER5 { get; set; }
        public string REST_USER6 { get; set; }
        public string REST_USER7 { get; set; }
        public string REST_USER8 { get; set; }
        public string REST_USER9 { get; set; }
        public string REST_USER10 { get; set; }
        public string RESTRICTIONS_ADDOPR { get; set; }
        public Nullable<System.DateTime> RESTRICTIONS_ADDDATE { get; set; }
        public string RESTRICTIONS_CHGOPR { get; set; }
        public Nullable<System.DateTime> RESTRICTIONS_CHGDATE { get; set; }
        public string REST_PRTL_DISPLAY_FLAG { get; set; }
        public string REST_PRTL_DISPLAY_DESC { get; set; }
        public string REST_PRTL_DISPLAY_DESC_DTL { get; set; }
        public string REST_PRTL_FOLLOW_UP_APP { get; set; }
        public string REST_PRTL_FOLLOW_UP_LINK_DEF { get; set; }
        public string REST_PRTL_FOLLOW_UP_WA_FORM { get; set; }
        public string REST_PRTL_FOLLOW_UP_LABEL { get; set; }
        public string REST_PRTL_FOLLOW_UP_IS_MTXT { get; set; }
        public string REST_INTG_CATEGORY { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RESTRICTIONS_LS> RESTRICTIONS_LS { get; set; }
        public virtual RESTRICTION RESTRICTIONS1 { get; set; }
        public virtual RESTRICTION RESTRICTION1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RESTRICTIONS_LS> RESTRICTIONS_LS1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENT_RESTRICTIONS> STUDENT_RESTRICTIONS { get; set; }
    }
}