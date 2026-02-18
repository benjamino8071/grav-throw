using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles display of information for a single public lobby in the 'Lobby' scene. Players looking to
/// join a lobby can choose which one to join (if there is more than one).
/// 
/// @author Ben Conway, with inspiration from https://www.youtube.com/watch?v=7glCsF9fv3s
/// @date May 2024
/// </summary>
public class LobbyListSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    private Lobby lobby;
    
    [SerializeField] private StringVariableSO difficultySO;

    private bool lobbySet;
    
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        if (lobbySet)
        {
            GameLobby.Instance.JoinWithId(lobby.Id);
        }
    }

    public void SetLobby(Lobby lobby)
    {
        this.lobby = lobby;
        
        if (lobby.Data != null)
        {
            int halfOfMaxPlayers = this.lobby.MaxPlayers / 2;
            string vsModeText = halfOfMaxPlayers + " vs " + halfOfMaxPlayers;
            
            string diff = lobby.Data[difficultySO.Value].Value;
            string lobbyText = lobby.Name + " - " + diff + " - " + vsModeText + " - " + lobby.Players.Count + "/" + lobby.MaxPlayers;
            lobbyNameText.text = lobbyText;
        }

        lobbySet = true;
    }

    public void UnsetLobby()
    {
        lobbySet = false;
        lobbyNameText.text = "Finding lobby...";
    }
}
