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
    
    public partial class TransactionTranscript
    {
        public int TransactionTranscriptId { get; set; }
        public string TransactionTranscriptUuid { get; set; }
        public string StudentId { get; set; }
        public string TranscriptGrouping { get; set; }
        public string TranscriptText { get; set; }
        public Nullable<System.DateTime> CompletedDateTime { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<System.DateTime> ModifiedDateTime { get; set; }
    }
}
