using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class NetworkBullet : NetworkBehaviour
{
    [SerializeField] private Rigidbody body;


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        InitializeBullet();
    }

    private void InitializeBullet()
    {
        body.AddForce(transform.forward * 500f);
        Destroy(gameObject, 5f);
    }
}
