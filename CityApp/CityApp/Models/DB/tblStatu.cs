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
    
    public partial class tblStatu
    {
        public int stus_StatusID { get; set; }
        public Nullable<System.DateTime> stus_CreatedDate { get; set; }
        public Nullable<int> stus_CreatedBy { get; set; }
        public Nullable<System.DateTime> stus_UpdatedDate { get; set; }
        public Nullable<int> stus_UpdatedBy { get; set; }
        public Nullable<int> stus_Deleted { get; set; }
        public string stus_Code { get; set; }
        public string stus_Name { get; set; }
        public string stus_Remark { get; set; }
    }
}