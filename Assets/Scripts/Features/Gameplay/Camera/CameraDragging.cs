using UnityEngine;
using CM.Events;

/// <summary>
/// Controls the 2 dimensional movement of the Camera.
/// </summary>
public class CameraDragging : MonoBehaviour
{
    /// <summary>
    /// A simple event to call when the camera is being moved.
    /// </summary>
    public event SimpleEvent OnDragging;

    private Camera _camera;

    private Vector3 _previousPosition;
    private Vector3 _touchOrigin;

    private bool _isDragging;

    [Tooltip("The boundaries of the Camera movement")]
    [SerializeField]
    private Boundary2D _boundary;

    [Tooltip("The speed of the camera dragging, also affects movement by keyboard.")]
    [SerializeField]
    private float _draggingSpeedMultiplier = 1;

    [Tooltip("The threshold for the camera dragging. If the mouse movement is bigger than the threshold, begin dragging.")]
    [SerializeField]
    private float _draggingThreshold = 0.001f;

    [SerializeField]
    private bool _canDrag = false;

    private void Start()
    {
        // Set _camera to the Main Camera.
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!_canDrag)
            return;

        // Movement via Dragging
        if (Input.GetMouseButtonDown(0))
            SaveOriginPositions();

        if (Input.GetMouseButton(0))
            MoveCamera();

        if (Input.GetMouseButtonUp(0))
        {
            _previousPosition = transform.position;
            _touchOrigin = _camera.ScreenToViewportPoint(Input.mousePosition);
            _isDragging = false;
        }

        // Movement via Keyboard
        Vector3 newPosition = new Vector3(0, 0, 0);
        if (Input.GetAxisRaw("Vertical") != 0)
            newPosition.z += Input.GetAxisRaw("Vertical")/5 * _draggingSpeedMultiplier;

        if (Input.GetAxisRaw("Horizontal") != 0)
            newPosition.x += Input.GetAxisRaw("Horizontal")/5 * _draggingSpeedMultiplier;


        transform.position += newPosition;

        ClampToBounds();
    }

    private void SaveOriginPositions()
    {
        _previousPosition = transform.position;

        _touchOrigin = _camera.ScreenToViewportPoint(Input.mousePosition);
    }

    private void MoveCamera()
    {
        OnDragging?.Invoke();

        // Get the new position of the mouse relative to the _touchOrigin.
        Vector3 newPosition;

        newPosition = _camera.ScreenToViewportPoint(Input.mousePosition) - _touchOrigin;

        //Checks if the mouse is outside the draggingThreshold
        if ((Mathf.Abs(newPosition.x) > _draggingThreshold || Mathf.Abs(newPosition.y) > _draggingThreshold) || _isDragging)
        {
            _isDragging = true;
            // Move the Camera.
            transform.position = new Vector3(
                _previousPosition.x + -newPosition.x * _draggingSpeedMultiplier * 10,
                transform.position.y,
                _previousPosition.z + -newPosition.y * _draggingSpeedMultiplier * 10
            );

            // Save the updated position as new origin position
            SaveOriginPositions();
        }
    }

    /// <summary>
    /// Clamp the Camera to the world bounds.
    /// </summary>
    public void ClampToBounds()
    {
        Vector2 newPosition = _boundary.Clamp(transform.position.x, transform.position.z);

        transform.position = new Vector3(
            newPosition.x,
            transform.position.y,
            newPosition.y
        );
    }

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

    #region Editor Region

    private void OnValidate()
    {
        ClampToBounds();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color32(255, 0, 0, 170);

        // Left boundary.
        Gizmos.DrawLine(new Vector3(
            _boundary.min.x,
            transform.position.y,
            _boundary.min.y),
            new Vector3(_boundary.min.x,
            transform.position.y,
            _boundary.max.y)
        );

        // Right boundary.
        Gizmos.DrawLine(new Vector3(
            _boundary.max.x,
            transform.position.y,
            _boundary.min.y),
            new Vector3(_boundary.max.x,
            transform.position.y,
            _boundary.max.y)
        );

        // Up boundary.
        Gizmos.DrawLine(new Vector3(
            _boundary.min.x,
            transform.position.y,
            _boundary.max.y),
            new Vector3(_boundary.max.x,
            transform.position.y,
            _boundary.max.y)
        );

        // Down boundary.
        Gizmos.DrawLine(new Vector3(
            _boundary.min.x,
            transform.position.y,
            _boundary.min.y),
            new Vector3(_boundary.max.x,
            transform.position.y,
            _boundary.min.y)
        );
    }

    #endregion

}
