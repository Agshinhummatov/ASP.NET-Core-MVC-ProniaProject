using System.ComponentModel.DataAnnotations;

namespace Pronia_BackEnd_Project.Areas.Admin.ViewModels
{
    public class ColorCreateVM
    {
        [Required]
        public string Name { get; set; }

    }

}
