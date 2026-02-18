using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays correct keybind in tutorial 'help' HUD component.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class KeybindCompositeTIUI : MonoBehaviour
{
    [SerializeField] private PlayerInputActionsSO plrInpActsSO;
    
    [SerializeField] private List<Binding> bindingsList;
    [SerializeField] private TextMeshProUGUI keybindsText;
    
    [SerializeField] private Image keybindImage;
    
    private void Update()
    {
        if (plrInpActsSO.isUsingGamepad)
        {
            keybindsText.gameObject.SetActive(false);
            keybindImage.gameObject.SetActive(true);
        }
        else
        {
            keybindImage.gameObject.SetActive(false);
            keybindsText.gameObject.SetActive(true);
            UpdateKeybindsText();
        }
    }

    private void UpdateKeybindsText()
    {
        string keybindText = "";
        foreach (Binding binding in bindingsList)
        {
            keybindText += plrInpActsSO.GetBindingText(binding) + "/";
        }

        keybindText = keybindText.Remove(keybindText.Length - 1, 1);
        keybindsText.SetText(keybindText);
    }
}