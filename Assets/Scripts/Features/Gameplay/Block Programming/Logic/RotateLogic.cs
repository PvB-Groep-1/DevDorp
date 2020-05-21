using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents rotation logic.
/// </summary>
public sealed class RotateLogic : ExecutionLogic
{
    [SerializeField]
    private XYLogic _XYLogic;

    [SerializeField]
    private RotLogic _RotLogic;

    /// <summary>
    /// Creates the Command, and executes the rotation according to the parameters given in _XYLogic and _RotLogic.
    /// </summary>
    public override void Execute()
    {
        if (_XYLogic.inputFieldX.text == "" || _XYLogic.inputFieldY.text == "" || _RotLogic.inputFieldRot.text == "")
            return;

        RotateObjectCommand command = new RotateObjectCommand(-15 + _XYLogic.x * 9, 5 + _XYLogic.y * 9, _RotLogic.rot);
        command.Execute();
    }
    
    /// <summary>
    /// Sets the values of the _XYLogic and _RotLogic.
    /// </summary>
    public void SetValues()
    {
        _XYLogic.SetValues();
        _RotLogic.SetValues();
    }
}
