using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drotic : MonoBehaviour
{
    public int damage = 2;

    private Rigidbody2D _rigidbody2D;

    private RemovableItem _removableItem;

    private bool isLanded;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _removableItem = GetComponent<RemovableItem>();
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.tag.Equals("Drotic") && !other.gameObject.tag.Equals("Floors") && !other.gameObject.tag.Equals("Traps"))
        {
            if (other.tag.Equals("Player"))
            {
                var player = other.gameObject.GetComponent<PlayerController>();
                if (!isLanded && player != null && !player.IsDead())
                {
                    player.Hurt(damage);
                    Destroy(this);
                }
            }
            else
            {
                if (_rigidbody2D != null)
                {
                    _rigidbody2D.velocity = new Vector2(0, 0);
                    _removableItem.DestroySelfAfterTimer();
                    isLanded = true;
                }
            }
        }
    }
}
