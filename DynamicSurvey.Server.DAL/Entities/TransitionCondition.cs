using System.Collections.Generic;

namespace DynamicSurvey.Server.DAL.Entities
{
	// операндом может быть либо операция либо айди

	public enum BinaryConditionEnum
	{
		And,
		Or
	}

	public enum UnaryConditionEnum
	{
        Equals,
        NotEquals,
        More,
        Less,
        MoreEquals,
        LessEquals
    }

	public abstract class ConditionBase
	{
        public ulong Id { get; set; }
		public string Operation { get; private set; }

		protected ConditionBase(ulong id, string operation)
		{
		    this.Id = id;
			this.Operation = operation;
		}

	}

	public class BinaryCondition : ConditionBase
	{
		public ConditionBase LeftOperand { get; private set; }

		public ConditionBase RightOperand { get; private set; }

		public BinaryCondition(ulong id, BinaryConditionEnum operation, ConditionBase leftOperand, ConditionBase rightOperand)
			: base(id, operation.ToString())
		{
			LeftOperand = leftOperand;
			RightOperand = rightOperand;
		}
	}

	public class UnaryCondition : ConditionBase
	{
		public ulong TargetFieldId { get; private set; }

		public string TargetValue { get; private set; }

		public UnaryCondition(ulong id, UnaryConditionEnum operation, ulong targetFieldId, string targetValue)
			: base(id, operation.ToString())
		{
			this.TargetFieldId = targetFieldId;
			this.TargetValue = targetValue;
		}
	}
}