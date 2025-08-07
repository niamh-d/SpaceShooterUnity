using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private float _speed = 3.5f;

    private const float _bottomBoundary = -3.8f;
    private const float _topBoundary = 0;
    private const float _rightBoundary = 9.2f;
    private const float _leftBoundary = -9.2f;
    private InputAction moveAction;
    private InputAction fireAction;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Player/Move");

        fireAction = InputSystem.actions.FindAction("Player/Fire");
        fireAction.performed += ctx => Fire();
    }

    private void Fire()
    {
        Instantiate(laserPrefab, transform.position, Quaternion.identity);
    }

    void Update()
    {
        CalcMovement();
    }

    void CalcMovement()
    {
        Vector3 movementDir = moveAction.ReadValue<Vector2>();

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
}