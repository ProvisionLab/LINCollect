namespace DynamicSurvey.Server.DAL.Entities
{
	public class AccessRight
	{
		public decimal Id { get; set; }
		public string Name { get; set; }
		public AccessLevel AccessLevel { get; set; }
	}
}
