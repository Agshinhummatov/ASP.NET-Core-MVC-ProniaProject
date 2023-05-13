namespace Pronia_BackEnd_Project.Areas.Admin.ViewModels
{
    public class ProdcutCreateVM
    {


        public string Name { get; set; }
        public string Description { get; set; }

        public string Price { get; set; }

        public int Rates { get; set; }
        public int SaleCount { get; set; }

        public int StockCount { get; set; }

        public string Sku { get; set; }

        public string Information { get; set; }

        public List<int> ProductColors { get; set; } = new();
        public List<int> ProductCategories { get; set; } = new();

        public List<int> ProductSize { get; set; } = new();


        public List<int> ProductTags { get; set; } = new();


        public List<IFormFile> Photos { get; set; }




    }
}
