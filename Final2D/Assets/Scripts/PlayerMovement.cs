using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private GameObject coreGame;
    [SerializeField] private GameObject theThruster;

    private DebugLogger debugLogger;
    private UserInput userInput;
    private Transform shipRotation;
    private Rigidbody2D shipRigidBody;
    private SpriteRenderer thrusterToggle;
    private GameLoop shipFuelTank;

    private int fuelUsage;
    private float shipRotationSpeed;
    private float thrusterForce;

    private void Start()
    {
        debugLogger = GetComponent<DebugLogger>();
        userInput = coreGame.GetComponent<UserInput>();
        shipRotation = GetComponent<Transform>();
        shipRigidBody = GetComponent<Rigidbody2D>();
        thrusterToggle = theThruster.GetComponent<SpriteRenderer>();
        shipFuelTank = coreGame.GetComponent<GameLoop>();

        fuelUsage = 1;
        shipRotationSpeed = 15;
        thrusterForce = 250;
    }

    private void Update()
    {
        if (userInput.LeftTurn)
        {
            shipRotation.eulerAngles += new Vector3(0, 0, shipRotationSpeed);
        }

        if (userInput.RightTurn)
        {
            shipRotation.eulerAngles += new Vector3(0, 0, -shipRotationSpeed);
        }

        if (userInput.ThrusterOff)
        {
            thrusterToggle.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (userInput.Thruster)
        {
            thrusterToggle.enabled = true;
            shipRigidBody.AddForce(transform.up * thrusterForce);
            shipFuelTank.fuelAmount -= fuelUsage;
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
