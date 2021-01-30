using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovableItem : MonoBehaviour
{
    public int secondsBeforeDestroy = 5;

    public void DestroySelfAfterTimer()
    {
        StartCoroutine(WaitAndDestroySelf());
    }

    private IEnumerator WaitAndDestroySelf()
    {
        yield return new WaitForSeconds(secondsBeforeDestroy);
        Destroy(gameObject);
    }
}
