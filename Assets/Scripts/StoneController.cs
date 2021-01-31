using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Photon.Pun;
using UnityEngine;

public class StoneController : MonoBehaviour, ITrapSetter
{
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    public int damage = 5;
    public float minHurtSpeed = 2;
    public float forceModifier = 1.5f;
    public float startForce = 200;
    public float startRotationForHorizontal = 100;

    public bool isVertical = true;
    public bool isLeft, isUp;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        PushStone();
    }

    public void PushStone()
    {
        if (isVertical)
        {
            _rigidbody2D.AddForce(new Vector2(0, isUp ? startForce : -startForce), ForceMode2D.Impulse);
        }
        else
        {
            if (isLeft)
            {
                _rigidbody2D.AddTorque(startRotationForHorizontal, ForceMode2D.Impulse);
                _rigidbody2D.AddForce(new Vector2(-startForce, 0), ForceMode2D.Impulse);
            }
            else
            {
                _rigidbody2D.AddTorque(-startRotationForHorizontal, ForceMode2D.Impulse);
                _rigidbody2D.AddForce(new Vector2(startForce, 0), ForceMode2D.Impulse);
            }
        }
    }
    void Update()
    {
        if (Math.Abs(_rigidbody2D.velocity.x) < 0.3 && Math.Abs(_rigidbody2D.velocity.y) > 0.1)
        {
            _rigidbody2D.SetRotation(Quaternion.identity);
            _animator.speed = 0.7f;
        }
        else
        {
            _animator.speed = 0.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((Math.Abs(_rigidbody2D.velocity.x) > minHurtSpeed || Math.Abs(_rigidbody2D.velocity.y) > minHurtSpeed) && other.tag.Equals("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player != null && !player.IsDead())
            {
                player.Hurt(damage);
                player.GetComponent<Rigidbody2D>().AddForce(_rigidbody2D.velocity * forceModifier, ForceMode2D.Impulse);
            }
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
            PhotonNetwork.Instantiate(isVertical ? isUp ? "StoneVerticalUp" : "StoneVerticalDown" : isLeft ? "StoneLeft" : "StoneRight", worldPos, Quaternion.identity);
            return true;
        }

        return false;
    }
}
