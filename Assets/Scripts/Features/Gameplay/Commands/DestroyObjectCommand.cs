using UnityEngine;
using CM.Patterns.Command;

/// <summary>
/// Destroys object on the given position.
/// </summary>
public class DestroyObjectCommand : Command
{
    // X position for the object that has to be destroyed.
    private int _xPos = 0;
    // Z position for the object that has to be destroyed.
    private int _zPos = 0;

    /// <summary>
    /// Sets the variables for the object that has to be destroyed.
    /// </summary>
    /// <param name="xPos">Takes in int variable for the X position on which the object has to be destroyed.</param>
    /// <param name="zPos">Takes in int variable for the Z position on which the object has to be destroyed.</param>
    public DestroyObject(int xPos, int zPos)
    {
        _xPos = xPos;
        _zPos = zPos;
    }

    /// <summary>
    /// Executes the functionality.
    /// </summary>
    public override void Execute()
    {
        RemoveObject();
    }

    /// <summary>
    /// Undoes the previously executed functionality.
    /// </summary>
    public override void Undo()
    {
        throw new System.NotImplementedException();
    }

    // This function destroys the object on the given position.
    private void RemoveObject()
    {
        int buildingLayerMask = 1 << LayerMask.NameToLayer("Building");

        Vector3 destroyPoint = new Vector3(_xPos, 0, _zPos);
        var hitCollider = Physics.OverlapBox(destroyPoint, Vector3.one / 4, Quaternion.identity, buildingLayerMask);

        if (hitCollider.Length > 0)
        {
            for (int i = 0; i < hitCollider.Length; i++)
            {
                Object.Destroy(hitCollider[i].gameObject);
            }
        }
        else
        {
            // Show to player that is not possible because space is not occupied 
            Debug.Log("Ah oh");
            return;
        }
    }
}
