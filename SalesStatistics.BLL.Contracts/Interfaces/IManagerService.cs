using System.Collections.Generic;
using SalesStatistics.BLL.Contracts.Requests;
using SalesStatistics.DataTransferObjects;

namespace SalesStatistics.BLL.Contracts.Interfaces
{
    public interface IManagerService : IService<ManagerDTO>
    {
        IEnumerable<ManagerDTO> GetManagers(ManagersRequest request = null);
    }
}