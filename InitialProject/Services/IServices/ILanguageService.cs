namespace InitialProject.Services.IServices
{
    using InitialProject.Domen.Model;
    using System.Collections.Generic;

    public interface ILanguageService
    {
        public List<Language> GetAll();

        List<string> GetAllToString();
    }
}
