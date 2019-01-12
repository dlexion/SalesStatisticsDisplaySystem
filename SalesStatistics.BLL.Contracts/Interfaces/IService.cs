using System;
using System.Collections.Generic;
using SalesStatistics.DataTransferObjects;

namespace SalesStatistics.BLL.Contracts.Interfaces
{
    // TODO move DTO objects to separate assembly
    // TODO split interface into several simpler interfaces
    // TODO make generic interface with add, getAll, remove and update methods
    public interface IService
    {
        void AddCustomer(CustomerDTO customer);
        IEnumerable<CustomerDTO> GetAllCustomers();
        CustomerDTO GetCustomerById(int id);
        CustomerDTO GetCustomerByName(string firstName, string lastName);
        void RemoveCustomer(CustomerDTO customer);
        void UpdateCustomer(CustomerDTO customer);

        void AddItem(ItemDTO item);
        IEnumerable<ItemDTO> GetAllItems();
        ItemDTO GetItemById(int id);
        ItemDTO GetItemByName(string name);
        void RemoveItem(ItemDTO item);
        void UpdateItem(ItemDTO item);

        void AddManager(ManagerDTO manager);
        IEnumerable<ManagerDTO> GetAllManagers();
        ManagerDTO GetManagerById(int id);
        ManagerDTO GetManagerByName(string lastName);
        void RemoveManager(ManagerDTO manager);
        void UpdateManager(ManagerDTO manager);

        void AddOrder(OrderDTO order);
        IEnumerable<OrderDTO> GetAllOrders();
        IEnumerable<OrderDTO> GetOrdersByDate(DateTime leftBound, DateTime rightBound);
        IEnumerable<OrderDTO> GetOrdersByManager(int managerId);
        IEnumerable<OrderDTO> GetOrdersByItem(int itemId);
        IEnumerable<OrderDTO> GetOrdersByCustomer(int customerId);

        //IEnumerable<OrderDTO> GetOrdersr(OrdersRequest request);

        OrderDTO GetOrderById(int id);
        void RemoveOrder(OrderDTO order);
        void UpdateOrder(OrderDTO order);
    }
}