using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Web.Data;
using Web.Migrations;
using Question = Web.Data.Question;
using Respondent = Web.Data.Respondent;
using Token = Web.Data.Token;

namespace Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Surveys = new HashSet<Survey>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Survey> Surveys { get; set; }

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
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            //Database.SetInitializer(new ApplicationDbInitializer());
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
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<ResultSection> ResultSections { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual DbSet<QuestionAnswerValue> QuestionAnswerValues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany<Survey>(t => t.Surveys)
                .WithMany(t => t.ApplicationUsers)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserId");
                    cs.MapRightKey("SurveyId");
                    cs.ToTable("UserSurvey");
                });
            base.OnModelCreating(modelBuilder);
        }
    }

    public class ApplicationDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.Languages.AddRange(new Language[]{
                new Language { Name = "English", Code = "en-US", ShortCode = "en" },
                new Language { Name = "Deutsch", Code = "de-DE", ShortCode = "de" },
                new Language { Name = "Russian", Code = "ru-RU", ShortCode = "ru" },
                new Language { Name = "Ukranian", Code = "uk-ua", ShortCode = "uk" }});

            context.SurveyStatuses.AddRange(new SurveyStatus[] {
                new SurveyStatus { Name = "Offline" },
                new SurveyStatus { Name = "Published" }});

            context.QuestionFormats.AddRange(new QuestionFormat[] {
                new QuestionFormat { Name = "Text", Code = "text" },
                new QuestionFormat { Name = "Choice Across", Code = "choice_across" },
                new QuestionFormat { Name = "Choice Down", Code = "choice_down" },
                new QuestionFormat { Name = "Drop Down", Code = "drop_down" },
                new QuestionFormat { Name = "Matrix", Code = "matrix" },
                new QuestionFormat { Name = "Slider", Code = "slider" }});

            context.QuestionLayouts.AddRange(new QuestionLayout[] {
                new QuestionLayout { Name = "QuestionCentric", Code = "qc" },
                new QuestionLayout { Name = "PersonCentric", Code = "pc" }});

            context.NodeSelections.AddRange(new NodeSelection[] {
                new NodeSelection { Name = "Normal", Code = "n" },
                new NodeSelection { Name = "Filtered", Code = "f" },
                new NodeSelection { Name = "Automatic", Code = "a" }});

            context.Sections.AddRange(new Section[]
            {
                new Section {Name = "RespondentBefore"},
                new Section {Name = "RespondentAfter"},
                new Section {Name = "Relationship"},
                new Section {Name = "Node"}
            });

            var administrator = context.Roles.Add(new IdentityRole
            {
                Name = "Administrator"
            });

            var interviewer = context.Roles.Add(new IdentityRole
            {
                Name = "Interviewer"
            });

            var user = context.Users.Add(new ApplicationUser
            {
                Email = "sa@test.com",
                PasswordHash = "AKyNK8+hqhxSup7h5AWLXn7dYpJoiAbmcBBUrh2Vk8Jhhje5iQvGE49uCu2AYgWgIw==",
                SecurityStamp = "f399883a-17ef-46f8-8aec-9890ee3c05ab",
                UserName = "sa@test.com",
                LockoutEnabled = true
            });

            user.Roles.Add(new IdentityUserRole { RoleId = administrator.Id, UserId = user.Id });

            context.SaveChanges();

            base.Seed(context);
        }
    }


}
