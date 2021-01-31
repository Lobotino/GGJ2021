using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueStone : MonoBehaviour, IitemsPickupable
{
    public GameObject blueStoneUI;
    public void OnItemPickup(PlayerController playerController)
    {
        blueStoneUI.SetActive(true);
        playerController.hasBlueStone = true;
        Destroy(gameObject);
    }
}
