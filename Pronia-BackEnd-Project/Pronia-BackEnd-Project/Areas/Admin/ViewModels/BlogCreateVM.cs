using System.ComponentModel.DataAnnotations;

namespace Pronia_BackEnd_Project.Areas.Admin.ViewModels
{
    public class BlogCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }

        public int AuthorId { get; set; }  // category id ni verecek bize

        [Required(ErrorMessage = "Don't be empty")]
        public List<IFormFile> Photos { get; set; }
    }
}
