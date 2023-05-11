using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipThruster : MonoBehaviour
{

    private DebugLogger debugLogger;
    private SpriteRenderer spriteRenderer;
    private AudioSource thrusterAudioSource;

    private void Start()
    {
        debugLogger = GetComponent<DebugLogger>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        thrusterAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        TurnThrusterSoundOn();
    }

    private void TurnThrusterSoundOn()
    {
        if (spriteRenderer.enabled)
        {
            ThrusterSoundOn();
        }
        else
        {
            ThrusterSoundOff();
        }
    }

    private void ThrusterSoundOff()
    {
        if (thrusterAudioSource != null)
        {
            thrusterAudioSource.enabled = false;
            DebugToCon("<color=yellow>Turned Thruster AudioSource Off</color>");
        }
        else
        {
            DebugToCon("<color=red>There is No Thruster AudioSource to turn On.</color>");
        }
    }

    private void ThrusterSoundOn()
    {
        if (thrusterAudioSource != null)
        {
            thrusterAudioSource.enabled = true;
            DebugToCon("<color=green>Turned Thruster AudioSource On</color>");
        }
        else
        {
            DebugToCon("<color=red>There is No Thruster AudioSource to turn On.</color>");
        }
    }

    private void DebugToCon(object message)
    {
        if (debugLogger)
        {
            debugLogger.DebugCon(message, this);
        }
    }
}
