using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Linconnect.Controllers.api.Result
{
    public class DataOperationResult<TData> : OperationResultBase
    {
        public TData Data { get; set; }

        public DataOperationResult()
            : base(HttpStatusCode.OK)
        {

        }

        public DataOperationResult(TData singleEntity): base(HttpStatusCode.OK)
        {
            Data = singleEntity;
        }
    }
}