/*
 * Displays the correct PC visual depending on which team the player is on.
 *
 * For online gameplay.
 *
 * @author Ben Conway
 * @date May 2024
 */
using Unity.Netcode;
using UnityEngine;

public class PlayerVisualSet : NetworkBehaviour
{
    [SerializeField] private PlayerVisualLocalCheck teamAVisual;
    [SerializeField] private PlayerVisualLocalCheck teamBVisual;
    
    private void Start()
    {
        SetVisual();
    }
    
    /// <summary>
    /// If this object is the player object, then we want to show the visual for the team they are on.
    /// Then we want to keep the mesh at 'none'.
    ///
    /// If this object is not the player object, then we want to show the visual for the team they are on
    /// Then we want to set the mesh to the one based on the team they are on
    /// </summary>

    private void SetVisual()
    {
        string teamLetter = GameManager.Instance.playersOnTeamsDict[OwnerClientId];
        
        if (teamLetter == "A")
        {
            teamBVisual.Hide();
            if (!IsOwner)
            {
                teamAVisual.AssignMesh();
            }
        }
        else if (teamLetter == "B")
        {
            teamAVisual.Hide();
            if (!IsOwner)
            {
                teamBVisual.AssignMesh();
            }
        }
        else
        {
            Debug.LogError("Team Letter not sent correctly!");
        }
    }
}
