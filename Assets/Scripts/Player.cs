using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _maxHealth = 3;

    private const float _bottomBoundary = -3.8f;
    private const float _topBoundary = 0;
    private const float _rightBoundary = 9.2f;
    private const float _leftBoundary = -9.2f;

    private Vector3 _laserSpawnOffset = new Vector3(0, 0.8f, 0);

    private InputAction _moveAction;
    private InputAction _fireAction;

    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Player/Move");

        _fireAction = InputSystem.actions.FindAction("Player/Fire");
        _fireAction.performed += ctx => Fire();
    }

    void Update()
    {
        CalcMovement();
    }

    void CalcMovement()
    {
        Vector3 movementDir = _moveAction.ReadValue<Vector2>();

        transform.transform.Translate(movementDir * _speed * Time.deltaTime);

        BoundMovement();
    }

    void BoundMovement()
    {
        transform.position = new Vector3(transform.position.x,
            Mathf.Clamp(transform.position.y, _bottomBoundary, _topBoundary), 0);

        if (transform.position.x > _rightBoundary)
        {
            transform.position = new Vector3(_leftBoundary, transform.position.y, 0);
        }
        else if (transform.position.x < _leftBoundary)
        {
            transform.position = new Vector3(_rightBoundary, transform.position.y, 0);
        }
    }

    private void Fire()
    {
        if (Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position + _laserSpawnOffset, Quaternion.identity);
        }
    }

    public void TakeDamage()
    {
        _maxHealth--;
        if (_maxHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}