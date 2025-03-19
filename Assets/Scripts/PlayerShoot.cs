using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerShoot : NetworkBehaviour
{
    [SerializeField] private NetworkBullet bulletPrefab;
    [SerializeField] private Transform weaponTip;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

    }

    // Update is called once per frame
    void Update()
    {
        if(IsOwner)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpawnBulletRpc();
                //ONLY SERVER CAN SPAWN OBJECTS

                #region NOTES
                //ALTERNATE OPTION:
                //NetworkObject.InstantiateAndSpawn(bulletPrefab.gameObject, NetworkManager);

                //ALTERNATE OPTION 2:
                //NetworkManager.SpawnManager.InstantiateAndSpawn(bulletClone.NetworkObject);

                //OFFLINE OPTION:
                //Rigidbody bulletClone = Instantiate(bulletPrefab, weaponTip.position, weaponTip.rotation);
                //bulletClone.AddForce(weaponTip.forward * 500f);
                #endregion
            }
        }

    }

    [Rpc(SendTo.Server)]
    public void SpawnBulletRpc(RpcParams param = default)
    {
        //Debug.Log(param.Receive.SenderClientId);

        NetworkBullet bulletClone = Instantiate(bulletPrefab, weaponTip.position, weaponTip.rotation);
        bulletClone.NetworkObject.Spawn();
        bulletClone.InitializeBullet();
        bulletClone.originId = param.Receive.SenderClientId;
    }

}
