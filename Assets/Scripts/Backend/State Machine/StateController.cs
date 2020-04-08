using UnityEngine.AI;
using UnityEngine;

/// <summary>
/// The controller of the AI, this is the brain that controlls the AI.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class StateController : MonoBehaviour
{
    // The NavMesh controller component.
    private NavMeshAgent _navMeshAgent;

    // The current state in which the AI currently is.
    [SerializeField] private State _currentState = null;

    // The awake function is to set all the references.
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // The update function is here to update the current state.
    private void Update()
    {
        _currentState.UpdateState(this);
    }

    // Display a box around the object to debug in which state the object is.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _currentState.gizmoColor;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    /// <summary>
    /// This function transitions the current state to the new state
    /// </summary>
    /// <param name="nextState">The next state the AI has to go into.</param>
    public void TransitionToState(State nextState)
    {
        if (nextState)
        {
            if (nextState != _currentState)
                _currentState = nextState;
        }
    }
}
