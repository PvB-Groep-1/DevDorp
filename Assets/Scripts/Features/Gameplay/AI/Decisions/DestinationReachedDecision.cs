using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This class decides if the villager has reached its destination or not.
/// </summary>
[CreateAssetMenu(menuName = "State Machine/Decisions/New Destination Reached Decision")]
public class DestinationReachedDecision : Decision
{
    // The chance for the state to change to a different state.
    [SerializeField][Range(0,100)]
    private int _changeStateChance = 50;

    /// <summary>
    /// This function makes the decision if its true or false.
    /// </summary>
    /// <param name="sc">Takes in the StateController class.</param>
    /// <returns>Returns true or false depending on the decision that has been made.</returns>
    public override bool Decide(StateController sc)
    {
        if (sc.navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid || sc.navMeshAgent.remainingDistance < .3f)
        {
            return ChooseRandomState();
        }
        else
            return false;
    }

    // This boolean function returns either true or false depending on which one is randomly chosen.
    private bool ChooseRandomState()
    { 
        int random = Random.Range(0, 100);

        if (random <= _changeStateChance)
        {
            return true;
        }
        else
            return false;
    }
}
