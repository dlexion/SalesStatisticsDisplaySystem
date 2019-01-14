using System.Collections.Generic;
using SalesStatistics.BLL.Contracts.Requests;
using SalesStatistics.DataTransferObjects;

namespace SalesStatistics.BLL.Contracts.Interfaces
{
    public interface IItemService : IService<ItemDTO>
    {
        IEnumerable<ItemDTO> GetItems(ItemsRequest request = null);
    }
}