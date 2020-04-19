using UnityEngine;
using UnityEngine.EventSystems;

public class ProgrammableBlock : MonoBehaviour, IPointerDownHandler
{
	public static ProgrammableBlock CurrentlyMovingProgrammableBlock { get; private set; }
	public SnapPoints SnappingPoints => _snapPoints;
	public bool IsLockedToMouse => _lockToMouse;

	private Vector3 _mouseSnapPoint;
	private bool _isSnapped = false;

	[SerializeField]
	private SnapPoints _snapPoints;

	[SerializeField]
	private bool _lockToMouse = false;

	[SerializeField]
	private float _snapThreshold = 65f;

	private void Update()
	{
		if (!_lockToMouse)
			return;

		if (CurrentlyMovingProgrammableBlock != this)
			return;

		if (_isSnapped)
		{
			if (Vector3.Distance(_mouseSnapPoint, Input.mousePosition) > _snapThreshold)
				ReleaseSnap();

			return;
		}

		transform.position = Input.mousePosition;
	}

	private bool CanSnap(SnapPoint snapPoint1, SnapPoint snapPoint2)
	{
		// Check for left and right snapping points.
		if (
			(snapPoint1 == _snapPoints.left && snapPoint2 == snapPoint2.ProgrammableBlock.SnappingPoints.right) ||
			(snapPoint1 == _snapPoints.right && snapPoint2 == snapPoint2.ProgrammableBlock.SnappingPoints.left)
		)
			return true;

		// Check for up and down snapping points.
		if (
			(snapPoint1 == _snapPoints.up && snapPoint2 == snapPoint2.ProgrammableBlock.SnappingPoints.up) ||
			(snapPoint1 == _snapPoints.down && snapPoint2 == snapPoint2.ProgrammableBlock.SnappingPoints.down)
		)
			return true;

		return false;
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

			CurrentlyMovingProgrammableBlock = this;
		}
		else
		{
			CurrentlyMovingProgrammableBlock = null;
		}
	}

	public void SnapTo(SnapPoint referencePoint, SnapPoint snapPoint)
	{
		if (!CanSnap(referencePoint, snapPoint))
			return;

		Vector3 referenceDistance = referencePoint.transform.position - transform.position;

		transform.position = snapPoint.transform.position - referenceDistance;

		_mouseSnapPoint = Input.mousePosition;
		_isSnapped = true;
	}

	public void ReleaseSnap()
	{
		_isSnapped = false;
	}

	[System.Serializable]
	public struct SnapPoints
	{
		public SnapPoint left;
		public SnapPoint right;
		public SnapPoint up;
		public SnapPoint down;
	}
}