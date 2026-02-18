/*
 * Removes online functionality-related singletons when player returns to MainMenu scene.
 *
 * @author Ben Conway, with inspiration from https://www.youtube.com/watch?v=7glCsF9fv3s
 * @date May 2024
 */
using Unity.Netcode;
using UnityEngine;

public class MainMenuCleanUp : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
        
        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }

        if (GameMultiplayer.Instance != null)
        {
            Destroy(GameMultiplayer.Instance.gameObject);
        }
        
        if (GameLobby.Instance != null)
        {
            Destroy(GameLobby.Instance.gameObject);
        }
    }
}
