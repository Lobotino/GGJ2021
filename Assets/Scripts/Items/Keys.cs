using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour, IitemsPickupable
{
    public void OnItemPickup(PlayerController playerController)
    {
        playerController.keysCount++;
        Destroy(gameObject);
    }
}
