using UnityEngine;

/// <summary>
/// A struct that keeps track of the boundaries.
/// </summary>
[System.Serializable]
public struct Boundary2D
{
    /// <summary>
    /// The minimal value of the boundary.
    /// </summary>
    public Vector2 min;

    /// <summary>
    /// The maximal value of the boundary.
    /// </summary>
    public Vector2 max;

    /// <summary>
    /// A function that keeps a Vector2 inside the min and max allowed values.
    /// </summary>
    /// <param name="currentPosition"> The current position of the object to clamp. </param>
    /// <returns> The new position of the object. </returns>
    public Vector2 Clamp(Vector2 currentPosition)
    {
        Vector2 nextPosition = new Vector2(
            Mathf.Clamp(currentPosition.x, min.x, max.x),
            Mathf.Clamp(currentPosition.y, min.y, max.y)
        );

        return nextPosition;
    }

    /// <summary>
    /// A function that keeps a object inside the min and max allowed values.
    /// </summary>
    /// <param name="currentX"> The current X position of the object to clamp. </param>
    /// <param name="currentY"> The current Y position of the object to clamp. </param>
    /// <returns> The new position of the object. </returns>
    public Vector2 Clamp(float currentX, float currentY)
    {
        Vector2 nextPosition = new Vector2(
            Mathf.Clamp(currentX, min.x, max.x),
            Mathf.Clamp(currentY, min.y, max.y)
        );

        return nextPosition;
    }
}
