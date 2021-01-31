using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedStone : MonoBehaviour, IitemsPickupable
{
    public GameObject redStoneUI;
    public void OnItemPickup(PlayerController playerController)
    {
        redStoneUI.SetActive(true);
        playerController.hasRedStone = true;
        Destroy(gameObject);
    }
}
