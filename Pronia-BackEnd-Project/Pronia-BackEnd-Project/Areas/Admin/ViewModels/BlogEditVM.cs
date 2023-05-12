using Pronia_BackEnd_Project.Models;
using System.ComponentModel.DataAnnotations;

namespace Pronia_BackEnd_Project.Areas.Admin.ViewModels
{
    public class BlogEditVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public string Title { get; set; }
       
       

        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }
        public ICollection<BlogImage> Images { get; set; }
        public List<IFormFile> Photos { get; set; }


    }
}
