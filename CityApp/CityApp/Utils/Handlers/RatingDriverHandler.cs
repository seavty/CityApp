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
using CityApp.Models.DTO.RatingDriver;

namespace CityApp.Utils.Handlers
{
    public class RatingDriverHandler
    {
        private readonly CityAppEntities db;

        public RatingDriverHandler()
        {
            db = new CityAppEntities();
            db.Database.CommandTimeout = ConstantHelper.DB_TIMEOUT;
        }

        //-> SelectByID
        public async Task<RatingDriverViewDTO> SelectByID(int id)
        {
            var record = await db.tblRatings.FirstOrDefaultAsync(x => x.deleted == null && x.id == id);
            if (record == null)
                throw new HttpException((int)HttpStatusCode.NotFound, "NotFound");

            var viewDTO = new RatingDriverViewDTO();
            viewDTO.star = record.star;

            viewDTO.customer = await new CustomerHandler().SelectByID(int.Parse(record.customerID.ToString()));
            viewDTO.driver = await new DriverHandler().SelectByID(int.Parse(record.driverID.ToString()));
            return viewDTO;
        }

        //-> GetList
        public async Task<GetListDTO<RatingDriverViewDTO>> GetList(RatingDriverFindDTO findDTO)
        {
            IQueryable<RatingDriverFindResultDTO> query = from r in db.tblRatings
                                                         join c in db.tblCustomers on r.customerID equals c.id
                                                         join d in db.tblDrivers on r.driverID equals d.id
                                                         where r.deleted == null
                                                         && (findDTO.customerID == 0 ? 1 == 1 : c.id == findDTO.customerID)
                                                         select new RatingDriverFindResultDTO
                                                         {
                                                             id = r.id,
                                                             star = r.star,
                                                             driverCode = d.driverCode,
                                                             driverName = d.driverName,
                                                             customerCode = c.customerCode,
                                                             customerName = c.customerName
                                                         };
            query = query.AsQueryable().OrderBy($"{findDTO.orderBy} {findDTO.orderDirection}");
            return await ListingHandler(findDTO.currentPage, query);

        }

        //-> ListingHandler
        private async Task<GetListDTO<RatingDriverViewDTO>> ListingHandler(int currentPage, IQueryable<dynamic> query)
        {
            int totalRecord = await query.CountAsync();
            var records = await query.Page(currentPage).ToListAsync();

            var myList = new List<RatingDriverViewDTO>();
            foreach (var record in records)
            {
                myList.Add(await SelectByID(record.id));
            }
            var getList = new GetListDTO<RatingDriverViewDTO>();
            getList.metaData = PaginationHelper.GetMetaData(currentPage, totalRecord);
            getList.metaData.numberOfColumn = 6; // need to change number of column
            getList.items = myList;
            return getList;
        }


    }
}