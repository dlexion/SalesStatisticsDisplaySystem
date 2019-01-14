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
using SalesStatistics.BLL.Contracts.Requests;
using SalesStatistics.BLL.Services;
using SalesStatistics.DataTransferObjects;
using SalesStatistics.DAL.Models;
using SalesStatistics.Web.Models.Requests;
using SalesStatistics.Web.Models.ViewModels;

namespace SalesStatistics.Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;

        public CustomerController()
        {
            _service = new CustomerService(new UnitOfWorkFactory());
        }
        // GET: Customer
        public ActionResult Index()
        {
            var filter = new CustomersRequestViewModel();
            ViewBag.Filter = filter;

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName")] CustomerViewModel customerViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(customerViewModel);
                }

                _service.Add(Mapper.Map<CustomerDTO>(customerViewModel));

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

                return View();
            }
        }

        // GET: Customer/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customerViewModel = Mapper.Map<CustomerViewModel>(_service.GetById((int)id));

            if (customerViewModel == null)
            {
                return HttpNotFound();
            }
            return View(customerViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName")] CustomerViewModel customerViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(customerViewModel);
                }

                _service.Update(Mapper.Map<CustomerDTO>(customerViewModel));

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

                return View(customerViewModel);
            }
        }

        // GET: Customer/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customerViewModel = Mapper.Map<CustomerViewModel>(_service.GetById((int)id));

            if (customerViewModel == null)
            {
                return HttpNotFound();
            }
            return View(customerViewModel);
        }

        // POST: Customer/Delete/5
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

                return View(Mapper.Map<CustomerViewModel>(_service.GetById(id)));
            }
        }

        public ActionResult GetCustomers()
        {
            var customers = Mapper.Map<IEnumerable<CustomerViewModel>>(_service.GetCustomers());

            return PartialView("_CustomersTable", customers);
        }

        public ActionResult Find(CustomersRequestViewModel request)
        {
            var customerRequest = new CustomersRequest()
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            return PartialView("_CustomersTable",
                Mapper.Map<IEnumerable<CustomerViewModel>>(_service.GetCustomers(customerRequest)));
        }
    }
}
