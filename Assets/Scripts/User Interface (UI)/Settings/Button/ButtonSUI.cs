using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides basic functionality for a button. If other scripts attached to the button want to use similar
/// functionality they call the functions in this script instead.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ButtonSUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    private void Awake()
    {
        button.onClick.AddListener(PlayButtonSFX);
    }

    private void PlayButtonSFX()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();
    }
    
    
    public Button GetButton()
    {
        return button;
    }

    public void SetButtonText(string text)
    {
        buttonText.SetText(text);
    }
    
    //No 'UpdateVisual' method here as pressing the button on or off could mean anything.
    //Therefore left to Specific class to update visual.
}
