using System;
using UnityEngine;

/// <summary>
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ListWithTextFontsPlayerChoice : ListPlayerChoiceSuper
{
    public event EventHandler OnButtonPressed;
    
    [SerializeField] private ListWithTextFontsSO listWithTextFontsSO;

    private void Start()
    {
        listWithTextFontsSO.LoadPointer();
        UpdatePlayerChoiceVisual();
    }

    protected override void OnLeftClickButtonPressed()
    {
        if(!listWithTextFontsSO.DecrementPointer())
            return;
        
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();
        
        OnButtonPressed?.Invoke(this, EventArgs.Empty);
        
        UpdatePlayerChoiceVisual();
    }

    protected override void OnRightClickButtonPressed()
    {
        if(!listWithTextFontsSO.IncrementPointer())
            return;
        
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();
        
        OnButtonPressed?.Invoke(this, EventArgs.Empty);
        
        UpdatePlayerChoiceVisual();
    }

    public void UpdatePlayerChoiceVisual()
    {
        string playerChoiceText = "ABC123";
        UpdatePlayerChoiceText(playerChoiceText);
    }
}
