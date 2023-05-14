using System.ComponentModel.DataAnnotations;

namespace Pronia_BackEnd_Project.Areas.Admin.ViewModels
{
    public class AdvertisingCreateVM
    {

        [Required(ErrorMessage = "Don't be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Don't be empty")]
       
        public IFormFile Photo { get; set; }
    }
}
