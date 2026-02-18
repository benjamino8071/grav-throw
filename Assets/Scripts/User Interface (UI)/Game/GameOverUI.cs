using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Shows GameOver interface when online game is complete (i.e. timer expires).
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI teamXWinsText;
    [SerializeField] private Button exitToMainMenuButton;
    
    private void Start()
    {
        exitToMainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenuScene");
        });
        
        GameOverManager.Instance.OnGameOver += GameOverManager_OnGameOver;
        
        Hide();
    }

    private void GameOverManager_OnGameOver(object sender, EventArgs e)
    {
        //Allows player to interact with UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        int teamAScore = TeamScoresManager.Instance.GetTeamAScore();
        int teamBScore = TeamScoresManager.Instance.GetTeamBScore();

        if (teamAScore > teamBScore)
        {
            //Team A WINS
            teamXWinsText.SetText("TEAM A WINS");
        }
        else if (teamAScore < teamBScore)
        {
            //Team B WINS
            teamXWinsText.SetText("TEAM B WINS");
        }
        else
        {
            //DRAW
            teamXWinsText.SetText("DRAW");
        }
        
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
