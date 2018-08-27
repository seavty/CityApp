using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.Driver
{
    public class DriverViewDTO : DriverNewDTO
    {
        public int id { get; set; }

        [MaxLength(100)]
        [DisplayName("Driver ID:")]
        public string driverCode { get; set; }
    }
}