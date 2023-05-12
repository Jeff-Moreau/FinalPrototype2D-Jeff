using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipThrusterSound : MonoBehaviour
{

    [SerializeField] private AudioClip _thrusterSound;

    private DebugLogger _debugLogger;
    private AudioSource _thrusterAudioSource;

    private bool _thrusterAudioSourceIsActive = false;

    private void Start()
    {
        _debugLogger = GetComponent<DebugLogger>();
        _thrusterAudioSource = GetComponent<AudioSource>();

        ThrusterAudioSourceCheck();
    }

    private void Update()
    {
        ThrusterSoundPlay();
    }

    private void ThrusterAudioSourceCheck()
    {
        if (_thrusterAudioSource != null)
        {
            _thrusterAudioSource.loop = true;
            _thrusterAudioSourceIsActive = true;
        }
        else
        {
            _thrusterAudioSourceIsActive = false;
            DebugToCon("<color=red>No AudioSource available to play Thruster Sound.</color>");
        }
    }

    private void ThrusterSoundPlay()
    {
            if (_thrusterAudioSourceIsActive && _thrusterAudioSource.enabled)
            {
                if (!_thrusterAudioSource.isPlaying)
                {
                    _thrusterAudioSource.PlayOneShot(_thrusterSound);
                    DebugToCon("<color=orange>Thruster Sound is Playing</color>");
                }
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
