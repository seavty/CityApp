using CityApp.Models.DTO.Customer;
using CityApp.Models.DTO.Driver;
using CityApp.Models.DTO.Vehicle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.Transaction
{
    public class TransactionViewDTO : TransactionNewDTO
    {
        public int tran_TransactionID { get; set; }
        public CustomerViewDTO customer { get; set; }
        public DriverViewDTO driver { get; set; }
        public VehicleViewDTO vehicle { get; set; }

        public string tran_Code { get; set; }

    }
}