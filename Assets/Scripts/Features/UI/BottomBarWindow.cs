using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents all functionality for the bottom UI bar.
/// </summary>
public class BottomBarWindow : MonoBehaviour
{
    /// <summary>
    /// All types of buttons in the BottomBarWindow.
    /// </summary>
    public enum ButtonTypes
    {
        BlockButton,
        HomeButton,
        GridButton
    }

    [SerializeField]
    private Button _gridButton;

    [SerializeField]
    private GameObject _gridObject;

    /// <summary>
    /// Opens the code window.
    /// </summary>
    public void OpenCodeWindow()
    {
        WindowApi.OpenWindow(WindowTypes.CodeMenu);
    }

    /// <summary>
    /// Opens the main menu scene.
    /// </summary>
    public void OpenMainMenu()
    {
        LevelManager.LoadMainMenu();
    }

    /// <summary>
    /// Toggles the grid GameObject
    /// </summary>
    public void ToggleGrid()
    {
        _gridObject.SetActive(!_gridObject.activeSelf);
    }
}