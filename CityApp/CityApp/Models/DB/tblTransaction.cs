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
    
    public partial class tblTransaction
    {
        public int tran_TransactionID { get; set; }
        public Nullable<System.DateTime> tran_CreatedDate { get; set; }
        public Nullable<int> tran_CreatedBy { get; set; }
        public Nullable<System.DateTime> tran_UpdatedDate { get; set; }
        public Nullable<int> tran_UpdatedBy { get; set; }
        public Nullable<int> tran_Deleted { get; set; }
        public string tran_Code { get; set; }
        public Nullable<System.DateTime> tran_Date { get; set; }
        public string tran_Status { get; set; }
        public Nullable<int> tran_DriverID { get; set; }
        public Nullable<int> tran_CustomerID { get; set; }
        public Nullable<int> tran_VehicleID { get; set; }
        public Nullable<System.DateTime> tran_StartDate { get; set; }
        public Nullable<System.DateTime> tran_EndTime { get; set; }
        public string tran_StartLatLong { get; set; }
        public string tran_StartAddress { get; set; }
        public string tran_EndLatLong { get; set; }
        public string tran_EndAddress { get; set; }
        public Nullable<decimal> tran_Distance { get; set; }
        public string tran_LatLog { get; set; }
    }
}
