/*
 * Handles timer system in online gameplay. The timer will countdown each second until it reaches 0; at that point,
 * the game is finished.
 */
using Unity.Netcode;
using UnityEngine;

public class GameTimerManager : NetworkBehaviour
{
    public static GameTimerManager Instance { get; private set; }
    
    [SerializeField] private GameTimerUI gamerTimerUI;
    
    private NetworkVariable<float> timerMinutes = new(3);
    private NetworkVariable<float> timerSeconds = new(0);
    
    private bool timerFinished;
    private bool startTimer;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        timerFinished = false;

        if (IsServer)
        {
            UpdateTimerVisualRpc();
        }
    }

    private void Update()
    {
        if (!IsServer || !GameManager.Instance.IsStateGamePlaying())
        {
            return;
        }
        
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        //Prevents timer from updating on-screen once game is over
        if(timerFinished)
            return;
        
        timerSeconds.Value -= Time.deltaTime;
        
        if (timerSeconds.Value < -1)
        {
            if (timerMinutes.Value == 0)
            {
                //Minutes and seconds are on zero - therefore timer is out!
                TimerEnd();
                return;
            }
            
            timerMinutes.Value -= 1;
            timerSeconds.Value = 59f;
        }
        
        UpdateTimerVisualRpc();
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void UpdateTimerVisualRpc()
    {
        gamerTimerUI.UpdateTimerText(timerMinutes.Value, timerSeconds.Value);
    }
    
    private void TimerEnd()
    {
        timerFinished = true;
        
        timerMinutes.Value = 0;
        timerSeconds.Value = 0;
        
        TimeEndRpc();
        
        GameOverManager.Instance.GameOver();
    }
    
    [Rpc(SendTo.ClientsAndHost)]
    private void TimeEndRpc()
    {
        gamerTimerUI.TimerEnd();
    }
}
