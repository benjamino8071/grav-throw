/*
 * Manages state of the Ball in online gameplay.
 *
 * @author Ben Conway
 * @date May 2024
 */
using System;
using Unity.Netcode;
using UnityEngine;

public class BallManager : NetworkBehaviour
{
    public static BallManager Instance { get; private set; }

    [SerializeField] private Transform ballInitialSpawnPoint;
    
    [SerializeField] private GameObject ballPrefab;

    [NonSerialized] public GameObject ballInstance;

    private NetworkObject currentOwnerOfBall;

    public override void OnNetworkSpawn()
    {
        Instance = this;
    }
    
    public void SpawnBall()
    {
        if (IsServer)
        { 
            GameObject ball = Instantiate(ballPrefab, ballInitialSpawnPoint);
            ball.GetComponent<NetworkObject>().Spawn();
            SetBallInstanceRpc(ball.GetComponent<NetworkObject>());
        }
    }
    
    [Rpc(SendTo.Everyone)]
    private void SetBallInstanceRpc(NetworkObjectReference ballRef)
    {
        ballRef.TryGet(out NetworkObject ballObject);

        ballInstance = ballObject.gameObject;
    }
    
    public void ResetBall()
    {
        ballInstance.GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        ballInstance.GetComponent<BallTargetTransform>().SetTargetTransform(ballInitialSpawnPoint);
    }

    public void BallPickedUp(NetworkObject playerObject)
    {
        BallPickedUpServRpc(playerObject);
    }
    
    [Rpc(SendTo.Server)]
    private void BallPickedUpServRpc(NetworkObjectReference networkObjectReference)
    {
        BallPickedUpCliRpc(networkObjectReference);
    }
    
    [Rpc(SendTo.ClientsAndHost)]
    private void BallPickedUpCliRpc(NetworkObjectReference networkObjectReference)
    {
        networkObjectReference.TryGet(out NetworkObject playerObject);
        
        Transform playerHoldBallTransform = playerObject.GetComponent<PlayerHoldAndThrowBallController>()
            .GetPlayerBallSpawnPointTransform();
        
        ballInstance.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ballInstance.GetComponent<BallTargetTransform>().SetTargetTransform(playerHoldBallTransform);
        
        if (currentOwnerOfBall)
        {
            //Stealing ball from player who already has it
            currentOwnerOfBall.GetComponent<PlayerHoldAndThrowBallController>().SetToNotHoldingBall();
        }
        
        Debug.Log("Current owner of ball: "+currentOwnerOfBall);
        currentOwnerOfBall = playerObject.GetComponent<NetworkObject>();
    }

    public void BallThrown(Vector3 forwardsDirection)
    {
        BallThrownServRpc(forwardsDirection);
    }
    
    [Rpc(SendTo.Server)]
    private void BallThrownServRpc(Vector3 forwardsDirection)
    {
        BallThrownCliRpc(forwardsDirection);
    }
    
    [Rpc(SendTo.ClientsAndHost)]
    private void BallThrownCliRpc(Vector3 forwardsDirection)
    {
        currentOwnerOfBall = null;
        
        ballInstance.GetComponent<BallTargetTransform>().UnSetTargetTransform();
        
        BallApplyForce ballForce = ballInstance.GetComponent<BallApplyForce>();
        ballForce.ApplyForwardsForce(forwardsDirection);
    }
}
