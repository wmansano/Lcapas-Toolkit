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
    
    public partial class COURSE_SECTIONS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public COURSE_SECTIONS()
        {
            this.STUDENT_COURSE_SEC = new HashSet<STUDENT_COURSE_SEC>();
            this.COURSE_SEC_FACULTY = new HashSet<COURSE_SEC_FACULTY>();
            this.COURSE_SECTIONS_LS = new HashSet<COURSE_SECTIONS_LS>();
        }
    
        public string COURSE_SECTIONS_ID { get; set; }
        public string SEC_SHORT_TITLE { get; set; }
        public string SEC_SCHED_TYPE { get; set; }
        public string SEC_LOCATION { get; set; }
        public Nullable<decimal> SEC_MIN_ENROLL { get; set; }
        public Nullable<decimal> SEC_CAPACITY { get; set; }
        public string SEC_SUBJECT { get; set; }
        public Nullable<decimal> SEC_CEUS { get; set; }
        public Nullable<decimal> SEC_MIN_CRED { get; set; }
        public Nullable<decimal> SEC_MAX_CRED { get; set; }
        public string SEC_REG_RETAKE_POLICY { get; set; }
        public string SEC_ONLY_PASS_NOPASS_FLAG { get; set; }
        public Nullable<decimal> SEC_FEE { get; set; }
        public string SEC_TERM { get; set; }
        public string SEC_COURSE_NO { get; set; }
        public string SEC_NO { get; set; }
        public string COURSE_SECTIONS_CHGOPR { get; set; }
        public Nullable<System.DateTime> COURSE_SECTIONS_CHGDATE { get; set; }
        public string COURSE_SECTIONS_ADDOPR { get; set; }
        public Nullable<System.DateTime> COURSE_SECTIONS_ADDDATE { get; set; }
        public Nullable<decimal> SEC_BILLING_CRED { get; set; }
        public string SEC_COURSE { get; set; }
        public string SEC_PETITION_REQD_FLAG { get; set; }
        public string SEC_ACAD_LEVEL { get; set; }
        public string SEC_XLIST { get; set; }
        public string SEC_CRED_TYPE { get; set; }
        public string SEC_ALLOW_PASS_NOPASS_FLAG { get; set; }
        public string SEC_ALLOW_AUDIT_FLAG { get; set; }
        public string SEC_GRADE_SCHEME { get; set; }
        public Nullable<System.DateTime> SEC_START_DATE { get; set; }
        public Nullable<System.DateTime> SEC_END_DATE { get; set; }
        public string SEC_COMMENTS { get; set; }
        public string SEC_TOPIC_CODE { get; set; }
        public string SEC_BILLING_PERIOD_TYPE { get; set; }
        public string SEC_GL_NO { get; set; }
        public string SEC_SYNONYM { get; set; }
        public Nullable<decimal> SEC_CONFLICT_DECIDER { get; set; }
        public Nullable<decimal> SEC_VAR_CRED_INCREMENT { get; set; }
        public Nullable<decimal> SEC_COURSE_COST { get; set; }
        public string SEC_ALLOW_WAITLIST_FLAG { get; set; }
        public Nullable<decimal> SEC_WAITLIST_MAX { get; set; }
        public string SEC_WAITLIST_RATING { get; set; }
        public Nullable<decimal> SEC_SCHED_CAPACITY { get; set; }
        public string SEC_BILLING_METHOD { get; set; }
        public string SEC_DROP_REG_REF_POL { get; set; }
        public Nullable<System.DateTime> SEC_OVR_REG_START_DATE { get; set; }
        public Nullable<System.DateTime> SEC_OVR_REG_END_DATE { get; set; }
        public Nullable<System.DateTime> SEC_OVR_ADD_START_DATE { get; set; }
        public Nullable<System.DateTime> SEC_OVR_ADD_END_DATE { get; set; }
        public Nullable<System.DateTime> SEC_OVR_PREREG_START_DATE { get; set; }
        public Nullable<System.DateTime> SEC_OVR_PREREG_END_DATE { get; set; }
        public Nullable<System.DateTime> SEC_OVR_DROP_END_DATE { get; set; }
        public string SEC_TRANSFER_STATUS { get; set; }
        public string SEC_PURPOSE { get; set; }
        public string SEC_DISABILITY_STATUS { get; set; }
        public string SEC_FACULTY_CONSENT_FLAG { get; set; }
        public string SEC_PRINTED_COMMENTS { get; set; }
        public Nullable<System.DateTime> SEC_OVR_DROP_GR_REQD_DATE { get; set; }
        public string SEC_NAME { get; set; }
        public Nullable<System.DateTime> SEC_OVR_DROP_START_DATE { get; set; }
        public string SEC_LAST_ATTENDANCE_SCHED { get; set; }
        public Nullable<decimal> SEC_NO_WEEKS { get; set; }
        public string SEC_WDRW_REG_REF_POL { get; set; }
        public Nullable<decimal> SEC_DISCOUNT_MAX_PCT { get; set; }
        public Nullable<decimal> SEC_DISCOUNT_MAX_AMT { get; set; }
        public string SEC_CIP { get; set; }
        public string SEC_FUNDING_ACCTG_METHOD { get; set; }
        public string SEC_TIME_BILL_FLAG { get; set; }
        public string SEC_USER1 { get; set; }
        public string SEC_USER2 { get; set; }
        public string SEC_USER3 { get; set; }
        public string SEC_USER4 { get; set; }
        public string SEC_USER5 { get; set; }
        public string SEC_USER6 { get; set; }
        public string SEC_USER7 { get; set; }
        public string SEC_USER8 { get; set; }
        public string SEC_USER9 { get; set; }
        public string SEC_USER10 { get; set; }
        public string SEC_USER11 { get; set; }
        public string SEC_USER12 { get; set; }
        public string SEC_USER13 { get; set; }
        public string SEC_USER14 { get; set; }
        public string SEC_USER15 { get; set; }
        public string SEC_USER16 { get; set; }
        public string SEC_USER17 { get; set; }
        public string SEC_USER18 { get; set; }
        public string SEC_USER19 { get; set; }
        public string SEC_USER20 { get; set; }
        public string SEC_USER21 { get; set; }
        public string SEC_CRNT_STATUS_IDX { get; set; }
        public string SEC_SECTION_TERM_IDX { get; set; }
        public string SEC_R2_EVENT { get; set; }
        public string SEC_SPECIAL_PROPERTY_IND { get; set; }
        public Nullable<decimal> SEC_WAITLIST_NO_DAYS { get; set; }
        public Nullable<System.DateTime> SEC_FIRST_MEETING_DATE { get; set; }
        public Nullable<System.DateTime> SEC_LAST_MEETING_DATE { get; set; }
        public string SEC_MEETING_INFO { get; set; }
        public string SEC_FACULTY_INFO { get; set; }
        public string SEC_PORTAL_SITE { get; set; }
        public string SEC_PRTL_ADD_SITE_FLAG { get; set; }
        public Nullable<System.DateTime> SEC_PRTL_CC_UPDATE_DATE { get; set; }
        public Nullable<System.DateTime> COURSE_SECTIONS_CHGTIME { get; set; }
        public Nullable<System.DateTime> SEC_PRTL_CC_UPDATE_TIME { get; set; }
        public string SEC_LEARNING_PROVIDER { get; set; }
        public string SEC_MEETING_MANUAL_FLAG { get; set; }
        public string SEC_CLOSE_WAITLIST_FLAG { get; set; }
        public Nullable<decimal> SEC_MIN_NO_COREQ_SECS { get; set; }
        public string SEC_OVERRIDE_CRS_REQS_FLAG { get; set; }
        public string SEC_REQS_CO_LOCATE_FLAG { get; set; }
        public string SEC_HIDE_IN_CATALOG { get; set; }
        public string SEC_ADD_AUTH_EXCLUSION_FLAG { get; set; }
        public string SEC_ATTEND_TRACKING_TYPE { get; set; }
        public string SEC_GRADE_SUBSCHEMES_ID { get; set; }
        public string SEC_SHOW_DROP_ROSTER_FLAG { get; set; }
        public string SEC_GRADE_BY_RANDOM_ID_FLAG { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENT_COURSE_SEC> STUDENT_COURSE_SEC { get; set; }
        public virtual GRADE_SCHEMES GRADE_SCHEMES { get; set; }
        public virtual TERM TERM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COURSE_SEC_FACULTY> COURSE_SEC_FACULTY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COURSE_SECTIONS_LS> COURSE_SECTIONS_LS { get; set; }
    }
}
