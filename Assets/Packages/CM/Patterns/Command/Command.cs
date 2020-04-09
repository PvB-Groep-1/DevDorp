namespace CM.Patterns.Command
{
	/// <summary>
	/// This class represents functionality that can be executed.
	/// </summary>
	public abstract class Command
	{
		/// <summary>
		/// Executes the functionality.
		/// </summary>
		public abstract void Execute();

		/// <summary>
		/// Undoes the previously executed functionality.
		/// </summary>
		public abstract void Undo();
	}
}