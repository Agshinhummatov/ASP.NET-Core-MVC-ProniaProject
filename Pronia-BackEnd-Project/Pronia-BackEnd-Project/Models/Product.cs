namespace Pronia_BackEnd_Project.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Rates { get; set; }
        public int SaleCount { get; set; }

        public int StockCount { get; set; }

        public string Sku { get; set; }

        public string Information { get; set; }


        public ICollection<ProductColor> ProductColors { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }

        public ICollection<ProductSize> ProductSize { get; set; }


        public ICollection<ProductTag> ProductTags { get; set; }


        public ICollection<ProductImage> ProductImages { get; set; }



    }
}
