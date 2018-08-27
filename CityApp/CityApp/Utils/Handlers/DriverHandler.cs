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

namespace CityApp.Utils.Handlers
{
    public class DriverHandler
    {
        private readonly CityAppEntities db;

        public DriverHandler()
        {
            db = new CityAppEntities();
            db.Database.CommandTimeout = ConstantHelper.DB_TIMEOUT;
        }

        //-> SelectByID
        public async Task<DriverViewDTO> SelectByID(int id)
        {
            var record = await db.tblDrivers.FirstOrDefaultAsync(x => x.deleted == null && x.id == id);
            if (record == null)
                throw new HttpException((int)HttpStatusCode.NotFound, "NotFound");
            return MappingHelper.MapDBClassToDTO<tblDriver, DriverViewDTO>(record);
        }

        //-> New
        public async Task<DriverViewDTO> New(DriverNewDTO newDTO)
        {
            newDTO = StringHelper.TrimStringProperties(newDTO);
            var record = (tblDriver)MappingHelper.MapDTOToDBClass<DriverNewDTO, tblDriver>(newDTO, new tblDriver());
            record.createdDate = DateTime.Now;
            db.tblDrivers.Add(record);

            await db.SaveChangesAsync();
            db.Entry(record).Reload();
            return await SelectByID(record.id);
        }

        //-> Edit
        public async Task<DriverViewDTO> Edit(DriverEditDTO editDTO)
        {
            var record = await db.tblDrivers.FirstOrDefaultAsync(x => x.deleted == null && x.id == editDTO.id);
            if (record == null)
                throw new HttpException((int)HttpStatusCode.NotFound, "NotFound");
            editDTO = StringHelper.TrimStringProperties(editDTO);
            record = (tblDriver)MappingHelper.MapDTOToDBClass<DriverEditDTO, tblDriver>(editDTO, record);
            record.updatedDate = DateTime.Now;
            await db.SaveChangesAsync();
            return await SelectByID(record.id);
        }

        //-> GetList
        public async Task<GetListDTO<DriverViewDTO>> GetList(DriverFindDTO findDTO)
        {
            IQueryable<tblDriver> query = db.tblDrivers.Where(x => x.deleted == null);
            
            if (!string.IsNullOrEmpty(findDTO.driverCode))        query = query.Where(x => x.driverCode.StartsWith(findDTO.driverCode));
            if (!string.IsNullOrEmpty(findDTO.driverName))   query = query.Where(x => x.driverName.StartsWith(findDTO.driverName));
            if (!string.IsNullOrEmpty(findDTO.phoneNumber))    query = query.Where(x => x.phoneNumber.StartsWith(findDTO.phoneNumber));

            query = query.AsQueryable().OrderBy($"{findDTO.orderBy} {findDTO.orderDirection}");
            
            return await ListingHandler(findDTO.currentPage, query);
        }

        //-> ListingHandler
        private async Task<GetListDTO<DriverViewDTO>> ListingHandler(int currentPage, IQueryable<tblDriver> query)
        {
            int totalRecord = await query.CountAsync();
            List<tblDriver> records = await query.Page(currentPage).ToListAsync();

            var myList = new List<DriverViewDTO>();
            foreach (var record in records)
            {
                myList.Add(await SelectByID(record.id));
            }
            var getList = new GetListDTO<DriverViewDTO>();
            getList.metaData = PaginationHelper.GetMetaData(currentPage, totalRecord);
            getList.metaData.numberOfColumn = 5; // need to change number of column
            getList.items = myList;
            return getList;
        }

        //-> Delete
        public async Task<Boolean> Delete(int id)
        {
            var record = await db.tblDrivers.FirstOrDefaultAsync(x => x.deleted == null && x.id == id);
            if (record == null)
                throw new HttpException((int)HttpStatusCode.NotFound, "NotFound");

            /*
            var checkRecord = await db.tblSaleOrders.FirstOrDefaultAsync(x => x.deleted == null && x.DriverID == id);
            if (checkRecord != null)
                throw new HttpException((int)HttpStatusCode.BadRequest, "Cannot this record because it is currently in used!");
                */
            record.deleted = 1;
            await db.SaveChangesAsync();
            return true;
        }

        //-> SSA
        public async Task<GetSSADTO<DriverViewDTO>> SSA(string search)
        {
            IQueryable<tblDriver> query = db.tblDrivers.Where(x => x.deleted == null);
            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.driverName.StartsWith(search));
            query = query.OrderBy(x => x.driverName);

            var ssa = new GetSSADTO<DriverViewDTO>();
            ssa.results = (await ListingHandler(1, query)).items;

            return ssa;
        }
    }
}