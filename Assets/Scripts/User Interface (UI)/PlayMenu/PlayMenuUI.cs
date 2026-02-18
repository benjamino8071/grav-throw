using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Part of main menu scene. Player is able to choose between online (multiplayer)
/// or offline (tutorial/freeplay) gameplay.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class PlayMenuUI : MenuUISuper
{
    [SerializeField] private Button multiplayerButton;
    [SerializeField] private Button trainingButton;

    [SerializeField] private TrainingMenuUI trainingMenuUI;
    
    private void Awake()
    {
        multiplayerButton.onClick.AddListener(OnMultiplayerButtonPressed);
        trainingButton.onClick.AddListener(OnTrainingButtonPressed);
    }

    private void Start()
    {
        Hide();
    }

    private void OnMultiplayerButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();
        
        SceneManager.LoadScene("LobbyScene");
    }

    private void OnTrainingButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        trainingMenuUI.Show();
        Hide();
    }
}
