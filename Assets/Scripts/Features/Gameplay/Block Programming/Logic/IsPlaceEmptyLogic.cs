using UnityEngine;
using UnityEngine.UI;

public class IsPlaceEmptyLogic : DecisionLogic
{
	[SerializeField]
	private InputField _inputFieldX;

	[SerializeField]
	private InputField _inputFieldY;

	[SerializeField]
	private int _x;

	[SerializeField]
	private int _y;

	public override bool Decide()
	{
		Vector3 spawnPoint = new Vector3(_x, 0, _y);
		Collider[] hitCollider = Physics.OverlapBox(spawnPoint, Vector3.one / 4);

		if (hitCollider.Length > 0)
			return false;

		return true;
	}

	public void SetValues()
	{
		int.TryParse(_inputFieldX.text, out int x);
		int.TryParse(_inputFieldY.text, out int y);

		_x = x;
		_y = y;
	}
}