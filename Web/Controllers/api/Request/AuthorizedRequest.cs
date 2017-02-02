using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linconnect.Controllers.api.Result
{
	public class AuthorizedRequest
	{
		public string Username { get; set; }
		public string Password { get; set; }

		public void Trim()
		{
			Func<string, string> trimmer = s => s.Trim('\"');

			Username = trimmer(Username);
			Password = trimmer(Password);
		}
	}
}