using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Controls the player's footstep sound effects, allowing them to dynamically change depending on the 
/// surface below the player.
/// </summary>
public class PlayerFootstepController : MonoBehaviour
{
    public PlayerController playerController;
    [Header("Sound Effects")]
    public string[] WoodSounds;
    public string[] ShallowPuddleSounds;

    private int footstepSoundId = 0;

    // If the Player is on the ground, plays the appropriate player footstep sound effect
    // depending on the surface type below the player. If no known surface is present, will play a fallback sound.
    public void PlayPlayerFootstepSound()
    {
        if (playerController != null)
        {
            if (playerController.IsGrounded())
            {
                // Check for the surface type of the first hit solid surface below you 
                RaycastHit2D[] Hits = Physics2D.RaycastAll(transform.position, Vector2.down);
                // Sort by the furthest point
                Array.Sort(Hits, (x, y) => x.distance.CompareTo(y.distance));
                // Search for a footstep surface in hits
                foreach(RaycastHit2D Hit in Hits)
                {
                    FootstepSurface footstepSurface = Hit.transform.GetComponent<FootstepSurface>();
                    if (footstepSurface != null)
                    {
                        PlayFootstepSoundForSurfaceType(footstepSurface.GetFootstepSurfaceType());
                        return;
                    }
                }

                PlayFootstepSoundForSurfaceType(FootstepSurfaceType.Wood);
            }
        }
    }

    // Given a footstep surface type, play the appropriate sound effect
    private void PlayFootstepSoundForSurfaceType(FootstepSurfaceType surfaceType)
    {
        string footstepSoundToPlay = "";
        switch(surfaceType)
        {
            case FootstepSurfaceType.Wood:
                footstepSoundToPlay = GetNextFootstepSoundName(WoodSounds, ref footstepSoundId);
                break;
            case FootstepSurfaceType.ShallowPuddle:
                footstepSoundToPlay = GetNextFootstepSoundName(ShallowPuddleSounds, ref footstepSoundId);
                break;
        }
        SoundController.Instance?.PlaySoundOneShotRandomPitch(footstepSoundToPlay, 0.07f);
    }

    // Given a list of sounds effect names and an index in the list, return the next sound name to play
    private string GetNextFootstepSoundName(string[] soundList, ref int id)
    {
        if (id >= soundList.Length)
        {
            id = 0;
        }
        string nextSoundName = soundList[id];
        id += 1;
        return nextSoundName;
    }
}
