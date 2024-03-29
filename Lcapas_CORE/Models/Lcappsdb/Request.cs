//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lcapas.Core.Models.Lcappsdb
{
    using System;
    using System.Collections.Generic;
    
    public partial class Request
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Request()
        {
            this.Recipients = new HashSet<Recipient>();
        }
    
        public int RequestId { get; set; }
        public Nullable<System.DateTime> SentDateTime { get; set; }
        public Nullable<System.DateTime> ReceivedDateTime { get; set; }
        public Nullable<System.DateTime> ViewedDateTime { get; set; }
        public Nullable<System.DateTime> SentToColleagueTRRQ { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }
        public int TransmissionData_TransmissionDataId { get; set; }
        public int Student_StudentId { get; set; }
        public Nullable<int> RequestorInstitution_InstitutionId { get; set; }
        public Nullable<int> Matched_StudentId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recipient> Recipients { get; set; }
        public virtual Student RequestedStudent { get; set; }
        public virtual TransmissionData TransmissionData { get; set; }
        public virtual Institution Institution { get; set; }
        public virtual Student MatchedStudent { get; set; }
    }
}
