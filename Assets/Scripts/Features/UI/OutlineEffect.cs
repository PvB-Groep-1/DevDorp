using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A fade in and out effect for the Outline component.
/// </summary>
[RequireComponent(typeof(Outline))]
public sealed class OutlineEffect : MonoBehaviour
{
	private Outline _outline;
	private float _startAlpha;
	private float _targetAlpha;
	private float _fadeTime;
	private float _currentTime;
	private bool _destroyAfterFadeOut = false;

	/// <summary>
	/// The time in seconds to fade the outline in.
	/// </summary>
	public float fadeInTime = 1f;

	/// <summary>
	/// The time in seconds to fade the outline out.
	/// </summary>
	public float fadeOutTime = 2f;

	private void Awake()
	{
		_outline = GetComponent<Outline>();
	}

	private void Start()
	{
		StartFadeIn();
	}

	private void Update()
	{
		Color newColor = _outline.effectColor;

		_currentTime += Time.deltaTime / _fadeTime;
		newColor.a = Mathf.Lerp(_startAlpha, _targetAlpha, _currentTime);

		_outline.effectColor = newColor;

		if (_outline.effectColor.a <= 0 && _destroyAfterFadeOut)
		{
			Destroy(this);
			Destroy(_outline);
		}

		else if (_outline.effectColor.a >= 1)
			StartFadeOut();

		else if (_outline.effectColor.a <= 0)
			StartFadeIn();
	}

	private void StartFadeIn()
	{
		_startAlpha = 0;
		_targetAlpha = 1;
		_fadeTime = fadeInTime;
		_currentTime = 0;
	}

	private void StartFadeOut()
	{
		_startAlpha = 1;
		_targetAlpha = 0;
		_fadeTime = fadeOutTime;
		_currentTime = 0;
	}

	/// <summary>
	/// Fades the outline out and destroys it.
	/// </summary>
	public void FadeOutAndDestroy()
	{
		_destroyAfterFadeOut = true;

		StartFadeOut();
	}
}