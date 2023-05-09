using Pronia_BackEnd_Project.Models;

namespace Pronia_BackEnd_Project.ViewModels
{
    public class BlogDetailVM
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Desciption { get; set; }

        public ICollection<BlogImage> Images { get; set; }

        public IEnumerable<Blog> BlogAll { get; set; } 

        public IEnumerable<Tag> Tags { get; set; } 
        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Banner> Banners { get; set; }

        public string AuthorName { get; set; }

        public DateTime Created { get; set; }

    }
}
