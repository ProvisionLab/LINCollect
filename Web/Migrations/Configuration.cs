namespace Web.Migrations
{
    using Data;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Web.Models.ApplicationDbContext context)
        {
            //context.Users.AddOrUpdate(u => u.Id,
            //    new ApplicationUser()
            //    {
            //        Id = "ac3d340a-bd03-4fac-84f9-92c74c283e6a",
            //        Email = "korobeinykovan@gmail.com",
            //        EmailConfirmed = true,
            //        PasswordHash = "AK2DCy1bBkOwXjTb9CxzX6oYfoOLjiI1PZdkP/Z5XET3F5x288evfumXVgyQGE2jCQ==",
            //        SecurityStamp = "c9feff7f-7e2e-461a-8d19-8eadb8103017",
            //        PhoneNumberConfirmed =false,
            //        TwoFactorEnabled = false,
            //        LockoutEnabled= true,
            //        AccessFailedCount = 0
            //    });
            context.Languages.AddOrUpdate(l => l.ShortCode,
                new Language { Name = "English", Code = "en-US", ShortCode = "en" },
                new Language { Name = "Deutsch", Code = "de-DE", ShortCode = "de" },
                new Language { Name = "Russian", Code = "ru-RU", ShortCode = "ru" },
                new Language { Name = "Ukranian", Code = "uk-ua", ShortCode = "uk" });

            context.SurveyStatuses.AddOrUpdate(s => s.Id,
                new SurveyStatus { Name = "Offline" },
                new SurveyStatus { Name = "Published" });

            context.QuestionFormats.AddOrUpdate(qf => qf.Code,
                new QuestionFormat { Name = "Text", Code = "text" },
                new QuestionFormat { Name = "Choice Across", Code = "choice_across" },
                new QuestionFormat { Name = "Choice Down", Code = "choice_down" },
                new QuestionFormat { Name = "Drop Down", Code = "drop_down" },
                new QuestionFormat { Name = "Matrix", Code = "matrix" },
                new QuestionFormat { Name = "Slider", Code = "slider" });

            context.QuestionLayouts.AddOrUpdate(ql => ql.Code,
                new QuestionLayout { Name = "QuestionCentric", Code = "qc" },
                new QuestionLayout { Name = "PersonCentric", Code = "pc" });

            context.NodeSelections.AddOrUpdate(ql => ql.Code,
                new NodeSelection { Name = "Normal", Code = "n" },
                new NodeSelection { Name = "Filtered", Code = "f" },
                new NodeSelection { Name = "Automatic", Code = "a" });
        }
    }
}
