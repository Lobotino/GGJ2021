
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameOnlineCallbacks : MonoBehaviourPunCallbacks
{
    public override void OnConnectedToMaster()
    {
        Debug.Log("INGAME: OnConnectedToMaster");
    }

    public override void OnConnected()
    {
        Debug.Log("INGAME: onConnected");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("INGAME: OnLeftRoom");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("INGAME: OnCreatedRoom");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("INGAME: OnJoinedLobby");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("INGAME: OnLeftLobby");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("INGAME: OnDisconnected: " + cause);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("INGAME: OnJoinedRoom");
    }
}
