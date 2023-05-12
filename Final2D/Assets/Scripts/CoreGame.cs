using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGame : MonoBehaviour
{
    [SerializeField] private GameObject _theShip;
    [SerializeField] private GameObject _theHUD;

    private UserInput _userInput;
    private Ship _shipAltitude;
    private Hud _writeToHud;
    private Rigidbody2D _shipRigidBody;

    private int _fuelAmount;
    private float _playerHorVelocity;
    private float _playerVerVelocity;
    private float _gameTimeMinutes;
    private float _gameTimeSeconds;
    private float _gameTime;
    private float _gameSeconds;
    private float _gameMinutes;
    private float _shipCurrentAltitude;
    private string _newSeconds;
    

    public int GetFuelAmount => _fuelAmount;
    public void SetFuelAmount(int changeFuel) => _fuelAmount = changeFuel;
    void Start()
    {
        _userInput = GetComponent<UserInput>();
        _shipAltitude = _theShip.GetComponent<Ship>();
        _writeToHud = _theHUD.GetComponent<Hud>();
        _shipRigidBody = _theShip.GetComponent<Rigidbody2D>();

        _fuelAmount = 750;
        _gameMinutes = 0;
    }

    void Update()
    {
        _gameTime = Time.deltaTime;

        _writeToHud.SetFuelTotal(_fuelAmount.ToString());
        _shipCurrentAltitude = _shipAltitude.GetShipAltitude;
        _writeToHud.SetAltitudeCurrent(Mathf.Floor(_shipCurrentAltitude * 420).ToString());

        HorArrows();
        VerArrows();
        SettingTime();

        if (_userInput.GetInsertCoin && _fuelAmount < 5250)
        {
            _fuelAmount += 750;
        }

        if (_fuelAmount <= 0)
        {
            _fuelAmount = 0;
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
        _playerVerVelocity = _shipRigidBody.velocity.y;
        _writeToHud.SetShipVerSpeedCurrent(Mathf.Floor(_playerVerVelocity * 100).ToString());

        if (_playerVerVelocity > 0)
        {
            _writeToHud.SetVerSpeedArrow("↑");
        }
        else if (_playerVerVelocity < 0)
        {
            _writeToHud.SetVerSpeedArrow("↓");
        }
        else if (_playerVerVelocity == 0 || _playerVerVelocity == 1)
        {
            _writeToHud.SetVerSpeedArrow("");
        }
    }

    private void HorArrows()
    {
        _playerHorVelocity = _shipRigidBody.velocity.x;
        _writeToHud.SetShipHorSpeedCurrent(Mathf.Floor(_playerHorVelocity * 100).ToString());

        if (_playerHorVelocity > 0)
        {
            _writeToHud.SetHorSpeedArrow("→");
        }
        else if (_playerHorVelocity < 0)
        {
            _writeToHud.SetHorSpeedArrow("←");
        }
        else if (_playerHorVelocity == 0 || _playerHorVelocity == 1)
        {
            _writeToHud.SetHorSpeedArrow("");
        }
    }
}
