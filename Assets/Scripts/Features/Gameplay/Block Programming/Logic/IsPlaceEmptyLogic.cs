using UnityEngine;

/// <summary>
/// Represents a decision for if a place is empty.
/// </summary>
public sealed class IsPlaceEmptyLogic : DecisionLogic
{
	[SerializeField]
	private XYLogic _XYLogic;

	/// <summary>
	/// Decides if the place is empty.
	/// </summary>
	/// <returns>True if there is no collider detected on given location.</returns>
	public override bool Decide()
	{
		if (_XYLogic.inputFieldX.text == "" || _XYLogic.inputFieldY.text == "")
			return false;

		Vector3 spawnPoint = new Vector3(-15 + _XYLogic.x * 9, 0, 5 + _XYLogic.y * 9);
		Collider[] hitCollider = Physics.OverlapBox(spawnPoint, Vector3.one / 4);

		if (hitCollider.Length > 0)
			return false;

		return true;
	}

	/// <summary>
	/// Saves values for given input.
	/// </summary>
	public void SetValues()
	{
		_XYLogic.SetValues();
	}
}