using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays correct keybind in tutorial 'help' HUD component.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class KeybindTIUI : MonoBehaviour
{
    [SerializeField] private PlayerInputActionsSO plrInpActsSO;

    [SerializeField] private bool overrideBindingWithManual;
    [SerializeField] private string overrideText;
    [SerializeField] private Binding binding;
    
    [SerializeField] private TextMeshProUGUI keybindText;

    [SerializeField] private GamepadIconsExample gamepadIconsExample;
    [SerializeField] private Image keybindImage;

    private void Update()
    {
        if (plrInpActsSO.isUsingGamepad)
        {
            keybindText.gameObject.SetActive(false);
            keybindImage.gameObject.SetActive(true);
            UpdateKeybindImage();
        }
        else
        {
            keybindImage.gameObject.SetActive(false);
            keybindText.gameObject.SetActive(true);
            UpdateKeybindText();
        }
    }

    private void UpdateKeybindText()
    {
        if (overrideBindingWithManual)
        {
            keybindText.SetText(overrideText);
        }
        else
        {
            keybindText.SetText(plrInpActsSO.GetBindingText(binding));
        }
    }
    
    private void UpdateKeybindImage()
    {
        if (!overrideBindingWithManual)
        {
            string path = plrInpActsSO.GetBindingPath(binding, true);
            keybindImage.sprite = gamepadIconsExample.GetSprite(path);
        }
    }
}
