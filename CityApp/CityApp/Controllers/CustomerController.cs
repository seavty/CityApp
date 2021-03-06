﻿using CityApp.Models.DTO.Customer;
using CityApp.Models.DTO.SSA;
using CityApp.Utils.Attribute;
using CityApp.Utils.Handlers;
using CityApp.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CityApp.Controllers
{
    //[Authentication] //--for testing tmp disable authentication
    [ErrorLogger]
    public class CustomerController : Controller
    {
        private readonly CustomerHandler handler;

        public CustomerController()
        {
            handler = new CustomerHandler(); 
        }

        //--> New
        public ActionResult New() { return View(); }

        //-> New
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> New(CustomerNewDTO newDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new HttpException((int)HttpStatusCode.BadRequest, ConstantHelper.KEY_IN_REQUIRED_FIELD);
                Response.StatusCode = 200;
                return Json(await handler.New(newDTO), JsonRequestBehavior.AllowGet);
            }
            catch (HttpException ex)
            {
                return Json(ConstantHelper.ERROR, JsonRequestBehavior.AllowGet);
                
            }
        }

        //-> View
        public async Task<ActionResult> View(int id)
        {
            var record = await handler.SelectByID(id);
            record.mode = ConstantHelper.MODE_VIEW;
            return View(record);
        }


        //-> Edit
        public async Task<ActionResult> Edit(int id) { return View(await handler.SelectByID(id)); }

        //-> Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit(CustomerEditDTO editDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new HttpException((int)HttpStatusCode.BadRequest, ConstantHelper.KEY_IN_REQUIRED_FIELD);
                Response.StatusCode = 200;
                return Json(await handler.Edit(editDTO), JsonRequestBehavior.AllowGet);

            }
            catch (HttpException)
            {
                return Json(ConstantHelper.ERROR, JsonRequestBehavior.AllowGet);
            }
        }

        //-> Find 
        [HttpGet]
        public ActionResult Find() { return View(); }

        //-> Paging
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Paging(CustomerFindDTO findDTO) { return PartialView(await handler.GetList(findDTO)); }

        //-> SSA
        //public async Task<JsonResult> SSA() { return Json(await handler.SSA(Request.QueryString["q"]), JsonRequestBehavior.AllowGet); }


        //-> Delete
        //??? why if use http delete - resource alway not found ? ???
        [HttpPost]
        public async Task<string> Delete(int id)
        {
            try
            {
                if (await handler.Delete(id))
                {
                    Response.StatusCode = 200;
                    return "ok";
                }
                return null;
            }
            catch (HttpException ex)
            {
                Response.StatusCode = 400;
                return ex.Message;
            }
        }

        //-> View
        public ActionResult FavouriteDriverTab(int id) { return View(); }

        //-> View
        public ActionResult RatingDriverTab(int id) { return View(); }

        //-> View
        public ActionResult TransactionTab(int id) { return View(); }
    }
}