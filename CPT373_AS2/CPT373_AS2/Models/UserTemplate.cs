//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CPT373_AS2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserTemplate
    {
        public int UserTemplateID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Cells { get; set; }
    
        public virtual User User { get; set; }
    }
}
