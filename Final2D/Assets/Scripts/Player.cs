using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource audioThruster;
    [SerializeField] private GameObject mainGame;
    [SerializeField] private GameObject theThruster;
    [SerializeField] private DebugLogger debugLogger;

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
    }

    void Update()
    {
        playerVerVelocity = GetComponent<Rigidbody2D>().velocity.y;
        GetComponent<Rigidbody2D>().freezeRotation = false;
        if (userInput.LeftTurn)
        {
            GetComponent<Transform>().eulerAngles += new Vector3(0, 0, rotSpeed);
        }
        if (userInput.RightTurn)
        {
            GetComponent<Transform>().eulerAngles += new Vector3(0, 0, -rotSpeed);
        }

        if (!userInput.Thruster)
        {
            DebugToCon("Audio Off");
            theThruster.gameObject.SetActive(false);
            if (audioThruster.isPlaying)
            {
                audioThruster.Stop();
                playAudio = false;
            }
        }
        if (playAudio)
        {
            if (!audioThruster.isPlaying)
            {
                DebugToCon("Audio On");
                audioThruster.Play();
            }
        }
    }

    private void FixedUpdate()
    {
        RaycastHit2D ground = Physics2D.Raycast(transform.position, Vector2.down, 500, 3);

        if (ground.collider != null)
        {
            altitude = Mathf.Abs(ground.point.y - transform.position.y);
            DebugToCon(ground.collider.name + altitude);
        }

        if (userInput.Thruster)
        {
            theThruster.gameObject.SetActive(true);
            playAudio = true;
            rb.AddForce(transform.up * thrustAmount);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Mountain")
        {
            if (Mathf.Floor(playerVerVelocity * 100) < -15)
            {
                Destroy(gameObject);
            }
            else
            {
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
