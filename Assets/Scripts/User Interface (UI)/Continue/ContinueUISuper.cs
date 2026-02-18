using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Provides functionality for Continue interface - which appears after the player completes the objective of a level.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ContinueUISuper : MonoBehaviour
{
    [SerializeField] private Selectable selectOnShow;
    
    public event EventHandler OnLevelComplete;
    
    [SerializeField] private string nextLevelSceneName;

    [SerializeField] private FloatVariableSO timerSO;
    [SerializeField] private TextMeshProUGUI timerText;
    
    [SerializeField] private Button continueButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitToMainMenuButton;
    
    [SerializeField] private ReticleUI reticleUI;
    [SerializeField] private SettingsUI settingsUI;

    [SerializeField] private BooleanVariableSO showBallMarkerSO;

    [SerializeField] private ConfirmLeaveUI confirmLeaveUI;
    
    private void Awake()
    {
        continueButton.onClick.AddListener(OnContinueButtonPressed);
        retryButton.onClick.AddListener(OnRetryButtonPressed);
        settingsButton.onClick.AddListener(OnSettingsButtonPressed);
        exitToMainMenuButton.onClick.AddListener(OnExitToMainMenuButtonPressed);
    }

    protected void LevelComplete()
    {
        showBallMarkerSO.SetToFalse();
        
        OnLevelComplete?.Invoke(this, EventArgs.Empty);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        Time.timeScale = 0f;
        
        timerText.SetText("Your Time: "+timerSO.GetValue().ToString("0.00")+"s");
        
        Show();
    }

    private void OnContinueButtonPressed()
    {
        Time.timeScale = 1f;

        if (nextLevelSceneName == "Quit")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(nextLevelSceneName);
        }
    }

    private void OnRetryButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnSettingsButtonPressed()
    {
        Hide();
        settingsUI.Show(Show);
    }

    private void OnExitToMainMenuButtonPressed()
    {
        confirmLeaveUI.Show(ExitToMainMenu);
    }

    private void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    private void Show()
    {
        gameObject.SetActive(true);
        selectOnShow.Select();
    }

    protected void Hide()
    {
        gameObject.SetActive(false);
    }
}
