namespace InitialProject.Aplication.Contracts.Repository
{
    using InitialProject.Domen.Model;
    using System.Collections.Generic;

    public interface ILanguageRepository
    {
        public List<Language> GetAll();
    }
}
