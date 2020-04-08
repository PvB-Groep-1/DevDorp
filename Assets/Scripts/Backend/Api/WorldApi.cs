using CM.Events;

/// <summary>
/// An Api class for everything about the world.
/// </summary>
public static class WorldApi
{
    /// <summary>
    /// An event for when the world loads.
    /// </summary>
    public static event SimpleEvent OnWorldLoad;

    /// <summary>
    /// An event for when the world exits.
    /// </summary>
    public static event SimpleEvent OnWorldExit;

    /// <summary>
    /// An event for when the world pauses.
    /// </summary>
    public static event SimpleEvent OnWorldPause;

    /// <summary>
    /// An event for when the world resumes.
    /// </summary>
    public static event SimpleEvent OnWorldResume;

    /// <summary>
    /// LoadWorld calls the OnWorldLoad event.
    /// </summary>
    public static void LoadWorld()
    {
        OnWorldLoad?.Invoke();
    }

    /// <summary>
    /// Exits the world.
    /// </summary>
    public static void ExitWorld()
    {
        OnWorldExit?.Invoke();
    }

    /// <summary>
    /// Pauses the world.
    /// </summary>
    public static void PauseWorld()
    {
        OnWorldPause?.Invoke();
    }

    /// <summary>
    /// Resumes the world.
    /// </summary>
    public static void ResumeWorld()
    {
        OnWorldResume?.Invoke();
    }
}
