using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.Vehicle
{
    public class VehicleFindDTO : FindDTO
    {
        [MaxLength(100)]
        [DisplayName("Vehicle ID:")]
        public string vehicleCode { get; set; }

        [MaxLength(100)]
        [DisplayName("Plate Number:")]
        public string plateNumber { get; set; }

        [MaxLength(100)]
        [DisplayName("Driver Name:")]
        public string driverName { get; set; }

        [DisplayName("DriverID:")]
        public int driverID { get; set; }
    }
}