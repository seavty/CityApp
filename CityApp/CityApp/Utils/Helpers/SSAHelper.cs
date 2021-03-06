﻿using CityApp.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CityApp.Utils.Helpers
{
    public static class SSAHelper
    {
        public static List<SelectListItem> CustomerSSA(int id)
        {
            if (id == 0)
                return new List<SelectListItem>();
            
            CityAppEntities db = new CityAppEntities();
            var query = db.tblCustomers.FirstOrDefault(x => x.deleted == null && x.id == id);
            if (query == null)
                throw new HttpException((int)HttpStatusCode.NotFound, "NotFound");
            return new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = $"{query.customerName} {query.phoneNumber}",
                    Value = id.ToString()
                }
            };
        }

        public static List<SelectListItem> DriverSSA(int id)
        {
            if (id == 0)
                return new List<SelectListItem>();

            CityAppEntities db = new CityAppEntities();
            var query = db.tblDrivers.FirstOrDefault(x => x.deleted == null && x.id == id);
            if (query == null)
                throw new HttpException((int)HttpStatusCode.NotFound, "NotFound");
            return new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = $"{query.driverName}",
                    Value = id.ToString()
                }
            };
        }
    }

}