



using GameStoreProject.Attributes;

namespace GameStoreProject.ViewModels
{
    public class CreateGameViewModel: GameViewModel
    {
        [FileExtension(validExtensions: FileSettings.FileExtensions),
            FileMaxSize(maxSizeInMegabytes: FileSettings.MaxSizeInMB)]
        public IFormFile Cover { get; set; } = default!;
    }
}
