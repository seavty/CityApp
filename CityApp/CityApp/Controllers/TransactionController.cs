using CityApp.Models.DTO.RatingDriver;
using CityApp.Models.DTO.Transaction;
using CityApp.Utils.Attribute;
using CityApp.Utils.Handlers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CityApp.Controllers
{
    //[Authentication] //--for testing tmp disable authentication
    [ErrorLogger]
    public class TransactionController : Controller
    {
        private readonly TransactionHandler handler;

        public TransactionController()
        {
            handler = new TransactionHandler(); 
        }

        //-> Paging
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Paging(TransactionFindDTO findDTO)
        {
            return PartialView(await handler.GetList(findDTO));
        }

    }
}