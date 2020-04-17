using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SnapPoint : MonoBehaviour
{
	[SerializeField]
	private ProgrammableBlock _programmableBlock;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "SnapPoint")
			return;

		if (!_programmableBlock.IsLockedToMouse)
			return;

		_programmableBlock.SnapTo(transform.position, other.transform.position);
	}
}