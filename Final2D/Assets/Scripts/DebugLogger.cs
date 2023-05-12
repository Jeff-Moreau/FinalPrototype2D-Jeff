using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogger : MonoBehaviour
{
    [SerializeField] bool _debugOn;
    public void DebugCon(object message, Object sender)
    {
        if (_debugOn)
        {
            Debug.Log(message, sender);
        }

    }
}
