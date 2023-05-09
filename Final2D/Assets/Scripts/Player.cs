using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource audioThruster;
    [SerializeField] private AudioClip soundThruster;

    private float rotSpeed;
    private float thrustAmount;
    private bool playAudio;

    // Start is called before the first frame update
    void Start()
    {
        playAudio = false;
        rotSpeed = 15;
        thrustAmount = 7.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<Transform>().eulerAngles += new Vector3(0, 0, rotSpeed);
           //transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<Transform>().eulerAngles += new Vector3(0, 0, -rotSpeed);
            //transform.Rotate(Vector3.forward * -rotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log("Audio On");
            playAudio = true;
            rb.AddForce(transform.up * thrustAmount);
            //transform.position += transform.up * thrustAmount;
            //transform.Rotate(Vector3.forward * -rotSpeed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            Debug.Log("Audio Off");
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
                audioThruster.PlayOneShot(soundThruster);
                //audioThruster.Play();
            }
        }
    }
}
