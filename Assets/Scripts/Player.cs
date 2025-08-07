using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public InputActionReference move;

    [SerializeField]
    private float _speed = 3.5f;

    private const float _bottomBoundary = -3.8f;
    private const float _topBoundary = 0;
    private const float _rightBoundary = 9.2f;
    private const float _leftBoundary = -9.2f;

    void Start()
    {

    }

    void Update()
    {
        CalcMovement();
    }

    void CalcMovement()
    {
        Vector3 movementDir = move.action.ReadValue<Vector2>();

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