using TMPro;
using UnityEngine;

/// <summary>
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class TeamScoresUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI teamAScoreText;
    [SerializeField] private TextMeshProUGUI teamBScoreText;

    public void UpdateTeamScoresUI(int teamAScore, int teamBScore)
    {
        teamAScoreText.SetText(teamAScore.ToString());
        teamBScoreText.SetText(teamBScore.ToString());
    }
}
