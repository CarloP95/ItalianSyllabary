namespace ItalianSyllabary
{
    /// <summary>
    /// Interface to implement to satisfy a Sillabary
    /// </summary>
    internal interface ISyllabary
    {

        /// <summary>
        /// Get Syllables that forms the word
        /// </summary>
        /// <param name="word">the word to decompose</param>
        /// <returns>a string array with all syllables</returns>
        /// <exception cref="Exceptions.CantGetSyllablesException">When syllables can't be obtained</exception>
        Task<string[]> GetSyllables(string word);


        /// <summary>
        /// Get Syllables count into a word
        /// </summary>
        /// <param name="word">the word to decompose</param>
        /// <returns>count of all syllables</returns>
        /// <exception cref="Exceptions.CantGetSyllablesException">When syllables can't be obtained</exception>
        Task<int> GetSyllablesCount(string word);

    }
}
