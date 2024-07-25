using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField][Range(0f, 3f)] private float _delay = 2f;

    private void Start() =>
        StartCoroutine(Spawn());

    private SpawnPoint GetRandomSpawnPoint() => 
        _spawnPoints[Random.Range(0, _spawnPoints.Count)];

    private IEnumerator Spawn()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            SpawnPoint point = GetRandomSpawnPoint();
            point.GetEnemy();

            yield return wait;
        }
    }
}