namespace GameStoreProject.Attributes
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        private readonly string _validExtensions;

        public FileExtensionAttribute(string validExtensions)
        {
            _validExtensions = validExtensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
                var FileExtension=Path.GetExtension(file.FileName);
                var Extensions=_validExtensions.Split(',');
                var IsFound=Extensions.Contains(FileExtension,StringComparer.OrdinalIgnoreCase);
                if (!IsFound)
                    return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return $"Invalid file extension. Allowed extensions are: {_validExtensions}.";
        }
    }

}
