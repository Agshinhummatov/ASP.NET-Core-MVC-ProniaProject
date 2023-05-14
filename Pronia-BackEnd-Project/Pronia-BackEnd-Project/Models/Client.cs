using System.ComponentModel.DataAnnotations.Schema;

namespace Pronia_BackEnd_Project.Models
{
    public class Client :BaseEntity
    {
            public string Image { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

           [NotMapped]
           public IFormFile Photo { get; set; }
    }
}
