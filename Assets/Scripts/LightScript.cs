using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Vector3 = UnityEngine.Vector3;

public class LightScript : MonoBehaviour
{
    public Light2D light;

    public float modifier;
    
    public float xMod, yMod;

    public float startCounter = 200;
    public float countDown = 200;
    public float fadeSpeed = 1;

    private bool isGoTOShine;
    
    void Update()
    {
//        if (isGoTOShine)
//        {
//            countDown -= Time.deltaTime;
//            float lerpVal = 1f - countDown / fadeSpeed;
//
//            // now lerp light intensity
//            light.intensity = Mathf.Lerp(light.intensity, 1, lerpVal);
//
//            if (countDown <= 0f)
//            {
//                isGoTOShine = false;
//                countDown = startCounter;
//            }
//        }
//        else
//        {
//            countDown -= Time.deltaTime;
//            float lerpVal = 1f - countDown / fadeSpeed;
//
//            // now lerp light intensity
//            light.intensity = Mathf.Lerp(light.intensity, 0, lerpVal);
//
//            if (countDown <= 0f)
//            {
//                isGoTOShine = false;
//                countDown = startCounter;
//            }
//        }

        var position = gameObject.transform.position;
//        light.transform.position = new Vector3(position.x + xMod * modifier, position.y + yMod * modifier, 0);
    }

    public void SetMods(float x, float y)
    {
        xMod = x;
        yMod = y;
    }
}
