using UnityEngine;
using TMPro;

/// <summary>
/// @author Ben Conway
/// @date May 2024
/// </summary>
[RequireComponent(typeof(SliderFloat))]
public class TextSizeSpecific : MonoBehaviour
{
    [SerializeField] private SliderFloat slider;

    private void Awake()
    {
        slider.GetSlider().onValueChanged.AddListener(UpdateTextSize);
    }

    private void UpdateTextSize(float sliderValue)
    {
        TextMeshProUGUI[] textObjects = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
        
        foreach (TextMeshProUGUI textObject in textObjects)
        {
            textObject.fontSize = sliderValue;
        }
    }
}
