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
		MoreThan,
		LessThan,
		Equals,
		NonEquals
	}

	public abstract class ConditionBase
	{
        public int Id { get; set; }
		public string Operation { get; private set; }

		protected ConditionBase(int id, string operation)
		{
		    this.Id = id;
			this.Operation = operation;
		}

	}

	public class BinaryCondition : ConditionBase
	{
		public ConditionBase LeftOperand { get; private set; }

		public ConditionBase RightOperand { get; private set; }

		public BinaryCondition(int id, BinaryConditionEnum operation, ConditionBase leftOperand, ConditionBase rightOperand)
			: base(id, operation.ToString())
		{
			LeftOperand = leftOperand;
			RightOperand = rightOperand;
		}
	}

	public class UnaryCondition : ConditionBase
	{
		public int TargetId { get; private set; }

		public string TargetValue { get; private set; }

		public UnaryCondition(int id, UnaryConditionEnum operation, int targetId, string targetValue)
			: base(id, operation.ToString())
		{
			this.TargetId = targetId;
			this.TargetValue = targetValue;
		}
	}
}