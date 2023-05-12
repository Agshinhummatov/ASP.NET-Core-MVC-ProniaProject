using Pronia_BackEnd_Project.Models;

namespace Pronia_BackEnd_Project.Areas.Admin.ViewModels
{
    public class BlogDetailVM
    {

       
        public string Title { get; set; }


        public string Description { get; set; }

        public int AuthorId { get; set; }

    
        public string AuthorName { get; set; }

        public ICollection<BlogImage> Images { get; set; }
        public List<IFormFile> Photos { get; set; }

    }
}
