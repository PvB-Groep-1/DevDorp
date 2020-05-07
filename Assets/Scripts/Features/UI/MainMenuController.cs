using System.Collections;
using System.Collections.Generic;
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
    /// This function controls everything for closing the game.
    /// </summary>
    public void CloseApplication()
    {
        Application.Quit();
    }
}
