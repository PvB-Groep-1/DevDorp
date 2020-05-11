/// <summary>
/// Represents a decision wich can be true or false.
/// </summary>
public abstract class DecisionLogic : ProgrammableBlockLogic
{
	/// <summary>
	/// Decides if the decision is true or false.
	/// </summary>
	/// <returns>True or false based on the decision.</returns>
	public abstract bool Decide();
}