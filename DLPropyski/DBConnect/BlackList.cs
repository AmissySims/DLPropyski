//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DLPropyski.DBConnect
{
    using System;
    using System.Collections.Generic;
    
    public partial class BlackList
    {
        public int id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Describe { get; set; }
    
        public virtual User User { get; set; }
    }
}
