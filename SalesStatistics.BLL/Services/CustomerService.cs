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
    public class CustomerService : Service<CustomerDTO>, ICustomerService
    {
        public CustomerService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public override void Add(CustomerDTO customer)
        {
            Add(customer);
        }

        public void TryAdd(CustomerDTO customer)
        {
            Add(customer, true);
        }

        private void Add(CustomerDTO customer, bool safe = false)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                Expression<Func<CustomerDTO, bool>> customerSearchCriteria = (x => x.FirstName == customer.FirstName && x.LastName == customer.LastName);

                var customerFromDb = unitOfWork.GetCustomers(customerSearchCriteria).FirstOrDefault();

                if (customerFromDb == null)
                {
                    lock (_lockers[customer.GetType()])
                    {
                        if (unitOfWork.GetCustomers(customerSearchCriteria).FirstOrDefault() == null)
                        {
                            unitOfWork.AddCustomer(customer);
                        }
                    }
                }
                else if (!safe)
                    throw new ArgumentException("Such customer already exists");
            }
        }

        public override CustomerDTO GetById(int id)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetCustomers(x => x.Id == id).FirstOrDefault();
            }
        }

        public override void Remove(CustomerDTO customer)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                unitOfWork.RemoveCustomer(customer);
            }
        }

        public override void Update(CustomerDTO customer)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                Expression<Func<CustomerDTO, bool>> customerSearchCriteria = (x => x.FirstName == customer.FirstName && x.LastName == customer.LastName);
                var customerFromDb = unitOfWork.GetCustomers(customerSearchCriteria).FirstOrDefault();

                if (customerFromDb != null)
                {
                    throw new ArgumentException("Such customer already exists");
                }

                unitOfWork.UpdateCustomer(customer);
            }
        }

        public IEnumerable<CustomerDTO> GetCustomers(CustomersRequest request = null)
        {
            Expression<Func<CustomerDTO, bool>> firstNameExpression;
            Expression<Func<CustomerDTO, bool>> lastNameExpression;
            Expression<Func<CustomerDTO, bool>> finalExpression;

            if (request == null || (request.LastName == null && request.FirstName == null))
            {
                finalExpression = null;
            }
            else if (request.LastName == null || request.FirstName == null)
            {
                if (request.FirstName != null)
                {
                    finalExpression = x => x.FirstName == request.FirstName;
                }
                else
                {
                    finalExpression = x => x.LastName == request.LastName;
                }
            }
            else
            {
                firstNameExpression = x => x.FirstName == request.FirstName;
                lastNameExpression = x => x.LastName == request.LastName;

                finalExpression = firstNameExpression.And(lastNameExpression);
            }

            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetCustomers(finalExpression);
            }
        }
    }
}