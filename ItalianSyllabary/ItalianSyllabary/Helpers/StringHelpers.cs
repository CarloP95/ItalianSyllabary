namespace ItalianSyllabary.Helpers
{
    internal static class StringHelpers
    {

        /// <summary>
        /// Check if the string is a diphthong.
        /// It consider accents, see example
        /// </summary>
        /// <param name="chars"></param>
        /// <returns>true if the string has diphthongs, false otherwise</returns>
        /// <example>
        /// "maria".HasDiphtong is true
        /// "marìa".HasDiphtong is false
        /// </example>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool HasDiphthong(this string chars)
        {
            ArgumentNullException.ThrowIfNull(chars, nameof(chars));

            var r = new System.Text.RegularExpressions.Regex(Constants.Common.DiphthongsRegexp);

            return r.Match(chars).Success;
        }


        /// <summary>
        /// Clean accents from string
        /// </summary>
        /// <param name="str"></param>
        /// <returns>The cleaned string</returns>
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
