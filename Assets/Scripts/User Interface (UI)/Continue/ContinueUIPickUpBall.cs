using System;
using UnityEngine;

/// <summary>
/// Shows Continue interface when player completes objective of a 'Pick up Ball' level in the tutorial.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ContinueUIPickUpBall : ContinueUISuper
{
    [SerializeField] private PlayerPickUpController playerPickUpController;
    
    private void Start()
    {
        Hide();
        
        playerPickUpController.OnBallPickedUp += PlayerPickUpController_OnBallPickedUp;
    }

    private void PlayerPickUpController_OnBallPickedUp(object sender, EventArgs e)
    {
        LevelComplete();
    }

    private void OnDestroy()
    {
        playerPickUpController.OnBallPickedUp -= PlayerPickUpController_OnBallPickedUp;
    }
}
