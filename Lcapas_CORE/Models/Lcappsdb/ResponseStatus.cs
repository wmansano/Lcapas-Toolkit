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
    
    public partial class ResponseStatus
    {
        public int ResponseStatusId { get; set; }
        public Nullable<Lcapas.Core.Library.Apas.AcademicRecord.ResponseStatusType> ResponseStatusType { get; set; }
        public string ResponseStatusData { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }
        public int Recipient_RecipientId { get; set; }
    
        public virtual Recipient Recipient { get; set; }
    }
}