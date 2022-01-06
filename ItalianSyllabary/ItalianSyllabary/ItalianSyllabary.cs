using ItalianSyllabary.Algorithms;
using ItalianSyllabary.Exceptions;

namespace ItalianSyllabary
{
    /// <summary>
    /// Permette di scomporre in sillabe qualsiasi parola che viene fornita in input.
    /// Si può configurare la libreria per funzionare seguendo le regole che sono dettate 
    /// dall'accademia della Crusca con la limitazione circa gli accenti (cfr. màrio, marìa)
    /// oppure utilizzando la versione online grazie a Wikitionary.
    /// Nel caso in cui la parola non sia trovata o non sia presente la scomposizione in sillabe
    /// da Wikitionary verrà comunque utilizzata la versione Manuale che segue le regole presenti 
    /// al link sottostante.
    /// Non garantisce risultati per parole non appartenenti all'alfabeto italiano.
    /// <see cref="https://accademiadellacrusca.it/it/consulenza/divisione-in-sillabe/302"/>
    /// </summary>
    public class ItalianSyllabary : ISyllabary
    {

        private IEnumerable<ISyllabary> _sillabarium;

        public ItalianSyllabary()
        {
            _sillabarium = new List<ISyllabary> 
            {
                new OnlineDictionarySillabary(),
                new ManualSillabary()
            };
        }

        public Task<string[]> GetSyllables(string word)
        {
            var sillabarium = GetSyllabariumOrdered();

            ISyllabary syllabary = sillabarium.First();

            try
            {
                return syllabary.GetSyllables(word);
            }
            catch (CantGetSyllablesException)
            {
                syllabary = sillabarium.Where(s => s is ManualSillabary).First();
                return syllabary.GetSyllables(word);
            }
        }


        public Task<int> GetSyllablesCount(string word)
        {
            var sillabarium = GetSyllabariumOrdered();

            ISyllabary syllabary = sillabarium.First();

            try
            {
                return syllabary.GetSyllablesCount(word);
            }
            catch (CantGetSyllablesException)
            {
                syllabary = sillabarium.Where(s => s is ManualSillabary).First();
                return syllabary.GetSyllablesCount(word);
            }
        }


        private IEnumerable<ISyllabary> GetSyllabariumOrdered()
        {
            if (Support.ItalianSyllabaryOptions.UseOnlineDictionary)
            {
                return _sillabarium.OrderByDescending(s => s is OnlineDictionarySillabary);
            }
            else
            {
                return _sillabarium.OrderByDescending(s => s is ManualSillabary);
            }
        }

    }
}