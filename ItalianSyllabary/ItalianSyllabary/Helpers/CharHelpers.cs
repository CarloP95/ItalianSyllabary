namespace ItalianSyllabary.Helpers
{
    /// <summary>
    /// Class that holds support method to char comparison
    /// </summary>
    internal static class CharHelpers
    {

        public static bool IsTerminationChar(this char c)
        {
            return c.Equals(char.MinValue);
        }

    }
}
