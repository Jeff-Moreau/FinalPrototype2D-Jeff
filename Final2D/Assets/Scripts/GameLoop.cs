using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private GameObject thePlayer;
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject theHUD;

    public int fuelAmount;

    private float playerHorVelocity;
    private float playerVerVelocity;
    private float gameTimeMinutes;
    private float gameTimeSeconds;
    private float gameSeconds;
    private float gameMinutes;
    private float altitudeNow;
    private string newSeconds;

    void Start()
    {
        fuelAmount = 750;
        gameMinutes = 0;
    }

    void Update()
    {
        gameTimeMinutes += Time.deltaTime;
        gameTimeSeconds += Time.deltaTime;

        theHUD.GetComponent<Hud>().fuelTotal.text = fuelAmount.ToString();
        altitudeNow = thePlayer.GetComponent<Player>().altitude;
        theHUD.GetComponent<Hud>().altitudeCurrent.text = "" + Mathf.Floor(altitudeNow * 420);

        HorArrows();
        VerArrows();
        SettingTime();

        if (Input.GetKeyDown(KeyCode.C) && fuelAmount < 6000)
        {
            fuelAmount += 750;
        }

        if (fuelAmount <= 0)
        {
            fuelAmount = 0;
        }
    }

    private void SettingTime()
    {
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

        theHUD.GetComponent<Hud>().timeTotal.text = "0" + gameMinutes + ":" + newSeconds;
    }

    private void VerArrows()
    {
        playerVerVelocity = thePlayer.GetComponent<Rigidbody2D>().velocity.y;
        theHUD.GetComponent<Hud>().verSpeedCurrent.text = "" + Mathf.Floor(playerVerVelocity * 100);

        if (playerVerVelocity > 0)
        {
            theHUD.GetComponent<Hud>().verSpeedArrow.text = "↑";
        }
        else if (playerVerVelocity < 0)
        {
            theHUD.GetComponent<Hud>().verSpeedArrow.text = "↓";
        }
        else if (playerVerVelocity == 0 || playerVerVelocity == 1)
        {
            theHUD.GetComponent<Hud>().verSpeedArrow.text = "";
        }
    }

    private void HorArrows()
    {
        playerHorVelocity = thePlayer.GetComponent<Rigidbody2D>().velocity.x;
        theHUD.GetComponent<Hud>().horSpeedCurrent.text = "" + Mathf.Floor(playerHorVelocity * 100);

        if (playerHorVelocity > 0)
        {
            theHUD.GetComponent<Hud>().horSpeedArrow.text = "→";
        }
        else if (playerHorVelocity < 0)
        {
            theHUD.GetComponent<Hud>().horSpeedArrow.text = "←";
        }
        else if (playerHorVelocity == 0 || playerHorVelocity == 1)
        {
            theHUD.GetComponent<Hud>().horSpeedArrow.text = "";
        }
    }
}
