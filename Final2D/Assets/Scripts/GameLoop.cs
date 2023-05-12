using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private GameObject thePlayer;
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject theHUD;

    private Hud writeToHud;

    public int fuelAmount;

    private float playerHorVelocity;
    private float playerVerVelocity;
    private float gameTimeMinutes;
    private float gameTimeSeconds;
    private float gameSeconds;
    private float gameMinutes;
    private float shipCurrentAltitude;
    private string newSeconds;

    void Start()
    {
        writeToHud = theHUD.GetComponent<Hud>();
        fuelAmount = 750;
        gameMinutes = 0;
    }

    void Update()
    {
        gameTimeMinutes += Time.deltaTime;
        gameTimeSeconds += Time.deltaTime;

        writeToHud.SetFuelTotal(fuelAmount.ToString());
        shipCurrentAltitude = thePlayer.GetComponent<Player>().GetShipAltitude();
        writeToHud.SetAltitudeCurrent("" + Mathf.Floor(shipCurrentAltitude * 420));

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

        writeToHud.SetTimeTotal("0" + gameMinutes + ":" + newSeconds);
    }

    private void VerArrows()
    {
        playerVerVelocity = thePlayer.GetComponent<Rigidbody2D>().velocity.y;
        writeToHud.SetShipVerSpeedCurrent("" + Mathf.Floor(playerVerVelocity * 100));

        if (playerVerVelocity > 0)
        {
            writeToHud.SetVerSpeedArrow("↑");
        }
        else if (playerVerVelocity < 0)
        {
            writeToHud.SetVerSpeedArrow("↓");
        }
        else if (playerVerVelocity == 0 || playerVerVelocity == 1)
        {
            writeToHud.SetVerSpeedArrow("");
        }
    }

    private void HorArrows()
    {
        playerHorVelocity = thePlayer.GetComponent<Rigidbody2D>().velocity.x;
        writeToHud.SetShipHorSpeedCurrent("" + Mathf.Floor(playerHorVelocity * 100));

        if (playerHorVelocity > 0)
        {
            writeToHud.SetHorSpeedArrow("→");
        }
        else if (playerHorVelocity < 0)
        {
            writeToHud.SetHorSpeedArrow("←");
        }
        else if (playerHorVelocity == 0 || playerHorVelocity == 1)
        {
            writeToHud.SetHorSpeedArrow("");
        }
    }
}
