using UnityEngine;

/// <summary>
/// If players join online game too early, they will be shown information stating that the network
/// is waiting for other players to join.
///
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class WaitingForPlayersUI : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.Instance.IsStateGamePlaying())
        {
            Show();
        }
        else
        {
            Hide();
        }
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
