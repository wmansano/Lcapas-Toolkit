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
    
    public partial class TranscriptJob
    {
        public int JobId { get; set; }
        public string JobKey { get; set; }
        public Lcapas.Core.Library.Enums.JobTypeType JobType { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<System.DateTime> StartedDateTime { get; set; }
        public Nullable<System.DateTime> ModifiedDateTime { get; set; }
        public Nullable<System.DateTime> CompletedDateTime { get; set; }
        public Nullable<System.DateTime> JobKilledDateTime { get; set; }
    }
}
