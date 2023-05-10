using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogger : MonoBehaviour
{
    [SerializeField] bool debugOn;
    public void DebugCon(object message, Object sender)
    {
        if (debugOn)
        {
            Debug.Log(message, sender);
        }

    }
}
