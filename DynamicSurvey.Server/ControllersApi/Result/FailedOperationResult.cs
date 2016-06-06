using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicSurvey.Server.ControllersApi.Result
{
	public class FailedOperationResult : OperationResultBase
	{
		public string ErrorMessage { get; private set; }

		public FailedOperationResult(int errorCode, string errorMessage)
			: base(errorCode)
		{
			ErrorMessage = errorMessage;
		}

		public FailedOperationResult(int errorCode, Exception ex)
			: base(errorCode)
		{
			ErrorMessage = ex.Message;
		}

		public FailedOperationResult(Exception ex)
			: base(500)
		{
			ErrorMessage = ex.Message;
		}

		public static FailedOperationResult Unauthorized = new FailedOperationResult(401, "Unautorized");

	}
}