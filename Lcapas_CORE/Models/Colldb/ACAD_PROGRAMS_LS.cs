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
    
    public partial class ACAD_PROGRAMS_LS
    {
        public string ACAD_PROGRAMS_ID { get; set; }
        public decimal POS { get; set; }
        public string ACPG_CATALOGS { get; set; }
        public string ACPG_CCDS { get; set; }
        public string ACPG_MAJORS { get; set; }
        public string ACPG_MINORS { get; set; }
        public string ACPG_SPECIALIZATIONS { get; set; }
        public string ACPG_LOCATIONS { get; set; }
        public string ACPG_DEPTS { get; set; }
        public string ACPG_ADDNL_CCDS { get; set; }
        public string ACPG_ADDNL_MAJORS { get; set; }
        public string ACPG_ADDNL_MINORS { get; set; }
        public string ACPG_ADDNL_SPECIALIZATIONS { get; set; }
        public string ACPG_LOCAL_GOVT_CODES { get; set; }
        public string ACPG_TYPES { get; set; }
        public string ACPG_EXTL_TRAN_GPA1_RULES { get; set; }
        public string ACPG_EXTL_TRAN_GPA2_RULES { get; set; }
        public string ACPG_EXTL_TRAN_GPA3_RULES { get; set; }
        public string ACPG_FUNDING_SOURCES { get; set; }
        public string ACPG_RELATED_PROGRAMS { get; set; }
        public string ACPG_GRAD_APP_RULES { get; set; }
    
        public virtual ACAD_PROGRAMS ACAD_PROGRAMS { get; set; }
        public virtual ACAD_PROGRAMS ACAD_PROGRAMS1 { get; set; }
        public virtual DEPT DEPT { get; set; }
    }
}
