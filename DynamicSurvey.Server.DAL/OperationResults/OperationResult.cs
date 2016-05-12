using System.Collections.Generic;

namespace DynamicSurvey.Server.DAL.OperationResults
{
	public class OperationResult
	{
		public List<string> Messages { get; set; }

		public ResultCode Result { get; set; }

		public OperationResult()
		{
			Messages = new List<string>();
			Result = ResultCode.Failed;
		}

	}

	public class OperationResult<T> : OperationResult
	{
		public T Result { get; set; }

		public OperationResult()
			: base()
		{
			Result = default(T);
		}
	}



}
