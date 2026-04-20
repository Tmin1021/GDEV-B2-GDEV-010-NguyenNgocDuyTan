using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private int poolSize = 5;
    [SerializeField] private GameObject prefab;

    private List<GameObject> _pool;

    void Awake()
    {
        _pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }

    public GameObject CreateNewObject()
    {
        if (prefab == null)
        {
            return null;
        }

        GameObject obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        _pool.Add(obj);
        return obj;
    }

    public GameObject GetPooledObject()
    {
        foreach (var obj in _pool)
        {
            if (!obj.activeSelf)
            {
                return obj;
            }
        }

        return CreateNewObject();
    }
}
