/*
 * Provides function for other classes to play sound effects.
 * 
 * @author Ben Conway
 * @date May 2024
 */
using UnityEngine;

public class SoundEffectsMenu : MonoBehaviour
{
    [SerializeField] private bool playBackgroundNoise = true;
    
    public static SoundEffectsMenu Instance { get; private set; }
    
    [SerializeField] private AudioClipRefsSO audioClipRefsSo;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private GameObject backgroundNoiseChild;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        backgroundNoiseChild.SetActive(playBackgroundNoise);
    }

    public void PlayMenuButtonPressedSound()
    {
        PlaySound(audioClipRefsSo.menuButtonPressed);
    }

    private void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}



