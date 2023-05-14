using System.ComponentModel.DataAnnotations;

namespace Pronia_BackEnd_Project.Areas.Admin.ViewModels
{
    public class SliderEditVM
    {

        [Required]
        public string Offer { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }

     
        public IFormFile Photo { get; set; }
        public string BackGroundImage { get; set; }

        public IFormFile PhotoBackGround { get; set; }
    }
}
