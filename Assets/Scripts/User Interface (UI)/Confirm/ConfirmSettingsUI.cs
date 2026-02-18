using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Allows player to confirm settings during InitialSetup scene.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ConfirmSettingsUI : MonoBehaviour
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    private Action onConfirmAction;

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

        onConfirmAction?.Invoke();
        EventSystem.current.SetSelectedGameObject(previouslySelectedButton);
        Hide();
    }

    private void OnCancelButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        onConfirmAction = null;
        EventSystem.current.SetSelectedGameObject(previouslySelectedButton);
        Hide();
    }

    public void Show(Action onConfirmAction)
    {
        previouslySelectedButton = EventSystem.current.currentSelectedGameObject;
        
        this.onConfirmAction = onConfirmAction;
        gameObject.SetActive(true);
        cancelButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
