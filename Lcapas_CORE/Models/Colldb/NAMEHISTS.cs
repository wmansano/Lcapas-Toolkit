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
    
    public partial class NAMEHISTS
    {
        public string ID { get; set; }
        public decimal POS { get; set; }
        public string NAME_HISTORY_LAST_NAME { get; set; }
        public string NAME_HISTORY_FIRST_NAME { get; set; }
        public string NAME_HISTORY_MIDDLE_NAME { get; set; }
    
        public virtual PEOPLE PERSON { get; set; }
    }
}