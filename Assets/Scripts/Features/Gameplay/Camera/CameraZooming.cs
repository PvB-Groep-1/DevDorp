using CM.Events;
using UnityEngine;

/// <summary>
/// A class for zooming the Camera in and out.
/// This script needs to be placed on the camera object.
/// </summary>
public class CameraZooming : MonoBehaviour
{
    /// <summary>
    /// A simple event to call when the camera is zooming in or out.
    /// </summary>
    public event SimpleEvent OnZooming;

    private bool _isZooming = false;

    [SerializeField]
    private float _zoomSpeedMultiplier = 10f;
    [SerializeField]
    private float _minZoomDistance = 30f;
    [SerializeField]
    private float _maxZoomDistance = 60f;

    [SerializeField]
    private bool _canZoom = true;

    private void Start()
    {
        SetZoomBoundaries(_minZoomDistance, _maxZoomDistance);
    }

    private void Update()
    {
        if (!_canZoom)
            return;

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            _isZooming = true;
            OnZooming?.Invoke();

            float fov = Camera.main.fieldOfView;
            fov -= Input.GetAxis("Mouse ScrollWheel") * _zoomSpeedMultiplier;
            fov = Mathf.Clamp(fov, _minZoomDistance, _maxZoomDistance);
            Camera.main.fieldOfView = fov;
        }
    }

    /// <summary>
    /// Sets the minimum and maximum zoom distance of the camera.
    /// </summary>
    /// <param name="min"> The minimal field of view (lower means more zoomed in). </param>
    /// <param name="max"> The maximal field of view (lower means more zoomed in). </param>
    public void SetZoomBoundaries(float min, float max)
    {
        _minZoomDistance = min;
        _maxZoomDistance = max;
    }

    /// <summary>
    /// Enables zooming, this function will be called by CameraZoomingEventSetter.
    /// </summary>
    public void EnableZooming()
    {
        _canZoom = true;
    }

    /// <summary>
    /// Disabled zooming, this function will be called by CameraZoomingEventSetter.
    /// </summary>
    public void DisableZooming()
    {
        _canZoom = false;
        _isZooming = false;
    }
}
