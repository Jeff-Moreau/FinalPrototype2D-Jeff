using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipThruster : MonoBehaviour
{

    private DebugLogger _debugLogger;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _thrusterAudioSource;

    private void Start()
    {
        _debugLogger = GetComponent<DebugLogger>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _thrusterAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        TurnThrusterSoundOn();
    }

    private void TurnThrusterSoundOn()
    {
        if (_spriteRenderer.enabled)
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
        if (_thrusterAudioSource != null)
        {
            _thrusterAudioSource.enabled = false;
            DebugToCon("<color=yellow>Turned Thruster AudioSource Off</color>");
        }
        else
        {
            DebugToCon("<color=red>There is No Thruster AudioSource to turn On.</color>");
        }
    }

    private void ThrusterSoundOn()
    {
        if (_thrusterAudioSource != null)
        {
            _thrusterAudioSource.enabled = true;
            DebugToCon("<color=green>Turned Thruster AudioSource On</color>");
        }
        else
        {
            DebugToCon("<color=red>There is No Thruster AudioSource to turn On.</color>");
        }
    }

    private void DebugToCon(object message)
    {
        if (_debugLogger)
        {
            _debugLogger.DebugCon(message, this);
        }
    }
}
