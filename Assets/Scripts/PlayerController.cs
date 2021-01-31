using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour, IPunObservable
{

    public float speed = 10f;
    public GameObject mainCamera;

    private Rigidbody2D _rigidbody;
    private PhotonView _photonView;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private LightScript _lightScript;
    
    public int health = 100;
    public int heardsUiCount = 5;
    public bool isDead, isInHat, hasBlueStone, hasRedStone, hasBigLight, hasGoldenStatue;
    public int keysCount = 0;
    public GameObject[] heardUI, brokenHeardsUI;
    public Text countOfKeysText;

    public GameManager gameManager;
    
    private bool isMoveHorizontal, isFlip, isMoveForward, isMoveBackward, isIdle = true;
    private static readonly int IsMoveForward = Animator.StringToHash("isMoveForward");
    private static readonly int IsMoveHorizontal = Animator.StringToHash("isMoveHorizontal");
    private static readonly int IsMoveBackward = Animator.StringToHash("isMoveBackward");
    private static readonly int IsIdle = Animator.StringToHash("isIdle");

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _lightScript = GetComponent<LightScript>();
        mainCamera = GameObject.Find("Main Camera");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        countOfKeysText = GameObject.Find("keysCountText").GetComponent<Text>();

        for (int i = 0; i < 5; i++)
        {
            heardUI[i] = GameObject.Find("Heart" + i);
            brokenHeardsUI[i] = GameObject.Find("BrokenHearts" + i);
        }
    }

    void FixedUpdate()
    {
        CheckSound();
        CheckAnimation();

//        _horizontalMove = Input.GetAxisRaw("Horizontal");
        var position = transform.position;

        if (_photonView.IsMine)
        {
            heardsUiCount = health / 20;
            for (var i = 0; i < 5; i++)
            {
                if (i > heardsUiCount)
                {
                    heardUI[i].SetActive(false);
                    brokenHeardsUI[i].SetActive(true);
                }
                else
                {
                    heardUI[i].SetActive(true);
                    brokenHeardsUI[i].SetActive(false);
                }
            }
            if (health <= 0)
            {
                MakeDeath();
            }

            if (!isDead)
            {
                MovePlayer(position);
                mainCamera.transform.position = new Vector3(position.x, position.y, -50);
            }

            countOfKeysText.text = keysCount.ToString();
        }
    }

    public void Update()
    {
        if (_photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DoAction();
            }
        }
    }

    public void DoAction()
    {
        var colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 1f);
        foreach (var col in colliders)
        {
            if (col.tag.Equals("Item"))
            {
                col.gameObject.GetComponent<IitemsPickupable>().OnItemPickup(this);
            }
            else
            {
                if (col.tag.Equals("Traps"))
                {
                    if (keysCount > 0 && !col.gameObject.GetComponent<TrapPrefs>().isBroken)
                    {
                        keysCount--;
                        col.gameObject.GetComponent<TrapPrefs>().isBroken = true;
                    }
                }
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
                isMoveHorizontal = true;
                isFlip = true;
            }
            else
            {
                if (Input.GetKey(KeyCode.D))
                {
                    resultXMove = position.x + speed;
                    isMoveHorizontal = true;
                    isFlip = false;
                }
                else
                {
                    isMoveHorizontal = false;
                }
            }
        }

        if (!(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)))
        {
            if (Input.GetKey(KeyCode.S))
            {
                isMoveBackward = true;
                isMoveForward = false;
                resultYMove = position.y - speed;
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    resultYMove = position.y + speed;
                    isMoveForward = true;
                    isMoveBackward = false;
                }
                else
                {
                    isMoveBackward = false;
                    isMoveForward = false;
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
        _spriteRenderer.flipX = isFlip;

        if (isMoveForward)
        {
            AnimMoveForward();
        }
        else
        {
            if (isMoveHorizontal)
            {
                AnimMoveHorizontal();
            }
            else
            {
                if (isMoveBackward)
                {
                    AnimMoveBackward();
                }
                else
                {
                    AnimIdle();
                }
            }
        }
    }

    private void AnimMoveForward()
    {
        isMoveForward = true;
        isMoveBackward = false;
        isMoveHorizontal = false;
        isIdle = false;
        SyncWithAnimator();
        _lightScript.SetMods(0, 1);
    }
    
    private void AnimMoveBackward()
    {
        isMoveForward = false;
        isMoveBackward = true;
        isMoveHorizontal = false;
        isIdle = false;
        SyncWithAnimator();
        _lightScript.SetMods(0, -2);
    }
    
    private void AnimMoveHorizontal()
    {
        isMoveForward = false;
        isMoveBackward = false;
        isMoveHorizontal = true;
        isIdle = false;
        SyncWithAnimator();
        _lightScript.SetMods(isFlip ? -1 : 1, 0);
    }
    
    private void AnimIdle()
    {
        isMoveForward = false;
        isMoveBackward = false;
        isMoveHorizontal = false;
        isIdle = true;
        SyncWithAnimator();
    }

    private void SyncWithAnimator()
    {
        _animator.SetBool(IsMoveForward, isMoveForward);
        _animator.SetBool(IsMoveBackward, isMoveBackward);
        _animator.SetBool(IsMoveHorizontal, isMoveHorizontal);
        _animator.SetBool(IsIdle, isIdle);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
            stream.SendNext(keysCount);
            stream.SendNext(isIdle);
            stream.SendNext(isFlip);
            stream.SendNext(isMoveBackward);
            stream.SendNext(isMoveForward);
            stream.SendNext(isMoveHorizontal);
            stream.SendNext(isDead);
            stream.SendNext(isInHat);
            stream.SendNext(hasBlueStone);
            stream.SendNext(hasBigLight);
            stream.SendNext(hasGoldenStatue);
            stream.SendNext(hasRedStone);
        }
        else
        {
            health = (int) stream.ReceiveNext();
            keysCount = (int) stream.ReceiveNext();
            isIdle = (bool) stream.ReceiveNext();
            isFlip = (bool) stream.ReceiveNext();
            isMoveBackward = (bool) stream.ReceiveNext();
            isMoveForward = (bool) stream.ReceiveNext();
            isMoveHorizontal = (bool) stream.ReceiveNext();
            isDead = (bool) stream.ReceiveNext();
            isInHat = (bool) stream.ReceiveNext();
            hasBlueStone = (bool) stream.ReceiveNext();
            hasBigLight = (bool) stream.ReceiveNext();
            hasGoldenStatue = (bool) stream.ReceiveNext();
            hasRedStone = (bool) stream.ReceiveNext();
        }
    }

    public void Hurt(int damage)
    {
        health -= damage;
        Debug.Log("That was hurt... Current health: " + health);

        StartCoroutine(HurtCoroutine());
        
        if (health <= 0)
        {
            MakeDeath();
        }
    }

    IEnumerator HurtCoroutine()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;
    }
    

    public void MakeDeath()
    {
        for (var i = 0; i < 5; i++)
        {
            heardUI[i].SetActive(false);
            brokenHeardsUI[i].SetActive(true);
            
        }
        isDead = true;
        gameManager.ShowLoose();
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
