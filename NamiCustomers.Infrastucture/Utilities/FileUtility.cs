namespace NamiCustomers.Infrastucture.Utilities
{
    public static class FileUtility
    {
        public static bool IsImage(this string fileExtention, string[] acceptableExtensions)
        {
            if (!acceptableExtensions.Contains(fileExtention))
            {
                return false;
            }
            return true;
        }
        public static bool AcceptableFileSize(this long fileSize, int maxSize, bool byMB = true)
        {
            if (byMB && fileSize > maxSize * 1024 * 1024)
            {
                return false;
            }
            if (!byMB && fileSize > maxSize * 1024)
            {
                return false;
            }
            return true;
        }
    }
}
