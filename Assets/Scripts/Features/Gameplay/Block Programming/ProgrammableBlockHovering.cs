using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Mouse hovering for the ProgrammableBlock.
/// This class checks for when the mouse is hovering over the object.
/// </summary>
public class ProgrammableBlockHovering : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	[SerializeField]
	private ProgrammableBlock _programmableBlock;

	/// <summary>
	/// When the mouse clicks on this object.
	/// </summary>
	/// <param name="eventData">The current data for the pointer.</param>
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.pointerId != -2)
			return;

		Destroy(_programmableBlock.gameObject);
	}

	/// <summary>
	/// When mouse enters this object.
	/// </summary>
	/// <param name="eventData">The current data for the pointer.</param>
	public void OnPointerEnter(PointerEventData eventData)
	{
		_programmableBlock.Hover();
	}

	/// <summary>
	/// When the mouse leaves this object.
	/// </summary>
	/// <param name="eventData">The current data for the pointer.</param>
	public void OnPointerExit(PointerEventData eventData)
	{
		_programmableBlock.Unhover();
	}
}