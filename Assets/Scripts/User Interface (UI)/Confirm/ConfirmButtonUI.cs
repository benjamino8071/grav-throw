using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Confirm button for User Test 1 'InitialSetup' screen
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ConfirmButtonUI : MonoBehaviour
{
    [SerializeField] private bool isFinalConfirm;
    [SerializeField] private ConfirmSettingsUI confirmSettingsUI;
    
    [SerializeField] private MenuUISuper parentSetting;
    [SerializeField] private MenuUISuper uiToShow;
    
    [SerializeField] private Button button;
    
    private void Awake()
    {
        button.onClick.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();
        
        if (isFinalConfirm)
        {
            confirmSettingsUI.Show(LoadTutorial);
        }
        else
        {
            parentSetting.Hide();
            uiToShow.Show();
        }
    }

    private void LoadTutorial()
    {
        SceneManager.LoadScene("1LevelOneScene");
    }
}
