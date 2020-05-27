using UnityEngine;

/// <summary>
/// Represents the current game.
/// </summary>
public class Game : MonoBehaviour
{
	/// <summary>
	/// The main camera in the game.
	/// </summary>
	public static GameCamera MainCamera { get; private set; }

	/// <summary>
	/// The window at the bottom of the screen.
	/// </summary>
	public static BottomBarWindow BottomBarWindow { get; private set; }

	[SerializeField]
	private GameCamera _mainCamera;

	[SerializeField]
	private BottomBarWindow _bottomBarWindow;

	private void Awake()
	{
		MainCamera = _mainCamera;
		BottomBarWindow = _bottomBarWindow;
	}
}