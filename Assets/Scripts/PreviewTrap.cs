using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewTrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main == null) return;
        var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3f);
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
