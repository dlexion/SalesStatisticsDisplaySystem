﻿namespace SalesStatistics.DAL.Contracts.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Cost { get; set; }

        public virtual ManagerDTO Manager { get; set; }
        public virtual CustomerDTO Customer { get; set; }
        public virtual ItemDTO Item { get; set; }
    }
}