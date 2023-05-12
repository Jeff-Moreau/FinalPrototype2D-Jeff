using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private AudioSource playerSoundFXSource;
    [SerializeField] private AudioSource shipWarningSoundFXSource;
    [SerializeField] private AudioSource gameBackgroundSound;
    [SerializeField] private AudioClip[] soundFX;
    [SerializeField] private GameObject coreGame;
    [SerializeField] private GameObject theThruster;

    private GameLoop shipFuelTank;
    private DebugLogger debugLogger;
    private Rigidbody2D shipRigidBody;
    private Camera gameCamera;
    
    private int currentScore;
    private int multiplyScore;
    private int baseScore;
    private float shipCurrentAltitude;
    private float shipAltitude;
    private float shipVerVelocity;
    private Vector3 initialShipPosition;
    private Vector3 initialCameraPosition;
    private bool goodToLand = false;

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public float GetShipAltitude()
    {
        return shipAltitude;
    }

    void Start()
    {
        shipFuelTank = coreGame.GetComponent<GameLoop>();
        debugLogger = GetComponent<DebugLogger>();
        shipRigidBody = GetComponent<Rigidbody2D>();
        gameCamera = Camera.main;

        initialCameraPosition = gameCamera.transform.position;
        initialShipPosition = transform.position;

        shipRigidBody.freezeRotation = false;
        baseScore = 50;
    }

    void Update()
    {
        shipVerVelocity = shipRigidBody.velocity.y;

        if (shipFuelTank.fuelAmount >0 && shipFuelTank.fuelAmount <= 100)
        {
            if (!shipWarningSoundFXSource.isPlaying)
            {
                shipWarningSoundFXSource.PlayOneShot(soundFX[2]);
                DebugToCon("Warning Beep");
            }
        }
        else if (shipFuelTank.fuelAmount <= 0)
        {
            DebugToCon("The Ship Has Crashed");
            playerSoundFXSource.PlayOneShot(soundFX[1]);
            gameBackgroundSound.Stop();
            gameObject.SetActive(false);
        }
        DebugToCon("Good To Land: " + goodToLand);
    }

    private void FixedUpdate()
    {
        RaycastHit2D mountain = Physics2D.Raycast(transform.position, Vector2.down, 500, 3);
        shipCurrentAltitude = Mathf.Floor(shipAltitude * 420);

        if (mountain.collider != null)
        {
            shipAltitude = Mathf.Abs(mountain.point.y - transform.position.y);
        }

        if (shipCurrentAltitude < 400)
        {
            gameCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            gameCamera.GetComponent<Camera>().orthographicSize = 1.5f;
        }
        else
        {
            gameCamera.transform.position = initialCameraPosition;
            gameCamera.GetComponent<Camera>().orthographicSize = 5;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Mountain")
        {
            if (!goodToLand)
            {
                currentScore += (baseScore - 30);
                ShipExplode();
            }
            else
            {
                if (Mathf.Floor(shipVerVelocity * 100) < -10)
                {
                    currentScore += (baseScore / 2);
                    ShipExplode();
                }
                else
                {
                    currentScore += (baseScore * multiplyScore);

                    ShipLanded();
                }
            }
        }
    }

    private void ShipLanded()
    {
        DebugToCon("Ship Has Successfuly Landed");
        transform.position = initialShipPosition;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
     }

    private void ShipExplode()
    {
        DebugToCon("The Ship Has Crashed");
        transform.position = initialShipPosition;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
        coreGame.GetComponent<GameLoop>().fuelAmount -= 100;
        playerSoundFXSource.PlayOneShot(soundFX[1]);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Bonus")
        {
            DebugToCon("You are touching a Bonus");
            multiplyScore = collision.gameObject.GetComponent<RandomBonus>().bonusRandom;
            goodToLand = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Bonus")
        {
            goodToLand = false;
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
