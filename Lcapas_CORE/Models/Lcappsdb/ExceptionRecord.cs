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
    
    public partial class ExceptionRecord
    {
        public int StatusTrackingId { get; set; }
        public string Project { get; set; }
        public string Page { get; set; }
        public string Function { get; set; }
        public string Variable { get; set; }
        public string Value { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }
    }
}
