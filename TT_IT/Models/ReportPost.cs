//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TT_IT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReportPost
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public int PostID { get; set; }
        public System.DateTime DateReport { get; set; }
    
        public virtual Post Post { get; set; }
    }
}
