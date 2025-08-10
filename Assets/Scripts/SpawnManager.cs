using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _spawnRate = 2.0f;
    [SerializeField]
    private GameObject _container;
    private Coroutine _spawnCoroutine;

    void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), 7.0f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _container.transform;
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    public void OnPlayerIsDead()
    {
        StopCoroutine(_spawnCoroutine);
    }
}
