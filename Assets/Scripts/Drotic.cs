using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drotic : MonoBehaviour
{
    public int damage = 2;

    private Rigidbody2D _rigidbody2D;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.tag.Equals("Drotic") && !other.gameObject.tag.Equals("Floors"))
        {
            if (other.tag.Equals("Player"))
            {
                var player = other.gameObject.GetComponent<PlayerController>();
                if (player != null && !player.IsDead())
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
                }
            }
        }
    }
}
