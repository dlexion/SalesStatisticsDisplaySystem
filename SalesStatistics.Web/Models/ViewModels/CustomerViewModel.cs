using System.ComponentModel.DataAnnotations;

namespace SalesStatistics.Web.Models.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "First Name must contain less than 25 characters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(25, ErrorMessage = "Last Name must contain less than 25 characters")]
        public string LastName { get; set; }
    }
}