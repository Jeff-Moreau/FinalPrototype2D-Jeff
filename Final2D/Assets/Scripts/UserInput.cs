using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{

    [SerializeField] private KeyCode rotateLeft;
    [SerializeField] private KeyCode rotateRight;
    [SerializeField] private KeyCode thrusterOn;

    public bool LeftTurn {  get; private set; }
    public bool RightTurn { get; private set; }
    public bool Thruster { get; private set; }
    public bool ThrusterOff { get; private set; }

    void Update()
    {
        GetKeyInput();
    }

    private void GetKeyInput()
    {
        LeftTurn = Input.GetKeyDown(rotateLeft);
        RightTurn = Input.GetKeyDown(rotateRight);
        Thruster = Input.GetKey(thrusterOn);
        ThrusterOff = Input.GetKeyUp(thrusterOn);
    }
}
