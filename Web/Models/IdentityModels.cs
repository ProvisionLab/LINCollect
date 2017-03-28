using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Web.Data;

namespace Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //Survey
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<SurveyStatus> SurveyStatuses { get; set; }
        public virtual DbSet<SurveyFile> SurveyFiles { get; set; }
        //Respondents
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<QuestionFormat> QuestionFormats { get; set; }
        public virtual DbSet<Respondent> Respondents { get; set; }
        //Relationship
        public virtual DbSet<QuestionLayout> QuestionLayouts { get; set; }
        public virtual DbSet<NodeSelection> NodeSelections { get; set; }
        public virtual DbSet<RAnswer> RAnswers { get; set; }
        public virtual DbSet<RQuestion> RQuestions { get; set; }
        public virtual DbSet<NAnswer> NAnswers { get; set; }
        public virtual DbSet<NQuestion> NQuestions { get; set; }
        public virtual DbSet<RelationshipItem> RelationshipItems { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<PublishSurvey> PublishSurveys { get; set; }
        public virtual DbSet<UserAnswer> UserAnswers { get; set; }
        public virtual DbSet<UserQuestionAnswer> UserQuestionAnswers{ get; set; }
        public virtual DbSet<UserQuestionAnswerValue> UserQuestionAnswerValues { get; set; }

    }

    public class ApplicationDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.Languages.AddRange( new Language[]{
                new Language { Name = "English", Code = "en-US", ShortCode = "en" },
                new Language { Name = "Deutsch", Code = "de-DE", ShortCode = "de" },
                new Language { Name = "Russian", Code = "ru-RU", ShortCode = "ru" },
                new Language { Name = "Ukranian", Code = "uk-ua", ShortCode = "uk" }});

            context.SurveyStatuses.AddRange(new SurveyStatus[] {
                new SurveyStatus { Name = "Offline" },
                new SurveyStatus { Name = "Published" }});

            context.QuestionFormats.AddRange( new QuestionFormat[] {
                new QuestionFormat { Name = "Text", Code = "text" },
                new QuestionFormat { Name = "Choice Across", Code = "choice_across" },
                new QuestionFormat { Name = "Choice Down", Code = "choice_down" },
                new QuestionFormat { Name = "Drop Down", Code = "drop_down" },
                new QuestionFormat { Name = "Matrix", Code = "matrix" },
                new QuestionFormat { Name = "Slider", Code = "slider" }});

            context.QuestionLayouts.AddRange(new QuestionLayout[] {
                new QuestionLayout { Name = "QuestionCentric", Code = "qc" },
                new QuestionLayout { Name = "PersonCentric", Code = "pc" }});

            context.NodeSelections.AddRange( new NodeSelection[] {
                new NodeSelection { Name = "Normal", Code = "n" },
                new NodeSelection { Name = "Filtered", Code = "f" },
                new NodeSelection { Name = "Automatic", Code = "a" }});

            context.SaveChanges();

            base.Seed(context);
        }
    }


}    
