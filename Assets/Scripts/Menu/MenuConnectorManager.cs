using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MenuConnectorManager : MonoBehaviour
{
    public string gameVersion = "1";

    public GameObject loadingTitle, menuObjects;
    
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
    }

    public void OnConnectedToMaster()
    {
        loadingTitle.SetActive(false);
        menuObjects.SetActive(true);
    }

    public void OnChooseMaster()
    {
        PlayerPrefs.SetInt("isMaster", 1);
        StartGame();
    }

    public void OnChoosePlayer()
    {
        PlayerPrefs.SetInt("isMaster", 0);
        StartGame();
    }

    private void StartGame()
    {
        var roomOptions = new RoomOptions {IsVisible = true, MaxPlayers = 10};
        PhotonNetwork.JoinOrCreateRoom("just_room", roomOptions, TypedLobby.Default);
    }
}
