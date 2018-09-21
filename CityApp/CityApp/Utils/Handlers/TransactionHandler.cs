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
using CityApp.Models.DTO.Transaction;

namespace CityApp.Utils.Handlers
{
    public class TransactionHandler
    {
        private readonly CityAppEntities db;

        public TransactionHandler()
        {
            db = new CityAppEntities();
            db.Database.CommandTimeout = ConstantHelper.DB_TIMEOUT;
        }

        //-> SelectByID
        public async Task<TransactionViewDTO> SelectByID(int id)
        {
            var record = await db.tblTransactions.FirstOrDefaultAsync(x => x.tran_Deleted == null && x.tran_TransactionID == id);
            if (record == null)
                throw new HttpException((int)HttpStatusCode.NotFound, "NotFound");

           //var viewDTO = new TransactionViewDTO();

            var viewDTO = MappingHelper.MapDBClassToDTO<tblTransaction, TransactionViewDTO>(record);


            viewDTO.customer = await new CustomerHandler().SelectByID(int.Parse(record.tran_CustomerID.ToString()));
            viewDTO.driver = await new DriverHandler().SelectByID(int.Parse(record.tran_DriverID.ToString()));
            viewDTO.vehicle = await new VehicleHandler().SelectByID(int.Parse(record.tran_VehicleID.ToString()));
            return viewDTO;
        }

        //-> GetList
        public async Task<GetListDTO<TransactionViewDTO>> GetList(TransactionFindDTO findDTO)
        {
            IQueryable<TransactionFindResultDTO> query = from x in db.tblTransactions
                                                         join c in db.tblCustomers on x.tran_CustomerID equals c.id
                                                         join d in db.tblDrivers on x.tran_DriverID equals d.id
                                                         join v in db.tblVehicles on x.tran_VehicleID equals v.id
                                                         where x.tran_Deleted == null
                                                         && (findDTO.customerID == 0 ? 1 == 1 : c.id == findDTO.customerID)
                                                         && (findDTO.tran_DriverID == 0 ? 1 == 1 : d.id == findDTO.tran_DriverID)
                                                         && (findDTO.tran_VehicleID == 0 ? 1 == 1 : v.id == findDTO.tran_VehicleID)
                                                         select new TransactionFindResultDTO
                                                         {
                                                             tran_TransactionID = x.tran_TransactionID,
                                                             tran_Code = x.tran_Code,
                                                             tran_StartAddress =x.tran_StartAddress,
                                                             tran_EndAddress = x.tran_EndAddress,
                                                             driverCode = d.driverCode,
                                                             driverName = d.driverName,
                                                             customerCode = c.customerCode,
                                                             customerName = c.customerName,
                                                             vehicleCode = v.vehicleCode
                                                            
                                                         };
            query = query.AsQueryable().OrderBy($"{findDTO.orderBy} {findDTO.orderDirection}");
            return await ListingHandler(findDTO.currentPage, query);

        }

        //-> ListingHandler
        private async Task<GetListDTO<TransactionViewDTO>> ListingHandler(int currentPage, IQueryable<dynamic> query)
        {
            int totalRecord = await query.CountAsync();
            var records = await query.Page(currentPage).ToListAsync();

            var myList = new List<TransactionViewDTO>();
            foreach (var record in records)
            {
                myList.Add(await SelectByID(record.tran_TransactionID));
            }
            var getList = new GetListDTO<TransactionViewDTO>();
            getList.metaData = PaginationHelper.GetMetaData(currentPage, totalRecord);
            getList.metaData.numberOfColumn = 9; // need to change number of column
            getList.items = myList;
            return getList;
        }


    }
}