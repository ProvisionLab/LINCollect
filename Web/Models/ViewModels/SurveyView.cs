namespace Web.Models.ViewModels
{
    public class SurveyView : Entity
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int LanguageId { get; set; }
        public string Language { get; set; }
        public int SurveyStatusId { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Banner { get; set; }
        public string Introduction { get; set; }
        public string Thanks { get; set; }
        public string Landing { get; set; }
        public string MailingFile { get; set; }
    }
}