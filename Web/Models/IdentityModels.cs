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
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
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
        public virtual DbSet<RelationshipItem> RelationshipItems { get; set; }
    }
}