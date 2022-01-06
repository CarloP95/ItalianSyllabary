using NUnit.Framework;

namespace ItalianSyllabaryTests.Online
{

    /// <summary>
    /// Massive tests to verify the correct syllables
    /// </summary>
    [TestFixture]
    public class MassiveTests
    {
        private ItalianSyllabary.ItalianSyllabary? _syllabary;

        [SetUp]
        public void Setup()
        {
            _syllabary = new ItalianSyllabary.ItalianSyllabary();
            ItalianSyllabary.Support.ItalianSyllabaryOptions.UseOnlineDictionary = true;
        }

    }
}