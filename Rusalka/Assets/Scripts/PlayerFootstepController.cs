using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Controls effects related to the player's footsteps, including sounds and particles 
/// that dynamically change depending on the material of the surface walked on.
/// </summary>
public class PlayerFootstepController : MonoBehaviour
{
    public PlayerController PlayerController;
    public Transform EffectSpawnPoint;
    [Header("Surface Settings")]
    public PlayerFootstepSurfaceSetting[] SurfaceSettings;

    private int footstepSoundId = 0;

    /// <summary>
    /// Sound + Particle effect settings for different surface types
    /// </summary>
    [System.Serializable]
    public struct PlayerFootstepSurfaceSetting
    {
        public FootstepSurfaceType SurfaceType;
        public string[] FootstepSounds;
        public GameObject FootstepParticleEffect;
    }

    // Triggers a footstep, including the proper sound effect and potential particle effect.
    public void PlayerFootstep()
    {
        PlayPlayerFootstepSound();
        // Particle Function
    }

    // If the Player is on the ground, plays the appropriate player footstep sound effect
    // depending on the surface type below the player. If no known surface is present, will play a fallback sound.
    private void PlayPlayerFootstepSound()
    {
        if (PlayerController != null)
        {
            if (PlayerController.IsGrounded())
            {
                // Check for the surface type of the first hit solid surface below you 
                RaycastHit2D[] Hits = Physics2D.RaycastAll(transform.position, Vector2.down);
                // Sort by the closet point to player (ie. surface standing on)
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

                PlayFootstepSoundForSurfaceType(FootstepSurfaceType.Stone);
            }
        }
    }

    // Given a footstep surface type, play the appropriate sound effect
    private void PlayFootstepSoundForSurfaceType(FootstepSurfaceType surfaceType)
    {
        PlayerFootstepSurfaceSetting setting = GetSurfaceSettingForType(surfaceType);
        SoundController.Instance?.PlaySoundOneShotRandomPitch(GetNextFootstepSoundName(setting.FootstepSounds, ref footstepSoundId), 0.07f);
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

    // Given a surface type, returns the corresponding surface setting
    private PlayerFootstepSurfaceSetting GetSurfaceSettingForType(FootstepSurfaceType surfaceType)
    {
        foreach(PlayerFootstepSurfaceSetting setting in SurfaceSettings)
        {
            if (setting.SurfaceType == surfaceType) return setting;
        }
        return new PlayerFootstepSurfaceSetting();
    }
}
