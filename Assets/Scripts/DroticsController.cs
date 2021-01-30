using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class DroticsController : MonoBehaviour
{
    public bool isLeft = true, isVertical = false;
    public int droticsCount = 8;
    public float shotSpeedInSeconds = 0.2f;
    public float shotPower = 10f;

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
            var drotic = PhotonNetwork.Instantiate("drotic", transform.position, Quaternion.identity);
            if (!isLeft)
            {
                if (isVertical)
                {
                    drotic.transform.rotation = new Quaternion(0, 0, 270, 0);
                }
                else
                {
                    drotic.transform.rotation = new Quaternion(0, 0, 180, 0);
                }
            }

            if (isVertical)
            {
                drotic.GetComponent<Rigidbody2D>()
                    .AddForce(new Vector2(0, -shotPower), ForceMode2D.Impulse);
            }
            else
            {
                drotic.GetComponent<Rigidbody2D>()
                    .AddForce(new Vector2(isLeft ? -shotPower : shotPower, 0), ForceMode2D.Impulse);
            }
        }
    }
}
