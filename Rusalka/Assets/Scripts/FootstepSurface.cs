using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script that represents a walkable surface of a certain material type that influences
/// which footstep sound effect is played when the player walks on top of it.
/// </summary>
public class FootstepSurface : MonoBehaviour
{
    [SerializeField] private FootstepSurfaceType footstepSurface;

    /// <summary>
    /// Returns the footstep surface type of this surface. 
    /// </summary>
    public FootstepSurfaceType GetFootstepSurfaceType()
    {
        return footstepSurface;
    }
}

/// <summary>
/// Represents the 'material' of a walkable surface. Determines what type of footstep
/// sound effect will play when walked on.
/// </summary>
public enum FootstepSurfaceType
{
    Wood,
    ShallowPuddle
}
