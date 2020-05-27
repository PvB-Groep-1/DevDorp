using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Creates a ProgrammableBlock when clicked.
/// </summary>
public class ProgrammableBlockCreator : MonoBehaviour, IPointerDownHandler
{
	[SerializeField]
	private ProgrammableBlock _programmableBlockPrefab;

	[SerializeField]
	private Transform _programmableBlockContainer;

	/// <summary>
	/// When the mouse clicks on this object.
	/// </summary>
	/// <param name="eventData">The current data for the pointer.</param>
	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.pointerId != -1)
			return;

		Instantiate(_programmableBlockPrefab, transform.position + new Vector3(Random.Range(-40, 40), 200, 0), Quaternion.identity, _programmableBlockContainer);
	}
}