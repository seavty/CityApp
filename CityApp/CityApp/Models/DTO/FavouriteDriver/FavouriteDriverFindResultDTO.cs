﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.FavouriteDriver
{
    public class FavouriteDriverFindResultDTO
    {
        public int id { get; set; }
        public string driverCode { get; set; }
        public string driverName { get; set; }

        public string customerCode { get; set; }
        public string customerName { get; set; }
    }
}