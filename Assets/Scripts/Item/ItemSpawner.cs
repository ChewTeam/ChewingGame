using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : ObjectPool
{
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private Transform _targetPointStorage;
    [SerializeField] private Transform _exitPoint;
    [SerializeField] private GameObject[] _templates;
    [SerializeField] private Transform[] _spawnPoints;


    private float _elapsedTime = 0;
    private ItemMovement _itemMovement;

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

                SetItem(item, _spawnPoints[spawnPointNumber].position);
            }
        }
    }

    private void SetItem(GameObject item, Vector3 spawnPoint)
    {
        item.SetActive(true);
        item.transform.position = spawnPoint;
        _itemMovement = item.GetComponent<ItemMovement>();
        _itemMovement.SetExitPoint(_exitPoint);
        _itemMovement.SetTargetPointStorage(_targetPointStorage);
        
    }
}
