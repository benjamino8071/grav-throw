using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

/// <summary>
/// Provides functionality for player to change volume of output device.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
[RequireComponent(typeof(SliderFloat))]
public class AudioSpecific : MonoBehaviour
{
    [SerializeField] private string AUDIO_MIXER_PARAM_NAME;
    
    [FormerlySerializedAs("floatSliderUI")] [SerializeField] private SliderFloat sliderFloat;

    [SerializeField] private AudioMixer audioMixer;
    
    private void Awake()
    {
        if(AUDIO_MIXER_PARAM_NAME == null)
            Debug.LogError("ERROR: Audio mixer parameter name not set!");
        
        sliderFloat.GetSlider().onValueChanged.AddListener(UpdateAudioMixer);
    }

    private void UpdateAudioMixer(float sliderValue)
    {
        sliderValue /= 100;
        float newVol = Mathf.Log10(sliderValue)*20;
        audioMixer.SetFloat(AUDIO_MIXER_PARAM_NAME, newVol);
    }
}
