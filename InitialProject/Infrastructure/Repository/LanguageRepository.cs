using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Domen.Model;
using InitialProject.Serializer;
using System.Collections.Generic;

namespace InitialProject.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private const string FilePath = "../../../Infrastructure/Resources/Data/languages.txt";

        private readonly Serializer<Language> _serializer;


        public LanguageRepository()
        {
            _serializer = new Serializer<Language>();
        }


        public List<Language> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

    }
}
