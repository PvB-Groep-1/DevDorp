using UnityEngine;

/// <summary>
/// A class for making a decision.
/// </summary>
public abstract class Decision : ScriptableObject
{
    /// <summary>
    /// The decision that has to be made.
    /// </summary>
    /// <param name="sc">Takes in the StateController class.</param>
    /// <returns>Returns true or false depending on the decision that has been made.</returns>
    public abstract bool Decide(StateController sc);
}
