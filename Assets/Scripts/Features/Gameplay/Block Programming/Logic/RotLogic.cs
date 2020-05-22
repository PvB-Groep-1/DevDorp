using UnityEngine.UI;

/// <summary>
/// Represents logic for an Rot input field.
/// </summary>
[System.Serializable]
public struct RotLogic
{
    /// <summary>
	/// Input field for the Rot value.
	/// </summary>
    public InputField inputFieldRot;

    /// <summary>
	/// The saved Rot value.
	/// </summary>
    public int rot;

    /// <summary>
    /// Saves the Rot value typed in the corresponding input field.
    /// </summary>
    public void SetValues()
    {
        int.TryParse(inputFieldRot.text, out int tempRot);

        // Text Validation is not needed

        rot = tempRot;
    }
}
