using ItalianSyllabary.Algorithms;
using ItalianSyllabary.Exceptions;
using ItalianSyllabary.Helpers;

namespace ItalianSyllabary
{
    /// <summary>
    /// Class that allows to decompose word in syllables.
    /// The library can be configured to use rules that are specified by the Accademia della Crusca
    /// with a limitation about accents (e.g. màrio, marìa)
    /// or can be configured to use the online version thanks to Online Dictionaries.
    /// The only Dictionary implemented for now is Wikitionary.
    /// When the online version of the word can't be found, it will fallback
    /// doing manual decomposition of syllables.
    /// It does not guarantee results for not italian words.
    /// The input must be a word per time
    /// <see cref="https://accademiadellacrusca.it/it/consulenza/divisione-in-sillabe/302"/>
    /// </summary>
    public class ItalianSyllabary : ISyllabary
    {

        private IEnumerable<ISyllabary> _sillabarium;

        /// <summary>
        /// Create an ItalianSyllabary instance preferring 
        /// manual decomposition in syllables
        /// </summary>
        public ItalianSyllabary()
        {
            _sillabarium = GetSyllabarium();
        }

        /// <summary>
        /// Create an ItalianSyllabary instance specifiying 
        /// if the preferred way to get word's syllables is online (with Wikitionary)
        /// </summary>
        /// <param name="preferOnline">if true, first the word's syllables will be checked online, then will fallback to manual syllabary</param>
        public ItalianSyllabary(bool preferOnline) : this()
        {
            Support.ItalianSyllabaryOptions.UseOnlineDictionary = preferOnline;
        }

        /// <summary>
        /// Get Syllables that forms the word
        /// </summary>
        /// <param name="word">the word to decompose, just a single word, not a sentence ("casa", "Marco", "Carne")</param>
        /// <returns>a string array with all syllables</returns>
        /// <see cref="ISyllabary"/>
        /// <exception cref="CantGetSyllablesException">When syllables can't be obtained</exception>
        public async Task<string[]> GetSyllables(string word)
        {
            CheckArgs(word);

            string mName = nameof(ISyllabary.GetSyllables);

            var res = await (
                RunAsync<string[]>(
                    mName,
                    word)
                    ?? Task.FromResult(new string[] { })
                );

            return res;
        }

        /// <summary>
        /// Get Syllables count into a word
        /// </summary>
        /// <param name="word">the word to decompose, just a single word, not a sentence ("casa", "Marco", "Carne")</param><
        /// <returns>count of all syllables</returns>
        /// <see cref="ISyllabary"/>
        /// <exception cref="CantGetSyllablesException">When syllables can't be obtained</exception>
        public async Task<int> GetSyllablesCount(string word)
        {
            CheckArgs(word);

            string mName = nameof(ISyllabary.GetSyllablesCount);

            var res = await (
                RunAsync<string>(
                    mName,
                    word)
                    ?? Task.FromResult(string.Empty)
                );

            return Convert.ToInt32(res);
        }

        protected bool CheckArgs(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException(nameof(word));
            }

            if (word.HasMoreThanAWord())
            {
                throw new ArgumentException("Can't decompose a sentence. Must be only a word as stated in the docs.", nameof(word));
            }

            return true;
        }

        protected async Task<T> RunAsync<T>(string methodName, string word)
        {
            var sillabarium = GetSyllabariumOrdered();
            ISyllabary syllabary = sillabarium.First();

            try
            {
                return methodName switch
                {
                    nameof(ISyllabary.GetSyllables) => (T)(await syllabary.GetSyllables(word) as object),
                    nameof(ISyllabary.GetSyllablesCount) => (T)(await syllabary.GetSyllablesCount(word) as object),
                    _ => throw new NotImplementedException($"{methodName} is not implemented")
                };
            }
            catch (CantGetSyllablesException ex)
            {
                // Already tried in best effort mode
                if (syllabary is ManualSillabary)
                {
                    throw ex;
                }

                // try in best effort mode
                syllabary = sillabarium.Where(s => s is ManualSillabary).First();

                return methodName switch
                {
                    nameof(ISyllabary.GetSyllables) => (T)Convert.ChangeType(await syllabary.GetSyllables(word), typeof(T)),
                    nameof(ISyllabary.GetSyllablesCount) => (T)Convert.ChangeType(await syllabary.GetSyllablesCount(word), typeof(T)),
                    _ => throw new NotImplementedException($"{methodName} is not implemented")
                };
            }

        }


        private IEnumerable<ISyllabary> GetSyllabariumOrdered()
        {
            if (Support.ItalianSyllabaryOptions.UseOnlineDictionary)
            {
                return _sillabarium.OrderByDescending(s => s is OnlineDictionarySillabary);
            }
            else
            {
                return _sillabarium.OrderByDescending(s => s is ManualSillabary);
            }
        }

        private IEnumerable<ISyllabary> GetSyllabarium()
        {
            return new List<ISyllabary>
            {
                new OnlineDictionarySillabary(),
                new ManualSillabary()
            };
        }

    }
}