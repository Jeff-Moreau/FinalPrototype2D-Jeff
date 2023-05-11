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
        spriteRenderer = GetComponent<SpriteRenderer>();
        thrusterAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        TurnAudioOn();
    }

    private void TurnAudioOn()
    {
        if (spriteRenderer.enabled)
        {
            thrusterAudioSource.enabled = true;
        }
        else
        {
            thrusterAudioSource.enabled = false;
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
