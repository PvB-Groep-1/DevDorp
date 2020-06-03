using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents all functionality for the bottom UI bar.
/// </summary>
public class BottomBarWindow : MonoBehaviour
{
	/// <summary>
	/// A delegate with a ButtonTypes parameter.
	/// </summary>
	/// <param name="value">A ButtonTypes parameter.</param>
	public delegate void ButtonEvent(ButtonTypes buttonType);

	/// <summary>
	/// An event for when a button from the ButtonTypes enum is pressed.
	/// </summary>
	public event ButtonEvent OnButtonPressed;

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
	private Button _blockButton;

	[SerializeField]
	private Button _homeButton;

	[SerializeField]
	private Button _gridButton;

	[SerializeField]
	private GameObject _gridObject;

	[SerializeField]
	private Sprite _gridOffSprite;

	[SerializeField]
	private Sprite _gridOnSprite;

	private void Awake()
	{
		_blockButton.onClick.AddListener(delegate
		{
			OnButtonPressed?.Invoke(ButtonTypes.BlockButton);
		});

		_homeButton.onClick.AddListener(delegate
		{
			OnButtonPressed?.Invoke(ButtonTypes.HomeButton);
		});

		_gridButton.onClick.AddListener(delegate
		{
			OnButtonPressed?.Invoke(ButtonTypes.GridButton);
		});
	}

	/// <summary>
	/// Opens the code window.
	/// </summary>
	public void OpenCodeWindow()
	{
		WindowApi.OpenWindow(WindowTypes.CodeMenu);
		Game.MainCamera.Dragging.DisableDragging();
		Game.MainCamera.Zooming.DisableZooming();
	}

	/// <summary>
	/// Opens the main menu scene.
	/// </summary>
	public void OpenMainMenu()
	{
		LevelManager.LoadMainMenu();
	}
    
    /// <summary>
	/// Opens the the exit confirmation window.
	/// </summary>
	public void OpenExitConfirmation()
	{
        WindowApi.OpenWindow(WindowTypes.ExitConfirmation);
        Game.MainCamera.Dragging.DisableDragging();
        Game.MainCamera.Zooming.DisableZooming();
	}

	/// <summary>
	/// Enables a button from the ButtonTypes enum.
	/// </summary>
	/// <param name="buttonType">The type of button to enable.</param>
	public void EnableButton(ButtonTypes buttonType)
	{
		switch (buttonType)
		{
			case ButtonTypes.BlockButton:
				_blockButton.interactable = true;
				break;
			case ButtonTypes.HomeButton:
				_homeButton.interactable = true;
				break;
			case ButtonTypes.GridButton:
				_gridButton.interactable = true;
				break;
		}
	}

	/// <summary>
	/// Disables a button from the ButtonTypes enum.
	/// </summary>
	/// <param name="buttonType">The type of button to disable.</param>
	public void DisableButton(ButtonTypes buttonType)
	{
		switch (buttonType)
		{
			case ButtonTypes.BlockButton:
				_blockButton.interactable = false;
				break;
			case ButtonTypes.HomeButton:
				_homeButton.interactable = false;
				break;
			case ButtonTypes.GridButton:
				_gridButton.interactable = false;
				break;
		}
	}

	/// <summary>
	/// Highlights a button with an outline from the ButtonTypes enum.
	/// </summary>
	/// <param name="buttonType">The type of button to highlight.</param>
	public void HighlightButton(ButtonTypes buttonType)
	{
		Outline outline = null;

		switch (buttonType)
		{
			case ButtonTypes.BlockButton:
				outline = _blockButton.gameObject.AddComponent<Outline>();
				break;
			case ButtonTypes.HomeButton:
				outline = _homeButton.gameObject.AddComponent<Outline>();
				break;
			case ButtonTypes.GridButton:
				outline = _gridButton.gameObject.AddComponent<Outline>();
				break;
		}

		if (!outline)
			return;

		outline.effectColor = new Color(0, 0, 0, 0);
		outline.effectDistance = new Vector2(10, -10);
		outline.gameObject.AddComponent<OutlineEffect>();
	}

	/// <summary>
	/// Unhighlights a button with an outline from the ButtonTypes enum.
	/// </summary>
	/// <param name="buttonType">The type of button to unhighlight.</param>
	public void UnhighlightButton(ButtonTypes buttonType)
	{
		Outline outline = null;

		switch (buttonType)
		{
			case ButtonTypes.BlockButton:
				outline = _blockButton.gameObject.GetComponent<Outline>();
				break;
			case ButtonTypes.HomeButton:
				outline = _homeButton.gameObject.GetComponent<Outline>();
				break;
			case ButtonTypes.GridButton:
				outline = _gridButton.gameObject.GetComponent<Outline>();
				break;
		}

		if (!outline)
			return;

		OutlineEffect outlineEffect = outline.GetComponent<OutlineEffect>();

		if (!outlineEffect)
			return;

		outlineEffect.FadeOutAndDestroy();
	}

    /// <summary>
    /// Toggles the grid GameObject.
    /// </summary>
    public void ToggleGrid()
    {
        _gridObject.SetActive(!_gridObject.activeSelf);
        if (_gridButton.image.sprite == _gridOffSprite)
            _gridButton.image.sprite = _gridOnSprite;
        else
            _gridButton.image.sprite = _gridOffSprite;
    }
}