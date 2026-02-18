using System;
using TMPro;
using UnityEngine;

/// <summary>
/// @author Ben Conway
/// @date May 2024
/// </summary>
[RequireComponent(typeof(ListWithTextFontsPlayerChoice))]
public class TextFontSpecific : MonoBehaviour
{
    [SerializeField] private SUISuper parentSetting;
    
    [SerializeField] private ListWithTextFontsPlayerChoice listWithTextFontsPC;

    [SerializeField] private ListWithTextFontsSO listWithTextFontsSO;
    
    private void Awake()
    {
        listWithTextFontsPC.OnButtonPressed += ListWithTextFontsSOOnOnButtonPressed;
        parentSetting.OnRestoreDefaults += ParentSetting_OnRestoreDefaults;
    }

    private void Start()
    {
        UpdateTextFont();
    }

    private void ParentSetting_OnRestoreDefaults(object sender, EventArgs e)
    {
        SetDefault();
    }

    private void ListWithTextFontsSOOnOnButtonPressed(object sender, EventArgs e)
    {
        UpdateTextFont();
    }

    private void Update()
    {
        UpdateTextFont();
    }

    private void UpdateTextFont()
    {
        TMP_FontAsset newFont = listWithTextFontsSO.GetCurrentValue();
        
        TextMeshProUGUI[] textObjects = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
        
        foreach (TextMeshProUGUI textObject in textObjects)
        {
            textObject.font = newFont;
        }
    }

    private void SetDefault()
    {
        listWithTextFontsSO.SetDefaultPtr();
        UpdateTextFont();
        listWithTextFontsPC.UpdatePlayerChoiceVisual();
    }
}
