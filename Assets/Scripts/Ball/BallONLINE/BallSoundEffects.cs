/*
 * Outputs sound based on actions performed by the Ball.
 *
 * @author Ben Conway
 * @date May 2024
 */
using UnityEngine;

public class BallSoundEffects : MonoBehaviour
{
    [SerializeField] private AudioClipRefsSO audioClipRefsSo;
    [SerializeField] private AudioSource audioSource;
    
    public void PlayBallWallBounceSound()
    {
        PlaySound(audioClipRefsSo.ballWallBounce);
    }

    private void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}

