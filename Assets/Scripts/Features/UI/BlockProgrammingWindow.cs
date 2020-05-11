using UnityEngine;

/// <summary>
/// Represents all functionality for the block programming UI.
/// </summary>
public class BlockProgrammingWindow : MonoBehaviour
{
	/// <summary>
	/// Closes the block programming window.
	/// </summary>
	public void CloseWindow()
	{
		WindowApi.CloseLastWindow();
	}
}