using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.Transaction
{
    public class TransactionFindResultDTO
    {
        public int tran_TransactionID { get; set; }
        
        public string driverCode { get; set; }
        public string driverName { get; set; }

        public string customerCode { get; set; }
        public string customerName { get; set; }

        public string vehicleCode  { get; set; }

        public string tran_Code { get; set; }
        public string tran_StartAddress { get; set; }
        public string tran_EndAddress { get; set; }
    }
}