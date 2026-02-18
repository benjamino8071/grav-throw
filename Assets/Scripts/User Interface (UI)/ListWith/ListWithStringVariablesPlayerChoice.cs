using UnityEngine;

/// <summary>
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ListWithStringVariablesPlayerChoice : ListPlayerChoiceSuper
{
    [SerializeField] private ListWithStringVariablesSO listWithStrVarsSO;

    private void Start()
    {
        listWithStrVarsSO.LoadPointer();
        
        UpdatePlayerChoiceVisual();
    }

    protected override void OnLeftClickButtonPressed()
    {
        if(!listWithStrVarsSO.DecrementPointer())
            return;
        
        UpdatePlayerChoiceVisual();
    }

    protected override void OnRightClickButtonPressed()
    {
        if(!listWithStrVarsSO.IncrementPointer())
            return;
        
        UpdatePlayerChoiceVisual();
    }

    public void UpdatePlayerChoiceVisual()
    {
        string playerChoiceText = listWithStrVarsSO.GetCurrentValue().Value;
        UpdatePlayerChoiceText(playerChoiceText);
    }
}
