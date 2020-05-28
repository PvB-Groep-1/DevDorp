using UnityEngine;
using CM.Patterns.Command;

/// <summary>
/// Rotates object on the given position the given degrees (relative).
/// </summary>
public class RotateObjectCommand : Command
{
    // Position of the object to be rotated.
    private int _xPos = 0;
    private int _zPos = 0;

    // Angles to be rotated relative to the current position.
    private int _rot = 0;

    /// <summary>
    /// Sets the variables for the object that has to be rotated.
    /// </summary>
    /// <param name="xPos"> Takes in int variable for the X position of the object. </param>
    /// <param name="zPos"> Takes in int variable for the Z position of the object. </param>
    /// <param name="rot"> Takes in int variable for the relative rotation of the object. </param>
    public RotateObjectCommand(int xPos, int zPos, int rot)
    {
        _xPos = xPos;
        _zPos = zPos;
        _rot = rot;
    }

    /// <summary>
    /// Executes the functionality
    /// </summary>
    public override void Execute()
    {
        RotateObject();
    }

    /// <summary>
    /// Undoes the previously executed functionality.
    /// </summary>
    public override void Undo()
    {
        throw new System.NotImplementedException();
    }

    private void RotateObject()
    {
        int buildingLayerMask = 1 << LayerMask.NameToLayer("Building");

        Vector3 rotatePoint = new Vector3(_xPos, 0, _zPos);
        var hitCollider = Physics.OverlapBox(rotatePoint, Vector3.one / 4, Quaternion.identity, buildingLayerMask);

        if (hitCollider.Length > 0)
        {
            for (int i = 0; i < hitCollider.Length; i++)
            {
                //rotate the object
                GameObject foundObject = hitCollider[i].gameObject;
                foundObject.transform.Rotate(0, _rot, 0);
            }
        }
        else
        {
            // Show to player that is not possible because there is no building there.
            return;
        }
    }
}
