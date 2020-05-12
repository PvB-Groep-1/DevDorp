using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// Represents building logic.
/// </summary>
public sealed class BuildLogic : ExecutionLogic
{
	[SerializeField]
	private Dropdown _buildDropdown;

	[SerializeField]
	private XYLogic _XYLogic;

	[SerializeField]
	private BuildingTypes _buildingType;

	/// <summary>
	/// Executes the building logic.
	/// </summary>
	public override void Execute()
	{
		if (_XYLogic.inputFieldX.text == "" || _XYLogic.inputFieldY.text == "")
			return;

		SpawnObjectCommand command = new SpawnObjectCommand(Properties.buildings[(int)_buildingType], -15 + _XYLogic.x * 9, 5 + _XYLogic.y * 9);
		command.Execute();
	}

	/// <summary>
	/// Saves values for given input.
	/// </summary>
	public void SetValues()
	{
		switch (_buildDropdown.captionText.text)
		{
			case "Huis 1":
				_buildingType = BuildingTypes.House1;
				break;
			case "Huis 2":
				_buildingType = BuildingTypes.House2;
				break;
			case "Huis 3":
				_buildingType = BuildingTypes.House5;
				break;
			case "Huis 4":
				_buildingType = BuildingTypes.House6;
				break;
			case "Huis 5":
				_buildingType = BuildingTypes.House7;
				break;
			case "Huis 6":
				_buildingType = BuildingTypes.House8;
				break;
			case "Bakker":
				_buildingType = BuildingTypes.Bakery;
				break;
			case "Flat":
				_buildingType = BuildingTypes.Flat;
				break;
			case "Winkel":
				_buildingType = BuildingTypes.Store;
				break;
			case "Stadhuis":
				_buildingType = BuildingTypes.Townhall;
				break;
		}

		_XYLogic.SetValues();
	}
}