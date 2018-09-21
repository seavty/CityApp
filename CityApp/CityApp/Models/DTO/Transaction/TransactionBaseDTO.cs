using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.Transaction
{
    public class TransactionBaseDTO : ModeDTO
    {
        [Required]
        [DisplayName("Driver ID (*):")]
        public int tran_DriverID { get; set; }

        [Required]
        [DisplayName("Customer ID (*):")]
        public int tran_CustomerID { get; set; }

        [Required]
        [DisplayName("Vehicle ID (*):")]
        public int tran_VehicleID { get; set; }


        [Required]
        [DisplayName("Start Address (*):")]
        public string tran_StartAddress { get; set; }


        [Required]
        [DisplayName("End Address (*):")]
        public string tran_EndAddress { get; set; }



    }
}