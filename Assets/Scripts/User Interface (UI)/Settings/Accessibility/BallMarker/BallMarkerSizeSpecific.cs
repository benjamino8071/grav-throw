using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Allows player to adjust size of ball marker.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
[RequireComponent(typeof(SliderFloat))]
public class BallMarkerSizeSpecific : MonoBehaviour
{
    [FormerlySerializedAs("floatSliderUI")] [SerializeField] private SliderFloat sliderFloat;

    [SerializeField] private Image ballMarkerExampleImage;
    
    private void Awake()
    {
        sliderFloat.GetSlider().onValueChanged.AddListener(OnSizeSliderChanged);
    }

    private void OnSizeSliderChanged(float sliderValue)
    {
        ballMarkerExampleImage.rectTransform.sizeDelta = new Vector2(sliderValue, sliderValue);
    }
}
