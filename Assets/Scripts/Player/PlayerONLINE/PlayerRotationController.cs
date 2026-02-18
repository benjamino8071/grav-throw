/*
 * Enables player to change their PC's direction of gravity. The PC auto-orients to accommodate this new direction.
 *
 * For online gameplay.
 * 
 * @author Ben Conway
 * @date May 2024
 */
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRotationController : NetworkBehaviour
{
    private float yRotation;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask staticLayer;
    
    private bool canRotate = true;
    
    //NEW VARIABLES
    [SerializeField] private float gravity = 9.81f;
    
    [SerializeField] private bool autoOrient = false;
    [SerializeField] private float autoOrientSpeed = 1f;

    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        RotationMechanic();
    }
    
    private void RotationMechanic()
    {
        
        RaycastHit hitPoint;
        float distanceFromWall = 5f;
        //If player is in front of a static object then we don't want player to rotate (even if there is ground object behind the static object)
        if (Physics.Raycast(transform.position, transform.forward, out hitPoint,
                distanceFromWall, staticLayer))
        {
            return;
        }
        
        //Player is facing new surface
        if (Physics.Raycast(transform.position, transform.forward, out hitPoint,
                distanceFromWall, groundLayer) && GameManager.Instance.IsStateGamePlaying())
        {
            Vector3 newGravityDirection = -hitPoint.normal;
            rb.AddForce(newGravityDirection.normalized * gravity * (rb.mass));
            
            if (autoOrient)
                AutoOrient(newGravityDirection);
        }
        //Player is not facing new surface
        else if (Physics.Raycast(transform.position, -transform.up, out hitPoint,
                     Mathf.Infinity, groundLayer))
        {
            Vector3 orientOnSurface = -hitPoint.normal;
            rb.AddForce(orientOnSurface.normalized * gravity * (rb.mass));
            
            if (autoOrient)
                AutoOrient(orientOnSurface);
        }
    }
    
    private void AutoOrient(Vector3 down)
    {
        Quaternion orientationDirection = Quaternion.FromToRotation(-transform.up, down) * transform.rotation;
        transform.rotation =
            Quaternion.Slerp(transform.rotation, orientationDirection, autoOrientSpeed * Time.deltaTime);
    }

    public void RotateAroundYAxis(float mouseX)
    {
        yRotation += mouseX;
        if (yRotation > 360 || yRotation < -360)
        {
            yRotation /= 360;
        }
        
        transform.Rotate(Vector3.up * mouseX, Space.Self);
    }
}

