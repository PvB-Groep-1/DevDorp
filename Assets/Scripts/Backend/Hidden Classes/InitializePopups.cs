using UnityEngine;

/// <summary>
/// Sends all popup screens as type GameObject to the Popup class.
/// </summary>
public class InitializePopups : MonoBehaviour
{
	[SerializeField]
	private GameObject _popupPrefab;

	private void Start()
	{
		Popup.popupPrefab = _popupPrefab;
	}
}