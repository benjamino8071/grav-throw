using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Displays HostDisconnect interface when the Host of the network leaves the lobby.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class HostDisconnectUI : MonoBehaviour
{
    [SerializeField] private Button exitToMainMenuButton;

    private void Awake()
    {
        exitToMainMenuButton.onClick.AddListener(OnExitToMainMenuButtonPressed);
    }

    private void Start()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
        
        Hide();
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        if (!NetworkManager.Singleton.IsServer)
        {
            //Server is shutting down
            Show();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnExitToMainMenuButtonPressed()
    {
        Debug.Log("Exit to main menu button pressed!");
        SceneManager.LoadScene("MainMenuScene");
    }

    private void Show()
    {
        gameObject.SetActive(true);
        exitToMainMenuButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback -= NetworkManager_OnClientDisconnectCallback;
    }
}
