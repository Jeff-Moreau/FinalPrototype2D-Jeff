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
    // Start is called before the first frame update
    void Start()
    {
        timerBlink = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timerBlink += Time.deltaTime;

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
        
    }
}
