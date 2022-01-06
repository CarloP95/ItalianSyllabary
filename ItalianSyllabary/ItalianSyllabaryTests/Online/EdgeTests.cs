using ItalianSyllabaryTests.Helpers;
using NUnit.Framework;

namespace ItalianSyllabaryTests.Online
{

    /// <summary>
    /// Edge tests to verify the correct syllables
    /// </summary>
    [TestFixture]
    public class EdgeTests
    {
        private ItalianSyllabary.ItalianSyllabary? _syllabary;

        [SetUp]
        public void Setup()
        {
            _syllabary = new ItalianSyllabary.ItalianSyllabary();
            ItalianSyllabary.Support.ItalianSyllabaryOptions.UseOnlineDictionary = true;
        }

        [Test(Description = "Test splitting with io, ia edge case")]
        public void IOGroupTest()
        {
            string firstWord = "maria",
                secondWord = "mario";
            string[] firstExpected = { "ma", "ri", "a" },
                secondExpected = { "ma", "rio" };

            TestHelper.SimpleTestProcedure(_syllabary, firstWord, firstExpected, $"Got error in splitting {firstWord}");
            TestHelper.SimpleTestProcedure(_syllabary, secondWord, secondExpected, $"Got error in splitting {secondWord}");
        }


    }
}