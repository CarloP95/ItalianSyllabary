using System.Text.Json.Serialization;

namespace ItalianSyllabary.Models.Enrichers
{

    /// <summary>
    /// Model to represent the default enrich file
    /// </summary>
    internal record JsonFile
    {

        [JsonPropertyName("words")]
        public Dictionary<string, string> Words { get; init; } = new Dictionary<string, string> { };

    }

}
