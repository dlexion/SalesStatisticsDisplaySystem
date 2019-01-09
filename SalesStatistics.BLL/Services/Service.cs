using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SalesStatistics.BLL.Contracts.Interfaces;
using SalesStatistics.DAL.Contracts.DTO;
using SalesStatistics.DAL.Contracts.Interfaces;

namespace SalesStatistics.BLL.Services
{
    public class Service : IService
    {
        private readonly Dictionary<Type, object> _lockers = new Dictionary<Type, object>()
        {
            {typeof(ManagerDTO), new object() },
            {typeof(CustomerDTO), new object() },
            {typeof(ItemDTO), new object() }
        };

        private readonly IUnitOfWorkFactory _factory;

        public Service(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public void AddCustomer(CustomerDTO customer)
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
                            // TODO check saving id
                            unitOfWork.AddCustomer(customer);
                        }
                    }
                }
                // TODO report about existing customer
            }
        }

        public void AddItem(ItemDTO item)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                Expression<Func<ItemDTO, bool>> itemSearchCriteria = x => x.Name == item.Name;

                var itemFromDb = unitOfWork.GetItems(itemSearchCriteria).FirstOrDefault();

                if (itemFromDb != null)
                {
                    return;
                }

                lock (_lockers[item.GetType()])
                {
                    if (unitOfWork.GetItems(itemSearchCriteria).FirstOrDefault() == null)
                    {
                        unitOfWork.AddItem(item);
                    }
                }
            }
        }

        public void AddManager(ManagerDTO manager)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                Expression<Func<ManagerDTO, bool>> managerSearchCriteria = x => x.LastName == manager.LastName;

                var managerFromDb = unitOfWork.GetManagers(managerSearchCriteria).FirstOrDefault();

                if (managerFromDb == null)
                {
                    lock (_lockers[manager.GetType()])
                    {
                        if (unitOfWork.GetManagers(managerSearchCriteria).FirstOrDefault() == null)
                        {
                            unitOfWork.AddManager(manager);
                        }
                    }
                }
            }
        }

        public void AddOrder(OrderDTO item)
        {
            // TODO Save order correctly
        }

        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetCustomers();
            }
        }

        public IEnumerable<ItemDTO> GetAllItems()
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetItems();
            }
        }

        public IEnumerable<ManagerDTO> GetAllManagers()
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetManagers();
            }
        }

        public IEnumerable<OrderDTO> GetAllOrders()
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetOrders();
            }
        }

        public CustomerDTO GetCustomerById(int id)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetCustomers(x => x.Id == id).FirstOrDefault();
            }
        }

        public CustomerDTO GetCustomerByName(string firstName, string lastName)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetCustomers(x => x.FirstName == firstName && x.LastName == lastName).FirstOrDefault();
            }
        }

        public ItemDTO GetItemById(int id)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetItems(x => x.Id == id).FirstOrDefault();
            }
        }

        public ItemDTO GetItemByName(string name)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetItems(x => x.Name == name).FirstOrDefault();
            }
        }

        public ManagerDTO GetManagerById(int id)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetManagers(x => x.Id == id).FirstOrDefault();
            }
        }

        public ManagerDTO GetManagerByName(string lastName)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetManagers(x => x.LastName == lastName).FirstOrDefault();
            }
        }

        public OrderDTO GetOrderById(int id)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetOrders(x => x.Id == id).FirstOrDefault();
            }
        }

        public IEnumerable<OrderDTO> GetOrdersByCustomer(int customerId)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetOrders(x => x.Customer.Id == customerId);
            }
        }

        public IEnumerable<OrderDTO> GetOrdersByDate(DateTime leftBound, DateTime rightBound)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetOrders(x => x.Date > leftBound && x.Date < rightBound);
            }
        }

        public IEnumerable<OrderDTO> GetOrdersByItem(int itemId)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetOrders(x => x.Item.Id == itemId);
            }
        }

        public IEnumerable<OrderDTO> GetOrdersByManager(int managerId)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                return unitOfWork.GetOrders(x => x.Manager.Id == managerId);
            }
        }

        public void RemoveCustomer(CustomerDTO customer)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                unitOfWork.RemoveCustomer(customer);
            }
        }

        public void RemoveItem(ItemDTO item)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                unitOfWork.RemoveItem(item);
            }
        }

        public void RemoveManager(ManagerDTO manager)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                unitOfWork.RemoveManager(manager);
            }
        }

        public void RemoveOrder(OrderDTO order)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                unitOfWork.RemoveOrder(order);
            }
        }

        public void UpdateCustomer(CustomerDTO customer)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                unitOfWork.UpdateCustomer(customer);
            }
        }

        public void UpdateItem(ItemDTO item)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                unitOfWork.UpdateItem(item);
            }
        }

        public void UpdateManager(ManagerDTO manager)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                unitOfWork.UpdateManager(manager);
            }
        }

        public void UpdateOrder(OrderDTO order)
        {
            using (var unitOfWork = _factory.GetInstance())
            {
                unitOfWork.UpdateOrder(order);
            }
        }
    }
}