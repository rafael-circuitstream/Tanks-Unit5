using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class NetworkHealth : NetworkBehaviour
{


    public void DealDamage(ulong damageOrigin)
    {
        Debug.Log(damageOrigin + " SHOT " + OwnerClientId);
    }
}
