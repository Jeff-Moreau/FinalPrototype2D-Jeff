using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTotal;
    [SerializeField] private TextMeshProUGUI timeTotal;
    [SerializeField] private TextMeshProUGUI fuelTotal;
    [SerializeField] private TextMeshProUGUI shipAltitudeCurrent;
    [SerializeField] private TextMeshProUGUI shipHorSpeedCurrent;
    [SerializeField] private TextMeshProUGUI shipVerSpeedCurrent;
    [SerializeField] private TextMeshProUGUI horSpeedArrow;
    [SerializeField] private TextMeshProUGUI verSpeedArrow;
    [SerializeField] private TextMeshProUGUI insertCoins;
    [SerializeField] private GameObject thePlayer;

    private float timerBlink;
    private float scoreNow;
 
    public void SetTimeTotal(string time)
    {
        timeTotal.text = time;
    }

    public void SetFuelTotal(string fuel)
    {
        fuelTotal.text = fuel;
    }

    public void SetAltitudeCurrent(string altitude)
    {
        shipAltitudeCurrent.text = altitude;
    }

    public void SetShipHorSpeedCurrent(string horSpeed)
    {
        shipHorSpeedCurrent.text = horSpeed;
    }

    public void SetShipVerSpeedCurrent(string verSpeed)
    {
        shipVerSpeedCurrent.text = verSpeed;
    }

    public void SetHorSpeedArrow(string horArrow)
    {
        horSpeedArrow.text = horArrow;
    }

    public void SetVerSpeedArrow(string verArrow)
    {
        verSpeedArrow.text = verArrow;
    }

    private void Start()
    {
        fuelTotal.text = "0000";
        timerBlink = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        timerBlink += Time.deltaTime;

        scoreNow = thePlayer.GetComponent<Player>().GetCurrentScore();

        if (timerBlink < 1.5f)
        {
            insertCoins.enabled = true;
        }
        else if (timerBlink > 1.5f && timerBlink < 2f)
        {
            insertCoins.enabled = false;
        }
        else
        {
            timerBlink = 0;
        }
        if (scoreNow == 0)
        {
            scoreTotal.text = "0000";
        }
        else if (scoreNow>0&& scoreNow <100)
        {
            scoreTotal.text = "00" + scoreNow;
        }
        else if (scoreNow > 100 && scoreNow < 1000)
        {
            scoreTotal.text = "0" + scoreNow;
        }
    }
}
