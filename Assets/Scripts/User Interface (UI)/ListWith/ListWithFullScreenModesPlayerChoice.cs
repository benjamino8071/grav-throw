using System;
using UnityEngine;

/// <summary>
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ListWithFullScreenModesPlayerChoice : ListPlayerChoiceSuper
{
    public event EventHandler OnButtonPressed; //When a Specific class requires knowledge of button pressed
    
    [SerializeField] private ListWithFullScreenModesSO listWithFSMsSO;

    private void Start()
    {
        listWithFSMsSO.LoadPointer();
        
        UpdatePlayerChoiceVisual();
    }

    protected override void OnLeftClickButtonPressed()
    {
        if(!listWithFSMsSO.DecrementPointer())
            return;
        
        OnButtonPressed?.Invoke(this, EventArgs.Empty);
        
        UpdatePlayerChoiceVisual();
    }

    protected override void OnRightClickButtonPressed()
    {
        if(!listWithFSMsSO.IncrementPointer())
            return;
        
        OnButtonPressed?.Invoke(this, EventArgs.Empty);
        
        UpdatePlayerChoiceVisual();
    }

    public void UpdatePlayerChoiceVisual()
    {
        string playerChoiceText = listWithFSMsSO.GetCurrentValue().ToString();
        UpdatePlayerChoiceText(playerChoiceText);
    }
}
