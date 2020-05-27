using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

	[SerializeField]
	private float _popupCutsceneLifetime = 10f;

	[SerializeField]
	private Canvas _canvas;

	[SerializeField]
	private Image _cutsceneUIPrefab;

	[SerializeField]
	private Sprite _cutsceneImage1;

	[SerializeField]
	private Sprite _cutsceneImage2;

	[SerializeField]
	private Sprite _cutsceneImage3;

	[SerializeField]
	private Sprite _characterImage1;

	[SerializeField]
	private Sprite _characterImage2;

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
			Game.BottomBarWindow.DisableButton(BottomBarWindow.ButtonTypes.GridButton);
			Game.MainCamera.Dragging.DisableDragging();
			Game.MainCamera.Zooming.DisableZooming();
			StartCutscene();
		};
	}

	private void StartCutscene()
	{
		Image cutsceneUI = Instantiate(_cutsceneUIPrefab, _canvas.transform);
		cutsceneUI.sprite = _cutsceneImage1;

		Popup popup = Popup.Create("Ik reis al mijn hele leven de wereld door, altijd op zoek naar nieuwe plekken, gebouwen en natuurlijk ook nieuwe vrienden! Tot ik op een dag in DevDorp aankwam.", _popupCutsceneLifetime, _popupFadeInTime, _popupFadeOutTime, 1f);
		popup.ShowCharacter(false);
		popup.ShowImage(false);
		popup.SetPosition(new Vector3(1500, 100, 0));

		popup.OnFinish += () =>
		{
			cutsceneUI.sprite = _cutsceneImage2;

			popup = Popup.Create("DevDorp was klein en had weinig bewoners maar gezellig was het wel. Ze bouwden daar door het gebruik van programmeren. Zo iets interessants had ik nog nooit gezien!", _popupCutsceneLifetime, _popupFadeInTime, _popupFadeOutTime, 1f);
			popup.ShowCharacter(false);
			popup.ShowImage(false);
			popup.SetPosition(new Vector3(1500, 100, 0));

			popup.OnFinish += () =>
			{
				cutsceneUI.sprite = _cutsceneImage3;

				popup = Popup.Create("Ik besloot dus om te blijven om iedereen in DevDorp te helpen met het uitbreiden van hun leuke dorpje. Het is al een stuk opgevrolijkt maar we kunnen nog wel een extra handje gebruiken, help je mee?", _popupCutsceneLifetime, _popupFadeInTime, _popupFadeOutTime, 1f);
				popup.ShowCharacter(false);
				popup.ShowImage(false);
				popup.SetPosition(new Vector3(1500, 100, 0));

				popup.OnFinish += () =>
				{
					StartCoroutine(CutsceneFadeOut(cutsceneUI));
					StartCoroutine(StartTutorialRoutine(_tutorialDelayInSeconds));
				};
			};
		};
	}

	private IEnumerator StartTutorialRoutine(float time)
	{
		yield return new WaitForSeconds(time);

		Popup popup = Popup.Create("Ik wil mijn dorp groter maken, wil je mij helpen? Dan zal ik je uitleggen wat je moet doen.", 10f, _popupFadeInTime, _popupFadeOutTime, _popupFinishDelay);
		popup.SetCharacter(_characterImage2);

		popup.OnFinish += () =>
		{
			popup = Popup.Create("Wil je meer van de wereld zien? Houd de linkermuisknop ingedrukt en sleep over het scherm.", _popupFadeInTime, _popupFadeOutTime, _popupFinishDelay);

			popup.OnFullyVisible += () =>
			{
				Game.MainCamera.Dragging.EnableDragging();
				Game.MainCamera.Dragging.OnDragging += CameraDraggingCheck;
			};

			popup.OnFinish += () =>
			{
				popup = Popup.Create("Je kan de wereld van dichtbij bekijken. Gebruik het wieltje op je muis.", _popupFadeInTime, _popupFadeOutTime, 1f);

				popup.OnFullyVisible += () =>
				{
					Game.MainCamera.Zooming.EnableZooming();
					Game.MainCamera.Zooming.OnZooming += CameraZoomingCheck;
				};

				popup.OnFinish += () =>
				{
					popup = Popup.Create("Wat heb je dat snel door!", 5f, _popupFadeInTime, _popupFadeOutTime, 1f);

					popup.OnFinish += () =>
					{
						popup = Popup.Create("Zie je het puzzelstukje onderaan het scherm? Klik op het puzzelstukje.", _popupFadeInTime, _popupFadeOutTime, _popupFinishDelay);

						popup.OnFullyVisible += () =>
						{
							Game.BottomBarWindow.EnableButton(BottomBarWindow.ButtonTypes.BlockButton);
							Game.BottomBarWindow.HighlightButton(BottomBarWindow.ButtonTypes.BlockButton);
							Game.BottomBarWindow.OnButtonPressed += PressedBlockButtonCheck;
						};

						popup.OnFinish += () =>
						{
							Game.BottomBarWindow.EnableButton(BottomBarWindow.ButtonTypes.GridButton);
							Game.BottomBarWindow.EnableButton(BottomBarWindow.ButtonTypes.HomeButton);
						};
					};
				};
			};
		};
	}

	private IEnumerator CutsceneFadeOut(Image cutsceneUI)
	{
		while (cutsceneUI.color.a > 0)
		{
			Color newColor = cutsceneUI.color;

			newColor.a -= 0.01f;

			cutsceneUI.color = newColor;

			if (cutsceneUI.color.a <= 0)
			{
				Destroy(cutsceneUI.gameObject);
				break;
			}

			yield return new WaitForEndOfFrame();
		}

		yield return null;
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