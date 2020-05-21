using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class OutlineEffect : MonoBehaviour
{
	private Outline _outline;
	private float _startAlpha;
	private float _targetAlpha;
	private float _fadeTime;
	private float _currentTime;
	private bool _destroyAfterFadeOut = false;

	public float fadeInTime = 1f;
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
			Destroy(_outline);
			Destroy(this);
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

	public void FadeOutAndDestroy()
	{
		_destroyAfterFadeOut = true;

		StartFadeOut();
	}
}