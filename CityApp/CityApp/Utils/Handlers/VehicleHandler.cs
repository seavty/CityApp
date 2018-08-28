using CityApp.Models.DB;
using CityApp.Models.DTO;
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
using CityApp.Models.DTO.Vehicle;

namespace CityApp.Utils.Handlers
{
    public class VehicleHandler
    {
        private readonly CityAppEntities db;

        public VehicleHandler()
        {
            db = new CityAppEntities();
            db.Database.CommandTimeout = ConstantHelper.DB_TIMEOUT;
        }

        //-> SelectByID
        public async Task<VehicleViewDTO> SelectByID(int id)
        {
            var query = await db.tblVehicles.FirstOrDefaultAsync(x => x.deleted == null && x.id == id);
            if (query == null)
                throw new HttpException((int)HttpStatusCode.NotFound, "NotFound");

            var dto = MappingHelper.MapDBClassToDTO<tblVehicle, VehicleViewDTO>(query);
            dto.driver = await new DriverHandler().SelectByID(int.Parse(query.driverID.ToString()));
            return dto;
        }

        //-> New
        public async Task<VehicleViewDTO> New(VehicleNewDTO newDTO)
        {
            newDTO = StringHelper.TrimStringProperties(newDTO);
            var record = (tblVehicle)MappingHelper.MapDTOToDBClass<VehicleNewDTO, tblVehicle>(newDTO, new tblVehicle());
            record.createdDate = DateTime.Now;
            db.tblVehicles.Add(record);

            await db.SaveChangesAsync();
            db.Entry(record).Reload();
            return await SelectByID(record.id);
        }

        //-> Edit
        public async Task<VehicleViewDTO> Edit(VehicleEditDTO editDTO)
        {
            var query = await db.tblVehicles.FirstOrDefaultAsync(x => x.deleted == null && x.id == editDTO.id);
            if (query == null)
                throw new HttpException((int)HttpStatusCode.NotFound, "NotFound");
            editDTO = StringHelper.TrimStringProperties(editDTO);
            query = (tblVehicle)MappingHelper.MapDTOToDBClass<VehicleEditDTO, tblVehicle>(editDTO, query);
            query.updatedDate = DateTime.Now;
            await db.SaveChangesAsync();
            return await SelectByID(query.id);
        }

        //-> GetList
        public async Task<GetListDTO<VehicleViewDTO>> GetList(VehicleFindDTO findDTO)
        {
            /*
            IQueryable<tblVehicle> query = db.tblVehicles.Where(x => x.deleted == null);
            if (!string.IsNullOrEmpty(findDTO.code))        query = query.Where(x => x.code.StartsWith(findDTO.code));
            if (!string.IsNullOrEmpty(findDTO.name))   query = query.Where(x => x.name.StartsWith(findDTO.name));
            if (!string.IsNullOrEmpty(findDTO.plateNumber))    query = query.Where(x => x.plateNumber.StartsWith(findDTO.plateNumber));

            query = query.AsQueryable().OrderBy($"{findDTO.orderBy} {findDTO.orderDirection}");
            
            return await ListingHandler(findDTO.currentPage, query);
            */
            IQueryable<VehicleFindResultDTO> query = from v in db.tblVehicles
                                                       join d in db.tblDrivers on v.driverID equals d.id
                                                       where v.deleted == null
                                                       && (string.IsNullOrEmpty(findDTO.vehicleCode) ? 1 == 1 : v.vehicleCode.StartsWith(findDTO.vehicleCode))
                                                       && (string.IsNullOrEmpty(findDTO.plateNumber) ? 1 == 1 : v.plateNumber.StartsWith(findDTO.plateNumber))
                                                       && (string.IsNullOrEmpty(findDTO.driverCode) ? 1 == 1 : d.driverName.StartsWith(findDTO.driverCode))
                                                       && (findDTO.driverID == 0 ? 1 == 1 : d.id == findDTO.driverID)
                                                       select new VehicleFindResultDTO
                                                       {
                                                           id = v.id,
                                                           vehicleCode = v.vehicleCode,
                                                           vehicleName = v.vehicleName,
                                                           driverCode = d.driverCode,
                                                           driverName = d.driverName,
                                                           plateNumber = v.plateNumber,
                                                           chassis = v.chassis,
                                                           engineNumber = v.engineNumber,
                                                       };
            query = query.AsQueryable().OrderBy($"{findDTO.orderBy} {findDTO.orderDirection}");
            return await ListingHandler(findDTO.currentPage, query);
        }

        //-> ListingHandler
        private async Task<GetListDTO<VehicleViewDTO>> ListingHandler(int currentPage, IQueryable<dynamic> query)
        {
            int totalRecord = await query.CountAsync();
            var records = await query.Page(currentPage).ToListAsync();

            var myList = new List<VehicleViewDTO>();
            foreach (var record in records)
            {
                myList.Add(await SelectByID(record.id));
            }
            var getList = new GetListDTO<VehicleViewDTO>();
            getList.metaData = PaginationHelper.GetMetaData(currentPage, totalRecord);
            getList.metaData.numberOfColumn = 8; // need to change number of column
            getList.items = myList;
            return getList;
        }

        //-> Delete
        public async Task<Boolean> Delete(int id)
        {
            var query = await db.tblVehicles.FirstOrDefaultAsync(x => x.deleted == null && x.id == id);
            if (query == null)
                throw new HttpException((int)HttpStatusCode.NotFound, "NotFound");

            /*
            var checkRecord = await db.tblSaleOrders.FirstOrDefaultAsync(x => x.deleted == null && x.VehiclerID == id);
            if (checkRecord != null)
                throw new HttpException((int)HttpStatusCode.BadRequest, "Cannot this record because it is currently in used!");
                */
            query.deleted = 1;
            await db.SaveChangesAsync();
            return true;
        }
    }
}