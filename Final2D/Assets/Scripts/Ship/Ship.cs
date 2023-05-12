using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
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
    private Transform shipRotation;

    private int currentScore;
    private int scoreMultiplier;
    private int baseScore;
    private float shipCurrentAltitude;
    private float shipAltitude;
    private float shipVerVelocity;
    private Vector3 initialShipPosition;
    private Vector3 initialCameraPosition;
    private bool goodToLand = false;

    public int GetCurrentScore() => currentScore;
    public float GetShipAltitude() => shipAltitude;

    void Start()
    {
        shipFuelTank = coreGame.GetComponent<GameLoop>();
        debugLogger = GetComponent<DebugLogger>();
        shipRigidBody = GetComponent<Rigidbody2D>();
        shipRotation = GetComponent<Transform>();
        gameCamera = Camera.main;
        
        initialCameraPosition = gameCamera.transform.position;
        initialShipPosition = transform.position;

        shipRigidBody.freezeRotation = false;
        baseScore = 50;
    }

    void Update()
    {
        shipVerVelocity = shipRigidBody.velocity.y;

        if (shipFuelTank.GetFuelAmount() > 0 && shipFuelTank.GetFuelAmount() <= 100)
        {
            if (!shipWarningSoundFXSource.isPlaying)
            {
                shipWarningSoundFXSource.PlayOneShot(soundFX[2]);
                DebugToCon("Warning Beep");
            }
        }
        else if (shipFuelTank.GetFuelAmount() <= 0)
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
            gameCamera.orthographicSize = 1.5f;
        }
        else
        {
            gameCamera.transform.position = initialCameraPosition;
            gameCamera.orthographicSize = 5;
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
                    currentScore += (baseScore * scoreMultiplier);

                    ShipLanded();
                }
            }
        }
    }

    private void ShipLanded()
    {
        DebugToCon("Ship Has Successfuly Landed");
        transform.position = initialShipPosition;
        shipRigidBody.freezeRotation = true;
        shipRotation.eulerAngles = new Vector3(0, 0, 0);
     }

    private void ShipExplode()
    {
        DebugToCon("The Ship Has Crashed");
        transform.position = initialShipPosition;
        shipRigidBody.freezeRotation = true;
        shipRotation.eulerAngles = new Vector3(0, 0, 0);
        shipFuelTank.SetFuelAmount( shipFuelTank.GetFuelAmount() - 100);
        playerSoundFXSource.PlayOneShot(soundFX[1]);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Bonus"))
        {
            DebugToCon("You are touching a Bonus");
            scoreMultiplier = collision.GetComponent<RandomBonus>().GetBonusRandom();
            goodToLand = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bonus"))
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
