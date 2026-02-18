/*
 * Handles animation of PC's visual components.
 *
 * For online gameplay.
 * 
 * @author Ben Conway
 * @date May 2024
 */
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : NetworkBehaviour
{
    private const string RUN_ANIM_PARAM = "Run";
    private const string JUMP_ANIM_PARAM = "Jump";
    
    private Animator animator;

    [SerializeField] private MovementStateSO movStaSO;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        switch (movStaSO.Value)
        {
            default:
            case MovementState.Idle:
                animator.SetBool(RUN_ANIM_PARAM, false);
                animator.SetBool(JUMP_ANIM_PARAM, false);
                break;
            case MovementState.Run:
                animator.SetBool(RUN_ANIM_PARAM, true);
                animator.SetBool(JUMP_ANIM_PARAM, false);
                break;
            case MovementState.Jump:
                animator.SetBool(RUN_ANIM_PARAM, false);
                animator.SetBool(JUMP_ANIM_PARAM, true);
                break;
        }
    }
}
