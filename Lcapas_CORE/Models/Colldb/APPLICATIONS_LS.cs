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
    
    public partial class APPLICATIONS_LS
    {
        public string APPLICATIONS_ID { get; set; }
        public decimal POS { get; set; }
        public string APPL_INFLUENCED_TO_APPLY { get; set; }
        public string APPL_CONTACTS { get; set; }
        public string APPL_REMINDERS { get; set; }
        public string APPL_EXTERNAL_TRANSCRIPTS { get; set; }
        public string APPL_RULE_FAIL_MSGS { get; set; }
        public string APPL_ACTIVECAMPUS_IDS { get; set; }
        public string APPL_STATUS_DATE_TIME_IDX { get; set; }
        public string APPL_FA_MERIT_AWARDS { get; set; }
        public string APPL_FA_NON_MERIT_AWARDS { get; set; }
        public string APPL_INTG_CAREER_GOALS { get; set; }
    
        public virtual APPLICATION APPLICATION { get; set; }
        public virtual EXTERNAL_TRANSCRIPTS EXTERNAL_TRANSCRIPTS { get; set; }
    }
}
