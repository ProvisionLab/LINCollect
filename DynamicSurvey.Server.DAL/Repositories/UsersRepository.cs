using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DynamicSurvey.Server.DAL.Entities;

namespace DynamicSurvey.Server.DAL
{

	public interface IUsersRepository
	{
		void AddOrUpdate(User caller, User target);
		void Remove(User caller, string username);
		User GetUserByName(string username);
		User[] GetUsers(User caller, UsersFilter filter);

		bool Authorize(string username, string password);

		bool CheckCredentials(string username, string hashedPassword);

		void Init(string username, string password, string accessLevelName);
	}

	public class UsersRepository : IUsersRepository
	{
		public void AddOrUpdate(User caller, User target)
		{
			using (var context = new DbSurveysContext())
			{
				ValidateCaller(context, caller);
				AddOrUpdateInternal(context, target.Username, target.Password, target.AccessRight.Name);
			}
		}

		private void AddOrUpdateInternal(DbSurveysContext context, string username, string plainPassword, string accessRightName)
		{
			var user = context.user.SingleOrDefault(u => u.login == username);
			// HACK: how strange AccessLevel entity performed. 
			var right = context.user_right.Single(r => r.name == accessRightName);
			var salt = DateTime.Now.ToString("yyyyMMddHHmmsstt");
			var hashedPassword = CalculateMD5Hash(salt + plainPassword);
			if (user == null)
			{
				context.user.Add(new user()
				{
					login = username,
					user_right_id = right.id,
					password = hashedPassword,
					salt = salt
				});
			}
			else
			{
				user.login = username;
				user.user_right_id = right.id;
				user.password = hashedPassword;
				user.salt = salt;
				user.is_deleted = 0;
			}

			context.SaveChanges();
		}

		private string CalculateMD5Hash(string input)
		{

			MD5 md5 = System.Security.Cryptography.MD5.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
			byte[] hash = md5.ComputeHash(inputBytes);

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				sb.Append(hash[i].ToString("X2"));
			}

			return sb.ToString();
		}

		public void Remove(User caller, string username)
		{
			using (var context = new DbSurveysContext())
			{
				ValidateCaller(context, caller);

				var user = context.user.SingleOrDefault(u => u.login.Equals(username, StringComparison.InvariantCultureIgnoreCase));

				if (user == null)
				{
					throw new ArgumentException(string.Format("user with name {0} not found", username));
				}

				user.is_deleted = 1;
				context.SaveChanges();
			}
		}

		public User GetUserByName(string username)
		{
			using (var context = new DbSurveysContext())
			{

				var user = context.user
					.Where(u => u.is_deleted != 1)
					.Where(u => u.login == username)
					.SingleOrDefault();

				if (user == null)
				{
					return null;
				}

				return user.ToContract();
			}
		}

		public User[] GetUsers(User caller, UsersFilter filter)
		{
			using (var context = new DbSurveysContext())
			{

				var userQuery = context.user
					.Where(u => u.is_deleted != 1)
					.AsQueryable();

				// final filter
				userQuery = filter.Pager.SelectPageQuery(userQuery, u => u.login);

				// TODO: rework
				return userQuery
					.ToArray()
					.Select(u => u.ToContract())
					.ToArray();
			}
		}


		public bool Authorize(string username, string password)
		{
			using (var context = new DbSurveysContext())
			{
				var user = context.user
					.Where(u => u.login.Equals(username))
					.SingleOrDefault();

				if (user == null)
					return false;

				var hashedPassword = CalculateMD5Hash(user.salt + password);
				return user.password.Equals(hashedPassword);
			}
		}

		public void Init(string username, string plainPassword, string accessLevel)
		{
			using (var context = new DbSurveysContext())
			{
				AddOrUpdateInternal(context, username, plainPassword, accessLevel);
			}
		}

		private void ValidateCaller(DbSurveysContext context, User caller)
		{
#if DEBUG
			if (caller == null)
			{
				return;
			}
#endif

			if (caller.AccessRight.AccessLevel != AccessLevel.Administrator)
			{
				throw new System.Security.SecurityException("caller not administrator");
			}
			if (GetUserByName(caller.Username) == null)
			{
				throw new System.ArgumentException("user {0} not found", caller.Username);
			}
		}


		public bool CheckCredentials(string username, string hashedPassword)
		{
			using (var context = new DbSurveysContext())
			{
				return context.user
					.Where(u => u.login.Equals(username))
					.Where(u => u.password.Equals(hashedPassword))
					.Any();
			}
		}
	}
}
