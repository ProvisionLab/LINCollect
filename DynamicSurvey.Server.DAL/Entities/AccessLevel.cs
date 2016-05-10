using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.Entities
{
	public enum AccessLevel : int
	{
		Administrator = 0,
		Enumerator = 1,
		Respondent = 2
	}
}
