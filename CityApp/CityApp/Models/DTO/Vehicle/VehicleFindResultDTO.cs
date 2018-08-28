using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.Vehicle
{
    public class VehicleFindResultDTO
    {
        public int id { get; set; }
        public string vehicleCode { get; set; }
        public string vehicleName { get; set; }
        public string driverCode { get; set; }
        public string driverName { get; set; }
        public string plateNumber { get; set; }
        public string chassis { get; set; }
        public string engineNumber { get; set; }

    }
}