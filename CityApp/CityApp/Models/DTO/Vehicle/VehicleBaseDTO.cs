using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.Vehicle
{
    public class VehicleBaseDTO: ModeDTO
    {
        [Required]
        [MaxLength(100)]
        [DisplayName("Vehicle Name (*):")]
        public string vehicleName { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Chassis (*):")]
        public string chassis { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Engine Number (*):")]
        public string engineNumber { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Plate Number (*):")]
        public string plateNumber { get; set; }

        [Required]
        [DisplayName("Driver Name (*):")]
        public int driverID { get; set; }

    }
}