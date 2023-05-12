using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class RandomBonus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bonusText;
    [SerializeField] private int maxRandom;
    [SerializeField] private int minRandom;

    private SpriteRenderer bonusSprite;
    private int bonusRandom;
    private float myTime;

    public int GetBonusRandom => bonusRandom;

    void Start()
    {
        bonusSprite = GetComponent<SpriteRenderer>();
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
                bonusSprite.enabled = true;
            }
            else if (myTime > 0.5f && myTime < 1)
            {
                bonusSprite.enabled = false;
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
