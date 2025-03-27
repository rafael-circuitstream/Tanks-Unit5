using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkGameManager : NetworkBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer(NetworkObject player)
    {
        //YOU CAN DO A LOT OF STUFF HERE
        //APPLY DELAY

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;

        player.GetComponent<NetworkHealth>().Revive();
    }

}
