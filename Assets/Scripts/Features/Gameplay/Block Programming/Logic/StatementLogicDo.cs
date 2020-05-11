/// <summary>
/// Represents a decision wich always returns true.
/// </summary>
public sealed class StatementLogicDo : DecisionLogic
{
	/// <summary>
	/// Decision that will return true.
	/// </summary>
	/// <returns>True.</returns>
	public override bool Decide()
	{
		return true;
	}
}