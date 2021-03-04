using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabs;
    public List<GameObject> Prefabs => prefabs;
    int prefabCount = 1;
    public Dictionary<string, List<GameObject>> prefabDictionary;
    static PoolManager instance;
    public static PoolManager Instance { get { return instance;}}

    private void Awake()
    {
        instance = this;
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
