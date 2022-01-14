using ItalianSyllabaryTests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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


        [Test(Description = "Test splitting with two consonants case")]
        public void CQGroupTest()
        {
            // ri-sciac-quo; cal-ma; ri-cer-ca; rab-do-man-te; im-bu-to; cal-do; in-ge-gne-re; quan-do; am-ni-sti-a; Gian-mar-co; tec-ni-co, a-rit-me-ti-ca.
            Assert.IsNotNull(_syllabary);
            ArgumentNullException.ThrowIfNull(_syllabary);

            Dictionary<string, string[]> testValues = new Dictionary<string, string[]>
            {
                { "risciacquo", new string[] {"ri", "sciac", "quo" } },
                { "calma", new string[] {"cal", "ma" } },
                { "ricerca", new string[] {"ri", "cer", "ca" } },
                { "rabdomante", new string[] {"rab", "do", "man", "te" } },
                { "imbuto", new string[] {"im", "bu", "to" } },
                { "caldo", new string[] {"cal", "do" } },
                { "ingegnere", new string[] {"in", "ge", "gne", "re" } },
                { "quando", new string[] {"quan", "do" } },
                { "amnistia", new string[] {"am", "ni", "sti", "a" } },
                { "gianmarco", new string[] {"gian", "mar", "co" } },
                { "tecnico", new string[] {"tec", "ni", "co" } },
                { "aritmetica", new string[] {"a", "rit", "me", "ti", "ca" } },
            };

            foreach (var (word, expected) in testValues)
            {
                TestHelper.SimpleTestProcedure(_syllabary, word, expected, $"Got error in splitting {word}");
            }
        }

        [Test(Description = "Test splitting words with simple vowel")]
        public void ShouldSplitSimpleVowel()
        {
            // e-la-bo-ra-re; a-lian-te; u-mi-do;i-do-lo; o-do-re, u-no
            Assert.IsNotNull(_syllabary);
            ArgumentNullException.ThrowIfNull(_syllabary);

            Dictionary<string, string[]> testValues = new Dictionary<string, string[]>
            {
                { "elaborare", new string[] {"e", "la", "bo", "ra", "re" } },
                { "aliante", new string[] {"a", "lian", "te" } },
                { "umido", new string[] {"u", "mi", "do" } },
                { "idolo", new string[] {"i", "do", "lo" } },
                { "odore", new string[] {"o", "do", "re" } },
                { "uno", new string[] {"u", "no" } },
            };

            foreach (var (word, expected) in testValues)
            {
                TestHelper.SimpleTestProcedure(_syllabary, word, expected, $"Got error in splitting {word}");
            }
        }

        [Test(Description = "Test splitting words with simple word")]
        public void ShouldSplitSGroup()
        {
            // o-stra-ci-smo; co-sto-la; sco-iat-to-lo; co-stru-i-re; ca-spi-ta, stri-scio-ne
            Assert.IsNotNull(_syllabary);
            ArgumentNullException.ThrowIfNull(_syllabary);

            Dictionary<string, string[]> testValues = new Dictionary<string, string[]>
            {
                { "ostracismo", new string[] {"o", "stra", "ci", "smo" } },
                { "costola", new string[] {"co", "sto", "la" } },
                { "scoiattolo", new string[] {"sco", "iat", "to", "lo" } },
                { "costruire", new string[] {"co", "stru", "i", "re" } },
                { "caspita", new string[] {"ca", "spi", "ta" } },
                { "striscione", new string[] {"stri", "scio", "ne" } },
            };

            foreach (var (word, expected) in testValues)
            {
                TestHelper.SimpleTestProcedure(_syllabary, word, expected, $"Got error in splitting {word}");
            }
        }

        [Test(Description = "Test splitting words with two consonant group")]
        public void ShouldSplitConsonantGroup()
        {
            // a-tle-ti-ca; bi-bli-co; bro-do; in-cli-to; cre-de-re; dro-me-da-rio; fle-bi-le; a-fri-ca-no; gla-di-o-lo; gre-co; pe-plo; pre-go; tre-no; a-vrà
            Assert.IsNotNull(_syllabary);
            ArgumentNullException.ThrowIfNull(_syllabary);

            Dictionary<string, string[]> testValues = new Dictionary<string, string[]>
            {
                { "atletica", new string[] {"a", "tle", "ti", "ca" } },
                { "biblico", new string[] {"bi", "bli", "co" } },
                { "brodo", new string[] {"bro", "do" } },
                { "inclito", new string[] {"in", "cli", "to" } },
                { "credere", new string[] {"cre", "de", "re" } },
                { "dromedario", new string[] {"dro", "me", "da", "rio" } },
                { "flebile", new string[] {"fle", "bi", "le" } },
                { "africano", new string[] {"a", "fri", "ca", "no" } },
                { "gladiolo", new string[] {"gla", "di", "o" ,"lo" } },
                { "greco", new string[] {"gre", "co" } },
                { "peplo", new string[] {"pe", "plo" } },
                { "prego", new string[] {"pre", "go" } },
                { "treno", new string[] {"tre", "no" } },
                { "avrà", new string[] {"a", "vrà" } },
            };

            foreach (var (word, expected) in testValues)
            {
                TestHelper.SimpleTestProcedure(_syllabary, word, expected, $"Got error in splitting {word}");
            }
        }

        [Test(Description = "Test splitting words with three consonant group")]
        public void ShouldSplitThreeConsonantGroup()
        {
            // con-trol-lo, ven-tri-co-lo, al-tro, scal-tro, in-ter-sti-zio, tran-stel-la-re, i-per-tro-fi-co, sub-tro-pi-ca-le, su-per-cri-ti-ci-tà
            Assert.IsNotNull(_syllabary);
            ArgumentNullException.ThrowIfNull(_syllabary);

            Dictionary<string, string[]> testValues = new Dictionary<string, string[]>
            {
                { "controllo", new string[] {"con", "trol", "lo" } },
                { "ventricolo", new string[] {"ven", "tri", "co", "lo" } },
                { "altro", new string[] {"al", "tro" } },
                { "scaltro", new string[] {"scal", "tro" } },
                { "interstizio", new string[] {"in", "ter", "sti", "zio" } },
                { "transtellare", new string[] {"tran", "stel", "la", "re" } },
                { "ipertrofico", new string[] {"i", "per", "tro", "fi", "co" } },
                { "subtropicale", new string[] {"sub", "tro", "pi", "ca", "le" } },
                { "supercriticità", new string[] {"su", "per", "cri" ,"ti", "ci", "tà" } },
            };

            foreach (var (word, expected) in testValues)
            {
                TestHelper.SimpleTestProcedure(_syllabary, word, expected, $"Got error in splitting {word}");
            }
        }
    }
}