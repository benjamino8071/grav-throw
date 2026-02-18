using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

/// <summary>
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class DeadzoneSpecific : MonoBehaviour
{
    [FormerlySerializedAs("floatSliderUI")] [SerializeField] private SliderFloat sliderFloat;
    [SerializeField] private PlayerInputActionsSO playerInputActionsSO;
    [SerializeField] private FloatVariableSO deadzoneSO;

    private void Awake()
    {
        sliderFloat.GetSlider().onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float sliderValue)
    {
        ChangeLeftStickDeadzone(sliderValue);
    }
    
    private void ChangeLeftStickDeadzone(float deadzoneValue)
    {
        playerInputActionsSO.Value.PlayerActionMap.Move.ApplyParameterOverride("StickDeadzone:min", deadzoneValue);
    }
}
