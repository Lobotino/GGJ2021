using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject playerInstance;
    
    void Start()
    {
        if (!UserProperties.IsMasterInstance && UserProperties.LocalPlayerInstance == null)
        {
            UserProperties.LocalPlayerInstance = PhotonNetwork.Instantiate(playerInstance.name, new Vector3(0f, 0f, 0f), Quaternion.identity);
            UserProperties.LocalPlayerInstance.name = "Player(" + PhotonNetwork.LocalPlayer.UserId + ")";
            UserProperties.UserId = PhotonNetwork.LocalPlayer.UserId;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
