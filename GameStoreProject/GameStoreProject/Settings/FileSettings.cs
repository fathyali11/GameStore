namespace GameStoreProject.Settings
{
    public static class FileSettings
    {
        public static string FileExtensions = ".jpg,.jpeg,.webp";
        public static string ImagePath = "Images/Games";
        public const int MaxSizeInMB = 1;
        public const int MaxSizeInByte = MaxSizeInMB*1024*1024;
    }
}
