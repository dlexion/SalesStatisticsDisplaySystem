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
    public class OrderController : Controller
    {
        private readonly IService _service;

        public OrderController()
        {
            _service = new Service(new UnitOfWorkFactory());
        }

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        // GET: Order/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Date,Cost")] OrderViewModel orderViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(orderViewModel);
                }

                _service.AddOrder(Mapper.Map<OrderDTO>(orderViewModel));

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

                return View();
            }
        }

        // GET: Order/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var orderViewModel = Mapper.Map<OrderViewModel>(_service.GetOrderById((int)id));

            if (orderViewModel == null)
            {
                return HttpNotFound();
            }

            return View(orderViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Date,Cost")] OrderViewModel orderViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(orderViewModel);
                }

                _service.UpdateOrder(Mapper.Map<OrderDTO>(orderViewModel));

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

                return View(orderViewModel);
            }
        }

        // GET: Order/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var orderViewModel = Mapper.Map<OrderViewModel>(_service.GetOrderById((int)id));

            if (orderViewModel == null)
            {
                return HttpNotFound();
            }

            return View(orderViewModel);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _service.RemoveOrder(_service.GetOrderById(id));

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

                return View(Mapper.Map<OrderViewModel>(_service.GetOrderById(id)));
            }
        }

        public ActionResult GetOrders()
        {
            var customers = Mapper.Map<IEnumerable<OrderViewModel>>(_service.GetAllOrders());

            return PartialView("_OrdersTable", customers);
        }
    }
}
