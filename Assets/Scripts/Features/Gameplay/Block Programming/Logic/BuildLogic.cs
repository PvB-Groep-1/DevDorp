using UnityEngine;
using UnityEngine.UI;

public class BuildLogic : ExecutionLogic
{
	[SerializeField]
	private Dropdown _buildDropdown;

	public override void Execute()
	{
		Debug.Log("test");
	}

	public void SetValues()
	{
		switch (_buildDropdown.itemText.text)
		{
			case "Huis":

				break;
		}
	}
}