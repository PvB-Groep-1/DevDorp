using UnityEngine;
using CM.Patterns.Command;

/// <summary>
/// Spawns object on the given position.
/// </summary>
public class SpawnObjectCommand : Command
{
    // The object that has to be spawned.
    private GameObject _building    = null;

    private GameObject _newObject = null;

	private BuildingTypes _buildingType;

    // X position for the object that has to be destroyed.
    private int _xPos             = 0;
    // Z position for the object that has to be destroyed.
    private int _zPos             = 0;

    /// <summary>
    /// Sets the variables for the object that has to be destroyed.
    /// </summary>
    /// <param name="building">Takes in gameobject reference for the object that has to be spawned.</param>
    /// <param name="xPos">Takes in int variable for the X position on which the object has to be spawned.</param>
    /// <param name="zPos">Takes in int variable for the Z position on which the object has to be spawned.</param>
    public SpawnObjectCommand(GameObject building, BuildingTypes buildingType, int xPos, int zPos)
    {
        _building = building;
		_buildingType = buildingType;
		_xPos = xPos;
        _zPos = zPos;
    }

    /// <summary>
    /// Executes the functionality.
    /// </summary>
    public override void Execute()
    {
        PlaceBuilding();
    }

    /// <summary>
    /// Undoes the previously executed functionality.
    /// </summary>
    public override void Undo()
    {
        throw new System.NotImplementedException();
    }

    // This function spawns the object on the given position.
    private void PlaceBuilding()
    {
        if (!_building)
            return;
        
        int buildingLayerMask = 1 << LayerMask.NameToLayer("Building");

        Vector3 spawnPoint = new Vector3(_xPos, 0, _zPos);
        var hitCollider = Physics.OverlapBox(spawnPoint, Vector3.one / 4, Quaternion.identity, buildingLayerMask);

        if (hitCollider.Length > 0)
        {
            // Show to player that is not possible because space is occupied
            return;
        }
        else {
            _newObject = Object.Instantiate(_building, new Vector3(_xPos, 0, _zPos), Quaternion.Euler(new Vector3(_building.transform.localEulerAngles.x, 180, _building.transform.localEulerAngles.z)));

            _newObject.layer = LayerMask.NameToLayer("Building");

			Building building = _newObject.AddComponent<Building>();

			if (building)
			{
				building.buildingType = _buildingType;
				BlockProgrammingWindow.BuildBuilding(building.buildingType);
			}
		}
        
    }
}
