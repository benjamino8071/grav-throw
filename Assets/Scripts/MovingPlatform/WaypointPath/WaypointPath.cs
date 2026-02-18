/*
 * @author https://www.youtube.com/watch?v=ly9mK0TGJJo&ab_channel=KetraGames
 * @date [sourced] May 2024
 */
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public Transform GetWaypoint(int waypointIndex)
    {
        return transform.GetChild(waypointIndex);
    }

    public int GetNextWaypointIndex(int currentWaypointIndex)
    {
        int nextWaypointIndex = currentWaypointIndex + 1;

        if (nextWaypointIndex == transform.childCount)
        {
            nextWaypointIndex = 0;
        }
        
        return nextWaypointIndex;
    }
}
