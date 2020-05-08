using UnityEngine.SceneManagement;

/// <summary>
/// This is a class that loads the levels for the game.
/// </summary>
public static class LevelManager
{
    /// <summary>
    /// This function loads the main level in which the player plays the game.
    /// </summary>
    public static void LoadGameLevel()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// This function loads the main menu.
    /// </summary>
    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// This function reloads the current scene.
    /// </summary>
    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
