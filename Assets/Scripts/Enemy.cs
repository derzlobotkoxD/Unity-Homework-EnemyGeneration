using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed = 2f;

    private Transform _target;

    public event UnityAction<Enemy> Died;

    public Rigidbody Rigidbody => _rigidbody;

    private void Update()
    {
        Move();
        Rotate();
    }

    public void SetTarget(Transform target) =>
        _target = target;

    public void Die() =>
        Died?.Invoke(this);

    private void Rotate()
    {
        Vector3 position = _target.position;
        position.y = 0f;

        transform.LookAt(position);
    }

    private void Move() =>
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
}
