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
    
    public partial class ACAD_CREDENTIALS_LS
    {
        public string ACAD_CREDENTIALS_ID { get; set; }
        public decimal POS { get; set; }
        public string ACAD_CCD { get; set; }
        public Nullable<System.DateTime> ACAD_CCD_DATE { get; set; }
        public string ACAD_MAJORS { get; set; }
        public string ACAD_MINORS { get; set; }
        public string ACAD_SPECIALIZATION { get; set; }
        public string ACAD_HONORS { get; set; }
        public string ACAD_AWARDS { get; set; }
        public string ACAD_TRANSCRIPT_ADDRESS { get; set; }
    
        public virtual ACAD_CREDENTIALS ACAD_CREDENTIALS { get; set; }
    }
}
