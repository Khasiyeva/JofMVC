using System.ComponentModel.DataAnnotations.Schema;

namespace JofMVC1.Areas.Admin.ViewModels
{
    public class CreateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
