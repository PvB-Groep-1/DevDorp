using System;

/// <summary>
/// Represents logic for an if-statement.
/// </summary>
public sealed class StatementLogicIf : DecisionLogic
{
	/// <summary>
	/// Decides if the condition is met.
	/// </summary>
	/// <returns>True if the next ProgrammableBlock is a condition that is equal to true.</returns>
	public override bool Decide()
	{
		try
		{
			if (programmableBlock.SnappingPoints.right.snapPoint.ConnectedSnapPoint.ProgrammableBlock.GetComponent<DecisionLogic>().Decide())
				return true;
		}
		catch (Exception)
		{
			return false;
		}

		return false;
	}
}