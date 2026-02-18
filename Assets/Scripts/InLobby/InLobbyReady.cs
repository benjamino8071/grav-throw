/*
 * Handles updating InLobby system when player presses 'ready' button.
 *
 * @author Ben Conway, with inspiration from https://www.youtube.com/watch?v=7glCsF9fv3s
 * @date May 2024
 */
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class InLobbyReady : NetworkBehaviour
{
    public static InLobbyReady Instance { get; private set; }

    public event EventHandler OnReadyChanged;
    
    private Dictionary<ulong, bool> playerReadyDictionary;

    private void Awake()
    {
        Instance = this;
        
        playerReadyDictionary = new Dictionary<ulong, bool>();
    }

    public void SetPlayerReady()
    {
        SetPlayerReadyRpc();
    }
    
    [Rpc(SendTo.Server, RequireOwnership = false)]
    private void SetPlayerReadyRpc(RpcParams rpcParams = default)
    {
        SetPlayerReadyRpc(rpcParams.Receive.SenderClientId);
        playerReadyDictionary[rpcParams.Receive.SenderClientId] = true;

        bool allClientsReady = true;
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if (!playerReadyDictionary.ContainsKey(clientId) || !playerReadyDictionary[clientId])
            {
                //This player is NOT ready
                allClientsReady = false;
                break;
            }
        }

        if (allClientsReady)
        {
            GameLobby.Instance.DeleteLobby();

            int mapChoice = UnityEngine.Random.Range(1, 4);
            string mapChoiceName = mapChoice + "MapScene";
            
            NetworkManager.Singleton.SceneManager.LoadScene(mapChoiceName, LoadSceneMode.Single);
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void SetPlayerReadyRpc(ulong clientId)
    {
        playerReadyDictionary[clientId] = true;
        
        OnReadyChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool IsPlayerReady(ulong clientId)
    {
        return playerReadyDictionary.ContainsKey(clientId) && playerReadyDictionary[clientId];
    }
}
