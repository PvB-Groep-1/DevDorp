using System;
using UnityEngine;

/// <summary>
/// Sends all buildings as type GameObject to the Properties class.
/// </summary>
public class InitializeBuildings : MonoBehaviour
{
	[SerializeField] private Building[] _buildings;

	private void Awake()
	{
		bool undefinedFields = false;
		bool containsDuplicateBuildingTypes = false;

		Properties.buildings = new GameObject[Enum.GetNames(typeof(BuildingTypes)).Length];

		// Buildings array check
		for (int i = 0; i < _buildings.Length; i++)
		{
			// Undefined Fields Check
			if (!undefinedFields && !_buildings[i].model)
				undefinedFields = true;

			// Contains Duplicate WindowTypes Check
			if (!containsDuplicateBuildingTypes && Properties.buildings[(int)_buildings[i].type])
				containsDuplicateBuildingTypes = true;

			Properties.buildings[(int)_buildings[i].type] = _buildings[i].model;
		}

		// Undefined Fields Warning
		if (undefinedFields)
			Debug.LogWarning("You have undefined fields in " + this);

		// Contains Duplicate BuildingTypes Warning
		if (containsDuplicateBuildingTypes)
			Debug.LogWarning("You have duplicate BuildingTypes in " + this);
	}

	/// <summary>
	/// Generates a building array with the size of the BuildingTypes enum.
	/// Automatically sets a different type for each building.
	/// </summary>
	public void GenerateBuildingArray()
	{
		int buildingTypesLength = Enum.GetNames(typeof(BuildingTypes)).Length;

		_buildings = new Building[buildingTypesLength];

		for (int i = 0; i < buildingTypesLength; i++)
			_buildings[i].type = (BuildingTypes)i;
	}

	[Serializable]
	private struct Building
	{
		/// <summary>
		/// The specific type of this building.
		/// </summary>
		public BuildingTypes type;

		/// <summary>
		/// The model as type GameObject of this building.
		/// </summary>
		public GameObject model;
	}
}