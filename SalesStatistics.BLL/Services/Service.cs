using System;
using System.Collections.Generic;
using SalesStatistics.BLL.Contracts.Interfaces;
using SalesStatistics.DataTransferObjects;
using SalesStatistics.DAL.Contracts.Interfaces;

namespace SalesStatistics.BLL.Services
{
    public abstract class Service<T> : IService<T> where T : class
    {
        protected readonly Dictionary<Type, object> _lockers = new Dictionary<Type, object>()
        {
            {typeof(ManagerDTO), new object() },
            {typeof(CustomerDTO), new object() },
            {typeof(ItemDTO), new object() }
        };

        protected IUnitOfWorkFactory _factory;

        public abstract void Add(T model);
        public abstract T GetById(int id);
        public abstract void Remove(T model);
        public abstract void Update(T model);
    }
}