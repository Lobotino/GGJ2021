using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : MonoBehaviour, IPunObservable
{

    public GameObject masterLight;
    public GameObject playerPrefab;

    public GameObject winUI, looseUI;
    
    public GameObject playerUI, masterUI;

    public bool isGoldenStatueStolen;

    void Start()
    {
        Debug.Log("isMaster " + Utils.IsPlayerMaster());
        
        if (!Utils.IsPlayerMaster() && UserProperties.LocalPlayerInstance == null)
        {
            PreparePlayerGame();
        }
        else
        {
            PrepareMasterGame();
        }
    }
    private void PrepareMasterGame()
    {
        masterLight.SetActive(true);
        masterUI.SetActive(true);

        GameObject.Find("Main Camera").AddComponent<MasterCameraControl>();
    }

    private void PreparePlayerGame()
    {
        UserProperties.LocalPlayerInstance = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(-98.56f, -131.49f, 0f), Quaternion.identity);
        UserProperties.LocalPlayerInstance.name = "Player(" + PhotonNetwork.LocalPlayer.UserId + ")";
        UserProperties.UserId = PhotonNetwork.LocalPlayer.UserId;
        
        playerUI.SetActive(true);
    }

    public void ShowWin()
    {
        winUI.SetActive(true);
        if (Utils.IsPlayerMaster())
        {
            masterUI.SetActive(false);
        }
        else
        {
            playerUI.SetActive(false);
        }
    }

    public void ShowLoose()
    {
        looseUI.SetActive(true);
        if (Utils.IsPlayerMaster())
        {
            masterUI.SetActive(false);
        }
        else
        {
            playerUI.SetActive(false);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isGoldenStatueStolen);       
        }
        else
        {
            isGoldenStatueStolen = (bool) stream.ReceiveNext();
        }
    }

    public void SetGoldenStatueStolen()
    {
        isGoldenStatueStolen = true;
    }
}
