using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.Entities
{
	public class AccessRight
	{
		public decimal Id { get; set; }
		public string Name { get; set; }
		public AccessLevel AccessLevel { get; set; }
	}
}
