using System;
using TMPro;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class JoinedLobbyUI : MonoBehaviour
{
    [SerializeField] private VivoxLogin vivoxLogin;

    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        messageText.SetText("Lobby found! Loading lobby...");
        
        mainMenuButton.onClick.AddListener(OnMainMenuButtonPressed);
        
        mainMenuButton.gameObject.SetActive(false);
    }

    private void Start()
    {
        vivoxLogin.OnCouldntConnect += VivoxLogin_OnCouldntConnect;
        VivoxService.Instance.ChannelJoined += VivoxService_OnChannelJoined;
    }

    private void VivoxLogin_OnCouldntConnect(object sender, EventArgs e)
    {
        mainMenuButton.gameObject.SetActive(true);
        messageText.SetText("Error loading lobby. Please press below to return to the main menu.");
    }

    private void VivoxService_OnChannelJoined(string obj)
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        VivoxService.Instance.ChannelJoined -= VivoxService_OnChannelJoined;
    }

    private void OnMainMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
