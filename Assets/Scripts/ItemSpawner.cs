using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : ObjectPool
{
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private GameObject[] _templates;
    [SerializeField] private Transform[] _spawnPoints;

    private float _elapsedTime = 0;

    private void Start()
    {
        Initalize(_templates);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _secondsBetweenSpawn)
        {
            if (TryGetObject(out GameObject item))
            {
                _elapsedTime = 0;

                int spawnPointNumber = Random.Range(0, _spawnPoints.Length);

                SetEnemy(item, _spawnPoints[spawnPointNumber].position);
            }
        }
    }

    private void SetEnemy(GameObject item, Vector3 spawnPoint)
    {
        item.SetActive(true);
        item.transform.position = spawnPoint;
    }
}
