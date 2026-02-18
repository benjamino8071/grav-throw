using System;
using TMPro;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Allows the player to adjust the volume of their Vivox input device.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class InputDeviceVolumeSpecific : MonoBehaviour
{
    [SerializeField] private SUISuper parentSetting;
    
    [SerializeField] private Slider inputDeviceVolumeSlider;
    [SerializeField] private TextMeshProUGUI inputDeviceVolumeVisual;
    
    [SerializeField] [Range(0.0001f, 100)] private float defaultValue;

    private bool joinedChannel;
    
    private void Awake()
    {
        inputDeviceVolumeSlider.onValueChanged.AddListener(OnOutputDeviceVolumeSliderChanged);
    }

    private void Start()
    {
        parentSetting.OnRestoreDefaults += ParentSetting_OnRestoreDefaults;
        VivoxService.Instance.ChannelJoined += VivoxService_OnChannelJoined;
        
        inputDeviceVolumeSlider.value = defaultValue;
        UpdateInputDeviceVolumeVisual((int)defaultValue);
    }

    private void OnEnable()
    {
        if (VivoxService.Instance.ActiveChannels.Count > 0)
        {
            joinedChannel = true;
        }
    }

    private void VivoxService_OnChannelJoined(string obj)
    {
        joinedChannel = true;
    }

    private void ParentSetting_OnRestoreDefaults(object sender, EventArgs e)
    {
        SetToDefault();
    }

    private void OnOutputDeviceVolumeSliderChanged(float value)
    {
        if (!joinedChannel)
        {
            return;
        }
        
        VivoxService.Instance.SetInputDeviceVolume((int)value);
        UpdateInputDeviceVolumeVisual((int)value);
    }
    
    private void SetToDefault()
    {
        if (!joinedChannel)
        {
            return;
        }
        
        inputDeviceVolumeSlider.value = defaultValue;
        UpdateInputDeviceVolumeVisual((int)defaultValue);
    }

    private void UpdateInputDeviceVolumeVisual(int value)
    {
        inputDeviceVolumeVisual.SetText(value.ToString());
    }
}
