using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowsScript : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().receiveShadows = true;
        GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On ; 
    }
}
