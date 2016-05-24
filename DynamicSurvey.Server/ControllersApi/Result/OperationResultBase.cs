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
		public static OperationResultBase ServerFault = new OperationResultBase(500);
		public static OperationResultBase Unauthorized = new OperationResultBase(401);
		// TODO: use enum
	}
}
