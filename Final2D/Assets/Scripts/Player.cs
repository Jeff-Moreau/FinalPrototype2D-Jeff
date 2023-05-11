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
    //[SerializeField] private GameObject theHUD;

    public int currentScore;
    public float altitude;
    public float locHit;
    public float altitudeNow;

    private DebugLogger debugLogger;
    private Rigidbody2D myRigidBody; 
    private Camera gameCamera;

    private int multiplyScore;
    private int baseScore;
    private float playerVerVelocity;
    private Vector3 initialPosition;
    private Vector3 initialCamPosition;
    private bool goodToLand = false;

    void Start()
    {
        debugLogger = GetComponent<DebugLogger>();
        myRigidBody = GetComponent<Rigidbody2D>();
        gameCamera = Camera.main;

        initialCamPosition = gameCamera.transform.position;
        initialPosition = transform.position;

        myRigidBody.freezeRotation = false;
        baseScore = 50;
    }

    void Update()
    {
        playerVerVelocity = myRigidBody.velocity.y;

        if (coreGame.GetComponent<GameLoop>().fuelAmount >0 && coreGame.GetComponent<GameLoop>().fuelAmount <= 100)
        {
            if (!shipWarningSoundFXSource.isPlaying)
            {
                shipWarningSoundFXSource.PlayOneShot(soundFX[2]);
                DebugToCon("Warning Beep");
            }
        }
        else if (coreGame.GetComponent<GameLoop>().fuelAmount <= 0)
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
        RaycastHit2D ground = Physics2D.Raycast(transform.position, Vector2.down, 500, 3);
        altitudeNow = Mathf.Floor(altitude * 420);

        if (ground.collider != null)
        {
            locHit = ground.point.y;
            altitude = Mathf.Abs(ground.point.y - transform.position.y);
        }

        if (altitudeNow < 400)
        {
            gameCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            gameCamera.GetComponent<Camera>().orthographicSize = 1.5f;
        }
        else
        {
            gameCamera.transform.position = initialCamPosition;
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
                if (Mathf.Floor(playerVerVelocity * 100) < -10)
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
        transform.position = initialPosition;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
     }

    private void ShipExplode()
    {
        DebugToCon("The Ship Has Crashed");
        transform.position = initialPosition;
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
