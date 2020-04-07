using UnityEngine;
using CM.Events;

// <summary>
// Controls the 2 dimensional movement of the Camera.
// </summary>

public class CameraDragging : MonoBehaviour
{
    private Camera _camera;

    //private Boundary2D _boundary; 

    private Vector3 _previousPosition;
    private Vector3 _touchOrigin;

    private bool _isDragging;
    private bool _canDrag;

    private float _draggingSpeedMultiplier;
    private float _draggingThreshold;

    public event SimpleEvent OnDragging;

    private void Awake()
    {}

    private void Start()
    {
        // Set _camera to the Main Camera.
        _camera = Camera.main;

        _canDrag = true;
        _draggingSpeedMultiplier = 1;
    }

    private void Update()
    {
        if (_canDrag)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButtonDown(0))
                SaveOriginPositions();

            if (Input.GetMouseButton(0))
                MoveCamera();

            if (Input.GetMouseButtonUp(0))
            {
                _previousPosition = transform.position;
                _touchOrigin = _camera.ScreenToViewportPoint(Input.mousePosition);
            }
#endif
        }
    }

    private void SaveOriginPositions()
    {
        _previousPosition = transform.position;

#if UNITY_EDITOR || UNITY_STANDALONE
        _touchOrigin = _camera.ScreenToViewportPoint(Input.mousePosition);
#endif
    }

    private void MoveCamera()
    {
        // Get the new position of the mouse relative to the _touchOrigin.
        Vector3 newPosition;

#if UNITY_EDITOR || UNITY_STANDALONE
        newPosition = _camera.ScreenToViewportPoint(Input.mousePosition) - _touchOrigin;
#endif

        // Move the Camera.
        transform.position = new Vector3(
            _previousPosition.x + -newPosition.x * _draggingSpeedMultiplier * 10,
            transform.position.y,
            _previousPosition.z + -newPosition.y * _draggingSpeedMultiplier * 10
        );

        ClampToBounds();

        // Save the updated position as new origin position
        SaveOriginPositions();
    }

    private void OnDestroy()
    {}

    private void OnValidate()
    {}

    private void OnDrawGizmosSelected()
    {}

    // Clamp the Camera to the world bounds.
    public void ClampToBounds()
    {}

    /// <summary>
    /// Enables the dragging of the Camera.
    /// </summary>
    public void EnableDragging()
    {
        _canDrag = true;
    }

    /// <summary>
    /// Disables the dragging of the Camera.
    /// Also stops the Camera if it is currently being dragged.
    /// </summary>
    public void DisableDragging()
    {
        _canDrag = false;
        _isDragging = false;
    }
}
