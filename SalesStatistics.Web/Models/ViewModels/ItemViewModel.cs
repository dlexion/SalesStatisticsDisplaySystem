using System.ComponentModel.DataAnnotations;

namespace SalesStatistics.Web.Models.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(25, ErrorMessage = "Name must contain less than 25 characters")]
        public string Name { get; set; }
    }
}