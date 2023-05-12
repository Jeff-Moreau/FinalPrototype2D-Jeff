using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{

    private bool _leftTurn;
    private bool _rightTurn;
    private bool _thrusterOn;
    private bool _thrusterOff;
    private bool _insertCoin;

    public bool GetLeftTurn =>  _leftTurn;
    public bool GetRightTurn => _rightTurn;
    public bool GetThrusterOn => _thrusterOn;
    public bool GetThrusterOff => _thrusterOff;
    public bool GetInsertCoin => _insertCoin;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))


        _leftTurn = Input.GetKeyDown(KeyCode.A);
        _rightTurn = Input.GetKeyDown(KeyCode.D);
        _thrusterOn = Input.GetKey(KeyCode.W);
        _thrusterOff = Input.GetKeyUp(KeyCode.W);
        _insertCoin = Input.GetKeyDown(KeyCode.Return);
    }
}
