using System;
using UnityEngine;

/// <summary>
/// Provides specific functionality for the pause interface of a 'pick up Ball' level (of the tutorial).
/// 
/// Notably, when the player picks up the Ball they will be unable to open the pause interface.
/// This is because the 'continue' interface is being shown instead.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class PauseUIBallPickUpLevel : PauseUI
{
    [SerializeField] private PlayerPickUpController playerPickUpController;
    
    private bool ballPickedUp;
    
    private void Start()
    {
        PlayerInputHandler.Instance.OnPauseBindingPressed += PlayerInputHandler_OnPauseBindingPressed;
        playerPickUpController.OnBallPickedUp += PlayerPickUpController_OnBallPickedUp;
        
        Hide();
    }

    private void PlayerPickUpController_OnBallPickedUp(object sender, EventArgs e)
    {
        ballPickedUp = true;
    }

    private void PlayerInputHandler_OnPauseBindingPressed(object sender, EventArgs e)
    {
        if (!settingsUI.gameObject.activeSelf && !ballPickedUp)
        {
            Show();
        }
    }
    
    private void OnDestroy()
    {
        PlayerInputHandler.Instance.OnPauseBindingPressed -= PlayerInputHandler_OnPauseBindingPressed;
        playerPickUpController.OnBallPickedUp -= PlayerPickUpController_OnBallPickedUp;
    }
}
