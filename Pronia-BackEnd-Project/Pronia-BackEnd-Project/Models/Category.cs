namespace Pronia_BackEnd_Project.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }


    }
}
