
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlineCallbacks : MonoBehaviourPunCallbacks
{
    public MenuConnectorManager menuConectorManager;
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN.");
        
        menuConectorManager.OnConnectedToMaster();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (!UserProperties.UserId.Equals(otherPlayer.UserId))
        {
            GameObject gm = GameObject.Find("Player(" + otherPlayer.UserId + ")");
            if (gm != null)
            {
                Destroy(gm);
            }
        }
    }

    public override void OnConnected()
    {
        Debug.Log("onConnected");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("OnLeftLobby");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected: " + cause);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        SceneManager.LoadScene("Main");
    }
}
