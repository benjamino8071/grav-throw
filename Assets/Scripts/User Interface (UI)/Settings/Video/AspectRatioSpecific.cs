using System;
using UnityEngine;

/// <summary>
/// If developer wishes to add more aspect ratios, they must add to the the scriptable object
/// referenced with aspRatSO.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
[RequireComponent(typeof(ListWithStringVariablesPlayerChoice))]
public class AspectRatioSpecific : MonoBehaviour
{
    [SerializeField] private SUISuper parentSetting;
    
    [SerializeField] private ListWithStringVariablesPlayerChoice listWithStrPC;
    [SerializeField] private ListWithStringVariablesSO aspRatSO;

    private void Awake()
    {
        parentSetting.OnRestoreDefaults += ParentSetting_OnRestoreDefaults;
    }

    private void ParentSetting_OnRestoreDefaults(object sender, EventArgs e)
    {
        SetDefault();
    }

    private void Start()
    {
        aspRatSO.LoadPointer();
    }

    private void SetDefault()
    {
        aspRatSO.SetDefaultPtr();
        listWithStrPC.UpdatePlayerChoiceVisual();
    }
}
