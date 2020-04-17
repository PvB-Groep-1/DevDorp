using UnityEngine;

/// <summary>
/// This class decides when the villager has to transition to another state when the Timer is finished.
/// </summary>
[CreateAssetMenu(menuName = "State Machine/Decisions/New Timer Decision")]
public class TimerDecision : Decision
{
    // Float variable that holds the max time for the timer.
    [SerializeField] private float _time = 5;

    // Float variable that hold the current time.
    private float _timer;

    /// <summary>
    /// This function makes the decision if its true or false.
    /// </summary>
    /// <param name="sc">Takes in the StateController class.</param>
    /// <returns>Returns true or false depending on the decision that has been made.</returns>
    public override bool Decide(StateController sc) { return Timer(); }

    /// <summary>
    /// This is the start function for the decision class, this is used to set variables before any decision is going to get made.
    /// </summary>
    /// <param name="sc">Takes in the StateController class.</param>
    public override void OnDecisionStart(StateController sc) { _timer = _time; }

    // This function controls the timer.
    private bool Timer()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            return false;
        }
        else
            return true;
    }
}
