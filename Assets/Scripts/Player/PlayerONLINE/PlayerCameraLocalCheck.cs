/*
 * Updates tag associated with PC's camera object based on whether that PC is owned by the player.
 * If it is another player's PC, the tag is set to 'Untagged'.
 *
 * For online gameplay.
 *
 * @author Ben Conway
 * @date May 2024
 */
using Unity.Netcode;
using UnityEngine;

public class PlayerCameraLocalCheck : NetworkBehaviour
{
    [SerializeField] private GameObject cameraObject;
    
    private void Start()
    {
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        if (IsOwner)
        {
            return;
        }

        cameraObject.tag = "Untagged";
        gameObject.SetActive(false);
    }
}
