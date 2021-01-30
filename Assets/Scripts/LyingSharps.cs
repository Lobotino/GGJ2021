using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Photon.Pun;
using UnityEngine;

public class LyingSharps : MonoBehaviour, ITrapSetter
{
    public Animator[] movableSharps;
    
    public int attackDelayInSeconds = 1;
    private int currentAttackSharpsUpDelay;
    
    public int damage = 4;
    
    private bool isSharpsUp, isAttackDelay;

    public int sharpsUpDelayInSeconds = 2;
    private int currentSharpsUpDelay;
    
    private static readonly int IsActiveAnimationState = Animator.StringToHash("isActive");

    void Start()
    {
        attackDelayInSeconds *= 50;
        sharpsUpDelayInSeconds *= 50;
    }

    void FixedUpdate()
    {
        if (isSharpsUp)
        {
            currentSharpsUpDelay++;
            if (currentSharpsUpDelay >= sharpsUpDelayInSeconds)
            {
                currentSharpsUpDelay = 0;
                SharpsDown();
            }
        }

        if (isAttackDelay)
        {
            currentSharpsUpDelay++;
            if (currentSharpsUpDelay >= attackDelayInSeconds)
            {
                currentAttackSharpsUpDelay = 0;
                isAttackDelay = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            HurtPlayer(other.gameObject);
        }
    }

    private void HurtPlayer(GameObject playerObj)
    {
        if (!isAttackDelay)
        {
            if (!isSharpsUp)
            {
                SharpsUp();
            }

            var player = playerObj.gameObject.GetComponent<PlayerController>();
            if (player != null && !player.IsDead())
            {
                player.Hurt(damage);
                isAttackDelay = true;
            }
        }
    }

    private void SharpsUp()
    {
        isSharpsUp = true;
        foreach (var sharp in movableSharps)
        {
            sharp.SetBool(IsActiveAnimationState, true);
        }
    }
    
    private void SharpsDown()
    {
        isSharpsUp = false;
        foreach (var sharp in movableSharps)
        {
            sharp.SetBool(IsActiveAnimationState, false);
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
            PhotonNetwork.Instantiate("SharpPicks", worldPos, Quaternion.identity);
            return true;
        }

        return false;
    }
}
