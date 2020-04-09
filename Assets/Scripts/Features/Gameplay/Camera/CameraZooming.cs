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

    private float _minZoomDistanceZ;
    private float _maxZoomDistanceZ;
    private float _minZoomDistanceY;
    private float _maxZoomDistanceY;

    private bool _isZooming = false;

    [SerializeField]
    private float _zoomSpeedMultiplier = 0.5f;
    [SerializeField]
    private float _minZoomDistance = 0.0f;
    [SerializeField]
    private float _maxZoomDistance = 5.0f;

    [SerializeField]
    private bool _canZoom = false;

    private void Start()
    {
        SetZoomBoundaries(_minZoomDistance, _maxZoomDistance);
    }

    private void Update()
    {
        if (!_canZoom)
            return;

#if UNITY_EDITOR || UNITY_STANDALONE

        if (Input.mouseScrollDelta.y != 0)
        {
            _isZooming = true;

            transform.position += new Vector3(
                0,
                -Mathf.Sin(transform.rotation.x) * Input.mouseScrollDelta.y * _zoomSpeedMultiplier,
                Mathf.Cos(transform.rotation.x) * Input.mouseScrollDelta.y * _zoomSpeedMultiplier
            );

            OnZooming?.Invoke();

            ClampPosition();
        }

#endif

    }

    /// <summary>
    /// Makes sure the zoomed position stays within the min and max zoom distance.
    /// </summary>
    public void ClampPosition()
    {
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, _minZoomDistanceY, _maxZoomDistanceY),
            Mathf.Clamp(transform.position.z, _minZoomDistanceZ, _maxZoomDistanceZ)
        );
    }

    /// <summary>
    /// Sets the minimum and maximum zoom distance of the camera.
    /// </summary>
    /// <param name="min"> The minimal zoom distance relative to the initial camera position (0 means the minimal zoom is the current position). </param>
    /// <param name="max"> The maximal zoom distance relative to the initial camera position. </param>
    public void SetZoomBoundaries(float min, float max)
    {
        _minZoomDistance = min;
        _maxZoomDistance = max;

        // Makes the min and max zoom level relative to the original camera position.
        _minZoomDistanceZ = Mathf.Cos(transform.rotation.x) * _minZoomDistance + transform.position.z;
        _maxZoomDistanceZ = Mathf.Cos(transform.rotation.x) * _maxZoomDistance + transform.position.z;

        _minZoomDistanceY = Mathf.Sin(transform.rotation.x) * _minZoomDistance + transform.position.y;
        _maxZoomDistanceY = Mathf.Sin(transform.rotation.x) * _maxZoomDistance + transform.position.y;
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
