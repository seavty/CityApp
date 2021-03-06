﻿using CityApp.Models.DB;
using CityApp.Models.DTO;
using CityApp.Models.DTO.Customer;
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
    public class CustomerHandler
    {
        private readonly CityAppEntities db;

        public CustomerHandler()
        {
            db = new CityAppEntities();
            db.Database.CommandTimeout = ConstantHelper.DB_TIMEOUT;
        }

        //-> SelectByID
        public async Task<CustomerViewDTO> SelectByID(int id)
        {
            var record = await db.tblCustomers.FirstOrDefaultAsync(x => x.deleted == null && x.id == id);
            if (record == null)
                throw new HttpException((int)HttpStatusCode.NotFound, "NotFound");
            return MappingHelper.MapDBClassToDTO<tblCustomer, CustomerViewDTO>(record);
        }

        //-> New
        public async Task<CustomerViewDTO> New(CustomerNewDTO newDTO)
        {
            newDTO = StringHelper.TrimStringProperties(newDTO);
            var record = (tblCustomer)MappingHelper.MapDTOToDBClass<CustomerNewDTO, tblCustomer>(newDTO, new tblCustomer());
            record.createdDate = DateTime.Now;
            db.tblCustomers.Add(record);

            await db.SaveChangesAsync();
            db.Entry(record).Reload();
            return await SelectByID(record.id);
        }

        //-> Edit
        public async Task<CustomerViewDTO> Edit(CustomerEditDTO editDTO)
        {
            var record = await db.tblCustomers.FirstOrDefaultAsync(x => x.deleted == null && x.id == editDTO.id);
            if (record == null)
                throw new HttpException((int)HttpStatusCode.NotFound, "NotFound");
            editDTO = StringHelper.TrimStringProperties(editDTO);
            record = (tblCustomer)MappingHelper.MapDTOToDBClass<CustomerEditDTO, tblCustomer>(editDTO, record);
            record.updatedDate = DateTime.Now;
            await db.SaveChangesAsync();
            return await SelectByID(record.id);
        }

        //-> GetList
        public async Task<GetListDTO<CustomerViewDTO>> GetList(CustomerFindDTO findDTO)
        {
            IQueryable<tblCustomer> query = db.tblCustomers.Where(x => x.deleted == null);
            
            if (!string.IsNullOrEmpty(findDTO.customerCode))        query = query.Where(x => x.customerCode.StartsWith(findDTO.customerCode));
            if (!string.IsNullOrEmpty(findDTO.customerName))   query = query.Where(x => x.customerName.StartsWith(findDTO.customerName));
            if (!string.IsNullOrEmpty(findDTO.phoneNumber))    query = query.Where(x => x.phoneNumber.StartsWith(findDTO.phoneNumber));

            query = query.AsQueryable().OrderBy($"{findDTO.orderBy} {findDTO.orderDirection}");
            
            return await ListingHandler(findDTO.currentPage, query);
        }

        //-> ListingHandler
        private async Task<GetListDTO<CustomerViewDTO>> ListingHandler(int currentPage, IQueryable<tblCustomer> query)
        {
            int totalRecord = await query.CountAsync();
            List<tblCustomer> records = await query.Page(currentPage).ToListAsync();

            var myList = new List<CustomerViewDTO>();
            foreach (var record in records)
            {
                myList.Add(await SelectByID(record.id));
            }
            var getList = new GetListDTO<CustomerViewDTO>();
            getList.metaData = PaginationHelper.GetMetaData(currentPage, totalRecord);
            getList.metaData.numberOfColumn = 5; // need to change number of column
            getList.items = myList;
            return getList;
        }

        /*
        //-> SSA
        public async Task<GetSSADTO<CustomerViewDTO>> SSA(string search)
        {
            IQueryable<tblCustomer> query = db.tblCustomers.Where(x => x.deleted == null);
            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.firstName.StartsWith(search));
            query = query.OrderBy(x => x.firstName);

            var ssa = new GetSSADTO<CustomerViewDTO>();
            ssa.results = (await ListingHandler(1, query)).items;

            return ssa;
        }
        */
        //-> Delete
        public async Task<Boolean> Delete(int id)
        {
            var record = await db.tblCustomers.FirstOrDefaultAsync(x => x.deleted == null && x.id == id);
            if (record == null)
                throw new HttpException((int)HttpStatusCode.NotFound, "NotFound");

            /*
            var checkRecord = await db.tblSaleOrders.FirstOrDefaultAsync(x => x.deleted == null && x.customerID == id);
            if (checkRecord != null)
                throw new HttpException((int)HttpStatusCode.BadRequest, "Cannot this record because it is currently in used!");
                */
            record.deleted = 1;
            await db.SaveChangesAsync();
            return true;
        }
    }
}