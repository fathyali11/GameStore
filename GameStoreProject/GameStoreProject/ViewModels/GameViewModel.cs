



using GameStoreProject.Attributes;

namespace GameStoreProject.ViewModels
{
    public class GameViewModel
    {
        [Required(ErrorMessage ="Please Enter Name")]
        public string Name { get; set; }=string.Empty;
        [Required(ErrorMessage = "Please Enter Description")]
        public string Description { get; set; } = string.Empty;
        [Display(Name="Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Categories")]
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        [Display(Name="Supported Devices")]
        public IEnumerable<SelectListItem> Devices { get; set; }=Enumerable.Empty<SelectListItem>();
        [Display(Name = "Devices")]
        public List<int> SelectedDevices { get; set; } = default!;
        [FileExtension(".jpg,.jpeg,.webp")]
        public IFormFile Cover { get; set; } = default!;
    }
}
