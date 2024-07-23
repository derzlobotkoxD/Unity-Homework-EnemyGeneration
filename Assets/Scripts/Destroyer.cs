using UnityEngine;
using UnityEngine.Events;

public class Destroyer : MonoBehaviour
{
    public event UnityAction<Enemy> EnemyDestroyed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
            if (EnemyDestroyed != null)
                EnemyDestroyed.Invoke(enemy);
            else
                Destroy(enemy);
    }
}