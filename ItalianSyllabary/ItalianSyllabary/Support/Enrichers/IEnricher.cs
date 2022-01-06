namespace ItalianSyllabary.Support
{
    /// <summary>
    /// Interface to abstract the way in which an Enricher 
    /// gets more information about a word and returns those
    /// into the forms of accent (in italian)
    /// </summary>
    internal interface IEnricher
    {


        /// <summary>
        /// Gets the word and finds out more 
        /// information about it (e.g. accents)
        /// </summary>
        /// <param name="word">word to search</param>
        /// <returns>the enriched word</returns>
        Task<string> EnrichWord(string word);


    }
}
