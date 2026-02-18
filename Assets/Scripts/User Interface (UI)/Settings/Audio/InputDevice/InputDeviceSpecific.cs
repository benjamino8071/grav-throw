using System;
using System.Collections.ObjectModel;
using TMPro;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Allows player to change the Vivox input device - i.e. their microphone.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class InputDeviceSpecific : MonoBehaviour
{
    [SerializeField] private Button leftClickButton;
    [SerializeField] private Button rightClickButton;
    [SerializeField] private TextMeshProUGUI optionChosenText;
    
    private ReadOnlyCollection<VivoxInputDevice> inputDevices;
    private int inputDevicesListPointer = 0;

    private bool channelJoined;
    
    private void Awake()
    {
        leftClickButton.onClick.AddListener(OnLeftClickButtonPressed);
        rightClickButton.onClick.AddListener(OnRightClickButtonPressed);
    }

    private void Start()
    {
        VivoxService.Instance.AvailableInputDevicesChanged += RefreshInputDeviceList;
        VivoxService.Instance.ChannelJoined += VivoxService_OnChannelJoined;
    }

    private void OnEnable()
    {
        if (VivoxService.Instance.ActiveChannels.Count == 0)
        {
            return;
        }

        channelJoined = true;
        RefreshInputDeviceList();
    }

    private void VivoxService_OnChannelJoined(string obj)
    {
        channelJoined = true;
        
        RefreshInputDeviceList();
    }

    private void OnLeftClickButtonPressed()
    {
        if (!channelJoined || inputDevicesListPointer <= 0)
        {
            return;
        }

        inputDevicesListPointer--;
        
        VivoxService.Instance.SetActiveInputDeviceAsync(inputDevices[inputDevicesListPointer]);
        
        UpdateOptionChosenVisual();
    }

    private void OnRightClickButtonPressed()
    {
        if (!channelJoined || inputDevicesListPointer >= inputDevices.Count - 1)
        {
            return;
        }

        inputDevicesListPointer++;

        VivoxService.Instance.SetActiveInputDeviceAsync(inputDevices[inputDevicesListPointer]);
            
        UpdateOptionChosenVisual();
    }
    
    private void RefreshInputDeviceList()
    {
        inputDevices = VivoxService.Instance.AvailableInputDevices;
        
        //In case size of inputDevices decreases
        if (inputDevicesListPointer >= inputDevices.Count)
        {
            inputDevicesListPointer = inputDevices.Count - 1;
        }
        
        UpdateOptionChosenVisual();
    }

    private void UpdateOptionChosenVisual()
    {
        string deviceChosen = inputDevices[inputDevicesListPointer].DeviceName;

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
        VivoxService.Instance.AvailableInputDevicesChanged -= RefreshInputDeviceList;
    }
}
