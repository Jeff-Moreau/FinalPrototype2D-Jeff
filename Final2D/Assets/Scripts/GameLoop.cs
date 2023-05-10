using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private GameObject thePlayer;
    [SerializeField] private int fuelAmount;
    [SerializeField] private Camera mainCam;

    private float playerHorVelocity;
    private float playerVerVelocity;
    private float gameTime;
    private float gameSeconds;
    private float gameMinutes;
    private float altitudeNow;
    // Start is called before the first frame update
    void Start()
    {
        gameMinutes = 0;
        gameObject.GetComponent<Hud>().fuelTotal.text = "" + 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;

        altitudeNow = thePlayer.GetComponent<Player>().altitude;
        gameObject.GetComponent<Hud>().altitudeCurrent.text = "" + Mathf.Floor(altitudeNow * 420);

        playerHorVelocity = thePlayer.GetComponent<Rigidbody2D>().velocity.x;
        gameObject.GetComponent<Hud>().horSpeedCurrent.text = "" + Mathf.Floor(playerHorVelocity*100);

        if (playerHorVelocity > 0) 
        {
            gameObject.GetComponent<Hud>().horSpeedArrow.text = "→";
        }
        else if (playerHorVelocity < 0)
        {
            gameObject.GetComponent<Hud>().horSpeedArrow.text = "←";
        }
        else if (playerHorVelocity == 0 || playerHorVelocity == 1)
        {
            gameObject.GetComponent<Hud>().horSpeedArrow.text = "";
        }

        playerVerVelocity = thePlayer.GetComponent<Rigidbody2D>().velocity.y;
        gameObject.GetComponent<Hud>().verSpeedCurrent.text = "" + Mathf.Floor(playerVerVelocity * 100);

        if (playerVerVelocity > 0)
        {
            gameObject.GetComponent<Hud>().verSpeedArrow.text = "↑";
        }
        else if (playerVerVelocity < 0)
        {
            gameObject.GetComponent<Hud>().verSpeedArrow.text = "↓";
        }
        else if (playerVerVelocity == 0 || playerVerVelocity == 1)
        {
            gameObject.GetComponent<Hud>().verSpeedArrow.text = "";
        }
        
        if (Input.GetKeyDown(KeyCode.C) && fuelAmount <6000)
        {
            fuelAmount += 750;
            gameObject.GetComponent<Hud>().fuelTotal.text = fuelAmount.ToString();
        }

        if (gameTime < 60)
        {
            gameSeconds = Mathf.Floor(gameTime);
        }

        if (gameTime >= 59)
        {
            gameMinutes = 1;
            gameSeconds = 0;
        }
        gameObject.GetComponent<Hud>().timeTotal.text = gameMinutes + ":" + gameSeconds;
    }
}
