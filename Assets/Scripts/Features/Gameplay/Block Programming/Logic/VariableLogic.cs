/// <summary>
/// A base class for logic with variables.
/// </summary>
public abstract class VariableLogic : ProgrammableBlockLogic
{
	/// <summary>
	/// The current variable selected.
	/// </summary>
	public CodeVariables variable;

	/// <summary>
	/// Gets the value of the currently selected variable.
	/// </summary>
	/// <returns>The value of the currently selected variable.</returns>
	public abstract float GetValue();
}