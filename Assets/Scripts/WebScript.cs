using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Photon.Pun;
using UnityEngine;

public class WebScript : MonoBehaviour, ITrapSetter
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().speed *= 0.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().speed *= 2f;
        }
    }

    public bool TryToInstantiateTrap(Vector2 mousePos)
    {
        if (Camera.main == null) return false;
        var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;
        RaycastHit2D neighbortCell = Physics2D.Raycast(worldPos, Vector2.zero);
        if (neighbortCell.collider == null || !neighbortCell.collider.gameObject.tag.Equals("Borders") && !neighbortCell.collider.gameObject.tag.Equals("Traps"))
        {
            PhotonNetwork.Instantiate("Web", worldPos, Quaternion.identity);
            return true;
        }

        return false;
    }
}
    
