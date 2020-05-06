using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _CloseGameConfirmationWindow = null;

    public void ToggleWindowVisibility(GameObject window)
    {
        window.SetActive(!window.activeSelf);
    }

    public void CloseApplication()
    {
        Application.Quit();
    }
}
