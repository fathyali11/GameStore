using GameStoreProject.Attributes;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GameStoreProject.ViewModels
{
    public class EditGameViewModel:GameViewModel
    {
        public int Id { get; set; }
        public string? CoverName { get; set; }
        [FileExtension(validExtensions: FileSettings.FileExtensions),
            FileMaxSize(maxSizeInMegabytes: FileSettings.MaxSizeInByte)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
