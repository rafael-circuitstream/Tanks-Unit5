using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private Rigidbody tankRigidbody;
    [SerializeField] private float rotatingSpeed = 3f;
    [SerializeField] private float movingSpeed = 2f;
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

            tankRigidbody.velocity = (transform.forward * Time.fixedDeltaTime * movingSpeed * vertical);
            transform.Rotate(transform.up * horizontal * Time.deltaTime * rotatingSpeed);
        }


    }

    private void FixedUpdate()
    {
        
    }
}
