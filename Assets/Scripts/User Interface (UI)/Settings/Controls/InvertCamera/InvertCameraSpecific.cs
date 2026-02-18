using System;
using UnityEngine;

/// <summary>
/// @author Ben Conway
/// @date May 2024
/// </summary>
[RequireComponent(typeof(ButtonSUI))]
public class InvertCameraSpecific : MonoBehaviour
{
    [SerializeField] private SUISuper parentSetting;
    
    [SerializeField] private BooleanVariableSO invertMouseSO;
    
    [SerializeField] private ButtonSUI settingsButtonUI;

    private string on = "On";
    private string off = "Off";
    
    private void Awake()
    {
        settingsButtonUI.GetButton().onClick.AddListener(OnButtonPressed);
        parentSetting.OnRestoreDefaults += ParentSetting_OnRestoreDefaults;
    }

    private void ParentSetting_OnRestoreDefaults(object sender, EventArgs e)
    {
        SetDefault();
    }

    private void Start()
    {
        invertMouseSO.LoadValue();
        UpdateInvertMouseText();
    }

    private void OnButtonPressed()
    {
        invertMouseSO.ChangeValue();
        UpdateInvertMouseText();
    }

    private void UpdateInvertMouseText()
    {
        if(invertMouseSO.GetValue())
            settingsButtonUI.SetButtonText(on);
        else
            settingsButtonUI.SetButtonText(off);
    }

    private void SetDefault()
    {
        invertMouseSO.SetDefaultValue();
        UpdateInvertMouseText();
    }
}
