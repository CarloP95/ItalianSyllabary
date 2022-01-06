using ItalianSyllabary.Exceptions;
using ItalianSyllabary.Helpers;
using ItalianSyllabary.Models;
using System.Text.Json;

namespace ItalianSyllabary.Algorithms
{
    /// <summary>
    /// Tries to fetch the syllables from an online service.
    /// This can be configured into the options in Support.ItalianSyllabaryOptions
    /// </summary>
    internal class OnlineDictionarySillabary : ISyllabary
    {

        private readonly HttpClient _httpClient;

        public OnlineDictionarySillabary()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Get syllables from online service
        /// </summary>
        /// <exception cref="HttpRequestException">When the request has not success status code</exception>
        /// <param name="word"></param>
        /// <returns></returns>
        public async Task<string[]> GetSyllables(string word)
        {
            string requestUri = BuildRequestUri(word);

            string content = string.Empty;
            try
            {
                var response = await _httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                content = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new CantGetSyllablesException(ex.Message);
            }

            return GetSyllablesFromContent(word, content);
        }

        public Task<int> GetSyllablesCount(string word)
        {
            throw new NotImplementedException();
        }


        private string BuildRequestUri(string word)
        {
            string wordEncoded = System.Web.HttpUtility.UrlEncode(word),
            query = string.Concat(
                Constants.Common.WikitionaryCommonQuery,
                string.Format(
                    Constants.Common.WikitionaryFillQuery,
                    wordEncoded)
            ),
            requestUri = string.Concat(
                Constants.Common.WikitionaryUrlBase,
                query
            );

            return requestUri;
        }

        private string[] GetSyllablesFromContent(string word, string content)
        {
            WikitionaryResponse? parsedContent = JsonSerializer.Deserialize<WikitionaryResponse>(content);

            if (parsedContent == null)
            {
                throw new CantGetSyllablesException("Content is empty");
            }

            var query = parsedContent.Query ?? new WikitionaryQuery();
            WikitionaryRevision revision;
            try
            {
                revision = query.Pages.First().Value.Revisions.First();
            }
            catch (Exception)
            {
                throw new CantGetSyllablesException($"{Constants.Common.PageNotFound} while searching for {word}");
            }

            string rawContent = revision.Content ?? string.Empty;
            if (!rawContent.Contains(Constants.Common.WikitionarySillabationString, StringComparison.OrdinalIgnoreCase))
            {
                throw new CantGetSyllablesException($"{Constants.Common.PageNotFound} while searching for {word}");
            }

            int idxOfStartSyllables = rawContent.IndexOf(Constants.Common.WikitionarySillabationString) + Constants.Common.WikitionarySillabationString.Length,
                idxOfEndSyllables = rawContent.Substring(idxOfStartSyllables)
                                        .IndexOf(Constants.Common.WikitionaryStartOfPlaceholder)
                                        + idxOfStartSyllables;

            string focusRawContent = rawContent.Substring(
                idxOfStartSyllables,
                idxOfEndSyllables - idxOfStartSyllables
            );

            string rawSyllables = System.Web.HttpUtility.HtmlDecode(focusRawContent)
                .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Where(s => s.Contains(Constants.Common.WikitionarySeparatorString))
                .FirstOrDefault() ?? string.Empty;

            // Clean
            string[] syllables = rawSyllables
                .Replace("\n", string.Empty)
                .Replace(";", string.Empty)
                .Trim()
                .Replace(" ", string.Empty)
                .CleanAccents()
                .Split(Constants.Common.WikitionarySeparatorString);

            // Check errors
            if (syllables.Any(s => Constants.Common.NotAllowedLetters.Intersect(s.ToCharArray()).Any()))
            {
                throw new CantGetSyllablesException($"{word} in Wikitionary has a definition, but not for italian. Sorry");
            }

            return syllables;
        }

    }
}
