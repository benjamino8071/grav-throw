using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides functionality for the main menu interface.
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class MainMenuUI : MenuUISuper
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    //Quit button is already handled independently

    [SerializeField] private PlayMenuUI playMenuUI;
    [SerializeField] private SettingsUI settingsUI; 
    
    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonPressed);
        settingsButton.onClick.AddListener(OnSettingsButtonPressed);
    }

    private void Start()
    {
        Show();
    }

    private void OnPlayButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();
        
        playMenuUI.Show();
        Hide();
    }

    private void OnSettingsButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        settingsUI.Show();
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        
        playButton.Select();
    }
    
    //Hide method is inherited
}
