/*
 * Handles action performed when Ball object collides with Goalzone in online gameplay.
 *
 * @author Ben Conway
 * @date May 2024
 */
using Unity.Netcode;
using UnityEngine;

public class Goalzone : NetworkBehaviour
{
    private const string BALL_LAYER = "Ball";
    [SerializeField] private string goalzoneTeamLetter;

    [SerializeField] private Transform teamLetterTextTransform;

    private void Update()
    {
        if (Camera.main)
        {
            teamLetterTextTransform.LookAt(Camera.main.transform);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(BALL_LAYER))
        {
            switch (goalzoneTeamLetter)
            {
                case "A":
                    //This is TeamA's goalzone. Therefore award goal to TeamB
                    TeamScoresManager.Instance.IncrementScoreTeamBRpc();
                    break;
                case "B":
                    //This is TeamB's goalzone. Therefore award goal to TeamA
                    TeamScoresManager.Instance.IncrementScoreTeamARpc();
                    break;
                default:
                    Debug.LogError("TO DEVELOPER: SET THE CORRECT LETTER FOR THE GOALZONE");
                    break;
            }
            
            BallManager.Instance.ResetBall();
        }
    }
}
