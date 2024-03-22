using System.Collections.Generic;
using MidniteOilSoftware;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _prefabs;
    [SerializeField] [Range(0f, 5f)] private float _spawnDelay = 0.1f;
    [SerializeField] [Range(10f, 30f)] private float _launchForce = 20;

    private Vector3 _spawnPosition;

    private void Awake()
    {
        _spawnPosition = transform.position;
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(SpawnRandomObject), 0f, _spawnDelay);
    }

    private void SpawnRandomObject()
    {
        GameObject prefab = _prefabs[Random.Range(0, _prefabs.Count)];
        _spawnPosition = Vector3.one;
        _spawnPosition.x = Random.Range(-1f, 1f);
        _spawnPosition.z = Random.Range(-1f, 1f);
        GameObject go = ObjectPoolManager.SpawnGameObject(prefab, _spawnPosition, Quaternion.identity);
        go.GetComponent<Rigidbody>()?.AddForce(transform.up * _launchForce, ForceMode.Impulse);
    }
}
