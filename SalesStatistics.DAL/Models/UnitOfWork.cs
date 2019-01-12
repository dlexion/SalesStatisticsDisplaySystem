using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using AutoMapper;
using SalesStatistics.DataTransferObjects;
using SalesStatistics.DAL.Contracts.Interfaces;
using SalesStatistics.DAL.Extensions;
using SalesStatistics.DAL.Repositories;

namespace SalesStatistics.DAL.Models
{
    // TODO mb split to separate UoW for each repository
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContext _context;
        private IGenericRepository<Customer> _customers;
        private IGenericRepository<Item> _items;
        private IGenericRepository<Manager> _managers;
        private IGenericRepository<Order> _orders;

        public UnitOfWork()
        {
            _context = new ReportContext();

            _customers = new GenericRepository<Customer>(_context);
            _items = new GenericRepository<Item>(_context);
            _managers = new GenericRepository<Manager>(_context);
            _orders = new GenericRepository<Order>(_context);
        }

        #region Items methods

        public IEnumerable<ItemDTO> GetItems(Expression<Func<ItemDTO, bool>> predicate = null)
        {
            var newExpression = predicate?.Project<ItemDTO, Item>();

            var result = _items.Get(newExpression);

            return Mapper.Map<IEnumerable<ItemDTO>>(result);
        }

        public void AddItem(ItemDTO item)
        {
            var entity = Mapper.Map<Item>(item);

            _items.Add(entity);

            Save();
        }

        public void RemoveItem(ItemDTO item)
        {
            var entity = Mapper.Map<Item>(item);

            _items.Remove(entity);

            Save();
        }

        public void UpdateItem(ItemDTO item)
        {
            var entity = Mapper.Map<Item>(item);

            _items.Update(entity);

            Save();

            //TODO update item id like id = entity.id
        }

        #endregion

        #region Customers methods

        public IEnumerable<CustomerDTO> GetCustomers(Expression<Func<CustomerDTO, bool>> predicate = null)
        {
            var newExpression = predicate?.Project<CustomerDTO, Customer>();

            var result = _customers.Get(newExpression);

            return Mapper.Map<IEnumerable<CustomerDTO>>(result);
        }

        public void AddCustomer(CustomerDTO item)
        {
            var entity = Mapper.Map<Customer>(item);

            _customers.Add(entity);

            Save();
        }

        public void RemoveCustomer(CustomerDTO item)
        {
            var entity = Mapper.Map<Customer>(item);

            _customers.Remove(entity);

            Save();
        }

        public void UpdateCustomer(CustomerDTO item)
        {
            var entity = Mapper.Map<Customer>(item);

            _customers.Update(entity);

            Save();
        }

        #endregion

        #region Managers methods

        public IEnumerable<ManagerDTO> GetManagers(Expression<Func<ManagerDTO, bool>> predicate = null)
        {
            var newExpression = predicate?.Project<ManagerDTO, Manager>();

            var result = _managers.Get(newExpression);

            return Mapper.Map<IEnumerable<ManagerDTO>>(result);
        }

        public void AddManager(ManagerDTO item)
        {
            var entity = Mapper.Map<Manager>(item);

            _managers.Add(entity);

            Save();
        }

        public void RemoveManager(ManagerDTO item)
        {
            var entity = Mapper.Map<Manager>(item);

            _managers.Remove(entity);

            Save();
        }

        public void UpdateManager(ManagerDTO item)
        {
            var entity = Mapper.Map<Manager>(item);

            _managers.Update(entity);

            Save();
        }

        #endregion

        #region Orders methods

        public IEnumerable<OrderDTO> GetOrders(Expression<Func<OrderDTO, bool>> predicate = null)
        {
            var newExpression = predicate?.Project<OrderDTO, Order>();

            var result = _orders.Get(newExpression);

            return Mapper.Map<IEnumerable<OrderDTO>>(result);
        }

        public void AddOrder(OrderDTO item)
        {
            var entity = Mapper.Map<Order>(item);

            _orders.Add(entity);

            Save();
        }

        public void RemoveOrder(OrderDTO item)
        {
            var entity = Mapper.Map<Order>(item);

            _orders.Remove(entity);

            Save();
        }

        public void UpdateOrder(OrderDTO item)
        {
            var entity = Mapper.Map<Order>(item);

            _orders.Update(entity);

            Save();
        }

        #endregion

        private void Save()
        {
            _context.SaveChanges();
        }

        #region IDisposable implementation

        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _customers = null;
                    _items = null;
                    _managers = null;
                    _orders = null;
                }

                _context.Dispose();
                _context = null;

                _disposedValue = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}