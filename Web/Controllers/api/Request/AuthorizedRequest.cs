using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Controllers.api.Request
{
	public class AuthorizedRequest
	{
        [Required]
		public string Username { get; set; }
        [Required]
        public string Password { get; set; }

		public void Trim()
		{
			Func<string, string> trimmer = s => s.Trim('\"');

			Username = trimmer(Username);
			Password = trimmer(Password);
		}
	}
}