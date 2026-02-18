using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class RestoreDefaultsSpecific : MonoBehaviour
{
    [SerializeField] private SUISuper parentSettings;
    
    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();
        parentSettings.RestoreDefaults();
    }
}
