using UnityEngine;

/// <summary>
/// A Class that links the CameraZooming script with other classes.
/// </summary>
[RequireComponent(typeof(CameraZooming))]
[RequireComponent(typeof(CameraDragging))]
public class CameraZoomingEventSetter : MonoBehaviour
{
    private CameraZooming _cameraZooming;
    private CameraDragging _cameraDragging;

    private void Awake()
    {
        _cameraZooming = GetComponent<CameraZooming>();
        _cameraDragging = GetComponent<CameraDragging>();

        WorldApi.OnWorldLoad += () =>
        {
            _cameraZooming.EnableZooming();
        };

        WorldApi.OnWorldResume += () =>
        {
            _cameraZooming.EnableZooming();
        };

        WorldApi.OnWorldExit += () =>
        {
            _cameraZooming.DisableZooming();
        };

        WorldApi.OnWorldPause += () =>
        {
            _cameraZooming.DisableZooming();
        };

        _cameraZooming.OnZooming += () => 
        {
            _cameraDragging.ClampToBounds();
        };
    }
}