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
    
    public partial class SvcSched
    {
        public int SvcSchedId { get; set; }
        public System.TimeSpan SvcStartTime { get; set; }
        public Nullable<int> SvcJobs_SvcJobId { get; set; }
    
        public virtual SvcJob SvcJob { get; set; }
    }
}
