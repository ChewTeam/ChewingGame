using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Initalize(GameObject template)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(template, _container.transform);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }

    protected void Initalize(GameObject[] templates)
    {
        for (int i = 0; i < _capacity; i++)
        {
            int randomIndex = Random.Range(0, templates.Length);
            GameObject spawned = Instantiate(templates[randomIndex], _container.transform);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }
}
