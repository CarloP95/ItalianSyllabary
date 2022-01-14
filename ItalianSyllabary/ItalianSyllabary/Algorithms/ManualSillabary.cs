using ItalianSyllabary.Helpers;
using ItalianSyllabary.Support;
using System.Text.RegularExpressions;

namespace ItalianSyllabary.Algorithms
{
    /// <summary>
    /// Execute the sillabation manually.
    /// Could lead to imprecise results due to the 
    /// accent rules in italian.
    /// If accents are given to the word, the result will be precise
    /// </summary>
    internal class ManualSillabary : ISyllabary
    {

        protected const string VOWELS = "AEIOU";
        protected const string CONSONANTS = "BCDFGHJKLMNPQRSTVXYZ";
        protected const string S_GROUP = "S";
        protected const string IO_GROUP = "IO";
        protected const string CQ_GROUP = "CQ";
        protected const string CONSONANT_GROUP_L_R_REGEXP = "([BCDFGPTV] |[LR])+([AEIOU])/i";
        protected readonly string[] NOT_ALLOWED_AT_START = new string[] { "CN", "LM", "RC", "BD", "MB", "MN", "LD", "NG", "ND", "NT", "NM", "TM" };
        protected readonly Regex _regexLRGroup;
        protected readonly IEnricher Enricher;

        private const StringComparison _stringComparisonType = StringComparison.OrdinalIgnoreCase;

        public ManualSillabary()
        {
            _regexLRGroup = new(CONSONANT_GROUP_L_R_REGEXP);
            Enricher = new JsonFileEnricher();
        }

        /**
         * Di seguito sono elencate le regole che vengono seguite per la divisione in sillabe.
         * Ogni regola è numerata perché qualche regola potrebbe essere scomposta in più pezzi di codice
         * 
         * 1. Una vocale iniziale seguita da una sola consonante costituisce una sillaba: e-la-bo-ra-re; a-lian-te; u-mi-do;i-do-lo; o-do-re, u-no.
         * 2. Una consonante semplice forma una sillaba con la vocale seguente: da-do; pe-ra.
         * 3. Non si divide mai un gruppo di consonanti formato da b, c, d, f, g, p, t, v + l oppure r: a-tle-ti-ca; bi-bli-co; bro-do; in-cli-to; cre-de-re; dro-me-da-rio; fle-bi-le; a-fri-ca-no; gla-di-o-lo; gre-co; pe-plo; pre-go; tre-no; a-vrà
         * 4. Non si divide mai un gruppo formato da s + consonante/i: o-stra-ci-smo; te-schio; co-sto-la; sco-iat-to-lo; co-stru-i-re; ca-spi-ta, stri-scio-ne.
         * 5. Si dividono i gruppi costituiti da due consonanti uguali (tt, dd, ecc. e anche cq) e i gruppi consonantici che non sono ammessi ad inizio di parola (del tipo cn, lm, rc, bd, mb, mn, ld, ng, nd, tm): tet-to; ac-qua; ri-sciac-quo; cal-ma; ri-cer-ca; rab-do-man-te; im-bu-to; cal-do; in-ge-gne-re; quan-do; am-ni-sti-a; Gian-mar-co; tec-ni-co, a-rit-me-ti-ca
         * 6. Nei gruppi consonantici formati da tre o più consonanti (rst, ntr, ltr, rtr, btr) si divide prima della seconda consonante, anche in presenza di prefissi come inter-, trans-, iper-, sub-, super-: con-trol-lo, ven-tri-co-lo, al-tro, scal-tro, in-ter-sti-zio, tran-stel-la-re, i-per-tro-fi-co, sub-tro-pi-ca-le, su-per-cri-ti-ci-tà.
         * */
        public Task<string[]> GetSyllables(string word)
        {
            // Enrich word
            string originalWord = word,
                enrichedWord = word.HasAccents() ? word : Enricher.EnrichWord(word).Result;
            word = word.HasAccents() ? word.CleanAccents() : word;

            // First implementation, will put all the logic apart in a second moment
            List<string> syllables = new List<string> { };
            List<int> ignoreIndexes = new List<int>();
            char[] splitted_word = word.ToArray();

            foreach ((char currentChar, int currentCharIdx) in splitted_word.Select((c, i) => (c, i)))
            {
                if (ignoreIndexes.Contains(currentCharIdx))
                {
                    continue;
                }

                char nextChar = splitted_word.ElementAtOrDefault(currentCharIdx + 1);
                char prevChar = splitted_word.ElementAtOrDefault(currentCharIdx - 1);

                char prevEnrichedChar = enrichedWord.ElementAtOrDefault(currentCharIdx - 1);
                char nextEnrichedChar = enrichedWord.ElementAtOrDefault(currentCharIdx + 1);
                char currentEnrichedChar = enrichedWord.ElementAtOrDefault(currentCharIdx);

                // dittongo controllo a posteriori (mario)
                string prevAndCur = $"{prevEnrichedChar}{currentEnrichedChar}".ToUpper();
                if (prevAndCur.HasDiphthong())
                {
                    syllables = syllables.Select((str, idx) => idx == syllables.Count() - 1 ? $"{str}{currentChar}" : str)
                        .ToList();
                    ignoreIndexes.Add(currentCharIdx);
                    continue;
                }

                // Empiric rule
                if (nextChar.IsTerminationChar())
                {
                    if (VOWELS.Contains(currentChar, _stringComparisonType))
                    {
                        syllables.Add(
                            currentChar.ToString()
                        );
                    }
                    if (CONSONANTS.Contains(currentChar, _stringComparisonType))
                    {
                        syllables = syllables.Select((str, idx) => idx == syllables.Count() - 1 ? $"{str}{currentChar}" : str)
                                                .ToList();
                    }
                    ignoreIndexes.Add(currentCharIdx);
                    continue; // will break immediately
                }

                // dittongo controllo a priori (auguri)
                string curAndNext = $"{currentEnrichedChar}{nextEnrichedChar}".ToUpper();
                if (curAndNext.HasDiphthong())
                {
                    syllables.Add(
                        curAndNext
                    );
                    ignoreIndexes.AddRange(Enumerable.Range(currentCharIdx, curAndNext.Length));
                    continue;
                }

                // Clean accents


                // 1.Una vocale iniziale seguita da una sola consonante costituisce una sillaba: e-la-bo-ra-re; a-lian-te; u-mi-do;i-do-lo; o-do-re, u-no.
                if (VOWELS.Contains(currentChar, _stringComparisonType)
                    && CONSONANTS.Contains(splitted_word.ElementAtOrDefault(currentCharIdx + 1), _stringComparisonType))
                {
                    syllables.Add(
                        currentChar.ToString()
                    );
                    ignoreIndexes.Add(currentCharIdx);
                    continue;
                }

                // 2.Una consonante semplice forma una sillaba con la vocale seguente: da-do; pe-ra.
                if (CONSONANTS.Contains(currentChar, _stringComparisonType)
                    && VOWELS.Contains(nextChar, _stringComparisonType))

                {
                    int lastIdxIgnored = ignoreIndexes.Any() ? ignoreIndexes.Max() : -1;
                    if (currentCharIdx > 0
                        && currentCharIdx != lastIdxIgnored)
                    {
                        syllables.Add(
                            word.Substring(lastIdxIgnored + 1, 2 + (currentCharIdx - lastIdxIgnored - 1))
                        );
                        ignoreIndexes.AddRange(Enumerable.Range(lastIdxIgnored + 1, (currentCharIdx - lastIdxIgnored) + 1));
                        continue;
                    }

                    syllables.Add(
                       word.Substring(currentCharIdx, 2)
                    );

                    ignoreIndexes.AddRange(new List<int> { currentCharIdx, currentCharIdx + 1 });
                    continue;
                }

                // 3. Non si divide mai un gruppo di consonanti formato da b, c, d, f, g, p, t, v + l oppure r: a-tle-ti-ca; bi-bli-co; bro-do; in-cli-to; cre-de-re; dro-me-da-rio; fle-bi-le; a-fri-ca-no; gla-di-o-lo; gre-co; pe-plo; pre-go; tre-no; a-vrà
                char twoCharsAhead = splitted_word.ElementAtOrDefault(currentCharIdx + 2);
                string composeChars = $"{currentChar}{nextChar}{twoCharsAhead}";
                var lr_match = _regexLRGroup.Match(composeChars);
                if (lr_match.Success)
                {
                    syllables.Add(
                         word.Substring(currentCharIdx, composeChars.Length)
                    );
                    ignoreIndexes.AddRange(Enumerable.Range(currentCharIdx, currentCharIdx + composeChars.Length));
                }

                // 4.Non si divide mai un gruppo formato da s + consonante/i: o-stra-ci-smo; te-schio; co-sto-la; sco-iat-to-lo; co-stru-i-re; ca-spi-ta, stri-scio-ne
                if (S_GROUP.Contains(currentChar, _stringComparisonType))
                {
                    string s = word.Substring(currentCharIdx);
                    char endOfSyllableChar = s.Select((c, i) => (c, i))
                        .FirstOrDefault(nc =>
                            VOWELS.Contains(nc.c, _stringComparisonType)
                            && !IO_GROUP.Contains($"{nc.c}{s.ElementAtOrDefault(nc.i + 1)}", _stringComparisonType) // te-schio, stri-scio-ne
                        )
                        .c;

                    int syllableLength = s.IndexOf(endOfSyllableChar) + 1;

                    syllables.Add(
                        word.Substring(currentCharIdx, syllableLength)
                    );
                    ignoreIndexes.AddRange(Enumerable.Range(currentCharIdx, syllableLength));
                    continue;
                }

                // 5.Si dividono i gruppi costituiti da due consonanti uguali (tt, dd, ecc. e anche cq) e i gruppi consonantici che non sono ammessi ad inizio di parola (del tipo cn, lm, rc, bd, mb, mn, ld, ng, nd, tm): tet-to; ac-qua; ri-sciac-quo; cal-ma; ri-cer-ca; rab-do-man-te; im-bu-to; cal-do; in-ge-gne-re; quan-do; am-ni-sti-a; Gian-mar-co; tec-ni-co, a-rit-me-ti-ca.
                // string curAndNext = $"{currentChar}{nextChar}".ToUpper();
                if ((CONSONANTS.Contains(currentChar, _stringComparisonType)
                        && nextChar.Equals(currentChar))
                    || CQ_GROUP.Contains(curAndNext, _stringComparisonType)
                    || NOT_ALLOWED_AT_START.Contains(curAndNext)
                    )
                {
                    syllables = syllables.Select((str, idx) => idx == syllables.Count() - 1 ? $"{str}{currentChar}" : str)
                        .ToList();
                    ignoreIndexes.Add(currentCharIdx);
                    continue;
                }

                // 6.Nei gruppi consonantici formati da tre o più consonanti (rst, ntr, ltr, rtr, btr) si divide prima della seconda consonante, anche in presenza di prefissi come inter-, trans-, iper-, sub-, super-: con-trol-lo, ven-tri-co-lo, al-tro, scal-tro, in-ter-sti-zio, tran-stel-la-re, i-per-tro-fi-co, sub-tro-pi-ca-le, su-per-cri-ti-ci-tà.
                if (CONSONANTS.Contains(currentChar, _stringComparisonType)
                    && CONSONANTS.Contains(nextChar, _stringComparisonType)
                    && CONSONANTS.Contains(twoCharsAhead, _stringComparisonType))
                {
                    syllables = syllables.Select((str, idx) => idx == syllables.Count() - 1 ? $"{str}{currentChar}" : str)
                        .ToList();
                    ignoreIndexes.Add(currentCharIdx);
                    continue;
                }

            }

            // Clean
            IEnumerable<int> lenghts = syllables.Select(s => s.Length);

            string[] originalSyllables = new string[] { };
            foreach((int l, int idx) in lenghts.Select((l, idx) => (l,idx)))
            {
                int reduced = lenghts.Select((l, innerIdx) => innerIdx < idx ? l : 0)
                    .Sum();
                originalSyllables = originalSyllables.Append(originalWord.Substring(idx == 0 ? 0 : reduced, l))
                    .ToArray();
            }

            return Task.FromResult(originalSyllables.ToArray());
        }


        public Task<int> GetSyllablesCount(string word)
        {
            return Task.FromResult(GetSyllables(word).Result.Length);
        }

    }
}
