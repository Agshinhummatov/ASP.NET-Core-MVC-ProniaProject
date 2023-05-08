using Pronia_BackEnd_Project.Models;

namespace Pronia_BackEnd_Project.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        //public IEnumerable<Advertising> Advertisings { get; set; }

        public IEnumerable<Client> Clients { get; set; }

       

        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Banner> Banners { get; set; }

        public IEnumerable<Product> Product { get; set; }

    }
}
