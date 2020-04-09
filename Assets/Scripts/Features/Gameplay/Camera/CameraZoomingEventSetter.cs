using UnityEngine;

/// <summary>
/// A Class that links the CameraZooming script with other classes.
/// </summary>
[RequireComponent(typeof(CameraZooming))]
public class CameraZoomingEventSetter : MonoBehaviour
{
    private CameraZooming _cameraZooming;

    private void Awake()
    {
        _cameraZooming = GetComponent<CameraZooming>();

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
    }
}