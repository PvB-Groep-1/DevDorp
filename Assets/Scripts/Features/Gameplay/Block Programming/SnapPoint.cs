using UnityEngine;

/// <summary>
/// Represents a 2D snapping point.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class SnapPoint : MonoBehaviour
{
	/// <summary>
	/// The ProgrammableBlock this snapping point is attached to.
	/// </summary>
	public ProgrammableBlock ProgrammableBlock { get => _programmableBlock; }

	/// <summary>
	/// Another snapping point that this snapping point is attached to.
	/// </summary>
	public SnapPoint ConnectedSnapPoint { get; set; }

	[SerializeField]
	private ProgrammableBlock _programmableBlock;

	private void Awake()
	{
		_programmableBlock.OnReleaseSnap += () =>
		{
			if (!ConnectedSnapPoint)
				return;

			ConnectedSnapPoint.ConnectedSnapPoint = null;

			if (!ConnectedSnapPoint.ProgrammableBlock.SnappingPoints.HasConnection())
				ConnectedSnapPoint.ProgrammableBlock.ReleaseSnap();

			ConnectedSnapPoint = null;
		};
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "SnapPoint")
			return;

		if (!ProgrammableBlock.IsLockedToMouse)
			return;

		SnapPoint otherSnapPoint = other.GetComponent<SnapPoint>();

		if (!otherSnapPoint)
			return;

		if (otherSnapPoint.ConnectedSnapPoint)
			return;

		if (ProgrammableBlock.SnapTo(this, otherSnapPoint))
		{
			ConnectedSnapPoint = otherSnapPoint;
			otherSnapPoint.ConnectedSnapPoint = this;
			otherSnapPoint.ProgrammableBlock.SnapTo(otherSnapPoint, this);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		if (ConnectedSnapPoint)
			Gizmos.DrawSphere(transform.position, 5f);
	}
}