using CityApp.Models.DTO.RatingDriver;
using CityApp.Utils.Attribute;
using CityApp.Utils.Handlers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CityApp.Controllers
{
    //[Authentication] //--for testing tmp disable authentication
    [ErrorLogger]
    public class RatingDriverController : Controller
    {
        private readonly RatingDriverHandler handler;

        public RatingDriverController()
        {
            handler = new RatingDriverHandler(); 
        }

        //-> Paging
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Paging(RatingDriverFindDTO findDTO)
        {
            return PartialView(await handler.GetList(findDTO));
        }

    }
}