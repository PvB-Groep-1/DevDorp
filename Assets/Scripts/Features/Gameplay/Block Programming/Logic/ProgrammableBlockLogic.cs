using UnityEngine;

/// <summary>
/// Represents logic for a ProgrammableBlock.
/// </summary>
[RequireComponent(typeof(ProgrammableBlock))]
public abstract class ProgrammableBlockLogic : MonoBehaviour
{
	protected ProgrammableBlock programmableBlock;

	private void Awake()
	{
		programmableBlock = GetComponent<ProgrammableBlock>();
	}
}