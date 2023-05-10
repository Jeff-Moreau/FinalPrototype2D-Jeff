using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class RandomBonus : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bonusText;
    [SerializeField] private int maxRandom;
    [SerializeField] private int minRandom;
    public int bonusRandom;
    private float myTime;

    // Start is called before the first frame update
    void Start()
    {
        bonusRandom = Random.Range(minRandom, maxRandom);
        MakeDecision();
    }

    private void Update()
    {
        myTime += Time.deltaTime;

        if (bonusRandom > 1)
        {
            if (myTime < 0.5f)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else if (myTime > 0.5f && myTime < 1)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                myTime = 0;
            }
        }

        if (bonusRandom <= 1)
        {
            gameObject.SetActive(false);
        }

    }

    private void MakeDecision()
    {

        if (bonusRandom <= 1) 
        {
            bonusText.text = "";
        }
        else if (bonusRandom == 2)
        {
            bonusText.text = "2X";
        }
        else if (bonusRandom == 3)
        {
            bonusText.text = "3X";
        }
        else if (bonusRandom == 4)
        {
            bonusText.text = "4X";
        }
        else if (bonusRandom >= 5)
        {
            bonusText.text = "<color=red>5X</color>";
        }
    }
}
