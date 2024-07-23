using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private Vector3 _direction;
    private float _speed = 2f;

    public Rigidbody Rigidbody => _rigidbody;

    private void Update()
    {
        if (_direction != null)
        {
            Move();
            Rotate();
        }
    }

    public void SetDirection(Vector3 direction) =>
        _direction = direction;

    private void Rotate() =>
        transform.rotation = Quaternion.LookRotation(_direction);

    private void Move() =>
        transform.position += _direction * _speed * Time.deltaTime;
}
