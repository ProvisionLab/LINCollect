using MySql.Data.MySqlClient;
using System.Data;

namespace DynamicSurvey.Server.DAL.Entities
{
	public class Company
	{
		public ulong Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public string PostalCode { get; set; }
		public ulong CityId { get; set; }
		public string City { get; set; }
		public ulong CountryId { get; set; }
		public string Country { get; set; }


		private static readonly string IdClause = "@Id";

		public Company()
		{

		}

		public Company(DataRow row)
		{
			Id = (ulong)row["Id"];
			Name = (string)row["Company"];
			Address = (string)row["Address"];
			PhoneNumber = (string)row["PhoneNumber"];
			PostalCode = (string)row["PostalCode"];
			CityId = (ulong)row["CityId"];
			City = (string)row["City"];
			CountryId = (ulong)row["CountryId"];
			Country = (string)row["Country"];
		}

		public static string ToWhereClause(ulong id)
		{
			return string.Format("WHERE {0}={1}", IdClause, id);
		}

		public static void FillCommandParam(MySqlCommand command, ulong id)
		{
			command.Parameters.Add(IdClause, id);
		}
	}
}