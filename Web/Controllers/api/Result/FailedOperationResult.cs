using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Linconnect.Controllers.api.Result
{
	public class FailedOperationResult : OperationResultBase
	{
		public string ErrorMessage { get; private set; }

		public FailedOperationResult(HttpStatusCode errorCode, string errorMessage)
			: base(errorCode)
		{
			ErrorMessage = errorMessage;
		}

		public FailedOperationResult(HttpStatusCode errorCode, Exception ex)
			: base(errorCode)
		{
			ErrorMessage = ex.Message;
		}

		public FailedOperationResult(Exception ex)
			: base(HttpStatusCode.InternalServerError)
		{
			ErrorMessage = ex.Message;
		}

		public static FailedOperationResult Unauthorized = new FailedOperationResult(HttpStatusCode.Unauthorized, "Unautorized");
		public static FailedOperationResult BadRequest = new FailedOperationResult(HttpStatusCode.BadRequest, "Bad request");

	}
}