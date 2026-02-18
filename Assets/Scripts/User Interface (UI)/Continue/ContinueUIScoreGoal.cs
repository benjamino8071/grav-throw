using System;
using UnityEngine;

/// <summary>
/// Shows Continue interface when player completes objective of a 'Score goal' level in the tutorial.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ContinueUIScoreGoal : ContinueUISuper
{
    [SerializeField] private GoalzoneOFFLINE goalZoneOffline;
    
    private void Start()
    {
        Hide();
        
        goalZoneOffline.OnGoalScored += GoalzoneOfflineOnOnGoalScored;
    }

    private void GoalzoneOfflineOnOnGoalScored(object sender, EventArgs e)
    {
        LevelComplete();
    }

    private void OnDestroy()
    {
        goalZoneOffline.OnGoalScored -= GoalzoneOfflineOnOnGoalScored;
    }
}
