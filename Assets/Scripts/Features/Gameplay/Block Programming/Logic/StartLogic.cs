using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Represents the logic for the starting ProgrammableBlock.
/// </summary>
public sealed class StartLogic : ExecutionLogic
{
	private ProgrammableBlock _currentProgrammableBlock;
	private bool _isRunning = false;

	/// <summary>
	/// An event for when the start logic finishes running.
	/// </summary>
	public UnityEvent OnFinish;

	private void AddOutline(ProgrammableBlock programmableBlock)
	{
		Outline outline = programmableBlock.Image.gameObject.AddComponent<Outline>();
		outline.effectColor = Color.black;
		outline.effectDistance = new Vector2(3, -3);
	}

	private void RemoveOutline(ProgrammableBlock programmableBlock)
	{
		Destroy(programmableBlock.Image.GetComponent<Outline>());
	}

	private IEnumerator ExecutionRoutine()
	{
		while (true)
		{
			AddOutline(_currentProgrammableBlock);
			DecisionLogic logic = _currentProgrammableBlock.GetComponent<DecisionLogic>();

			if (!logic)
				yield return null;

			yield return new WaitForSeconds(1);

			RemoveOutline(_currentProgrammableBlock);

			if (logic.Decide())
			{

				#region Execute Actions

				ProgrammableBlock tempProgrammableBlock = _currentProgrammableBlock;
				ExecutionLogic executionLogic = null;

				while (true)
				{
					tempProgrammableBlock = tempProgrammableBlock.GetConnectedProgrammableBlock(ProgrammableBlock.Direction.Right);

					if (!tempProgrammableBlock)
						break;

					executionLogic = tempProgrammableBlock.GetComponent<ExecutionLogic>();
					AddOutline(tempProgrammableBlock);

					if (!executionLogic)
					{
						yield return new WaitForSeconds(1);
						RemoveOutline(tempProgrammableBlock);
						continue;
					}

					executionLogic.Execute();

					yield return new WaitForSeconds(1);

					RemoveOutline(tempProgrammableBlock);
				}

				#endregion

			}

			_currentProgrammableBlock = _currentProgrammableBlock.GetConnectedProgrammableBlock(ProgrammableBlock.Direction.Down);

			if (!_currentProgrammableBlock)
				break;
		}

		_isRunning = false;
		OnFinish.Invoke();

		yield return null;
	}

	/// <summary>
	/// Executes all connected blocks.
	/// </summary>
	public override void Execute()
	{
		if (_isRunning)
			return;

		_currentProgrammableBlock = programmableBlock.GetConnectedProgrammableBlock(ProgrammableBlock.Direction.Down);

		if (!_currentProgrammableBlock)
		{
			OnFinish.Invoke();
			return;
		}

		_isRunning = true;
		StartCoroutine(ExecutionRoutine());
	}
}