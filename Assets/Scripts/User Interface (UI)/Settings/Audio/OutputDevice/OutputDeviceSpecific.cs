using System;
using System.Collections.ObjectModel;
using TMPro;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Allows player to change the Vivox output device - i.e. their speakers/headphones.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class OutputDeviceSpecific : MonoBehaviour
{
    [SerializeField] private Button leftClickButton;
    [SerializeField] private Button rightClickButton;
    [SerializeField] private TextMeshProUGUI optionChosenText;
    
    private ReadOnlyCollection<VivoxOutputDevice> outputDevices;
    private int outputDevicesListPointer = 0;

    private bool channelJoined;
    
    private void Awake()
    {
        leftClickButton.onClick.AddListener(OnLeftClickButtonPressed);
        rightClickButton.onClick.AddListener(OnRightClickButtonPressed);
    }

    private void Start()
    {
        VivoxService.Instance.AvailableOutputDevicesChanged += RefreshOutputDeviceList;
        VivoxService.Instance.ChannelJoined += VivoxService_OnChannelJoined;
    }

    private void OnEnable()
    {
        if (VivoxService.Instance.ActiveChannels.Count == 0)
        {
            return;
        }

        channelJoined = true;
        RefreshOutputDeviceList();
    }

    private void VivoxService_OnChannelJoined(string obj)
    {
        channelJoined = true;
        
        RefreshOutputDeviceList();
    }

    private void OnLeftClickButtonPressed()
    {
        if (!channelJoined || outputDevicesListPointer <= 0)
        {
            return;
        }

        outputDevicesListPointer--;
        
        VivoxService.Instance.SetActiveOutputDeviceAsync(outputDevices[outputDevicesListPointer]);
        
        UpdateOptionChosenVisual();
    }

    private void OnRightClickButtonPressed()
    {
        if (!channelJoined || outputDevicesListPointer >= outputDevices.Count - 1)
        {
            return;
        }

        outputDevicesListPointer++;

        VivoxService.Instance.SetActiveOutputDeviceAsync(outputDevices[outputDevicesListPointer]);
            
        UpdateOptionChosenVisual();
    }
    
    private void RefreshOutputDeviceList()
    {
        outputDevices = VivoxService.Instance.AvailableOutputDevices;
        
        //In case size of inputDevices decreases
        if (outputDevicesListPointer >= outputDevices.Count)
        {
            outputDevicesListPointer = outputDevices.Count - 1;
        }
        
        UpdateOptionChosenVisual();
    }

    private void UpdateOptionChosenVisual()
    {
        string deviceChosen = outputDevices[outputDevicesListPointer].DeviceName;

        if (deviceChosen.Length > 23)
        {
            string deviceChosenShort = deviceChosen.Substring(0, Math.Min(deviceChosen.Length, 20));
            deviceChosen = deviceChosenShort+"...";
        }
        
        optionChosenText.SetText(deviceChosen);
    }

    private void OnDestroy()
    {
        VivoxService.Instance.ChannelJoined -= VivoxService_OnChannelJoined;
        VivoxService.Instance.AvailableInputDevicesChanged -= RefreshOutputDeviceList;
    }
}
