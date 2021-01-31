using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player") && gameManager.isGoldenStatueStolen)
        {
            if (Utils.IsPlayerMaster())
            {
                gameManager.ShowLoose();
            }
            else
            {
                gameManager.ShowWin();
            }
        }
    }
}
