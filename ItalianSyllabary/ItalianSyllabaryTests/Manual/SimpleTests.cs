using ItalianSyllabaryTests.Helpers;
using NUnit.Framework;
using System;

namespace ItalianSyllabaryTests.Manual
{

    /// <summary>
    /// Simple tests to verify the correct syllables
    /// </summary>
    [TestFixture]
    public class SimpleTests
    {
        private ItalianSyllabary.ItalianSyllabary? _syllabary;

        [SetUp]
        public void Setup()
        {
            _syllabary = new ItalianSyllabary.ItalianSyllabary();
        }

        [Test(Description = "Test with no particular cases")]
        public void SimpleTest()
        {
            Assert.IsNotNull(_syllabary);
            ArgumentNullException.ThrowIfNull(_syllabary);

            const string word = "casa";
            string[] expected = { "ca", "sa" };

            TestHelper.SimpleTestProcedure(_syllabary, word, expected, @$"Got error in splitting word ""{word}"". This library is really messed up...");
        }

        [Test(Description = "Test if the library should ignore upper case")]
        public void ShouldIgnoreUpperCase()
        {
            Assert.IsNotNull(_syllabary);
            ArgumentNullException.ThrowIfNull(_syllabary);

            const string word = "CASA";
            string[] expected = { "CA", "SA" };

            TestHelper.SimpleTestProcedure(_syllabary, word, expected, @$"Got error in splitting word ""{word}"". This library is really messed up...");
        }

        [Test(Description = "Test with accents")]
        public void SimpleAccentTest()
        {
            Assert.IsNotNull(_syllabary);
            ArgumentNullException.ThrowIfNull(_syllabary);

            const string word = "c�sa";
            string[] expected = { "c�", "sa" };

            TestHelper.SimpleTestProcedure(_syllabary, word, expected, @$"Got error in splitting word ""{word}"". This library is really messed up...");
        }

        [Test(Description = "Test with long word")]
        public void LongWordTest()
        {
            Assert.IsNotNull(_syllabary);
            ArgumentNullException.ThrowIfNull(_syllabary);

            const string word = "casanova";
            string[] expected = { "ca", "sa", "no", "va" };

            TestHelper.SimpleTestProcedure(_syllabary, word, expected, @$"Got error in splitting word ""{word}""");
        }

        [Test(Description = "Test with lonely vocal")]
        public void LonelyVocalTest()
        {
            Assert.IsNotNull(_syllabary);
            ArgumentNullException.ThrowIfNull(_syllabary);

            const string word = "amore";
            string[] expected = { "a", "mo", "re" };

            TestHelper.SimpleTestProcedure(_syllabary, word, expected, @$"Got error in splitting word ""{word}""");
        }

        [Test(Description = "Test with S group")]
        public void SimpleSGroupTest()
        {
            Assert.IsNotNull(_syllabary);
            ArgumentNullException.ThrowIfNull(_syllabary);

            const string word = "teschio";
            string[] expected = { "te", "schio" };

            TestHelper.SimpleTestProcedure(_syllabary, word, expected, @$"Got error in splitting word ""{word}"". S group may have problems");
        }


        [Test(Description = "Test with double consonant group")]
        public void SimpleDoubleConsonantGroupTest()
        {
            Assert.IsNotNull(_syllabary);
            ArgumentNullException.ThrowIfNull(_syllabary);

            const string word = "tetto";
            string[] expected = { "tet", "to" };

            TestHelper.SimpleTestProcedure(_syllabary, word, expected, @$"Got error in splitting word ""{word}"". Double consonant group may have problems");
        }

        [Test(Description = "Test with triple consonant group")]
        public void SimpleTripleConsonantGroupTest()
        {
            Assert.IsNotNull(_syllabary);
            ArgumentNullException.ThrowIfNull(_syllabary);

            const string word = "ventricolo";
            string[] expected = { "ven", "tri", "co", "lo" };

            TestHelper.SimpleTestProcedure(_syllabary, word, expected, @$"Got error in splitting word ""{word}"". Triple consonant group may have problems");
        }

    }
}