using UnityEngine;

/// <summary>
/// This class controls the idle action.
/// </summary>
[CreateAssetMenu(menuName = "State Machine/Actions/New Idle Action")]
public class IdleAction : Action
{
    /// <summary>
    /// The update fucntion for the action class.
    /// </summary>
    /// <param name="sc">Reference to the brain of the AI(State Controller class).</param>
    public override void Act(StateController sc)
    {
        sc.gameObject.GetComponent<Animator>().Play("Idle");
    }
}
