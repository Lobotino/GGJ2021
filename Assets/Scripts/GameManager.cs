using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject masterLight;
    public GameObject playerPrefab;
    
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

    private MasterUI _masterUi;
    private void PrepareMasterGame()
    {
        masterLight.SetActive(true);
        _masterUi = GameObject.Find("MasterManager").GetComponent<MasterUI>();
        _masterUi.ShowMasterPanels();
    }

    private void PreparePlayerGame()
    {
        UserProperties.LocalPlayerInstance = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity);
        UserProperties.LocalPlayerInstance.name = "Player(" + PhotonNetwork.LocalPlayer.UserId + ")";
        UserProperties.UserId = PhotonNetwork.LocalPlayer.UserId;
    }
    
}
