using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChatUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField messageInputField;
    [SerializeField] private TextMeshProUGUI messageDisplay;
    NetworkChat chat;

    public void InitializeChatUI()
    {
        chat = FindObjectOfType<NetworkChat>();
        chat.OnMessageReceived += ReceiveMessageText;
    }

    public void Btn_SendMessage()
    {
        string messageToSend = messageInputField.text;


        //SEND TO NETWORK - NEW PART
        if(!chat)
        {
            InitializeChatUI();
        }
        //NEW PART ^^^^^^^


        chat.SendMessageToChat(messageToSend);
    }

    public void ReceiveMessageText(string messageReceived)
    {
        messageDisplay.text += "\n" + messageReceived;
    }
}
