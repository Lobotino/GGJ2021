using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigLight : MonoBehaviour, IitemsPickupable
{
    public GameObject bigLightUI;
    public void OnItemPickup(PlayerController playerController)
    {
        bigLightUI.SetActive(true);
        playerController.hasBigLight = true;
        Destroy(gameObject);
    }
}
