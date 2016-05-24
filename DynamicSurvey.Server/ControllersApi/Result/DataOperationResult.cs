using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicSurvey.Server.ControllersApi.Result
{
	public class DataOperationResult<TData> : OperationResultBase
	{
		public TData[] Data { get; set; }

		public DataOperationResult() : base(200)
		{

		}
	}
}