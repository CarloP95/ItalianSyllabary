using NUnit.Framework;

namespace ItalianSyllabaryTests.Helpers
{
    internal static class TestHelper
    {

        /// <summary>
        /// Method used to assert simple cases
        /// </summary>
        /// <param name="_syllabary">_syllabary instance</param>
        /// <param name="word">word to be tested with _syllabary</param>
        /// <param name="expected">string array that should be the result</param>
        /// <param name="errorMessage">message to print in case of error</param>
        public static void SimpleTestProcedure(ItalianSyllabary.ItalianSyllabary _syllabary, string word, string[] expected, string errorMessage)
        {
            if (_syllabary is null)
            {
                Assert.Fail("Not instantiated");
            }

            var result = _syllabary.GetSyllables(word)
                    .Result;

            Assert.That(
                    result,
                    Is.EquivalentTo(expected),
                    errorMessage
                );
        }

    }
}
