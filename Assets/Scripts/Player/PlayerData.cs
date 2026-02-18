/*
 * Allows online functionality systems to hold and update information about a player.
 *
 * @author https://www.youtube.com/watch?v=7glCsF9fv3s
 * @date [sourced] May 2024
 */
using System;
using Unity.Collections;
using Unity.Netcode;

public struct PlayerData : IEquatable<PlayerData>, INetworkSerializable
{
    public ulong clientId;
    public FixedString64Bytes playerName;
    public FixedString64Bytes playerId;
    
    
    public bool Equals(PlayerData other)
    {
        return clientId == other.clientId &&
               playerName == other.playerName &&
               playerId == other.playerId;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref clientId);
        serializer.SerializeValue(ref playerName);
        serializer.SerializeValue(ref playerId);
    }
}
