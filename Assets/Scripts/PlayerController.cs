using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{

    public float speed = 1f;
    public float runXCurrent = 1f;
    public GameObject mainCamera;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private float _horizontalMove;
    public bool isRun;

    void FixedUpdate()
    {

        CheckSound();
        CheckAnimation();

//        if (!photonView.IsMine)
//        {
//            armature.armature.flipX = flipX;
//            if (isRun)
//            {
//                if (armature.armature.animation.lastAnimationName != "Run")
//                    armature.armature.animation.GotoAndPlayByTime("Run");
//            }
//            else
//            {
//                if (armature.armature.animation.lastAnimationName != "Stand")
//                    armature.armature.animation.GotoAndPlayByTime("Stand");
//            }
//        }
//        else
//        {

        _horizontalMove = Input.GetAxisRaw("Horizontal");
        var position = transform.position;
        mainCamera.transform.position = new Vector3(position.x, position.y, -50);
        
        MovePlayer(position);
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            //do some action
        }
    }

    private void MovePlayer(Vector2 position)
    {
        float resultXMove = position.x, resultYMove = position.y;

        if (!(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
        {
            if (Input.GetKey(KeyCode.A))
            {
                resultXMove = position.x - speed;
            }
            else
            {
                if (Input.GetKey(KeyCode.D))
                {
                    resultXMove = position.x + speed;
                }
            }
        }

        if (!(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)))
        {
            if (Input.GetKey(KeyCode.S))
            {
                resultYMove = position.y - speed;
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    resultYMove = position.y + speed;
                }
            }
        }

        _rigidbody.MovePosition(Vector2.Lerp(position, new Vector2(resultXMove, resultYMove), Time.deltaTime));
    }

    private void CheckSound()
    {
        //        if (Math.Abs(_rigidbody.velocity.x) > 0.01) //сука звук
//        {
//            walkAudio.Play();
//        }
//        else
//        {
//            walkAudio.Pause();
//        }
    }

    private void CheckAnimation()
    {
        isRun = Mathf.Abs(_horizontalMove) > 0.002f;
//            if (isRun)
//            {
//                if (armature.armature.animation.lastAnimationName != "Run")
//                    armature.armature.animation.GotoAndPlayByTime("Run");
//            }
//            else
//            {
//                if (armature.armature.animation.lastAnimationName != "Stand")
//                    armature.armature.animation.GotoAndPlayByTime("Stand");
//            }
//
//
//            if (_horizontalMove > 0)
//            {
//                armature.armature.flipX = false;
//                flipX = false;
//            }
//            else
//            {
//                if (_horizontalMove < 0)
//                {
//                    armature.armature.flipX = true;
//                    flipX = true;
//                }
//            }
    }
}
