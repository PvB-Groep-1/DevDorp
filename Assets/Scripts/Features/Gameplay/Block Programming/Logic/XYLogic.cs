using UnityEngine.UI;

/// <summary>
/// Represents logic for an X and Y input field.
/// </summary>
[System.Serializable]
public struct XYLogic
{
	/// <summary>
	/// Input field for the X value.
	/// </summary>
	public InputField inputFieldX;

	/// <summary>
	/// Input field for the Y value.
	/// </summary>
	public InputField inputFieldY;

	/// <summary>
	/// The saved X value.
	/// </summary>
	public int x;

	/// <summary>
	/// The saved Y value.
	/// </summary>
	public int y;

	/// <summary>
	/// Saves the X and Y value typed in the corresponding input fields.
	/// </summary>
	public void SetValues()
	{
		int.TryParse(inputFieldX.text, out int tempX);
		int.TryParse(inputFieldY.text, out int tempY);

		if (tempX < 0 || inputFieldX.text == "-")
		{
			tempX = 0;
			inputFieldX.text = tempX.ToString();
		}

		if (tempY < 0 || inputFieldY.text == "-")
		{
			tempY = 0;
			inputFieldY.text = tempY.ToString();
		}

		if (tempX > 8)
		{
			tempX = 8;
			inputFieldX.text = tempX.ToString();
		}

		if (tempY > 8)
		{
			tempY = 8;
			inputFieldY.text = tempY.ToString();
		}

		x = tempX;
		y = tempY;
	}
}