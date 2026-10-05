using System.Collections.Generic;
using UnityEngine;

public class PrefabPool
{
    private Transform _parent;
    private GameObject _prefab;
    private Queue<GameObject> _prefabPool = new Queue<GameObject>();

    public PrefabPool(GameObject prefab, int basePoolSize, Transform parent)
    {
        _parent = parent;
        _prefab = prefab;
        InitializePool(basePoolSize);
    }
    public GameObject Get()
    {
        if (_prefabPool.Count == 0)
        {
            CreateInstance();
        }
        GameObject obj = _prefabPool.Dequeue();
        obj.SetActive(true);
        return obj;
    }
    public void Release(GameObject obj)
    {
        obj.transform.SetParent(_parent);
        obj.SetActive(false);
        _prefabPool.Enqueue(obj);
    }
    private void InitializePool(int basePoolSize)
    {
        for (int i = 0; i < basePoolSize; i++)
        {
            CreateInstance();
        }
    }
    private void CreateInstance()
    {
        GameObject obj = GameObject.Instantiate(_prefab, _parent);
        obj.SetActive(false);
        _prefabPool.Enqueue(obj);
    }
}
