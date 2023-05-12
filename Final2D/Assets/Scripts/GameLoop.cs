using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private GameObject theShip;
    [SerializeField] private GameObject theHUD;

    private Ship shipAltitude;
    private Hud writeToHud;
    private Rigidbody2D shipRigidBody;

    private int fuelAmount;
    private float playerHorVelocity;
    private float playerVerVelocity;
    private float gameTimeMinutes;
    private float gameTimeSeconds;
    private float gameTime;
    private float gameSeconds;
    private float gameMinutes;
    private float shipCurrentAltitude;
    private string newSeconds;

    public int GetFuelAmount() => fuelAmount;
    public void SetFuelAmount(int changeFuel) => fuelAmount = changeFuel;
    void Start()
    {
        shipRigidBody = theShip.GetComponent<Rigidbody2D>();
        shipAltitude = theShip.GetComponent<Ship>();
        writeToHud = theHUD.GetComponent<Hud>();
        fuelAmount = 750;
        gameMinutes = 0;
    }

    void Update()
    {
        gameTime = Time.deltaTime;

        writeToHud.SetFuelTotal(fuelAmount.ToString());
        shipCurrentAltitude = shipAltitude.GetShipAltitude();
        writeToHud.SetAltitudeCurrent(Mathf.Floor(shipCurrentAltitude * 420).ToString());

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
        gameTimeSeconds += gameTime;
        gameTimeMinutes += gameTime;

        if (gameTimeSeconds < 10)
        {
            gameSeconds = Mathf.Floor(gameTimeSeconds);
            newSeconds = "0" + gameSeconds;
        }
        else if (gameTimeSeconds >= 10 && gameTimeSeconds < 60)
        {
            gameSeconds = Mathf.Floor(gameTimeSeconds);
            newSeconds = gameSeconds.ToString();
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
        playerVerVelocity = shipRigidBody.velocity.y;
        writeToHud.SetShipVerSpeedCurrent(Mathf.Floor(playerVerVelocity * 100).ToString());

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
        playerHorVelocity = shipRigidBody.velocity.x;
        writeToHud.SetShipHorSpeedCurrent(Mathf.Floor(playerHorVelocity * 100).ToString());

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
