using System;

namespace DynamicSurvey.Server.ControllersApi
{
	public class OperationResultBase
	{
		public int HttpResponse {get; set;}

		public OperationResultBase (int httpResponseCode)
		{
			HttpResponse = httpResponseCode;
		}

		public static OperationResultBase Ok = new OperationResultBase(200);
		public static OperationResultBase Success = new OperationResultBase(200);
		// TODO: use enum
	}

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
