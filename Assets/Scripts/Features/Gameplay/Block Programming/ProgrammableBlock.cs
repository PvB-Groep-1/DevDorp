using CM.Events;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProgrammableBlock : MonoBehaviour, IPointerDownHandler
{
	public static ProgrammableBlock CurrentlyMovingProgrammableBlock { get; private set; }
	public SnapPoints SnappingPoints => _snapPoints;
	public bool IsLockedToMouse => _lockToMouse;

	private Vector3 _mouseSnapPoint;
	private bool _isSnapped = false;

	public enum Direction
	{
		Left,
		Right,
		Up,
		Down
	}

	public event SimpleEvent OnReleaseSnap;

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

	private bool CanSnapDirection(Direction direction1, Direction direction2)
	{
		// Left and Right Direction check.
		if (direction1 == Direction.Left && direction2 == Direction.Right)
			return true;

		// Right and Left Direction check.
		else if (direction1 == Direction.Right && direction2 == Direction.Left)
			return true;

		// Up and Down Direction check.
		else if (direction1 == Direction.Up && direction2 == Direction.Down)
			return true;

		// Down and Up Direction check.
		else if (direction1 == Direction.Down && direction2 == Direction.Up)
			return true;

		return false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (tag == "StartNode")
			return;

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

	public bool SnapTo(SnapPoint referencePoint, SnapPoint snapPoint)
	{
		if (!CanSnapDirection(_snapPoints.GetSnapPointData(referencePoint).direction, snapPoint.ProgrammableBlock.SnappingPoints.GetSnapPointData(snapPoint).direction))
			return false;

		Vector3 referenceDistance = referencePoint.transform.position - transform.position;

		transform.position = snapPoint.transform.position - referenceDistance;

		_mouseSnapPoint = Input.mousePosition;
		_isSnapped = true;

		return true;
	}

	public void ReleaseSnap()
	{
		_isSnapped = false;

		OnReleaseSnap?.Invoke();
	}

	#region Editor Region

	private void OnValidate()
	{
		_snapPoints.SetDefaultDirections();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;

		if (_isSnapped && IsLockedToMouse)
			Gizmos.DrawWireSphere(_mouseSnapPoint, _snapThreshold);
	}

	#endregion

	[System.Serializable]
	public struct SnapPoints
	{
		public SnapPointData left;
		public SnapPointData right;
		public SnapPointData up;
		public SnapPointData down;

		public void SetDefaultDirections()
		{
			left.direction = Direction.Left;
			right.direction = Direction.Right;
			up.direction = Direction.Up;
			down.direction = Direction.Down;
		}

		public SnapPointData GetSnapPointData(SnapPoint snapPoint)
		{
			if (snapPoint == left.snapPoint)
				return left;

			else if (snapPoint == right.snapPoint)
				return right;

			else if (snapPoint == up.snapPoint)
				return up;

			else if(snapPoint == down.snapPoint)
				return down;

			return left;
		}

		public SnapPointData GetSnapPointData(Direction direction)
		{
			switch (direction)
			{
				case Direction.Left:
					return left;
				case Direction.Right:
					return right;
				case Direction.Up:
					return up;
				case Direction.Down:
					return down;
			}

			return left;
		}
	}

	[System.Serializable]
	public struct SnapPointData
	{
		public Direction direction;
		public SnapPoint snapPoint;
	}
}