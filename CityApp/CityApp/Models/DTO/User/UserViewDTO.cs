using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityApp.Models.DTO.User
{
    public class UserViewDTO: UserBaseDTO
    {
        public int id { get; set; }
        public string session { get; set; }
    }
}