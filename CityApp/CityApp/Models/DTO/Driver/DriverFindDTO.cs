using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.Driver
{
    public class DriverFindDTO : FindDTO
    {
        [MaxLength(100)]
        [DisplayName("Driver ID:")]
        public string code { get; set; }

        [MaxLength(100)]
        [DisplayName("Name:")]
        public string name { get; set; }

        [MaxLength(100)]
        [DisplayName("Phone Number:")]
        public string phoneNumber { get; set; }

        
    }
}