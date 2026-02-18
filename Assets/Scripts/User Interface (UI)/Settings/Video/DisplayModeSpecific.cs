using System;
using UnityEngine;

/// <summary>
/// If Unity offers more screen modes in future versions, the developer may wish to add more screen modes
/// by adding them to the scriptable object referenced with displayModeSO.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
[RequireComponent(typeof(ListWithFullScreenModesPlayerChoice))]
public class DisplayModeSpecific : MonoBehaviour
{
    [SerializeField] private SUISuper parentSetting;
    
    [SerializeField] private ListWithFullScreenModesPlayerChoice listWithFSMsPCSO;
    [SerializeField] private ListWithFullScreenModesSO displayModeSO;
    
    private void Awake()
    {
        listWithFSMsPCSO.OnButtonPressed += ListWithFSMsPCSOOnOnButtonPressed;
        parentSetting.OnRestoreDefaults += ParentSetting_OnRestoreDefaults;
    }

    private void ParentSetting_OnRestoreDefaults(object sender, EventArgs e)
    {
        SetDefault();
    }

    private void Start()
    {
        displayModeSO.LoadPointer();
        
        UpdateFullScreenMode();
    }

    private void ListWithFSMsPCSOOnOnButtonPressed(object sender, EventArgs e)
    {
        UpdateFullScreenMode();
    }

    private void UpdateFullScreenMode()
    {
        Screen.fullScreenMode = displayModeSO.GetCurrentValue();
    }

    private void SetDefault()
    {
        displayModeSO.SetDefaultPtr();
        UpdateFullScreenMode();
        listWithFSMsPCSO.UpdatePlayerChoiceVisual();
    }
}
