using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SalesStatistics.BLL.Contracts.Interfaces;
using SalesStatistics.BLL.Contracts.Requests;
using SalesStatistics.BLL.Extensions;
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
            if (request == null || AreAllPropertiesNull(request))
            {
                using (var unitOfWork = _factory.GetInstance())
                {
                    return unitOfWork.GetOrders();
                }

            }

            Expression<Func<OrderDTO, bool>> finalExpression = null;

            if (request.Cost != null)
            {
                Expression<Func<OrderDTO, bool>> exp = x => x.Cost == request.Cost;
                finalExpression = CombineExpressions(finalExpression, exp);
            }

            if (request.CustomersRequest != null)
            {
                if (request.CustomersRequest.LastName != null)
                {
                    Expression<Func<OrderDTO, bool>> exp = x => x.Customer.LastName == request.CustomersRequest.LastName;
                    finalExpression = CombineExpressions(finalExpression, exp);
                }

                if (request.CustomersRequest.FirstName != null)
                {
                    Expression<Func<OrderDTO, bool>> exp = x => x.Customer.FirstName == request.CustomersRequest.FirstName;
                    finalExpression = CombineExpressions(finalExpression, exp);
                }
            }


            if (request.ManagersRequest != null && request.ManagersRequest.LastName != null)
            {
                Expression<Func<OrderDTO, bool>> exp = x => x.Manager.LastName == request.ManagersRequest.LastName;
                finalExpression = CombineExpressions(finalExpression, exp);
            }

            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetOrders(finalExpression);
            }
        }

        private bool AreAllPropertiesNull(OrdersRequest request)
        {
            if (request.Cost == null &&
                request.CustomersRequest == null &&
                request.ManagersRequest == null)
            {
                return true;
            }
            if (request.CustomersRequest != null &&
                     request.ManagersRequest != null &&
                     request.Cost == null &&
                     request.CustomersRequest.LastName == null &&
                     request.CustomersRequest.FirstName == null &&
                     request.ManagersRequest.LastName == null)
                return true;

            return false;
        }

        private Expression<Func<OrderDTO, bool>> CombineExpressions(Expression<Func<OrderDTO, bool>> ex1, Expression<Func<OrderDTO, bool>> ex2)
        {
            if (ex1 != null && ex2 != null)
            {
                return ex1.Or(ex2);
            }

            if (ex1 != null)
            {
                return ex1;
            }

            if (ex2 != null)
            {
                return ex2;
            }

            return null;
        }
    }
}