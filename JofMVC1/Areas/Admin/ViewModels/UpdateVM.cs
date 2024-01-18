namespace JofMVC1.Areas.Admin.ViewModels
{
    public class UpdateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
