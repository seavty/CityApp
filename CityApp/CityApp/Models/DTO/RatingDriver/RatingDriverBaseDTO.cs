using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.RatingDriver
{
    public class RatingDriverBaseDTO: ModeDTO
    {
        [DisplayName("CustomerID:")]
        public int customerID { get; set; }
    }
}