using System;
using UnityEngine;

/// <summary>
/// Provides specific functionality for the pause interface of a 'score goal' level (of the tutorial).
/// 
/// Notably, when the player scores a goal they will be unable to open the pause interface.
/// This is because the 'continue' interface is being shown instead.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class PauseUIScoreGoalLevel : PauseUI
{
    [SerializeField] private GoalzoneOFFLINE goalzoneOffline;
    
    private bool goalScored;
    
    private void Start()
    {
        PlayerInputHandler.Instance.OnPauseBindingPressed += PlayerInputHandler_OnPauseBindingPressed;
        goalzoneOffline.OnGoalScored += GoalzoneOffline_OnGoalScored;
        
        Hide();
    }

    private void GoalzoneOffline_OnGoalScored(object sender, EventArgs e)
    {
        goalScored = true;
    }

    private void PlayerInputHandler_OnPauseBindingPressed(object sender, EventArgs e)
    {
        if (!settingsUI.gameObject.activeSelf && !goalScored)
        {
            Show();
        }
    }
    
    private void OnDestroy()
    {
        PlayerInputHandler.Instance.OnPauseBindingPressed -= PlayerInputHandler_OnPauseBindingPressed;
        goalzoneOffline.OnGoalScored += GoalzoneOffline_OnGoalScored;
    }
}
