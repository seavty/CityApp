using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.Transaction
{
    public class TransactionFindDTO : FindDTO
    {
        [DisplayName("CustomerID:")]
        public int customerID { get; set; }

        [DisplayName("Driver ID:")]
        public int tran_DriverID { get; set; }

        [DisplayName("Vehicle ID:")]
        public int tran_VehicleID { get; set; }

    }
}