using CityApp.Models.DTO.RatingDriver;
using CityApp.Utils.Attribute;
using CityApp.Utils.Handlers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CityApp.Controllers
{
    //[Authentication] //--for testing tmp disable authentication
    [ErrorLogger]
    public class MapController : Controller
    {
        private readonly RatingDriverHandler handler;

        public MapController()
        {
            //handler = new RatingDriverHandler(); 
        }

        //--> New
        public ActionResult Map() { return View(); }

    }
}