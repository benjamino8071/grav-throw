using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Provides superclass functionality for 'player choice' subclasses.
/// 
/// 'Player choice' is a naming convention for the use of the SettingsChoice template prefab.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ListPlayerChoiceSuper : MonoBehaviour
{
    [SerializeField] private Button leftClickButton, rightClickButton;
    [SerializeField] private TextMeshProUGUI playerChoiceText;

    public Button GetLeftClickButton()
    {
        return leftClickButton;
    }

    public Button GetRightClickButton()
    {
        return rightClickButton;
    }

    private void Awake()
    {
        leftClickButton.onClick.AddListener(OnLeftClickButtonPressed);
        rightClickButton.onClick.AddListener(OnRightClickButtonPressed);
    }

    protected void UpdatePlayerChoiceText(string text)
    {
        playerChoiceText.SetText(text);
    }

    protected virtual void OnLeftClickButtonPressed()
    {
        
    }
    
    protected virtual void OnRightClickButtonPressed()
    {
        
    }
}