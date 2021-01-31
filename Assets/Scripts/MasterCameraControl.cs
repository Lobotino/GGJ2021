using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCameraControl : MonoBehaviour
{
    public float speed = 10;
    private void FixedUpdate()
    {
        float resultXMove = transform.position.x, resultYMove = transform.position.y;

        if (!(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
        {
            if (Input.GetKey(KeyCode.A))
            {
                resultXMove = transform.position.x - speed;
            }
            else
            {
                if (Input.GetKey(KeyCode.D))
                {
                    resultXMove = transform.position.x + speed;
                }
            }
        }

        if (!(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)))
        {
            if (Input.GetKey(KeyCode.S))
            {
                resultYMove = transform.position.y - speed;
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    resultYMove = transform.position.y + speed;
                }
            }
        }

        gameObject.transform.position = Vector3.Lerp(transform.position, new Vector3(resultXMove, resultYMove, -10), Time.deltaTime);
    }
}
