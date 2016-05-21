using DynamicSurvey.Server.DAL.Entities.SimpleEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.Repositories
{
	public interface ILookupRepository
	{
		IEnumerable<StringEntity> GetCompanies();
		IEnumerable<StringEntity> GetCities();
		IEnumerable<StringEntity> GetCountries();
	}
	public class LookupRepository : ILookupRepository
	{

		public IEnumerable<StringEntity> GetCompanies()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<StringEntity> GetCities()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<StringEntity> GetCountries()
		{
			throw new NotImplementedException();
		}
	}
}
