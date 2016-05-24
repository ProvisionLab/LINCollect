using DynamicSurvey.Core.Entities;
using DynamicSurvey.Core.SessionStorage;

namespace DynamicSurvey.Bootstrapper
{
    public static class DbBootstrapper
    {
        public static void BootstrapDatabase()
        {
            CreateLanguages();
        }

        private static void CreateLanguages()
        {
            try
            {
                var session = PersistenceContext.GetCurrentSession();
                using (var transaction = session.BeginTransaction())
                {
                    var englishLanguage = new UserLanguage {Name = "English"};
                    var russianLanguage = new UserLanguage {Name = "Russian"};

                    session.Save(englishLanguage);
                    session.Save(russianLanguage);

                    transaction.Commit();
                }
            }
            finally
            {
                PersistenceContext.DisposeCurrentSession();
            }
        }
    }
}
