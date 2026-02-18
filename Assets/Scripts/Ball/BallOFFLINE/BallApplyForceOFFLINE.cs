/*
 * Handles movement of the Ball in offline gameplay.
 *
 * @author Ben Conway
 * @date May 2024
 */
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class BallApplyForceOFFLINE : MonoBehaviour
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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public void ApplyForwardsForce(Vector3 forwardsDirection)
    {
        speed = MAX_SPEED;
        
        direction = forwardsDirection.normalized;
        
        Vector3 forwardsForce = direction * speed; 
        rb.AddForce(forwardsForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        string colliderLayerName = LayerMask.LayerToName(other.gameObject.layer);
        if (colliderLayerName == GROUND_LAYER || colliderLayerName == STATIC_PLATFORM_LAYER || colliderLayerName == GOALZONE_EXTERNAL_LAYER)
        {
            Bounce(Vector3.Reflect(this.direction, other.GetContact(0).normal));
        }
    }

    private void Bounce(Vector3 forwardsDirection)
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

