using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class OnlineConnecter : MonoBehaviour
{
    public string gameVersion = "1";
    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
