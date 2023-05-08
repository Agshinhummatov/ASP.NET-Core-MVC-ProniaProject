using Pronia_BackEnd_Project.Helpers;
using Pronia_BackEnd_Project.Models;

namespace Pronia_BackEnd_Project.ViewModels
{
    public class ShopVM
    {

        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Color> Colors { get; set; }

        public IEnumerable<Tag> Tags { get; set; }


        public Paginate<Product> PaginatedDatas { get; set; }





    }
}
