using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Front-end functionality for player interacting with binding interface.
///
/// Back-end changing of bindings is performed by functions in BindingModifier.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
[RequireComponent(typeof(ButtonSUI))]
public class BindingSpecific : MonoBehaviour
{
    [SerializeField] private bool isGamepad;
    
    [SerializeField] private ButtonSUI settingsButtonUI;

    [SerializeField] private BindingsSUI bindingsUI;
    [SerializeField] private BindingModifier bindingModifier;

    [SerializeField] private BindingModifier.Binding binding;

    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image buttonImage;

    [SerializeField] private GamepadIconsExample gamepadIconsExample;
    
    private void Awake()
    {
        settingsButtonUI.GetButton().onClick.AddListener(OnButtonPressed);
        
        bindingModifier.OnRestoreDefaultKeybinds += BindingModifierOnRestoreDefaultBindings;
        bindingModifier.OnKeybindsChanged += BindingModifierOnBindingsChanged;
    }

    private void Start()
    {
        UpdateKeybindVisual();
    }

    private void OnButtonPressed()
    {
        bindingsUI.ShowRebindBindingCover();
        bindingModifier.RebindBinding(binding, OnRebindComplete, isGamepad);
    }

    private void OnRebindComplete()
    {
        UpdateKeybindVisual();
        bindingsUI.HideRebindBindingCover();
    }

    private void UpdateKeybindVisual()
    {
        if (isGamepad)
        {
            UpdateKeybindImage();
        }
        else
        {
            UpdateKeybindText();
        }
    }
    
    private void UpdateKeybindText()
    {
        string keybindText = bindingModifier.GetBindingText(binding, isGamepad);
        settingsButtonUI.SetButtonText(keybindText);
    }

    private void UpdateKeybindImage()
    {
        string path = bindingModifier.GetBindingPath(binding, isGamepad);
        buttonImage.sprite = gamepadIconsExample.GetSprite(path);
    }
    
    private void BindingModifierOnBindingsChanged(object sender, EventArgs e)
    {
        UpdateKeybindVisual();
    }
    
    private void BindingModifierOnRestoreDefaultBindings(object sender, EventArgs e)
    {
        UpdateKeybindVisual();
    }
}
