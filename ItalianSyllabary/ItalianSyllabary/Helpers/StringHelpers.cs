namespace ItalianSyllabary.Helpers
{
    internal static class StringHelpers
    {

        /// <summary>
        /// Check if the string is a diphthong
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static bool IsDiphthong(this string chars)
        {
            ArgumentNullException.ThrowIfNull(chars, nameof(chars));

            return false;
        }


        /// <summary>
        /// Clean accents from string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CleanAccents(this string str)
        {
            return str
                .Replace('à', 'a')
                .Replace('á', 'a')
                .Replace('è', 'e')
                .Replace('é', 'e')
                .Replace('ì', 'i')
                .Replace('í', 'i')
                .Replace('ò', 'o')
                .Replace('ó', 'o')
                .Replace('ù', 'u')
                .Replace('ú', 'u');
        }

    }
}
