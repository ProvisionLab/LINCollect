using System;

namespace DynamicSurvey.Server.ControllersApi.Result
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

	public class OperationResultDynamic : OperationResultBase
	{
		public dynamic Result { get; set; }

		public OperationResultDynamic() : base(200)
		{
		}
	}

	
}
