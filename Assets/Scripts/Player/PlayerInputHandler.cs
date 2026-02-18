/*
 * Stores current state of input bindings for use in other gameplay systems, specifically the Player controls.
 *
 * @author Ben Conway, with inspiration from https://www.youtube.com/watch?v=7glCsF9fv3s
 * @date May 2024
 */
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public bool ignoreInput;
    
    public event EventHandler OnPauseBindingPressed, OnJumpBindingPressed;
    
    public static PlayerInputHandler Instance { get; private set; }

    [SerializeField] private PlayerInputActionsSO plrInpActsSO;

    [NonSerialized] public bool isJumpButtonDown = false;
    [NonSerialized] public bool isRotateButtonDown = false;
    [NonSerialized] public bool isThrowButtonDown = false;
    [NonSerialized] public bool isPickUpButtonDown = false;
    
    //FOR DEBUGGING ONLY
    [NonSerialized] public bool isEndGameButtonDown = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        plrInpActsSO.Value.PlayerActionMap.Move.performed += MoveOnperformed;
        
        plrInpActsSO.Value.PlayerActionMap.Jump.performed += JumpOnperformed;
        plrInpActsSO.Value.PlayerActionMap.Jump.canceled += JumpOncanceled;
        
        plrInpActsSO.Value.PlayerActionMap.Throw.performed += ThrowOnperformed;
        plrInpActsSO.Value.PlayerActionMap.Throw.canceled += ThrowOncanceled;
        
        plrInpActsSO.Value.PlayerActionMap.MouseLook.performed += MouseLookOnperformed;
        
        plrInpActsSO.Value.PlayerActionMap.Pause.performed += PauseOnperformed;
    }

    private void MoveOnperformed(InputAction.CallbackContext obj)
    {
        if (obj.control.device.name == "Keyboard" || obj.control.device.name == "Mouse")
        {
            //Using keyboard or mouse
            UpdateControllerInUse(obj.control.device.name); //doesn't matter whether we feed function 'Keyboard' or 'Mouse' argument
        }
        else
        {
            //Using gamepad. We need to check they are moving the gamepad though
            bool movedGamepadStick = Mathf.Abs(GetPlayerMoveVector2Normalized().x) >= 0.5f || Mathf.Abs(GetPlayerMoveVector2Normalized().y) >= 0.5f;
            if (movedGamepadStick)
            {
                Debug.Log("Player is moving with gamepad!");
                UpdateControllerInUse("Gamepad");
            }
        }
    }

    private void JumpOnperformed(InputAction.CallbackContext obj)
    {
        if (ignoreInput)
        {
            return;
        }
        
        UpdateControllerInUse(obj.control.device.name);
        
        OnJumpBindingPressed?.Invoke(this, EventArgs.Empty);
        isJumpButtonDown = true;
    }
    
    private void JumpOncanceled(InputAction.CallbackContext obj)
    {
        isJumpButtonDown = false;
    }
    
    private void ThrowOnperformed(InputAction.CallbackContext obj)
    {
        if (ignoreInput)
        {
            return;
        }
        
        UpdateControllerInUse(obj.control.device.name);
        
        isThrowButtonDown = true;
    }

    private void ThrowOncanceled(InputAction.CallbackContext obj)
    {
        isThrowButtonDown = false;
    }
    
    private void MouseLookOnperformed(InputAction.CallbackContext obj)
    {
        bool movedCamera = Mathf.Abs(GetCameraInputVector2Normalized().x) >= 0.5f || Mathf.Abs(GetCameraInputVector2Normalized().y) >= 0.5f;
        if (movedCamera)
        {
            UpdateControllerInUse(obj.control.device.name);
        }
    }

    private void PauseOnperformed(InputAction.CallbackContext obj)
    {
        UpdateControllerInUse(obj.control.device.name);
        
        OnPauseBindingPressed?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetCameraInputVector2()
    {
        if (ignoreInput)
        {
            return Vector2.zero;
        }
        
        return plrInpActsSO.Value.PlayerActionMap.MouseLook.ReadValue<Vector2>();
    }

    private Vector2 GetCameraInputVector2Normalized()
    {
        if (ignoreInput)
        {
            return Vector2.zero;
        }
        
        return plrInpActsSO.Value.PlayerActionMap.MouseLook.ReadValue<Vector2>().normalized;
    }

    public Vector2 GetPlayerMoveVector2Normalized()
    {
        if (ignoreInput)
        {
            return Vector2.zero;
        }
        
        return plrInpActsSO.Value.PlayerActionMap.Move.ReadValue<Vector2>().normalized;
    }
    
    private void UpdateControllerInUse(string deviceName)
    {
        plrInpActsSO.isUsingGamepad = deviceName is not ("Keyboard" or "Mouse");
    }
}
