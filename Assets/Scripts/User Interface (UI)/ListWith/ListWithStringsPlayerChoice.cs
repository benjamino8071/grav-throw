using UnityEngine;

/// <summary>
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ListWithStringsPlayerChoice : ListPlayerChoiceSuper
{
    [SerializeField] private ListWithStringsSO listWithStringsSO;

    private void Start()
    {
        listWithStringsSO.LoadPointer();
        
        UpdatePlayerChoiceVisual();
    }

    protected override void OnLeftClickButtonPressed()
    {
        if(!listWithStringsSO.DecrementPointer())
            return;
        
        UpdatePlayerChoiceVisual();
    }

    protected override void OnRightClickButtonPressed()
    {
        if(!listWithStringsSO.IncrementPointer())
            return;
        
        UpdatePlayerChoiceVisual();
    }

    private void UpdatePlayerChoiceVisual()
    {
        string playerChoiceText = listWithStringsSO.GetCurrentValue();
        UpdatePlayerChoiceText(playerChoiceText);
    }
}
