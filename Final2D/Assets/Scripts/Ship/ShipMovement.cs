using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{

    [SerializeField] private GameObject _coreGame;
    [SerializeField] private GameObject _theThruster;

    private DebugLogger _debugLogger;
    private UserInput _userInput;
    private Transform _shipRotation;
    private Rigidbody2D _shipRigidBody;
    private SpriteRenderer _thrusterToggle;
    private CoreGame _shipFuelTank;

    private int _fuelUsage;
    private float _shipRotationSpeed;
    private float _thrusterForce;

    private void Start()
    {
        _debugLogger = GetComponent<DebugLogger>();
        _userInput = _coreGame.GetComponent<UserInput>();
        _shipRotation = GetComponent<Transform>();
        _shipRigidBody = GetComponent<Rigidbody2D>();
        _thrusterToggle = _theThruster.GetComponent<SpriteRenderer>();
        _shipFuelTank = _coreGame.GetComponent<CoreGame>();

        _fuelUsage = 1;
        _shipRotationSpeed = 15;
        _thrusterForce = 250;
    }

    private void Update()
    {
        if (_userInput.GetLeftTurn)
        {
            _shipRotation.eulerAngles += new Vector3(0, 0, _shipRotationSpeed);
        }

        if (_userInput.GetRightTurn)
        {
            _shipRotation.eulerAngles += new Vector3(0, 0, -_shipRotationSpeed);
        }

        if (_userInput.GetThrusterOff)
        {
            _thrusterToggle.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (_userInput.GetThrusterOn)
        {
            _thrusterToggle.enabled = true;
            _shipRigidBody.AddForce(transform.up * _thrusterForce);
            _shipFuelTank.SetFuelAmount(_shipFuelTank.GetFuelAmount - _fuelUsage);
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
