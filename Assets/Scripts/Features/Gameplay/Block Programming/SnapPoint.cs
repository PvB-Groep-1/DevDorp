using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SnapPoint : MonoBehaviour
{
	public ProgrammableBlock ProgrammableBlock { get => _programmableBlock; }
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

		if (ProgrammableBlock.SnapTo(this, otherSnapPoint))
		{
			ConnectedSnapPoint = otherSnapPoint;
			otherSnapPoint.ConnectedSnapPoint = this;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		if (ConnectedSnapPoint)
			Gizmos.DrawSphere(transform.position, 5f);
	}
}