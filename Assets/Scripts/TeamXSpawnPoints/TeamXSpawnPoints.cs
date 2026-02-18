/*
 * Attached to parent of spawn points for either team A or team B.
 * Used to iterate through spawn points during initial spawn sequence.
 *
 * @author Ben Conway
 * @date May 2024
 */
using System.Collections.Generic;
using UnityEngine;

public class TeamXSpawnPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPointsList;
    private int pointer = 0;

    public Transform GetNextSpawnPoint()
    {
        Debug.Log("Getting next spawn point. The pointer value is "+pointer);
        Transform spawnPoint = spawnPointsList[pointer];
        pointer++;
        return spawnPoint;
    }
}
