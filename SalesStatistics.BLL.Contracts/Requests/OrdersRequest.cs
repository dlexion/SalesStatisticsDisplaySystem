using System;

namespace SalesStatistics.BLL.Contracts.Requests
{
    public class OrdersRequest
    {
        //public DateTime? DateFrom { get; set; }

        //public DateTime? DateTo { get; set; }

        public decimal? Cost { get; set; }

        public ManagersRequest ManagersRequest { get; set; }

        public CustomersRequest CustomersRequest { get; set; }
    }
}