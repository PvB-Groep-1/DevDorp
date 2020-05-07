using UnityEngine;

/// <summary>
/// This class controls the confirmation quit windows.
/// </summary>
public class ConfirmationQuitWindow : MonoBehaviour
{
    /// <summary>
    /// This function closes the quit confirmation windows.
    /// </summary>
    public void CancelQuit()
    {
        WindowApi.CloseLastWindow();
    }

    /// <summary>
    /// This function closes the application.
    /// </summary>
    public void CloseApplication()
    {
        Application.Quit();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
