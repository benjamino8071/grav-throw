/*
 * @author Ben Conway
 * @date May 2024
 */
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip
        backgroundNoise,
        mainTheme,
        menuButtonPressed,
        walk,
        jump,
        groundHit,
        ballPickUp,
        ballThrow,
        rotate,
        ballWallBounce,
        goalScored;
}
