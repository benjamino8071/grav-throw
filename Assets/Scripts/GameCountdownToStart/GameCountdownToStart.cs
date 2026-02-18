/*
 * Handles countdown timer in online gameplay.
 *
 * @author Ben Conway
 * @date May 2024
 */
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameCountdownToStart : NetworkBehaviour
{
    private NetworkVariable<float> countdownTimer = new(4);
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Update()
    {
        if (!IsServer)
        {
            return;
        }

        UpdateCountdown();
    }
    
    private void UpdateCountdown()
    {
        countdownTimer.Value -= Time.deltaTime;
        
        UpdateCountdownVisualRpc();

        if (countdownTimer.Value <= -1)
        {
            HideRpc();
        }
        else if (countdownTimer.Value <= 0)
        {
            GameManager.Instance.SetStateToGamePlaying();
        }
    }
    
    [Rpc(SendTo.ClientsAndHost)]
    private void UpdateCountdownVisualRpc()
    {
        if (countdownTimer.Value <= 0)
        {
            countdownText.text = "GO!";
        }
        else if(GameManager.Instance.IsStateCountdownToStart())
        {
            countdownText.text = Mathf.Ceil(countdownTimer.Value).ToString();
        }
    }
    
    [Rpc(SendTo.ClientsAndHost)]
    private void HideRpc()
    {
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
