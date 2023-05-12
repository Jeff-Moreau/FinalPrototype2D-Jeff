using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class RandomBonus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bonusText;
    [SerializeField] private int _maxRandom;
    [SerializeField] private int _minRandom;

    private SpriteRenderer _bonusSprite;
    private int _bonusRandom;
    private float _myTime;

    public int GetBonusRandom => _bonusRandom;

    void Start()
    {
        _bonusSprite = GetComponent<SpriteRenderer>();
        _bonusRandom = Random.Range(_minRandom, _maxRandom);
        CurrentBonus();
    }

    private void Update()
    {
        BlinkLandingLocation();

        if (_bonusRandom <= 1)
        {
            gameObject.SetActive(false);
        }
    }

    private void BlinkLandingLocation()
    {
        _myTime += Time.deltaTime;

        if (_bonusRandom > 1)
        {
            if (_myTime < 0.5f)
            {
                _bonusSprite.enabled = true;
            }
            else if (_myTime > 0.5f && _myTime < 1)
            {
                _bonusSprite.enabled = false;
            }
            else
            {
                _myTime = 0;
            }
        }
    }

    private void CurrentBonus()
    {

        if (_bonusRandom <= 1) 
        {
            _bonusText.text = "";
        }
        else if (_bonusRandom == 2)
        {
            _bonusText.text = "2X";
        }
        else if (_bonusRandom == 3)
        {
            _bonusText.text = "3X";
        }
        else if (_bonusRandom == 4)
        {
            _bonusText.text = "4X";
        }
        else if (_bonusRandom >= 5)
        {
            _bonusText.text = "<color=red>5X</color>";
        }
    }
}
