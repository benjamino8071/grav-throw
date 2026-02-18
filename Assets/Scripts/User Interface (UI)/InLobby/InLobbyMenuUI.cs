using TMPro;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Performs actions based on interaction with the InLobby interface.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class InLobbyMenuUI : NetworkBehaviour
{
    [SerializeField] private Button readyButton;
    [FormerlySerializedAs("exitLobbyButton")] [SerializeField] private Button leaveLobbyButton;
    [SerializeField] private Button settingsButton;

    [SerializeField] private MenuUISuper settingsMenu;
    
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    [SerializeField] private TextMeshProUGUI lobbyCodeText;
    [SerializeField] private TextMeshProUGUI lobbyPlayersAmountText;
    
    [SerializeField] private TextMeshProUGUI waitingForPlayersText;
    
    [FormerlySerializedAs("confirmLeaveLobbyUI")] [SerializeField] private ConfirmLeaveUI confirmLeaveUI;

    [SerializeField] private Selectable settingsNewUpSelectable;
    
    private Lobby lobby;
    
    private int numOfPlayersInLobby = 1; //Can initialise with 1 as there will always be at least one person in lobby 
    private int maxNumOfPlayers;
    
    private void Awake()
    {
        readyButton.onClick.AddListener(OnReadyButtonPressed);
        leaveLobbyButton.onClick.AddListener(OnLeaveLobbyButtonPressed);
    }
    
    private void Start()
    {
        lobby = GameLobby.Instance.GetLobby();
        
        lobbyNameText.text = lobby.Name;
        lobbyCodeText.text = "Code: " + lobby.LobbyCode;
        
        waitingForPlayersText.gameObject.SetActive(false);
        
        VivoxService.Instance.ChannelJoined += VivoxService_OnChannelJoined;
        
        if (IsServer)
        {
            maxNumOfPlayers = lobby.MaxPlayers;
            NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
            PlayerJoinedLobbyRpc(numOfPlayersInLobby, maxNumOfPlayers);
        }
        
        settingsButton.onClick.AddListener(OnSettingsButtonPressed);
    }

    private void VivoxService_OnChannelJoined(string obj)
    {
        readyButton.Select();
    }

    private void NetworkManager_OnClientConnectedCallback(ulong obj)
    {
        numOfPlayersInLobby++;
        PlayerJoinedLobbyRpc(numOfPlayersInLobby, maxNumOfPlayers);
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong obj)
    {
        numOfPlayersInLobby--;
        PlayerJoinedLobbyRpc(numOfPlayersInLobby, maxNumOfPlayers);
    }

    private void OnReadyButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        InLobbyReady.Instance.SetPlayerReady();
        
        readyButton.gameObject.SetActive(false);
        
        Navigation customNav = new Navigation();
        customNav.mode = Navigation.Mode.Explicit;
        customNav.selectOnUp = settingsNewUpSelectable;
        customNav.selectOnDown = settingsButton.navigation.selectOnDown;
        customNav.selectOnLeft = settingsButton.navigation.selectOnLeft;
        
        settingsButton.navigation = customNav;
        settingsButton.Select();
        
        waitingForPlayersText.gameObject.SetActive(true);
    }
    
    private void OnSettingsButtonPressed()
    {
        settingsMenu.Show();
    }

    private void OnLeaveLobbyButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        confirmLeaveUI.Show(LeaveLobby);
    }

    private void LeaveLobby()
    {
        GameLobby.Instance.LeaveLobby();
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene("MainMenuScene");
    }
    
    [Rpc(SendTo.ClientsAndHost)]
    private void PlayerJoinedLobbyRpc(int playersInLobbyAmount, int maxPlayerAmount)
    {
        lobbyPlayersAmountText.text = "In Lobby: "+ playersInLobbyAmount + "/" + maxPlayerAmount;
    }

    public override void OnDestroy()
    {
        VivoxService.Instance.ChannelJoined -= VivoxService_OnChannelJoined;

        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= NetworkManager_OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback -= NetworkManager_OnClientDisconnectCallback;
        }
    }
}
