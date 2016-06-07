using DynamicSurvey.Server.DAL.DataRowConverters;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Entities.SimpleEntities;
using System.Collections.Generic;

namespace DynamicSurvey.Server.DAL.Repositories
{
	public interface ILookupRepository
	{
		StringEntity[] GetCompanies();
		Company[] GetCompaniesFullInfo();
		Company GetCompanyById(ulong id);
		StringEntity[] GetCities();
		StringEntity[] GetCountries();

		// todo: replace UserName by Id
		ulong AddOrUpdateCompany(User caller, Company company);
	}
	public class LookupRepository : ILookupRepository
	{

		public StringEntity[] GetCompanies()
		{
			var entities = new List<StringEntity>();
			DataEngine.Engine.SelectFromView(DataEngine.vw_company_lookup, row => entities.Add(StringEntityConverter.ToEntity(row)));
			return entities.ToArray();
		}

		public Company[] GetCompaniesFullInfo()
		{
			var entities = new List<Company>();
			DataEngine.Engine.SelectFromView(DataEngine.vw_company, row => entities.Add(new Company(row)));
			return entities.ToArray();
		}

		public Company GetCompanyById(ulong id)
		{
			Company result = null;
			DataEngine.Engine.SelectFromView(DataEngine.vw_company, row => result = new Company(row),
				whereClause: Company.ToWhereClause(id),
				fillCommandAction: cmd => Company.FillCommandParam(cmd, id));
			return result;
		}

		public ulong AddOrUpdateCompany(User caller, Company company)
		{
			return DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_add_company, cmd => 
			{
				cmd.Parameters.AddWithValue("creator_login", caller.Username);
				cmd.Parameters.AddWithValue("creator_password", caller.Password);
				cmd.Parameters.AddWithValue("id", company.Id);
				cmd.Parameters.AddWithValue("name", company.Name);
				cmd.Parameters.AddWithValue("address", company.Address);
				cmd.Parameters.AddWithValue("phone_number", company.PostalCode);
				cmd.Parameters.AddWithValue("postal_code", company.PhoneNumber);
				cmd.Parameters.AddWithValue("city_name", company.City);
				cmd.Parameters.AddWithValue("country_name", company.Country); 
			});
		}

		public StringEntity[] GetCities()
		{
			var entities = new List<StringEntity>();
			DataEngine.Engine.SelectFromView(DataEngine.vw_city_lookup, row => entities.Add(StringEntityConverter.ToEntity(row)));
			return entities.ToArray();
		}

		public StringEntity[] GetCountries()
		{
			var entities = new List<StringEntity>();
			DataEngine.Engine.SelectFromView(DataEngine.vw_country_lookup, row => entities.Add(StringEntityConverter.ToEntity(row)));
			return entities.ToArray();
		}
	}
}
