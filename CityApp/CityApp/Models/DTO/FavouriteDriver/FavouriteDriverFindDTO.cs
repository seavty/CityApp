﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.FavouriteDriver
{
    public class FavouriteDriverFindDTO : FindDTO
    {
        [DisplayName("CustomerID:")]
        public int customerID { get; set; }
    }
}