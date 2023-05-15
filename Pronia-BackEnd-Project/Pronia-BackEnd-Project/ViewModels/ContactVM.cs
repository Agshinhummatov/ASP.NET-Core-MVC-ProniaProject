using System.ComponentModel.DataAnnotations;

namespace Pronia_BackEnd_Project.ViewModels
{
    public class ContactVM
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
