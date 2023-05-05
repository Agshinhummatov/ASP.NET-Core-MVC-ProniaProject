namespace Pronia_BackEnd_Project.Models
{
    public class Author:BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Blog> Blogs { get; set; }


    }
}
