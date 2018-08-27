using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.FavouriteDriver
{
    public class FavouriteDriverBaseDTO: ModeDTO
    {
        [DisplayName("CustomerID:")]
        public int customerID { get; set; }
    }
}