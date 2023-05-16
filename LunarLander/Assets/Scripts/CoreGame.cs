using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGame : MonoBehaviour
{
    [SerializeField] private GameObject _theShip;
    [SerializeField] private GameObject _theHUD;

    private UserInput _userInput;
    private Ship _ship;
    private Hud _writeToHud;
    private Rigidbody2D _shipRigidBody;

    private float _shipHorVelocity;
    private float _shipVerVelocity;
    private float _gameTimeMinutes;
    private float _gameTimeSeconds;
    private float _gameTime;
    private float _gameSeconds;
    private float _gameMinutes;
    private float _shipCurrentAltitude;
    private string _newSeconds;
    
    void Start()
    {
        _userInput = GetComponent<UserInput>();
        _ship = _theShip.GetComponent<Ship>();
        _writeToHud = _theHUD.GetComponent<Hud>();
        _shipRigidBody = _theShip.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _gameTime = Time.deltaTime;

        _writeToHud.SetFuelTotal(_ship.GetShipCurrentFuelInTank.ToString());
        _shipCurrentAltitude = _ship.GetShipAltitude;
        _writeToHud.SetAltitudeCurrent(Mathf.Floor(_shipCurrentAltitude * 420).ToString());

        HorArrows();
        VerArrows();
        SettingTime();

        if (_userInput.GetInsertCoin && _ship.GetShipCurrentFuelInTank <= 5250)
        {
            int fuelPerCoin = 750;
            _ship.SetShipCurrentFuelInTank(_ship.GetShipCurrentFuelInTank + fuelPerCoin);
        }

    }

    private void SettingTime()
    {
        _gameTimeSeconds += _gameTime;
        _gameTimeMinutes += _gameTime;

        if (_gameTimeSeconds < 10)
        {
            _gameSeconds = Mathf.Floor(_gameTimeSeconds);
            _newSeconds = "0" + _gameSeconds;
        }
        else if (_gameTimeSeconds >= 10 && _gameTimeSeconds < 60)
        {
            _gameSeconds = Mathf.Floor(_gameTimeSeconds);
            _newSeconds = _gameSeconds.ToString();
        }
        else
        {
            _gameTimeSeconds = 0;
        }

        if (_gameTimeMinutes >= 60)
        {
            _gameMinutes += 1;
            _gameTimeMinutes = 0;
        }

        _writeToHud.SetTimeTotal("0" + _gameMinutes + ":" + _newSeconds);
    }

    private void VerArrows()
    {
        _shipVerVelocity = _shipRigidBody.velocity.y;
        _writeToHud.SetShipVerSpeedCurrent(Mathf.Floor(_shipVerVelocity * 100).ToString());

        if (_shipVerVelocity > 0)
        {
            _writeToHud.SetVerSpeedArrow("↑");
        }
        else if (_shipVerVelocity < 0)
        {
            _writeToHud.SetVerSpeedArrow("↓");
        }
        else if (_shipVerVelocity == 0 || _shipVerVelocity == 1)
        {
            _writeToHud.SetVerSpeedArrow("");
        }
    }

    private void HorArrows()
    {
        _shipHorVelocity = _shipRigidBody.velocity.x;
        _writeToHud.SetShipHorSpeedCurrent(Mathf.Floor(_shipHorVelocity * 100).ToString());

        if (_shipHorVelocity > 0)
        {
            _writeToHud.SetHorSpeedArrow("→");
        }
        else if (_shipHorVelocity < 0)
        {
            _writeToHud.SetHorSpeedArrow("←");
        }
        else if (_shipHorVelocity == 0 || _shipHorVelocity == 1)
        {
            _writeToHud.SetHorSpeedArrow("");
        }
    }
}
