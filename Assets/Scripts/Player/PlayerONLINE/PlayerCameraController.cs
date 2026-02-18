/*
 * Enables player to control the first-person camera object using either mouse or analog stick as input per fixed frame.
 *
 * For online gameplay.
 *
 * @author Ben Conway
 * @date May 2024
 */
using Unity.Netcode;
using UnityEngine;

public class PlayerCameraController : NetworkBehaviour
{
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private BooleanVariableSO invCamX;
    [SerializeField] private BooleanVariableSO invCamY;
    
    [SerializeField] private FloatVariableSO camXSensSO;
    [SerializeField] private FloatVariableSO camYSensSO;
    
    [SerializeField] private PlayerRotationController playerRotationController;

    private float xRotation;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void Update()
    {
        RotateAroundXAxis();
    }

    private void RotateAroundXAxis()
    {
        int camXInverted = 1;
        int camYInverted = 1;
        
        if (invCamX.GetValue())
        {
            camXInverted = -1;
        }

        if (invCamY.GetValue())
        {
            camYInverted = -1;
        }
        
        Vector2 cameraInput = PlayerInputHandler.Instance.GetCameraInputVector2();
        float cameraX = cameraInput.x * Time.deltaTime * camXSensSO.GetValue() * camXInverted;
        float cameraY = cameraInput.y * Time.deltaTime * camYSensSO.GetValue() * camYInverted;
        playerRotationController.RotateAroundYAxis(cameraX);
        
        xRotation -= cameraY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector3 lineStart = transform.position;
        float lineLength = 20f;
        Vector3 lineEndForward = transform.position + transform.forward * lineLength;
        
        Gizmos.DrawLine(lineStart, lineEndForward);
    }
}