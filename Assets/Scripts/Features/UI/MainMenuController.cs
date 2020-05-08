using UnityEngine;

/// <summary>
/// This class controls the main menu.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    /// <summary>
    /// This function toggles the visibility for the exit confirmation window.
    /// </summary>
    public void OpenExitConfirmationWindow()
    {
        WindowApi.OpenWindow(WindowTypes.ExitConfirmation);
    }
    
    /// <summary>
    /// This function loads the main scene in which the player will play the game.
    /// </summary>
    public void LoadGameScene()
    {
        LevelManager.LoadGameLevel();
    }
}
