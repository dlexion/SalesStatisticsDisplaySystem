using System.ComponentModel.DataAnnotations;

namespace SalesStatistics.Web.Models.ViewModels
{
    public class ManagerViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(25, ErrorMessage = "Last Name must contain less than 25 characters")]
        public string LastName { get; set; }
    }
}