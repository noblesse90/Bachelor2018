using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool> {

    [SerializeField] private GameObject[] _objectPrefabs;

    private readonly List<GameObject> _pooledObjects = new List<GameObject>();

	public GameObject GetObject(string type)
    {

        foreach(GameObject go in _pooledObjects)
        {
            if (go.name != type || go.activeInHierarchy) continue;
            go.SetActive(true);
            return go;
        }


        foreach (var prefab in _objectPrefabs)
        {
            if (prefab.name != type) continue;
            GameObject newObject = Instantiate(prefab);
            _pooledObjects.Add(newObject);
            newObject.name = type;
            return newObject;
        }

        return null;
    }


    public void ReleaseObject(GameObject go)
    {
        go.SetActive(false);
    }
}
