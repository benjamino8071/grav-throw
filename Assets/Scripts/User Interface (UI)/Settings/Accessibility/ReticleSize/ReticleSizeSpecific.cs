using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Allows player to adjust size of reticle.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
[RequireComponent(typeof(SliderFloat))]
public class ReticleSizeSpecific : MonoBehaviour
{
    [FormerlySerializedAs("floatSliderUI")] [SerializeField] private SliderFloat sliderFloat;

    [SerializeField] private Image reticleExampleSizeImage;
    
    private void Awake()
    {
        sliderFloat.GetSlider().onValueChanged.AddListener(OnSizeSliderChanged);
    }

    private void OnSizeSliderChanged(float sliderValue)
    {
        reticleExampleSizeImage.rectTransform.sizeDelta = new Vector2(sliderValue, sliderValue);
    }
}
