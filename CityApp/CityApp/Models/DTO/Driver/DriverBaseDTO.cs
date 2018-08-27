using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.Driver
{
    public class DriverBaseDTO: ModeDTO
    {
        [Required]
        [MaxLength(100)]
        [DisplayName("Driver Name (*):")]
        public string driverName { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Phone Number (*):")]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        [DisplayName("E-mail:")]
        public string email { get; set; }

    }
}