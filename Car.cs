using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Car : MonoBehaviour
{
    private Rigidbody _rb;
    public float _rotationSpeed = 5;

    private float _currentSpeed = 0;
    private float _maxSpeed = 35;

    [SerializeField] private Inputs _movementInputs;
    private float _realSpeed;

    // public float keepInCheck = _currentSpeed;

    public bool activeBoost = false;
    public bool checkBoost = false;
    public bool _isLeftCar = false;

    private float curTime;
    private float maxTime = 5;

    public float _lapTime;
    public int _lap = 1;

    Vector3 rotation = new Vector3(0, 30, 0);

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Physics.IgnoreLayerCollision(7, 7);

        if (_isLeftCar)
        {
            //The laptimer
            if (GameManager.instance._canTime)
            {
                _lapTime += 1 * Time.deltaTime;
                UIManager.instance.UpdateLapTimeLeft(_lapTime);
            }

            if (Input.GetKey(_movementInputs._forwardMovementLeft) && GameManager.instance._canDrive)
            {
                _currentSpeed = Mathf.Lerp(_currentSpeed, _maxSpeed, Time.deltaTime * 0.5f);
            }
            else if (Input.GetKey(_movementInputs._backwardsMovementLeft) && GameManager.instance._canDrive)
            {
                _currentSpeed = Mathf.Lerp(_currentSpeed, -_maxSpeed / 1.75f, 1f * Time.deltaTime);
            }
            else
            {
                _currentSpeed = Mathf.Lerp(_currentSpeed, 0, Time.deltaTime * 1.5f);
            }

            Vector3 vel = transform.forward * _currentSpeed;
            vel.y = _rb.velocity.y; //gravity
            _rb.velocity = vel;

            UIManager.instance.SpeedUpdateLeft(_currentSpeed);

            if (Input.GetKey(_movementInputs._rightMovementLeft) && GameManager.instance._canDrive)
            {
                Quaternion deltaRotationRight = Quaternion.Euler(rotation * Time.deltaTime * _rotationSpeed);
                _rb.MoveRotation(_rb.rotation * deltaRotationRight);
            }

            if (Input.GetKey(_movementInputs._leftMovementLeft) && GameManager.instance._canDrive)
            {
                Quaternion deltaRotationLeft = Quaternion.Euler(-rotation * Time.deltaTime * _rotationSpeed);
                _rb.MoveRotation(_rb.rotation * deltaRotationLeft);
            }
        }
        else if(!_isLeftCar)
        {
            //The laptimer
            if (GameManager.instance._canTime)
            {
                _lapTime += 1 * Time.deltaTime;
                UIManager.instance.UpdateLapTimeRight(_lapTime);
            }

            if (Input.GetKey(_movementInputs._forwardMovementRight) && GameManager.instance._canDrive)
            {
                _currentSpeed = Mathf.Lerp(_currentSpeed, _maxSpeed, Time.deltaTime * 0.5f);
            }
            else if (Input.GetKey(_movementInputs._backwardsMovementRight) && GameManager.instance._canDrive)
            {
                _currentSpeed = Mathf.Lerp(_currentSpeed, -_maxSpeed / 1.75f, 1f * Time.deltaTime);
            }
            else
            {
                _currentSpeed = Mathf.Lerp(_currentSpeed, 0, Time.deltaTime * 1.5f);
            }

            Vector3 vel = transform.forward * _currentSpeed;
            vel.y = _rb.velocity.y; //gravity
            _rb.velocity = vel;

            UIManager.instance.SpeedUpdateRight(_currentSpeed);

            if (Input.GetKey(_movementInputs._rightMovementRight) && GameManager.instance._canDrive)
            {
                Quaternion deltaRotationRight = Quaternion.Euler(rotation * Time.deltaTime * _rotationSpeed);
                _rb.MoveRotation(_rb.rotation * deltaRotationRight);
            }

            if (Input.GetKey(_movementInputs._leftMovementRight) && GameManager.instance._canDrive)
            {
                Quaternion deltaRotationLeft = Quaternion.Euler(-rotation * Time.deltaTime * _rotationSpeed);
                _rb.MoveRotation(_rb.rotation * deltaRotationLeft);
            }
        }

        if (activeBoost)
        {
            if (curTime <= maxTime)
            {
                curTime += 1 * Time.deltaTime;
                Boost();
            }
            else
            {
                curTime = 0;
                _maxSpeed -= 20f;
                activeBoost = false;
            }
        }
    }

    public void Boost()
    {
        _maxSpeed += 20f;
    }
}
