using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.Customer
{
    public class CustomerFindDTO : FindDTO
    {
        [MaxLength(100)]
        [DisplayName("Customer ID:")]
        public string customerCode { get; set; }

        [MaxLength(100)]
        [DisplayName("Customer Name:")]
        public string customerName { get; set; }

        [MaxLength(100)]
        [DisplayName("Phone Number:")]
        public string phoneNumber { get; set; }

        
    }
}