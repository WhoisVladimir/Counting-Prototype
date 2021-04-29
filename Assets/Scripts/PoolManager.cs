using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singletone<PoolManager>
{
    [SerializeField] List<GameObject> prefabs;
    public List<GameObject> Prefabs => prefabs;
    int prefabCount = 1;
    public Dictionary<string, List<GameObject>> prefabDictionary;

    protected override void Awake()
    {
        if (IsInitialized) DestroyImmediate(gameObject);
        base.Awake();
        prefabDictionary = new Dictionary<string, List<GameObject>>();
        prefabDictionary = FillDictionary();
    }
    Dictionary<string, List<GameObject>> FillDictionary()
    {
        foreach (var prefab in prefabs)
        {
            string prefabName = prefab.name;
            List<GameObject> pool = GeneratePool(prefabCount, prefab);
            prefabDictionary.Add(prefabName, pool);
        }
        return prefabDictionary;
    }
    public List<GameObject> GeneratePool(int amount, GameObject prefab)
    {
        List<GameObject> bulletPool = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            bulletPool.Add(obj);
        }
        return bulletPool;
    }

    public GameObject GetPoolObject(string keyName)
    {
        List<GameObject> pool = prefabDictionary[keyName];

        foreach (var item in pool)
        {
            if (!item.activeInHierarchy) return item;
        }
        GameObject obj = prefabs.Find(item => item.name.Equals(keyName));
        obj = Instantiate(obj);
        pool.Add(obj);
        return obj;
    }
}
