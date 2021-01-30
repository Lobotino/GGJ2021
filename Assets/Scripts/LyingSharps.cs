using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LyingSharps : MonoBehaviour
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
                isSharpsUp = false;
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


    private void OnTriggerStay2D(Collider2D other)
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
        foreach (var sharp in movableSharps)
        {
            sharp.SetBool(IsActiveAnimationState, true);
        }
//
//        var position = transform.position;
//        position = Vector2.Lerp(position, new Vector2(position.x, position.y + (transform.localScale. * 0.6)), )
//        transform.position = position;
    }
    
    private void SharpsDown()
    {
        foreach (var sharp in movableSharps)
        {
            sharp.SetBool(IsActiveAnimationState, false);
        }
    }
}
