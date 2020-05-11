using UnityEngine;

/// <summary>
/// Represents all functionality for the bottom UI bar.
/// </summary>
public class BottomBarWindow : MonoBehaviour
{
	/// <summary>
	/// Opens the code window.
	/// </summary>
	public void OpenCodeWindow()
	{
		WindowApi.OpenWindow(WindowTypes.CodeMenu);
	}
}