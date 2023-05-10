using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreTotal;
    [SerializeField] public TextMeshProUGUI timeTotal;
    [SerializeField] public TextMeshProUGUI fuelTotal;
    [SerializeField] public TextMeshProUGUI altitudeCurrent;
    [SerializeField] public TextMeshProUGUI horSpeedCurrent;
    [SerializeField] public TextMeshProUGUI verSpeedCurrent;
    [SerializeField] public TextMeshProUGUI horSpeedArrow;
    [SerializeField] public TextMeshProUGUI verSpeedArrow;
    [SerializeField] private TextMeshProUGUI insertCoins;
    [SerializeField] private GameObject thePlayer;

    private float timerBlink;
    private float scoreNow;
    // Start is called before the first frame update
    void Start()
    {
        timerBlink = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timerBlink += Time.deltaTime;

        scoreNow = thePlayer.GetComponent<Player>().currentScore;

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
