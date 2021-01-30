using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserProperties : MonoBehaviour
{
    public static bool IsMasterInstance;
    
    public static GameObject LocalPlayerInstance;

    public static string UserId;

    void Start()
    {
        IsMasterInstance = Utils.IsPlayerMaster();
    }
}
