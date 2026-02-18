/*
 * Handles movement of the Ball in online gameplay.
 *
 * @author Ben Conway
 * @date May 2024
 */
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class BallApplyForce : NetworkBehaviour
{
    private const string GROUND_LAYER = "Ground";
    private const string STATIC_PLATFORM_LAYER = "StaticPlatform";
    private const string GOALZONE_EXTERNAL_LAYER = "GoalzoneExternal";
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float MAX_SPEED;
    
    private float speed;

    private Vector3 direction;
    
    private Rigidbody rb;

    [FormerlySerializedAs("ballSoundEffectsOffline")] [SerializeField] private BallSoundEffects ballSoundEffects;

    public override void OnNetworkSpawn()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ApplyForwardsForce(Vector3 forwardsDirection)
    {
        ApplyForwardsForceServRpc(forwardsDirection);
    }
    
    [Rpc(SendTo.Server)]
    private void ApplyForwardsForceServRpc(Vector3 forwardsDirection)
    {
        ApplyForwardsForceCliRpc(forwardsDirection);
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void ApplyForwardsForceCliRpc(Vector3 forwardsDirection)
    {
        speed = MAX_SPEED;
        
        direction = forwardsDirection.normalized;
        
        Vector3 forwardsForce = direction * speed; 
        rb.AddForce(forwardsForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!IsServer)
        {
            return;
        }
        
        string colliderLayerName = LayerMask.LayerToName(other.gameObject.layer);
        if (colliderLayerName == GROUND_LAYER || colliderLayerName == STATIC_PLATFORM_LAYER || colliderLayerName == GOALZONE_EXTERNAL_LAYER)
        {
            BounceRpc(Vector3.Reflect(direction, other.GetContact(0).normal));
        }
    }
    
    [Rpc(SendTo.ClientsAndHost)]
    private void BounceRpc(Vector3 forwardsDirection)
    {
        direction = forwardsDirection.normalized;
        
        rb.velocity = Vector3.zero;
        
        //Ball slows down as it hits more surfaces
        speed /= 2;
        
        Vector3 forwardsForce = direction * speed;
        rb.AddForce(forwardsForce, ForceMode.Impulse);
        
        ballSoundEffects.PlayBallWallBounceSound();
    }
}