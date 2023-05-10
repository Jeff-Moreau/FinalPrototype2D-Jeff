using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource audioThruster;
    [SerializeField] private GameObject mainGame;
    [SerializeField] private GameObject theThruster;

    private UserInput userInput;
    private float rotSpeed;
    private float thrustAmount;
    private bool playAudio;
    private Vector3 initialPosition;
    public float altitude;

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
            Debug.Log("Audio Off");
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
                Debug.Log("Audio On");
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
            Debug.Log(ground.collider.name + altitude);
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
            transform.position = initialPosition;
            GetComponent<Rigidbody2D>().freezeRotation = true;
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
