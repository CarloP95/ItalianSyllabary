using ItalianSyllabary.Models.Enrichers;
using System.Reflection;

namespace ItalianSyllabary.Support
{


    /// <summary>
    /// Default implementation for an enricher with a file embedded into the library
    /// </summary>
    internal class JsonFileEnricher : IEnricher
    {

        private static readonly string ResourceName = "ItalianSyllabary.Resources.enrich.json";
        private readonly JsonFile? _jsonFile;

        /// <summary>
        /// Gets the file into the constructor
        /// </summary>
        public JsonFileEnricher()
        {
            Assembly? assembly = Assembly.GetExecutingAssembly();
            if (assembly == null)
            {
                return;
            }


            using Stream? stream = assembly.GetManifestResourceStream(ResourceName);
            if (stream == null)
            {
                return;
            }

            using StreamReader? reader = new StreamReader(stream, System.Text.Encoding.Latin1);
            if (reader == null)
            {
                return;
            }

            string content = reader.ReadToEnd();
            _jsonFile = System.Text.Json.JsonSerializer.Deserialize<JsonFile>(content);
        }

        /// <summary>
        /// Tries to retrieve accents of word from file embedded.
        /// If not found, it returns the same word
        /// </summary>
        /// <param name="word">the word to enrich with accents</param>
        /// <returns>the word enriched if found, the same input word if not found</returns>
        public Task<string> EnrichWord(string word)
        {
            ArgumentNullException.ThrowIfNull(word, nameof(word));

            if (_jsonFile == null)
            {
                return Task.FromResult(word);
            }

            return Task.FromResult(_jsonFile.Words.GetValueOrDefault(word) ?? word);
        }
    }
}
