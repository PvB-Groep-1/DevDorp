using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This class controls the wandering action.
/// </summary>
[CreateAssetMenu(menuName = "State Machine/Actions/New Wandering Action")]
public class WanderingAction : Action
{
    /// <summary>
    /// The update funtion for the state.
    /// </summary>
    /// <param name="sc">Reference to the brain of the AI(State Controller class).</param>
    public override void Act(StateController sc)
    {
        if(sc.navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid || sc.navMeshAgent.remainingDistance < .3f || !sc.navMeshAgent.hasPath)
        {
            sc.targetDestination = GetRandomDestination(sc);
            SetDesination(sc);

            sc.navMeshAgent.isStopped = false;
        }
    }

    /// <summary>
    /// The start function for the state.
    /// </summary>
    /// <param name="sc">Reference to the brain of the AI(State Controller class).</param>
    public override void OnActionStart(StateController sc)
    {
		sc.animation.Play("walk");
        sc.navMeshAgent.isStopped = false;

        sc.targetDestination = GetRandomDestination(sc);
        SetDesination(sc);
    }

    // This function sets the destination in the NavMeshAgent, this lets the villager know where it has to walk to.
    private void SetDesination(StateController sc)
    {
        sc.navMeshAgent.SetDestination(sc.targetDestination);
    }

    // This Vector function returns a random Vector3 location.
    private Vector3 GetRandomDestination(StateController sc)
    {
        Vector3 randomDirection = Random.insideUnitSphere * 20;

        randomDirection += sc.gameObject.transform.position;

        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 20, NavMesh.AllAreas);

        return hit.position;
    }
}
