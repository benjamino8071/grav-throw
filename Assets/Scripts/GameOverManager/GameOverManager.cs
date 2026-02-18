/*
 * Handles functionality after timer has expired in online gameplay.
 *
 * @author Ben Conway
 * @date May 2024
 */
using System;
using Unity.Netcode;
using UnityEngine;

public class GameOverManager : NetworkBehaviour
{
    public event EventHandler OnGameOver;
    
    public static GameOverManager Instance { get; private set; }
    
    [SerializeField] private GameOverUI gameOverUI;
    
    private void Awake()
    {
        Instance = this;
    }

    public void GameOver()
    {
        GameManager.Instance.SetStateToGameOver();
        
        GameOverRpc();
    }
    
    [Rpc(SendTo.ClientsAndHost)]
    private void GameOverRpc()
    {
        Time.timeScale = 0;
        OnGameOver?.Invoke(this, EventArgs.Empty);
    }
}
