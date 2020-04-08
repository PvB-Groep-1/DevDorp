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

    private bool _canZoom = true;
    private bool _isZooming = false;

    [SerializeField]
    private float _zoomSpeedMultiplier = 0.5f;
    [SerializeField]
    private float _minZoomDistance = 0.0f;
    [SerializeField]
    private float _maxZoomDistance = 5.0f;



    private void Start()
    {
        // Makes the min and max zoom level relative to the original camera position.
        _minZoomDistanceZ = Mathf.Cos(transform.rotation.x) * _minZoomDistance + transform.position.z;
        _maxZoomDistanceZ = Mathf.Cos(transform.rotation.x) * _maxZoomDistance + transform.position.z;

        _minZoomDistanceY = Mathf.Sin(transform.rotation.x) * _minZoomDistance + transform.position.y;
        _maxZoomDistanceY = Mathf.Sin(transform.rotation.x) * _maxZoomDistance + transform.position.y;
    }

    private void Update()
    {
        if (_canZoom)
        {

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
    }

    public void ClampPosition()
    {
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, _minZoomDistanceY, _maxZoomDistanceY),
            Mathf.Clamp(transform.position.z, _minZoomDistanceZ, _maxZoomDistanceZ)
        );
    }

    public void EnableZooming()
    {
        _canZoom = true;
    }

    public void DisableZooming()
    {
        _canZoom = false;
        _isZooming = false;
    }
}
