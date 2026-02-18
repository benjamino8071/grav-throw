using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Provides functionality for a 'pause' interface.
///
/// If the pause interface is in offline gameplay, then Time.timescale is set to 0.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class PauseUI : MenuUISuper
{
    public event EventHandler OnPaused, OnUnPaused;
    
    [SerializeField] protected Button resumeButton;
    [SerializeField] private Button reloadButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitToMainMenuButton;
    
    [SerializeField] protected ReticleUI reticleUI;
    [SerializeField] protected SettingsUI settingsUI;
    
    [SerializeField] private bool isOnlineScene;

    [SerializeField] private ConfirmLeaveUI confirmLeaveUI;

    private bool hostDisconnected;
    
    private void Awake()
    {
        resumeButton.onClick.AddListener(OnResumeButtonPressed);
        reloadButton.onClick.AddListener(OnReloadButtonPressed);
        settingsButton.onClick.AddListener(OnSettingsButtonPressed);
        exitToMainMenuButton.onClick.AddListener(OnExitToMainMenuButtonPressed);
    }

    private void Start()
    {
        PlayerInputHandler.Instance.OnPauseBindingPressed += PlayerInputHandler_OnPauseBindingPressed;

        if (NetworkManager.Singleton)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
        }
        
        Hide();
    }

    private void Update()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject);
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong obj)
    {
        if (!NetworkManager.Singleton.IsServer)
        {
            hostDisconnected = true;
        }
    }

    private void OnResumeButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        Hide();
    }

    private void OnReloadButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnSettingsButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        HideToShowSettingsUI();
        settingsUI.Show(Show);
    }

    private void OnExitToMainMenuButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();
        
        confirmLeaveUI.Show(ExitToMainMenu);
    }

    private void ExitToMainMenu()
    {
        if (NetworkManager.Singleton)
        {
            NetworkManager.Singleton.Shutdown();
        }
        SceneManager.LoadScene("MainMenuScene");
    }

    private void PlayerInputHandler_OnPauseBindingPressed(object sender, EventArgs e)
    {
        if (!settingsUI.gameObject.activeSelf && !hostDisconnected)
        {
            Show();
        }
    }

    protected void Show()
    {
        OnPaused?.Invoke(this, EventArgs.Empty);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        if (isOnlineScene)
        {
            PlayerInputHandler.Instance.ignoreInput = true;
        }
        else
        {
            Time.timeScale = 0;
        }
        
        reticleUI.Hide();
        
        gameObject.SetActive(true);
        
        resumeButton.Select();
    }

    private void HideToShowSettingsUI()
    {
        gameObject.SetActive(false);
    }

    public override void Hide()
    {
        OnUnPaused?.Invoke(this, EventArgs.Empty);
        
        if (isOnlineScene)
        {
            PlayerInputHandler.Instance.ignoreInput = false;
        }
        else
        {
            Time.timeScale = 1;
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        gameObject.SetActive(false);
        reticleUI.Show();
    }

    private void OnDestroy()
    {
        PlayerInputHandler.Instance.OnPauseBindingPressed -= PlayerInputHandler_OnPauseBindingPressed;
    }
}
