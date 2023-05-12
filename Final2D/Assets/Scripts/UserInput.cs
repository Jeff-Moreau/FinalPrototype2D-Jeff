using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{

    private KeyCode rotateLeft;
    private KeyCode rotateRight;
    private KeyCode thrusterOn;

    public bool LeftTurn {  get; private set; }
    public bool RightTurn { get; private set; }
    public bool Thruster { get; private set; }
    public bool ThrusterOff { get; private set; }

    private void Start()
    {
        rotateLeft = KeyCode.A;
        rotateRight = KeyCode.D;
        thrusterOn = KeyCode.W;
    }
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
