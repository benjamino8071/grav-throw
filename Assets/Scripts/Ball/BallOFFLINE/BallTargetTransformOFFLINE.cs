/*
 * Handles the position of the Ball within the scene, during offline gameplay.
 *
 * @author Ben Conway
 * @date May 2024
 */
using UnityEngine;

public class BallTargetTransformOFFLINE : MonoBehaviour
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

