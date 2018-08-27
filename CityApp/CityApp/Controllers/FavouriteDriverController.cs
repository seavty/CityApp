using CityApp.Models.DTO.Driver;
using CityApp.Models.DTO.FavouriteDriver;
using CityApp.Models.DTO.SSA;
using CityApp.Utils.Attribute;
using CityApp.Utils.Handlers;
using CityApp.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CityApp.Controllers
{
    //[Authentication] //--for testing tmp disable authentication
    [ErrorLogger]
    public class FavouriteDriverController : Controller
    {
        private readonly FavouriteDriverHandler handler;

        public FavouriteDriverController()
        {
            handler = new FavouriteDriverHandler(); 
        }

        //-> Paging
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Paging(FavouriteDriverFindDTO findDTO)
        {
            return PartialView(await handler.GetList(findDTO));
        }

    }
}