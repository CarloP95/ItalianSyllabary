using System.Text.Json.Serialization;

namespace ItalianSyllabary.Models;


/// <summary>
/// Outer response
/// </summary>
/// <param name="Batchcomplete"></param>
/// <param name="warnings"></param>
/// <param name="Query"></param>
internal record WikitionaryResponse
{

    [JsonPropertyName("batchcomplete")]
    public string? Batchcomplete { get; init; }

    [JsonPropertyName("warnings")]
    public object? Warnings { get; init; }

    [JsonInclude]
    [JsonPropertyName("query")]
    public WikitionaryQuery? Query { get; init; }

}


/// <summary>
/// Query when requesting page to Wikitionary
/// </summary>
internal record WikitionaryQuery
{

    [JsonPropertyName("pages")]
    public Dictionary<string, WikitionaryPage> Pages { get; init; } = new Dictionary<string, WikitionaryPage>();

}


/// <summary>
/// Page when requesting page to Wikitionary
/// </summary>
internal record WikitionaryPage
{

    /// <summary>
    /// Page Id
    /// </summary>
    [JsonPropertyName("pageid")]
    public long PageId { get; init; }


    /// <summary>
    /// Namespace
    /// </summary>
    [JsonPropertyName("ns")]

    public int Namespace { get; init; }


    /// <summary>
    /// Requested title
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>
    /// Revisions
    /// </summary>
    [JsonPropertyName("revisions")]
    public IEnumerable<WikitionaryRevision> Revisions { get; init; } = new List<WikitionaryRevision>();

}




/// <summary>
/// Content of the revision of the requested word
/// </summary>
internal record WikitionaryRevision
{

    /// <summary>
    /// Format, will be text/x-wiki
    /// </summary>
    [JsonPropertyName("contentformat")]
    public string? ContentFormat { get; init; }


    /// <summary>
    /// Model, will be Wikitext
    /// </summary>
    [JsonPropertyName("contentmodel")]
    public string? ContentModel { get; init; }


    /// <summary>
    /// Real Content
    /// </summary>
    [JsonPropertyName("*")]
    public string? Content { get; init; }

}