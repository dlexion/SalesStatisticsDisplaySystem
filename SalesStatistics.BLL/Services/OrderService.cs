using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SalesStatistics.BLL.Contracts.Interfaces;
using SalesStatistics.DataTransferObjects;
using SalesStatistics.DAL.Contracts.Interfaces;

namespace SalesStatistics.BLL.Services
{
    public class OrderService : Service<OrderDTO>, IOrderService
    {
        public OrderService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public override void Add(OrderDTO order)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                var managerService = new ManagerService(_factory);
                managerService.TryAdd(order.Manager);

                var customerService = new CustomerService(_factory);
                customerService.TryAdd(order.Customer);

                var itemService = new ItemService(_factory);
                itemService.TryAdd(order.Item);

                unitOfWork.AddOrder(order);
            }
        }

        public override OrderDTO GetById(int id)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetOrders(x => x.Id == id).FirstOrDefault();
            }
        }

        public override void Remove(OrderDTO order)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                unitOfWork.RemoveOrder(order);
            }
        }

        public override void Update(OrderDTO order)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                unitOfWork.UpdateOrder(order);
            }
        }

        public IEnumerable<OrderDTO> GetOrders(OrdersRequest request = null)
        {
            if (request == null)
            {
                using (var unitOfWork = _factory.GetInstance())
                {
                    return unitOfWork.GetOrders();
                }

            }
            // TODO
            return null;
        }
    }
}