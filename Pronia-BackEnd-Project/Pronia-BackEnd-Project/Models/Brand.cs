using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pronia_BackEnd_Project.Models
{
    public class Brand:BaseEntity
    {
        public string Image { get; set; }


        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}
