using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GoldenStatueLight : MonoBehaviour
{
    private Light2D light;

    public GameManager gameManager;

    public int step = 5;

    private bool isRed = false;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isRed && gameManager.isGoldenStatueStolen)
        {
            light.color = Color.red;
            isRed = true;
        }
        
        transform.Rotate(new Vector3(0,0,1), step);
    }
}
