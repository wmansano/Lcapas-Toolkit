﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class colldb_entities : DbContext
    {
        public colldb_entities()
            : base("name=colldb_entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ADDRESSES> ADDRESS { get; set; }
        public virtual DbSet<ADDRESS_LS> ADDRESS_LS { get; set; }
        public virtual DbSet<ADR_PHONES> ADR_PHONES { get; set; }
        public virtual DbSet<APPL_STATUSES> APPL_STATUSES { get; set; }
        public virtual DbSet<APPLICATION> APPLICATIONS { get; set; }
        public virtual DbSet<COURSE_SECTIONS> COURSE_SECTIONS { get; set; }
        public virtual DbSet<GRADE> GRADES { get; set; }
        public virtual DbSet<NAMEHISTS> NAMEHIST { get; set; }
        public virtual DbSet<PEOPLE_EMAIL> PEOPLE_EMAIL { get; set; }
        public virtual DbSet<PEOPLE> PERSON { get; set; }
        public virtual DbSet<PERSON_ALT> PERSON_ALT { get; set; }
        public virtual DbSet<STC_STATUSES> STC_STATUSES { get; set; }
        public virtual DbSet<STPR_DATES> STPR_DATES { get; set; }
        public virtual DbSet<STPR_STATUSES> STPR_STATUSES { get; set; }
        public virtual DbSet<STUDENT_ACAD_CRED> STUDENT_ACAD_CRED { get; set; }
        public virtual DbSet<STUDENT_COURSE_SEC> STUDENT_COURSE_SEC { get; set; }
        public virtual DbSet<STUDENT_STANDINGS> STUDENT_STANDINGS { get; set; }
        public virtual DbSet<STUDENT_TERMS> STUDENT_TERMS { get; set; }
        public virtual DbSet<APPLICATIONS_LS> APPLICATIONS_LS { get; set; }
        public virtual DbSet<EXTERNAL_NOTES> EXTERNAL_NOTES { get; set; }
        public virtual DbSet<EXTERNAL_TRANSCRIPTS> EXTERNAL_TRANSCRIPTS { get; set; }
        public virtual DbSet<EXTERNAL_TRANSCRIPTS_LS> EXTERNAL_TRANSCRIPTS_LS { get; set; }
        public virtual DbSet<GRADE_SCHEMES> GRADE_SCHEMES { get; set; }
        public virtual DbSet<GRADE_SCHEMES_LS> GRADE_SCHEMES_LS { get; set; }
        public virtual DbSet<PERSON_ST> PERSON_ST { get; set; }
        public virtual DbSet<PERSON_ST_LS> PERSON_ST_LS { get; set; }
        public virtual DbSet<STUDENT_EQUIV_EVALS> STUDENT_EQUIV_EVALS { get; set; }
        public virtual DbSet<STUDENT_EQUIV_EVALS_LS> STUDENT_EQUIV_EVALS_LS { get; set; }
        public virtual DbSet<RESTRICTION> RESTRICTIONS { get; set; }
        public virtual DbSet<RESTRICTIONS_LS> RESTRICTIONS_LS { get; set; }
        public virtual DbSet<STUDENT_RESTRICTIONS> STUDENT_RESTRICTIONS { get; set; }
        public virtual DbSet<RULES_CHECK> RULES_CHECK { get; set; }
        public virtual DbSet<INSTITUTION> INSTITUTIONS { get; set; }
        public virtual DbSet<STUDENT_REQUEST_LOGS> STUDENT_REQUEST_LOGS { get; set; }
        public virtual DbSet<STUDENT_REQUEST_LOGS_LS> STUDENT_REQUEST_LOGS_LS { get; set; }
        public virtual DbSet<APPL_LOCATION_INFO> APPL_LOCATION_INFO { get; set; }
        public virtual DbSet<CH_CORR> CH_CORR { get; set; }
        public virtual DbSet<FOREIGN_PERSON> FOREIGN_PERSON { get; set; }
        public virtual DbSet<STUDENT_NON_COURSES> STUDENT_NON_COURSES { get; set; }
        public virtual DbSet<PERPHONE> PERPHONE { get; set; }
        public virtual DbSet<MAILING> MAILING { get; set; }
        public virtual DbSet<CC_COMMENTS> CC_COMMENTS { get; set; }
        public virtual DbSet<APPLICATION_STATUSES> APPLICATION_STATUSES { get; set; }
        public virtual DbSet<ACAD_PROGRAMS> ACAD_PROGRAMS { get; set; }
        public virtual DbSet<ACAD_PROGRAMS_LS> ACAD_PROGRAMS_LS { get; set; }
        public virtual DbSet<TERM> TERMS { get; set; }
        public virtual DbSet<STUDENT_PROGRAMS> STUDENT_PROGRAMS { get; set; }
        public virtual DbSet<STUDENT_ACAD_CRED_LS> STUDENT_ACAD_CRED_LS { get; set; }
        public virtual DbSet<DEPT> DEPTS { get; set; }
        public virtual DbSet<VAL> VALS { get; set; }
        public virtual DbSet<ACAD_CREDENTIALS> ACAD_CREDENTIALS { get; set; }
        public virtual DbSet<ACAD_CREDENTIALS_LS> ACAD_CREDENTIALS_LS { get; set; }
        public virtual DbSet<COURSE_SEC_FACULTY> COURSE_SEC_FACULTY { get; set; }
        public virtual DbSet<COURSE_SECTIONS_LS> COURSE_SECTIONS_LS { get; set; }
    }
}