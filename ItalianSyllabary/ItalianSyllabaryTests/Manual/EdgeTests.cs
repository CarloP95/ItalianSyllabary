using ItalianSyllabaryTests.Helpers;
using NUnit.Framework;
using System;

namespace ItalianSyllabaryTests.Manual
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
        }

        [Test(Description = "Test splitting with io, ia edge case")]
        public void IOGroupTest()
        {
            Assert.IsNotNull(_syllabary);
            ArgumentNullException.ThrowIfNull(_syllabary);

            string firstWord = "maria",
                secondWord = "mario";
            string[] firstExpected = { "ma", "ri", "a" },
                secondExpected = { "ma", "rio" };

            TestHelper.SimpleTestProcedure(_syllabary, firstWord, firstExpected, $"Got error in splitting {firstWord}");
            TestHelper.SimpleTestProcedure(_syllabary, secondWord, secondExpected, $"Got error in splitting {secondWord}");
        }

        [Test(Description = "Test splitting with diphtongs, ue edge case")]
        public void DipthongTest()
        {
            Assert.IsNotNull(_syllabary);
            ArgumentNullException.ThrowIfNull(_syllabary);

            string word = "emanuela";
            string[] expected = { "e", "ma", "nue", "la" };

            TestHelper.SimpleTestProcedure(_syllabary, word, expected, $"Got error in splitting {word}");
        }

        [Test(Description = "Test splitting with two consonants case")]
        public void CQGroupTest()
        {
            // tet-to; ac-qua; ri-sciac-quo; cal-ma; ri-cer-ca; rab-do-man-te; im-bu-to; cal-do; in-ge-gne-re; quan-do; am-ni-sti-a; Gian-mar-co; tec-ni-co, a-rit-me-ti-ca.
            Assert.IsNotNull(_syllabary);
            ArgumentNullException.ThrowIfNull(_syllabary);

            string word = "acqua";
            string[] expected = { "ac", "qua" };

            TestHelper.SimpleTestProcedure(_syllabary, word, expected, $"Got error in splitting {word}");
        }


    }
}