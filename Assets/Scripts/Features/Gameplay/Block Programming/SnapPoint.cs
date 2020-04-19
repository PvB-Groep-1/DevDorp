using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SnapPoint : MonoBehaviour
{
	public ProgrammableBlock ProgrammableBlock { get => _programmableBlock; }

	[SerializeField]
	private ProgrammableBlock _programmableBlock;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "SnapPoint")
			return;

		if (!ProgrammableBlock.IsLockedToMouse)
			return;

		ProgrammableBlock.SnapTo(this, other.GetComponent<SnapPoint>());
	}
}