using CM.Events;
using UnityEngine;

/// <summary>
/// An Api class for everything about the world.
/// </summary>
public class WorldApi : MonoBehaviour
{
    /// <summary>
    /// An event for when the world loads.
    /// </summary>
    public event SimpleEvent OnWorldLoad;

    /// <summary>
    /// An event for when the world exits.
    /// </summary>
    public event SimpleEvent OnWorldExit;

    /// <summary>
    /// An event for when the world pauses.
    /// </summary>
    public event SimpleEvent OnWorldPause;

    /// <summary>
    /// An event for when the world resumes.
    /// </summary>
    public event SimpleEvent OnWorldResume;

    /// <summary>
    /// LoadWorld calls the OnWorldLoad event.
    /// </summary>
    public void LoadWorld()
    {
        OnWorldLoad?.Invoke();
    }

    /// <summary>
    /// Exits the world.
    /// </summary>
    public void ExitWorld()
    {
        OnWorldExit?.Invoke();
    }

    /// <summary>
    /// Pauses the world.
    /// </summary>
    public void PauseWorld()
    {
        OnWorldPause?.Invoke();
    }

    /// <summary>
    /// Resumes the world.
    /// </summary>
    public void ResumeWorld()
    {
        OnWorldResume?.Invoke();
    }
}
