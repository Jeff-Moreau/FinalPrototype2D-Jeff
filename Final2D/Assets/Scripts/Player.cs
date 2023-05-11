using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private DebugLogger debugLogger;
    [SerializeField] private AudioSource playerSoundFXSource;
    [SerializeField] private AudioSource shipWarningSoundFXSource;
    [SerializeField] private AudioSource gameBackgroundSound;
    [SerializeField] private AudioClip[] soundFX;
    [SerializeField] private GameObject mainGame;
    [SerializeField] private GameObject theThruster;
    [SerializeField] private GameObject theHUD;

    public int currentScore;
    public float altitude;
    public float locHit;
    public float altitudeNow;

    private Rigidbody2D myRigidBody; 
    private UserInput userInput;

    private int fuelUsage;
    private int totalFuel;
    private int multiplyScore;
    private int baseScore;
    private float playerVerVelocity;
    private float rotSpeed;
    private float thrustAmount;
    private Vector3 initialPosition;
    private Vector3 initialCam;
    private bool goodToLand = false;

    void Start()
    {
        userInput = mainGame.GetComponent<UserInput>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myRigidBody.freezeRotation = false;
        
        initialCam = mainGame.transform.position;
        initialPosition = transform.position;

        fuelUsage = 1;
        baseScore = 50;
        rotSpeed = 15;
        thrustAmount = 250;
    }

    void Update()
    {
        playerVerVelocity = myRigidBody.velocity.y;


        if (userInput.LeftTurn)
        {
            GetComponent<Transform>().eulerAngles += new Vector3(0, 0, rotSpeed);
        }
        if (userInput.RightTurn)
        {
            GetComponent<Transform>().eulerAngles += new Vector3(0, 0, -rotSpeed);
        }

        if (userInput.ThrusterOff)
        {
            theThruster.gameObject.SetActive(false);

            if (playerSoundFXSource.isPlaying)
            {
                playerSoundFXSource.Stop();
                DebugToCon("Thruster Sound Off");
            }
        }

        if (theHUD.GetComponent<GameLoop>().fuelAmount >0 && theHUD.GetComponent<GameLoop>().fuelAmount <= 100)
        {
            if (!shipWarningSoundFXSource.isPlaying)
            {
                shipWarningSoundFXSource.PlayOneShot(soundFX[2]);
                DebugToCon("Warning Beep");
            }
        }
        else if (theHUD.GetComponent<GameLoop>().fuelAmount <= 0)
        {
            DebugToCon("The Ship Has Crashed");
            playerSoundFXSource.PlayOneShot(soundFX[1]);
            gameBackgroundSound.Stop();
            gameObject.SetActive(false);
        }

    }

    private void FixedUpdate()
    {
        RaycastHit2D ground = Physics2D.Raycast(transform.position, Vector2.down, 500, 3);
        altitudeNow = Mathf.Floor(altitude * 420);

        if (ground.collider != null)
        {
            locHit = ground.point.y;
            //DebugToCon(locHit);
            altitude = Mathf.Abs(ground.point.y - transform.position.y);
            //DebugToCon(ground.collider.name + altitude);
        }

        if (userInput.Thruster)
        {
            theThruster.gameObject.SetActive(true);
            myRigidBody.AddForce(transform.up * thrustAmount);
            theHUD.GetComponent<GameLoop>().fuelAmount -= fuelUsage;

            if (!playerSoundFXSource.isPlaying)
            {
                playerSoundFXSource.PlayOneShot(soundFX[0]);
                DebugToCon("Thruster Sound On");
            }
        }

        if (altitudeNow < 400)
        {
            mainGame.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            mainGame.GetComponent<Camera>().orthographicSize = 1.5f;
        }
        else
        {
            mainGame.transform.position = initialCam;
           mainGame.GetComponent<Camera>().orthographicSize = 5;
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
        theHUD.GetComponent<GameLoop>().fuelAmount -= 100;
        playerSoundFXSource.PlayOneShot(soundFX[1]);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Bonus")
        {
            DebugToCon("You touched a Bonus");
            multiplyScore = collision.gameObject.GetComponent<RandomBonus>().bonusRandom;
            goodToLand = true;
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
