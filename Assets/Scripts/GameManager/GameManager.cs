/*
 * Handles online game state as well as miscellaneous tasks - specifically connecting players to NGO network.
 *
 * @author Ben Conway
 * @date May 2024
 */
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private Transform playerPrefab;
    [SerializeField] private TeamXSpawnPoints teamASpawnPoints;
    [SerializeField] private TeamXSpawnPoints teamBSpawnPoints;
    private bool isNextSpawnForTeamA = true; //First player to spawn will be allocated to team A

    [SerializeField] private BallMarkerUI ballMarkerUI;

    private NetworkVariable<GameState> gameState = new(GameState.WaitingToStart);

    public Dictionary<ulong, string> playersOnTeamsDict = new();
    
    private void Awake()
    {
        Instance = this;
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneManager_OnLoadEventCompleted;
        }
        
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong obj)
    {
        if (!IsServer)
        {
            Time.timeScale = 0;
        }
    }

    private void SceneManager_OnLoadEventCompleted(string scenename, LoadSceneMode loadscenemode, List<ulong> clientscompleted, List<ulong> clientstimedout)
    {
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if (isNextSpawnForTeamA)
            {
                AddToPlayersOnTeamsDictRpc(clientId, "A");

                Transform nextSpawnPoint = teamASpawnPoints.GetNextSpawnPoint();
                
                Transform playerTransform = Instantiate(playerPrefab, nextSpawnPoint);
                Vector3 spawnPointRotation = new Vector3
                (
                    nextSpawnPoint.localRotation.x,
                    nextSpawnPoint.localRotation.y,
                    nextSpawnPoint.localRotation.z
                );
                playerTransform.Rotate(spawnPointRotation, Space.Self);
                
                playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
            }
            else
            {
                AddToPlayersOnTeamsDictRpc(clientId, "B");
                
                Transform nextSpawnPoint = teamBSpawnPoints.GetNextSpawnPoint();
                
                //For team B, player is upside down (in world space)
                Transform playerTransform = Instantiate(playerPrefab, nextSpawnPoint);
                Vector3 spawnPointRotation = new Vector3
                (
                    nextSpawnPoint.localRotation.x,
                    nextSpawnPoint.localRotation.y,
                    nextSpawnPoint.localRotation.z
                );
                playerTransform.Rotate(spawnPointRotation, Space.Self);
                
                playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
            }

            isNextSpawnForTeamA = !isNextSpawnForTeamA;
        }
        SetBallMarkerUIRpc();
        SpawnBall();

        SetStateToCountdownToStart();
    }
    
    [Rpc(SendTo.ClientsAndHost)]
    private void AddToPlayersOnTeamsDictRpc(ulong clientId, string teamLetter)
    {
        Debug.Log("Setting team letter for player: "+clientId);
        
        playersOnTeamsDict[clientId] = teamLetter;
        
        Debug.Log("Team letter set.");
    }
    
    [Rpc(SendTo.Everyone, RequireOwnership = false)]
    private void SetBallMarkerUIRpc()
    {
        ballMarkerUI.Show();
    }

    private void SpawnBall()
    {
        BallManager.Instance.SpawnBall();
    }
    
    public void SetStateToWaitingToStart()
    {
        gameState.Value = GameState.WaitingToStart;
    }
    
    public void SetStateToCountdownToStart()
    {
        gameState.Value = GameState.CountdownToStart;
    }
    
    public void SetStateToGamePlaying()
    {
        gameState.Value = GameState.GamePlaying;
    }
    
    public void SetStateToGameOver()
    {
        gameState.Value = GameState.GameOver;
    }

    public bool IsStateWaitingToStart()
    {
        return gameState.Value == GameState.WaitingToStart;
    }

    public bool IsStateCountdownToStart()
    {
        return gameState.Value == GameState.CountdownToStart;
    }

    public bool IsStateGamePlaying()
    {
        return gameState.Value == GameState.GamePlaying;
    }

    public bool IsStateGameOver()
    {
        return gameState.Value == GameState.GameOver;
    }

    public override void OnNetworkDespawn()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback -= NetworkManager_OnClientDisconnectCallback;
    }
}
