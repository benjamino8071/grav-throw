using TMPro;
using UnityEngine;

/// <summary>
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class GameTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameTimerText;
    private bool isTimerEnd = false;
    
    public void UpdateTimerText(float minutes, float seconds)
    {
        if (isTimerEnd)
        {
            return;
        }
        
        if (seconds <= 9)
        {
            gameTimerText.SetText(minutes+":0"+Mathf.Ceil(seconds));
        }
        else
        {
            gameTimerText.SetText(minutes+":"+Mathf.Ceil(seconds));
        }
    }

    public void TimerEnd()
    {
        isTimerEnd = true;
        
        gameTimerText.SetText("0:00");
    }
}
