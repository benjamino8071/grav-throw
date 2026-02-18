/*
 * Manages state of the Ball in offline gameplay.
 * 
 * @author Ben Conway
 * @date May 2024
 */
using System;
using UnityEngine;

public class BallManagerOFFLINE : MonoBehaviour
{
    public static BallManagerOFFLINE Instance { get; private set; }

    [SerializeField] private Transform ballInitialSpawnPoint;
    
    [SerializeField] private GameObject ballPrefab;

    [NonSerialized] public GameObject ballInstance;

    private GameObject currentOwnerOfBall;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnBall();
    }

    private void SpawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, ballInitialSpawnPoint);
        ballInstance = ball;
    }
    
    public void ResetBall()
    {
        ballInstance.GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        currentOwnerOfBall.GetComponent<PlayerHoldAndThrowBallControllerOFFLINE>().SetToNotHoldingBall();
        
        ballInstance.GetComponent<BallTargetTransformOFFLINE>().SetTargetTransform(ballInitialSpawnPoint);
    }
    
    public void BallPickedUp(Transform playerBallHoldTransform, GameObject player)
    {
        ballInstance.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ballInstance.GetComponent<BallTargetTransformOFFLINE>().SetTargetTransform(playerBallHoldTransform);
        
        currentOwnerOfBall = player;
    }
    
    public void BallThrown(Vector3 forwardsDirection)
    {
        ballInstance.GetComponent<BallTargetTransformOFFLINE>().UnSetTargetTransform();
        
        BallApplyForceOFFLINE ballForce = ballInstance.GetComponent<BallApplyForceOFFLINE>();
        ballForce.ApplyForwardsForce(forwardsDirection);
    }
}

