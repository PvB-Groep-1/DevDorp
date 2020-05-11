using UnityEngine;

/// <summary>
/// Represents logic for destroying an object.
/// </summary>
public sealed class DestroyLogic : ExecutionLogic
{
	[SerializeField]
	private XYLogic _XYLogic;

	/// <summary>
	/// Executes the destroy logic.
	/// </summary>
	public override void Execute()
	{
		if (_XYLogic.inputFieldX.text == "" || _XYLogic.inputFieldY.text == "")
			return;

		DestroyObjectCommand command = new DestroyObjectCommand(-15 + _XYLogic.x * 8, 5 + _XYLogic.y * 8);
		command.Execute();
	}

	/// <summary>
	/// Saves values for given input.
	/// </summary>
	public void SetValues()
	{
		_XYLogic.SetValues();
	}
}