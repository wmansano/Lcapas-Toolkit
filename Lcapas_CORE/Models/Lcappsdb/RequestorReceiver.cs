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
    
    public partial class RequestorReceiver
    {
        public int RequestorReceiverId { get; set; }
        public string ReceiverType { get; set; }
        public string Receiver { get; set; }
    
        public virtual Recipient Recipient { get; set; }
    }
}
