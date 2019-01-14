using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using SalesStatistics.BLL.Contracts.Interfaces;
using SalesStatistics.BLL.Services;
using SalesStatistics.DataTransferObjects;
using SalesStatistics.DAL.Models;
using SalesStatistics.Web.Models.ViewModels;

namespace SalesStatistics.Web.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IItemService _service;

        public ItemController()
        {
            _service = new ItemService(new UnitOfWorkFactory());
        }
        // GET: Item
        public ActionResult Index()
        {
            return View();
        }

        // GET: Item/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Name")] ItemViewModel itemViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(itemViewModel);
                }

                _service.Add(Mapper.Map<ItemDTO>(itemViewModel));

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

                return View();
            }
        }

        // GET: Item/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var itemViewModel = Mapper.Map<ItemViewModel>(_service.GetById((int) id));

            if (itemViewModel == null)
            {
                return HttpNotFound();
            }
            return View(itemViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Name")] ItemViewModel itemViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(itemViewModel);
                }

                _service.Update(Mapper.Map<ItemDTO>(itemViewModel));

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

                return View(itemViewModel);
            }
        }

        // GET: Item/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var itemViewModel = Mapper.Map<ItemViewModel>(_service.GetById((int) id));

            if (itemViewModel == null)
            {
                return HttpNotFound();
            }
            return View(itemViewModel);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _service.Remove(_service.GetById(id));

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

                return View(Mapper.Map<ItemViewModel>(_service.GetById(id)));
            }
        }

        public ActionResult GetItems()
        {
            var items = Mapper.Map<IEnumerable<ItemViewModel>>(_service.GetItems());

            return PartialView("_ItemsTable", items);
        }
    }
}
