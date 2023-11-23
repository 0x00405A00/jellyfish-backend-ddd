namespace Shared.MimePart
{
    public static class MimeExtension
    {

        public static Dictionary<string, string> MimeTypeToExtension = new Dictionary<string, string>
        {
            { "image/jpeg", ".jpg" },
            { "image/png", ".png" },
            { "image/gif", ".gif" },
            { "image/bmp", ".bmp" },
            { "image/tiff", ".tiff" },
            { "image/webp", ".webp" },
        };
        public static bool IsValidMimeTypeForMediaContent(string mimeType) => mimeType is not null && MimeTypeToExtension.ContainsKey(mimeType);
        public static string GetFileExtension(string mimeType)
        {
            if(mimeType is null) throw new ArgumentNullException(nameof(mimeType));
            string lowerMimeType = mimeType.ToLower();

            if (IsValidMimeTypeForMediaContent(lowerMimeType))
            {
                return MimeTypeToExtension[lowerMimeType];
            }

            return ".dat";//Fallback for undefined type
        }
        public static string? GetImageAsMime(string base64Str, string mimeType)
        {

            if (base64Str is null || !MimeExtension.IsValidMimeTypeForMediaContent(mimeType))
                return null;

            return $"data:{mimeType};base64,{base64Str}";
        }
    }
}
