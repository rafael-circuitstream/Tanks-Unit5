using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class NetworkChat : NetworkBehaviour
{
    public Action<string> OnMessageReceived;

    public void SendMessageToChat(string messageReceived)
    {
        string whoSentThis = "Client";
        if (IsOwner) whoSentThis = "Host";

        SendMessageRpc(messageReceived, whoSentThis);
    }


    [Rpc(SendTo.Everyone)]
    public void SendMessageRpc(string messageReceived, string username)
    {
        OnMessageReceived?.Invoke(username + ": " + messageReceived);
    }
}
