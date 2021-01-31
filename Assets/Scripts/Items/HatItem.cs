using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatItem : MonoBehaviour, IitemsPickupable
{
    public GameObject hatUI;
    public void OnItemPickup(PlayerController playerController)
    {
        hatUI.SetActive(true);
        playerController.isInHat = true;
        Destroy(gameObject);
    }
}
