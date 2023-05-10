namespace Pronia_BackEnd_Project.Models
{
    public class Blog:BaseEntity
    {
        public string Title { get; set; }

        public string Desciption { get; set; }

        public ICollection<BlogImage> Images { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

      


    }
}
