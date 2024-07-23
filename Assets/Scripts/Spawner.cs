using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Enemy _prefabEnemy;
    [SerializeField] private Destroyer _destroyer;

    [SerializeField][Range(0f, 3f)] private float _delay = 2f;
    [SerializeField][Range(0f, 20f)] private int _startSizePool = 5;
    [SerializeField][Range(0f, 20f)] private int _maxSizePool = 10;

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

    private void OnEnable() =>
        _destroyer.EnemyDestroyed += Release;

    private void OnDisable() =>
        _destroyer.EnemyDestroyed -= Release;

    private void Start() =>
        StartCoroutine(Spawn());

    private void GetEnemy() =>
        _pool.Get();

    private void Release(Enemy enemy) =>
        _pool.Release(enemy);

    private void ActivateEnemy(Enemy enemy)
    {
        Vector3 direction = Random.onUnitSphere;
        direction.y = 0;

        enemy.Rigidbody.velocity = Vector3.zero;
        enemy.transform.position = GetSpawnPoint();
        enemy.transform.rotation = Quaternion.LookRotation(direction);
        enemy.gameObject.SetActive(true);
        enemy.SetDirection(direction.normalized);
    }

    private Vector3 GetSpawnPoint()
    {
        Transform point = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

        return point.position;
    }

    private IEnumerator Spawn()
    {
        while (enabled)
        {
            GetEnemy();
            yield return new WaitForSeconds(_delay);
        }
    }
}