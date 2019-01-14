﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SalesStatistics.BLL.Contracts.Interfaces;
using SalesStatistics.BLL.Services;
using SalesStatistics.DataTransferObjects;
using SalesStatistics.DAL;
using SalesStatistics.DAL.Models;
using SalesStatistics.Web.Models.ViewModels;

namespace SalesStatistics.Web.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        private readonly IService _service;

        public ManagerController()
        {
            _service = new Service(new UnitOfWorkFactory());
        }

        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        // GET: Manager/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,LastName")] ManagerViewModel managerViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(managerViewModel);
                }

                _service.AddManager(Mapper.Map<ManagerDTO>(managerViewModel));

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

                return View();
            }
        }

        // GET: Manager/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int newId = id ?? default(int);
            var manager = Mapper.Map<ManagerViewModel>(_service.GetManagerById(newId));

            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,LastName")] ManagerViewModel managerViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(managerViewModel);
                }

                _service.UpdateManager(Mapper.Map<ManagerDTO>(managerViewModel));

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

                return View(managerViewModel);
            }
        }

        // GET: Manager/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            int newId = id ?? default(int);
            var item = Mapper.Map<ManagerViewModel>(_service.GetManagerById(newId));

            return View(item);
        }

        // POST: Manager/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _service.RemoveManager(_service.GetManagerById(id));

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

                return View();
            }
        }

        public ActionResult GetManagers()
        {
            var items = Mapper.Map<IEnumerable<ManagerViewModel>>(_service.GetAllManagers());

            return PartialView("_ManagersTable", items);
        }

        public JsonResult GetChartData()
        {
            var result = Mapper.Map<List<OrderDTO>>(_service.GetAllOrders())
                .GroupBy(x => x.Manager.LastName)
                .Select(y => new object[] { y.Key, y.Count() }).ToArray();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
