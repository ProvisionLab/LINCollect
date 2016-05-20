namespace DynamicSurvey.Core.Entities
{
    public class User
    {
        public virtual int Id { get; protected set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }
        public virtual string Salt { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual UserRight UserRight { get; set; }
    }
}
