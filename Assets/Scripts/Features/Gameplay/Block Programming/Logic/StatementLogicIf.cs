using System;

public sealed class StatementLogicIf : DecisionLogic
{
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