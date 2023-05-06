namespace Pronia_BackEnd_Project.Models
{
    public class Color:BaseEntity
    {
        public string Name { get; set; }

        public ICollection<ProductColor> ProductColors { get; set; }

    }
}
