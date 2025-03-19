using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private GameObject chatPrefab;

    [SerializeField] private Rigidbody tankRigidbody;
    [SerializeField] private float rotatingSpeed = 3f;
    [SerializeField] private float movingSpeed = 2f;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsServer && IsLocalPlayer && IsOwner)
        {
            //Spawn the chat prefab
            NetworkObject.InstantiateAndSpawn(chatPrefab, NetworkManager);
        }

        if(IsLocalPlayer)
        {
            FindObjectOfType<ChatUI>().InitializeChatUI();
        }    

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsOwner)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            MoveTankRpc(horizontal, vertical);
        }
    }

    [Rpc(SendTo.Server)]
    public void MoveTankRpc(float horizontal, float vertical)
    {
        //horizontal = Mathf.Clamp(horizontal, -1f, +1f);

        tankRigidbody.velocity = (transform.forward * Time.fixedDeltaTime * movingSpeed * vertical);
        tankRigidbody.rotation = Quaternion.Euler(transform.eulerAngles + transform.up * horizontal * rotatingSpeed * Time.fixedDeltaTime);
        //transform.Rotate(transform.up * horizontal * Time.deltaTime * rotatingSpeed);
    }

}
