using UnityEngine;
using UnityEngine.Pool;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _prefabEnemy;
    [SerializeField] private Transform _target;

    private int _startSizePool = 5;
    private int _maxSizePool = 10;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_prefabEnemy),
            actionOnGet: (obj) => ActivateEnemy(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _startSizePool,
            maxSize: _maxSizePool);
    }

    public void GetEnemy() =>
        _pool.Get();

    private void Release(Enemy enemy)
    {
        enemy.Died -= Release;
        _pool.Release(enemy);
    }

    private void ActivateEnemy(Enemy enemy)
    {
        enemy.Died += Release;

        enemy.transform.position = transform.position;
        enemy.Rigidbody.velocity = Vector3.zero;
        enemy.SetTarget(_target);

        enemy.gameObject.SetActive(true);
    }

    private void OnDestroy() =>
        _pool.Dispose();
}