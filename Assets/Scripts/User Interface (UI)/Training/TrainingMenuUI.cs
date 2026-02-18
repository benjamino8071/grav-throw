using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Allows player to choose between playing tutorial or free play.
///
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class TrainingMenuUI : MenuUISuper
{
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button freePlayButton;

    private Action onCloseButtonAction;

    private void Awake()
    {
        tutorialButton.onClick.AddListener(OnTutorialButtonPressed);
        freePlayButton.onClick.AddListener(OnFreePlayButtonPressed);
    }

    private void Start()
    {
        Hide();
    }

    private void OnTutorialButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        //Load level one (tutorial) scene
        SceneManager.LoadScene("1LevelOneScene");
    }

    private void OnFreePlayButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        //Load free play scene
        SceneManager.LoadScene("FreePlayScene");
    }
}
