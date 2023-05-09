using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTotal;
    [SerializeField] private TextMeshProUGUI timeTotal;
    [SerializeField] public TextMeshProUGUI fuelTotal;
    [SerializeField] private TextMeshProUGUI altitudeCurrent;
    [SerializeField] private TextMeshProUGUI horSpeedCurrent;
    [SerializeField] private TextMeshProUGUI verSpeedCurrent;
    [SerializeField] private TextMeshProUGUI horSpeedArrow;
    [SerializeField] private TextMeshProUGUI verSpeedArrow;
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

        Debug.Log(timerBlink);
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
