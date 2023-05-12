using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTotal;
    [SerializeField] private TextMeshProUGUI _timeTotal;
    [SerializeField] private TextMeshProUGUI _fuelTotal;
    [SerializeField] private TextMeshProUGUI _shipAltitudeCurrent;
    [SerializeField] private TextMeshProUGUI _shipHorSpeedCurrent;
    [SerializeField] private TextMeshProUGUI _shipVerSpeedCurrent;
    [SerializeField] private TextMeshProUGUI _horSpeedArrow;
    [SerializeField] private TextMeshProUGUI _verSpeedArrow;
    [SerializeField] private TextMeshProUGUI _insertCoins;
    [SerializeField] private Ship _theShip;

    private float _timerBlink;
    private float _scoreNow;

    public void SetTimeTotal(string time) => _timeTotal.text = time;
    public void SetFuelTotal(string fuel) => _fuelTotal.text = fuel;
    public void SetAltitudeCurrent(string altitude) => _shipAltitudeCurrent.text = altitude;
    public void SetShipHorSpeedCurrent(string horSpeed) => _shipHorSpeedCurrent.text = horSpeed;
    public void SetShipVerSpeedCurrent(string verSpeed) => _shipVerSpeedCurrent.text = verSpeed;
    public void SetHorSpeedArrow(string horArrow) => _horSpeedArrow.text = horArrow;
    public void SetVerSpeedArrow(string verArrow) => _verSpeedArrow.text = verArrow;

    private void Start()
    {
        _fuelTotal.text = "0000";
        _timerBlink = 0;
    }

    private void Update()
    {
        CoinBlink();
        CurrentScore();
    }

    private void CoinBlink()
    {
        _timerBlink += Time.deltaTime;

        if (_timerBlink < 1.5f)
        {
            _insertCoins.enabled = true;
        }
        else if (_timerBlink > 1.5f && _timerBlink < 2f)
        {
            _insertCoins.enabled = false;
        }
        else
        {
            _timerBlink = 0;
        }
    }

    private void CurrentScore()
    {
        _scoreNow = _theShip.GetCurrentScore;

        if (_scoreNow == 0)
        {
            _scoreTotal.text = "0000";
        }
        else if (_scoreNow > 0 && _scoreNow < 100)
        {
            _scoreTotal.text = "00" + _scoreNow;
        }
        else if (_scoreNow > 100 && _scoreNow < 1000)
        {
            _scoreTotal.text = "0" + _scoreNow;
        }
    }
}
