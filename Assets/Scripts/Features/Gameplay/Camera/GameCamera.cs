using UnityEngine;

/// <summary>
/// Represents the camera used in the game.
/// </summary>
[RequireComponent(typeof(CameraDragging))]
[RequireComponent(typeof(CameraZooming))]
public class GameCamera : MonoBehaviour
{
	/// <summary>
	/// Points to CameraDragging.
	/// </summary>
	public CameraDragging Dragging { get; private set; }

	/// <summary>
	/// Points to CameraZooming.
	/// </summary>
	public CameraZooming Zooming { get; private set; }

	private void Awake()
	{
		Dragging = GetComponent<CameraDragging>();
		Zooming = GetComponent<CameraZooming>();
	}
}