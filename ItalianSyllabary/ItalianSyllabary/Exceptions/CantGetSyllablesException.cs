namespace ItalianSyllabary.Exceptions
{
    /// <summary>
    /// Custom exception to be thrown when 
    /// syllables can't be obtained from library
    /// </summary>
    internal class CantGetSyllablesException: Exception
    {
        public CantGetSyllablesException() : base() { }

        public CantGetSyllablesException(string message) : base(message) { }

    }
}
