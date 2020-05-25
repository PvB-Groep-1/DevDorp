﻿using System.Collections;
using UnityEngine;

/// <summary>
/// Everything that needs to happen at the start of the game.
/// </summary>
public sealed class StartGame : MonoBehaviour
{
	private int _cameraMovedFrames = 0;
	private int _cameraZoomedFrames = 0;

	[SerializeField]
	private bool _showTutorial = true;

	[SerializeField]
	private float _tutorialDelayInSeconds = 5f;

	[SerializeField]
	private float _popupFadeInTime = 2f;

	[SerializeField]
	private float _popupFadeOutTime = 2f;

	[Tooltip("The time in seconds as a delay after fully fading out and destroying the popup.")]
	[SerializeField]
	private float _popupFinishDelay = 3f;

	private void Awake()
	{
		if (_showTutorial)
			InitializeTutorial();
	}

	private void Start()
	{
		WorldApi.LoadWorld();
	}

	private void InitializeTutorial()
	{
		WorldApi.OnWorldLoad += () =>
		{
			Game.BottomBarWindow.DisableButton(BottomBarWindow.ButtonTypes.BlockButton);
			Game.BottomBarWindow.DisableButton(BottomBarWindow.ButtonTypes.HomeButton);
			Game.MainCamera.Dragging.DisableDragging();
			Game.MainCamera.Zooming.DisableZooming();
			StartCoroutine(StartTutorialRoutine(_tutorialDelayInSeconds));
		};
	}

	private IEnumerator StartTutorialRoutine(float time)
	{
		yield return new WaitForSeconds(time);

		Popup popup = Popup.Create("WIL JE MEER VAN DE WERELD ZIEN? HOUD DE LINKERMUISKNOP INGEDRUKT EN SLEEP DE MUIS OVER HET SCHERM", _popupFadeInTime, _popupFadeOutTime, _popupFinishDelay);

		popup.OnFullyVisible += () =>
		{
			Game.MainCamera.Dragging.EnableDragging();
			Game.MainCamera.Dragging.OnDragging += CameraDraggingCheck;
		};

		popup.OnFinish += () =>
		{
			popup = Popup.Create("JE KAN DE WERELD VAN DICHTBIJ BEKIJKEN. GEBRUIK HET WIELTJE OP JE MUIS", _popupFadeInTime, _popupFadeOutTime, _popupFinishDelay);

			popup.OnFullyVisible += () =>
			{
				Game.MainCamera.Zooming.EnableZooming();
				Game.MainCamera.Zooming.OnZooming += CameraZoomingCheck;
			};

			popup.OnFinish += () =>
			{
				popup = Popup.Create("ZIE JE HET PUZZELSTUKJE ONDERAAN HET SCHERM? KLIK OP HET PUZZELSTUKJE", _popupFadeInTime, _popupFadeOutTime, _popupFinishDelay);

				popup.OnFullyVisible += () =>
				{
					Game.BottomBarWindow.EnableButton(BottomBarWindow.ButtonTypes.BlockButton);
					Game.BottomBarWindow.HighlightButton(BottomBarWindow.ButtonTypes.BlockButton);
					Game.BottomBarWindow.OnButtonPressed += PressedBlockButtonCheck;
				};
			};
		};
	}

	private void CameraDraggingCheck()
	{
		_cameraMovedFrames++;

		if (_cameraMovedFrames >= 200)
		{
			Popup.activePopup.FadeOut();
			Game.MainCamera.Dragging.OnDragging -= CameraDraggingCheck;
		}
	}

	private void CameraZoomingCheck()
	{
		_cameraZoomedFrames++;

		if (_cameraZoomedFrames >= 20)
		{
			Popup.activePopup.FadeOut();
			Game.MainCamera.Zooming.OnZooming -= CameraZoomingCheck;
		}
	}

	private void PressedBlockButtonCheck(BottomBarWindow.ButtonTypes buttonType)
	{
		if (buttonType != BottomBarWindow.ButtonTypes.BlockButton)
			return;

		Popup.activePopup.FadeOut();
		Game.BottomBarWindow.UnhighlightButton(BottomBarWindow.ButtonTypes.BlockButton);
		Game.BottomBarWindow.OnButtonPressed -= PressedBlockButtonCheck;
	}
}