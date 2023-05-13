using System.ComponentModel.DataAnnotations;

namespace Pronia_BackEnd_Project.Areas.Admin.ViewModels
{
    public class TeamCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Don't be empty")]
        public string Position { get; set; }


        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile Photo { get; set; }

    }
}
