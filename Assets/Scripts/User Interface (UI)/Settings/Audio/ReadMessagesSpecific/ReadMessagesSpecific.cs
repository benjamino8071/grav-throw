using TMPro;
using Unity.Services.Vivox;
using UnityEngine;

/// <summary>
/// Allows player to change whether or not they want Text-To-Speech functionality for incoming text messages.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ReadMessagesSpecific : MonoBehaviour
{
    [SerializeField] private ButtonSUI buttonSUI;

    [SerializeField] private BooleanVariableSO readMesSO;

    [SerializeField] private TextMeshProUGUI buttonText;
    
    private void Start()
    {
        UpdateButtonText();
        
        buttonSUI.GetButton().onClick.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        if (readMesSO.GetValue())
        {
            //Set to false
            readMesSO.SetToFalse();
            VivoxService.Instance.TextToSpeechCancelMessages(TextToSpeechMessageType.LocalPlayback);
        }
        else
        {
            //Set to true
            readMesSO.SetToTrue();
        }
        
        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        if (readMesSO.GetValue())
        {
            buttonText.SetText("ON");
        }
        else
        {
            buttonText.SetText("OFF");
        }
    }
}
