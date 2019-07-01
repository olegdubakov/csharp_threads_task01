namespace SimpleSynchronization.Helpers
{
    using System;

    internal static class FileNameBuilder
    {
        public static string GenerateFileName(string prefix, string extension)
        {
            return $"{prefix}_{Guid.NewGuid()}.{extension}";
        }
    }
}
