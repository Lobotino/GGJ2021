
using UnityEngine;

public class Utils 
{
    public static bool IsPlayerMaster()
    {
        return PlayerPrefs.GetInt("isMaster", 0) == 1;
    }
}
