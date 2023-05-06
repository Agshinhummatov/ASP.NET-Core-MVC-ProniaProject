namespace Pronia_BackEnd_Project.Models
{
    public class Size:BaseEntity
    {
        public string Name { get; set; }

        public ICollection<ProductSize> ProductSize { get; set; }

    }
}
