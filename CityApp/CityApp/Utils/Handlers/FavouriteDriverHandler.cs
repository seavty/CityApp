using CityApp.Models.DB;
using CityApp.Models.DTO;
using CityApp.Models.DTO.Driver;
using CityApp.Models.DTO.SSA;
using CityApp.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Linq.Dynamic;
using CityApp.Utils.Extension;
using CityApp.Models.DTO.FavouriteDriver;

namespace CityApp.Utils.Handlers
{
    public class FavouriteDriverHandler
    {
        private readonly CityAppEntities db;

        public FavouriteDriverHandler()
        {
            db = new CityAppEntities();
            db.Database.CommandTimeout = ConstantHelper.DB_TIMEOUT;
        }

        //-> SelectByID
        public async Task<FavouriteDriverViewDTO> SelectByID(int id)
        {
            var record = await db.tblFavouriteDrivers.FirstOrDefaultAsync(x => x.deleted == null && x.id == id);
            if (record == null)
                throw new HttpException((int)HttpStatusCode.NotFound, "NotFound");

            var viewDTO = new FavouriteDriverViewDTO();

            viewDTO.customer = await new CustomerHandler().SelectByID(int.Parse(record.customerID.ToString()));
            viewDTO.driver = await new DriverHandler().SelectByID(int.Parse(record.driverID.ToString()));
            return viewDTO;
        }

        //-> GetList
        public async Task<GetListDTO<FavouriteDriverViewDTO>> GetList(FavouriteDriverFindDTO findDTO)
        {
            IQueryable<FavouriteDriverFindResultDTO> query = from f in db.tblFavouriteDrivers
                                                         join c in db.tblCustomers on f.customerID equals c.id
                                                         join d in db.tblDrivers on f.driverID equals d.id
                                                         where f.deleted == null
                                                         && (findDTO.customerID == 0 ? 1 == 1 : c.id == findDTO.customerID)
                                                         select new FavouriteDriverFindResultDTO
                                                         {
                                                             id = f.id,
                                                             driverCode = d.driverCode,
                                                             driverName = d.driverName,
                                                             customerCode = c.customerCode,
                                                             customerName = c.customerName 
                                                         };
            query = query.AsQueryable().OrderBy($"{findDTO.orderBy} {findDTO.orderDirection}");
            return await ListingHandler(findDTO.currentPage, query);

        }

        //-> ListingHandler
        private async Task<GetListDTO<FavouriteDriverViewDTO>> ListingHandler(int currentPage, IQueryable<dynamic> query)
        {
            int totalRecord = await query.CountAsync();
            var records = await query.Page(currentPage).ToListAsync();

            var myList = new List<FavouriteDriverViewDTO>();
            foreach (var record in records)
            {
                myList.Add(await SelectByID(record.id));
            }
            var getList = new GetListDTO<FavouriteDriverViewDTO>();
            getList.metaData = PaginationHelper.GetMetaData(currentPage, totalRecord);
            getList.metaData.numberOfColumn = 5; // need to change number of column
            getList.items = myList;
            return getList;
        }


    }
}