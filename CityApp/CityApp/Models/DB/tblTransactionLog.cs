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
    
    public partial class tblTransactionLog
    {
        public int trlg_TransactionLogID { get; set; }
        public Nullable<System.DateTime> trlg_CreatedDate { get; set; }
        public Nullable<int> trlg_CreatedBy { get; set; }
        public Nullable<System.DateTime> trlg_UpdatedDate { get; set; }
        public Nullable<int> trlg_UpdatedBy { get; set; }
        public Nullable<int> trlg_Deleted { get; set; }
        public string trlg_Code { get; set; }
        public Nullable<int> trlg_TransactionID { get; set; }
        public string trlg_Status { get; set; }
        public Nullable<System.DateTime> trlg_Date { get; set; }
    }
}
