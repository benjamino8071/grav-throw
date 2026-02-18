/*
 * Holds reference to PC's team A and team B visual mesh renderers.
 *
 * For online gameplay.
 *
 * @author Ben Conway
 * @date May 2024
 */
using Unity.Netcode;
using UnityEngine;

public class PlayerVisualLocalCheck : NetworkBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private Mesh playerMesh;
    
    public void AssignMesh()
    {
        skinnedMeshRenderer.sharedMesh = playerMesh;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
