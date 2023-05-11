using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipThrusterSound : MonoBehaviour
{
    [SerializeField] private AudioClip thrusterSound;

    private DebugLogger debugLogger;
    private AudioSource thrusterAudioSource;

    private bool thrusterAudioSourceIsActive;

    private void Start()
    {
        debugLogger = GetComponent<DebugLogger>();
        thrusterAudioSource = GetComponent<AudioSource>();
        thrusterAudioSourceIsActive = false;

        ThrusterAudioSourceCheck();
    }

    private void Update()
    {
        ThrusterSoundPlay();
    }

    private void ThrusterAudioSourceCheck()
    {
        if (thrusterAudioSource != null)
        {
            thrusterAudioSource.loop = true;
            thrusterAudioSourceIsActive = true;
        }
        else
        {
            thrusterAudioSourceIsActive = false;
            DebugToCon("<color=red>No AudioSource is available for the Thruster.</color>");
        }
    }

    private void ThrusterSoundPlay()
    {
            if (thrusterAudioSourceIsActive && thrusterAudioSource.enabled)
            {
                if (!thrusterAudioSource.isPlaying)
                {
                    thrusterAudioSource.PlayOneShot(thrusterSound);
                    DebugToCon("Thruster Sound On");
                }
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
