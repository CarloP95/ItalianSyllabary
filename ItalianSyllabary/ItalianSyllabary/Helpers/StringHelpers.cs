namespace ItalianSyllabary.Helpers
{
    internal static class StringHelpers
    {

        private const string Space = " ";


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
        /// Check if the word has space in it
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool HasSpaces(this string word) => word.Contains(Space);


        /// <summary>
        /// Check if the word has more words in it
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool HasMoreThanAWord(this string word) =>
            word.HasSpaces()
            && word.Split(Space, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Length > 1;


        /// <summary>
        /// Check if str has accents
        /// </summary>
        /// <param name="str"></param>
        /// <returns>true if has accents, otherwise false</returns>
        public static bool HasAccents(this string str)
        {
            return str.Contains('à')
                || str.Contains('á')
                || str.Contains('è')
                || str.Contains('é')
                || str.Contains('ì')
                || str.Contains('í')
                || str.Contains('ò')
                || str.Contains('ó')
                || str.Contains('ù')
                || str.Contains('ú');
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
