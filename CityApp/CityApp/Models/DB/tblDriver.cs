//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CityApp.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblDriver
    {
        public int id { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<int> createdBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
        public Nullable<int> updatedBy { get; set; }
        public Nullable<int> deleted { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string note { get; set; }
    }
}
