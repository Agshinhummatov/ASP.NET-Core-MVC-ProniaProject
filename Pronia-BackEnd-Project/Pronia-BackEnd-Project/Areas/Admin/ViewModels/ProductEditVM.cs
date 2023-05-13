using Pronia_BackEnd_Project.Models;
using System.ComponentModel.DataAnnotations;

namespace Pronia_BackEnd_Project.Areas.Admin.ViewModels
{
    public class ProductEditVM
    {

        public int Id { get; set; }
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

        public List<int> ProductColorsId { get; set; } = new();
        public List<int> ProductCategoriesId { get; set; } = new();

        public List<int> ProductSizeId { get; set; } = new();

        public List<int> ProductTagsId { get; set; } = new();

    
        public List<IFormFile> Photos { get; set; }

        public ICollection<ProductImage> Images { get; set; }

    }
}
