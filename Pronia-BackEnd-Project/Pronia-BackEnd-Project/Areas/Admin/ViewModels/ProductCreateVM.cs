using System.ComponentModel.DataAnnotations;

namespace Pronia_BackEnd_Project.Areas.Admin.ViewModels
{
    public class ProductCreateVM
    {

        [Required(ErrorMessage = "Don't be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Don't be empty")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Don't be empty")]
        public int Rates { get; set; }

        [Required(ErrorMessage = "Don't be empty")]
        public int SaleCount { get; set; }

        [Required(ErrorMessage = "Don't be empty")]

        public int StockCount { get; set; }

        [Required(ErrorMessage = "Don't be empty")]
        public string Sku { get; set; }

        [Required(ErrorMessage = "Don't be empty")]
        public string Information { get; set; }

        public List<int> ProductColors { get; set; } = new();
        public List<int> ProductCategories { get; set; } = new();

        public List<int> ProductSize { get; set; } = new();


        public List<int> ProductTags { get; set; } = new();

        [Required(ErrorMessage = "Don't be empty")]
        public List<IFormFile> Photos { get; set; }




    }
}
