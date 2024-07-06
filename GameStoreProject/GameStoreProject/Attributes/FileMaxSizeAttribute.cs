
namespace GameStoreProject.Attributes
{
    public class FileMaxSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSizeInBytes;

        public FileMaxSizeAttribute(int maxSizeInMegabytes)
        {
            _maxSizeInBytes = maxSizeInMegabytes * 1024 * 1024;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var fileLength = file.Length;
                if (fileLength > _maxSizeInBytes)
                    return new ValidationResult($"Your file is larger than {_maxSizeInBytes / (1024 * 1024)}MB.");
            }
            return ValidationResult.Success;
        }
    }
}
