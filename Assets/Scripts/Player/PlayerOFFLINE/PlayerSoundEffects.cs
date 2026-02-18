/*
 * Outputs sound effects based on actions performed by the player and interaction of the PC with the scene environment.
 *
 * @author Ben Conway
 * @date May 2024
 */
using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour
{
    [SerializeField] private AudioClipRefsSO audioClipRefsSo;
    [SerializeField] private AudioSource audioSource;
    
    
    public void PlayJumpSound()
    {
        PlaySound(audioClipRefsSo.jump);
    }
    
    public void PlayGroundHitSound()
    {
        PlaySound(audioClipRefsSo.groundHit);
    }

    public void PlayBallPickUpSound()
    {
        PlaySound(audioClipRefsSo.ballPickUp);
    }

    public void PlayBallThrowSound()
    {
        PlaySound(audioClipRefsSo.ballThrow);
    }

    private void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
