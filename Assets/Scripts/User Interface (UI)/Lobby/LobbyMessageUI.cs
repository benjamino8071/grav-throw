using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates player on state of creating/joining a lobby.
/// 
/// Contains multiple event calls to fulfill this purpose.
/// 
/// @author Ben Conway, with inspiration from https://www.youtube.com/watch?v=7glCsF9fv3s
/// @date May 2024
/// </summary>
public class LobbyMessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button closeButton;
    
    private void Awake()
    {
        closeButton.onClick.AddListener(OnCloseButtonPressed);
    }

    private void Start()
    {
        GameMultiplayer.Instance.OnFailedToJoinGame += GameMultiplayer_OnFailedToJoinGame;
        GameLobby.Instance.OnCreateLobbyStarted += GameLobby_OnCreateLobbyStarted;
        GameLobby.Instance.OnCreateLobbyFailed += GameLobby_OnCreateLobbyFailed;
        GameLobby.Instance.OnJoinStarted += GameLobby_OnJoinStarted;
        GameLobby.Instance.OnJoinFailed += GameLobby_OnJoinFailed;
        GameLobby.Instance.OnQuickJoinFailed += GameLobby_OnQuickJoinFailed;
        GameLobby.Instance.OnJoinWithCodeFailed += GameLobby_OnJoinWithCodeFailed;
        Hide();
    }

    private void OnCloseButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();
        
        Hide();
    }

    private void GameLobby_OnJoinWithCodeFailed(object sender, EventArgs e)
    {
        ShowMessage("Could not join a Lobby with this code!");
        
        closeButton.gameObject.SetActive(true);
        closeButton.Select();
    }

    private void GameLobby_OnQuickJoinFailed(object sender, EventArgs e)
    {
        ShowMessage("Could not find a Lobby to Quick Join!");
        
        closeButton.gameObject.SetActive(true);
        closeButton.Select();
    }

    private void GameLobby_OnJoinFailed(object sender, EventArgs e)
    {
        ShowMessage("Failed to join Lobby!");
        
        closeButton.gameObject.SetActive(true);
        closeButton.Select();
    }

    private void GameLobby_OnJoinStarted(object sender, EventArgs e)
    {
        ShowMessage("Joining Lobby...");
    }

    private void GameLobby_OnCreateLobbyFailed(object sender, EventArgs e)
    {
        ShowMessage("Failed to create Lobby!");
        
        closeButton.gameObject.SetActive(true);
        closeButton.Select();
    }

    private void GameLobby_OnCreateLobbyStarted(object sender, EventArgs e)
    {
        ShowMessage("Creating Lobby...");
    }

    private void GameMultiplayer_OnFailedToJoinGame(object sender, EventArgs e)
    {
        if (NetworkManager.Singleton.DisconnectReason == "")
        {
            ShowMessage("Failed to connect");
        }
        else
        {
            ShowMessage(NetworkManager.Singleton.DisconnectReason);
        }
        
        closeButton.gameObject.SetActive(true);
        closeButton.Select();
    }

    private void ShowMessage(string message)
    {
        Show();
        closeButton.gameObject.SetActive(false);
        messageText.text = message;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameMultiplayer.Instance.OnFailedToJoinGame -= GameMultiplayer_OnFailedToJoinGame;
        GameLobby.Instance.OnCreateLobbyStarted -= GameLobby_OnCreateLobbyStarted;
        GameLobby.Instance.OnCreateLobbyFailed -= GameLobby_OnCreateLobbyFailed;
        GameLobby.Instance.OnJoinStarted -= GameLobby_OnJoinStarted;
        GameLobby.Instance.OnJoinFailed -= GameLobby_OnJoinFailed;
        GameLobby.Instance.OnQuickJoinFailed -= GameLobby_OnQuickJoinFailed;
        GameLobby.Instance.OnJoinWithCodeFailed -= GameLobby_OnJoinWithCodeFailed;
    }
}
