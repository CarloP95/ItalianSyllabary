using NUnit.Framework;

namespace ItalianSyllabaryTests.Manual
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
        }

    }
}