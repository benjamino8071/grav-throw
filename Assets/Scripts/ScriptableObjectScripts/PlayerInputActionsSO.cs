/*
 * @author Ben Conway
 * @date May 2024
 */
using UnityEngine;

[CreateAssetMenu()]
public class PlayerInputActionsSO : ScriptableObject
{
    public PlayerInputActions Value;

    public bool isUsingGamepad;
    
    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Forward:
                return Value.PlayerActionMap.Move.bindings[1].ToDisplayString();
            case Binding.Backward:
                return Value.PlayerActionMap.Move.bindings[2].ToDisplayString();
            case Binding.Left:
                return Value.PlayerActionMap.Move.bindings[3].ToDisplayString();
            case Binding.Right:
                return Value.PlayerActionMap.Move.bindings[4].ToDisplayString();
            case Binding.Jump:
                return Value.PlayerActionMap.Jump.bindings[0].ToDisplayString();
            case Binding.Throw:
                return Value.PlayerActionMap.Throw.bindings[0].ToDisplayString();
            case Binding.Pause:
                return Value.PlayerActionMap.Pause.bindings[0].ToDisplayString();
        }
    }
    
    public string GetBindingPath(Binding binding, bool isGamepad=false)
    {
        switch (binding)
        {
            default:
            case Binding.Forward:
                return Value.PlayerActionMap.Move.bindings[1].effectivePath;
            case Binding.Backward:
                return Value.PlayerActionMap.Move.bindings[2].effectivePath;
            case Binding.Left:
                return Value.PlayerActionMap.Move.bindings[3].effectivePath;
            case Binding.Right:
                return Value.PlayerActionMap.Move.bindings[4].effectivePath;
            case Binding.Jump:
                return Value.PlayerActionMap.Jump.bindings[isGamepad ? 1 : 0].effectivePath;
            case Binding.Throw:
                return Value.PlayerActionMap.Throw.bindings[isGamepad ? 1 : 0].effectivePath;
            case Binding.Pause:
                return Value.PlayerActionMap.Pause.bindings[isGamepad ? 1 : 0].effectivePath;
        }
    }
}
