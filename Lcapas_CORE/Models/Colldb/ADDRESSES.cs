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
    
    public partial class ADDRESSES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADDRESSES()
        {
            this.ADDRESS_LS = new HashSet<ADDRESS_LS>();
            this.ADR_PHONES = new HashSet<ADR_PHONES>();
            this.People = new HashSet<PEOPLE>();
            this.People1 = new HashSet<PEOPLE>();
            this.STUDENT_REQUEST_LOGS = new HashSet<STUDENT_REQUEST_LOGS>();
        }
    
        public string ADDRESS_ID { get; set; }
        public string ZIP { get; set; }
        public string STATE { get; set; }
        public string CITY { get; set; }
        public string ADDRESS_FAST_SEARCH_LINE { get; set; }
        public string ADDRESS_CHANGE_SOURCE { get; set; }
        public Nullable<System.DateTime> ADDRESS_ADD_DATE { get; set; }
        public string ADDRESS_ROUTE_CODE { get; set; }
        public string TIME_ZONE { get; set; }
        public string COUNTY { get; set; }
        public string COUNTRY { get; set; }
        public string ADDRESS_ADD_OPERATOR { get; set; }
        public string ADDRESS_CORP_NAME { get; set; }
        public string BUS_ADR_TYPE { get; set; }
        public string DIVISION_BRANCH_NAME { get; set; }
        public Nullable<System.DateTime> ADDRESS_CHANGE_DATE { get; set; }
        public string ADDRESS_CHANGE_OPERATOR { get; set; }
        public string CARRIER_ROUTE { get; set; }
        public string PENDING_ADR_MOVE_KEY { get; set; }
        public Nullable<System.DateTime> ADDRESS_CHANGE_DATE2 { get; set; }
        public string CHANGE_TYPE { get; set; }
        public string ADDRESS_USER1 { get; set; }
        public string ADDRESS_USER2 { get; set; }
        public string ADDRESS_USER3 { get; set; }
        public string ADDRESS_USER4 { get; set; }
        public string ADDRESS_USER5 { get; set; }
        public string ADDRESS_USER6 { get; set; }
        public string ADDRESS_USER7 { get; set; }
        public string ADDRESS_USER8 { get; set; }
        public string ADDRESS_USER9 { get; set; }
        public string ADDRESS_USER10 { get; set; }
        public Nullable<System.DateTime> ADDRESS_ADDTIME { get; set; }
        public Nullable<System.DateTime> ADDRESS_CHGTIME { get; set; }
        public string CORRECTION_DIGIT { get; set; }
        public string DELIVERY_POINT { get; set; }
        public string INTL_LOCALITY { get; set; }
        public string INTL_POSTAL_CODE { get; set; }
        public string INTL_REGION { get; set; }
        public string INTL_SUB_REGION { get; set; }
        public Nullable<decimal> LATITUDE { get; set; }
        public Nullable<decimal> LONGITUDE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADDRESS_LS> ADDRESS_LS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADR_PHONES> ADR_PHONES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEOPLE> People { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEOPLE> People1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENT_REQUEST_LOGS> STUDENT_REQUEST_LOGS { get; set; }
    }
}
