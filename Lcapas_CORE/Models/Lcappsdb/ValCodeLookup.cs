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
    
    public partial class ValCodeLookup
    {
        public int ApasCodeId { get; set; }
        public int ValCodeId { get; set; }
        public int CodeTypeId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }
    
        public virtual ApasCode ApasCode { get; set; }
        public virtual CodeType CodeType { get; set; }
        public virtual ValCode ValCode { get; set; }
    }
}