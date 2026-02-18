using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Vivox;
using Unity.Netcode;

/// <summary>
/// @author Vivox sample package
/// @date May 2024
/// </summary>
public class RosterItem : MonoBehaviour
{
    // Player specific items.
    public VivoxParticipant Participant;
    public Text PlayerNameText;

    public Image ChatStateImage;
    public Sprite MutedImage;
    public Sprite SpeakingImage;
    public Sprite NotSpeakingImage;
    public Button MuteButton;

    const float k_minSliderVolume = -50;
    const float k_maxSliderVolume = 7;
    readonly Color k_MutedColor = new Color(1, 0.624f, 0.624f, 1);

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
    }

    private void NetworkManager_OnClientConnectedCallback(ulong obj)
    {
        Debug.Log(obj.ToString()+" joined!");
    }

    private void UpdateChatStateImage()
    {
        if (Participant.IsMuted)
        {
            ChatStateImage.sprite = MutedImage;
            ChatStateImage.gameObject.transform.localScale = Vector3.one;
        }
        else
        {
            if (Participant.SpeechDetected)
            {
                ChatStateImage.sprite = SpeakingImage;
                ChatStateImage.gameObject.transform.localScale = Vector3.one;
            }
            else
            {
                ChatStateImage.sprite = NotSpeakingImage;
            }
        }
    }

    public void SetupRosterItem(VivoxParticipant participant)
    {
        Participant = participant;
        if (participant.DisplayName == GameMultiplayer.Instance.GetPlayerName())
        {
            PlayerNameText.text = "YOU: "+participant.DisplayName;
        }
        else
        {
            PlayerNameText.text = participant.DisplayName;
        }
        UpdateChatStateImage();
        Participant.ParticipantMuteStateChanged += UpdateChatStateImage;
        Participant.ParticipantSpeechDetected += UpdateChatStateImage;

        MuteButton.onClick.AddListener(() =>
        {
            // If already muted, unmute, and vice versa.
            if (Participant.IsMuted)
            {
                participant.UnmutePlayerLocally();
                MuteButton.image.color = Color.white;
            }
            else
            {
                participant.MutePlayerLocally();
                MuteButton.image.color = k_MutedColor;
            }
        });
    }

    void OnDestroy()
    {
        Participant.ParticipantMuteStateChanged -= UpdateChatStateImage;
        Participant.ParticipantSpeechDetected -= UpdateChatStateImage;
        MuteButton.onClick.RemoveAllListeners();
    }
}
