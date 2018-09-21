using CityApp.Models.DTO.RatingDriver;
using CityApp.Models.DTO.Transaction;
using CityApp.Utils.Attribute;
using CityApp.Utils.Handlers;
using CityApp.Utils.Helpers;
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

        //-> View
        public async Task<ActionResult> View(int id)
        {
            var record = await handler.SelectByID(id);
            record.mode = ConstantHelper.MODE_VIEW;
            return View(record);
        }

    }
}