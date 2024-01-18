using JofMVC1.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace JofMVC1.Models
{
    public class Fruit:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
