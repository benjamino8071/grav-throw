using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles creation of lobbies. Players can choose whether to make a lobby 'public' or 'private',
/// as well as the maximum amount of players. Also if they want the lobby to be 'casual' or 'competitive'.
/// 
/// @author Ben Conway, with inspiration from https://www.youtube.com/watch?v=7glCsF9fv3s
/// @date May 2024
/// </summary>
public class LobbyCreateUI : MonoBehaviour
{
    private bool isPrivate;
    [SerializeField] private Button choosePublicButton;
    [SerializeField] private Image choosePublicSelectedVisual;
    [SerializeField] private Button choosePrivateButton;
    [SerializeField] private Image choosePrivateSelectedVisual;
    [SerializeField] private Color normalButtonColor;
    [SerializeField] private Color optionChosenColor;
    
    private int maxPlayersAmount; //Lowest amount is 2 (1 vs 1), Highest amount is 10 (5 vs 5)
    [SerializeField] private Button decreaseMaxPlayersButton;
    [SerializeField] private Button increaseMaxPlayersButton;

    [SerializeField] private TextMeshProUGUI playerVersusText;

    private string difficultyChosen;
    [SerializeField] private Button chooseCasualButton;
    [SerializeField] private Image chooseCasualSelectedVisual;
    [SerializeField] private StringVariableSO digCasSO;
    
    [SerializeField] private Button chooseCompetitiveButton;
    [SerializeField] private Image chooseCompetitiveSelectedVisual;
    [SerializeField] private StringVariableSO difCompSO;
    
    [SerializeField] private Button createButton;
    
    [SerializeField] private Button closeButton;
    
    private readonly Dictionary<int, string> playerVersusDict = new()
    {
        { 4, "2 vs 2" },
        { 6, "3 vs 3" },
        { 8, "4 vs 4" },
        { 10, "5 vs 5" }
    };
    
    private readonly List<int> playerAmountList = new() {4, 6, 8, 10};
    private int playerAmountPointer;
    
    private void Awake()
    {
        choosePublicButton.onClick.AddListener(OnChoosePublicButtonPressed);
        choosePrivateButton.onClick.AddListener(OnChoosePrivateButtonPressed);
        
        decreaseMaxPlayersButton.onClick.AddListener(OnDecreaseMaxPlayersButtonPressed);
        increaseMaxPlayersButton.onClick.AddListener(OnIncreaseMaxPlayersButtonPressed);
        
        chooseCasualButton.onClick.AddListener(OnChooseCasualButtonPressed);
        chooseCompetitiveButton.onClick.AddListener(OnChooseCompetitiveButtonPressed);
        
        createButton.onClick.AddListener(OnCreateButtonPressed);
        
        closeButton.onClick.AddListener(OnCloseButtonPressed);
    }

    private void Start()
    {
        playerAmountPointer = 1; //Initialise on 6 players (3 vs 3)
        UpdatePlayerVersusText();
        
        Hide();
    }

    private void OnChoosePublicButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();
        
        ChoosePublic();
    }

    private void ChoosePublic()
    {
        isPrivate = false;
        
        choosePrivateSelectedVisual.gameObject.SetActive(false);
        choosePublicSelectedVisual.gameObject.SetActive(true);
    }

    private void OnChoosePrivateButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();
        
        ChoosePrivate();
    }

    private void ChoosePrivate()
    {
        isPrivate = true;
        
        choosePublicSelectedVisual.gameObject.SetActive(false);
        choosePrivateSelectedVisual.gameObject.SetActive(true);
    }

    private void OnDecreaseMaxPlayersButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        playerAmountPointer--;
        if (playerAmountPointer < 0)
        {
            playerAmountPointer = playerAmountList.Count - 1;
        }
        UpdatePlayerVersusText();
    }

    private void OnIncreaseMaxPlayersButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();
        
        playerAmountPointer++;
        if (playerAmountPointer > playerAmountList.Count - 1)
        {
            playerAmountPointer = 0;
        }
        UpdatePlayerVersusText();
    }

    private void OnChooseCasualButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();
        
        ChooseCasual();
    }

    private void ChooseCasual()
    {
        difficultyChosen = digCasSO.Value;
        
        chooseCasualSelectedVisual.gameObject.SetActive(true);
        chooseCompetitiveSelectedVisual.gameObject.SetActive(false);
    }

    private void OnChooseCompetitiveButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();
        
        ChooseCompetitive();
    }

    private void ChooseCompetitive()
    {
        difficultyChosen = difCompSO.Value;
        
        chooseCasualSelectedVisual.gameObject.SetActive(false);
        chooseCompetitiveSelectedVisual.gameObject.SetActive(true);
    }

    private void OnCreateButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        string randomLobbyName = "Lobby" + Random.Range(0, 10000);
        GameLobby.Instance.CreateLobby(randomLobbyName, isPrivate, playerAmountList[playerAmountPointer], difficultyChosen);
    }

    private void OnCloseButtonPressed()
    {
        SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();

        Hide();
    }

    private void UpdatePlayerVersusText()
    {
        playerVersusText.SetText(playerVersusDict[playerAmountList[playerAmountPointer]]);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        
        choosePublicButton.Select();
        ChoosePublic();
        ChooseCasual();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
