namespace Pronia_BackEnd_Project.Models
{
    public class Banner:BaseEntity
    {
        public string Image { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public bool IsLarge { get; set; }


    }
}
