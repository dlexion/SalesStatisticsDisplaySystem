namespace SalesStatistics.DAL.Contracts.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Cost { get; set; }

        public ManagerDTO Manager { get; set; }
        public CustomerDTO Customer { get; set; }
        public ItemDTO Item { get; set; }
    }
}