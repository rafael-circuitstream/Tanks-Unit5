using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class NetworkHealth : NetworkBehaviour
{
    [SerializeField] private int defaultHealth;
    [SerializeField] private NetworkObject deathFx;

    //Reference to slider/image/UI script

    public NetworkVariable<int> health = new NetworkVariable<int>(
        readPerm: NetworkVariableReadPermission.Everyone, 
        writePerm: NetworkVariableWritePermission.Server);


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(IsServer)
        {
            health.Value = defaultHealth;
        }
        else
        {
            health.OnValueChanged += OnHealthZero; 
            //Client is listening for changes on the Value
        }
    }

    private void OnHealthZero(int previous, int current)
    {
        //CHANGE HELATHBAR VALUE HERE
        //change value in the reference 

        //THIS IS HAPPENING FOR EVERY CLIENT

        if(current <= 0) //Client is checking if current value of life is 0
        {
            //Start an UI EFFECT
            Debug.Log("I'm Dead!!!!");
        }
    }

    public void DealDamage(ulong damageOrigin)
    {
        health.Value--;

        Debug.Log(damageOrigin + " SHOT " + OwnerClientId);

        if (health.Value <= 0) //Server is checking if life is 0
        {
            //Die();
            
            NetworkObject effect = Instantiate(
                deathFx, 
                transform.position + Vector3.up, 
                Quaternion.identity);

            effect.Spawn();
            Destroy(effect.gameObject, 3f);

            FindObjectOfType<NetworkGameManager>().RespawnPlayer(NetworkObject);
        }     
    }

    public void Revive()
    {
        health.Value = defaultHealth;
    }
}
