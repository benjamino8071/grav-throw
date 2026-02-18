/*
 * Special script used during tutorial scenes, when the objective of a level is to pick up the ball.
 *
 * @author Ben Conway
 * @date May 2024
 */
using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class PlayerPickUpController : MonoBehaviour
{
    public event EventHandler OnBallPickedUp;
    
    [Header("References")]
    [SerializeField] private ListWithStringsSO ballPickUpSO;
    [SerializeField] private Transform playerBallSpawnPoint;

    [Header("Pick up ball")]
    [SerializeField] private float overlapSphereRadius;
    [SerializeField] private LayerMask ballLayer;
    
    private Rigidbody rb;

    [FormerlySerializedAs("playerSoundEffectsOffline")] [SerializeField] private PlayerSoundEffects playerSoundEffects;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Player is not holding ball and there is no instance to confirm the player has recently thrown a ball
        //Therefore check if player is near ball - if so, player can pick it up
        Collider[] ballsCollided = Physics.OverlapSphere(transform.position, overlapSphereRadius, ballLayer);
            
        //If player has chosen AUTO, then this is true always
        //If player has chosen MANUAL, then this is true if they are pressing the PickUp button, false otherwise
        bool playerBallPickUpChoice =
            (ballPickUpSO.GetCurrentValue() == "MANUAL" && PlayerInputHandler.Instance.isPickUpButtonDown) ||
            ballPickUpSO.GetCurrentValue() == "AUTO";
            
        if (ballsCollided.Length > 0 && playerBallPickUpChoice)
        {
            BallPickUp();
        }
    }
    
    private void BallPickUp()
    {
        BallManagerOFFLINE.Instance.BallPickedUp(playerBallSpawnPoint, gameObject);
        
        playerSoundEffects.PlayBallPickUpSound();
        
        SetToHoldingBall();
        
        OnBallPickedUp?.Invoke(this, EventArgs.Empty);
    }
    
    private void SetToHoldingBall()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll; //Player cannot move while they hold ball
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        Gizmos.DrawWireSphere(transform.position, overlapSphereRadius);
    }
}
