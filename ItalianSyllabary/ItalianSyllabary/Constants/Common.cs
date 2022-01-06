namespace ItalianSyllabary.Constants
{
    internal static class Common
    {

        internal const string PageNotFound = "404 - No Page found";


        internal static readonly char[] NotAllowedLetters = { 'ă', 'ĭ' };


        #region Wikitionary


        /// <summary>
        /// Base url to use the Wikitionary api
        /// </summary>
        internal const string WikitionaryUrlBase = "https://it.wiktionary.org/w/api.php";

        /// <summary>
        /// Common query to retrieve all the informations needed for the library
        /// </summary>
        internal const string WikitionaryCommonQuery = "?action=query&prop=revisions&rvprop=content&format=json";

        /// <summary>
        /// Query to format with the urlEncoded word to search
        /// </summary>
        /// <example>
        /// string.Format(ItalianSyllabaryOptions.WikitionaryFillQuery, --stringToSearch--)
        /// </example>
        internal const string WikitionaryFillQuery = "&titles={0}";


        /// <summary>
        /// string to understand where, according to wikitionary markup language, a new paragraph starts
        /// </summary>
        internal const string WikitionaryStartOfPlaceholder = "{{";


        /// <summary>
        /// String to search to get the sillabation
        /// </summary>
        internal const string WikitionarySillabationString = "{{-sill-}}";


        /// <summary>
        /// Separator used for syllables 
        /// </summary>
        internal const string WikitionarySeparatorString = "|";


        #endregion Wikitionary

    }
}
