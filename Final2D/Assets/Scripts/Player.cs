using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioSource playerSoundFXSource;
    [SerializeField] private AudioClip soundFXThruster;
    [SerializeField] private AudioClip soundFXShipExplode;
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
    private float playerVerVelocity;

    void Start()
    {
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
            altitude = Mathf.Abs(ground.point.y - transform.position.y);
            //DebugToCon(ground.collider.name + altitude);
        }

        if (userInput.Thruster)
        {
            theThruster.gameObject.SetActive(true);
            myRigidBody.AddForce(transform.up * thrustAmount);

            if (!playerSoundFXSource.isPlaying)
            {
                playerSoundFXSource.PlayOneShot(soundFXThruster);
                DebugToCon("Thruster Sound On");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Mountain")
        {
            if (Mathf.Floor(playerVerVelocity * 100) < -15)
            {
                DebugToCon("The Ship Has Crashed");
                playerSoundFXSource.PlayOneShot(soundFXShipExplode);
            }
            else
            {
                DebugToCon("Ship Has Successfuly Landed");
                transform.position = initialPosition;
                GetComponent<Rigidbody2D>().freezeRotation = true;
                GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
            }
            
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
