/*
 * Plays main menu theme.
 * 
 * @author Ben Conway
 * @date May 2024
 */
using UnityEngine;

public class MusicMenu : MonoBehaviour
{
    [SerializeField] private AudioClipRefsSO audioClipRefsSo;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        PlayMainThemeSound();
    }

    private void PlayMainThemeSound()
    {
        PlaySound(audioClipRefsSo.mainTheme);
    }

    private void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
