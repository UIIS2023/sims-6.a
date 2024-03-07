namespace InitialProject.Services
{
    using InitialProject.Aplication.Contracts.Repository;
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Services.IServices;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _repository;

        public LanguageService()
        {
            _repository = Injector.CreateInstance<ILanguageRepository>();
        }
        public List<Language> GetAll()
        {
            return _repository.GetAll();
        }

        public List<string> GetAllToString()
        {
            List<string> listOfLanguages = new List<string>();
            foreach(var language in _repository.GetAll())
            {
                listOfLanguages.Add(language.Name);
            }
            return listOfLanguages;
        }
    }
}
