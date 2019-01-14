using System.Collections.Generic;
using SalesStatistics.BLL.Contracts.Requests;
using SalesStatistics.DataTransferObjects;

namespace SalesStatistics.BLL.Contracts.Interfaces
{
    public interface ICustomerService : IService<CustomerDTO>
    {
        IEnumerable<CustomerDTO> GetCustomers(CustomersRequest request = null);
    }
}