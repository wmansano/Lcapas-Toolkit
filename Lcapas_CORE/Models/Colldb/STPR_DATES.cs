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
    
    public partial class STPR_DATES
    {
        public string STUDENT_PROGRAMS_ID { get; set; }
        public decimal POS { get; set; }
        public Nullable<System.DateTime> STPR_START_DATE { get; set; }
        public Nullable<System.DateTime> STPR_END_DATE { get; set; }
    
        public virtual STUDENT_PROGRAMS STUDENT_PROGRAMS { get; set; }
    }
}
