using Unity.Services.Authentication;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Displays voice and text chat UI based on connection status with Vivox channel.
///
/// For online gameplay.
/// 
/// @author Ben Conway, with inspiration from the Vivox sample package
/// @date May 2024
/// </summary>
public class GameVivoxUI : MonoBehaviour
{
    public Button LogoutButton;
    public GameObject LobbyScreen;
    public GameObject ConnectionIndicatorDot;
    public GameObject ConnectionIndicatorText;

    EventSystem m_EventSystem;    Image m_ConnectionIndicatorDotImage;
    Text m_ConnectionIndicatorDotText;
    
    void Start()
    {
        m_EventSystem = EventSystem.current;
        if (!m_EventSystem)
        {
            Debug.LogError("Unable to find EventSystem object.");
        }
        m_ConnectionIndicatorDotImage = ConnectionIndicatorDot.GetComponent<Image>();
        if (!m_ConnectionIndicatorDotImage)
        {
            Debug.LogError("Unable to find ConnectionIndicatorDot Image object.");
        }
        
        /*
        m_ConnectionIndicatorDotText = ConnectionIndicatorText.GetComponent<Text>();
        if (!m_ConnectionIndicatorDotText)
        {
            Debug.LogError("Unable to find ConnectionIndicatorText Text object.");
        }
        */
        
        VivoxService.Instance.LoggedOut += OnUserLoggedOut;
        VivoxService.Instance.ConnectionRecovered += OnConnectionRecovered;
        VivoxService.Instance.ConnectionRecovering += OnConnectionRecovering;
        VivoxService.Instance.ConnectionFailedToRecover += OnConnectionFailedToRecover;
        
        m_ConnectionIndicatorDotImage.color = Color.green;
        //m_ConnectionIndicatorDotText.text = "Connected";

        LogoutButton.onClick.AddListener(() => { LogoutOfVivoxServiceAsync(); });
    }

    void OnDestroy()
    {
        VivoxService.Instance.LoggedOut -= OnUserLoggedOut;
        VivoxService.Instance.ConnectionRecovered -= OnConnectionRecovered;
        VivoxService.Instance.ConnectionRecovering -= OnConnectionRecovering;
        VivoxService.Instance.ConnectionFailedToRecover -= OnConnectionFailedToRecover;

        LogoutButton.onClick.RemoveAllListeners();
    }

    async void LogoutOfVivoxServiceAsync()
    {
        LogoutButton.interactable = false;

        await VivoxService.Instance.LogoutAsync();
        AuthenticationService.Instance.SignOut();
    }

    async void OnUserLoggedIn()
    {
        LobbyScreen.SetActive(true);
        LogoutButton.interactable = true;
        m_EventSystem.SetSelectedGameObject(LogoutButton.gameObject, null);
    }

    void OnUserLoggedOut()
    {
        LobbyScreen.SetActive(false);
    }

    void OnConnectionRecovering()
    {
        m_ConnectionIndicatorDotImage.color = Color.yellow;
        //m_ConnectionIndicatorDotText.text = "Connection Recovering";
    }

    void OnConnectionRecovered()
    {
        m_ConnectionIndicatorDotImage.color = Color.green;
        //m_ConnectionIndicatorDotText.text = "Connection Recovered";
    }

    void OnConnectionFailedToRecover()
    {
        m_ConnectionIndicatorDotImage.color = Color.black;
        //m_ConnectionIndicatorDotText.text = "Connection Failed to Recover";
    }
}
