using CityApp.Models.DTO.Customer;
using CityApp.Models.DTO.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.FavouriteDriver
{
    public class FavouriteDriverViewDTO
    {
        public int id { get; set; }
        public CustomerViewDTO customer { get; set; }
        public DriverViewDTO driver { get; set; }
    }
}