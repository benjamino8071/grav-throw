using UnityEngine;

/// <summary>
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ListWithIntPairsPlayerChoice : ListPlayerChoiceSuper
{
    [SerializeField] private ListWithIntPairsSO listWithIntPairsSO;

    private void Start()
    {
        listWithIntPairsSO.LoadPointer();
    }

    protected override void OnLeftClickButtonPressed()
    {
        listWithIntPairsSO.DecrementPointer();
    }

    protected override void OnRightClickButtonPressed()
    {
        listWithIntPairsSO.IncrementPointer();
    }
    
    /*
     * Pair may be represented in any possible format.
     * Therefore left to the Specific class on same object as this class to update visual
     */
    public void UpdateVisual(string text)
    {
        UpdatePlayerChoiceText(text);
    }
}
