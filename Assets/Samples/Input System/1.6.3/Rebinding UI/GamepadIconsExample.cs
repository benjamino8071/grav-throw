using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

////TODO: have updateBindingUIEvent receive a control path string, too (in addition to the device layout name)

/// <summary>
    /// This is an example for how to override the default display behavior of bindings. The component
    /// hooks into <see cref="RebindActionUI.updateBindingUIEvent"/> which is triggered when UI display
    /// of a binding should be refreshed. It then checks whether we have an icon for the current binding
    /// and if so, replaces the default text display with an icon.
    /// </summary>
    public class GamepadIconsExample : MonoBehaviour
    {
        public Sprite buttonSouth;
        public Sprite buttonNorth;
        public Sprite buttonEast;
        public Sprite buttonWest;
        public Sprite startButton;
        public Sprite selectButton;
        public Sprite leftTrigger;
        public Sprite rightTrigger;
        public Sprite leftShoulder;
        public Sprite rightShoulder;
        public Sprite dpad;
        public Sprite dpadUp;
        public Sprite dpadDown;
        public Sprite dpadLeft;
        public Sprite dpadRight;
        public Sprite leftStick;
        public Sprite rightStick;
        public Sprite leftStickPress;
        public Sprite rightStickPress;
        
        public Sprite GetSprite(string controlPath)
        {
            // From the input system, we get the path of the control on device. So we can just
            // map from that to the sprites we have for gamepads.
            switch (controlPath)
            {
                case "<Gamepad>/buttonSouth": return buttonSouth;
                case "<Gamepad>/buttonNorth": return buttonNorth;
                case "<Gamepad>/buttonEast": return buttonEast;
                case "<Gamepad>/buttonWest": return buttonWest;
                case "<Gamepad>/start": return startButton;
                case "<Gamepad>/select": return selectButton;
                case "<Gamepad>/leftTrigger": return leftTrigger;
                case "<Gamepad>/rightTrigger": return rightTrigger;
                case "<Gamepad>/leftShoulder": return leftShoulder;
                case "<Gamepad>/rightShoulder": return rightShoulder;
                case "<Gamepad>/dpad": return dpad;
                case "<Gamepad>/dpad/up": return dpadUp;
                case "<Gamepad>/dpad/down": return dpadDown;
                case "<Gamepad>/dpad/left": return dpadLeft;
                case "<Gamepad>/dpad/right": return dpadRight;
                case "<Gamepad>/leftStick": return leftStick;
                case "<Gamepad>/rightStick": return rightStick;
                case "<Gamepad>/leftStickPress": return leftStickPress;
                case "<Gamepad>/rightStickPress": return rightStickPress;
            }
            return null;
        }
    }
