using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private GameObject thePlayer;
    [SerializeField] private Camera mainCam;

    public int fuelAmount;
    private float playerHorVelocity;
    private float playerVerVelocity;
    private float gameTimeMinutes;
    private float gameTimeSeconds;
    private float gameSeconds;
    private float gameMinutes;
    private float altitudeNow;
    private string newSeconds;

    // Start is called before the first frame update
    void Start()
    {
        fuelAmount = 750;
        gameMinutes = 0;
        gameObject.GetComponent<Hud>().fuelTotal.text = "0000";  
    }

    // Update is called once per frame
    void Update()
    {
        gameTimeMinutes += Time.deltaTime;
        gameTimeSeconds += Time.deltaTime;

        gameObject.GetComponent<Hud>().fuelTotal.text = fuelAmount.ToString();
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
        }

        if (gameTimeSeconds < 10)
        {
            gameSeconds = Mathf.Floor(gameTimeSeconds);
            newSeconds = "0" + gameSeconds;
        }
        else if (gameTimeSeconds >= 10 && gameTimeSeconds < 60)
        {
            gameSeconds = Mathf.Floor(gameTimeSeconds);
            newSeconds = "" + gameSeconds;
        }
        else
        {
            gameTimeSeconds = 0;
        }

        if (gameTimeMinutes >= 60)
        {
            gameMinutes += 1;
            gameTimeMinutes = 0;
        }

        gameObject.GetComponent<Hud>().timeTotal.text = "0" + gameMinutes + ":" + newSeconds;

        
    }
}
