using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Handles actions performed on the 'ConfirmQuit' interface.
/// 
/// Purpose is to ensure player does not accidentally perform destructive actions.
/// Similar to ConfirmLeaveUI.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ConfirmQuitUI : MonoBehaviour
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    private GameObject previouslySelectedButton;
    
    private void Awake()
    {
        confirmButton.onClick.AddListener(OnConfirmButtonPressed);
        cancelButton.onClick.AddListener(OnCancelButtonPressed);
    }

    private void Start()
    {
        Hide();
    }

    private void OnConfirmButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();
        
        Application.Quit();
    }

    private void OnCancelButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        EventSystem.current.SetSelectedGameObject(previouslySelectedButton);
        Hide();
    }

    public void Show()
    {
        previouslySelectedButton = EventSystem.current.currentSelectedGameObject;

        gameObject.SetActive(true);
        cancelButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
