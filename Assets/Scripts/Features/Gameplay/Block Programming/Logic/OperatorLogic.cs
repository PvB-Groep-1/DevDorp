/// <summary>
/// Represents logic for operators.
/// </summary>
public sealed class OperatorLogic : ProgrammableBlockLogic
{
	/// <summary>
	/// The current operator selected.
	/// </summary>
	public OperatorTypeEnum OperatorType { get; private set; }

	/// <summary>
	/// All types of operators.
	/// </summary>
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

	/// <summary>
	/// Decides if value1 and value2 return true or false depending on the currently selected operator.
	/// </summary>
	/// <param name="value1">The value to the left of the currently selected operator.</param>
	/// <param name="value2">The value to the right of the currently selected operator.</param>
	/// <returns>Depending on the currently selected operator used with value1 and value2 this will return true or false.</returns>
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