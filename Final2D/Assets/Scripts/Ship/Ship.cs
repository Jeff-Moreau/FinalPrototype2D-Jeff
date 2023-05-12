using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

    [SerializeField] private AudioSource _playerSoundFXSource;
    [SerializeField] private AudioSource _shipWarningSoundFXSource;
    [SerializeField] private AudioSource _gameBackgroundSound;
    [SerializeField] private AudioClip[] _soundFX;

    private DebugLogger _debugLogger;
    private Rigidbody2D _shipRigidBody;
    private Camera _gameCamera;
    private Transform _shipRotation;

    private int _currentScore;
    private int _scoreMultiplier;
    private int _baseScore;

    private int _shipFuelTankCapacity;
    private int _shipCurrentFuelInTank;
    private int _shipFuelTankWarningLevel;
    private int _shipFuelTankEmpty;

    private float _shipCurrentAltitude;
    private float _shipAltitude;
    private float _shipVerVelocity;

    private Vector3 _initialShipPosition;
    private Vector3 _initialCameraPosition;
    private bool _goodToLand = false;

    public int GetCurrentScore => _currentScore;
    public float GetShipAltitude => _shipAltitude;
    public float GetShipVerVelocity => _shipVerVelocity;
    public int GetShipFuelCapacity => _shipFuelTankCapacity;
    public int GetShipCurrentFuelInTank => _shipCurrentFuelInTank;
    public void SetShipCurrentFuelInTank(int fuel) => _shipCurrentFuelInTank = fuel;
    void Start()
    {

        _debugLogger = GetComponent<DebugLogger>();
        _shipRigidBody = GetComponent<Rigidbody2D>();
        _shipRotation = GetComponent<Transform>();
        _gameCamera = Camera.main;
        _initialCameraPosition = _gameCamera.transform.position;
        _initialShipPosition = transform.position;
        _shipRigidBody.freezeRotation = false;
        _shipCurrentFuelInTank = 750;
        _shipFuelTankCapacity = 6000;
        _shipFuelTankWarningLevel = 100;
        _shipFuelTankEmpty = 0;
        _baseScore = 50;
    }

    void Update()
    {
        _shipVerVelocity = _shipRigidBody.velocity.y;

        if (_shipCurrentFuelInTank > _shipFuelTankEmpty && _shipCurrentFuelInTank <= _shipFuelTankWarningLevel)
        {
            if (!_shipWarningSoundFXSource.isPlaying)
            {
                _shipWarningSoundFXSource.PlayOneShot(_soundFX[2]);
                DebugToCon("Warning Beep");
            }
        }
        else if (_shipCurrentFuelInTank <= _shipFuelTankEmpty)
        {
            DebugToCon("The Ship Has Crashed");
            _playerSoundFXSource.PlayOneShot(_soundFX[1]);
            _gameBackgroundSound.Stop();
            gameObject.SetActive(false);
        }

        ShipTankIsEmpty();
    }

    private void ShipTankIsEmpty()
    {
        if (_shipCurrentFuelInTank <= 0)
        {
            _shipCurrentFuelInTank = 0;
        }
    }

    private void FixedUpdate()
    {
        RaycastHit2D mountain = Physics2D.Raycast(transform.position, Vector2.down, 500, 3);
        _shipCurrentAltitude = Mathf.Floor(_shipAltitude * 420);

        if (mountain.collider != null)
        {
            _shipAltitude = Mathf.Abs(mountain.point.y - transform.position.y);
        }

        if (_shipCurrentAltitude < 400)
        {
            _gameCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            _gameCamera.orthographicSize = 1.5f;
        }
        else
        {
            _gameCamera.transform.position = _initialCameraPosition;
            _gameCamera.orthographicSize = 5;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Mountain")
        {
            if (!_goodToLand)
            {
                _currentScore += (_baseScore - 30);
                ShipExplode();
            }
            else
            {
                if (Mathf.Floor(_shipVerVelocity * 100) < -10)
                {
                    _currentScore += (_baseScore / 2);
                    ShipExplode();
                }
                else
                {
                    _currentScore += (_baseScore * _scoreMultiplier);

                    ShipLanded();
                }
            }
        }
    }

    private void ShipLanded()
    {
        DebugToCon("Ship Has Successfuly Landed");
        transform.position = _initialShipPosition;
        _shipRigidBody.freezeRotation = true;
        _shipRotation.eulerAngles = new Vector3(0, 0, 0);
     }

    private void ShipExplode()
    {
        DebugToCon("The Ship Has Crashed");
        transform.position = _initialShipPosition;
        _shipRigidBody.freezeRotation = true;
        _shipRotation.eulerAngles = new Vector3(0, 0, 0);
        _shipCurrentFuelInTank -= _shipFuelTankWarningLevel;
        _playerSoundFXSource.PlayOneShot(_soundFX[1]);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Bonus"))
        {
            DebugToCon("You are touching a Bonus");
            _scoreMultiplier = collision.GetComponent<RandomBonus>().GetBonusRandom;
            _goodToLand = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bonus"))
        {
            _goodToLand = false;
        }
    }
    private void DebugToCon(object message)
    {
        if (_debugLogger)
        {
            _debugLogger.DebugCon(message, this);
        }
    }
}
