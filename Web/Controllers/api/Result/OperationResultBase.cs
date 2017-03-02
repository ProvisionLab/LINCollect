using System;
using System.Net;

namespace Linconnect.Controllers.api.Result
{
	public class OperationResultBase
	{
		public HttpStatusCode HttpResponse {get; set;}

		public OperationResultBase (HttpStatusCode httpResponseCode)
		{
			HttpResponse = httpResponseCode;
		}

		public static OperationResultBase Ok = new OperationResultBase(HttpStatusCode.OK);
		public static OperationResultBase Success = new OperationResultBase(HttpStatusCode.OK);
		// TODO: use enum
	}

	public class OperationResultDynamic : OperationResultBase
	{
		public dynamic Result { get; set; }

		public OperationResultDynamic() : base(HttpStatusCode.OK)
		{

		}
	}

	
}
