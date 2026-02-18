using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Not complete. Purpose is to simulate change in brightness by changing alpha value of a black image.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class BrightnessCover : MonoBehaviour
{
    [SerializeField] private FloatVariableSO brightnessSO;
    
    [SerializeField] private Volume volume;

    private ColorAdjustments colorAdjustments;

    private void Start()
    {
        foreach (ColorAdjustments component in volume.profile.components)
        {
            colorAdjustments = component;
        }
    }

    private void Update()
    {
        UpdateBrightnessCover();
    }

    private void UpdateBrightnessCover()
    {
        colorAdjustments.postExposure.value = brightnessSO.GetValue();
    }
}
