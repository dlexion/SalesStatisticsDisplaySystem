using System;
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
        private readonly Service _service;

        public ManagerController()
        {
            _service = new Service(new UnitOfWorkFactory());
        }

        // GET: Manager
        public ActionResult Index()
        {
            var items = Mapper.Map<IEnumerable<ManagerViewModel>>(_service.GetAllManagers());

            return View(items);
        }

        // GET: Manager/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {

            return View();
        }

        // POST: Manager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                // TODO notify user
                return View();
            }
        }

        // GET: Manager/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            return View();
        }

        // POST: Manager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                // TODO notify user
                return View();
            }
        }

        // GET: Manager/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            return View();
        }

        // POST: Manager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
                return RedirectToAction("Index");
            }
        }
    }
}
