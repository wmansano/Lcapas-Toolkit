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
    
    public partial class PermissionRecord
    {
        public int PermissionRecordId { get; set; }
        public string PermissionRecordNote { get; set; }
        public bool Enabled { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }
        public int ActionType_ActionId { get; set; }
        public int AccessGroup_AccessGroupId { get; set; }
    
        public virtual AccessGroup AccessGroup { get; set; }
        public virtual ActionType ActionType { get; set; }
    }
}
