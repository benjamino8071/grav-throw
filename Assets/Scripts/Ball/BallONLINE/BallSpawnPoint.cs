/*
 * Handles spawning Ball object into online scene.
 *
 * @author Ben Conway
 * @date May 2024
 */
using System;
using Unity.Netcode;
using UnityEngine;

public class BallSpawnPoint : NetworkBehaviour
{
    public static BallSpawnPoint Instance { get; private set; }
    
    [SerializeField] private GameObject ballPrefab;

    [NonSerialized] public BallApplyForce ballInstance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (IsServer)
        {
            SpawnBallServerAuth();
        }
    }

    private void SpawnBallServerAuth()
    {
        GameObject ballInstantiated = Instantiate(ballPrefab, transform);
        ballInstantiated.GetComponent<NetworkObject>().Spawn();
        ballInstance = ballInstantiated.GetComponent<BallApplyForce>();
    }
}
