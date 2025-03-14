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
                NetworkBullet bulletClone = Instantiate(bulletPrefab, weaponTip.position, weaponTip.rotation);
                bulletClone.NetworkObject.Spawn(); //ONLY SERVER CAN SPAWN OBJECTS

                //ALTERNATE OPTION:
                //NetworkObject.InstantiateAndSpawn(bulletPrefab.gameObject, NetworkManager);

                //ALTERNATE OPTION 2:
                //NetworkManager.SpawnManager.InstantiateAndSpawn(bulletClone.NetworkObject);

                //OFFLINE OPTION:
                //Rigidbody bulletClone = Instantiate(bulletPrefab, weaponTip.position, weaponTip.rotation);
                //bulletClone.AddForce(weaponTip.forward * 500f);
            }
        }

    }

}
