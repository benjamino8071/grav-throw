using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class QuitButton : MonoBehaviour
{
    [SerializeField] private ConfirmQuitUI confirmQuitUI;
    
    [SerializeField] private Button button;
    
    private void Awake()
    {
        button.onClick.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        confirmQuitUI.Show();
    }
}
