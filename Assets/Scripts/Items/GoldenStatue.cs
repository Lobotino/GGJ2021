using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenStatue : MonoBehaviour, IitemsPickupable
{
    public GameObject goldenStatueUI;
    public void OnItemPickup(PlayerController playerController)
    {
        goldenStatueUI.SetActive(true);
        playerController.hasGoldenStatue = true;
        GameObject.Find("GameManager").GetComponent<GameManager>().isGoldenStatueStolen = true;
        Destroy(gameObject);
    }
}
