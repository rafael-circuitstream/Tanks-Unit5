using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class NetworkBullet : NetworkBehaviour
{
    public ulong originId;
    [SerializeField] private Rigidbody body;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(IsServer)
        {
            //INITIALIZATION IS HAPPENING ON PlayerShoot.cs NOW
            //InitializeBullet();
        }
        
    }

    public void InitializeBullet()
    {
        body.AddForce(transform.forward * 500f);
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(IsServer)
        {
            if (collision.rigidbody)
            {
                NetworkHealth possibleNetworkObject = collision.rigidbody.GetComponent<NetworkHealth>();
                if (possibleNetworkObject)
                {
                    if(originId != possibleNetworkObject.OwnerClientId)
                    {
                        possibleNetworkObject.DealDamage(originId);

                        //ShowKillFeedRpc(originId, possibleNetworkObject.OwnerClientId);
                    }
                    
                }
            }

            Destroy(gameObject);
            //Debug.Log(originId + " SHOT " + collision.body.GetComponent<NetworkObject>().OwnerClientId);
        }
        //increase score
        
    }

    [Rpc(SendTo.NotServer)]
    public void ShowKillFeedRpc(ulong killer, ulong killed)
    {
        Debug.Log(killer + " SHOT " + killed);
    }
}
