using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Photon.Pun;
using UnityEngine;

public class DroticsController : MonoBehaviour, ITrapSetter
{
    public bool isLeftWall = true, isVertical = false;
    public int droticsCount = 8;
    public float shotSpeedInSeconds = 0.2f;
    public float shotPower = 10f;

    public float cellParallax = 1.5f;

    void Start()
    {
        ShotDrotics();
    }

    public void ShotDrotics()
    {
        StartCoroutine(WaitAndShot());
    }

    private IEnumerator WaitAndShot()
    {
        for (int i = 0; i < droticsCount; i++)
        {
            yield return new WaitForSeconds(shotSpeedInSeconds);

            var startRotation = !isLeftWall ? Quaternion.AngleAxis(isVertical ? 90 : 0, new Vector3(0, 0, 1)) : Quaternion.AngleAxis(180, new Vector3(0, 0, 1));
            
            var drotic = PhotonNetwork.Instantiate("drotic", transform.position, startRotation);

            if (isVertical)
            {
                drotic.GetComponent<Rigidbody2D>()
                    .AddForce(new Vector2(0, -shotPower), ForceMode2D.Impulse);
            }
            else
            {
                drotic.GetComponent<Rigidbody2D>()
                    .AddForce(new Vector2(isLeftWall ? shotPower : -shotPower, 0), ForceMode2D.Impulse);
            }
        }
        
        GetComponentInParent<RemovableItem>().DestroySelfAfterTimer();
    }

    public bool TryToInstantiateTrap(Vector2 mousePos)
    {
        if (Camera.main == null) return false;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag.Equals("Borders"))
            {
                if (isVertical)
                {
                    var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                    int posX = (int) worldPos.x;
                    int posY = (int) worldPos.y;
                    var worldCenterCellResult = new Vector3(posX - 0.5f, posY - cellParallax, -10);
                    RaycastHit2D neighbortCell = Physics2D.Raycast(worldCenterCellResult, Vector2.zero);
                    Debug.Log("raycast to " + worldCenterCellResult);
                    if (neighbortCell.collider == null || !neighbortCell.collider.gameObject.tag.Equals("Borders") &&
                        !neighbortCell.collider.gameObject.tag.Equals("Traps"))
                    {
                        PhotonNetwork.Instantiate("DroticsWallTop", new Vector2(posX - 0.5f, posY - 0.5f), Quaternion.identity);
                        return true;
                    }
                }
                else
                {
                    if (isLeftWall)
                    {
                        var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                        int posX = (int) worldPos.x;
                        int posY = (int) worldPos.y;
                        var worldCenterCellResult = new Vector3(posX + 2f, posY - 0.5f, 0);
                        RaycastHit2D neighbortCell = Physics2D.Raycast(worldCenterCellResult, Vector2.zero);
                        Debug.Log("leftWall raycast to " + worldCenterCellResult);
                        if (neighbortCell.collider == null ||
                            !neighbortCell.collider.gameObject.tag.Equals("Borders") &&
                            !neighbortCell.collider.gameObject.tag.Equals("Traps"))
                        {
                            PhotonNetwork.Instantiate("DroticsWallLeft", new Vector2(posX, posY - 0.5f),
                                Quaternion.identity);
                            return true;
                        }
                    }
                    else
                    {
                        var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                        int posX = (int) worldPos.x;
                        int posY = (int) worldPos.y;
                        var worldCenterCellResult = new Vector3(posX - 2f, posY - 0.5f, 0);
                        RaycastHit2D neighbortCell = Physics2D.Raycast(worldCenterCellResult, Vector2.zero);
                        Debug.Log("rightWall raycast to " + worldCenterCellResult);
                        if (neighbortCell.collider == null ||
                            !neighbortCell.collider.gameObject.tag.Equals("Borders") &&
                            !neighbortCell.collider.gameObject.tag.Equals("Traps"))
                        {
                            PhotonNetwork.Instantiate("DroticsWallRight", new Vector2(posX - 0.8f, posY - 0.5f),
                                Quaternion.identity);
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
}
