/*
 * Handles team score system in online gameplay.
 *
 * Final score is used in 'game over' system to determine results of the game.
 *
 * @author Ben Conway
 * @date May 2024
 */
using Unity.Netcode;
using UnityEngine;

public class TeamScoresManager : NetworkBehaviour
{
    public static TeamScoresManager Instance { get; private set; }
    
    private int teamAScore;
    private int teamBScore;
    
    [SerializeField] private TeamScoresUI teamScoresUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ResetScores();
    }

    private void ResetScores()
    {
        teamAScore = 0;
        teamBScore = 0;
        
        UpdateScoresUI();
    }
    
    [Rpc(SendTo.Everyone)]
    public void IncrementScoreTeamARpc()
    {
        teamAScore++;
        UpdateScoresUI();
    }
    
    [Rpc(SendTo.Everyone)]
    public void IncrementScoreTeamBRpc()
    {
        teamBScore++;
        UpdateScoresUI();
    }

    private void UpdateScoresUI()
    {
        teamScoresUI.UpdateTeamScoresUI(teamAScore, teamBScore);
    }

    public int GetTeamAScore()
    {
        return teamAScore;
    }

    public int GetTeamBScore()
    {
        return teamBScore;
    }
}
