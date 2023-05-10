using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioSource playerSoundFXSource;
    [SerializeField] private AudioClip[] soundFX;
    [SerializeField] private GameObject mainGame;
    [SerializeField] private GameObject theThruster;
    [SerializeField] private DebugLogger debugLogger;

    private Rigidbody2D myRigidBody; 
    private UserInput userInput;
    private float rotSpeed;
    private float thrustAmount;
    private bool playAudio;
    private Vector3 initialPosition;
    public float altitude;
    public float locHit;
    public float altitudeNow;
    private float playerVerVelocity;
    private Vector3 initialCam;
    private int multiplyScore;
    private int baseScore;
    public int currentScore;

    void Start()
    {
        baseScore = 50;
        initialCam = mainGame.transform.position;
        userInput = mainGame.GetComponent<UserInput>();
        initialPosition = transform.position;
        playAudio = false;
        rotSpeed = 15;
        thrustAmount = 250;
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerVerVelocity = myRigidBody.velocity.y;
        myRigidBody.freezeRotation = false;
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

    }

    private void FixedUpdate()
    {
        RaycastHit2D ground = Physics2D.Raycast(transform.position, Vector2.down, 500, 3);

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

            if (!playerSoundFXSource.isPlaying)
            {
                playerSoundFXSource.PlayOneShot(soundFX[0]);
                DebugToCon("Thruster Sound On");
            }
        }
        altitudeNow = Mathf.Floor(altitude * 420);
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
            if (Mathf.Floor(playerVerVelocity * 100) < -10)
            {
                currentScore += (baseScore/2);
                DebugToCon("The Ship Has Crashed");
                playerSoundFXSource.PlayOneShot(soundFX[1]);
            }
            else
            {
                currentScore += (baseScore * multiplyScore);
                DebugToCon("Ship Has Successfuly Landed");
                transform.position = initialPosition;
                GetComponent<Rigidbody2D>().freezeRotation = true;
                GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Bonus")
        {
            DebugToCon("You touched a Bonus");
            multiplyScore = collision.gameObject.GetComponent<RandomBonus>().bonusRandom;
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
