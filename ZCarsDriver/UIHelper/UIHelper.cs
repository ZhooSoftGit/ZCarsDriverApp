namespace ZCarsDriver.UIHelper
{
    public static class UIHelper
    {
        #region Methods

        public static double GetFileSizeAsync(FileResult fileResult)
        {
            var filePath = fileResult.FullPath; // Works on Windows, MacCatalyst, and Android (not iOS)
            var fileInfo = new FileInfo(filePath);
            var kbsize = fileInfo.Length / 1024.0;
            return kbsize; // File size in bytes
        }

        public static async Task<ImageSource?> GetImageSource(FileResult? result = null)
        {
            if (result == null)
                result = await FilePicker.Default.PickAsync();

            if (result != null && (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase)))
            {
                Stream? imageStream = await result.OpenReadAsync().ConfigureAwait(true);
                if (imageStream is not null)
                {
                    // Create a copy of the stream to avoid disposing issues
                    MemoryStream copyStream = new();
                    await imageStream.CopyToAsync(copyStream).ConfigureAwait(true);
                    copyStream.Position = 0; // Reset position to the beginning

                    // Use the copied stream to create the ImageSource
                    return ImageSource.FromStream(() => new MemoryStream(copyStream.ToArray())); // <=== Include a new MemoryStream
                }
            }

            return null;
        }

        public static async Task<FileResult> PickFile()
        {
            var result = await FilePicker.Default.PickAsync();
            return result;
        }

        #endregion
    }
}
