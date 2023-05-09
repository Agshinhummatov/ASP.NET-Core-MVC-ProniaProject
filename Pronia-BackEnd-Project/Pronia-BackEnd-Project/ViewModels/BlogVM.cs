using Pronia_BackEnd_Project.Models;

namespace Pronia_BackEnd_Project.ViewModels
{
    public class BlogVM
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Tag> Tags  { get; set; }

        public IEnumerable<Banner> Banners { get; set; }

    }
}
