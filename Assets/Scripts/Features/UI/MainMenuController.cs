using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls the main menu.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    /// <summary>
    /// This function toggles the visibility for the given gameobject.
    /// </summary>
    /// <param name="window">Takes in the gameobject of which the visibilty has to be toggled.</param>
    public void ToggleWindowVisibility(GameObject window)
    {
        window.SetActive(!window.activeSelf);
    }

    /// <summary>
    /// This function controls everything for closing the game.
    /// </summary>
    public void CloseApplication()
    {
        Application.Quit();
    }
}
