/*
 * Handles the position of the Ball within the scene, during online gameplay.
 *
 * @author Ben Conway
 * @date May 2024
 */
using Unity.Netcode;
using UnityEngine;

public class BallTargetTransform : NetworkBehaviour
{
    private Transform targetTransform;

    public void SetTargetTransform(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }

    public void UnSetTargetTransform()
    {
        targetTransform = null;
    }

    private void LateUpdate()
    {
        if(targetTransform == null)
            return;

        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;
    }
}
