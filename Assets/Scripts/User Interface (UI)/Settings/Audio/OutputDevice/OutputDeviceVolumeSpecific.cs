using System;
using TMPro;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Allows player to adjust the volume of their Vivox output device.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class OutputDeviceVolumeSpecific : MonoBehaviour
{
    [SerializeField] private SUISuper parentSetting;

    [SerializeField] private Slider outputDeviceVolumeSlider;
    [SerializeField] private TextMeshProUGUI outputDeviceVolumeVisual;
    
    [SerializeField] [Range(0.0001f, 100)] private float defaultValue;
    
    private bool joinedChannel;
    
    private void Awake()
    {
        outputDeviceVolumeSlider.onValueChanged.AddListener(OnOutputDeviceVolumeSliderChanged);
    }

    private void Start()
    {
        parentSetting.OnRestoreDefaults += ParentSetting_OnRestoreDefaults;
        VivoxService.Instance.ChannelJoined += VivoxService_OnChannelJoined;

        outputDeviceVolumeSlider.value = defaultValue;
        UpdateOutputDeviceVolumeVisual((int)defaultValue);
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
        
        VivoxService.Instance.SetOutputDeviceVolume((int)value);
        UpdateOutputDeviceVolumeVisual((int)value);
    }

    private void SetToDefault()
    {
        if (!joinedChannel)
        {
            return;
        }
        
        outputDeviceVolumeSlider.value = defaultValue;
        UpdateOutputDeviceVolumeVisual((int)defaultValue);
    }

    private void UpdateOutputDeviceVolumeVisual(int value)
    {
        outputDeviceVolumeVisual.SetText(value.ToString());
    }
}
