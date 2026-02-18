using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles performance of actions for different components of the Lobby menu interface.
/// 
/// Players have the option to create a lobby, join a public lobby directly, or search for a private lobby
/// using that lobby's 'join code'.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class LobbyMenuUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button createLobbyButton;
    [SerializeField] private Button quickJoinButton;
    [SerializeField] private Button joinCodeButton;
    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private LobbyCreateUI lobbyCreateUI;
    [SerializeField] private Transform lobbyListContainer;
    [SerializeField] private Transform singleLobbyListTemplate;

    [SerializeField] private List<LobbyListSingleUI> lobbyButtonsList;
    private int currentNumOfLobbies = 0;
    
    private void Awake()
    {
        mainMenuButton.onClick.AddListener(OnMainMenuButtonPressed);
        createLobbyButton.onClick.AddListener(OnCreateLobbyButtonPressed);
        quickJoinButton.onClick.AddListener(OnQuickJoinButtonPressed);
        joinCodeButton.onClick.AddListener(OnJoinCodeButtonPressed);
        
        singleLobbyListTemplate.gameObject.SetActive(false);
    }

    private void OnMainMenuButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();
            
        GameLobby.Instance.LeaveLobby();
        SceneManager.LoadScene("MainMenuScene");
    }

    private void OnCreateLobbyButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        lobbyCreateUI.Show();
    }

    private void OnQuickJoinButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        GameLobby.Instance.QuickJoin();
    }

    private void OnJoinCodeButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        GameLobby.Instance.JoinWithCode(joinCodeInputField.text);
    }

    private void Start()
    {
        playerNameText.SetText(GameMultiplayer.Instance.GetPlayerName());
        
        GameLobby.Instance.OnLobbyListChanged += GameLobby_OnLobbyListChanged;
        UpdateLobbyList(new List<Lobby>());
        
        createLobbyButton.Select(); //For gamepad
    }

    private void GameLobby_OnLobbyListChanged(object sender, GameLobby.OnLobbyListChangedEventArgs e)
    {
        UpdateLobbyList(e.lobbyList);
    }

    private void UpdateLobbyList(List<Lobby> lobbyList)
    {
        Debug.Log("UpdateLobbyList method called");
        
        foreach (LobbyListSingleUI lobbyListSingleUI in lobbyButtonsList)
        {
            lobbyListSingleUI.UnsetLobby();
        }

        if (lobbyList.Count >= lobbyButtonsList.Count)
        {
            for (int i = 0; i < lobbyButtonsList.Count; i++)
            {
                lobbyButtonsList[i].SetLobby(lobbyList[i]);
            }
        }
        else
        {
            for (int i = 0; i < lobbyList.Count; i++)
            {
                lobbyButtonsList[i].SetLobby(lobbyList[i]);
            }
        }
    }

    private void OnDestroy()
    {
        GameLobby.Instance.OnLobbyListChanged -= GameLobby_OnLobbyListChanged;
    }
}