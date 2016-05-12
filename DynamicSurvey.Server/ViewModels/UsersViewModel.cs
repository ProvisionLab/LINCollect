using System.Collections.Generic;
using DynamicSurvey.Server.DAL.Entities;

namespace DynamicSurvey.Server.ViewModels
{
	public class UsersViewModel
	{
		public UsersFilter Filter { get; set; }
		public IEnumerable<User> Users { get; set; }
	}
}
