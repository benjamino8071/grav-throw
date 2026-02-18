/*
 * @author Ben Conway
 * @date May 2024
 */
using System.Collections.Generic;
using Unity.Services.Vivox;
using UnityEngine;

[CreateAssetMenu()]
public class DictWithStringVivoxPartListSO : ScriptableObject
{
    public List<VivoxParticipant> Value;

    public void InitialiseValue()
    {
        Value = new List<VivoxParticipant>();
    }
}
