/*
 * Enables player to move in multiple directions based on forwards, backwards, left and right input per fixed frame.
 *
 * For offline gameplay.
 * 
 * @author Ben Conway, with inspiration from https://www.youtube.com/watch?v=f473C43s8nE&ab_channel=Dave%2FGameDevelopment
 * @date May 2024
 */
using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementControllerOFFLINE : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private Vector3 moveDirection;

    private float moveSpeed;
    [FormerlySerializedAs("walkSpeed")] [SerializeField] private float runSpeed;
    [SerializeField] private float airSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask groundLayer;
    private bool isOnGround;

    [SerializeField] private int MAX_NUM_JUMPS = 2;
    private int numOfJumps;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    private bool readyToJump = true;
    
    [SerializeField] private MovementStateSO movStaSO;

    [FormerlySerializedAs("playerSoundEffectsOffline")] [SerializeField] private PlayerSoundEffects playerSoundEffects;

    private void Start()
    {
        PlayerInputHandler.Instance.OnJumpBindingPressed += PlayerInputHandler_OnJumpBindingPressed;
        
        numOfJumps = MAX_NUM_JUMPS;
    }

    private void Update()
    {
        StateHandler();
    }

    private void FixedUpdate()
    {
        GroundDrag();
        MovePlayer();
        //SpeedControl();
    }
    
    private void PlayerInputHandler_OnJumpBindingPressed(object sender, EventArgs e)
    {
        Jump();
    }
    
    //GROUND DRAG
    private void GroundDrag()
    {
        bool onGroundCheck = Physics.Raycast(transform.position, -transform.up, playerHeight * 0.6f, groundLayer);
        if (onGroundCheck)
        {
            //Player is on ground
            
            if (!isOnGround)
            {
                //If the player is on the ground in the current frame,
                //but was NOT on the ground in the previous frame.
                //Play ground hit sound
                playerSoundEffects.PlayGroundHitSound();
            }
            rb.drag = groundDrag;
            numOfJumps = MAX_NUM_JUMPS;
        }
        else
        {
            rb.drag = 0;
        }

        isOnGround = onGroundCheck;
    }
    
    //4D MOVEMENT
    private void MovePlayer()
    {
        Vector2 moveInput = PlayerInputHandler.Instance.GetPlayerMoveVector2Normalized();
        
        Vector3 oppositeDirection = -transform.forward;

        float tempMoveSpeed = moveSpeed;
        
        if (!isOnGround)
        {
            tempMoveSpeed = airSpeed;
        }
        
        moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;
        
        rb.AddForce(moveDirection.normalized * (tempMoveSpeed * 10f), ForceMode.Force);
    }
    
    //SPEED CONTROL
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
        
        //Limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            //Don't normalise the downwards direction!
            Vector3 downwards = -transform.up.normalized;
            if (downwards.x != 0)
            {
                //There is downwards force being applied in the x axis. Therefore do not limit x axis.
                flatVel.x = 0;
            }
            if (downwards.y != 0)
            {
                //There is downwards force being applied in the y axis. Therefore do not limit y axis.
                flatVel.y = 0;
            }
            if (downwards.z != 0)
            {
                //There is downwards force being applied in the z axis. Therefore do not limit z axis.
                flatVel.z = 0;
            }
            
            Debug.Log(flatVel.normalized);
            
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, limitedVel.y, limitedVel.z);
        }
    }
    
    //JUMP
    private void Jump()
    {
        if (readyToJump && (isOnGround || numOfJumps > 0))
        {
            readyToJump = false;
            
            rb.velocity = Vector3.zero;
        
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            
            playerSoundEffects.PlayJumpSound();

            numOfJumps--;

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void StateHandler()
    {
        if (isOnGround)
        {
            if (moveDirection == Vector3.zero)
            {
                movStaSO.Value = MovementState.Idle;
                moveSpeed = 0;
            }
            else
            {
                movStaSO.Value = MovementState.Run;
                moveSpeed = runSpeed;
            }
        }
        else
        {
            movStaSO.Value = MovementState.Jump;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * playerHeight * 0.6f);
    }

    private void OnDestroy()
    {
        PlayerInputHandler.Instance.OnJumpBindingPressed -= PlayerInputHandler_OnJumpBindingPressed;
    }
}
