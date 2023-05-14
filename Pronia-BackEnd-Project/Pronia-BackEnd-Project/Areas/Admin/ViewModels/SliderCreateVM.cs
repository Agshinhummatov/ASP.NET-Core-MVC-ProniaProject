using System.ComponentModel.DataAnnotations;

namespace Pronia_BackEnd_Project.Areas.Admin.ViewModels
{
    public class SliderCreateVM
    {
        [Required]
        public string Offer { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
     
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public IFormFile BackGroundPhoto { get; set; }

    }
}
