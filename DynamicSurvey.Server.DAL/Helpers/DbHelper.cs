using System;

namespace DynamicSurvey.Server.DAL.Helpers
{
	public static class DbHelper
	{
		public static bool IsNull(this object target)
		{
			return target == null || target.GetType() == typeof(DBNull);
		}
	}
}
