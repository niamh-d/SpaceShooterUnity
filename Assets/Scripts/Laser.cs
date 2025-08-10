using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField]
    private float _speed = 8f;

    private float deadZoneTop = 7f;

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > deadZoneTop)
        {
            Destroy(gameObject);
        }
    }
}
