using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicSurvey.Server.ViewModels
{
	public class UsersViewModel
	{
		public UsersFilter Filter { get; set; }
		public IEnumerable<User> Users { get; set; }

	}
}