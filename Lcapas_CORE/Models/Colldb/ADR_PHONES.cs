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
    
    public partial class ADR_PHONES
    {
        public string ADDRESS_ID { get; set; }
        public decimal POS { get; set; }
        public string ADDRESS_PHONES { get; set; }
        public string ADDRESS_PHONE_TYPE { get; set; }
        public string ADDRESS_PHONE_EXTENSION { get; set; }
    
        public virtual ADDRESSES ADDRESS { get; set; }
    }
}