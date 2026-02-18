/*
 * Enables player to pick up, hold and throw the Ball whilst in offline gameplay.
 * 
 * @author Ben Conway, with inspiration from https://www.youtube.com/watch?v=F20Sr5FlUlE&t=197s&ab_channel=Dave%2FGameDevelopment
 * @date May 2024
 */
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class PlayerHoldAndThrowBallControllerOFFLINE : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform ballSpawnPoint;

    [Header("Pick up ball")]
    [SerializeField] private float overlapSphereRadius;
    [SerializeField] private LayerMask ballLayer;
    
    private bool isHoldingBall;
    
    private const float CAN_HOLD_BALL_DELAY_TIME_MAX = 1f;
    private float canHoldBallDelayTime;
    
    private Rigidbody rb;

    [FormerlySerializedAs("playerSoundEffectsOffline")] [SerializeField] private PlayerSoundEffects playerSoundEffects;

    [SerializeField] private BooleanVariableSO showBallMarkerSO;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        SetToNotHoldingBall();

        canHoldBallDelayTime = CAN_HOLD_BALL_DELAY_TIME_MAX;
    }

    private void FixedUpdate()
    {
        if (canHoldBallDelayTime < CAN_HOLD_BALL_DELAY_TIME_MAX)
        {
            canHoldBallDelayTime -= Time.deltaTime;
            if (canHoldBallDelayTime <= 0)
            {
                canHoldBallDelayTime = CAN_HOLD_BALL_DELAY_TIME_MAX;
            }
        }
        else if(!isHoldingBall)
        {
            //Player is not holding ball and there is no instance to confirm the player has recently thrown a ball
            //Therefore check if player is near ball - if so, player can pick it up
            Collider[] ballsCollided = Physics.OverlapSphere(transform.position, overlapSphereRadius, ballLayer);
            
            if (ballsCollided.Length > 0)
            {
                BallPickUp();
            }
        }
        
        if(PlayerInputHandler.Instance.isThrowButtonDown && isHoldingBall)
        {
            Throw();
        }
    }
    
    private void BallPickUp()
    {
        BallManagerOFFLINE.Instance.BallPickedUp(ballSpawnPoint, gameObject);
        
        playerSoundEffects.PlayBallPickUpSound();
        
        SetToHoldingBall();
    }

    private void Throw()
    {
        // calculate direction
        Vector3 forwardsDirection = playerCameraTransform.transform.forward;
        
        BallManagerOFFLINE.Instance.BallThrown(forwardsDirection);
        
        playerSoundEffects.PlayBallThrowSound();
        
        SetToNotHoldingBall();
    }
    
    private void SetToHoldingBall()
    {
        //ShowVisual();
        showBallMarkerSO.SetToFalse();
        isHoldingBall = true;
        rb.constraints = RigidbodyConstraints.FreezeAll; //Player cannot move while they hold ball
    }
    
    public void SetToNotHoldingBall()
    {
        //Start timer for player before they can pick up ball again
        //(stops player picking up ball as soon as they throw it)
        canHoldBallDelayTime -= Time.deltaTime;
        
        //HideVisual();
        showBallMarkerSO.SetToTrue();
        isHoldingBall = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation; //Must keep rigidbody's rotation frozen
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        Gizmos.DrawWireSphere(transform.position, overlapSphereRadius);
    }
}

