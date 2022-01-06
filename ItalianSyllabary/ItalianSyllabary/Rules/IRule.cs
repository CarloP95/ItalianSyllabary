namespace ItalianSyllabary.Rules
{
    /// <summary>
    /// Interface to implement to apply a rule to the flow
    /// </summary>
    internal interface IRule
    {

        /// <summary>
        /// Tells if the rule can be applied
        /// </summary>
        /// <param name="word">word to decompose</param>
        /// <returns>true or false</returns>
        bool CanApply(string word);

        /// <summary>
        /// Apply the rule
        /// </summary>
        /// <param name="word">word to decompose</param>
        /// <returns>a string array with the splitted word for the rule</returns>
        string[] Apply(string word);

    }
}
