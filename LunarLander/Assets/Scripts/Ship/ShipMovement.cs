using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{

    [SerializeField] private DebugLogger _debugLogger;
    [SerializeField] private UserInput _userInput;
    [SerializeField] private Transform _shipRotation;
    [SerializeField] private Rigidbody2D _shipRigidBody;
    [SerializeField] private SpriteRenderer _thrusterToggle;
    [SerializeField] private Ship _ship;
    
    private int _fuelUsage;
    private float _shipRotationSpeed;
    private float _thrusterForce;

    private void Start()
    {
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
            _ship.SetShipCurrentFuelInTank(_ship.GetShipCurrentFuelInTank - _fuelUsage);
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
