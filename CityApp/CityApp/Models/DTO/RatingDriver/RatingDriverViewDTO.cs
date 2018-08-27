using CityApp.Models.DTO.Customer;
using CityApp.Models.DTO.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.RatingDriver
{
    public class RatingDriverViewDTO
    {
        public int id { get; set; }
        public int? star { get; set; }
        public CustomerViewDTO customer { get; set; }
        public DriverViewDTO driver { get; set; }
    }
}