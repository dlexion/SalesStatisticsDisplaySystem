using System.Collections.Generic;
using SalesStatistics.BLL.Contracts.Requests;
using SalesStatistics.DataTransferObjects;

namespace SalesStatistics.BLL.Contracts.Interfaces
{
    public interface IOrderService : IService<OrderDTO>
    {
        IEnumerable<OrderDTO> GetOrders(OrdersRequest request = null);
    }
}