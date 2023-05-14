using System.ComponentModel.DataAnnotations.Schema;

namespace Pronia_BackEnd_Project.Models
{
    public class Advertising:BaseEntity  
    {

        public string Name { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

        public string Description { get; set; }

      
    }
}
