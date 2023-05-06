namespace Pronia_BackEnd_Project.Models
{
    public class Tag:BaseEntity
    {
        public string Name { get; set; }

        public ICollection<ProductTag> ProductTags { get; set; }
    }
}
