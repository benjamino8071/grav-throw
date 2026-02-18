/*
 * Controls field of view for PC's camera object based on value set by player in settings menu.
 *
 * @author Ben Conway
 * @date May 2024
 */
using Cinemachine;
using UnityEngine;

public class PlayerCameraAdjustFieldOfView : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private FloatVariableSO fovSliderSO;
    private float zoomSpeed = 1f;
    
    private void Update()
    {
        UpdateFieldOfView();
    }
    
    private void UpdateFieldOfView()
    {
        cinemachineVirtualCamera.m_Lens.FieldOfView = fovSliderSO.GetValue();
    }
}
