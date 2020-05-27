using CM.Events;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents an image with a message.
/// </summary>
public class Popup : MonoBehaviour
{
	/// <summary>
	/// The text to display.
	/// </summary>
	public string Text
	{
		get
		{
			return _textComponent.text;
		}
		set
		{
			_textComponent.text = value;
		}
	}

	private float _lifetime = -1;
	private float _startAlpha;
	private float _targetAlpha = -1;
	private float _fadeTime;
	private float _currentFadeTime = -1;

	/// <summary>
	/// The prefab used for creating new popups.
	/// </summary>
	public static GameObject popupPrefab;

	/// <summary>
	/// The current active popup shown on the screen;
	/// </summary>
	public static Popup activePopup;

	/// <summary>
	/// An event for when the popup is finished.
	/// </summary>
	public SimpleEvent OnFinish;

	/// <summary>
	/// An event for when the popup is fully visible for the first time.
	/// </summary>
	public SimpleEvent OnFullyVisible;

	/// <summary>
	/// The time in seconds to fade the popup in.
	/// </summary>
	public float fadeInTime = -1;

	/// <summary>
	/// The time in seconds to fade the popup out.
	/// </summary>
	public float fadeOutTime = -1;

	/// <summary>
	/// The time in seconds as a delay after fully fading out and destroying the popup.
	/// </summary>
	public float finishDelay = -1;

	[SerializeField]
	private Text _textComponent;

	[SerializeField]
	private Image _imageComponent;

	[SerializeField]
	private Image _characterImageComponent;

	private void Update()
	{
		if (_currentFadeTime != -1 && _targetAlpha == 1)
			FadingIn();

		if (_currentFadeTime != -1 && _targetAlpha == 0)
			FadingOut();

		if (_textComponent.color.a >= 1 && _imageComponent.color.a >= 1 && _lifetime != -1)
			DecreaseLifetime();

		if (_textComponent.color.a <= 0 && _imageComponent.color.a <= 0 && _lifetime == -1 && finishDelay > 0)
			DecreaseFinishDelay();

		else if (finishDelay <= 0)
		{
			OnFinish?.Invoke();
			Destroy(gameObject);
		}
		
		if (_lifetime <= 0 && _lifetime != -1)
		{
			FadeOut();
			_lifetime = -1;
		}
	}

	private void FadingIn()
	{
		Color newTextColor = _textComponent.color;
		Color newImageColor = _imageComponent.color;
		Color newCharacterImageColor = _characterImageComponent.color;

		_currentFadeTime += Time.deltaTime / _fadeTime;

		newTextColor.a = Mathf.Lerp(_startAlpha, _targetAlpha, _currentFadeTime);
		newImageColor.a = Mathf.Lerp(_startAlpha, _targetAlpha, _currentFadeTime);
		newCharacterImageColor.a = Mathf.Lerp(_startAlpha, _targetAlpha, _currentFadeTime);

		if (_textComponent.color.a >= 1 && _imageComponent.color.a >= 1 && _characterImageComponent.color.a >= 1)
		{
			newTextColor.a = 1;
			newImageColor.a = 1;
			newCharacterImageColor.a = 1;
			_currentFadeTime = -1;
			_targetAlpha = -1;
			OnFullyVisible?.Invoke();
		}

		_textComponent.color = newTextColor;
		_imageComponent.color = newImageColor;
		_characterImageComponent.color = newCharacterImageColor;
	}

	private void FadingOut()
	{
		Color newTextColor = _textComponent.color;
		Color newImageColor = _imageComponent.color;
		Color newCharacterImageColor = _characterImageComponent.color;

		_currentFadeTime += Time.deltaTime / _fadeTime;

		newTextColor.a = Mathf.Lerp(_startAlpha, _targetAlpha, _currentFadeTime);
		newImageColor.a = Mathf.Lerp(_startAlpha, _targetAlpha, _currentFadeTime);
		newCharacterImageColor.a = Mathf.Lerp(_startAlpha, _targetAlpha, _currentFadeTime);

		if (_textComponent.color.a <= 0 && _imageComponent.color.a <= 0 && _characterImageComponent.color.a <= 0)
		{
			newTextColor.a = 0;
			newImageColor.a = 0;
			newCharacterImageColor.a = 0;
			_currentFadeTime = -1;
			_targetAlpha = -1;
		}

		_textComponent.color = newTextColor;
		_imageComponent.color = newImageColor;
		_characterImageComponent.color = newCharacterImageColor;
	}

	private void DecreaseLifetime()
	{
		_lifetime -= Time.deltaTime;
	}

	private void DecreaseFinishDelay()
	{
		finishDelay -= Time.deltaTime;
	}

	/// <summary>
	/// Creates a new popup screen.
	/// </summary>
	/// <param name="text">The text to display in the popup.</param>
	/// <param name="finishDelay">The time in seconds as a delay after fully fading out and destroying the popup.</param>
	public static Popup Create(string text, float finishDelay = 0)
	{
		Popup popup = Instantiate(popupPrefab, FindObjectOfType<Canvas>().transform).GetComponent<Popup>();

		popup.Text = text;
		popup.finishDelay = finishDelay;

		activePopup = popup;

		return popup;
	}

	/// <summary>
	/// Creates a new popup screen.
	/// </summary>
	/// <param name="text">The text to display in the popup.</param>
	/// <param name="lifetime">The time in seconds that the popup is shown.</param>
	/// <param name="finishDelay">The time in seconds as a delay after fully fading out and destroying the popup.</param>
	public static Popup Create(string text, float lifetime, float finishDelay = 0)
	{
		Popup popup = Create(text, finishDelay);

		popup.SetLifetime(lifetime);

		return popup;
	}

	/// <summary>
	/// Creates a new popup screen and fades it in.
	/// </summary>
	/// <param name="text">The text to display in the popup.</param>
	/// <param name="fadeInTime">The time in seconds to fade the popup in.</param>
	/// <param name="fadeOutTime">The time in seconds to fade the popup out.</param>
	/// <param name="finishDelay">The time in seconds as a delay after fully fading out and destroying the popup.</param>
	public static Popup Create(string text, float fadeInTime, float fadeOutTime, float finishDelay = 0)
	{
		Popup popup = Create(text, finishDelay);

		popup.fadeInTime = fadeInTime;
		popup.fadeOutTime = fadeOutTime;
		popup.FadeIn();

		return popup;
	}

	/// <summary>
	/// Creates a new popup screen and fades it in.
	/// </summary>
	/// <param name="text">The text to display in the popup.</param>
	/// <param name="lifetime">The time in seconds that the popup is shown.</param>
	/// <param name="fadeInTime">The time in seconds to fade the popup in.</param>
	/// <param name="fadeOutTime">The time in seconds to fade the popup out.</param>
	/// <param name="finishDelay">The time in seconds as a delay after fully fading out and destroying the popup.</param>
	public static Popup Create(string text, float lifetime, float fadeInTime, float fadeOutTime, float finishDelay = 0)
	{
		Popup popup = Create(text, lifetime, finishDelay);

		popup.fadeInTime = fadeInTime;
		popup.fadeOutTime = fadeOutTime;
		popup.FadeIn();

		return popup;
	}

	/// <summary>
	/// Sets the new lifetime value.
	/// </summary>
	/// <param name="lifetime">The time in seconds that the popup is shown.</param>
	public void SetLifetime(float lifetime)
	{
		_lifetime = lifetime;
	}

	/// <summary>
	/// Fades the popup in.
	/// </summary>
	public void FadeIn()
	{
		_startAlpha = 0;
		_targetAlpha = 1;
		_fadeTime = fadeInTime;
		_currentFadeTime = 0;

		Color newTextColor = _textComponent.color;
		Color newImageColor = _imageComponent.color;
		Color newCharacterImageColor = _characterImageComponent.color;

		newTextColor.a = 0;
		newImageColor.a = 0;
		newCharacterImageColor.a = 0;

		_textComponent.color = newTextColor;
		_imageComponent.color = newImageColor;
		_characterImageComponent.color = newCharacterImageColor;
	}

	/// <summary>
	/// Fades the popup out.
	/// </summary>
	public void FadeOut()
	{
		_startAlpha = 1;
		_targetAlpha = 0;
		_fadeTime = fadeOutTime;
		_currentFadeTime = 0;
	}

	/// <summary>
	/// Show or hide the character image.
	/// </summary>
	/// <param name="value">True if the character needs to be shown.</param>
	public void ShowCharacter(bool value)
	{
		_characterImageComponent.gameObject.SetActive(value);
	}

	/// <summary>
	/// Show or hide the background image.
	/// </summary>
	/// <param name="value">True if the image needs to be shown.</param>
	public void ShowImage(bool value)
	{
		_imageComponent.gameObject.SetActive(value);
	}

	/// <summary>
	/// Sets a new position for the popup window.
	/// </summary>
	/// <param name="newPosition">The new position for the popup window.</param>
	public void SetPosition(Vector3 newPosition)
	{
		gameObject.transform.position = newPosition;
	}

	/// <summary>
	/// Sets a new character sprite.
	/// </summary>
	/// <param name="sprite">The new character sprite.</param>
	public void SetCharacter(Sprite sprite)
	{
		_characterImageComponent.sprite = sprite;
	}
}