using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class UINetworkManager : MonoBehaviour
{
    public TMP_InputField nicknameInputfield;

    public void BtnClick_StartHost()
    {
        Debug.Log("Starting host...");
        NetworkManager.Singleton.StartHost();
    }

    public void BtnClick_StartClient()
    {
        Debug.Log("Starting client...");
        NetworkManager.Singleton.StartClient();
    }
}
