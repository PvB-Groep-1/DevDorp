using UnityEngine;
using UnityEngine.EventSystems;

public class ProgrammableBlock : MonoBehaviour, IPointerDownHandler
{
	public bool IsLockedToMouse => _lockToMouse;

	private Vector3 _mouseSnapPoint;

	private bool _isSnapped = false;

	[SerializeField]
	private bool _lockToMouse = false;

	[SerializeField]
	private float _snapThreshold = 65f;

	private void Update()
	{
		if (!_lockToMouse)
			return;

		if (_isSnapped)
		{
			if (Vector3.Distance(_mouseSnapPoint, Input.mousePosition) > _snapThreshold)
				ReleaseSnap();

			return;
		}

		transform.position = Input.mousePosition;
	}

	public void SnapTo(Vector3 referencePoint, Vector3 snapPoint)
	{
		Vector3 referenceDistance = referencePoint - transform.position;

		transform.position = snapPoint - referenceDistance;

		_mouseSnapPoint = Input.mousePosition;

		_isSnapped = true;
	}

	public void ReleaseSnap()
	{
		_isSnapped = false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_lockToMouse = !_lockToMouse;

		_mouseSnapPoint = transform.position;

		if (_lockToMouse)
		{
			/*
				Move the transform to the end of the local transform list.
				This object will be drawn on top of other objects.
			*/
			transform.SetAsLastSibling();

			ReleaseSnap();
		}
	}
}