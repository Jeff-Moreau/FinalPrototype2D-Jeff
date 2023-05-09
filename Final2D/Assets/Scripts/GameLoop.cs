using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private GameObject thePlayer;
    [SerializeField] private int fuelAmount;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Hud>().fuelTotal.text = "" + 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && fuelAmount <6000)
        {
            fuelAmount += 750;
            gameObject.GetComponent<Hud>().fuelTotal.text = fuelAmount.ToString();
        }
    }
}
