using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Photon.Pun;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour, IPunObservable
{

    public float speed = 10f;
    public GameObject mainCamera;

    private Rigidbody2D _rigidbody;
    private PhotonView _photonView;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _rigidbody = GetComponent<Rigidbody2D>();
        mainCamera = GameObject.Find("Main Camera");
    }

    void FixedUpdate()
    {

        CheckSound();
        CheckAnimation();

//        _horizontalMove = Input.GetAxisRaw("Horizontal");
        var position = transform.position;

        if (_photonView.IsMine)
        {
            MovePlayer(position);
            mainCamera.transform.position = new Vector3(position.x, position.y, -50);

            if (Input.GetKeyDown(KeyCode.E))
            {
                //do some action
            }
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
//        isRun = Mathf.Abs(_horizontalMove) > 0.002f;
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
//        if (stream.IsWriting)
//        {
//            stream.SendNext(_rigidbody.velocity);
//            stream.SendNext(_rigidbody.position);
//        }
//        else
//        {
//            _rigidbody.velocity = (Vector2) stream.ReceiveNext();
//            _rigidbody.MovePosition(Vector2.Lerp(_rigidbody.position, (Vector2)stream.ReceiveNext(), Time.deltaTime));
//        }
    }
}
