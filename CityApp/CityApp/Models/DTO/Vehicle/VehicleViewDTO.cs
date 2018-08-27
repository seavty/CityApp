using CityApp.Models.DTO.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.Vehicle
{
    public class VehicleViewDTO : VehicleNewDTO
    {
        public int id { get; set; }

        [MaxLength(100)]
        [DisplayName("Vehicle ID (*):")]
        public string vehicleCode { get; set; }

        public DriverViewDTO driver { get; set; }
    }
}