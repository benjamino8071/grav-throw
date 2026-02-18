/*
 * Handles action performed when Ball object collides with Goalzone in offline gameplay.
 *
 * @author Ben Conway
 * @date May 2024
 */
using System;
using UnityEngine;

public class GoalzoneOFFLINE : MonoBehaviour
{
    public event EventHandler OnGoalScored;
    
    private const string BALL_LAYER = "Ball";

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(BALL_LAYER))
        {
            BallManagerOFFLINE.Instance.ResetBall();
            OnGoalScored?.Invoke(this, EventArgs.Empty);
        }
    }
}