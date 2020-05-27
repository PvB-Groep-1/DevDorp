using UnityEngine;

/// <summary>
/// A Class that links the CameraDragging script with other classes.
/// </summary>
[RequireComponent(typeof(CameraDragging))]
public class CameraDraggingEventSetter : MonoBehaviour
{
    private CameraDragging _cameraDragging;

    private void Awake()
    {
        _cameraDragging = GetComponent<CameraDragging>();

        WorldApi.OnWorldResume += () =>
        {
            _cameraDragging.EnableDragging();
        };

        WorldApi.OnWorldExit += () =>
        {
            _cameraDragging.DisableDragging();
        };

        WorldApi.OnWorldPause += () =>
        {
            _cameraDragging.DisableDragging();
        };
    }
}
