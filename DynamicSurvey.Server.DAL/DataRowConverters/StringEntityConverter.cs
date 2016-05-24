using DynamicSurvey.Server.DAL.Entities.SimpleEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.DataRowConverters
{
	public static class StringEntityConverter
	{
		public static StringEntity ToEntity(DataRow row)
		{
			return new StringEntity()
			{
				Id = (ulong)row["Id"],
				Name = (string)row["Name"]
			};
		}
	}
}
