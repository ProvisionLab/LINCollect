namespace DynamicSurvey.Core.Entities
{
    public class Company
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual City City { get; set; }
    }
}
