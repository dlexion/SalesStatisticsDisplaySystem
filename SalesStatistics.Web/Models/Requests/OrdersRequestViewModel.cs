namespace SalesStatistics.Web.Models.Requests
{
    public class OrdersRequestViewModel
    {

        public decimal? Cost { get; set; }

        public ManagersRequestViewModel ManagersRequest { get; set; }

        public CustomersRequestViewModel CustomersRequest { get; set; }
    }
}