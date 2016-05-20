namespace DynamicSurvey.Core.Entities
{
    public class City
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual Country Country { get; set; }
    }
}
