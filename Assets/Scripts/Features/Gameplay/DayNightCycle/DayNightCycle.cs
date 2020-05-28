using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Changes the light to day and night.
/// </summary>
[RequireComponent(typeof(Light))]
public class DayNightCycle : MonoBehaviour
{
	/// <summary>
	/// The current day of the world.
	/// </summary>
	public int CurrentDay { get; private set; }

	private Light _light;

	[SerializeField]
	private float _nightValue;

	[SerializeField]
	private float _dayValue;

	[SerializeField]
	private float _dayTime;

	[SerializeField]
	private float _nightTime;

	[SerializeField]
	private float _intensityIncreaseValue;

	[SerializeField]
	private string _dayText;

	[SerializeField]
	private Text _dayTextComponent;

	private void Awake()
	{
		_light = GetComponent<Light>();
	}

	private void Start()
	{
		CurrentDay = 1;
		StartCoroutine(NightRoutine());
	}

	private IEnumerator NightRoutine()
	{
		yield return new WaitForSeconds(_dayTime);

		while (true)
		{
			_light.intensity -= _intensityIncreaseValue;

			if (_light.intensity <= _nightValue)
			{
				_light.intensity = _nightValue;
				StartCoroutine(DayRoutine());
				break;
			}

			yield return new WaitForEndOfFrame();
		}
	}

	private IEnumerator DayRoutine()
	{
		yield return new WaitForSeconds(_nightTime);

		while (true)
		{
			_light.intensity += _intensityIncreaseValue;

			if (_light.intensity >= _dayValue)
			{
				_light.intensity = _dayValue;

				IncreaseDay();
				StartCoroutine(NightRoutine());
				break;
			}

			yield return new WaitForEndOfFrame();
		}
	}

	private void IncreaseDay()
	{
		CurrentDay++;

		_dayTextComponent.text = _dayText + CurrentDay;
	}
}