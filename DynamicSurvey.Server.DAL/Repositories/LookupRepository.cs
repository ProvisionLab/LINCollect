﻿using DynamicSurvey.Server.DAL.Entities.SimpleEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicSurvey.Server.DAL.DataRowConverters;
using DynamicSurvey.Server.DAL.Entities;

namespace DynamicSurvey.Server.DAL.Repositories
{
	public interface ILookupRepository
	{
		StringEntity[] GetCompanies();
		Company GetCompanyById(ulong id);
		StringEntity[] GetCities();
		StringEntity[] GetCountries();
		void AddOrUpdateCompany(User caller, Company company);
	}
	public class LookupRepository : ILookupRepository
	{

		public StringEntity[] GetCompanies()
		{
			var entities = new List<StringEntity>();
			DataEngine.Engine.SelectFromView(DataEngine.vw_company_lookup, row => entities.Add(StringEntityConverter.ToEntity(row)));
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

		public void AddOrUpdateCompany(User caller, Company company)
		{
			DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_add_company, cmd => 
			{
				cmd.Parameters.AddWithValue("creator_login", caller.Username);
				cmd.Parameters.AddWithValue("creator_password", caller.Password);
				cmd.Parameters.AddWithValue("company_id", company.Id);

				cmd.Parameters.AddWithValue("company_address", company.Address);
				cmd.Parameters.AddWithValue("company_postal_code", company.PostalCode);
				cmd.Parameters.AddWithValue("company_id", company.PhoneNumber);
				cmd.Parameters.AddWithValue("city_id", company.CityId);
				cmd.Parameters.AddWithValue("city_name", company.City);
				cmd.Parameters.AddWithValue("country_id", company.CountryId);
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
