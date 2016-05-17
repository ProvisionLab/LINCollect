namespace DynamicSurvey.Server.DAL.Entities
{
	public class AccessRight
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public AccessLevel AccessLevel { get; set; }
	}
}
