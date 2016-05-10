using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
