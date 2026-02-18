using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Handles actions performed on the 'ConfirmLeave' interface.
/// 
/// Purpose is to ensure player does not accidentally perform destructive actions.
/// Similar to ConfirmQuitUI.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ConfirmLeaveUI : MonoBehaviour
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    private GameObject previouslySelectedButton;

    private Action onConfirmPressedAction;
    
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

        onConfirmPressedAction?.Invoke();
    }

    private void OnCancelButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        EventSystem.current.SetSelectedGameObject(previouslySelectedButton);
        Hide();

        onConfirmPressedAction = null;
    }

    public void Show(Action onConfPreAct = null)
    {
        onConfirmPressedAction = onConfPreAct;
        
        previouslySelectedButton = EventSystem.current.currentSelectedGameObject;

        gameObject.SetActive(true);
        cancelButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
