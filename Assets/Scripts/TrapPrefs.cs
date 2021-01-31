using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TrapPrefs : MonoBehaviour, IPunObservable
{
    public float manaCost;

    public bool isBroken;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isBroken);
        }
        else
        {
            isBroken = (bool) stream.ReceiveNext();
        }
    }

    private bool isRed;
    private void Update()
    {
        if (isBroken && !isRed)
        {
            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
                if (spriteRenderer == null)
                {
                    spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
                }
            }

            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.red;
            }

            isRed = true;
        }
    }
}
