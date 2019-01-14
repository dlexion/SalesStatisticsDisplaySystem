using System.ComponentModel.DataAnnotations;
using SalesStatistics.DataTransferObjects;

namespace SalesStatistics.Web.Models.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public System.DateTime Date { get; set; }

        [Required]
        [Display(Name = "Cost")]
        public decimal Cost { get; set; }

        [Required]
        [Display(Name = "Manager Last Name")]
        public string ManagerLastName { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Required]
        [Display(Name = "Customer First Name")]
        public string CustomerFirstName { get; set; }

        [Required]
        [Display(Name = "Customer Last Name")]
        public string CustomerLastName { get; set; }


        public ManagerDTO Manager { get; set; }
        public CustomerDTO Customer { get; set; }
        public ItemDTO Item { get; set; }
    }
}