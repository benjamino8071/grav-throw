using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// Holds all functionality for changing keybinds in the 'bindings' interface.
///
/// Redirects flow of execution based on whether the player wants to change a keyboard/mouse binding, or on their gamepad.
///
/// Issue: Could not implement button through gamepad that allows player to cancel re-binding.
/// Re-binding can only be cancelled by pressing 'Esc' key on keyboard.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class BindingModifier : MonoBehaviour
{
    public event EventHandler OnRestoreDefaultKeybinds, OnKeybindsChanged;
    
    private string defaultKeybindsJSON;
    
    private const string PLAYER_PREFS_INPUT_KBM_BINDINGS = "KBMInputBindings";
    
    [SerializeField] private PlayerInputActionsSO plrInpActsSO;
    
    public enum Binding
    {
        Forward,
        Backward,
        Left,
        Right,
        Jump,
        Throw,
        Pause
    }

    
    private void Awake()
    {
        //IMPORTANT COMMAND: Initialises the playerInputActions input system
        plrInpActsSO.Value = new PlayerInputActions();

        defaultKeybindsJSON = plrInpActsSO.Value.SaveBindingOverridesAsJson();
        
        if (PlayerPrefs.HasKey(PLAYER_PREFS_INPUT_KBM_BINDINGS))
        {
            plrInpActsSO.Value.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_INPUT_KBM_BINDINGS));
        }
        
        plrInpActsSO.Value.Enable();
    }
    
    
    /*
     * Logic reference: https://youtu.be/AmGSEH7QcDg?t=33740
     */
    public string GetBindingText(Binding binding, bool isGamepad=false)
    {
        switch (binding)
        {
            default:
            case Binding.Forward:
                return plrInpActsSO.Value.PlayerActionMap.Move.bindings[1].ToDisplayString();
            case Binding.Backward:
                return plrInpActsSO.Value.PlayerActionMap.Move.bindings[2].ToDisplayString();
            case Binding.Left:
                return plrInpActsSO.Value.PlayerActionMap.Move.bindings[3].ToDisplayString();
            case Binding.Right:
                return plrInpActsSO.Value.PlayerActionMap.Move.bindings[4].ToDisplayString();
            case Binding.Jump:
                return plrInpActsSO.Value.PlayerActionMap.Jump.bindings[isGamepad ? 1 : 0].ToDisplayString();
            case Binding.Throw:
                return plrInpActsSO.Value.PlayerActionMap.Throw.bindings[isGamepad ? 1 : 0].ToDisplayString();
            case Binding.Pause:
                return plrInpActsSO.Value.PlayerActionMap.Pause.bindings[isGamepad ? 1 : 0].ToDisplayString();
        }
    }
    
    public string GetBindingPath(Binding binding, bool isGamepad=false)
    {
        switch (binding)
        {
            default:
            case Binding.Forward:
                return plrInpActsSO.Value.PlayerActionMap.Move.bindings[1].effectivePath;
            case Binding.Backward:
                return plrInpActsSO.Value.PlayerActionMap.Move.bindings[2].effectivePath;
            case Binding.Left:
                return plrInpActsSO.Value.PlayerActionMap.Move.bindings[3].effectivePath;
            case Binding.Right:
                return plrInpActsSO.Value.PlayerActionMap.Move.bindings[4].effectivePath;
            case Binding.Jump:
                return plrInpActsSO.Value.PlayerActionMap.Jump.bindings[isGamepad ? 1 : 0].effectivePath;
            case Binding.Throw:
                return plrInpActsSO.Value.PlayerActionMap.Throw.bindings[isGamepad ? 1 : 0].effectivePath;
            case Binding.Pause:
                return plrInpActsSO.Value.PlayerActionMap.Pause.bindings[isGamepad ? 1 : 0].effectivePath;
        }
    }
    
    /*
     * Logic reference: https://youtu.be/AmGSEH7QcDg?t=33740
     */
    public void RebindBinding(Binding binding, Action onActionRebound, bool isGamepad=false)
    {
        plrInpActsSO.Value.PlayerActionMap.Disable();
        
        GameObject buttonSelected = EventSystem.current.currentSelectedGameObject;
        
        EventSystem.current.SetSelectedGameObject (null);
        
        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.Forward:
                inputAction = plrInpActsSO.Value.PlayerActionMap.Move;
                bindingIndex = 1;
                break;
            case Binding.Backward:
                inputAction = plrInpActsSO.Value.PlayerActionMap.Move;
                bindingIndex = 2;
                break;
            case Binding.Left:
                inputAction = plrInpActsSO.Value.PlayerActionMap.Move;
                bindingIndex = 3;
                break;
            case Binding.Right:
                inputAction = plrInpActsSO.Value.PlayerActionMap.Move;
                bindingIndex = 4;
                break;
            case Binding.Jump:
                inputAction = plrInpActsSO.Value.PlayerActionMap.Jump;
                bindingIndex = isGamepad ? 1 : 0;
                break;
            case Binding.Throw:
                inputAction = plrInpActsSO.Value.PlayerActionMap.Throw;
                bindingIndex = isGamepad ? 1 : 0;
                break;
            case Binding.Pause:
                inputAction = plrInpActsSO.Value.PlayerActionMap.Pause;
                bindingIndex = isGamepad ? 1 : 0;
                break;
        }
        
        // If the binding is a composite, we need to rebind each part in turn.
        if (inputAction.bindings[bindingIndex].isComposite)
        {
            var firstPartIndex = bindingIndex + 1;
            if (firstPartIndex >= inputAction.bindings.Count ||
                !inputAction.bindings[firstPartIndex].isPartOfComposite) return;
            if (isGamepad)
            {
                GamepadRebind(inputAction, onActionRebound, bindingIndex, buttonSelected, allCompositeParts: true);
            }
            else
            {
                KBMRebind(inputAction, onActionRebound, bindingIndex, buttonSelected, allCompositeParts: true);
            }
        }
        else
        {
            if (isGamepad)
            {
                GamepadRebind(inputAction, onActionRebound, bindingIndex, buttonSelected);
            }
            else
            {
                KBMRebind(inputAction, onActionRebound, bindingIndex, buttonSelected);
            }
        }
    }
    
    private void KBMRebind(InputAction inputAction, Action onActionRebound, int bindingIndex, GameObject buttonSelected, bool allCompositeParts = false)
    {
        inputAction.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("<Gamepad>")
            .WithCancelingThrough("<Gamepad>/start")
            .WithCancelingThrough("<Keyboard>/escape")
            .OnCancel(callback =>
            {
                callback.Dispose();
                onActionRebound();
                EventSystem.current.SetSelectedGameObject(buttonSelected);
            })
            .OnComplete(callback =>
            {
                callback.Dispose();

                if (CheckDuplicateBindings(inputAction, bindingIndex, allCompositeParts))
                {
                    inputAction.RemoveBindingOverride(bindingIndex);
                    KBMRebind(inputAction, onActionRebound, bindingIndex, buttonSelected, allCompositeParts);
                    return;
                }
                
                // If there's more composite parts we should bind, initiate a rebind
                // for the next part.
                if (allCompositeParts)
                {
                    var nextBindingIndex = bindingIndex + 1;
                    if (nextBindingIndex < inputAction.bindings.Count && inputAction.bindings[nextBindingIndex].isPartOfComposite)
                        KBMRebind(inputAction, onActionRebound, nextBindingIndex, buttonSelected, true);
                }
                
                plrInpActsSO.Value.PlayerActionMap.Enable();
                onActionRebound();

                SaveKeybinds();
                EventSystem.current.SetSelectedGameObject(buttonSelected);
                OnKeybindsChanged?.Invoke(this, EventArgs.Empty);
            })
            .Start();
    }
    
    private void GamepadRebind(InputAction inputAction, Action onActionRebound, int bindingIndex, GameObject buttonSelected, bool allCompositeParts = false)
    {
        inputAction.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("<Keyboard>")
            .WithControlsExcluding("<Mouse>")
            .WithControlsExcluding("<Gamepad>/rightStick")
            .WithControlsExcluding("<Gamepad>/leftStick")
            .WithCancelingThrough("<Gamepad>/start")
            .WithCancelingThrough("<Keyboard>/escape")
            .OnCancel(callback =>
            {
                Debug.Log("OnCancel for Gamepad called");
                callback.Dispose();
                onActionRebound();
                EventSystem.current.SetSelectedGameObject(buttonSelected);
            })
            .OnComplete(callback =>
            {
                callback.Dispose();
                
                if (CheckDuplicateBindings(inputAction, bindingIndex))
                {
                    inputAction.RemoveBindingOverride(bindingIndex);
                    GamepadRebind(inputAction, onActionRebound, bindingIndex, buttonSelected, allCompositeParts);
                    return;
                }
                
                // If there's more composite parts we should bind, initiate a rebind
                // for the next part.
                if (allCompositeParts)
                {
                    var nextBindingIndex = bindingIndex + 1;
                    if (nextBindingIndex < inputAction.bindings.Count && inputAction.bindings[nextBindingIndex].isPartOfComposite)
                        GamepadRebind(inputAction, onActionRebound, nextBindingIndex, buttonSelected, true);
                }
                
                plrInpActsSO.Value.PlayerActionMap.Enable();
                onActionRebound();

                SaveKeybinds();
                EventSystem.current.SetSelectedGameObject(buttonSelected);
                OnKeybindsChanged?.Invoke(this, EventArgs.Empty);
            })
            .Start();
    }

    private bool CheckDuplicateBindings(InputAction action, int bindingIndex, bool allCompositeParts = false)
    {
        InputBinding newBinding = action.bindings[bindingIndex];
        int currentIndex = -1;

        foreach (InputBinding binding in action.actionMap.bindings)
        {
            currentIndex++;

            if (binding.action == newBinding.action)
            {
                if (binding.isPartOfComposite && currentIndex != bindingIndex)
                {
                    if (binding.effectivePath == newBinding.effectivePath)
                    {
                        Debug.Log("Duplicate binding found in composite: " + newBinding.effectivePath);
                        return true;
                    }
                }

                else
                {
                    continue;
                }
            }

            if (binding.effectivePath == newBinding.effectivePath)
            {
                Debug.Log("Duplicate binding found: " + newBinding.effectivePath);
                return true;
            }               

        }

        if (allCompositeParts)
        {
            for (int i = 1; i < bindingIndex; i++)
            {
                if (action.bindings[i].effectivePath == newBinding.overridePath)
                {
                    //Debug.Log("Duplicate binding found: " + newBinding.effectivePath);
                    return true;
                }
            }
        }

        return false;
    }

    public void RestoreDefaultKeybinds()
    {
        plrInpActsSO.Value.LoadBindingOverridesFromJson(defaultKeybindsJSON);
        SaveKeybinds();
        OnRestoreDefaultKeybinds?.Invoke(this, EventArgs.Empty);
    }

    private void SaveKeybinds()
    {
        PlayerPrefs.SetString(PLAYER_PREFS_INPUT_KBM_BINDINGS, plrInpActsSO.Value.SaveBindingOverridesAsJson());
        PlayerPrefs.Save();
    }
}
