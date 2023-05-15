using System.ComponentModel.DataAnnotations;

namespace Pronia_BackEnd_Project.Areas.Admin.ViewModels
{
    public class TeamEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }

        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}
