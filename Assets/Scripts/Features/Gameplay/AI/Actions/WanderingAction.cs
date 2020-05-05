using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This class controls the wandering action.
/// </summary>
[CreateAssetMenu(menuName = "State Machine/Actions/New Wandering Action")]
public class WanderingAction : Action
{
    // Reference to the NavMeshAgent component.
    private NavMeshAgent _navMeshAgent;

    // The destination the villager has to wander to.
    private Vector3 _targetDestination;

    /// <summary>
    /// The update funtion for the state.
    /// </summary>
    /// <param name="sc">Reference to the brain of the AI(State Controller class).</param>
    public override void Act(StateController sc)
    {
        if(_navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid || _navMeshAgent.remainingDistance < .3f || !_navMeshAgent.hasPath)
        {
            _targetDestination = GetRandomDestination(sc);
            SetDesination(sc);

            _navMeshAgent.isStopped = false;
        }
    }

    /// <summary>
    /// The start function for the state.
    /// </summary>
    /// <param name="sc">Reference to the brain of the AI(State Controller class).</param>
    public override void OnActionStart(StateController sc)
    {
		sc.animation.Play("walk");

        _navMeshAgent = sc.navMeshAgent;
        _navMeshAgent.isStopped = false;

        _targetDestination = GetRandomDestination(sc);
        SetDesination(sc);
    }

    // This function sets the destination in the NavMeshAgent, this lets the villager know where it has to walk to.
    private void SetDesination(StateController sc)
    {
        sc.targetDestination = _targetDestination;
        _navMeshAgent.SetDestination(_targetDestination);
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
