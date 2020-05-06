public sealed class OperatorLogic : ProgrammableBlockLogic
{
	public OperatorTypeEnum OperatorType { get; private set; }

	public enum OperatorTypeEnum
	{
		EqualsTo,
		GreaterThan,
		SmallerThan,
		GreaterThanOrEqualsTo,
		SmallerThanOrEqualsTo
	}

	#region Operator Methods

	private bool EqualsTo(float value1, float value2)
	{
		return value1 == value2;
	}

	private bool GreaterThan(float value1, float value2)
	{
		return value1 > value2;
	}

	private bool SmallerThan(float value1, float value2)
	{
		return value1 < value2;
	}

	private bool GreaterThanOrEqualsTo(float value1, float value2)
	{
		return value1 >= value2;
	}

	private bool SmallerThanOrEqualsTo(float value1, float value2)
	{
		return value1 <= value2;
	}

	#endregion

	public bool Decide(float value1, float value2)
	{
		switch (OperatorType)
		{
			case OperatorTypeEnum.EqualsTo:

				return EqualsTo(value1, value2);

			case OperatorTypeEnum.GreaterThan:

				return GreaterThan(value1, value2);

			case OperatorTypeEnum.SmallerThan:

				return SmallerThan(value1, value2);

			case OperatorTypeEnum.GreaterThanOrEqualsTo:

				return GreaterThanOrEqualsTo(value1, value2);

			case OperatorTypeEnum.SmallerThanOrEqualsTo:

				return SmallerThanOrEqualsTo(value1, value2);
		}

		return false;
	}
}