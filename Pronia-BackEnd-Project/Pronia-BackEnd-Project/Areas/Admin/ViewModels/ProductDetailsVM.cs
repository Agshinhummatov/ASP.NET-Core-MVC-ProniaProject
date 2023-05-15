namespace Pronia_BackEnd_Project.Areas.Admin.ViewModels
{
    public class ProductDetailsVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int SaleCount { get; set; }
        public int StockCount { get; set; }
        public string SKU { get; set; }
        public int Rating { get; set; }
        public IEnumerable<string> Images { get; set; }
         public IEnumerable<string> ColorName { get; set; }
      

        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<string> Sizes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
